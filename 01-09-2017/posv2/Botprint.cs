using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Drawing.Printing;
using System.Drawing;
using System.Data;

namespace posv2
{
    public class Botprint
    {
        PrintDocument pdoc = null;
        int orderid, qty;
        DateTime TicketDate;
        String itemname, tabel;
        DataTable botorder;

        public Botprint()
        {

        }

        public Botprint(int id, DataTable order, string tbl)
        {
            botorder = order;
            orderid = id;

            if (tbl != "")
            {
                tabel = tbl;
            }
            else {
                tabel = "NOT DEFINED";
            }
            
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
                MessageBox.Show("BOT Printer is invalid.");
            }
        }





        void pdoc_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics graphics = e.Graphics;
            Font font = new Font("Courier New", 10);
            float fontHeight = font.GetHeight();
            int startX = 0;
            int startY = 0;
            int Offset = -5;
            //header start
            graphics.DrawString("BOT", new Font("Courier New", 18),
                                new SolidBrush(Color.Red),100, startY + Offset);

            Offset = Offset + 30;
            graphics.DrawString("Tabel No:" + tabel,
                     new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString("Order ID :" + orderid,
                     new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            graphics.DrawString("Steward :" + SessionData.stward,
                     new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 15;
            String underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            Offset = Offset + 10;
            graphics.DrawString("Product", new Font("Courier New", 11),
                     new SolidBrush(Color.Black), startX, startY + Offset);

            // Offset = Offset + 20;
            graphics.DrawString("Qty", new Font("Courier New", 11),
                     new SolidBrush(Color.Black), 200, startY + Offset);


            Offset = Offset + 10;
            String underLineend = "------------------------------------------";
            graphics.DrawString(underLineend, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            //header end
            for (int i = 0; i < botorder.Rows.Count; i++)
            {
                DataRow dr = botorder.Rows[i];
                //Item start
                Offset = Offset + 15;
                graphics.DrawString(dr["name"].ToString(), new Font("Courier New", 11),
                         new SolidBrush(Color.Black), startX, startY + Offset);
                Offset = Offset + 15;
                graphics.DrawString(dr["qty"].ToString(), new Font("Courier New", 11),
                         new SolidBrush(Color.Black),220, startY + Offset);
                //Item Ends


            }
            //footer start
            Offset = Offset + 10;
            underLine = "------------------------------------------";
            graphics.DrawString(underLine, new Font("Courier New", 10),
                     new SolidBrush(Color.Black), startX, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("Date : " + DateTime.Now, new Font("Courier New", 8),
                     new SolidBrush(Color.Black), startX, startY + Offset);

           // String DrawnBy = "";
           // graphics.DrawString("Thank You Come Again! ", new Font("Courier New", 8),
                   //  new SolidBrush(Color.Black), 30, startY + Offset);

            Offset = Offset + 20;
            graphics.DrawString("Powered by Mcreatives", new Font("Courier New", 8),
                     new SolidBrush(Color.Black), 50, startY + Offset);
            Offset = Offset + 10;
            graphics.DrawString("+94 117 - 208 375", new Font("Courier New", 8),
                     new SolidBrush(Color.Black), 60, startY + Offset);
            //footer ends
        }


    }
}
