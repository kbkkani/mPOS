namespace posv2
{
    partial class Form_change
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form_change));
            this.lbl_change = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.btn_change_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lbl_change
            // 
            this.lbl_change.AutoSize = true;
            this.lbl_change.Font = new System.Drawing.Font("Microsoft Sans Serif", 48F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_change.ForeColor = System.Drawing.Color.Lime;
            this.lbl_change.Location = new System.Drawing.Point(109, 89);
            this.lbl_change.Name = "lbl_change";
            this.lbl_change.Size = new System.Drawing.Size(158, 73);
            this.lbl_change.TabIndex = 0;
            this.lbl_change.Text = "0.00";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 36F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(23, 18);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(392, 55);
            this.label1.TabIndex = 1;
            this.label1.Text = "Change        LKR";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // btn_change_ok
            // 
            this.btn_change_ok.BackColor = System.Drawing.Color.Maroon;
            this.btn_change_ok.FlatAppearance.BorderSize = 0;
            this.btn_change_ok.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_change_ok.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_change_ok.Location = new System.Drawing.Point(122, 201);
            this.btn_change_ok.Name = "btn_change_ok";
            this.btn_change_ok.Size = new System.Drawing.Size(179, 52);
            this.btn_change_ok.TabIndex = 2;
            this.btn_change_ok.Text = "DONE";
            this.btn_change_ok.UseVisualStyleBackColor = false;
            this.btn_change_ok.Click += new System.EventHandler(this.btn_change_ok_Click);
            // 
            // Form_change
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.ClientSize = new System.Drawing.Size(433, 265);
            this.Controls.Add(this.btn_change_ok);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lbl_change);
            this.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form_change";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "CHANGE";
            this.Load += new System.EventHandler(this.Form_change_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lbl_change;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn_change_ok;
    }
}