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
    public partial class Form_continue_order : Form
    {
        public Form_continue_order()
        {
            InitializeComponent();
        }

        private void btn_continue_order_Click(object sender, EventArgs e)
        {
            if (txt_continue_order.Text != "")
            {
                long orderid = long.Parse(txt_continue_order.Text);

                Form_display.continueOrderId = orderid;
                this.Close();
            }
            else {
                MessageBox.Show("Invalid Order ID");
            }



        }
    }
}
