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
    public partial class Form_SearchProduct : Form
    {
        public Form_SearchProduct()
        {
            InitializeComponent();
        }

        private void Form_SearchProduct_Load(object sender, EventArgs e)
        {
            searchbycode();
            searchbyitemname();
        }

        void searchbycode() {
            db con = new db();
            comboBox2.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox2.AutoCompleteSource = AutoCompleteSource.CustomSource;
            DataTable products;
            con.MysqlQuery("select * from products");
            products = con.QueryEx();

            if (products.Rows.Count > 0)
            {
                Dictionary<string, string> comboSource = new Dictionary<string, string>();
                for (int i = 0; i < products.Rows.Count; i++)
                {
                    DataRow dr = products.Rows[i];
                    comboSource.Add(dr["id"].ToString(), dr["id"].ToString());
                }
                AutoCompleteStringCollection DataCollection = new AutoCompleteStringCollection();
                comboBox2.DataSource = new BindingSource(comboSource, null);
                comboBox2.DisplayMember = "Value";
                comboBox2.ValueMember = "Key";
                comboBox2.AutoCompleteSource = AutoCompleteSource.ListItems;
            }
            con.conClose();
            comboBox2.Text = "";
        }


        void searchbyitemname()
        {

            db con = new db();
            comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            comboBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;
            DataTable products;
            con.MysqlQuery("select * from products");
            products = con.QueryEx();

            if (products.Rows.Count > 0)
            {
                Dictionary<string, string> comboSource = new Dictionary<string, string>();
                for (int i = 0; i < products.Rows.Count; i++)
                {
                    DataRow dr = products.Rows[i];
                    comboSource.Add(dr["id"].ToString(), dr["name"].ToString());
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

        private void btn_search_itemname_Click(object sender, EventArgs e)
        {
            string key = ((KeyValuePair<string, string>)comboBox1.SelectedItem).Key;
            Form_display.productId = key;
            Form_display.proceedSearchProduct = true;
            this.Close();
        }

        private void btn_search_item_code_Click(object sender, EventArgs e)
        {
            string key = ((KeyValuePair<string, string>)comboBox2.SelectedItem).Key;
            Form_display.productId = key;
            Form_display.proceedSearchProduct = true;
            this.Close();
        }

    


    }
}
