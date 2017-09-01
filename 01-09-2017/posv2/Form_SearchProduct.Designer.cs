namespace posv2
{
    partial class Form_SearchProduct
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_SearchProduct));
            this.tabControl_searchProduct = new System.Windows.Forms.TabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.btn_search_itemname = new System.Windows.Forms.Button();
            this.comboBox1 = new System.Windows.Forms.ComboBox();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.btn_search_item_code = new System.Windows.Forms.Button();
            this.comboBox2 = new System.Windows.Forms.ComboBox();
            this.tabControl_searchProduct.SuspendLayout();
            this.tabPage1.SuspendLayout();
            this.tabPage2.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl_searchProduct
            // 
            this.tabControl_searchProduct.Appearance = System.Windows.Forms.TabAppearance.FlatButtons;
            this.tabControl_searchProduct.Controls.Add(this.tabPage1);
            this.tabControl_searchProduct.Controls.Add(this.tabPage2);
            this.tabControl_searchProduct.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl_searchProduct.ItemSize = new System.Drawing.Size(72, 60);
            this.tabControl_searchProduct.Location = new System.Drawing.Point(0, 0);
            this.tabControl_searchProduct.Name = "tabControl_searchProduct";
            this.tabControl_searchProduct.SelectedIndex = 0;
            this.tabControl_searchProduct.Size = new System.Drawing.Size(495, 304);
            this.tabControl_searchProduct.TabIndex = 0;
            // 
            // tabPage1
            // 
            this.tabPage1.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tabPage1.Controls.Add(this.btn_search_itemname);
            this.tabPage1.Controls.Add(this.comboBox1);
            this.tabPage1.Location = new System.Drawing.Point(4, 64);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(487, 236);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "ITEM NAME";
            // 
            // btn_search_itemname
            // 
            this.btn_search_itemname.Location = new System.Drawing.Point(24, 125);
            this.btn_search_itemname.Name = "btn_search_itemname";
            this.btn_search_itemname.Size = new System.Drawing.Size(438, 68);
            this.btn_search_itemname.TabIndex = 2;
            this.btn_search_itemname.Text = "DONE";
            this.btn_search_itemname.UseVisualStyleBackColor = true;
            this.btn_search_itemname.Click += new System.EventHandler(this.btn_search_itemname_Click);
            // 
            // comboBox1
            // 
            this.comboBox1.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.comboBox1.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox1.FormattingEnabled = true;
            this.comboBox1.Location = new System.Drawing.Point(24, 28);
            this.comboBox1.Name = "comboBox1";
            this.comboBox1.Size = new System.Drawing.Size(438, 81);
            this.comboBox1.TabIndex = 1;
            // 
            // tabPage2
            // 
            this.tabPage2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.tabPage2.Controls.Add(this.btn_search_item_code);
            this.tabPage2.Controls.Add(this.comboBox2);
            this.tabPage2.Location = new System.Drawing.Point(4, 64);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(487, 236);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "ITEM CODE";
            // 
            // btn_search_item_code
            // 
            this.btn_search_item_code.Location = new System.Drawing.Point(24, 125);
            this.btn_search_item_code.Name = "btn_search_item_code";
            this.btn_search_item_code.Size = new System.Drawing.Size(438, 68);
            this.btn_search_item_code.TabIndex = 2;
            this.btn_search_item_code.Text = "DONE";
            this.btn_search_item_code.UseVisualStyleBackColor = true;
            this.btn_search_item_code.Click += new System.EventHandler(this.btn_search_item_code_Click);
            // 
            // comboBox2
            // 
            this.comboBox2.BackColor = System.Drawing.SystemColors.InactiveBorder;
            this.comboBox2.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.comboBox2.FormattingEnabled = true;
            this.comboBox2.Location = new System.Drawing.Point(24, 28);
            this.comboBox2.Name = "comboBox2";
            this.comboBox2.Size = new System.Drawing.Size(438, 81);
            this.comboBox2.TabIndex = 1;
            // 
            // Form_SearchProduct
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(496, 272);
            this.Controls.Add(this.tabControl_searchProduct);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_SearchProduct";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "SEARCH ITEM";
            this.Load += new System.EventHandler(this.Form_SearchProduct_Load);
            this.tabControl_searchProduct.ResumeLayout(false);
            this.tabPage1.ResumeLayout(false);
            this.tabPage2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabControl_searchProduct;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.ComboBox comboBox1;
        private System.Windows.Forms.TabPage tabPage2;
        private System.Windows.Forms.ComboBox comboBox2;
        private System.Windows.Forms.Button btn_search_itemname;
        private System.Windows.Forms.Button btn_search_item_code;
    }
}