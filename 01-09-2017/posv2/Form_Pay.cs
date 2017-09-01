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
    public partial class Form_Pay : Form
    {
        private int paymentType = 1;
        public Form_Pay()
        {
            InitializeComponent();
            
        }

        void MyButtonClick(object sender, EventArgs e)
        {
            Button button = sender as Button;
            string s = button.Text;
            textBox1.Text += s;
            this.ActiveControl = textBox1;
        }

        private void Form_Pay_Load(object sender, EventArgs e)
        {
            SessionData.setBillamount();
            label1.Text = String.Format("{0:n}",SessionData.billamount);
            textBox7.Text = String.Format("{0:n}", SessionData.billamount);
            button13.Visible = false;

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

        //done 
        private void button12_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "") {
                SessionData.SetpaymentType(2);
                double paid = double.Parse(textBox1.Text);

                if (paid >= SessionData.billamount)
                {

                    SessionData.SetPayamount(paid);
                    SessionData.setBillamount();
                    Form_display.cash = SessionData.payamount;
                    Form_display.balance = SessionData.change;
                    double bal = SessionData.billamount;

                    if (bal <= 0)
                    {
                        Form_display.paymentDone = true;
                        Form_change frmchange = new Form_change(bal);
                        frmchange.ShowDialog();
                        this.Close();
                    }
                    else
                    {
                        label1.Text = String.Format("{0:n}", bal);
                    }
                }
                else {
                    MessageBox.Show("Invalid Payment!");
                }


            }
            
        }

        private void btn_cardPay_Click(object sender, EventArgs e)
        {
            SessionData.SetpaymentType(1);
            panel1.Show();
        }

        private void textBox6_KeyUp(object sender, KeyEventArgs e)
        {
            if (textBox6.Text.Length.Equals(4) && textBox2.Text != "" && comboBox1.Text != "" && textBox7.Text != "")
            {
                btn_skiptocash.Visible = false;
                button13.Visible = true;
            }
        }

        private void textBox6_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && (e.KeyChar != '.'))
            {
                e.Handled = true;
            }
        }


        //card payment
        private void button13_Click(object sender, EventArgs e)
        {
            if (textBox7.Text != "" && textBox2.Text != "" && textBox6.Text != "" && comboBox1.Text != "") {
                SessionData.SetpaymentType(1);
                SessionData.SetCardName(textBox2.Text);
                SessionData.SetCardNumber(int.Parse(textBox6.Text));
                SessionData.SetCardtype(comboBox1.SelectedItem.ToString());
                double paid = double.Parse(textBox7.Text);
                SessionData.SetPayamount(paid);
                SessionData.setBillamount();
                Form_display.cash = SessionData.payamount;
                Form_display.balance = SessionData.change;
                double bal = SessionData.billamount;

                if (bal <= 0)
                {
                    Form_display.paymentDone = true;
                    this.Close();
                }
            }

        }

        private void btn_skiptocash_Click(object sender, EventArgs e)
        {
            SessionData.SetpaymentType(2);
            panel1.Hide();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            if (!textBox1.Text.Equals(""))
            {
                textBox1.Text = textBox1.Text.Remove(textBox1.Text.Length - 1);
            }
        }
    }
}
