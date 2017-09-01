using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace posv2
{
    public partial class Form_login : Form
    {
        private db con;
        private string errormsg = "";
        double _totalSale,_totalDiscount,_totalServiceCharge,_totalcardsale,_totalcashsale;
        DataTable _cardWiseSale, _voidItems, _categorySale, _cardSaleShift, _cashSaleShift;
        int _guestCount;
        public Form_login()
        {
            InitializeComponent();
            
        }

        //zreport data

        //today total sale
        void getTotalSale(){
            DataTable result;
            con = new db();
            string query = "SELECT (IF(SUM(order_details.subtotal)>0,FORMAT(SUM(order_details.subtotal),2),FORMAT(0,2))) AS totalsale FROM `order_details` WHERE date(order_details.added) = date(CURDATE()) AND order_details.online=1";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _totalSale = double.Parse(result.Rows[0][0].ToString()); 
            
        }

        //card wise sale
        void getCardwiseSale() {
            DataTable result;
            con = new db();
            string query = "SELECT COUNT(order_details.id) AS itemcount,(SUM(order_details.subtotal)) AS cardsale, (IF(paymentdetails.cardtype='','CASH',paymentdetails.cardtype)) AS cardtype FROM order_details JOIN orders ON orders.id=order_details.order_id JOIN paymentdetails ON paymentdetails.orders_id = order_details.order_id WHERE date(order_details.added) = CURDATE() AND order_details.online = 1 GROUP BY paymentdetails.cardtype ";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _cardWiseSale = result;
        }

        //get void items
        void getVoidItems() {
            DataTable result;
            con = new db();
            string query = "SELECT products.name,order_details.qty,order_details.subtotal,order_details.product_id FROM `order_details` JOIN products ON products.id = order_details.product_id WHERE date(order_details.added) = date(CURDATE()) AND order_details.online = 0 ";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _voidItems = result;
        }

        //get category sale
        void getCategorySale() {
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
            else {
                _guestCount = int.Parse(result.Rows[0][0].ToString());
            }
            
        }

        //get total discount and servicecharge


        //card sale shift
        void getCardSaleShift() {
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
            //old query modify for null values//SELECT DISTINCT order_details.shift_no, (SELECT group_concat(DISTINCT order_details.order_id) FROM order_details) as order_id, (SELECT SUM(od.subtotal) FROM order_details od LEFT JOIN paymentdetails p ON p.orders_id = od.order_id WHERE date(od.added) = date(CURDATE()) AND od.online = 1  AND od.shift_no = order_details.shift_no AND p.orders_id IS NULL) AS cardsale FROM order_details LEFT JOIN paymentdetails ON paymentdetails.orders_id = order_details.order_id WHERE paymentdetails.orders_id IS NULL AND order_details.online = 1 AND date(order_details.added) = date(CURDATE())
            string query = "SELECT DISTINCT order_details.shift_no, (SELECT group_concat(DISTINCT order_details.order_id) FROM order_details) as order_id, (IF((SELECT SUM(od.subtotal) FROM order_details od LEFT JOIN paymentdetails p ON p.orders_id = od.order_id WHERE date(od.added) = date(CURDATE()) AND od.online = 1 AND od.shift_no = order_details.shift_no AND p.orders_id IS NULL)IS NULL,0,(SELECT SUM(od.subtotal) FROM order_details od LEFT JOIN paymentdetails p ON p.orders_id = od.order_id WHERE date(od.added) = date(CURDATE()) AND od.online = 1 AND od.shift_no = order_details.shift_no AND p.orders_id IS NULL))) AS cardsale FROM order_details LEFT JOIN paymentdetails ON paymentdetails.orders_id = order_details.order_id WHERE paymentdetails.orders_id IS NULL AND order_details.online = 1 AND date(order_details.added) = date(CURDATE())";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();

            if (result != null) {
                foreach (DataRow cash in result.Rows)
                {
                    _totalcashsale = _totalcashsale + double.Parse(cash["cardsale"].ToString());
                }
            }
        }

        void getTotaldiscount() {
            DataTable result;
            con = new db();
            string query = "SELECT (IF(SUM(discount_price)IS NULL,0,SUM(discount_price))) AS total_discount_price FROM (SELECT (SUM(order_details.subtotal) * orders.discount/100) AS discount_price FROM `order_details` JOIN orders WHERE order_details.order_id = orders.id AND date(order_details.added) = date(CURDATE()) GROUP BY order_details.order_id ) T";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _totalDiscount = double.Parse(result.Rows[0][0].ToString());
        }

        void getTotalServicecharge() {
            DataTable result;
            con = new db();
            string query = "SELECT (IF(SUM(serviceCharge)IS NULL,0,SUM(serviceCharge))) AS total_service_charge FROM (SELECT (SUM(order_details.subtotal) * orders.service_charge/100) AS serviceCharge FROM `order_details` JOIN orders WHERE order_details.order_id = orders.id AND date(order_details.added) = date(CURDATE()) GROUP BY order_details.order_id ) T";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _totalServiceCharge = double.Parse(result.Rows[0][0].ToString());
        }


        public static bool switcheUser = false;
        public static bool zreportout = false;
        private void btn_login_Click(object sender, EventArgs e)
        {
            if (CheckUserAvailabel()==1)
            {
                Form_display frmdisplay = new Form_display();
                this.WindowState = FormWindowState.Minimized;
                this.Hide();
                frmdisplay.ShowDialog();
                if (switcheUser)
                {
                    //MessageBox.Show(switcheUser.ToString());
                    SessionData.SetUserAuth(false);
                    SessionData.SetUserId("");
                    SessionData.setauthType("");
                    SessionData.setUser("");
                    this.Show();
                    this.WindowState = FormWindowState.Normal;
                }else {
                    //genarate z report
                    getTotalSale();
                    getCardwiseSale();
                    getVoidItems();
                    getCategorySale();
                    getGuestCount();
                    getCardSaleShift();
                    getCashSaleShift();
                    getCashSale();
                    getTotaldiscount();
                    getTotalServicecharge();

                    if (zreportout) {
                        Zreport zreport = new Zreport(_guestCount, _totalSale, _totalDiscount, _totalServiceCharge, _totalcardsale, _totalcashsale, _cardWiseSale, _cardSaleShift, _cashSaleShift, _voidItems, _categorySale);
                        zreport.print(SessionData._defaultPrinter);
                        this.Close();
                    }

  
                }
               
            }
            else if (CheckUserAvailabel() == 0)
            {
                MessageBox.Show("Invalid Authentication!");
            }
            else {
                FormMessage frmmsg = new FormMessage(errormsg);
                frmmsg.ShowDialog();
            }
        }







        private int CheckUserAvailabel()
        {
            con = new db();
            int status = 0;
            SessionData.SetMd5PasswordToConvert(textBox2.Text);
            string md5pass = SessionData.md5Password;
            DataTable users;
            DataTable shift;
            int count;
            string query = "SELECT id,username,user_type FROM users WHERE username='" + textBox1.Text + "' AND password = '" + md5pass + "' AND users.user_type IN('C','A')";
            con.MysqlQuery(query);
            users = con.QueryEx();
            count = users.Rows.Count;

            if (count > 0)
            {
                //SELECT shift.id FROM `shift` WHERE shift.users_id = 2 AND shift.shift_end > "" ORDER BY shift.id DESC
                SessionData.SetUserAuth(true);
                SessionData.SetUserId(users.Rows[0][0].ToString());
                SessionData.setauthType(users.Rows[0][2].ToString());
                SessionData.setUser(users.Rows[0][1].ToString());
                SessionData.SetTillOpenBalance(50000);
                SessionData.SetTillOpenTime(DateTime.Now.ToString("HH:mm:ss"));
                //SessionData.SetDefaultPrinter("CASHIER");
                SessionData.SetDefaultPrinter("POS-80Series");

                string shiftQuery = "SELECT shift.id,shift.users_id,users.username,shift.shift_no FROM `shift` JOIN users ON users.id = shift.users_id WHERE shift.shift_end IS NULL ORDER BY shift.id DESC LIMIT 1";
                con.MysqlQuery(shiftQuery);
                shift = con.QueryEx();
                con.conClose();

                //set shift
                if (shift.Rows.Count > 0) {
                    SessionData.SetUserShiftId(int.Parse(shift.Rows[0][0].ToString()));
                    SessionData.SetUserShiftNo(int.Parse(shift.Rows[0][3].ToString()));
                }


                if (shift.Rows.Count > 0 && shift.Rows[0][1].ToString() != SessionData.userid)
                {
                    errormsg = "Previous Shift (" + shift.Rows[0][2].ToString() + ") was not Sign Out properly. could not start a new shift for (" + SessionData.user + "). please signout last shift.";
                   
                    status = 2;
                }
                else {
                    status =  1;
                }
                return status;
            }
            else
            {
                return status;
                
            }
            
           
        }
 
    }
}
