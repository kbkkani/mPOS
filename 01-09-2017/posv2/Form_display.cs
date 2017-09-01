using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Threading;

namespace posv2
{

    public partial class Form_display : Form
    {
        private List<Receipt> orderDetails = new List<Receipt>();
        private db con;
        private string populateFolder = "categories";
        private int category_id;
        private int menu = 1;//FOR CATEGORIES  2 FOR PRODUCTS
        private bool cashdisplay = SessionData.cashdisplayon;
        private string bilprinter = SessionData._defaultPrinter;
        //private string kotprinter = "kot";
        //private string botprinter = "POS-80-Series";

       private string kotprinter = "POS-80Series";
       private string botprinter = "POS-80Series";


        //CART
        public static string productId;
        public static int productqty;
        public static string productSize;


        //commands
        public static bool addProduct = false;

        //Auth
        public static bool authproceed = false;
        public static bool managerPassword;
        public static string authid;
        public static bool updatemode = false;
       
        public Form_display()
        {
            InitializeComponent();
            btn_save.Visible = false;
            panel_menue.Visible = false;
            loadOrderDetails();
            label29.Text = SessionData.user;
            lbl_user.Text = SessionData.user;
            lbl_user_id.Text = SessionData.userid.ToString();
            userLog(SessionData.userid ,"login");
            btn_logout.Enabled = false;
         //   dataGridView_cart.ScrollBars = ScrollBars.Horizontal;

            //MessageBox.Show(DateTime.Now.ToString("d-M-yyyy"));
           // MessageBox.Show(DateTime.Now.ToString("t"));
            //MessageBox.Show(DateTime.Now.ToString("yyyyMMddHHmmss"));
           
            //reopen order details
            label1.Visible = false;
            label3.Visible = false;
            lbl_reopen_orderid.Visible = false;
            lbl_reopen_stwerd.Visible = false;
            lbl_table.Visible = false;
            lbl_tableno.Visible = false;
            btn_void_from_allorders.Visible = false;

            createShift();
        }

        void createShift() {
            con = new db();
            string shiftend="";
            DataTable result;
            int shifNo = 0;

            //find shif end or not
            string queryForCheckShift = "select id,shift_end,shift_start from shift where shift_date = '" + DateTime.Now.ToString("yyyy-M-d") + "'";
            con.MysqlQuery(queryForCheckShift);
            result = con.QueryEx();

            if (result.Rows.Count > 0) {
                
                for (int i = 0; i < result.Rows.Count; i++)
                {
                    DataRow dr = result.Rows[i];
                    shiftend = dr["shift_end"].ToString();
                }
            }

            

            if (result.Rows.Count == 0 || shiftend != "")
            {
                //set shift No
                if (shiftend != "")
                {
                    shifNo = result.Rows.Count + 1;
                }
                else {
                    shifNo = 1;
                }

                //create a new shift
                string q = "insert into shift (users_id,shift_date,shift_start,shift_no) values('" + SessionData.userid + "','" + DateTime.Now.ToString("yyyy-M-d") + "','" + DateTime.Now.ToString("yyyyMMddHHmmss") + "','" + shifNo + "')";
                con.MysqlQuery(q);
                long shiftid = con.NonQueryEx();
                SessionData.SetUserShiftId(int.Parse(shiftid.ToString()));

                DataTable shiftno;
                string queryshiftno = "SELECT shift.shift_no FROM shift WHERE shift.id ='"+SessionData.shiftId+"' ";
                con.MysqlQuery(queryshiftno);
                shiftno = con.QueryEx();
                SessionData.SetUserShiftNo(int.Parse(shiftno.Rows[0][0].ToString()));
            }
         

            con.conClose();
        }

        //create login log
        void userLog(string user,string user_event) {
            con = new db();
            string q = "insert into user_log (user_id,event) values('" + user+ "','" + user_event + "')";
            con.MysqlQuery(q);
            con.NonQueryEx();
            con.conClose();
        }



        //CREATE IMAGELITS
        // Create and return imagelist for both categories and products
        private ImageList populateImageList()
        {
            string table = "categories";
            string query = "SELECT * FROM " + table + " WHERE online = 1";
            if (!populateFolder.Equals("categories"))
            {
                table = "products";
                query = "SELECT * FROM " + table + " WHERE online = 1  AND category_id = '" + category_id + "'";
            }
            else
            {
                table = "categories";
                query = "SELECT * FROM " + table + " WHERE online = 1";
            }
            con = new db();
            //ImageList
            ImageList imgList = new ImageList();
            imgList.Images.Clear();
            DataTable iamges;
            con.MysqlQuery(query);
            iamges = con.QueryEx();
            imgList.ImageSize = new Size(152, 100);
            imgList.ColorDepth = ColorDepth.Depth32Bit;

            foreach (DataRow row in iamges.Rows) // Loop over the rows.
            {
                try
                {
                    imgList.Images.Add(Image.FromFile(@"c:/xampp/htdocs/mpos/images/" + populateFolder + "/" + row["image"].ToString()));
                }
                catch (Exception w)
                {
                    MessageBox.Show(w.Message);
                }
            }
            con.conClose();
            return imgList;

        }
        //POPULATE LIST VIEW
        private void populate(MySqlDataReader data)
        {
            //ImageList
            ImageList imgList = new ImageList();
            //CLEAR listview_Category items
            listView1.Items.Clear();
            //set listview category items as Large icons
            listView1.View = View.LargeIcon;
            //ADD image list into Listview
            listView1.LargeImageList = populateImageList();
            int i = 0;
            //ADD image into imagelist and Listview
            while (data.Read())
            {
                ListViewItem category = new ListViewItem();
                category.ImageIndex = i;
                //set Category name
                category.Text = data.GetString(1);
                category.Tag = data.GetString(0);
                //set font list items styles
                category.Font = new System.Drawing.Font("Courier New", 11, System.Drawing.FontStyle.Bold);
                category.ForeColor = System.Drawing.Color.FromArgb(144, 30, 36);
                category.SubItems.Add(data.GetString(0));
                //ADD category items into Listview
                listView1.Items.Add(category);
                i++;
            }
            con.conClose();
        }

        //GET CATEGORIES
        private MySqlDataReader LoadCategories()
        {
            populateFolder = "categories";
            MySqlDataReader result;
            con = new db();
            con.MysqlQuery("SELECT * FROM categories WHERE online = 1");
            result = con.ExReader();
            return result;
        }

