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
    public partial class FormMessage : Form
    {

        private string _msg;
        public FormMessage(string msg)
        {
            InitializeComponent();
            _msg = msg;
        }

        private void FormMessage_Load(object sender, EventArgs e)
        {
            lbl_msg.Text = _msg;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
