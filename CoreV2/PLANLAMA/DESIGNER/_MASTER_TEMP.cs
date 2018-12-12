using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CoreV2.PLANLAMA.DESIGNER
{
    public partial class _MASTER_TEMP : UserControl
    {        /// <summary>
        /// TARIFE DATA VIEW 
        /// </summary>
        public DataView DW_MASTER; 
        _GLOBAL_PARAMETRELER GLOBAL = new _GLOBAL_PARAMETRELER();
        public string TARIFE_KODU = "", SELECT_SABIT_SECENEKLER = "", GAZETE_TARIFE_TURU = "STANDART_TARIFE", GAZETE_TARIFE_SECENEK = "MECRA";

        DevExpress.XtraGrid.GridControl grdCntrl_ = null;
        DevExpress.XtraGrid.Views.Grid.GridView gridView_ = null;

        DevExpress.XtraGrid.GridControl grdCntrlVER_ = null;
        DevExpress.XtraGrid.Views.Grid.GridView gridViewVER_ = null;

        DataSet ds;

        public _MASTER_TEMP()
        {
            InitializeComponent();
        }
 
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            TARIFELER._TARIFE_LISTESI tmp = new TARIFELER._TARIFE_LISTESI("MASTER", "");
            tmp.ShowDialog();
            if (tmp._BTN_TYPE == "Tamam" && tmp._TARIFE_KODU != null)
            {
                MASTER_TABLE_READ(tmp._TARIFE_KODU, tmp._FILITRE_TEXT, tmp._TARIFE_ID, tmp._BASLIKLAR, tmp._MECRA_TURLERI);
            }
        }

        public void MASTER_TABLE_READ(string _TARIFE_KODU, string _FILITRE_TEXT, string _TARIFE_ID, string _BASLIKLAR, string _MECRA_TURU)
        { 
            try
            { 
                    string TAB_VARMI = "YOK";
                    int TabCount = 0;
                    for (int i = 0; i < xtraTabControl_MASTER_MNG_DETAY.TabPages.Count; i++)
                    {
                        if (xtraTabControl_MASTER_MNG_DETAY.TabPages[i].Name == _TARIFE_KODU)
                        {
                            TAB_VARMI = "VAR"; TabCount = i;
                            break;
                        }
                    } 
                    if (TAB_VARMI == "YOK")
                    {
                        if (_TARIFE_KODU != null)
                        {
                            grdCntrl_ = new DevExpress.XtraGrid.GridControl();
                            gridView_ = new DevExpress.XtraGrid.Views.Grid.GridView();
                            ds = new DataSet();
                            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("SELECT  CAST('' as nvarchar ) NEW, * FROM  dbo.__MAS_EDT_{0}_{1}", _TARIFE_ID, _TARIFE_KODU), conn) };
                                adapter.Fill(ds, "TRF_MASTER");
                                DataViewManager dvManager = new DataViewManager(ds);
                                DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                                grdCntrl_.DataSource = DW_LIST_TARIFE;
                            }
                        }


                        System.Windows.Forms.ToolStripStatusLabel TOOL_FILITRE = new ToolStripStatusLabel();
                        System.Windows.Forms.ToolStripProgressBar TOOL_PROGRESS = new ToolStripProgressBar();
                        System.Windows.Forms.ToolStripStatusLabel TOOL_ID = new ToolStripStatusLabel();
                        System.Windows.Forms.ToolStripStatusLabel TOOL_FILE_NAME = new ToolStripStatusLabel();
                        System.Windows.Forms.ToolStripStatusLabel TOOL_TXT_MECRA_TURU = new ToolStripStatusLabel();


                        TARIFE_KODU = _TARIFE_KODU;
                        TOOL_FILITRE.Text = _FILITRE_TEXT;
                        TOOL_ID.Text = _TARIFE_ID;
                        TOOL_FILE_NAME.Text = _TARIFE_KODU;
                        TOOL_TXT_MECRA_TURU.Text = _MECRA_TURU;

                        System.Windows.Forms.StatusStrip statusStrips = new StatusStrip();
                        statusStrips.Items.Add(TOOL_ID);
                        statusStrips.Items.Add(TOOL_FILITRE);
                        statusStrips.Items.Add(TOOL_FILE_NAME);
                        statusStrips.Items.Add(TOOL_TXT_MECRA_TURU);
                        statusStrips.Items.Add(TOOL_PROGRESS);
                        statusStrips.SendToBack();  
             

                        xtraTabControl_MASTER_MNG_DETAY.TabPages.Add("");
                        xtraTabControl_MASTER_MNG_DETAY.TabPages[xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1].Controls.Add(grdCntrl_);
                        xtraTabControl_MASTER_MNG_DETAY.TabPages[xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1].Name = TOOL_FILE_NAME.Text;
                        xtraTabControl_MASTER_MNG_DETAY.TabPages[xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1].Text = TOOL_FILE_NAME.Text;
                        xtraTabControl_MASTER_MNG_DETAY.TabPages[xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1].Tag = _TARIFE_ID;
                        xtraTabControl_MASTER_MNG_DETAY.TabPages[xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1].ContextMenuStrip = CNMN_TARIFE;
                        // 
                        // grdCntrl_List
                        // 
                        grdCntrl_.Dock = DockStyle.Fill;
                        grdCntrl_.Location = new System.Drawing.Point(0, 0);
                        grdCntrl_.MainView = gridView_;
                        grdCntrl_.ContextMenuStrip = CNMN_TARIFE;
                        grdCntrl_.Name = TARIFE_KODU;
                        grdCntrl_.Tag = _TARIFE_ID;
                        grdCntrl_.Size = new System.Drawing.Size(699, 368);
                        grdCntrl_.TabIndex = 1;
                        grdCntrl_.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView_ });
                        // 
                        // gridView_List
                        // 
                        // gridView_.ColumnPanelRowHeight = 40;
                        gridView_.GridControl = grdCntrl_;
                        gridView_.Name = TARIFE_KODU;
                        gridView_.Tag = _TARIFE_ID;
                        gridView_.OptionsView.ShowGroupPanel = false;
                        //gridView_.OptionsBehavior.Editable = false;
                        gridView_.OptionsView.ColumnAutoWidth = true;
                        gridView_.OptionsSelection.MultiSelect = true;
                        gridView_.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
                        if (_BASLIKLAR != null)
                        {
                            _BASLIKLAR = "NEW;ID;" + _BASLIKLAR;
                            string[] Onesz = _BASLIKLAR.Split(';');
                            for (int i = 0; i < Onesz.Length - 1; i++)
                            {
                                gridView_.Columns[i].Caption = Onesz[i].Trim();
                                gridView_.Columns[i].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                            }
                        }

                        Splitter sp = new Splitter();
                        sp.Dock = DockStyle.Bottom;
                        sp.Height = 7;
                        xtraTabControl_MASTER_MNG_DETAY.TabPages[xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1].Controls.Add(sp);

                        if (_TARIFE_KODU != null)
                        {
                            grdCntrlVER_ = new DevExpress.XtraGrid.GridControl();
                            gridViewVER_ = new DevExpress.XtraGrid.Views.Grid.GridView();
                            DataSet dsVER = new DataSet();
                            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("SELECT  * FROM  dbo.__MAS_EDT_GENEL_VERSION ", _TARIFE_ID, _TARIFE_KODU), conn) };
                                adapter.Fill(dsVER, "TRF_MASTER");
                                DataViewManager dvManager = new DataViewManager(dsVER);
                                DataView DW_LIST_VERSIYON = dvManager.CreateDataView(dsVER.Tables[0]);
                                grdCntrlVER_.DataSource = DW_LIST_VERSIYON;
                            }
                        }
                        xtraTabControl_MASTER_MNG_DETAY.TabPages[xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1].Controls.Add(grdCntrlVER_);
                        // 
                        // grdCntrl_List
                        // 

                        grdCntrlVER_.Dock = DockStyle.Bottom;
                        grdCntrlVER_.Location = new System.Drawing.Point(0, 0);
                        grdCntrlVER_.MainView = gridViewVER_;
                        //  grdCntrlVER_.ContextMenuStrip = CNMN_TARIFE;
                        grdCntrlVER_.Name = TARIFE_KODU;
                        grdCntrlVER_.Tag = _TARIFE_ID;
                        grdCntrlVER_.Size = new System.Drawing.Size(699, 368);
                        grdCntrlVER_.TabIndex = 1;
                        grdCntrlVER_.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridViewVER_ });
                        // 
                        // gridView_List
                        // 
                        // gridView_.ColumnPanelRowHeight = 40;
                        gridViewVER_.GridControl = grdCntrlVER_;
                        gridViewVER_.Name = TARIFE_KODU;
                        gridViewVER_.Tag = _TARIFE_ID;
                        gridViewVER_.OptionsView.ShowGroupPanel = false;
                        //gridView_.OptionsBehavior.Editable = false;
                        gridViewVER_.OptionsView.ColumnAutoWidth = true;
                        gridViewVER_.OptionsSelection.MultiSelect = true;
                        gridViewVER_.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
                        //if (tmp._BASLIKLAR != null)
                        //{
                        //    tmp._BASLIKLAR = "ID;" + tmp._BASLIKLAR;
                        //    string[] Onesz = tmp._BASLIKLAR.Split(';');
                        //    for (int i = 0; i < Onesz.Length - 1; i++)
                        //    {
                        //        gridView_.Columns[i].Caption = Onesz[i].Trim();
                        //        gridView_.Columns[i].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;  
                        //    }
                        //}  

                        //System.Windows.Forms.StatusStrip statusStrips = new StatusStrip();
                        //statusStrips.Items.Add(TOOL_ID);
                        //statusStrips.Items.Add(TOOL_FILITRE);
                        //statusStrips.Items.Add(TOOL_FILE_NAME);
                        //statusStrips.Items.Add(TOOL_PROGRESS);
                        //statusStrips.SendToBack();
                        xtraTabControl_MASTER_MNG_DETAY.TabPages[xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1].Controls.Add(statusStrips);
                        grdCntrlVER_.Height = 120;


                        gridView_.RowStyle += GridView__RowStyle;
                    }
                    else
                    {

                        grdCntrl_ = new DevExpress.XtraGrid.GridControl();
                        var status = xtraTabControl_MASTER_MNG_DETAY.TabPages[TabCount].Controls[1];
                        //System.Windows.Forms.StatusStrip strtp = new StatusStrip(); 
                        //strtp = (System.Windows.Forms.StatusStrip)status; 
                        //strtp.Items[0].Text = tmp._TARIFE_ID;
                        //strtp.Items[1].Text = tmp._FILITRE_TEXT;
                        //strtp.Items[2].Text = tmp._TARIFE_KODU; 
                        //strtp.Items[3].Text = "";
                        var rtb = xtraTabControl_MASTER_MNG_DETAY.TabPages[TabCount].Controls[0];
                        grdCntrl_ = (DevExpress.XtraGrid.GridControl)rtb;
                        gridView_ = (DevExpress.XtraGrid.Views.Grid.GridView)grdCntrl_.DefaultView;
                        DataSet ds = new DataSet();
                        using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("SELECT * FROM     dbo.__MAS_EDT_{0}_{1}", _TARIFE_ID, _TARIFE_KODU), conn) };
                            adapter.Fill(ds, "TRF_MASTER");
                            DataViewManager dvManager = new DataViewManager(ds);
                            DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                            grdCntrl_.DataSource = DW_LIST_TARIFE;
                        }

                        grdCntrlVER_ = new DevExpress.XtraGrid.GridControl();
                        var rtbver = xtraTabControl_MASTER_MNG_DETAY.TabPages[TabCount].Controls[2];
                        grdCntrlVER_ = (DevExpress.XtraGrid.GridControl)rtbver;
                        gridViewVER_ = (DevExpress.XtraGrid.Views.Grid.GridView)grdCntrlVER_.DefaultView;
                        DataSet dsVer = new DataSet();
                        using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {

                            SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("SELECT  * FROM  dbo.__MAS_EDT_GENEL_VERSION ", _TARIFE_ID, _TARIFE_KODU), conn) };
                            adapter.Fill(dsVer, "TRF_MASTER_VER");
                            DataViewManager dvManager = new DataViewManager(dsVer);
                            DataView DW_LIST_VER = dvManager.CreateDataView(dsVer.Tables[0]);
                            grdCntrlVER_.DataSource = DW_LIST_VER;
                        }
                    }



                    if (gridView_.Columns["ID"] != null) gridView_.Columns["ID"].Visible = false;
                    if (gridView_.Columns["GUID"]!=null) gridView_.Columns["GUID"].Visible = false;
                    if (gridView_.Columns["NEW"] != null) gridView_.Columns["NEW"].Visible = false;
                    if (gridView_.Columns["DAHIL_HARIC"] != null) gridView_.Columns["DAHIL_HARIC"].Width = 40;
                    if (gridView_.Columns["ALT"] != null) gridView_.Columns["ALT"].Width = 20;


                    if (gridViewVER_.Columns["ID"] != null) gridViewVER_.Columns["ID"].Visible = false;
                    if (gridViewVER_.Columns["GUID"] != null) gridViewVER_.Columns["GUID"].Visible = false;
                    if (gridViewVER_.Columns["RAPOR_KODU"] != null) gridViewVER_.Columns["RAPOR_KODU"].Visible = false;
                    if (gridViewVER_.Columns["RAPOR_ID"] != null) gridViewVER_.Columns["RAPOR_ID"].Visible = false;
                    if (gridViewVER_.Columns["SIRKET_KODU"] != null) gridViewVER_.Columns["SIRKET_KODU"].Visible = false;
                    if (gridViewVER_.Columns["DAHIL_HARIC"] != null) gridViewVER_.Columns["DAHIL_HARIC"].Width = 40;



              
            }
            catch (Exception EX)
            { MessageBox.Show(EX.Data.ToString()); }

        }
    
        private void GridView__RowStyle(object sender, RowStyleEventArgs e)
        {
            //try
            //{
            //    if (rowhandles.Any(x => x == e.RowHandle))
            //    {
            //        if (color1 != null && color2 != null)
            //        {
            //            e.Appearance.BackColor = color1;
            //            e.Appearance.BackColor2 = color2;
            //        }
            //    }
            //}
            //catch
            //{
            //}


            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                //string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["ALT"]);
                //if (category == "+")
                //{
                //    e.Appearance.BackColor = Color.Yellow;
                //    e.Appearance.BackColor2 = Color.LightYellow;
                //}
            }

            if (e.RowHandle >= 0)
            {
                //string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["DAHIL_HARIC"]);
                //if (category == "Hariç")
                //{
                //    e.Appearance.BackColor = Color.LavenderBlush;
                ////    e.Appearance.BackColor2 = Color.LightCoral;
                //}
            }

        } 


        private void xtraTabControl_MASTER_MNG_DETAY_CloseButtonClick(object sender, EventArgs e)
        { 
            int index = xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex;
            xtraTabControl_MASTER_MNG_DETAY.TabPages.RemoveAt(xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex);
            xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex = index; 
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            //  int index = xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex;
            for (int index = 0; index < xtraTabControl_MASTER_MNG_DETAY.TabPages.Count; index++)
            {
                if (index != -1)
                {
                    var rtb = xtraTabControl_MASTER_MNG_DETAY.TabPages[index].Controls[0];
                    DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
                    System.Data.DataView RW = (System.Data.DataView)grd_.DataSource;
                    SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                    con.Open();
                    if (RW != null)
                    {
                        TARIFELER._GLOBAL_TARIFELER MAS = new TARIFELER._GLOBAL_TARIFELER();
                        // SATIR SİL
                        RW.RowStateFilter = DataViewRowState.Deleted;
                        if (RW.Count != 0)
                        {
                            int Counter = 0;
                            string GUIS = "";
                            for (int i = 0; i <= RW.Count - 1; i++)
                            {
                                Counter++;
                                DataRow DR = RW[i].Row;
                                if (DR["ID", DataRowVersion.Original].ToString() != "") GUIS += "'" + DR["ID", DataRowVersion.Original].ToString() + "',";

                                if (Counter == 1000)
                                {
                                    GUIS = GUIS.Substring(0, GUIS.Length - 1);
                                    SqlCommand Cmd = new SqlCommand() { CommandText = " DELETE [dbo].[__MAS_EDT_" + rtb.Tag.ToString() + "_" + rtb.Name + "]  WHERE ID IN  ( " + GUIS + " )" };
                                    Cmd.CommandTimeout = 0;
                                    Cmd.Connection = con;
                                    Cmd.ExecuteNonQuery();
                                    Counter = 0;
                                    GUIS = "";
                                }
                            }
                            if (GUIS.Length > 0)
                            {
                                GUIS = GUIS.Substring(0, GUIS.Length - 1);
                                SqlCommand smd = new SqlCommand() { CommandText = " DELETE [dbo].[__MAS_EDT_" + rtb.Tag.ToString() + "_" + rtb.Name + "]  WHERE ID IN  ( " + GUIS + " )" };
                                smd.CommandTimeout = 0;
                                smd.Connection = con;
                                smd.ExecuteNonQuery();
                            }
                        }

                        // Yeni eklenmiş Satırları kaydet
                        RW.RowStateFilter = DataViewRowState.Added;
                        if (RW.Count != 0)
                        {
                            for (int i = 0; i <= RW.Count - 1; i++)
                            {
                                DataRow DR = RW[i].Row;
                                MAS.MASTER_ROW_ADD(con, DR, rtb.Name, rtb.Tag.ToString());
                            }
                        }
                        // SATIR GUNCELLE 
                        RW.RowStateFilter = DataViewRowState.ModifiedOriginal;
                        if (RW.Count != 0)
                        {
                            for (int i = 0; i <= RW.Count - 1; i++)
                            {
                                DataRow DR = RW[i].Row;
                                MAS.MASTER_ROW_UPDATE(con, DR, rtb.Name, rtb.Tag.ToString());
                            }
                        }
                        RW.Table.AcceptChanges();
                        RW.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                }
            }
        } 

        private void TARIFE_COPY()
        { 
            int index = xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex;
            var rtb = xtraTabControl_MASTER_MNG_DETAY.TabPages[index].Controls[0];
            DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb; 
            System.Data.DataView RW = (System.Data.DataView)grd_.DataSource;
            DevExpress.XtraGrid.Views.Grid.GridView GR = (DevExpress.XtraGrid.Views.Grid.GridView)grd_.MainView;
            //DevExpress.XtraGrid.GridControlViewCollection  
            //grd_.Views
            GR.CopyToClipboard(); 
        }

        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            TARIFE_COPY();
        }

        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            TARIFE_YAPISTIR();
        }

        private void TARIFE_YAPISTIR()
        {                      
            int index = xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex;
            var rtb = xtraTabControl_MASTER_MNG_DETAY.TabPages[index].Controls[0];
            DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
            System.Data.DataView RW = (System.Data.DataView)grd_.DataSource;
            DevExpress.XtraGrid.Views.Grid.GridView GR = (DevExpress.XtraGrid.Views.Grid.GridView)grd_.MainView;          
            CLASS.DESIGNE_CLASS paste = new CLASS.DESIGNE_CLASS();
            paste.MASTER_YAPISTIR(RW, GR); 
            GR.RefreshData();          
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            SATIR_SIL();
        }

        private void  TUMUNU_SIL()
        {
                int index = xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex;
                var rtb = xtraTabControl_MASTER_MNG_DETAY.TabPages[index].Controls[0];
                DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
                System.Data.DataView RW = (System.Data.DataView)grd_.DataSource;
                DevExpress.XtraGrid.Views.Grid.GridView GR = (DevExpress.XtraGrid.Views.Grid.GridView)grd_.MainView; 
                if (GR.RowCount > 0)
                { 
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    { 
                        SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        con.Open();
                        SqlCommand Cmd = new SqlCommand() { CommandText = " TRUNCATE TABLE [dbo].[__MAS_EDT_" + xtraTabControl_MASTER_MNG_DETAY.SelectedTabPage.Tag.ToString() + "_" + xtraTabControl_MASTER_MNG_DETAY.SelectedTabPage.Name + "]" };
                        Cmd.CommandTimeout = 0;
                        Cmd.Connection = con;
                        Cmd.ExecuteNonQuery();
                   }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); } 

            RW.Table.Rows.Clear();
            RW.Table.AcceptChanges();       
            GR.RefreshData(); 
        }

        private void SATIR_SIL()
        {
            int index = xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex;
            var rtb = xtraTabControl_MASTER_MNG_DETAY.TabPages[index].Controls[0];
            DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
            System.Data.DataView RW = (System.Data.DataView)grd_.DataSource;
            DevExpress.XtraGrid.Views.Grid.GridView GR = (DevExpress.XtraGrid.Views.Grid.GridView)grd_.MainView;
            if (GR.RowCount > 0)
            {
                // int[] GETROW = GR.GetSelectedRows();
                DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                if (c == DialogResult.Yes)
                {
                    DeleteSelectedRows(GR);
                    //GR.ClearSorting();
                    //for (int i = GETROW.Length - 1; i >= 0; i--)
                    //{
                    //    RW.Delete(Convert.ToInt32(GETROW[i]));
                    //}
                }
            }
            else { MessageBox.Show("TARİFE SEÇİNİZ"); }
            GR.RefreshData();
        }

        private void CNT_TUM_TARIFEYI_SIL_Click(object sender, EventArgs e)
        {

            TUMUNU_SIL();

        }

        private void CNT_DAHIL_Click(object sender, EventArgs e)
        {
            int index = xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex;
            var rtb = xtraTabControl_MASTER_MNG_DETAY.TabPages[index].Controls[0];
            DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
            System.Data.DataView RW = (System.Data.DataView)grd_.DataSource;
            DevExpress.XtraGrid.Views.Grid.GridView GR = (DevExpress.XtraGrid.Views.Grid.GridView)grd_.MainView;



            int[] GETROW = GR.GetSelectedRows();
            if (GR.RowCount > 0)
            {
                for (int i = 0; i < GETROW.Length; i++)
                {
                    DataRow dr = GR.GetDataRow(Convert.ToInt32(GETROW[i]));
                    if (GR.Columns["DAHIL_HARIC"] != null) dr["DAHIL_HARIC"] = "Dahil";
                }
            } 
 
         //   GR.EndUpdate();
         //   GR.RefreshData();

        }

        private void CNT_HARIC_Click(object sender, EventArgs e)
        {
            int index = xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex;
            var rtb = xtraTabControl_MASTER_MNG_DETAY.TabPages[index].Controls[0];
            DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
            System.Data.DataView RW = (System.Data.DataView)grd_.DataSource;
            DevExpress.XtraGrid.Views.Grid.GridView GR = (DevExpress.XtraGrid.Views.Grid.GridView)grd_.MainView;
            int[] GETROW = GR.GetSelectedRows();
            if (GR.RowCount > 0)
            {
                
                for (int i = 0; i < GETROW.Length; i++)
                {
                    DataRow dr = GR.GetDataRow(Convert.ToInt32(GETROW[i]));
                    if (GR.Columns["DAHIL_HARIC"] != null) dr["DAHIL_HARIC"] = "Hariç";
                }
            }
 
          //  GR.EndUpdate();
      //      GR.RefreshData();


        }

        private void newToolStripButton_Click(object sender, EventArgs e)
        {
            TARIFELER.MASTER_FREE todo = new TARIFELER.MASTER_FREE();
            //{ Text = string.Format("{0}-{1}", "Free Master", childCount), MdiParent = this };

            todo.StartPosition = FormStartPosition.CenterParent;
            todo.WindowState = System.Windows.Forms.FormWindowState.Maximized;
          
            todo.ShowDialog(); 

        }

        private void DeleteSelectedRows(DevExpress.XtraGrid.Views.Grid.GridView view)
        { 
            if (view == null || view.SelectedRowsCount == 0) return;  
            DataRow[] rows = new DataRow[view.SelectedRowsCount]; 
            for (int i = 0; i < view.SelectedRowsCount; i++) 
                rows[i] = view.GetDataRow(view.GetSelectedRows()[i]);  

            view.BeginSort(); 
            try
            { 
                foreach (DataRow row in rows) 
                    row.Delete(); 
            } 
            finally
            { 
                view.EndSort(); 
            }

        }


        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            int index = xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex;
            if (index != -1)
            {
                xtraTabControl_MASTER_MNG_DETAY.TabPages.RemoveAt(xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex);
                xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex = index;
            }
        }

        private void CNT_YAPISTIR_Click(object sender, EventArgs e)
        {

            TARIFE_YAPISTIR();

            //int index = xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex;
            //var rtb = xtraTabControl_MASTER_MNG_DETAY.TabPages[index].Controls[0];
            //DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
            //System.Data.DataView RW = (System.Data.DataView)grd_.DataSource;
            //DevExpress.XtraGrid.Views.Grid.GridView GR = (DevExpress.XtraGrid.Views.Grid.GridView)grd_.MainView;

            //GridCell[] selectedCells = GR.GetSelectedCells();
            //GR.BeginUpdate();
            //try
            //{
            //    for (int i = 0; (i <= (selectedCells.Length - 1)); i++)
            //    {
            //        if (selectedCells[i].Column.OptionsColumn.AllowEdit)
            //        {
            //            GR.SetRowCellValue(selectedCells[i].RowHandle, selectedCells[i].Column, Clipboard.GetText().ToString());
            //        }
            //    }
            //}
            //finally
            //{
            //    GR.EndUpdate();
            //}
        }

    }
}
