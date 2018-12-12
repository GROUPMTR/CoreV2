namespace CoreV2.FILITRELER
{
    partial class OZEL_FILITRE
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
            this.TXT_METIN = new System.Windows.Forms.TextBox();
            this.RD_ICINDE = new System.Windows.Forms.RadioButton();
            this.RD_BASINDA = new System.Windows.Forms.RadioButton();
            this.RD_SONUNDA = new System.Windows.Forms.RadioButton();
            this.label3 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.BTN_VAZGEC = new DevExpress.XtraEditors.SimpleButton();
            this.BTN_TAMAM = new DevExpress.XtraEditors.SimpleButton();
            this.label2 = new System.Windows.Forms.Label();
            this.TOOGLE_OZEL_GENEL_FILITRE = new DevExpress.XtraEditors.ToggleSwitch();
            this.TOOGLE_DAHIL_HARIC = new DevExpress.XtraEditors.ToggleSwitch();
            ((System.ComponentModel.ISupportInitialize)(this.TOOGLE_OZEL_GENEL_FILITRE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TOOGLE_DAHIL_HARIC.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // TXT_METIN
            // 
            this.TXT_METIN.Location = new System.Drawing.Point(100, 52);
            this.TXT_METIN.Name = "TXT_METIN";
            this.TXT_METIN.Size = new System.Drawing.Size(143, 21);
            this.TXT_METIN.TabIndex = 614;
            this.TXT_METIN.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TXT_METIN_KeyDown);
            this.TXT_METIN.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TXT_METIN_KeyPress);
            // 
            // RD_ICINDE
            // 
            this.RD_ICINDE.AutoSize = true;
            this.RD_ICINDE.Checked = true;
            this.RD_ICINDE.Location = new System.Drawing.Point(134, 29);
            this.RD_ICINDE.Name = "RD_ICINDE";
            this.RD_ICINDE.Size = new System.Drawing.Size(82, 17);
            this.RD_ICINDE.TabIndex = 619;
            this.RD_ICINDE.TabStop = true;
            this.RD_ICINDE.Text = "% İçinde %";
            this.RD_ICINDE.UseVisualStyleBackColor = true;
            // 
            // RD_BASINDA
            // 
            this.RD_BASINDA.AutoSize = true;
            this.RD_BASINDA.Location = new System.Drawing.Point(15, 53);
            this.RD_BASINDA.Name = "RD_BASINDA";
            this.RD_BASINDA.Size = new System.Drawing.Size(76, 17);
            this.RD_BASINDA.TabIndex = 620;
            this.RD_BASINDA.TabStop = true;
            this.RD_BASINDA.Text = "% Başında";
            this.RD_BASINDA.UseVisualStyleBackColor = true;
            // 
            // RD_SONUNDA
            // 
            this.RD_SONUNDA.AutoSize = true;
            this.RD_SONUNDA.Location = new System.Drawing.Point(252, 55);
            this.RD_SONUNDA.Name = "RD_SONUNDA";
            this.RD_SONUNDA.Size = new System.Drawing.Size(81, 17);
            this.RD_SONUNDA.TabIndex = 621;
            this.RD_SONUNDA.TabStop = true;
            this.RD_SONUNDA.Text = "Sonunda %";
            this.RD_SONUNDA.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(15, 82);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(64, 13);
            this.label3.TabIndex = 622;
            this.label3.Text = "Dahil / Hariç";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(15, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 13);
            this.label1.TabIndex = 624;
            this.label1.Text = "Filitre Kriteri";
            // 
            // BTN_VAZGEC
            // 
            this.BTN_VAZGEC.ImageUri.Uri = "Close;Size16x16";
            this.BTN_VAZGEC.Location = new System.Drawing.Point(73, 138);
            this.BTN_VAZGEC.Name = "BTN_VAZGEC";
            this.BTN_VAZGEC.Size = new System.Drawing.Size(99, 32);
            this.BTN_VAZGEC.TabIndex = 623;
            this.BTN_VAZGEC.Text = "Vazgeç";
            this.BTN_VAZGEC.Click += new System.EventHandler(this.BTN_VAZGEC_Click);
            // 
            // BTN_TAMAM
            // 
            this.BTN_TAMAM.ImageUri.Uri = "Apply;Size16x16";
            this.BTN_TAMAM.Location = new System.Drawing.Point(178, 138);
            this.BTN_TAMAM.Name = "BTN_TAMAM";
            this.BTN_TAMAM.Size = new System.Drawing.Size(99, 32);
            this.BTN_TAMAM.TabIndex = 615;
            this.BTN_TAMAM.Text = "TAMAM";
            this.BTN_TAMAM.Click += new System.EventHandler(this.BTN_TAMAM_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(15, 111);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 13);
            this.label2.TabIndex = 626;
            this.label2.Text = "Özel / Genel";
            // 
            // TOOGLE_OZEL_GENEL_FILITRE
            // 
            this.TOOGLE_OZEL_GENEL_FILITRE.Location = new System.Drawing.Point(108, 105);
            this.TOOGLE_OZEL_GENEL_FILITRE.Name = "TOOGLE_OZEL_GENEL_FILITRE";
            this.TOOGLE_OZEL_GENEL_FILITRE.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.TOOGLE_OZEL_GENEL_FILITRE.Properties.Appearance.Options.UseFont = true;
            this.TOOGLE_OZEL_GENEL_FILITRE.Properties.OffText = "Özel Filitre";
            this.TOOGLE_OZEL_GENEL_FILITRE.Properties.OnText = "Genel Filitre";
            this.TOOGLE_OZEL_GENEL_FILITRE.Size = new System.Drawing.Size(135, 23);
            this.TOOGLE_OZEL_GENEL_FILITRE.TabIndex = 627;
            this.TOOGLE_OZEL_GENEL_FILITRE.Toggled += new System.EventHandler(this.TOOGLE_OZEL_GENEL_FILITRE_Toggled);
            // 
            // TOOGLE_DAHIL_HARIC
            // 
            this.TOOGLE_DAHIL_HARIC.EditValue = true;
            this.TOOGLE_DAHIL_HARIC.Location = new System.Drawing.Point(108, 79);
            this.TOOGLE_DAHIL_HARIC.Name = "TOOGLE_DAHIL_HARIC";
            this.TOOGLE_DAHIL_HARIC.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.TOOGLE_DAHIL_HARIC.Properties.Appearance.Options.UseFont = true;
            this.TOOGLE_DAHIL_HARIC.Properties.OffText = "Hariç";
            this.TOOGLE_DAHIL_HARIC.Properties.OnText = "Dahil";
            this.TOOGLE_DAHIL_HARIC.Size = new System.Drawing.Size(135, 23);
            this.TOOGLE_DAHIL_HARIC.TabIndex = 628;
            this.TOOGLE_DAHIL_HARIC.Toggled += new System.EventHandler(this.TOOGLE_DAHIL_HARIC_Toggled);
            // 
            // OZEL_FILITRE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(339, 184);
            this.Controls.Add(this.TOOGLE_DAHIL_HARIC);
            this.Controls.Add(this.TOOGLE_OZEL_GENEL_FILITRE);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BTN_VAZGEC);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.RD_SONUNDA);
            this.Controls.Add(this.RD_BASINDA);
            this.Controls.Add(this.RD_ICINDE);
            this.Controls.Add(this.BTN_TAMAM);
            this.Controls.Add(this.TXT_METIN);
            this.Name = "OZEL_FILITRE";
            this.Text = "Özel Filitre";
            ((System.ComponentModel.ISupportInitialize)(this.TOOGLE_OZEL_GENEL_FILITRE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TOOGLE_DAHIL_HARIC.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraEditors.SimpleButton BTN_TAMAM;
        private System.Windows.Forms.TextBox TXT_METIN;
        private System.Windows.Forms.RadioButton RD_ICINDE;
        private System.Windows.Forms.RadioButton RD_BASINDA;
        private System.Windows.Forms.RadioButton RD_SONUNDA;
        private System.Windows.Forms.Label label3;
        private DevExpress.XtraEditors.SimpleButton BTN_VAZGEC;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraEditors.ToggleSwitch TOOGLE_OZEL_GENEL_FILITRE;
        public DevExpress.XtraEditors.ToggleSwitch TOOGLE_DAHIL_HARIC;
    }
}