        //GET PRODUCTS
        private MySqlDataReader loadProducts()
        {
            populateFolder = "products";
            MySqlDataReader result;
            con = new db();
            con.MysqlQuery("SELECT * FROM products WHERE category_id = '" + category_id + "' AND online = 1");
            result = con.ExReader();
            return result;

        }

        //seperate neworder or repoen order
        public static int isNewItem = 1;
        private void setProductToListView(string id, string size)
        {
            DataTable product = new DataTable();
            //GET PRODUCT DETAILS (product ID and Product Size)
            product = getProduct(productId, size);
            for (int i = 0; i < product.Rows.Count; i++)
            {
                DataRow dr = product.Rows[i];
                dataGridView_cart.Rows.Add(
                    dr["id"].ToString(),
                    dr["itemcode"].ToString(),
                    dr["name"].ToString(),
                    dr["size"].ToString(),
                    productqty.ToString(),
                    String.Format("{0:n}", double.Parse(dr["price"].ToString())),
                    String.Format("{0:n}", calRowPrice(double.Parse(dr["price"].ToString()), productqty)),
                    dr["item_type"].ToString(),
                    isNewItem //newly added item or reopened item
                    );

               

                SessionData.SetCartTotal(double.Parse(dr["price"].ToString()) * productqty);
                SessionData.SetCartItemCount(1);
                //send to serial port
                //ComPort(dr["name"].ToString() + " x " + productqty.ToString(), "Sub- " + String.Format("{0:n}", subtotal));
                panel_menue.Hide();
                
            }

        }


        private void ShowCartTotal()
        {
         //   MessageBox.Show(SessionData.cartTotal.ToString());
            double discount = SessionData.discount;
            double srviceCharge = SessionData.serviceCharge;
            lbl_total.Text = String.Format("{0:n}", SessionData.cartTotal - (SessionData.cartTotal * SessionData.discount / 100) + (SessionData.cartTotal * SessionData.serviceCharge / 100));
            lbl_discount.Text = SessionData.discount.ToString() + '%';
            lbl_service_charge.Text = SessionData.serviceCharge.ToString() + '%';
            SessionData.SetLastbillamount();
            lbl_dprice.Text = String.Format("{0:n}",SessionData.discountedPrice);
            lbl_sprice.Text = String.Format("{0:n}",SessionData.servicePrice);

            lbl_total_items.Text = SessionData.itemCount.ToString();

           // MessageBox.Show(SessionData.lastbillamout.ToString());


           // SessionData.SetPayamount(SessionData.cartTotal - (discount / 100 * SessionData.cartTotal) + (srviceCharge / 100 * SessionData.cartTotal));
        }


        private DataTable getProduct(string productID, string size)
        {
            //MessageBox.Show("SELECT products.*,product_sizes.price,product_sizes.size FROM `products` JOIN product_sizes ON product_sizes.products_id= products.id WHERE product_sizes.products_id='" + productID + "' AND product_sizes.size='" + size + "' AND products.online=1 ");
            DataTable result;
            con = new db();
            con.MysqlQuery("SELECT products.*,products.id AS itemcode FROM `products` WHERE products.id='" + productID + "' AND products.online=1 ");
            result = con.QueryEx();
            con.conClose();
            return result;
        }

        //CALCULATE ROW PRICE (ITEM PRICE * QTY)
        private double calRowPrice(double price, int qty)
        {
            return price * qty;
        }


        public static double cash;
        public static double balance;
        private bool paymentSuccess = false;
        private string paymentStatus;
        public static bool paymentDone;
        void processOrder(int processType) {
            if (processType.Equals(1))
            { //dinein order
                createOrder(1);
            }
            else { //Takeaway order
                createOrder(2);
            }
        }


        //create new order
        private void createOrder(int orderType)
        {
            int orderstatus;
            if (orderType.Equals(1))
            {
                orderstatus = 0;
            }
            else
            {
                if (paymentDone)
                {
                    orderstatus = 1;
                }
                else {
                    orderstatus = 0;
                }
                
            }

            //DateTime localDate = DateTime.Now.ToString("yyyy-M-d");
            int noOfGuest = SessionData.guest;
            double cartPrice = SessionData.cartTotal;
            con = new db();
            string q = "insert into orders (created,guest,order_type,discount,service_charge,active,user_id) values('" + DateTime.Now.ToString("yyyy-MM-dd hh:mm tt") + "','" + noOfGuest + "','Default','" + SessionData.discount + "','" + SessionData.serviceCharge + "','" + orderstatus + "','" + SessionData.userid + "')";

            con.MysqlQuery(q);
            con.NonQueryEx();

            //SET ORDER ID
            SessionData.SetNewOrderId(con.cmd.LastInsertedId);
            con.conClose();


        }
        //INSERT ORDER DETAILS
        private void gridviewDataIntoDb()
        {
            db con = new db();
            DataTable shift;
            //get shift
            string queryshift = "SELECT shift.id,shift.users_id,users.username,shift.shift_no FROM `shift` JOIN users ON users.id = shift.users_id WHERE shift.shift_end IS NULL ORDER BY shift.id DESC LIMIT 1";
            con.MysqlQuery(queryshift);
            shift = con.QueryEx();

            for (int i = 0; i < dataGridView_cart.Rows.Count; i++)
            {
                //inset only new items
                if (dataGridView_cart.Rows[i].Cells["newitem"].Value.Equals(1))
                {
                    con.MysqlQuery("INSERT INTO order_details (order_id,product_id,size,qty,unit_price,kot_status,subtotal,item_type,shift_id,shift_no) VALUES('" + SessionData.newOrderId + "','" + dataGridView_cart.Rows[i].Cells["itemcode"].Value.ToString() + "','" + dataGridView_cart.Rows[i].Cells["size"].Value.ToString() + "','" + dataGridView_cart.Rows[i].Cells["qty"].Value.ToString() + "','" + double.Parse(dataGridView_cart.Rows[i].Cells["price"].Value.ToString()) + "','" + 0 + "','" + double.Parse(dataGridView_cart.Rows[i].Cells["subtotal"].Value.ToString()) + "','" + dataGridView_cart.Rows[i].Cells["item_type"].Value.ToString() + "','" + SessionData.shiftId + "','" + SessionData.shiftno + "');");
                    con.NonQueryEx();
                }
            }
            con.conClose();
            //update tabel details
            updateGuestTabelDetails();

            updateOrderDiscount();

        }

