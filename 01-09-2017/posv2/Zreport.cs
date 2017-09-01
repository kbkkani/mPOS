using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;

namespace posv2
{
    public class Zreport
    {
        PrintDocument pdoc = null;
        int _guestcount;
        double _totalsale, _totalcardsale, _totalcashsale,_totalDiscount,_totalServiceCharge ;
        DataTable _cardwisesale, _totalshiftcardsale, _totalshiftcashsale, _voiditems, _categorysale;

        public double TotalSale
        {
            //set the TotalSale
            set { this._totalsale = value; }
            //get the TotalSale
            get { return this._totalsale; }
        }

        public double SetTotalCardSale
        {
            //set the totalcardsale
            set { this._totalcardsale = value; }
            //get the totalcardsale
            get { return this._totalcardsale; }
        }

        public double SetTotalCashSale
        {
            //set the totalcashsale
            set { this._totalcashsale = value; }
            //get the totalcashsale
            get { return this._totalcashsale; }
        }

        

        public DataTable CardWiseSale
        {
            //set the Card wise sale
            set { this._cardwisesale = value; }
            //get the Card wise sale
            get { return this._cardwisesale; }
        }


        //shift based
        public DataTable TalalCardSaleShift
        {
            //set the TalalCardSale
            set { this._totalshiftcardsale = value; }
            //get the TalalCardSale
            get { return this._totalshiftcardsale; }
        }

        //shift based
        public DataTable TalalCashSaleShift
        {
            //set the TalalCardSale
            set { this._totalshiftcashsale = value; }
            //get the TalalCardSale
            get { return this._totalshiftcashsale; }
        }

        public DataTable VoidItems
        {
            //set the voiditems
            set { this._voiditems = value; }
            //get the voiditems
            get { return this._voiditems; }
        }

        public DataTable CategorySale
        {
            //set the categorysale
            set { this._categorysale = value; }
            //get the categorysale
            get { return this._categorysale; }
        }


        public int GuestCount
        {
            //set the guestcount
            set { this._guestcount = value; }
            //get the guestcount
            get { return this._guestcount; }
        }

        public double Discount
        {
            //set the total discout
            set { this._totalDiscount = value; }
            //get the total discout
            get { return this._totalDiscount; }
        }

        public double ServiceCharge
        {
            //set the total service chage
            set { this._totalServiceCharge = value; }
            //get the total service chage
            get { return this._totalServiceCharge; }
        }


