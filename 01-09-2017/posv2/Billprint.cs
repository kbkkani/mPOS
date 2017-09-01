using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;

namespace posv2
{
    public class Billprint
    {
        PrintDocument pdoc = null;
        int orderid, qty, user, preprintbill;
        DateTime TicketDate;
        String itemname, tabel, created;
        List<Receipt> _list;
        double discount, servc, cash,balance, billamount;



        public double Billamount
        {
            //set the bill amount
            set { this.billamount = value; }
            //get the bill amount
            get { return this.billamount; }
        }

        public string Tabel
        {
            //set the Tabel
            set { this.tabel = value; }
            //get the Tabel
            get { return this.tabel; }
        }

        public double Cash
        {
            //set the cash
            set { this.cash = value; }
            //get the cash
            get { return this.cash; }
        }

        public double Balance
        {
            //set the Tabel
            set { this.balance = value; }
            //get the Tabel
            get { return this.balance;}
        }

        public int PrePrint
        {
            //set the Tabel
            set { this.preprintbill = value; }
            //get the Tabel
            get { return this.preprintbill; }
        }



        public Billprint()
        {

        }

        string receiptTitle = "";
        public Billprint(List<Receipt> datasource,double billa, string guesttabel,int preprint)
        {
            receiptTitle = "Payment Receipt";
            _list = datasource;
            this.tabel = guesttabel;
            this.billamount = billa;
            this.preprintbill = preprint;
        }
        public Billprint(List<Receipt> datasource, double ccash, double billa, string guesttabel,double customerbalance)
        {
            receiptTitle = "Customer Bill";
            _list = datasource;
            this.billamount = billa;
            this.tabel = guesttabel;
            this.cash = ccash;
            this.balance = customerbalance;
        }


        public void print(string printer)
        {
            PrintDocument pdoc = new PrintDocument();
            pdoc.PrinterSettings.PrinterName = printer;

            if (pdoc.PrinterSettings.IsValid)
            {
                pdoc.PrintPage += new PrintPageEventHandler(pdoc_PrintPage);
                pdoc.Print();
            }
            else
            {
                MessageBox.Show("Printer is invalid.");
            }
        }





        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            // MessageBox.Show(orderid.ToString());


            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            string customfont = "Courier New";
            float fontHeight = font.GetHeight();
            int startX = 0;
            int startY = 0;
            int Offset = -5;
            //heder start

            //logo
            Image photo = Image.FromFile(@"c:/xampp/htdocs/mpos/images/logo.jpg");
            //print logo if neccessary
            graphics.DrawImage(photo,100,0);


