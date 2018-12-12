namespace CoreV2.ADMIN
{
    partial class RAPORLAR
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(RAPORLAR));
            this.barManagers = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.BR_KAPAT = new DevExpress.XtraBars.BarButtonItem();
            this.BR_TEMP = new DevExpress.XtraBars.BarButtonItem();
            this.BR_SABITLE = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.BR_SELECT_ROW = new DevExpress.XtraBars.BarStaticItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.gridControl_LISTE = new DevExpress.XtraGrid.GridControl();
            this.gridView_LISTE = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gridColumn7 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn2 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.re_IMAGE = new DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox();
            this.ımageList1 = new System.Windows.Forms.ImageList(this.components);
            this.gridColumn3 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn4 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn6 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn5 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gridColumn8 = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.barManagers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_LISTE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LISTE)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.re_IMAGE)).BeginInit();
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
            this.BR_SABITLE,
            this.BR_SELECT_ROW,
            this.BR_TEMP});
            this.barManagers.MaxItemId = 7;
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
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BR_TEMP, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BR_SABITLE, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
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
            // BR_TEMP
            // 
            this.BR_TEMP.Caption = "Temp";
            this.BR_TEMP.Id = 6;
            this.BR_TEMP.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BR_TEMP.ImageOptions.Image")));
            this.BR_TEMP.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BR_TEMP.ImageOptions.LargeImage")));
            this.BR_TEMP.Name = "BR_TEMP";
            this.BR_TEMP.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BR_TEMP_ItemClick);
            // 
            // BR_SABITLE
            // 
            this.BR_SABITLE.Caption = "Sabitle";
            this.BR_SABITLE.Id = 4;
            this.BR_SABITLE.ImageOptions.Image = ((System.Drawing.Image)(resources.GetObject("BR_SABITLE.ImageOptions.Image")));
            this.BR_SABITLE.ImageOptions.LargeImage = ((System.Drawing.Image)(resources.GetObject("BR_SABITLE.ImageOptions.LargeImage")));
            this.BR_SABITLE.Name = "BR_SABITLE";
            this.BR_SABITLE.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.BR_SABITLE_ItemClick);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.BR_SELECT_ROW)});
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            this.bar3.Text = "Status bar";
            // 
            // BR_SELECT_ROW
            // 
            this.BR_SELECT_ROW.Id = 5;
            this.BR_SELECT_ROW.Name = "BR_SELECT_ROW";
            this.BR_SELECT_ROW.TextAlignment = System.Drawing.StringAlignment.Near;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Manager = this.barManagers;
            this.barDockControlTop.Size = new System.Drawing.Size(1340, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 646);
            this.barDockControlBottom.Manager = this.barManagers;
            this.barDockControlBottom.Size = new System.Drawing.Size(1340, 25);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManagers;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 615);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1340, 31);
            this.barDockControlRight.Manager = this.barManagers;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 615);
            // 
            // gridControl_LISTE
            // 
            this.gridControl_LISTE.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControl_LISTE.Location = new System.Drawing.Point(0, 31);
            this.gridControl_LISTE.MainView = this.gridView_LISTE;
            this.gridControl_LISTE.Name = "gridControl_LISTE";
            this.gridControl_LISTE.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.re_IMAGE});
            this.gridControl_LISTE.Size = new System.Drawing.Size(1340, 615);
            this.gridControl_LISTE.TabIndex = 6;
            this.gridControl_LISTE.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_LISTE});
            this.gridControl_LISTE.Click += new System.EventHandler(this.gridControl_LISTE_Click);
            // 
            // gridView_LISTE
            // 
            this.gridView_LISTE.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gridView_LISTE.ColumnPanelRowHeight = 40;
            this.gridView_LISTE.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gridColumn7,
            this.gridColumn1,
            this.gridColumn2,
            this.gridColumn3,
            this.gridColumn4,
            this.gridColumn6,
            this.gridColumn5,
            this.gridColumn8});
            this.gridView_LISTE.CustomizationFormBounds = new System.Drawing.Rectangle(1093, 542, 216, 323);
            this.gridView_LISTE.GridControl = this.gridControl_LISTE;
            this.gridView_LISTE.Name = "gridView_LISTE";
            this.gridView_LISTE.OptionsView.ShowGroupPanel = false;
            this.gridView_LISTE.OptionsView.ShowIndicator = false;
            // 
            // gridColumn7
            // 
            this.gridColumn7.Caption = "RAPOR KODU";
            this.gridColumn7.FieldName = "RAPOR_KODU";
            this.gridColumn7.Name = "gridColumn7";
            this.gridColumn7.OptionsColumn.AllowEdit = false;
            this.gridColumn7.Visible = true;
            this.gridColumn7.VisibleIndex = 1;
            this.gridColumn7.Width = 128;
            // 
            // gridColumn1
            // 
            this.gridColumn1.AppearanceHeader.Options.UseTextOptions = true;
            this.gridColumn1.AppearanceHeader.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.gridColumn1.AppearanceHeader.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.gridColumn1.Caption = "RAPOR AÇIKLAMASI";
            this.gridColumn1.FieldName = "ACIKLAMA";
            this.gridColumn1.Name = "gridColumn1";
            this.gridColumn1.OptionsColumn.AllowEdit = false;
            this.gridColumn1.Visible = true;
            this.gridColumn1.VisibleIndex = 2;
            this.gridColumn1.Width = 232;
            // 
            // gridColumn2
            // 
            this.gridColumn2.Caption = "Tipi";
            this.gridColumn2.ColumnEdit = this.re_IMAGE;
            this.gridColumn2.FieldName = "DURUMU";
            this.gridColumn2.Name = "gridColumn2";
            this.gridColumn2.OptionsColumn.AllowEdit = false;
            this.gridColumn2.Visible = true;
            this.gridColumn2.VisibleIndex = 0;
            this.gridColumn2.Width = 45;
            // 
            // re_IMAGE
            // 
            this.re_IMAGE.AutoHeight = false;
            this.re_IMAGE.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.re_IMAGE.Items.AddRange(new DevExpress.XtraEditors.Controls.ImageComboBoxItem[] {
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "SABIT", 0),
            new DevExpress.XtraEditors.Controls.ImageComboBoxItem("", "TEMP", 1)});
            this.re_IMAGE.LargeImages = this.ımageList1;
            this.re_IMAGE.Name = "re_IMAGE";
            // 
            // ımageList1
            // 
            this.ımageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.ımageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.ımageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // gridColumn3
            // 
            this.gridColumn3.Caption = "Tarih";
            this.gridColumn3.Name = "gridColumn3";
            this.gridColumn3.OptionsColumn.AllowEdit = false;
            this.gridColumn3.Visible = true;
            this.gridColumn3.VisibleIndex = 4;
            this.gridColumn3.Width = 73;
            // 
            // gridColumn4
            // 
            this.gridColumn4.Caption = "Saat";
            this.gridColumn4.Name = "gridColumn4";
            this.gridColumn4.OptionsColumn.AllowEdit = false;
            this.gridColumn4.Visible = true;
            this.gridColumn4.VisibleIndex = 5;
            this.gridColumn4.Width = 59;
            // 
            // gridColumn6
            // 
            this.gridColumn6.Caption = "Güncelleyen";
            this.gridColumn6.Name = "gridColumn6";
            this.gridColumn6.OptionsColumn.AllowEdit = false;
            this.gridColumn6.Visible = true;
            this.gridColumn6.VisibleIndex = 6;
            this.gridColumn6.Width = 120;
            // 
            // gridColumn5
            // 
            this.gridColumn5.Caption = "Mecra Türleri";
            this.gridColumn5.FieldName = "MECRA_TURLERI";
            this.gridColumn5.Name = "gridColumn5";
            this.gridColumn5.OptionsColumn.AllowEdit = false;
            this.gridColumn5.Visible = true;
            this.gridColumn5.VisibleIndex = 3;
            this.gridColumn5.Width = 345;
            // 
            // gridColumn8
            // 
            this.gridColumn8.Caption = "Tablo Adı";
            this.gridColumn8.FieldName = "TABLO_ADI";
            this.gridColumn8.Name = "gridColumn8";
            this.gridColumn8.Width = 153;
            // 
            // RAPORLAR
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1340, 671);
            this.Controls.Add(this.gridControl_LISTE);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "RAPORLAR";
            this.Text = "RAPORLAR";
            ((System.ComponentModel.ISupportInitialize)(this.barManagers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridControl_LISTE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LISTE)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.re_IMAGE)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagers;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem BR_KAPAT;
        private DevExpress.XtraBars.BarButtonItem BR_SABITLE;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarStaticItem BR_SELECT_ROW;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gridControl_LISTE;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_LISTE;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn7;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn2;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn3;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn4;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn6;
        private DevExpress.XtraBars.BarButtonItem BR_TEMP;
        private DevExpress.XtraEditors.Repository.RepositoryItemImageComboBox re_IMAGE;
        private System.Windows.Forms.ImageList ımageList1;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn5;
        private DevExpress.XtraGrid.Columns.GridColumn gridColumn8;
    }
}