        private void updateOrderDiscount() {
            db con = new db();
            string query = "";
            query = "UPDATE `orders` SET `discount`='" + SessionData.discount + "' WHERE id = '" + SessionData.newOrderId + "'";
            con.MysqlQuery(query);
            con.NonQueryEx();
            con.conClose();
        
        }


        private void updateGuestTabelDetails()
        {
            db con = new db();
            string query = "";
            query = "UPDATE `orders` SET `tabel`='" + SessionData.tabel + "' WHERE id = '" + SessionData.newOrderId + "'";
            con.MysqlQuery(query);
            con.NonQueryEx();
            con.conClose();
        }




        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }


        //reopen order
        private void button16_Click(object sender, EventArgs e)
        {
            if (txt_selected_orderid.Text != "")
            {
                updatemode = true;
                tab_control.SelectedTab = tabPage1;
                SessionData.SetNewOrderId(long.Parse(txt_selected_orderid.Text));
                reOpenOrder();
                ShowCartTotal();
                btn_dine_in.Visible = false;
                btn_save.Visible = true;
                btn_take_away.Enabled = false;
                btn_pay.Enabled = true;
                btn_logout.Enabled = true;
                panel_menue.Hide();
            }
            else {
                MessageBox.Show("Select Order!");
            }
        }

        private void btn_menue_Click(object sender, EventArgs e)
        {
            panel_menue.Show();
        }

