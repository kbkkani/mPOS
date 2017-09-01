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
    public partial class Form_change : Form
    {
        private double balnce;
        public Form_change(double bal)
        {
            InitializeComponent();
            balnce = bal;
        }

        private void Form_change_Load(object sender, EventArgs e)
        {
            lbl_change.Text = String.Format("{0:n}",balnce);
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btn_change_ok_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