        public Zreport(int guestCount, double totalSale,double discount,double service, double totalcardsale, double totalcashsale, DataTable cardWiseSale, DataTable cardSaleByShift, DataTable cashSaleByShift, DataTable voidItems, DataTable categorySale)
        {
            _guestcount = guestCount;
            _totalsale = totalSale;
            _totalcardsale = totalcardsale;
            _totalcashsale = totalcashsale;
            _totalshiftcardsale = cardSaleByShift;
            _totalshiftcashsale = cashSaleByShift;
            _voiditems = voidItems;
            _cardwisesale = cardWiseSale;
            _categorysale = categorySale;
            _totalDiscount = discount;
            _totalServiceCharge = service;
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
            graphics.DrawString("Z-Report", new Font("Courier New", 12),
                                new SolidBrush(Color.Red), 70, startY + Offset);
            Offset = Offset + 20;
            //Report date
            graphics.DrawString("Report Date ",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            graphics.DrawString(DateTime.Now.ToString("yyyy-MM-dd"),
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), 150, startY + Offset);
            Offset = Offset + 15;


            //Till Open Time
            graphics.DrawString("Till Open Time ",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            graphics.DrawString(SessionData.tillOpenTime,
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), 150, startY + Offset);
            //Till Open Time
            Offset = Offset + 15;
            graphics.DrawString("Till Closed Time ",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            graphics.DrawString(DateTime.Now.ToString("HH:mm:ss"),
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), 150, startY + Offset);
            //Till closing balance
            Offset = Offset + 25;





            graphics.DrawString("Till Open Balance ",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            //Till close time
            Offset = Offset + 15;
            graphics.DrawString(String.Format("{0:n}", SessionData.openBalance),
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), 50, startY + Offset);
            //Till close time
            Offset = Offset + 15;



            graphics.DrawString("Till Closing Balance ",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            //Cashier
            Offset = Offset + 15;
            graphics.DrawString(String.Format("{0:n}", SessionData.openBalance + _totalsale),
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), 50, startY + Offset);
            //Cashier
            Offset = Offset + 25;





            graphics.DrawString("Cashier ",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            graphics.DrawString(SessionData.user,
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), 150, startY + Offset);


            String underLine = "-------------------------------";
            Offset = Offset + 15;
            graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            

            if (_cardwisesale.Rows.Count > 0) {

                //Title Sales Summery
                Offset = Offset + 15;
                graphics.DrawString("Sales Summary", new Font("Courier New", 12),
                                    new SolidBrush(Color.Red), 70, startY + Offset);
                Offset = Offset + 15;
                graphics.DrawString(underLine, new Font(customfont, 10),
                         new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 10;

                if (_cardwisesale.Rows != null)
                {

                    Offset = Offset + 5;
                    graphics.DrawString("Card Type",
                            new Font(customfont, 10),
                            new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString("Total",
                            new Font(customfont, 10),
                            new SolidBrush(Color.Black), 150, startY + Offset);
                    Offset = Offset + 15;

                    foreach (DataRow cardwisesalerow in _cardwisesale.Rows)
                    {
                        graphics.DrawString(cardwisesalerow["cardtype"].ToString(),
                                 new Font(customfont, 10),
                                 new SolidBrush(Color.Black), startX, startY + Offset);

                        graphics.DrawString(String.Format("{0:n}", cardwisesalerow["cardsale"]),
                                new Font(customfont, 10),
                                new SolidBrush(Color.Black), 150, startY + Offset);
                        Offset = Offset + 15;
                    }
                }
            }

           

       
            graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            //End cardwise sale

            

            //Total card sale

            if (_totalshiftcardsale.Rows.Count > 0) {
                Offset = Offset + 30;
                graphics.DrawString("Total Card Sale",
                         new Font(customfont, 10),
                         new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 15;
                //shifts
                if (_totalshiftcardsale != null)
                {
                    foreach (DataRow cardsaleRows in _totalshiftcardsale.Rows)
                    {
                        graphics.DrawString("Shift  " + cardsaleRows["shift_no"].ToString(),
                                 new Font(customfont, 10),
                                 new SolidBrush(Color.Black), 30, startY + Offset);

                        graphics.DrawString(String.Format("{0:n}", cardsaleRows["cardsale"]),
                                new Font(customfont, 10),
                                new SolidBrush(Color.Black), 150, startY + Offset);
                        Offset = Offset + 15;
                    }
                }
            }




            //Total cash sale

            if (_totalshiftcashsale.Rows.Count > 0) {
                Offset = Offset + 15;
                graphics.DrawString("Total Cash Sale",
                         new Font(customfont, 10),
                         new SolidBrush(Color.Black), startX, startY + Offset);


                Offset = Offset + 15;
                //shifts
                if (_totalshiftcashsale != null)
                {
                    foreach (DataRow cardsaleRows in _totalshiftcashsale.Rows)
                    {
                        graphics.DrawString("Shift  " + cardsaleRows["shift_no"].ToString(),
                                 new Font(customfont, 10),
                                 new SolidBrush(Color.Black), 30, startY + Offset);

                        graphics.DrawString(String.Format("{0:n}", cardsaleRows["cardsale"]),
                                new Font(customfont, 10),
                                new SolidBrush(Color.Black), 150, startY + Offset);
                        Offset = Offset + 15;
                    }
                }
            }

           



            

            //Title void sale

            if (_voiditems.Rows.Count > 0) {

                Offset = Offset + 15;
                graphics.DrawString(underLine, new Font(customfont, 10),
                         new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;
                graphics.DrawString("Void Sale", new Font("Courier New", 12),
                                    new SolidBrush(Color.Red), 70, startY + Offset);
                Offset = Offset + 25;
                graphics.DrawString(underLine, new Font(customfont, 10),
                         new SolidBrush(Color.Black), startX, startY + Offset);


                //Void Items
                if (_voiditems != null)
                {
                    Offset = Offset + 10;
                    graphics.DrawString("Item",
                            new Font(customfont, 10),
                            new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString("Qty",
                            new Font(customfont, 10),
                            new SolidBrush(Color.Black), 110, startY + Offset);


                    graphics.DrawString("Amount",
                           new Font(customfont, 10),
                           new SolidBrush(Color.Black), 170, startY + Offset);
                    Offset = Offset + 15;
                    foreach (DataRow voidItemsRows in _voiditems.Rows)
                    {
                        Offset = Offset + 15;
                        graphics.DrawString(voidItemsRows["name"].ToString(),
                                 new Font(customfont, 10),
                                 new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;
                        graphics.DrawString(voidItemsRows["product_id"].ToString(),
                                 new Font(customfont, 10),
                                 new SolidBrush(Color.Black), startX, startY + Offset);
                        graphics.DrawString(voidItemsRows["qty"].ToString(),
                                new Font(customfont, 10),
                                new SolidBrush(Color.Black), 110, startY + Offset);

                        graphics.DrawString(String.Format("{0:n}", voidItemsRows["subtotal"]),
                                new Font(customfont, 10),
                                new SolidBrush(Color.Black), 170, startY + Offset);

                    }
                }
            }

            
           
            //------------------------------------------------------------------------

            //Title Department Sale

            if (_categorysale.Rows.Count > 0) {

                Offset = Offset + 15;
                //underline
                graphics.DrawString(underLine, new Font(customfont, 10),
                         new SolidBrush(Color.Black), startX, startY + Offset);

                Offset = Offset + 15;
                graphics.DrawString("Department Sale", new Font("Courier New", 12),
                                    new SolidBrush(Color.Red), 50, startY + Offset);
                Offset = Offset + 25;
                graphics.DrawString(underLine, new Font(customfont, 10),
                         new SolidBrush(Color.Black), startX, startY + Offset);


                //Category wise sale
                if (_categorysale != null)
                {
                    Offset = Offset + 10;
                    graphics.DrawString("Category",
                            new Font(customfont, 10),
                            new SolidBrush(Color.Black), startX, startY + Offset);

                    graphics.DrawString("Sale",
                            new Font(customfont, 10),
                            new SolidBrush(Color.Black), 110, startY + Offset);


                    graphics.DrawString("Item Count",
                           new Font(customfont, 10),
                           new SolidBrush(Color.Black), 170, startY + Offset);
                    Offset = Offset + 15;
                    foreach (DataRow catRows in _categorysale.Rows)
                    {
                        Offset = Offset + 15;
                        graphics.DrawString(catRows["name"].ToString(),
                                 new Font(customfont, 10),
                                 new SolidBrush(Color.Black), startX, startY + Offset);
                        Offset = Offset + 15;
                        graphics.DrawString(String.Format("{0:n}", catRows["sale"]),
                                new Font(customfont, 10),
                                new SolidBrush(Color.Black), 110, startY + Offset);
                        graphics.DrawString(catRows["itemcount"].ToString(),
                                new Font(customfont, 10),
                                new SolidBrush(Color.Black), 220, startY + Offset);

                    }
                }
            }

            
            Offset = Offset + 15;
            //underline
            graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            //Total Sale
            Offset = Offset + 15;
            graphics.DrawString("Total Sale",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            graphics.DrawString(String.Format("{0:n}", _totalsale),
                     new Font(customfont, 12, FontStyle.Bold),
                     new SolidBrush(Color.Black), 150, startY + Offset);
            Offset = Offset + 15;
            //underline
            graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 2;
            graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);



            //Total discount
            Offset = Offset + 15;
            graphics.DrawString("Total Discount",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString(String.Format("{0:n}",_totalDiscount),
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), 40, startY + Offset);

            //Total ServiceCharge
            Offset = Offset + 15;
            graphics.DrawString("Total Service Charge",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString(String.Format("{0:n}", _totalServiceCharge),
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), 40, startY + Offset);



            Offset = Offset + 15;
            graphics.DrawString("Total Guest Count",
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            graphics.DrawString(_guestcount.ToString(),
                     new Font(customfont, 10),
                     new SolidBrush(Color.Black), 220, startY + Offset);

            Offset = Offset + 15;
            //underline
            graphics.DrawString(underLine, new Font(customfont, 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);




            Offset = Offset + 10;
            graphics.DrawString("Powered by mPOS", new Font(customfont, 8),
                     new SolidBrush(Color.Black), 50, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("+94 117 - 208 375", new Font(customfont, 8),
                     new SolidBrush(Color.Black), 55, startY + Offset);
            Offset = Offset + 10;
        }


    }
}
