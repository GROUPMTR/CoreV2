using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoreV2.PLANLAMA.DESIGNER
{
    public partial class _KEYWORD_TEMP : UserControl
    {
        public DataView DW_MASTER;
        _GLOBAL_PARAMETRELER GLOBAL = new _GLOBAL_PARAMETRELER();
        public string TARIFE_KODU = "", SELECT_SABIT_SECENEKLER = "", GAZETE_TARIFE_TURU = "STANDART_TARIFE", GAZETE_TARIFE_SECENEK = "MECRA";

          DevExpress.XtraGrid.GridControl grdCntrl_ = null;
          DevExpress.XtraGrid.Views.Grid.GridView gridView_ = null;
          DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit re_WORD_MEMO=null;
          DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit re_FIELD_SELECT=null;
          DevExpress.XtraEditors.Repository.RepositoryItemComboBox re_FIELD_UPDATES=null;
        public _KEYWORD_TEMP()
        {
            InitializeComponent(); 
        } 
        public void SECENEKLER_LIST(string RAPOR_KODU)
        {  
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format("   SELECT ID ,ParentID, TEXT, CHILD_INDEX,GUI,PATH,CHECKS,NAME FROM   dbo.ADM_RAPOR_BASLIK    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}' order by ParentID,CHILD_INDEX", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU);
                SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReader.Read())
                {
                    re_FIELD_SELECT.Items.Add(myReader["TEXT"]);
                    re_FIELD_UPDATES.Items.Add(myReader["TEXT"]);
                } 
            } 
        } 
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            TARIFELER._TARIFE_LISTESI tmp = new TARIFELER._TARIFE_LISTESI("KEYWORD", "KEYWORD");
            tmp.ShowDialog();
            try
            {
                if (tmp._BTN_TYPE == "Tamam" && tmp._TARIFE_KODU != null)
                {
                    TARIFE_KODU = tmp._TARIFE_KODU;
                    string TAB_VARMI = "YOK";
                    int TabCount = 0;
                    for (int i = 0; i < xtraTabControl_WORD_MNG_DETAY.TabPages.Count; i++)
                    {
                        if (xtraTabControl_WORD_MNG_DETAY.TabPages[i].Name == TARIFE_KODU) { TAB_VARMI = "VAR"; TabCount = i; }
                    }
                    // TARIFE_CHANGE(TF._MECRA_TURU); 
                    // this.ParentForm.GetType().InvokeMember("TARIFE_SELECT", System.Reflection.BindingFlags.InvokeMethod, null, this.ParentForm, new object[] { TF._MECRA_TURU });
                    if (TAB_VARMI == "YOK")
                    {
                        if (tmp._TARIFE_KODU != null)
                        {
                            grdCntrl_ = new DevExpress.XtraGrid.GridControl();
                            gridView_ = new DevExpress.XtraGrid.Views.Grid.GridView();  
                            re_FIELD_SELECT = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
                            re_WORD_MEMO = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                            re_FIELD_UPDATES = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
                            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                string SQL = string.Format("   SELECT ID ,ParentID, TEXT, CHILD_INDEX,GUI, CHECKS FROM   dbo.ADM_RAPOR_BASLIK    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}' order by ParentID,CHILD_INDEX", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._RAPOR_KODU);
                                SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                                myConnection.Open();
                                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                                while (myReader.Read())
                                {
                                    re_FIELD_SELECT.Items.Add(myReader["TEXT"]);
                                    re_FIELD_UPDATES.Items.Add(myReader["TEXT"]);
                                }
                            }   
                            DataSet ds = new DataSet();
                            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                SqlDataAdapter adapter = new SqlDataAdapter(string.Format("SELECT ID,[ALANLAR],[KEYWORDS],[ALAN],[DEGER]   FROM  dbo.TRF_KEYWORD  WHERE   TARIFE_KODU='{0}' and SIRKET_KODU='{1}'", tmp._TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA), conn); 
                                adapter.Fill(ds, "TRF_MASTER");
                                DataViewManager dvManager = new DataViewManager(ds);
                                DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                                grdCntrl_.DataSource = DW_LIST_TARIFE;
                                for (int i = 0; i <= 5; i++)
                                { DW_LIST_TARIFE.AddNew(); }
                            }
                        }
                        //ToolStrip mn = new ToolStrip();
                        //mn.Dock = DockStyle.Top;
                        //mn.SendToBack(); 
                        //xtraTabControl_MASTER_MNG_DETAY.TabPages.Add("Yeni");
                        //xtraTabControl_MASTER_MNG_DETAY.TabPages[xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1].Controls.Add(mn); 
                        xtraTabControl_WORD_MNG_DETAY.TabPages.Add("");
                        xtraTabControl_WORD_MNG_DETAY.TabPages[xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1].Controls.Add(grdCntrl_);
                        xtraTabControl_WORD_MNG_DETAY.TabPages[xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1].Name = TARIFE_KODU;
                        xtraTabControl_WORD_MNG_DETAY.TabPages[xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1].Text = TARIFE_KODU;
                        xtraTabControl_WORD_MNG_DETAY.TabPages[xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1].Tag = tmp._TARIFE_ID; 
                        xtraTabControl_WORD_MNG_DETAY.TabPages[xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1].ContextMenuStrip  = CNMN_TARIFE;                        
                        // 
                        // grdCntrl_List
                        // 
                        grdCntrl_.Dock = DockStyle.Fill;
                        grdCntrl_.Location = new System.Drawing.Point(0, 0);
                        grdCntrl_.MainView = gridView_;
                        //grdCntrl_.MenuManager = this.barManagers;
                        grdCntrl_.ContextMenuStrip = CNMN_TARIFE;
                        grdCntrl_.Name = TARIFE_KODU;//_FIELD_NAME;
                        grdCntrl_.Size = new System.Drawing.Size(699, 368);
                        grdCntrl_.TabIndex = 1;
                        grdCntrl_.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView_ });
                        grdCntrl_.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
                         re_WORD_MEMO,
                         re_FIELD_SELECT,
                         re_FIELD_UPDATES});

                        // 
                        // gridView_List
                        // 
                        gridView_.ColumnPanelRowHeight = 40;
                        gridView_.GridControl = grdCntrl_;
                        gridView_.Name = TARIFE_KODU;///_FIELD_NAME; 
                        gridView_.OptionsView.ShowGroupPanel = false;
                        //gridView_.OptionsBehavior.Editable = false;
                        gridView_.OptionsView.ColumnAutoWidth = true;
                        gridView_.OptionsSelection.MultiSelect = true;
                        gridView_.RowHeight = 100; 
                        gridView_.Columns["ALANLAR"].ColumnEdit = re_FIELD_SELECT;
                        gridView_.Columns["ALAN"].ColumnEdit = re_FIELD_UPDATES;
                        gridView_.Columns["KEYWORDS"].ColumnEdit = re_WORD_MEMO;
                        gridView_.Columns["ID"].Visible = false;
                        SECENEKLER_LIST(_GLOBAL_PARAMETRELER._RAPOR_KODU); 
                    }
                    else
                    {
                        var rtb = xtraTabControl_WORD_MNG_DETAY.TabPages[TabCount].Controls[0];
                        DataSet ds = new DataSet();
                        using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            SqlDataAdapter adapter = new SqlDataAdapter("SELECT   ID,[ALANLAR]  ,[KEYWORDS]  ,[ALAN]  ,[DEGER]  FROM  dbo.TRF_KEYWORD  WHERE   TARIFE_KODU='" + tmp._TARIFE_KODU + "' and SIRKET_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "'", conn);  
                            adapter.Fill(ds, "TRF_MASTER");
                            DataViewManager dvManager = new DataViewManager(ds);
                            DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                            DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
                            grd_.DataSource = DW_LIST_TARIFE;
                            for (int i = 0; i <= 5; i++)
                            { DW_LIST_TARIFE.AddNew(); }
                            gridView_.Columns["ID"].Visible = false;
                        }
                    }  
                }
            }
            catch (Exception EX)
            { MessageBox.Show(EX.Data.ToString()); } 
        } 
        private void CNT_TUM_TARIFEYI_SIL_Click(object sender, EventArgs e)
        {

        }

        private void BTN_NEW_Click(object sender, EventArgs e)
        {
            TARIFELER.MASTER_KEYWORD todo = new TARIFELER.MASTER_KEYWORD();
            //{ Text = string.Format("{0}-{1}", "Free Master", childCount), MdiParent = this };

            todo.StartPosition = FormStartPosition.CenterParent;
            todo.WindowState = System.Windows.Forms.FormWindowState.Maximized;

            todo.ShowDialog();
        }

        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            int index = xtraTabControl_WORD_MNG_DETAY.SelectedTabPageIndex;
            if (index != -1)
            {
                var rtb = xtraTabControl_WORD_MNG_DETAY.TabPages[index].Controls[0];
                DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
                System.Data.DataView RW = (System.Data.DataView)grd_.DataSource;
                SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                con.Open();
                if (RW != null)
                {
                    TARIFELER._GLOBAL_TARIFELER WORD = new TARIFELER._GLOBAL_TARIFELER();
                    // SATIR SİL
                    RW.RowStateFilter = DataViewRowState.Deleted;
                    if (RW.Count != 0)
                    {
                        for (int i = 0; i <= RW.Count - 1; i++)
                        {
                            DataRow DR = RW[i].Row;
                            WORD.KEYWORD_ROW_DELETE(con, DR, xtraTabControl_WORD_MNG_DETAY.SelectedTabPage.Name, xtraTabControl_WORD_MNG_DETAY.SelectedTabPage.Tag.ToString());
                        }
                    }

                    // Yeni eklenmiş Satırları kaydet
                    RW.RowStateFilter = DataViewRowState.Added;
                    if (RW.Count != 0)
                    {
                        for (int i = 0; i <= RW.Count - 1; i++)
                        {
                            DataRow DR = RW[i].Row;
                            WORD.KEYWORD_ROW_ADD(con, DR, xtraTabControl_WORD_MNG_DETAY.SelectedTabPage.Name, xtraTabControl_WORD_MNG_DETAY.SelectedTabPage.Tag.ToString());
                        }
                    }
                    // SATIR GUNCELLE 
                    RW.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    if (RW.Count != 0)
                    {
                        for (int i = 0; i <= RW.Count - 1; i++)
                        {
                            DataRow DR = RW[i].Row;
                            if (DR["ID"].ToString() == "")
                            {
                                WORD.KEYWORD_ROW_ADD(con, DR, xtraTabControl_WORD_MNG_DETAY.SelectedTabPage.Name, xtraTabControl_WORD_MNG_DETAY.SelectedTabPage.Tag.ToString());
                            }
                            else
                            {
                                WORD.KEYWORD_ROW_UPDATE(con, DR, xtraTabControl_WORD_MNG_DETAY.SelectedTabPage.Name, xtraTabControl_WORD_MNG_DETAY.SelectedTabPage.Tag.ToString());
                            }
                        }
                    }
                    RW.Table.AcceptChanges();
                    RW.RowStateFilter = DataViewRowState.CurrentRows; 
                }
            }
        }   
        private void xtraTabControl_WORD_MNG_DETAY_CloseButtonClick(object sender, EventArgs e)
        {
           int index = xtraTabControl_WORD_MNG_DETAY.SelectedTabPageIndex;
           if (index != -1)
           {
            xtraTabControl_WORD_MNG_DETAY.TabPages.RemoveAt(xtraTabControl_WORD_MNG_DETAY.SelectedTabPageIndex);
            xtraTabControl_WORD_MNG_DETAY.SelectedTabPageIndex = index;
           }
        }
        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            int index = xtraTabControl_WORD_MNG_DETAY.SelectedTabPageIndex;
            if (index != -1)
            {
                xtraTabControl_WORD_MNG_DETAY.TabPages.RemoveAt(xtraTabControl_WORD_MNG_DETAY.SelectedTabPageIndex);
                xtraTabControl_WORD_MNG_DETAY.SelectedTabPageIndex = index;
            }
        }
    }
}