        private void dataGridView2_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }


        //disable row selection
        private void dataGridView2_SelectionChanged(object sender, EventArgs e)
        {
            //dataGridView2.ClearSelection();

            // selected sel value
           // txt_selected_orderid.Text = dataGridView2.CurrentCell.Value.ToString();
        }


        
        private void btn_void_from_allorders_Click(object sender, EventArgs e)
        {
            if (txt_selected_orderid.Text != "")
            {
                SessionData.SetNewOrderId(long.Parse(txt_selected_orderid.Text));
                Form_auth frmauth = new Form_auth();
                frmauth.ShowDialog();

                if (managerPassword)
                {
                    //canceled order
                    db con = new db();
                    string query = "";
                    query = "UPDATE `order_details` SET `online`='0' WHERE order_id = '" + SessionData.newOrderId + "'";
                    con.MysqlQuery(query);
                    con.NonQueryEx();
                    con.conClose();

                    dataGridView2.Rows.Clear();
                    dataGridView2.Refresh();
                    loadOrderDetails();
                    userLog(authid, "void order: " + SessionData.newOrderId);
                }
                else
                {
                    MessageBox.Show("Authentication Faild!");
                }
            }
            else {
                MessageBox.Show("Order Id not found. please select the order or check the order ID.");
            }
        }


        //menue
        private void btn_menue_Click_1(object sender, EventArgs e)
        {
            panel_menue.Show();
        }

        private void btn_menuePanelHide_Click(object sender, EventArgs e)
        {
            if (populateFolder.Equals("products"))
            {
                populateFolder = "categories";
                populate(LoadCategories());
                con.conClose();
                lbl_menueTitle.Text = "CATEGORIES";
                menu = 1;
            }
            else {
                panel_menue.Hide();
            }  
        }

        private void Form_display_Load(object sender, EventArgs e)
        {
            populate(LoadCategories());
            con.conClose();
            btn_pay.Enabled = false;
            

            //datagridview styles
            dataGridView2.Columns[1].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Regular);
            dataGridView_cart.Columns[2].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Regular);
            dataGridView_cart.Columns[3].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 10, FontStyle.Regular);
            dataGridView_cart.Columns[5].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 11, FontStyle.Regular);
            dataGridView_cart.Columns[6].DefaultCellStyle.Font = new System.Drawing.Font("Verdana", 11, FontStyle.Regular);

            //setFirstOrderId();
            lbl_newOrderCount.Visible = false;
            label5.Visible = false;
            label7.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            lbl_date.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        }

        


        //menue click
        private void listView1_Click(object sender, EventArgs e)
        {
            if (populateFolder.Equals("categories"))
            {
                populateFolder = "products";
            }

            try
            {
                category_id = int.Parse(listView1.SelectedItems[0].Tag.ToString());
                if (menu.Equals(1))
                {
                    lbl_menueTitle.Text = "PRODUCTS";
                    menu = 2; //set menu display products
                    populate(loadProducts());//load products
                    con.conClose();
                }
                else
                {
                    lbl_menueTitle.Text = "CATEGORIES";
                    productId = listView1.SelectedItems[0].Tag.ToString();

                    //ProductSize formProductsize = new ProductSize();
                    //formProductsize.ShowDialog();
                    //reset command
                    if (addProduct.Equals(true))
                    {
                        addProduct = false;
                    }

                    Form_Productqty formProductQty = new Form_Productqty();
                    formProductQty.ShowDialog();

                    if (productqty > 0)
                    {
                        setProductToListView(productId, "L");


                        SessionData.SetSeviceCharge(10);
                        ShowCartTotal();
                        //cash display
                        // setCashdisplay(1);


                        // textBox2.Text = paid.ToString();
                        // calculatePrice();
                        //  button1.Show();
                        //   }

                        //  restMainMenu();
                    }

                    productqty = 0;

                  
                }
            }
            catch (Exception ea)
            {
                MessageBox.Show("Liustview click error " + ea.Message);
            }
        }


        private void btn_take_away_Click(object sender, EventArgs e)
        {
            SessionData.SetSeviceCharge(0);
            SessionData.SetTabelDetails("TAKEAWAY");
            if (SessionData.cartTotal > 1)
            {
                Form_Pay frmpay = new Form_Pay();
                frmpay.ShowDialog();

                if (paymentDone)
                {
                    createOrder(2);
                    gridviewDataIntoDb();
                    updateOrder();

                   // if (SessionData.paymentType.Equals(1))
                   // {
                        //save card details
                        saveCardDetails();
                        List<Receipt> order;
                        order = LoadReceiptData();
                        SessionData.setBillamount();



                        Billprint bill = new Billprint(order, cash, SessionData.lastbillamout,SessionData.tabel,balance);
                      //  bill.print("CASHIER");
                        bill.print(bilprinter);
                       order.Clear();
                       
                   // }
                       resetAll();
                }
                
            }
        }

        public static bool tabelStatus = false;
        private void btn_dine_in_Click(object sender, EventArgs e)
        {
            if (SessionData.cartTotal > 1)
            {
                Form_dineIn frmdinein = new Form_dineIn();
                frmdinein.ShowDialog();
                if (tabelStatus)
                {
                    createOrder(1);
                    gridviewDataIntoDb();//save order details
                    updateOrder();
                    resetAll();
                    loadOrderDetails();
                    tabelStatus = false;
                }
            }
        }

        private void updateOrder()
        {
            db con = new db();
            string query = "";
            if (paymentDone)
            {
                query = "UPDATE `orders` SET `paid`='" + SessionData.payamount + "',`active`='0' WHERE id = '" + SessionData.newOrderId + "'";
            }
            else
            {
                query = "UPDATE `orders` SET `paid`='" + SessionData.payamount + "',`active`='1' WHERE id = '" + SessionData.newOrderId + "'";
            }
            con.MysqlQuery(query);
            con.NonQueryEx();
            con.conClose();
        }

        void resetAll() {
            paymentDone = false;
            dataGridView_cart.Rows.Clear();
            dataGridView_cart.Refresh();
            SessionData.SetDiscount(0);
            SessionData.ResetCartTotal();
            SessionData.SetCartTotal(-SessionData.cartTotal);
            SessionData.SetpaymentType(1);
            SessionData.SetCardName("");
            SessionData.SetCardNumber(0);
            SessionData.SetCardtype("");
            SessionData.SetCartItemCount(-SessionData.itemCount);
            SessionData.SetPayamount(-SessionData.payamount);
            SessionData.setBillamount();
            SessionData.SetTabelDetails("");
            SessionData.SetGuest(-SessionData.guest);
            lbl_total.Text = String.Format("{0:n}", 0);
            lbl_discount.Text = "0%";
            lbl_service_charge.Text ="0%";
            lbl_sprice.Text = "0.00";
            lbl_total_items.Text = SessionData.itemCount.ToString();
            lbl_dprice.Text = "0.00";
            updatemode = false;

           // GC.Collect();
           // GC.WaitForPendingFinalizers();
        }

        private void btn_pay_Click(object sender, EventArgs e)
        {
            Form_Pay frmpay = new Form_Pay();
            frmpay.ShowDialog();

            if (paymentDone) {
                if (SessionData.paymentType.Equals(1))
                {
                    //save card details
                    saveCardDetails();
                    updateOrder();
                    //Set Receipt data
                    List<Receipt> order;
                    order = LoadReceiptData();
                    SessionData.setBillamount();

                    Billprint bill = new Billprint(order, cash, SessionData.lastbillamout,SessionData.tabel,balance);
                    // bill.print("CASHIER");
                    bill.print(bilprinter);
                    bill = null;
                    order.Clear();
                }
                else { 
                //cash payment
                    updateOrder();
                    //Set Receipt data
                    List<Receipt> order;
                    order = LoadReceiptData();
                    SessionData.setBillamount();

                    Billprint bill = new Billprint(order, cash, SessionData.lastbillamout,SessionData.tabel,balance);
                    // bill.print("CASHIER");
                    bill.print(bilprinter);
                    bill = null;
                    order.Clear();
                }
                resetAll();

                btn_save.Visible = false;
                btn_dine_in.Visible = true;
                btn_take_away.Enabled = true;
                btn_pay.Enabled = false;
                label1.Visible = false;
                label3.Visible = false;
                lbl_reopen_orderid.Visible = false;
                lbl_reopen_stwerd.Visible = false;
                lbl_table.Visible = false;
                lbl_tableno.Visible = false;
                //GC.Collect();
                //GC.WaitForPendingFinalizers();

            }

            loadOrderDetails();
        }

        void saveCardDetails() {
            db con = new db();
            con.MysqlQuery("INSERT INTO paymentdetails (cardname,cardno,cardtype,amount,orders_id) VALUES('" + SessionData.cardname + "','" + SessionData.cardlastdigits + "','" + SessionData.cardtype + "','" + SessionData.lastbillamout + "','" + SessionData.newOrderId + "');");
            con.NonQueryEx();
            con.conClose();
        }

        private DataTable getAllordersForProcess() {
            DataTable orders;
            con = new db();
            //con.MysqlQuery("SELECT orders.id,orders.tabel,users.username,(SELECT COUNT(order_details.id) FROM order_details WHERE order_details.order_id=orders.id AND order_details.item_type=1) kotorders,(SELECT COUNT(order_details.id) FROM order_details WHERE order_details.order_id=orders.id AND order_details.item_type=2) botorders,(SELECT COUNT(order_details.id) FROM order_details WHERE order_details.order_id=orders.id AND order_details.online=0) canceledorders,(SELECT COUNT(order_details.id) FROM order_details WHERE order_details.order_id=orders.id) AS itemcount ,(SELECT SUM(order_details.subtotal) FROM order_details WHERE order_details.order_id=orders.id) AS due FROM orders JOIN order_details ON order_details.order_id = orders.id JOIN users ON users.id=orders.user_id WHERE (SELECT SUM(order_details.subtotal) FROM order_details WHERE order_details.order_id=orders.id)- (SELECT SUM(order_details.subtotal) FROM order_details WHERE order_details.order_id=orders.id)*orders.discount/100 + (SELECT SUM(order_details.subtotal) FROM order_details WHERE order_details.order_id=orders.id)*orders.service_charge/100 -  orders.paid > 0 AND (SELECT COUNT(order_details.id) FROM order_details WHERE order_details.order_id=orders.id AND order_details.online=1) >0 GROUP BY orders.id ORDER BY orders.id DESC"); //30-08-2017
            con.MysqlQuery("SELECT orders.id,orders.tabel,users.username,(SELECT COUNT(order_details.id) FROM order_details WHERE order_details.order_id=orders.id AND order_details.item_type=1) kotorders,(SELECT COUNT(order_details.id) FROM order_details WHERE order_details.order_id=orders.id AND order_details.item_type=2) botorders,(SELECT COUNT(order_details.id) FROM order_details WHERE order_details.order_id=orders.id AND order_details.online=0) canceledorders,(SELECT COUNT(order_details.id) FROM order_details WHERE order_details.online = 1 AND order_details.order_id=orders.id) AS itemcount ,(SELECT SUM(order_details.subtotal) FROM order_details WHERE order_details.online = 1 AND order_details.order_id=orders.id) AS due FROM orders JOIN order_details ON order_details.order_id = orders.id JOIN users ON users.id=orders.user_id WHERE (SELECT SUM(order_details.subtotal) FROM order_details WHERE order_details.online = 1 AND order_details.order_id=orders.id)- (SELECT SUM(order_details.subtotal) FROM order_details WHERE  order_details.online = 1 AND order_details.order_id=orders.id)*orders.discount/100 + (SELECT SUM(order_details.subtotal) FROM order_details WHERE order_details.online = 1 AND order_details.order_id=orders.id)*orders.service_charge/100 -  orders.paid > 0 AND (SELECT COUNT(order_details.id) FROM order_details WHERE order_details.order_id=orders.id AND order_details.online=1) >0 GROUP BY orders.id ORDER BY orders.id DESC");// 30-08-2017
            orders = con.QueryEx();
            con.conClose();
            return orders;
        }


        private long firstOrderId;
        void setFirstOrderId() {
            DataTable orders;
            con = new db();
            con.MysqlQuery("SELECT orders.id FROM orders WHERE orders.active = 1 ORDER BY orders.id DESC LIMIT 1 ");
            orders = con.QueryEx();
            con.conClose();

            if (orders.Rows.Count > 0) {
                firstOrderId = long.Parse(orders.Rows[0][0].ToString());
            }
        }

        private long lastOrderId;
        void checkNewOrders() {
            //SELECT orders.id FROM orders WHERE orders.active = 1 ORDER BY orders.id DESC LIMIT 1 
            DataTable orders;
            con = new db();
            con.MysqlQuery("SELECT orders.id FROM orders WHERE orders.active = 1 ORDER BY orders.id DESC LIMIT 1 ");
            orders = con.QueryEx();
            con.conClose();

            if (orders.Rows.Count > 0)
            {
                lastOrderId = long.Parse(orders.Rows[0][0].ToString());
            }

            if (lastOrderId > firstOrderId) {
                SessionData.SetNewOrderCount(int.Parse(lastOrderId.ToString()) - int.Parse(firstOrderId.ToString()));
                
            }

        }

        private void loadOrderDetails()
        {
            DataTable orders = getAllordersForProcess();
            string gtabel;
            dataGridView2.Rows.Clear();
            dataGridView2.Refresh();
            if (orders.Rows.Count > 0) {

                
                
                for (int i = 0; i < orders.Rows.Count; i++)
                {
                    DataRow dr = orders.Rows[i];
                    if (dr["tabel"].ToString() != "")
                    {
                        gtabel = dr["tabel"].ToString();
                    }
                    else
                    {
                        gtabel = "NOT DEFINED";
                    }
                   

                    dataGridView2.Rows.Add(
                        dr["id"].ToString(),
                        gtabel,
                        dr["itemcount"].ToString(),
                        String.Format("{0:n}", double.Parse(dr["due"].ToString())),
                        dr["username"].ToString()
                        );
                }
               
                
            }
            orders.Clear();
            //setFirstOrderId();
            //checkNewOrders();
            //firstOrderId = lastOrderId;
            lbl_newOrderCount.Text = SessionData.newordercount.ToString();
            

         //   GC.Collect();
          //  GC.WaitForPendingFinalizers();

        }

        void refreshOrder() {
            DataTable orders = getAllordersForProcess();

            label24.Text = orders.Rows.Count.ToString();//orders count
            int kotorders = 0;
            int botorders = 0;
            int cancelorders = 0;
            string gtabel;

            if (orders.Rows.Count > 0)
            {
                for (int i = 0; i < orders.Rows.Count; i++)
                {
                    DataRow dr = orders.Rows[i];
                    kotorders += int.Parse(dr["kotorders"].ToString());
                    botorders += int.Parse(dr["botorders"].ToString());
                    cancelorders += int.Parse(dr["canceledorders"].ToString());
                }
                label28.Text = kotorders.ToString();
                label27.Text = botorders.ToString();
            }

            lbl_newOrderCount.Text = SessionData.newordercount.ToString();
        }

        void checkOrders()// check print status
        {
            DataTable orders;
            con = new db();
            string query = "SELECT orders.*,users.username FROM `orders` JOIN users ON users.id=orders.user_id JOIN order_details ON order_details.order_id = orders.id WHERE (SELECT COUNT(order_details.id) FROM order_details WHERE order_details.print_status = 0 AND order_details.order_id=orders.id) > 0 ";
            con.MysqlQuery(query);
            orders = con.QueryEx();

            if (orders.Rows.Count > 0)
            {
                for (int i = 0; i < orders.Rows.Count; i++)
                {
                    DataRow dr = orders.Rows[i];
                    int orderid = int.Parse(dr["id"].ToString());
                    string tabel = dr["tabel"].ToString();
                    processBotOrders(orderid, tabel, dr["username"].ToString());
                    Thread.Sleep(200); 
                    processKotOrders(orderid, tabel, dr["username"].ToString());


                }
            }
            con.conClose();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            //set summary
            refreshOrder();
            loadOrderDetails();
            //check print ststus and print KOT/BOT
            checkOrders();
            //check new orders
            //checkNewOrders();
        }


        void reOpenOrder() {
            resetAll();
            dataGridView_cart.Rows.Clear();
            dataGridView_cart.Refresh();
            
            DataTable order;
            con = new db();
            con.MysqlQuery("SELECT orders.tabel,orders.discount,order_details.*,products.name,products.id AS itemcode FROM order_details JOIN products ON products.id=order_details.product_id JOIN orders ON orders.id = order_details.order_id WHERE order_details.order_id = '" + SessionData.newOrderId + "' AND order_details.online=1");
            order = con.QueryEx();

            DataTable reopenorder;
            string query = "select users.username,orders.tabel from orders join users on users.id = orders.user_id where orders.online = 1 and orders.id = '" + SessionData.newOrderId + "'";
            con.MysqlQuery(query);
            reopenorder = con.QueryEx();


            label1.Visible = true;
            label3.Visible = true;
            lbl_reopen_orderid.Visible = true;
            lbl_reopen_stwerd.Visible = true;
            lbl_table.Visible = true;
            lbl_tableno.Visible = true;
            lbl_reopen_orderid.Text = SessionData.newOrderId.ToString();
            lbl_reopen_stwerd.Text = reopenorder.Rows[0][0].ToString();
            lbl_tableno.Text = reopenorder.Rows[0][1].ToString();



            con.conClose();
            for (int i = 0; i < order.Rows.Count; i++)
            {
                DataRow dr = order.Rows[i];
                dataGridView_cart.Rows.Add(
                    dr["id"].ToString(),
                    dr["itemcode"].ToString(),
                    dr["name"].ToString(),
                    dr["size"].ToString(),
                    dr["qty"].ToString(),
                    String.Format("{0:n}", double.Parse(dr["unit_price"].ToString())),
                    String.Format("{0:n}", calRowPrice(double.Parse(dr["unit_price"].ToString()), int.Parse(dr["qty"].ToString()))),
                    dr["item_type"].ToString(),
                    0 //newly added item or reopened item
                    );
                SessionData.SetTabelDetails(dr["tabel"].ToString());
                SessionData.SetDiscount(double.Parse(dr["discount"].ToString()));
                SessionData.SetSeviceCharge(10);
                SessionData.SetCartTotal(double.Parse(dr["unit_price"].ToString()) * int.Parse(dr["qty"].ToString()));
                SessionData.SetCartItemCount(1);
            }

            updatemode = true;
           
        }

        private void btn_save_Click(object sender, EventArgs e)
        {
            gridviewDataIntoDb();
            ShowCartTotal();
            resetAll();
            btn_dine_in.Visible = true;
            btn_save.Visible = false;
            btn_take_away.Enabled = true;
            btn_pay.Enabled = false;
            dataGridView_cart.Rows.Clear();
            dataGridView_cart.Refresh();
            loadOrderDetails();
            //reopen order details
            label1.Visible = false;
            label3.Visible = false;
            lbl_reopen_orderid.Visible = false;
            lbl_reopen_stwerd.Visible = false;
            lbl_table.Visible = false;
            lbl_tableno.Visible = false;

            //service charge 
            lbl_sprice.Text = "0.00";
        }

      

        

        void processKotOrders(int orderid, string tabel,string std)
        {
            DataTable kotorders;
            con = new db();
            string query = "select order_details.*,products.name from order_details join products on products.id = order_details.product_id where order_details.item_type = '1' and order_details.order_id ='" + orderid + "' AND order_details.print_status=0 ";
            con.MysqlQuery(query);
            kotorders = con.QueryEx();
            SessionData.SetStward(std);
            if (kotorders.Rows.Count > 0)
            {
                //print kot orders
                Kotprint kotprint = new Kotprint(orderid, kotorders, tabel);
                kotprint.print(kotprinter);//KOT PRINTER
             //   kotprint.print("KOT");//KOT PRINTER
                updateOrderPrintStatus(orderid);
            }
            con.conClose();
        }



        void processBotOrders(int orderid, string tabel,string std)
        {
            DataTable botorders;
            con = new db();
            string query = "select order_details.*,products.name from order_details join products on products.id = order_details.product_id where order_details.item_type = '2' and order_details.order_id ='" + orderid + "' AND order_details.print_status=0";
            con.MysqlQuery(query);
            botorders = con.QueryEx();
            SessionData.SetStward(std);
            if (botorders.Rows.Count > 0)
            {
                //print bot orders
                Botprint botprint = new Botprint(orderid, botorders, tabel);
                botprint.print(botprinter);//BOT PRINTER
              //  botprint.print("BOT");//BOT PRINTER
                updateOrderPrintStatus(orderid);
               

            }
            con.conClose();
        }

        void updateOrderPrintStatus(int id)
        {
            db con = new db();
            string query = "";
            query = "UPDATE `order_details` SET `print_status`= '1' WHERE order_id = '" + id + "' AND print_status = 0";
            con.MysqlQuery(query);
            con.NonQueryEx();
            con.conClose();
        }

        private List<Receipt> LoadReceiptData()
        {
            //Datagridview data into Receipt
            for (int i = 0; i < dataGridView_cart.Rows.Count; i++)
            {
                orderDetails.Add(new Receipt()
                {
                    id = dataGridView_cart.Rows[i].Cells["itemcode"].Value.ToString(),
                    product = dataGridView_cart.Rows[i].Cells["itemname"].Value.ToString(),
                    size = "L",
                    qty = int.Parse(dataGridView_cart.Rows[i].Cells["qty"].Value.ToString()),
                    unit_price = double.Parse(dataGridView_cart.Rows[i].Cells["price"].Value.ToString())
                });
            }

            return orderDetails;
        }


        //print
        private void btn_logout_Click(object sender, EventArgs e)
        {

          //  MessageBox.Show(updatemode.ToString());
            if (dataGridView_cart.Rows.Count > 0)
            {
                ShowCartTotal();
                gridviewDataIntoDb();
                loadOrderDetails();
                //this.Close();
                //Set Receipt data
                List<Receipt> order;
                order = LoadReceiptData();
                SessionData.setBillamount();

                Billprint bill = new Billprint(order, SessionData.lastbillamout, SessionData.tabel, 1);
                // bill.print("CASHIER");
                bill.print(bilprinter);
                order.Clear();
                orderDetails.Clear();

              //  GC.Collect();
              //  GC.WaitForPendingFinalizers();
                

                btn_logout.Enabled = false;
            }
            else {
                MessageBox.Show("No order selected. Please select an order first.",
        "Print information", MessageBoxButtons.OK,
            MessageBoxIcon.Information);
            }
           
        }

        private void label16_Click(object sender, EventArgs e)
        {
            string orid = "";
            if (dataGridView_cart.SelectedCells.Count > 0)
            {
                Form_auth frmauth = new Form_auth();
                frmauth.ShowDialog();

                if (authproceed)
                {
                    if (managerPassword)
                    {
                        //canceled order
                        //update order details
                        db con = new db();
                        string query = "";
                        query = "UPDATE `order_details` SET `online`='0'  WHERE order_id = '" + SessionData.newOrderId + "'";
                        con.MysqlQuery(query);
                        con.NonQueryEx();

                        //update order
                        query = "UPDATE `orders` SET `active`='0', `online` ='0'  WHERE id = '" + SessionData.newOrderId + "'";
                        con.MysqlQuery(query);
                        con.NonQueryEx();


                        con.conClose();


                        userLog(authid, "void order: " + SessionData.newOrderId);

                        dataGridView2.Rows.Clear();
                        dataGridView2.Refresh();
                        loadOrderDetails();

                        resetAll();
                        dataGridView_cart.Rows.Clear();
                        dataGridView_cart.Refresh();
                        printVoidReceipt();

                        VoidOrder voidreceipt = new VoidOrder(_voidOrder);
                        voidreceipt.print(SessionData._defaultPrinter);
                        managerPassword = false;

                        label1.Visible = false;
                        label3.Visible = false;
                        lbl_reopen_orderid.Visible = false;
                        lbl_reopen_stwerd.Visible = false;
                        lbl_table.Visible = false;
                        lbl_tableno.Visible = false;
                        //GC.Collect();
                        //GC.WaitForPendingFinalizers();
                    }
                    else
                    {
                        MessageBox.Show("Authentication Faild!");
                    }
                    authproceed = false;
                }

            }
            else
            {

                MessageBox.Show("Empty Cart!");
            }
        }


       //void ORDER
        DataTable _voidOrder;
        private void btn_void_Click(object sender, EventArgs e)
        {
            string orid="";
            if (dataGridView_cart.SelectedCells.Count > 0)
            {
                Form_auth frmauth = new Form_auth();
                frmauth.ShowDialog();

                if (authproceed) {
                    if (managerPassword)
                    {
                        //canceled order
                        //update order details
                        db con = new db();
                        string query = "";
                        query = "UPDATE `order_details` SET `online`='0'  WHERE order_id = '" + SessionData.newOrderId + "'";
                        con.MysqlQuery(query);
                        con.NonQueryEx();

                        //update order
                        query = "UPDATE `orders` SET `active`='0', `online` ='0'  WHERE id = '" + SessionData.newOrderId + "'";
                        con.MysqlQuery(query);
                        con.NonQueryEx();


                        con.conClose();


                        userLog(authid, "void order: " + SessionData.newOrderId);

                        dataGridView2.Rows.Clear();
                        dataGridView2.Refresh();
                        loadOrderDetails();

                        resetAll();
                        dataGridView_cart.Rows.Clear();
                        dataGridView_cart.Refresh();
                        printVoidReceipt();

                        VoidOrder voidreceipt = new VoidOrder(_voidOrder);
                        voidreceipt.print(SessionData._defaultPrinter);
                        managerPassword = false;

                        btn_save.Visible = false;
                        btn_dine_in.Visible = true;
                        btn_take_away.Enabled = true;
                        btn_pay.Enabled = false;
                        label1.Visible = false;
                        label3.Visible = false;
                        lbl_reopen_orderid.Visible = false;
                        lbl_reopen_stwerd.Visible = false;
                        lbl_table.Visible = false;
                        lbl_tableno.Visible = false;
                        //GC.Collect();
                        //GC.WaitForPendingFinalizers();
                    }
                    else
                    {
                        MessageBox.Show("Authentication Faild!");
                    }
                    authproceed = false;
                }
               
            }
            else {

                MessageBox.Show("Empty Cart!");
            }

            
            
        }

        void printVoidReceipt() {
            long orderId = SessionData.newOrderId;
            DataTable result;
            con = new db();
            string query = "SELECT order_details.*,products.name AS itemname,(SELECT SUM(order_details.subtotal) FROM order_details WHERE order_details.order_id ='" + orderId + "') AS total FROM order_details JOIN products ON products.id = order_details.product_id WHERE order_details.order_id = '" + orderId + "'";
            con.MysqlQuery(query);
            result = con.QueryEx();
            con.conClose();
            _voidOrder = result;
        }



        private void btn_discount_Click(object sender, EventArgs e)
        {
            Discount frmdiscount = new Discount();
            frmdiscount.ShowDialog();

            SessionData.setBillamount();
            ShowCartTotal();


        }

        private void label20_Click(object sender, EventArgs e)
        {

        }


        public static bool proceedSearchProduct;
        private void btn_product_search_Click(object sender, EventArgs e)
        {
            Form_SearchProduct frmsearch = new Form_SearchProduct();
            frmsearch.ShowDialog();
            if (proceedSearchProduct) {
                Form_Productqty formProductQty = new Form_Productqty();
                formProductQty.ShowDialog();
                setProductToListView(productId, "L");
                SessionData.SetSeviceCharge(10);
                ShowCartTotal();
            }
            
        }

        private void btn_delete_item_Click(object sender, EventArgs e)
        {

            if (dataGridView_cart.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView_cart.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView_cart.Rows[selectedrowindex];
                int itemid = int.Parse(selectedRow.Cells[0].Value.ToString());
                int isnewitem = int.Parse(selectedRow.Cells[8].Value.ToString());

                        if (isnewitem.Equals(1))
                        { //if newly added item
                            double itemprice = double.Parse(selectedRow.Cells[6].Value.ToString());
                            SessionData.UpdateCartTotal(itemprice);
                            foreach (DataGridViewRow item in this.dataGridView_cart.SelectedRows)
                            {
                                dataGridView_cart.Rows.RemoveAt(item.Index);
                            }
                            SessionData.DeleteItem();
                            ShowCartTotal();
                        }
                        else {
                            Form_auth frmauth = new Form_auth();
                            frmauth.ShowDialog();
                            if (authproceed)
                            { // if auth has process or just closed
                                if (managerPassword)
                                { //if manager password correct
                                    voidOrderItems(itemid);
                                    userLog(authid, "void order item : " + itemid);
                                    dataGridView_cart.Rows.Clear();
                                    dataGridView_cart.Refresh();

                                    dataGridView2.Rows.Clear();
                                    dataGridView2.Refresh();
                                    loadOrderDetails();
                                    reOpenOrder();
                                    managerPassword = false;
                                }//manager correct
                                authproceed = false;
                            }//auth process
                        }
            }
        }

        void voidOrderItems(int itemid) {
        //    MessageBox.Show(itemid.ToString());
            db con = new db();
            string query = "";
            query = "UPDATE `order_details` SET `online`='0' WHERE id = '" + itemid + "'";
            con.MysqlQuery(query);
            con.NonQueryEx();
            con.conClose();
            reOpenOrder();
        }

        private void Form_display_FormClosing(object sender, FormClosingEventArgs e)
        {
            userLog(SessionData.userid,"logout");
        }

        private void label21_Click(object sender, EventArgs e)
        {
            if (dataGridView_cart.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView_cart.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView_cart.Rows[selectedrowindex];
                int itemid = int.Parse(selectedRow.Cells[0].Value.ToString());
                int isnewitem = int.Parse(selectedRow.Cells[8].Value.ToString());

                if (isnewitem.Equals(1))
                { //if newly added item
                    double itemprice = double.Parse(selectedRow.Cells[6].Value.ToString());
                    SessionData.UpdateCartTotal(itemprice);
                    foreach (DataGridViewRow item in this.dataGridView_cart.SelectedRows)
                    {
                        dataGridView_cart.Rows.RemoveAt(item.Index);
                    }
                    SessionData.DeleteItem();
                    ShowCartTotal();
                }
                else
                {
                    Form_auth frmauth = new Form_auth();
                    frmauth.ShowDialog();
                    if (authproceed)
                    { // if auth has process or just closed
                        if (managerPassword)
                        { //if manager password correct
                            voidOrderItems(itemid);
                            userLog(authid, "void order item : " + itemid);
                            dataGridView_cart.Rows.Clear();
                            dataGridView_cart.Refresh();

                            dataGridView2.Rows.Clear();
                            dataGridView2.Refresh();
                            loadOrderDetails();
                            reOpenOrder();
                            managerPassword = false;
                        }//manager correct
                        authproceed = false;
                    }//auth process
                }
            }
        }

        private void btn_up_Click(object sender, EventArgs e)
        {
            if (dataGridView_cart.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView_cart.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView_cart.Rows[selectedrowindex];
                int itemid = int.Parse(selectedRow.Cells[0].Value.ToString());
                int isnewitem = int.Parse(selectedRow.Cells[8].Value.ToString());

               // MessageBox.Show(isnewitem.ToString());

                if (isnewitem.Equals(1)) {
                    selectedRow.Cells[4].Value = int.Parse(selectedRow.Cells[4].Value.ToString()) + 1;
                }


                
             //   voidOrderItems(itemid);
              //  userLog(authid, "void order item : " + itemid);
              //  dataGridView_cart.Rows.Clear();
              //  dataGridView_cart.Refresh();
              //  loadOrderDetails();
              //  reOpenOrder();
            }
        }

        private void btn_down_Click(object sender, EventArgs e)
        {
            if (dataGridView_cart.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView_cart.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView_cart.Rows[selectedrowindex];
                int itemid = int.Parse(selectedRow.Cells[0].Value.ToString());
                int isnewitem = int.Parse(selectedRow.Cells[8].Value.ToString());

                // MessageBox.Show(isnewitem.ToString());

                if (isnewitem.Equals(1))
                {
                    if (int.Parse(selectedRow.Cells[4].Value.ToString()) > 0)
                    {
                        selectedRow.Cells[4].Value = int.Parse(selectedRow.Cells[4].Value.ToString()) - 1;
                    }
                    else {
                        MessageBox.Show("Invaid");
                        selectedRow.Cells[4].Value = 1;
                    }
                    
                }



                //   voidOrderItems(itemid);
                //  userLog(authid, "void order item : " + itemid);
                //  dataGridView_cart.Rows.Clear();
                //  dataGridView_cart.Refresh();
                //  loadOrderDetails();
                //  reOpenOrder();
            }
        }

        private void pictureBox_logo_Click(object sender, EventArgs e)
        {

        }

        public static bool logoutStatus=false;
        
        
        private void btnLogOut_Click(object sender, EventArgs e)
        {
            FormConfirmLogout frmcl = new FormConfirmLogout();
            frmcl.ShowDialog();

            if (logoutStatus)
            {
                logoutStatus = false;
                this.Close();
            }
            else {

                FormWakeUp frmwp = new FormWakeUp();
                frmwp.ShowDialog();
            
            }

        }

        private void dataGridView_cart_Scroll(object sender, ScrollEventArgs e)
        {
            //vScrollBar1.Value = e.NewValue;
        }

        private void vScrollBar1_Scroll(object sender, ScrollEventArgs e)
        {
            dataGridView_cart.FirstDisplayedScrollingRowIndex = e.NewValue;
        }


        public static bool cmd_editorder;
        private void dataGridView2_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedCells.Count > 0)
            {
                int selectedrowindex = dataGridView2.SelectedCells[0].RowIndex;
                DataGridViewRow selectedRow = dataGridView2.Rows[selectedrowindex];
                string a = Convert.ToString(selectedRow.Cells[0].Value);

                Form_ChangeGuestTable frmchangegtable = new Form_ChangeGuestTable(long.Parse(a.ToString()));
                frmchangegtable.ShowDialog();

                if (cmd_editorder)
                { //open order
                    updatemode = true;
                    tab_control.SelectedTab = tabPage1;
                    SessionData.SetNewOrderId(long.Parse(a));
                    reOpenOrder();
                    ShowCartTotal();
                    btn_dine_in.Visible = false;
                    btn_save.Visible = true;
                    btn_take_away.Enabled = false;
                    btn_pay.Enabled = true;
                    btn_logout.Enabled = true;
                    panel_menue.Hide();
                }
            }
            loadOrderDetails();
        }


        public static long continueOrderId;
        private void btn_continue_order_Click(object sender, EventArgs e)
        {
            Form_continue_order frmconor = new Form_continue_order();
            frmconor.ShowDialog();

            if (continueOrderId > 0) {
                updatemode = true;
                tab_control.SelectedTab = tabPage1;
                SessionData.SetNewOrderId(continueOrderId);
                reOpenOrder();
                ShowCartTotal();
                btn_dine_in.Visible = false;
                btn_save.Visible = true;
                btn_take_away.Enabled = false;
                btn_pay.Enabled = true;
                btn_logout.Enabled = true;
                panel_menue.Hide();
            }

            

        }

        private void button1_Click(object sender, EventArgs e)
        {
            loadOrderDetails();
        }

        private void label22_Click(object sender, EventArgs e)
        {

        }

        private void label19_Click(object sender, EventArgs e)
        {

        }

        private void label18_Click(object sender, EventArgs e)
        {

        }

       

      
    }
}
