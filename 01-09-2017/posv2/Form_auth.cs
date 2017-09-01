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
    public partial class Form_auth : Form
    {
        private db con;
        public Form_auth()
        {
            InitializeComponent();
        }

        private void btn_auth_done_Click(object sender, EventArgs e)
        {
            string password = textBox1.Text;
            string username = textBox2.Text;
            Form_display.managerPassword = checkAuthUser(username,password);
            Form_display.authproceed = true;
            this.Close();
        }

        private bool checkAuthUser(string user,string pass) {
            SessionData.SetMd5PasswordToConvert(textBox2.Text);
            string md5pass = SessionData.md5Password;
            DataTable order;
            con = new db();
            string query = "SELECT * FROM `users` WHERE users.password = '" + md5pass + "' AND users.user_type= 'A'";
            con.MysqlQuery(query);
            order = con.QueryEx();
            con.conClose();

            string currentCellValue = string.Empty;
            foreach (DataRow dr in order.Rows)
            {
                currentCellValue = dr["id"].ToString();
            }
            Form_display.authid = currentCellValue;
            if (order.Rows.Count > 0)
            {
                return true;
            }
            else {
                MessageBox.Show("Authentication Faild!");
                return false;
            }
        }


    }
}
