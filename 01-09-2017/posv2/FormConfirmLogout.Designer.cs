namespace posv2
{
    partial class FormConfirmLogout
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.switch_user = new System.Windows.Forms.Button();
            this.btn_doShoutDown = new System.Windows.Forms.Button();
            this.btn_doSleep = new System.Windows.Forms.Button();
            this.btn_xout = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.panel1.Controls.Add(this.pictureBox1);
            this.panel1.Location = new System.Drawing.Point(12, 118);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(387, 110);
            this.panel1.TabIndex = 3;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = global::posv2.Properties.Resources.loading;
            this.pictureBox1.Location = new System.Drawing.Point(201, 11);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(97, 92);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // switch_user
            // 
            this.switch_user.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.switch_user.BackgroundImage = global::posv2.Properties.Resources.change_user_512;
            this.switch_user.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.switch_user.FlatAppearance.BorderSize = 0;
            this.switch_user.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.switch_user.Location = new System.Drawing.Point(274, 12);
            this.switch_user.Name = "switch_user";
            this.switch_user.Size = new System.Drawing.Size(125, 110);
            this.switch_user.TabIndex = 2;
            this.switch_user.UseVisualStyleBackColor = false;
            this.switch_user.Click += new System.EventHandler(this.switch_user_Click);
            // 
            // btn_doShoutDown
            // 
            this.btn_doShoutDown.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_doShoutDown.BackgroundImage = global::posv2.Properties.Resources.power;
            this.btn_doShoutDown.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_doShoutDown.FlatAppearance.BorderSize = 0;
            this.btn_doShoutDown.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_doShoutDown.Location = new System.Drawing.Point(143, 12);
            this.btn_doShoutDown.Name = "btn_doShoutDown";
            this.btn_doShoutDown.Size = new System.Drawing.Size(125, 110);
            this.btn_doShoutDown.TabIndex = 1;
            this.btn_doShoutDown.UseVisualStyleBackColor = false;
            this.btn_doShoutDown.Click += new System.EventHandler(this.btn_doShoutDown_Click);
            // 
            // btn_doSleep
            // 
            this.btn_doSleep.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_doSleep.BackgroundImage = global::posv2.Properties.Resources.lock_xxl;
            this.btn_doSleep.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_doSleep.FlatAppearance.BorderSize = 0;
            this.btn_doSleep.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_doSleep.Location = new System.Drawing.Point(12, 12);
            this.btn_doSleep.Name = "btn_doSleep";
            this.btn_doSleep.Size = new System.Drawing.Size(125, 110);
            this.btn_doSleep.TabIndex = 0;
            this.btn_doSleep.UseVisualStyleBackColor = false;
            this.btn_doSleep.Click += new System.EventHandler(this.btn_doSleep_Click);
            // 
            // btn_xout
            // 
            this.btn_xout.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btn_xout.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.btn_xout.FlatAppearance.BorderSize = 0;
            this.btn_xout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_xout.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_xout.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btn_xout.Location = new System.Drawing.Point(405, 12);
            this.btn_xout.Name = "btn_xout";
            this.btn_xout.Size = new System.Drawing.Size(125, 110);
            this.btn_xout.TabIndex = 4;
            this.btn_xout.Text = "X Out";
            this.btn_xout.UseVisualStyleBackColor = false;
            this.btn_xout.Click += new System.EventHandler(this.btn_xout_Click);
            // 
            // FormConfirmLogout
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.ClientSize = new System.Drawing.Size(537, 132);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.btn_doSleep);
            this.Controls.Add(this.btn_doShoutDown);
            this.Controls.Add(this.switch_user);
            this.Controls.Add(this.btn_xout);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "FormConfirmLogout";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormConfirmLogout";
            this.Load += new System.EventHandler(this.FormConfirmLogout_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btn_doSleep;
        private System.Windows.Forms.Button btn_doShoutDown;
        private System.Windows.Forms.Button switch_user;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Button btn_xout;
    }
}