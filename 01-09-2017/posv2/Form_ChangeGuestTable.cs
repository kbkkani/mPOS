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
    public partial class Form_ChangeGuestTable : Form
    {

        private long _orderid;
        public Form_ChangeGuestTable(long orderid)
        {
            InitializeComponent();
            _orderid = orderid;
            searchTabel();

            DataTable order;
            label4.Text = _orderid.ToString();
            db con = new db();
            con.MysqlQuery("select tabel from orders where id = '" + _orderid + "'");
            order = con.QueryEx();
            con.conClose();

            if (order.Rows.Count > 0) { 
            label3.Text = order.Rows[0][0].ToString();
            }
        }
        void searchTabel()
        {
            db con = new db();
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            DataTable products;
            con.MysqlQuery("select * from table_no");
            products = con.QueryEx();

            if (products.Rows.Count > 0)
            {
                Dictionary<string, string> comboSource = new Dictionary<string, string>();
                for (int i = 0; i < products.Rows.Count; i++)
                {
                    DataRow dr = products.Rows[i];
                    comboSource.Add(dr["id"].ToString(), dr["table_name"].ToString());
                }
                AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                comboBox1.DataSource = new BindingSource(comboSource, null);
                comboBox1.DisplayMember = "Value";
                comboBox1.ValueMember = "Key";
                comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            con.conClose();
            comboBox1.Text = "";
        }
        private void button2_Click(object sender, EventArgs e)
        {
            Form_display.cmd_editorder = true;
            this.Close();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SessionData.SetTabelDetails(comboBox1.Text);
            db con = new db();

            DataTable activeTables;
            con.MysqlQuery("SELECT COUNT(orders.id) AS activeOrders FROM `orders` WHERE orders.active =1 and orders.tabel = '" + comboBox1.Text + "' AND date(orders.created) = CURDATE()");
            activeTables = con.QueryEx();

            if (int.Parse(activeTables.Rows[0][0].ToString()) != 0)
            {
                //error message table not available
                string msg = "Table " + comboBox1.Text + " is not available. please close the order first! or select another table.";
                FormMessage frmmsg = new FormMessage(msg);
                frmmsg.ShowDialog();

            }
            else
            {
                string query = "";
                query = "UPDATE `orders` SET `tabel`= '" + SessionData.tabel + "' WHERE id = '" + _orderid + "'";
                con.MysqlQuery(query);
                con.NonQueryEx();
                
            }

            con.conClose();
            this.Close();


           
        }

        private void Form_ChangeGuestTable_Load(object sender, EventArgs e)
        {

        }
    }
}