            Offset = Offset + 65;
            graphics.DrawString("The Heritage Cafe & Bistro", new Font("Courier New", 12),
                                new SolidBrush(Color.Red), 0, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("61, Pedlar Street, Galle", new Font("Courier New", 9),
                                new SolidBrush(Color.Red), 50, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString("80000, Sri Lanka", new Font("Courier New", 9),
                                new SolidBrush(Color.Red), 70, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString("+94 77 142 1157", new Font("Courier New", 8),
                                new SolidBrush(Color.Red), 80, startY + Offset);
            Offset = Offset + 25;

            if (this.tabel == "TAKEAWAY")
            {
                graphics.DrawString("Order Type:" + this.tabel,
                              new Font(customfont, 10, FontStyle.Bold),
                              new SolidBrush(Color.Black), startX, startY + Offset);
            }
            else {
                graphics.DrawString("Tabel No:" + this.tabel,
                             new Font(customfont, 10, FontStyle.Bold),
                             new SolidBrush(Color.Black), startX, startY + Offset);
            }
            


            Offset = Offset + 15;
            graphics.DrawString("Order ID :" + SessionData.newOrderId,
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString("Cashier :" + SessionData.user,
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString("Date :" + DateTime.Now,
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            //header end



            Offset = Offset + 15;
            String underLine = "-----------------------------";
            graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);




            Offset = Offset + 10;
            graphics.DrawString("Product", new Font(customfont, 11),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            // Offset = Offset + 20;
            graphics.DrawString("Price", new Font(customfont, 11),
                     new SolidBrush(Color.Black), 80, startY + Offset);


            graphics.DrawString("Qty", new Font(customfont, 11),
                     new SolidBrush(Color.Black), 150, startY + Offset);



            graphics.DrawString("Total", new Font(customfont, 11),
                     new SolidBrush(Color.Black), 200, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);


            foreach (var item in _list)
            {
                //Item start
                Offset = Offset + 15;
                graphics.DrawString(item.product, new Font(customfont, 9),
                         new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;

                //item code
                graphics.DrawString(String.Format("{0:n}", item.id), new Font(customfont, 9),
                        new SolidBrush(Color.Black), startX, startY + Offset);

                //item price
                graphics.DrawString("(" + String.Format("{0:n}", item.unit_price), new Font(customfont, 9),
                        new SolidBrush(Color.Black), 55, startY + Offset);


                graphics.DrawString("X", new Font(customfont, 9),
                        new SolidBrush(Color.Black), 140, startY + Offset);

               //item qty
                graphics.DrawString(item.qty.ToString() + ")", new Font(customfont, 9),
                         new SolidBrush(Color.Black), 160, startY + Offset);

                graphics.DrawString(String.Format("{0:n}", item.total), new Font(customfont, 9),
                         new SolidBrush(Color.Black), 190, startY + Offset);
                //Item Ends
            }
            Offset = Offset + 20;

            graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;

            graphics.DrawString("Total :", new Font(customfont, 18),
                      new SolidBrush(Color.Black), startX, startY + Offset);
            graphics.DrawString(String.Format("{0:n}", this.billamount), new Font(customfont, 18, FontStyle.Bold),
                     new SolidBrush(Color.Black), 120, startY + Offset);

            Offset = Offset + 20;

            graphics.DrawString(underLine, new Font(customfont, 10),
                    new SolidBrush(Color.Black), startX, startY + Offset);


            //discount
            Offset = Offset + 15;
            graphics.DrawString("Discount:" + SessionData.discount + "%",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            if (preprintbill != 1) {
                //cash
                graphics.DrawString("Cash:" + String.Format("{0:n}", this.cash),
                         new Font(customfont, 10),
                         new SolidBrush(Color.Black), 120, startY + Offset);
            }
           



            //service charg
            Offset = Offset + 15;
            graphics.DrawString("Service:" + SessionData.serviceCharge + "%",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            if (preprintbill != 1)
            {
                //balance
                graphics.DrawString("Balance:" + String.Format("{0:n}", this.balance),
                        new Font(customfont, 10),
                        new SolidBrush(Color.Black), 120, startY + Offset);

            }




            Offset = Offset + 20;

            String DrawnBy = "";
            graphics.DrawString("Thank You & Come Again! ", new Font(customfont, 10),
                     new SolidBrush(Color.Black),20, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString("Keep in Touch with Us ", new Font(customfont,10),
                     new SolidBrush(Color.Black), 29, startY + Offset);

            Offset = Offset + 15;
            graphics.DrawString("******************", new Font(customfont, 10),
                     new SolidBrush(Color.Black), 42, startY + Offset);

            Offset = Offset + 15;
            graphics.DrawString(underLine, new Font(customfont, 10),
                    new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 25;
            graphics.DrawString("Facebook : The-Heritage-Cafe-Bistro", new Font(customfont, 8),
                     new SolidBrush(Color.Black), 0, startY + Offset);

            Offset = Offset + 15;
            graphics.DrawString("Web : http://heritagecafeandbistro.com", new Font(customfont, 8),
                     new SolidBrush(Color.Black), 0, startY + Offset);

            Offset = Offset + 15;
            graphics.DrawString("Mail : info@heritagecafe.com", new Font(customfont, 8),
                     new SolidBrush(Color.Black), 0, startY + Offset);





            Offset = Offset + 20;
            graphics.DrawString("© mPOS |  +94 117 - 208 375", new Font(customfont, 8),
                     new SolidBrush(Color.Black), 0, startY + Offset);
            //Offset = Offset + 10;
            //graphics.DrawString("+94 117 - 208 375", new Font(customfont, 8),
            //         new SolidBrush(Color.Black),55, startY + Offset);
            Offset = Offset + 10;
            
            
        }


    }
}
