namespace CoreV2.FILITRELER
{
    partial class GENEL_FILITRELER
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(GENEL_FILITRELER));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.barManagers = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.BR_KAPAT = new DevExpress.XtraBars.BarButtonItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.grdCntrl_LIST = new DevExpress.XtraGrid.GridControl();
            this.contextMenus = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MN_TUMUNU_SEC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.MN_EKLE = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView_LIST = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label2 = new System.Windows.Forms.Label();
            this.TOGGLE_BASLIK_FILITRE = new DevExpress.XtraEditors.ToggleSwitch();
            this.TOOGLE_OZEL_GENEL_FILITRE = new DevExpress.XtraEditors.ToggleSwitch();
            this.TOOGLE_DAHIL_HARIC = new DevExpress.XtraEditors.ToggleSwitch();
            this.label1 = new System.Windows.Forms.Label();
            this.grdCntrl_SELECT = new DevExpress.XtraGrid.GridControl();
            this.contextMenus_TARGET = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.MN_TRG_TUMUNU_SEC = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.MN_SIL = new System.Windows.Forms.ToolStripMenuItem();
            this.gridView_SELECT = new DevExpress.XtraGrid.Views.Grid.GridView();
            ((System.ComponentModel.ISupportInitialize)(this.barManagers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.grdCntrl_LIST)).BeginInit();
            this.contextMenus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LIST)).BeginInit();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TOGGLE_BASLIK_FILITRE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TOOGLE_OZEL_GENEL_FILITRE.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TOOGLE_DAHIL_HARIC.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCntrl_SELECT)).BeginInit();
            this.contextMenus_TARGET.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gridView_SELECT)).BeginInit();
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
            this.BR_KAPAT});
            this.barManagers.MaxItemId = 1;
            this.barManagers.StatusBar = this.bar3;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(DevExpress.XtraBars.BarLinkUserDefines.PaintStyle, this.BR_KAPAT, DevExpress.XtraBars.BarItemPaintStyle.CaptionGlyph)});
            this.bar1.OptionsBar.AllowCollapse = true;
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
            this.barDockControlTop.Size = new System.Drawing.Size(1093, 31);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 608);
            this.barDockControlBottom.Manager = this.barManagers;
            this.barDockControlBottom.Size = new System.Drawing.Size(1093, 23);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 31);
            this.barDockControlLeft.Manager = this.barManagers;
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 577);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(1093, 31);
            this.barDockControlRight.Manager = this.barManagers;
            this.barDockControlRight.Size = new System.Drawing.Size(0, 577);
            // 
            // splitContainer1
            // 
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.Location = new System.Drawing.Point(0, 31);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.grdCntrl_LIST);
            this.splitContainer1.Panel1.Controls.Add(this.groupBox1);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.grdCntrl_SELECT);
            this.splitContainer1.Size = new System.Drawing.Size(1093, 577);
            this.splitContainer1.SplitterDistance = 559;
            this.splitContainer1.TabIndex = 7;
            // 
            // grdCntrl_LIST
            // 
            this.grdCntrl_LIST.ContextMenuStrip = this.contextMenus;
            this.grdCntrl_LIST.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCntrl_LIST.Location = new System.Drawing.Point(0, 71);
            this.grdCntrl_LIST.MainView = this.gridView_LIST;
            this.grdCntrl_LIST.Name = "grdCntrl_LIST";
            this.grdCntrl_LIST.Size = new System.Drawing.Size(559, 506);
            this.grdCntrl_LIST.TabIndex = 571;
            this.grdCntrl_LIST.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_LIST});
            this.grdCntrl_LIST.DoubleClick += new System.EventHandler(this.grdCntrl_LIST_DoubleClick);
            // 
            // contextMenus
            // 
            this.contextMenus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MN_TUMUNU_SEC,
            this.toolStripSeparator1,
            this.MN_EKLE});
            this.contextMenus.Name = "contextMenus";
            this.contextMenus.Size = new System.Drawing.Size(142, 54);
            // 
            // MN_TUMUNU_SEC
            // 
            this.MN_TUMUNU_SEC.Name = "MN_TUMUNU_SEC";
            this.MN_TUMUNU_SEC.Size = new System.Drawing.Size(141, 22);
            this.MN_TUMUNU_SEC.Text = "Tümünü Seç";
            this.MN_TUMUNU_SEC.Click += new System.EventHandler(this.MN_TUMUNU_SEC_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(138, 6);
            // 
            // MN_EKLE
            // 
            this.MN_EKLE.Name = "MN_EKLE";
            this.MN_EKLE.Size = new System.Drawing.Size(141, 22);
            this.MN_EKLE.Text = "Ekle";
            this.MN_EKLE.Click += new System.EventHandler(this.MN_EKLE_Click);
            // 
            // gridView_LIST
            // 
            this.gridView_LIST.ColumnPanelRowHeight = 40;
            this.gridView_LIST.GridControl = this.grdCntrl_LIST;
            this.gridView_LIST.Name = "gridView_LIST";
            this.gridView_LIST.OptionsBehavior.Editable = false;
            this.gridView_LIST.OptionsSelection.MultiSelect = true;
            this.gridView_LIST.OptionsView.ShowGroupPanel = false;
            this.gridView_LIST.OptionsView.ShowIndicator = false;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.TOGGLE_BASLIK_FILITRE);
            this.groupBox1.Controls.Add(this.TOOGLE_OZEL_GENEL_FILITRE);
            this.groupBox1.Controls.Add(this.TOOGLE_DAHIL_HARIC);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox1.Location = new System.Drawing.Point(0, 0);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(559, 71);
            this.groupBox1.TabIndex = 573;
            this.groupBox1.TabStop = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(30, 17);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(62, 13);
            this.label2.TabIndex = 614;
            this.label2.Text = "Filitreli Liste";
            // 
            // TOGGLE_BASLIK_FILITRE
            // 
            this.TOGGLE_BASLIK_FILITRE.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.TOGGLE_BASLIK_FILITRE.EditValue = true;
            this.TOGGLE_BASLIK_FILITRE.Location = new System.Drawing.Point(184, 12);
            this.TOGGLE_BASLIK_FILITRE.MenuManager = this.barManagers;
            this.TOGGLE_BASLIK_FILITRE.Name = "TOGGLE_BASLIK_FILITRE";
            this.TOGGLE_BASLIK_FILITRE.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TOGGLE_BASLIK_FILITRE.Properties.OffText = "Tüm Liste";
            this.TOGGLE_BASLIK_FILITRE.Properties.OnText = "Filitreli Liste";
            this.TOGGLE_BASLIK_FILITRE.Size = new System.Drawing.Size(161, 24);
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Text = "Filitre Ekle / Çıkart";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "Filitre aktif olduğunda sadece bu alana ait filitreler içinden seçim yapabilirsin" +
    "iz. \r\n\r\nFarklı bir seçenek ilave etmek istediğinizde bu filitreyi off konumuna g" +
    "etirmeniz gerekmektedir.";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.TOGGLE_BASLIK_FILITRE.SuperTip = superToolTip1;
            this.TOGGLE_BASLIK_FILITRE.TabIndex = 613;
            this.TOGGLE_BASLIK_FILITRE.Toggled += new System.EventHandler(this.TOGGLE_BASLIK_FILITRE_Toggled);
            // 
            // TOOGLE_OZEL_GENEL_FILITRE
            // 
            this.TOOGLE_OZEL_GENEL_FILITRE.Location = new System.Drawing.Point(379, 42);
            this.TOOGLE_OZEL_GENEL_FILITRE.MenuManager = this.barManagers;
            this.TOOGLE_OZEL_GENEL_FILITRE.Name = "TOOGLE_OZEL_GENEL_FILITRE";
            this.TOOGLE_OZEL_GENEL_FILITRE.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.TOOGLE_OZEL_GENEL_FILITRE.Properties.Appearance.Options.UseFont = true;
            this.TOOGLE_OZEL_GENEL_FILITRE.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TOOGLE_OZEL_GENEL_FILITRE.Properties.OffText = "Özel Filitre";
            this.TOOGLE_OZEL_GENEL_FILITRE.Properties.OnText = "Genel Filitre";
            this.TOOGLE_OZEL_GENEL_FILITRE.Size = new System.Drawing.Size(136, 23);
            this.TOOGLE_OZEL_GENEL_FILITRE.TabIndex = 612;
            // 
            // TOOGLE_DAHIL_HARIC
            // 
            this.TOOGLE_DAHIL_HARIC.Location = new System.Drawing.Point(184, 42);
            this.TOOGLE_DAHIL_HARIC.MenuManager = this.barManagers;
            this.TOOGLE_DAHIL_HARIC.Name = "TOOGLE_DAHIL_HARIC";
            this.TOOGLE_DAHIL_HARIC.Properties.Appearance.Font = new System.Drawing.Font("Tahoma", 8F);
            this.TOOGLE_DAHIL_HARIC.Properties.Appearance.Options.UseFont = true;
            this.TOOGLE_DAHIL_HARIC.Properties.GlyphAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.TOOGLE_DAHIL_HARIC.Properties.OffText = "Dahil";
            this.TOOGLE_DAHIL_HARIC.Properties.OnText = "Hariç";
            this.TOOGLE_DAHIL_HARIC.Size = new System.Drawing.Size(161, 23);
            this.TOOGLE_DAHIL_HARIC.TabIndex = 611;
            this.TOOGLE_DAHIL_HARIC.Toggled += new System.EventHandler(this.TOOGLE_GAZETE_Toggled);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(30, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(129, 13);
            this.label1.TabIndex = 574;
            this.label1.Text = "Seçilen Satırlar Raporuma";
            // 
            // grdCntrl_SELECT
            // 
            this.grdCntrl_SELECT.ContextMenuStrip = this.contextMenus_TARGET;
            this.grdCntrl_SELECT.Cursor = System.Windows.Forms.Cursors.Default;
            this.grdCntrl_SELECT.Dock = System.Windows.Forms.DockStyle.Fill;
            this.grdCntrl_SELECT.Location = new System.Drawing.Point(0, 0);
            this.grdCntrl_SELECT.MainView = this.gridView_SELECT;
            this.grdCntrl_SELECT.Name = "grdCntrl_SELECT";
            this.grdCntrl_SELECT.Size = new System.Drawing.Size(530, 577);
            this.grdCntrl_SELECT.TabIndex = 572;
            this.grdCntrl_SELECT.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gridView_SELECT});
            this.grdCntrl_SELECT.DoubleClick += new System.EventHandler(this.grdCntrl_SELECT_DoubleClick);
            // 
            // contextMenus_TARGET
            // 
            this.contextMenus_TARGET.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.MN_TRG_TUMUNU_SEC,
            this.toolStripSeparator2,
            this.MN_SIL});
            this.contextMenus_TARGET.Name = "contextMenus";
            this.contextMenus_TARGET.Size = new System.Drawing.Size(142, 54);
            // 
            // MN_TRG_TUMUNU_SEC
            // 
            this.MN_TRG_TUMUNU_SEC.Name = "MN_TRG_TUMUNU_SEC";
            this.MN_TRG_TUMUNU_SEC.Size = new System.Drawing.Size(141, 22);
            this.MN_TRG_TUMUNU_SEC.Text = "Tümünü Seç";
            this.MN_TRG_TUMUNU_SEC.Click += new System.EventHandler(this.MN_TRG_TUMUNU_SEC_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(138, 6);
            // 
            // MN_SIL
            // 
            this.MN_SIL.Name = "MN_SIL";
            this.MN_SIL.Size = new System.Drawing.Size(141, 22);
            this.MN_SIL.Text = "Sil";
            this.MN_SIL.Click += new System.EventHandler(this.MN_SIL_Click);
            // 
            // gridView_SELECT
            // 
            this.gridView_SELECT.ColumnPanelRowHeight = 40;
            this.gridView_SELECT.GridControl = this.grdCntrl_SELECT;
            this.gridView_SELECT.Name = "gridView_SELECT";
            this.gridView_SELECT.OptionsBehavior.Editable = false;
            this.gridView_SELECT.OptionsSelection.MultiSelect = true;
            this.gridView_SELECT.OptionsView.ShowGroupPanel = false;
            this.gridView_SELECT.OptionsView.ShowIndicator = false;
            // 
            // GENEL_FILITRELER
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1093, 631);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "GENEL_FILITRELER";
            this.Text = "GENEL_FILITRELER";
            ((System.ComponentModel.ISupportInitialize)(this.barManagers)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.grdCntrl_LIST)).EndInit();
            this.contextMenus.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView_LIST)).EndInit();
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.TOGGLE_BASLIK_FILITRE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TOOGLE_OZEL_GENEL_FILITRE.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TOOGLE_DAHIL_HARIC.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdCntrl_SELECT)).EndInit();
            this.contextMenus_TARGET.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gridView_SELECT)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevExpress.XtraBars.BarManager barManagers;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem BR_KAPAT;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private DevExpress.XtraGrid.GridControl grdCntrl_LIST;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_LIST;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label1;
        private DevExpress.XtraGrid.GridControl grdCntrl_SELECT;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView_SELECT;
        private System.Windows.Forms.ContextMenuStrip contextMenus;
        private System.Windows.Forms.ToolStripMenuItem MN_TUMUNU_SEC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem MN_EKLE;
        private System.Windows.Forms.ContextMenuStrip contextMenus_TARGET;
        private System.Windows.Forms.ToolStripMenuItem MN_TRG_TUMUNU_SEC;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem MN_SIL;
        public DevExpress.XtraEditors.ToggleSwitch TOOGLE_DAHIL_HARIC;
        public DevExpress.XtraEditors.ToggleSwitch TOOGLE_OZEL_GENEL_FILITRE;
        private System.Windows.Forms.Label label2;
        public DevExpress.XtraEditors.ToggleSwitch TOGGLE_BASLIK_FILITRE;
    }
}