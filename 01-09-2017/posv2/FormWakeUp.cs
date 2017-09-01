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
    public partial class FormWakeUp : Form
    {

        private db con;
        public FormWakeUp()
        {
            InitializeComponent();
            panel1.Visible = false;
        }
        int tries = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            
            if (textBox1.Text != "") {
                SessionData.SetMd5PasswordToConvert(textBox1.Text);
                string pass = SessionData.md5Password;
                DataTable order;
                con = new db();
                string query = "SELECT * FROM `users` WHERE users.username='" + SessionData.user + "' AND users.password = '" + pass + "' AND users.user_type= 'A'";
                con.MysqlQuery(query);
                order = con.QueryEx();
                con.conClose();

                if (order.Rows.Count > 0)
                {
                    tries = 0;
                    this.Close();
                }
                else {
                    panel1.Visible = true;
                    tries ++;
                    label1.Text = tries.ToString();
                
                }
            
            }

        }
    }
}
