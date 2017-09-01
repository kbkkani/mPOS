using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace posv2
{
    public partial class FormConfirmLogout : Form
    {
        private db con;
        double _totalSale, _totalDiscount, _totalServiceCharge, _totalcardsale, _totalcashsale;
        DataTable _cardWiseSale, _voidItems, _categorySale, _cardSaleShift, _cashSaleShift;
        int _guestCount;
        public FormConfirmLogout()
        {
            InitializeComponent();
        }

        private void btn_doSleep_Click(object sender, EventArgs e)
        {
            Form_display.logoutStatus = false;
            this.Close();
        }

        //zreport data

        //today total sale
        void getTotalSale()
        {
            DataTable result;
            con = new db();
            string query = "SELECT (IF(SUM(order_details.subtotal)>0,FORMAT(SUM(order_details.subtotal),2),FORMAT(0,2))) AS totalsale FROM `order_details` WHERE date(order_details.added) = date(CURDATE()) AND order_details.online=1";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _totalSale = double.Parse(result.Rows[0][0].ToString());

        }

        //card wise sale
        void getCardwiseSale()
        {
            DataTable result;
            con = new db();
            string query = "SELECT COUNT(order_details.id) AS itemcount,(SUM(order_details.subtotal)) AS cardsale, (IF(paymentdetails.cardtype='','CASH',paymentdetails.cardtype)) AS cardtype FROM order_details JOIN orders ON orders.id=order_details.order_id JOIN paymentdetails ON paymentdetails.orders_id = order_details.order_id WHERE date(order_details.added) = CURDATE() AND order_details.online = 1 GROUP BY paymentdetails.cardtype ";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _cardWiseSale = result;
        }

        //get void items
        void getVoidItems()
        {
            DataTable result;
            con = new db();
            string query = "SELECT products.name,order_details.qty,order_details.subtotal,order_details.product_id AS itemid FROM `order_details` JOIN products ON products.id = order_details.product_id WHERE date(order_details.added) = date(CURDATE()) AND order_details.online = 0 ";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _voidItems = result;
        }

        //get category sale
        void getCategorySale()
        {
            DataTable result;
            con = new db();
            string query = "SELECT SUM(order_details.subtotal) AS sale,categories.name, COUNT(order_details.product_id) AS itemcount FROM order_details JOIN products ON products.id = order_details.product_id JOIN categories ON categories.id = products.category_id WHERE date(order_details.added) = CURDATE() AND order_details.online = 1 GROUP BY categories.id";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _categorySale = result;
        }

        //get guest count
        void getGuestCount()
        {
            DataTable result;
            con = new db();
            string query = "SELECT (IF(SUM(orders.guest) IS NULL,0,SUM(orders.guest))) AS guestcount FROM order_details JOIN orders ON orders.id = order_details.order_id WHERE date(order_details.added) = date(CURDATE())";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();

            if (result == null)
            {
                _guestCount = 0;
            }
            else
            {
                _guestCount = int.Parse(result.Rows[0][0].ToString());
            }

        }

        //get total discount and servicecharge


        //card sale shift
        void getCardSaleShift()
        {
            DataTable result;
            con = new db();
            string query = "SELECT DISTINCT order_details.shift_no, (SELECT group_concat(DISTINCT order_details.order_id) FROM order_details) as order_id, (SELECT SUM(od.subtotal) FROM order_details od JOIN paymentdetails p WHERE date(od.added) = date(CURDATE()) AND od.online = 1 AND od.shift_no = order_details.shift_no AND p.orders_id = od.order_id) AS cardsale FROM order_details JOIN paymentdetails WHERE order_details.online = 1 AND paymentdetails.orders_id = order_details.order_id AND date(order_details.added) = date(CURDATE())";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _cardSaleShift = result;
        }

        //cash sale shift
        void getCashSaleShift()
        {
            DataTable result;
            con = new db();
            string query = "SELECT DISTINCT order_details.shift_no, (SELECT group_concat(DISTINCT order_details.order_id) FROM order_details) as order_id, (SELECT SUM(od.subtotal) FROM order_details od LEFT JOIN paymentdetails p ON p.orders_id = od.order_id WHERE date(od.added) = date(CURDATE()) AND od.online = 1  AND od.shift_no = order_details.shift_no AND p.orders_id IS NULL) AS cardsale FROM order_details LEFT JOIN paymentdetails ON paymentdetails.orders_id = order_details.order_id WHERE paymentdetails.orders_id IS NULL AND order_details.online = 1 AND date(order_details.added) = date(CURDATE())";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _cashSaleShift = result;
        }

        //total cash
        void getCashSale()
        {
            DataTable result;
            con = new db();
            string query = "SELECT DISTINCT order_details.shift_no, (SELECT group_concat(DISTINCT order_details.order_id) FROM order_details) as order_id, (IF((SELECT SUM(od.subtotal) FROM order_details od LEFT JOIN paymentdetails p ON p.orders_id = od.order_id WHERE date(od.added) = date(CURDATE()) AND od.online = 1 AND od.shift_no = order_details.shift_no AND p.orders_id IS NULL)IS NULL,0,(SELECT SUM(od.subtotal) FROM order_details od LEFT JOIN paymentdetails p ON p.orders_id = od.order_id WHERE date(od.added) = date(CURDATE()) AND od.online = 1 AND od.shift_no = order_details.shift_no AND p.orders_id IS NULL))) AS cardsale FROM order_details LEFT JOIN paymentdetails ON paymentdetails.orders_id = order_details.order_id WHERE paymentdetails.orders_id IS NULL AND order_details.online = 1 AND date(order_details.added) = date(CURDATE())";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();

            if (result != null)
            {
                foreach (DataRow cash in result.Rows)
                {
                    if (cash["cardsale"].ToString() != null)
                    {
                        _totalcashsale = _totalcashsale + double.Parse(cash["cardsale"].ToString());
                    }
                    else {
                        _totalcashsale = 0;
                    }
                   
                }
            }
        }






        private void btn_doShoutDown_Click(object sender, EventArgs e)
        {

            DialogResult dr = MessageBox.Show("Are you sure want to shoutdown.", "Title", MessageBoxButtons.YesNoCancel,
            MessageBoxIcon.Information);

            if (dr == DialogResult.Yes)
            {
                Form_display.logoutStatus = true;
                Form_login.zreportout = true;
                this.Close();
            }
            
        }

        private void switch_user_Click(object sender, EventArgs e)
        {
            //process logout'
            DataTable shift;
            con = new db();
            string shiftQuery = "SELECT shift.id,shift.users_id,users.username FROM `shift` JOIN users ON users.id = shift.users_id WHERE shift.shift_end IS NULL ORDER BY shift.id DESC LIMIT 1";
            con.MysqlQuery(shiftQuery);
            shift = con.QueryEx();
            con.conClose();

            if (shift.Rows.Count > 0) {
                closeShift(int.Parse(shift.Rows[0][0].ToString()));
            }

        }

        void closeShift(int shiftid) {
            con = new db();
            string q = "UPDATE shift SET shift_end = '" + DateTime.Now.ToString("yyyyMMddHHmmss") + "' WHERE shift.id = '" + shiftid + "'";
            con.MysqlQuery(q);
            con.NonQueryEx();
            con.conClose();
            Form_display.logoutStatus = true;
            panel1.Visible = true;
            Thread.Sleep(5000);
            // set login form to switch user
            Form_login.switcheUser = true;
            this.Close();
        }

        private void FormConfirmLogout_Load(object sender, EventArgs e)
        {
            panel1.Visible = false;
        }

        private void btn_xout_Click(object sender, EventArgs e)
        {
            getTotalSale();
            getCardwiseSale();
            getVoidItems();
            getCategorySale();
            getGuestCount();
            getCardSaleShift();
            getCashSaleShift();
            getCashSale();

            Xreport xreport = new Xreport(_guestCount, _totalSale, _totalcardsale, _totalcashsale, _cardWiseSale, _cardSaleShift, _cashSaleShift, _voidItems, _categorySale);
            xreport.print(SessionData._defaultPrinter);

            //Form_display.logoutStatus = true;
            //this.Close();  
        }

    }
}
