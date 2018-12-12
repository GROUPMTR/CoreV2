namespace CoreV2.ADMIN
{
    partial class KULLANICI_TANIMLA
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(KULLANICI_TANIMLA));
            this.txt_MAIL = new DevExpress.XtraEditors.TextEdit();
            this.CMB_FIRMA = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.BTN_KAPAT = new DevExpress.XtraBars.BarButtonItem();
            this.BTN_TAMAM = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.txt_MAIL.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // txt_MAIL
            // 
            this.txt_MAIL.Location = new System.Drawing.Point(71, 95);
            this.txt_MAIL.Name = "txt_MAIL";
            this.txt_MAIL.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.txt_MAIL.Properties.Appearance.Options.UseFont = true;
            this.txt_MAIL.Properties.Mask.EditMask = "([a-zA-Z0-9_\\-\\.]+)@((\\[[0-9]{1,3}\\.[0-9]{1,3}\\.[0-9]{1,3}\\.)|(([a-zA-Z0-9\\-]+\\.)" +
    "+))([a-zA-Z]{2,4}|[0-9]{1,3})(\\]?)";
            this.txt_MAIL.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.RegEx;
            this.txt_MAIL.Size = new System.Drawing.Size(356, 26);
            this.txt_MAIL.TabIndex = 183;
            // 
            // CMB_FIRMA
            // 
            this.CMB_FIRMA.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Bold);
            this.CMB_FIRMA.FormattingEnabled = true;
            this.CMB_FIRMA.Location = new System.Drawing.Point(71, 63);
            this.CMB_FIRMA.Name = "CMB_FIRMA";
            this.CMB_FIRMA.Size = new System.Drawing.Size(128, 27);
            this.CMB_FIRMA.TabIndex = 182;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(13, 98);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(41, 20);
            this.label5.TabIndex = 181;
            this.label5.Text = "Mail";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(12, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(54, 20);
            this.label4.TabIndex = 180;
            this.label4.Text = "Firma";
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.BTN_KAPAT,
            this.BTN_TAMAM});
            this.barManager1.MaxItemId = 6;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BTN_KAPAT, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BTN_TAMAM, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // BTN_KAPAT
            // 
            this.BTN_KAPAT.Caption = "Kapat";
            this.BTN_KAPAT.Id = 0;
            this.BTN_KAPAT.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BTN_KAPAT.ImageOptions.Image")));
            this.BTN_KAPAT.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BTN_KAPAT.ImageOptions.LargeImage")));
            this.BTN_KAPAT.Name = "BTN_KAPAT";
            this.BTN_KAPAT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BTN_KAPAT_ItemClick);
            // 
            // BTN_TAMAM
            // 
            this.BTN_TAMAM.Caption = "Kaydet";
            this.BTN_TAMAM.Id = 5;
            this.BTN_TAMAM.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BTN_TAMAM.ImageOptions.Image")));
            this.BTN_TAMAM.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BTN_TAMAM.ImageOptions.LargeImage")));
            this.BTN_TAMAM.Name = "BTN_TAMAM";
            this.BTN_TAMAM.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BTN_TAMAM_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManager1;
            this.barDockControlTop.Size = new System.Drawing.Size(448, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 133);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(448, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 102);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(448, 31);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 102);
            // 
            // KULLANICI_TANIMLA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(448, 156);
            this.Controls.Add(this.txt_MAIL);
            this.Controls.Add(this.CMB_FIRMA);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "KULLANICI_TANIMLA";
            this.Text = "KULLANICI_TANIMLA";
            ((System.ComponentModel.ISupportInitialize)(this.txt_MAIL.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public DevExpress.XtraEditors.TextEdit txt_MAIL;
        public System.Windows.Forms.ComboBox CMB_FIRMA;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem BTN_KAPAT;
        private DevExpress.XtraBars.BarButtonItem BTN_TAMAM;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}