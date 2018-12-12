namespace CoreV2.TEXT
{
    partial class DEGISTIR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DEGISTIR));
            this.barManagers = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.BR_KAPAT = new DevExpress.XtraBars.BarButtonItem();
            this.BR_TAMAM = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.txt_KIRILIM_YENI_KOD = new DevExpress.XtraEditors.TextEdit();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.txt_KIRILIM_ESKI_KOD = new DevExpress.XtraEditors.TextEdit();
            ((System.ComponentModel.ISupportInitialize)(this.barManagers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_KIRILIM_YENI_KOD.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_KIRILIM_ESKI_KOD.Properties)).BeginInit();
            this.SuspendLayout();
            // 
            // barManagers
            // 
            this.barManagers.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1,
            this.bar3});
            this.barManagers.DockControls.Add(this.barDockControlTop);
            this.barManagers.DockControls.Add(this.barDockControlBottom);
            this.barManagers.DockControls.Add(this.barDockControlLeft);
            this.barManagers.DockControls.Add(this.barDockControlRight);
            this.barManagers.Form = this;
            this.barManagers.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.BR_KAPAT,
            this.BR_TAMAM});
            this.barManagers.MaxItemId = 4;
            this.barManagers.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BR_KAPAT, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BR_TAMAM, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.UseWholeRow = true;
            this.bar1.Text = "Tools";
            // 
            // BR_KAPAT
            // 
            this.BR_KAPAT.Caption = "Kapat";
            this.BR_KAPAT.Id = 0;
            this.BR_KAPAT.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BR_KAPAT.ImageOptions.Image")));
            this.BR_KAPAT.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BR_KAPAT.ImageOptions.LargeImage")));
            this.BR_KAPAT.Name = "BR_KAPAT";
            this.BR_KAPAT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BR_KAPAT_ItemClick);
            // 
            // BR_TAMAM
            // 
            this.BR_TAMAM.Caption = "Tamam";
            this.BR_TAMAM.Id = 1;
            this.BR_TAMAM.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BR_TAMAM.ImageOptions.Image")));
            this.BR_TAMAM.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BR_TAMAM.ImageOptions.LargeImage")));
            this.BR_TAMAM.Name = "BR_TAMAM";
            this.BR_TAMAM.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BR_TAMAM_ItemClick);
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
            this.barDockControlTop.Manager = this.barManagers;
            this.barDockControlTop.Size = new System.Drawing.Size(296, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 126);
            this.barDockControlBottom.Manager = this.barManagers;
            this.barDockControlBottom.Size = new System.Drawing.Size(296, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManagers;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 95);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(296, 31);
            this.barDockControlRight.Manager = this.barManagers;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 95);
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl2.Appearance.Options.UseFont = true;
            this.labelControl2.Location = new System.Drawing.Point(12, 90);
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.Size = new System.Drawing.Size(58, 18);
            this.labelControl2.TabIndex = 13;
            this.labelControl2.Text = "Yeni Kod";
            // 
            // txt_KIRILIM_YENI_KOD
            // 
            this.txt_KIRILIM_YENI_KOD.Location = new System.Drawing.Point(81, 81);
            this.txt_KIRILIM_YENI_KOD.Name = "txt_KIRILIM_YENI_KOD";
            this.txt_KIRILIM_YENI_KOD.Size = new System.Drawing.Size(203, 36);
            this.txt_KIRILIM_YENI_KOD.TabIndex = 12;
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(162)));
            this.labelControl1.Appearance.Options.UseFont = true;
            this.labelControl1.Location = new System.Drawing.Point(12, 51);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(54, 18);
            this.labelControl1.TabIndex = 11;
            this.labelControl1.Text = "Eski Kod";
            // 
            // txt_KIRILIM_ESKI_KOD
            // 
            this.txt_KIRILIM_ESKI_KOD.Enabled = false;
            this.txt_KIRILIM_ESKI_KOD.Location = new System.Drawing.Point(81, 41);
            this.txt_KIRILIM_ESKI_KOD.Name = "txt_KIRILIM_ESKI_KOD";
            this.txt_KIRILIM_ESKI_KOD.Size = new System.Drawing.Size(203, 36);
            this.txt_KIRILIM_ESKI_KOD.TabIndex = 10;
            // 
            // DEGISTIR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(296, 149);
            this.Controls.Add(this.labelControl2);
            this.Controls.Add(this.txt_KIRILIM_YENI_KOD);
            this.Controls.Add(this.labelControl1);
            this.Controls.Add(this.txt_KIRILIM_ESKI_KOD);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.LookAndFeel.TouchUIMode = DevExpress.Utils.DefaultBoolean.True;
            this.Name = "DEGISTIR";
            this.Text = "DEGISTIR";
            ((System.ComponentModel.ISupportInitialize)(this.barManagers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_KIRILIM_YENI_KOD.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txt_KIRILIM_ESKI_KOD.Properties)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagers;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem BR_KAPAT;
        private DevExpress.XtraBars.BarButtonItem BR_TAMAM;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        public DevExpress.XtraEditors.TextEdit txt_KIRILIM_YENI_KOD;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        public DevExpress.XtraEditors.TextEdit txt_KIRILIM_ESKI_KOD;
    }
}