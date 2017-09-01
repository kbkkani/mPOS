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
    public partial class Form_dineIn : Form
    {

        private TextBox lastFocused;
        public Form_dineIn()
        {
            InitializeComponent();
            searchTabel();
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



        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "" && comboBox1.Text != "")
            {
                //check tabel is open or closed
                //SELECT COUNT(orders.id) AS activeOrders FROM `orders` WHERE orders.active =1 and orders.tabel = 'G1' AND date(orders.created) = '2017-08-23' 
                db con = new db();
                DataTable activeTables;
                con.MysqlQuery("SELECT COUNT(orders.id) AS activeOrders FROM `orders` WHERE orders.active =1 and orders.tabel = '" + comboBox1.Text + "' AND date(orders.created) = CURDATE()");
                activeTables = con.QueryEx();
                con.conClose();

                if (int.Parse(activeTables.Rows[0][0].ToString())!=0)
                {
                    //error message table not available
                    string msg = "Table " + comboBox1.Text+" is not available. please close the order first! or select another table.";
                    FormMessage frmmsg = new FormMessage(msg);
                    frmmsg.ShowDialog();
                    
                }
                else {
                    Form_display.tabelStatus = true;
                    SessionData.SetTabelDetails(comboBox1.Text);
                    SessionData.SetGuest(int.Parse(textBox1.Text));
                    this.Close();
                }


                
            }
            
        }


        void MyButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string s = button.Text;
            lastFocused.Text += s;
            this.ActiveControl = lastFocused;
        }

        private void textBoxFocusLost(object sender, EventArgs e)
        {
            lastFocused = (TextBox)sender;
        }

        private void Form_dineIn_Load(object sender, EventArgs e)
        {
            foreach (TextBox box in new TextBox[] { textBox1 })
            {
                box.LostFocus += textBoxFocusLost;
            }
            this.ActiveControl = textBox1;
            button1.Click += MyButtonClick;
            button2.Click += MyButtonClick;
            button3.Click += MyButtonClick;
            button4.Click += MyButtonClick;
            button5.Click += MyButtonClick;
            button6.Click += MyButtonClick;
            button7.Click += MyButtonClick;
            button8.Click += MyButtonClick;
            button9.Click += MyButtonClick;
            button10.Click += MyButtonClick;
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!lastFocused.Text.Equals(""))
            {
                lastFocused.Text = lastFocused.Text.Remove(lastFocused.Text.Length - 1);
            }
        }

        private void comboBox1_KeyUp(object sender, KeyEventArgs e)
        {
           // searchTabel();
        }







    }
}
