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
    public partial class Form_Productqty : Form
    {
        public Form_Productqty()
        {
            InitializeComponent();
        }
        private int qty;
        private void button12_Click(object sender, EventArgs e)
        {
            if (String.IsNullOrEmpty(textBox1.Text))
            {
                qty = 1;
            }
            else
            {
                qty = int.Parse(textBox1.Text);
            }
            Form_display.productqty = qty;
            this.Close();
        }

        void MyButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string s = button.Text;
            textBox1.Text += s;
            this.ActiveControl = textBox1;
        }

        private void Form_Productqty_Load(object sender, EventArgs e)
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

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (textBox1.Text != "" && int.Parse(textBox1.Text) > 0 && e.KeyCode == Keys.Enter)
            {
                Form_display.productqty = int.Parse(textBox1.Text);
                Form_display.addProduct = true;
                this.Close();

            }
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }




    }
}
