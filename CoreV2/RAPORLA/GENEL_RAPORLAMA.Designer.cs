namespace CoreV2.RAPORLA
{
    partial class GENEL_RAPORLAMA
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GENEL_RAPORLAMA));
            this.gridControls = new DevExpress.XtraGrid.GridControl();
            this.gridViews = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.btn_KAPAT = new DevExpress.XtraBars.BarButtonItem();
            this.btn_EXPORT = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.btn_RUN = new DevExpress.XtraBars.BarButtonItem();
            ((System.ComponentModel.ISupportInitialize)(this.gridControls)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViews)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            this.SuspendLayout();
            // 
            // gridControls
            // 
            this.gridControls.Dock = System.Windows.Forms.DockStyle.Fill;
            this.gridControls.Location = new System.Drawing.Point(0, 28);
            this.gridControls.MainView = this.gridViews;
            this.gridControls.Name = "gridControls";
            this.gridControls.Size = new System.Drawing.Size(1305, 750);
            this.gridControls.TabIndex = 0;
            this.gridControls.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridViews});
            // 
            // gridViews
            // 
            this.gridViews.GridControl = this.gridControls;
            this.gridViews.Name = "gridViews";
            this.gridViews.OptionsView.ColumnAutoWidth = false;
            this.gridViews.OptionsView.ShowGroupPanel = false;
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
            this.btn_KAPAT,
            this.btn_RUN,
            this.btn_EXPORT});
            this.barManager1.MaxItemId = 3;
            this.barManager1.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btn_KAPAT, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph),
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.btn_EXPORT, "", true, true, true, 0, null, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.Text = "Tools";
            // 
            // btn_KAPAT
            // 
            this.btn_KAPAT.Caption = "Kapat";
            this.btn_KAPAT.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_KAPAT.Glyph")));
            this.btn_KAPAT.Id = 0;
            this.btn_KAPAT.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_KAPAT.LargeGlyph")));
            this.btn_KAPAT.Name = "btn_KAPAT";
            this.btn_KAPAT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_KAPAT_ItemClick);
            // 
            // btn_EXPORT
            // 
            this.btn_EXPORT.Caption = "Excel";
            this.btn_EXPORT.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_EXPORT.Glyph")));
            this.btn_EXPORT.Id = 2;
            this.btn_EXPORT.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_EXPORT.LargeGlyph")));
            this.btn_EXPORT.Name = "btn_EXPORT";
            this.btn_EXPORT.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.btn_EXPORT_ItemClick);
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
            this.barDockControlTop.Size = new System.Drawing.Size(1305, 28);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 778);
            this.barDockControlBottom.Size = new System.Drawing.Size(1305, 18);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 28);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 750);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1305, 28);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 750);
            // 
            // btn_RUN
            // 
            this.btn_RUN.Caption = "Başla";
            this.btn_RUN.Glyph = ((System.Drawing.Image)(resources.GetObject("btn_RUN.Glyph")));
            this.btn_RUN.Id = 1;
            this.btn_RUN.LargeGlyph = ((System.Drawing.Image)(resources.GetObject("btn_RUN.LargeGlyph")));
            this.btn_RUN.Name = "btn_RUN";
            // 
            // GENEL_RAPORLAMA
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1305, 796);
            this.Controls.Add(this.gridControls);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "GENEL_RAPORLAMA";
            this.Text = "GENEL_RAPORLAMA";
            ((System.ComponentModel.ISupportInitialize)(this.gridControls)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridViews)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraGrid.Views.Grid.GridView gridViews;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem btn_KAPAT;
        private DevExpress.XtraBars.BarButtonItem btn_RUN;
        private DevExpress.XtraBars.BarButtonItem btn_EXPORT;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        public DevExpress.XtraGrid.GridControl gridControls;
    }
}