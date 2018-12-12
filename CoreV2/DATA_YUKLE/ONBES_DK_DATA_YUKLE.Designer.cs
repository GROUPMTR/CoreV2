namespace CoreV2.DATA_YUKLE
{
    partial class ONBES_DK_DATA_YUKLE
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ONBES_DK_DATA_YUKLE));
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.BR_KAPAT = new DevExpress.XtraBars.BarButtonItem();
            this.BR_FILE_SELECT = new DevExpress.XtraBars.BarEditItem();
            this.re_FILE_SELECT = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.BR_DATE = new DevExpress.XtraBars.BarEditItem();
            this.re_TARIH = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.BR_YUKLE = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.BR_CHANNEL_KONTROL = new DevExpress.XtraBars.BarButtonItem();
            this.xtraTabControls = new DevExpress.XtraTab.XtraTabControl();
            this.spreadsheetControls = new DevExpress.XtraSpreadsheet.SpreadsheetControl();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.re_FILE_SELECT)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.re_TARIH)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.re_TARIH.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControls)).BeginInit();
            this.SuspendLayout();
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
            this.BR_KAPAT,
            this.BR_FILE_SELECT,
            this.BR_YUKLE,
            this.BR_DATE,
            this.BR_CHANNEL_KONTROL});
            this.barManager1.MaxItemId = 5;
            this.barManager1.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.re_FILE_SELECT,
            this.re_TARIH});
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BR_KAPAT, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.BR_FILE_SELECT, "", true, true, true, 245, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(((DevExpress.XtraBars.BarLinkUserDefines)((DevExpress.XtraBars.BarLinkUserDefines.PaintStyle | DevExpress.XtraBars.BarLinkUserDefines.Width))), this.BR_DATE, "", true, true, true, 147, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BR_YUKLE, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            // BR_FILE_SELECT
            // 
            this.BR_FILE_SELECT.Caption = "Dosya Seç";
            this.BR_FILE_SELECT.Edit = this.re_FILE_SELECT;
            this.BR_FILE_SELECT.EditWidth = 206;
            this.BR_FILE_SELECT.Id = 1;
            this.BR_FILE_SELECT.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BR_FILE_SELECT.ImageOptions.Image")));
            this.BR_FILE_SELECT.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BR_FILE_SELECT.ImageOptions.LargeImage")));
            this.BR_FILE_SELECT.Name = "BR_FILE_SELECT";
            // 
            // re_FILE_SELECT
            // 
            this.re_FILE_SELECT.AutoHeight = false;
            this.re_FILE_SELECT.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.re_FILE_SELECT.Name = "re_FILE_SELECT";
            this.re_FILE_SELECT.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.re_FILE_SELECT_ButtonClick);
            // 
            // BR_DATE
            // 
            this.BR_DATE.Caption = "Tarih";
            this.BR_DATE.Edit = this.re_TARIH;
            this.BR_DATE.EditWidth = 121;
            this.BR_DATE.Id = 3;
            this.BR_DATE.Name = "BR_DATE";
            // 
            // re_TARIH
            // 
            this.re_TARIH.AutoHeight = false;
            this.re_TARIH.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.re_TARIH.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.re_TARIH.Name = "re_TARIH";
            // 
            // BR_YUKLE
            // 
            this.BR_YUKLE.Caption = "Yükle";
            this.BR_YUKLE.Id = 2;
            this.BR_YUKLE.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BR_YUKLE.ImageOptions.Image")));
            this.BR_YUKLE.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BR_YUKLE.ImageOptions.LargeImage")));
            this.BR_YUKLE.Name = "BR_YUKLE";
            this.BR_YUKLE.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BR_YUKLE_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(893, 28);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 507);
            this.barDockControlBottom.Manager = this.barManager1;
            this.barDockControlBottom.Size = new System.Drawing.Size(893, 22);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 28);
            this.barDockControlLeft.Manager = this.barManager1;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 479);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(893, 28);
            this.barDockControlRight.Manager = this.barManager1;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 479);
            // 
            // BR_CHANNEL_KONTROL
            // 
            this.BR_CHANNEL_KONTROL.Caption = "barButtonItem1";
            this.BR_CHANNEL_KONTROL.Id = 4;
            this.BR_CHANNEL_KONTROL.Name = "BR_CHANNEL_KONTROL";
            // 
            // xtraTabControls
            // 
            this.xtraTabControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.xtraTabControls.Location = new System.Drawing.Point(0, 28);
            this.xtraTabControls.Name = "xtraTabControls";
            this.xtraTabControls.Size = new System.Drawing.Size(893, 479);
            this.xtraTabControls.TabIndex = 4;
            // 
            // spreadsheetControls
            // 
            this.spreadsheetControls.Location = new System.Drawing.Point(131, 77);
            this.spreadsheetControls.MenuManager = this.barManager1;
            this.spreadsheetControls.Name = "spreadsheetControls";
            this.spreadsheetControls.Options.Import.Csv.Encoding = ((System.Text.Encoding)(resources.GetObject("spreadsheetControls.Options.Import.Csv.Encoding")));
            this.spreadsheetControls.Options.Import.Txt.Encoding = ((System.Text.Encoding)(resources.GetObject("spreadsheetControls.Options.Import.Txt.Encoding")));
            this.spreadsheetControls.Size = new System.Drawing.Size(400, 200);
            this.spreadsheetControls.TabIndex = 0;
            this.spreadsheetControls.Text = "spreadsheetControl1";
            // 
            // ONBES_DK_DATA_YUKLE
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(893, 529);
            this.Controls.Add(this.xtraTabControls);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "ONBES_DK_DATA_YUKLE";
            this.Text = "ONBES_DK_RATING";
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.re_FILE_SELECT)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.re_TARIH.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.re_TARIH)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.xtraTabControls)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem BR_KAPAT;
        private DevExpress.XtraBars.BarEditItem BR_FILE_SELECT;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit re_FILE_SELECT;
        private DevExpress.XtraBars.BarEditItem BR_DATE;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit re_TARIH;
        private DevExpress.XtraBars.BarButtonItem BR_YUKLE;
        private DevExpress.XtraBars.BarButtonItem BR_CHANNEL_KONTROL;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraTab.XtraTabControl xtraTabControls;
        private DevExpress.XtraSpreadsheet.SpreadsheetControl spreadsheetControls;
    }
}