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
    public partial class Discount : Form
    {
        public Discount()
        {
            InitializeComponent();
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }

        void MyButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string s = button.Text;
            textBox1.Text += s;
            this.ActiveControl = textBox1;
        }

        private void Discount_Load(object sender, EventArgs e)
        {
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

        private void btn_discountDone_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                SessionData.SetDiscount(double.Parse(textBox1.Text));
                this.Close();
            }
            else {
                MessageBox.Show("Invalid Input!");
            }
            
        }
    }
}
