using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;

namespace posv2
{
    public class VoidOrder
    {
        PrintDocument pdoc = null;
        double _totalsale, _totalcardsale, _totalcashsale;
        DataTable _voidOrder;

        public double TotalSale
        {
            //set the TotalSale
            set { this._totalsale = value; }
            //get the TotalSale
            get { return this._totalsale; }
        }


        public DataTable SetVoidOrder
        {
            //set the guestcount
            set { this._voidOrder = value; }
            //get the guestcount
            get { return this._voidOrder; }
        }


        public VoidOrder(double totalSale,DataTable voidorder)
        {
           
            _totalsale = totalSale;
            _voidOrder = voidorder;

        }

        public VoidOrder(DataTable voidorder)
        {

            _voidOrder = voidorder;

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
            Image photo = Image.FromFile(@"c:/xampp/htdocs/mpos/images/mpos.png");
            //print logo if neccessary
            //graphics.DrawImage(photo, 10, -60);


            Offset = Offset + 5;
            graphics.DrawString("Heritage Cafe & Bistro", new Font("Courier New", 14),
                                new SolidBrush(Color.Red), 0, startY + Offset);
            Offset = Offset + 20;
            graphics.DrawString("Void Bill", new Font("Courier New", 12),
                                new SolidBrush(Color.Red), 70, startY + Offset);
            Offset = Offset + 20;
            //Report date
            graphics.DrawString("Bill Date ",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd"),
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), 150, startY + Offset);
            Offset = Offset + 15;



            graphics.DrawString("Cashier ",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            graphics.DrawString(SessionData.user,
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), 150, startY + Offset);


            String underLine = "----------------------------";
            Offset = Offset + 15;
            graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            //Title Sales Summery
            Offset = Offset + 15;
            graphics.DrawString("Void Order", new Font("Courier New", 12),
                                new SolidBrush(Color.Red), 70, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 10;
            if (_voidOrder.Rows != null)
            {

                Offset = Offset + 5;
                graphics.DrawString("Item Name",
                        new Font(customfont, 10),
                        new SolidBrush(Color.Black), startX, startY + Offset);

                graphics.DrawString("Qty",
                        new Font(customfont, 10),
                        new SolidBrush(Color.Black), 120, startY + Offset);
                graphics.DrawString("Sub Total",
                        new Font(customfont, 10),
                        new SolidBrush(Color.Black), 170, startY + Offset);


                Offset = Offset + 25;
                double total = 0;
                foreach (DataRow voidsitems in _voidOrder.Rows)
                {

                    graphics.DrawString(voidsitems["itemname"].ToString(),
                             new Font(customfont, 11),
                             new SolidBrush(Color.Black), startX, startY + Offset);
                    Offset = Offset + 15;
                    graphics.DrawString(voidsitems["product_id"].ToString(),
                             new Font(customfont, 10),
                             new SolidBrush(Color.Black), startX, startY + Offset);
                    graphics.DrawString(voidsitems["qty"].ToString(),
                            new Font(customfont, 11),
                            new SolidBrush(Color.Black), 120, startY + Offset);

                    graphics.DrawString(String.Format("{0:n}", voidsitems["subtotal"]),
                            new Font(customfont, 11),
                            new SolidBrush(Color.Black), 170, startY + Offset);

                    total = double.Parse(voidsitems["total"].ToString());
                    Offset = Offset + 15;
                }
                Offset = Offset + 25;
                graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;
                graphics.DrawString("Total",
                        new Font(customfont, 12),
                        new SolidBrush(Color.Black), startX, startY + Offset);


                graphics.DrawString(String.Format("{0:n}", total),
                        new Font(customfont, 12),
                        new SolidBrush(Color.Black), 170, startY + Offset);





                Offset = Offset + 55;
                graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;
                graphics.DrawString("Authorized Signature",
                           new Font(customfont, 10),
                           new SolidBrush(Color.Black), 50, startY + Offset);

            }   
        }
    }
}
