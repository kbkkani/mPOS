using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace posv2
{
    public static class SessionData
    {

        static SessionData()
        {
            string host,_defaultPrinter;
            string dbuser;
            string dbpassword;
            string db;
            string comport;
            int loginMode = 1;//1 for cashier 2 for kot and 3 for admin
            bool userAuth = false;
            string authType = "U";
            string user;
            string userid;
            double cartTotal = 0;
            int itemCount = 0;
            double serviceCharge = 10;
            double discount = 0;
            double change;
            double payamount;
            double billamount;
            int guest, shiftno;
            string tabel = "";
            long newOrderId;
            int orderType = 0;
            bool cashdisplayon = true;
            string cardname;
            string cardtype;
            int cardlastdigits, shiftId;
            int paymentType,newordercount;
            double lastbillamout;
            double discountedPrice;
            double servicePrice;
            string stward;
            string md5Password;
            string tillOpenTime;
            double openBalance;


            //Receipt = 1, Kot=2, Bot = 3
            int receiptType;

        }
        //SET STWARD
        public static string stward { get; private set; }
        public static void SetStward(string name)
        {
            stward = name;
        }

        //SET USER ID
        public static string userid { get; private set; }
        public static void SetUserId(string id)
        {
            userid = id;
        }

        //SET HOST
        public static string host { get; private set; }
        public static void SetHost(string hostname)
        {
            host = hostname;
        }

        //SET DB USER
        public static string dbuser { get; private set; }
        public static void SetDbUser(string user)
        {
            dbuser = user;
        }

        //SET DB PASSWORD
        public static string dbpassword { get; private set; }
        public static void SetDbUserPassword(string dbpass)
        {
            dbpassword = dbpass;
        }

        //SET DB 
        public static string db { get; private set; }
        public static void SetDb(string database)
        {
            db = database;
        }

        //SET COMPORT
        public static string comport { get; private set; }
        public static void SetComPort(string port)
        {
            comport = port;
        }

        //set login mode
        public static int loginMode { get; private set; }
        public static void SetLoginMode(int mode)
        {
            loginMode = mode;
        }

        //set user authentication
        public static bool userAuth { get; private set; }
        public static void SetUserAuth(bool auth)
        {
            userAuth = auth;
        }

        //Uer Type
        public static string authType { get; private set; }
        public static void setauthType(string auth_type)
        {
            authType = auth_type;
        }

        //set user
        public static string user { get; private set; }
        public static void setUser(string u)
        {
            user = u;
        }



        //cal cart total
        public static double cartTotal { get; private set; }
        public static void SetCartTotal(double subtotal)
        {
            cartTotal += subtotal;
        }

        public static void ResetCartTotal()
        {
            cartTotal = cartTotal - cartTotal;
        }

        public static void UpdateCartTotal(double deleteprice)
        {
            cartTotal = cartTotal - deleteprice;
        }

        //cat count Items
        public static int itemCount { get; private set; }
        public static void SetCartItemCount(int qty)
        {
            itemCount += qty;
        }

        public static void DeleteItem()
        {
            itemCount = itemCount - 1;
        }

        //set service charge
        public static double serviceCharge { get; private set; }
        public static void SetSeviceCharge(double charge)
        {
            serviceCharge = charge;
        }

        //set discount
        public static double discount { get; private set; }
        public static void SetDiscount(double dis)
        {
            discount = dis;
        }
        //Set change money
        public static double change { get; private set; }
        public static void SetChageMoney()
        {
            change = lastbillamout - payamount;
        }



        //set payamout and set billamout you can get both change and lastbillamount
        public static double payamount { get; private set; }
        public static void SetPayamount(double paid)
        {
            payamount += paid;
        }

        public static double billamount { get; private set; }
        public static void setBillamount()
        {
            billamount = (cartTotal - (cartTotal * discount / 100) + (cartTotal * serviceCharge / 100)) - payamount;
            SetLastbillamount();
            SetChageMoney();
        }

        public static double lastbillamout { get; private set; }
        public static double discountedPrice { get; private set; }
        public static double servicePrice { get; private set; }
        public static void SetLastbillamount()
        {
            lastbillamout = (cartTotal - (cartTotal * discount / 100) + (cartTotal * serviceCharge / 100));
            discountedPrice = cartTotal * discount / 100;
            servicePrice = cartTotal * serviceCharge / 100;

        }





        //gests details
        public static int guest { get; private set; }
        public static void SetGuest(int count)
        {
            guest = count;
        }

        //tabel details
        public static string tabel { get; private set; }
        public static void SetTabelDetails(string tbl)
        {
            tabel = tbl;

            //if (String.IsNullOrEmpty(tabel))
            //{
            //   tabel += tbl;
            //}
            // else {
            //   tabel += "-" + tbl;
            //  } 
        }

        //Set New order id
        public static long newOrderId { get; private set; }
        public static void SetNewOrderId(long orderid)
        {
            newOrderId = orderid;
        }


        //set order type
        public static int orderType { get; private set; }
        public static void SetOrderType(int ortype)
        {
            orderType = ortype;
        }

        //cash display on off

        public static bool cashdisplayon { get; private set; }
        public static void SetCashdisplayStatus(bool cashdisplaystatus)
        {
            cashdisplayon = cashdisplaystatus;
        }

        public static int receiptType { get; private set; }
        public static void SetReceiptType(int rtype)
        {
            receiptType = rtype;
        }

        public static string cardtype { get; private set; }
        public static void SetCardtype(string ctype) {
            cardtype = ctype;
        }
        public static string cardname { get; private set; }
        public static void SetCardName(string cname) {
            cardname = cname;
        }
        public static int cardlastdigits { get; private set; }
        public static void SetCardNumber(int cnumber) {
            cardlastdigits = cnumber;
        }

        public static int paymentType { get; private set; }
        public static void SetpaymentType(int ptype)
        {
            paymentType = ptype;
        }

        public static string md5Password { get; private set; }
        public static void SetMd5PasswordToConvert(string source)
        {
            StringBuilder sb = new StringBuilder();
            using (MD5 md5 = MD5.Create())
            {
                byte[] md5HashBytes = md5.ComputeHash(Encoding.UTF8.GetBytes(source));
                foreach (byte b in md5HashBytes)
                {
                    sb.Append(b.ToString("X2"));
                }
            }
            md5Password = sb.ToString();
        }

        public static string tillOpenTime { get; private set; }
        public static void SetTillOpenTime(string opentime)
        {
            tillOpenTime = opentime;
        }

        public static double openBalance { get; private set; }
        public static void SetTillOpenBalance(double opbalence)
        {
            openBalance = opbalence;
        }

        public static int shiftId { get; private set; }
        public static void SetUserShiftId(int shifti)
        {
            shiftId = shifti;
        }

        public static int shiftno { get; private set; }
        public static void SetUserShiftNo(int shiftNumber)
        {
            shiftno = shiftNumber;
        }

        public static string _defaultPrinter { get; private set; }
        public static void SetDefaultPrinter(string defaultPrinter)
        {
            _defaultPrinter = defaultPrinter;
        }


        public static int newordercount { get; private set; }
        public static void SetNewOrderCount(int neworders)
        {
            newordercount = neworders;
        }


        
        
    }
}
