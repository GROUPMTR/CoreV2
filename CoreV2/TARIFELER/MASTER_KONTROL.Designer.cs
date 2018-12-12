namespace CoreV2.TARIFELER
{
    partial class MASTER_KONTROL
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
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_TAMAM = new DevExpress.XtraEditors.SimpleButton();
            this.BTN_VAZGEC = new DevExpress.XtraEditors.SimpleButton();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label1.Location = new System.Drawing.Point(35, 25);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(197, 19);
            this.label1.TabIndex = 611;
            this.label1.Text = "Tarifelerimi  Kontrol Et";
            // 
            // BTN_TAMAM
            // 
            this.BTN_TAMAM.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BTN_TAMAM.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BTN_TAMAM.Appearance.Options.UseFont = true;
            this.BTN_TAMAM.Appearance.Options.UseForeColor = true;
            this.BTN_TAMAM.ImageUri.Uri = "Apply;Size16x16;Colored";
            this.BTN_TAMAM.Location = new System.Drawing.Point(134, 59);
            this.BTN_TAMAM.LookAndFeel.SkinName = "VS2010";
            this.BTN_TAMAM.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BTN_TAMAM.Name = "BTN_TAMAM";
            this.BTN_TAMAM.Size = new System.Drawing.Size(85, 33);
            this.BTN_TAMAM.TabIndex = 612;
            this.BTN_TAMAM.Text = "Tamam";
            this.BTN_TAMAM.Click += new System.EventHandler(this.BTN_TAMAM_Click);
            // 
            // BTN_VAZGEC
            // 
            this.BTN_VAZGEC.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.BTN_VAZGEC.Appearance.ForeColor = System.Drawing.Color.Black;
            this.BTN_VAZGEC.Appearance.Options.UseFont = true;
            this.BTN_VAZGEC.Appearance.Options.UseForeColor = true;
            this.BTN_VAZGEC.ImageUri.Uri = "Close;Size16x16;Colored";
            this.BTN_VAZGEC.Location = new System.Drawing.Point(43, 59);
            this.BTN_VAZGEC.LookAndFeel.SkinName = "VS2010";
            this.BTN_VAZGEC.LookAndFeel.UseDefaultLookAndFeel = false;
            this.BTN_VAZGEC.Name = "BTN_VAZGEC";
            this.BTN_VAZGEC.Size = new System.Drawing.Size(85, 33);
            this.BTN_VAZGEC.TabIndex = 616;
            this.BTN_VAZGEC.Text = "Vazgeç";
            this.BTN_VAZGEC.Click += new System.EventHandler(this.BTN_VAZGEC_Click);
            // 
            // MASTER_KONTROL
            // 
            this.Appearance.BackColor = System.Drawing.Color.White;
            this.Appearance.Options.UseBackColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(266, 121);
            this.Controls.Add(this.BTN_VAZGEC);
            this.Controls.Add(this.BTN_TAMAM);
            this.Controls.Add(this.label1);
            this.Name = "MASTER_KONTROL";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Data Kontrol";
            this.TopMost = true;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private DevExpress.XtraEditors.SimpleButton BTN_TAMAM;
        private DevExpress.XtraEditors.SimpleButton BTN_VAZGEC;
    }
}