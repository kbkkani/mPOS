namespace posv2
{
    partial class Form_continue_order
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
            this.btn_continue_order = new System.Windows.Forms.Button();
            this.txt_continue_order = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btn_continue_order
            // 
            this.btn_continue_order.BackColor = System.Drawing.Color.CornflowerBlue;
            this.btn_continue_order.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_continue_order.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_continue_order.Location = new System.Drawing.Point(121, 118);
            this.btn_continue_order.Name = "btn_continue_order";
            this.btn_continue_order.Size = new System.Drawing.Size(163, 48);
            this.btn_continue_order.TabIndex = 0;
            this.btn_continue_order.Text = "Continue";
            this.btn_continue_order.UseVisualStyleBackColor = false;
            this.btn_continue_order.Click += new System.EventHandler(this.btn_continue_order_Click);
            // 
            // txt_continue_order
            // 
            this.txt_continue_order.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_continue_order.Location = new System.Drawing.Point(60, 50);
            this.txt_continue_order.Name = "txt_continue_order";
            this.txt_continue_order.Size = new System.Drawing.Size(297, 62);
            this.txt_continue_order.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label1.Location = new System.Drawing.Point(56, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(81, 24);
            this.label1.TabIndex = 2;
            this.label1.Text = "Order ID";
            // 
            // Form_continue_order
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(408, 185);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txt_continue_order);
            this.Controls.Add(this.btn_continue_order);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Form_continue_order";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Continue Order";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btn_continue_order;
        private System.Windows.Forms.TextBox txt_continue_order;
        private System.Windows.Forms.Label label1;
    }
}