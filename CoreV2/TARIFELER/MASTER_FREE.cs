using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraTreeList;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraTreeList.Nodes;
using System.Collections; 

namespace CoreV2.TARIFELER
{
    public partial class MASTER_FREE : DevExpress.XtraEditors.XtraForm
    {
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManagers;

        DataView DW_LIST_KIRLIMLAR= null,DW_LIST= null,DW_LIST_FILITRE = null,   DW_LIST_ALT=null;
        ArrayList SQL_ARR = new ArrayList();
        string _RAPOR_DURUMU = string.Empty;
        string QUERY;
        string TMP_QUERY_TELEVIZYON, TMP_QUERY_GAZETE, TMP_QUERY_DERGI, TMP_QUERY_SINEMA, TMP_QUERY_RADYO, TMP_QUERY_OUTDOOR, TMP_QUERY_INTERNET,
                   QUERY_TELEVIZYON, QUERY_GAZETE, QUERY_DERGI, QUERY_SINEMA, QUERY_RADYO, QUERY_OUTDOOR, QUERY_INTERNET;

        string DAHIL_HARIC = string.Empty, RAPORA_EKLE = string.Empty, SABITLER_SELECT_NAME = string.Empty;
        string KIRILIM = string.Empty, KIRILIM_CAST = string.Empty, KIRILIM_FIELD = string.Empty, KIRILIM_TABLE_CREATE = string.Empty;
        string OZEL_TANIMLAMA = string.Empty, OZEL_TANIMLAMA_CAST = string.Empty, OZEL_TANIMLAMA_FIELD = string.Empty, OZEL_TABLE_CREATE = string.Empty;
        string BASLIK = string.Empty, BASLIK_CAST = string.Empty, BASLIK_FIELD = string.Empty, BASLIK_TABLE_CREATE = string.Empty;
        string OLCUM = string.Empty, OLCUM_CAST = string.Empty, OLCUM_FIELD = string.Empty, OLCUM_TABLE_CREATE = string.Empty;
        string FILITRE = string.Empty, FILITRE_CAST = string.Empty, FILITRE_FIELD = string.Empty, FILITRE_TABLE_CREATE = string.Empty;
        string TABLE_CREATE_FIELD_NAME = string.Empty, TABLE_CREATE_INSERT_QUERY = string.Empty;
        int _TELEVIZYON = 0, _RADYO = 0, _GAZETE = 0, _DERGI = 0, _SINEMA = 0, _INTERNET = 0, _OUTDOOR = 0;

  

        string FIELD_TELEVIZYON = string.Empty, FIELD_TELEVIZYON_GROUP_BY = string.Empty, FIELD_TELEVIZYON_SUM = string.Empty,
                 FIELD_GAZETE = string.Empty, FIELD_GAZETE_GROUP_BY = string.Empty, FIELD_GAZETE_SUM = string.Empty,
                 FIELD_DERGI = string.Empty, FIELD_DERGI_GROUP_BY = string.Empty, FIELD_DERGI_SUM = string.Empty,
                 FIELD_SINEMA = string.Empty, FIELD_SINEMA_GROUP_BY = string.Empty, FIELD_SINEMA_SUM = string.Empty,
                 FIELD_RADYO = string.Empty, FIELD_RADYO_GROUP_BY = string.Empty, FIELD_RADYO_SUM = string.Empty,
                 FIELD_OUTDOOR = string.Empty, FIELD_OUTDOOR_GROUP_BY = string.Empty, FIELD_OUTDOOR_SUM = string.Empty,
                 FIELD_INTERNET = string.Empty, FIELD_INTERNET_GROUP_BY = string.Empty, FIELD_INTERNET_SUM = string.Empty;

     
        private void treeList_KIRILIMLAR_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
          string  KIRILIM_NODE = e.Node.GetValue("PATH").ToString ();
        }

        string FIELD_SELECT = string.Empty;
        string FIELD_GROUP_BY = string.Empty;
        string FIELD_SUM = string.Empty;
        string ANA_TEXT_TREE = string.Empty;       
        string CAST_FIELD = string.Empty, CAST_KIRILIM = string.Empty, CAST_KIRILIM_TABLE_CREATE = string.Empty;
        string STATIC_NAME = string.Empty;
        string STATIC_KIRLIMLAR = string.Empty;
        string _CHANNEL = string.Empty;  
        string PARENT_NAME = string.Empty;
        int COLUMS_COUNT = 0;
        string BASLIK_BUL = string.Empty;

        string BASLIKLAR = "", BASLIKLAR_GROUPBY="";
        public MASTER_FREE()
        {
            InitializeComponent();  
            SECENEKLER_LIST(_GLOBAL_PARAMETRELER._RAPOR_KODU );
        }


        private void MASTER_FREE_Load(object sender, EventArgs e)
        {

        }

        public void SECENEKLER_LIST(string RAPOR_KODU)
        {

            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet dsKr = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("      SELECT ID ,ParentID, CHILD_INDEX,GUI,CHECKS,TEXT,NAME,TYPE,PATH  FROM   dbo.ADM_RAPOR_KIRILIM    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}' order by ParentID,CHILD_INDEX", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU), conn) };
                adapter.Fill(dsKr, "ADM_RAPOR_KIRILIM");
                DataViewManager dvManager = new DataViewManager(dsKr);
                DW_LIST_KIRLIMLAR = dvManager.CreateDataView(dsKr.Tables[0]);
                treeList_KIRILIMLAR.KeyFieldName = "ID";
                treeList_KIRILIMLAR.ParentFieldName = "ParentID";
                treeList_KIRILIMLAR.DataSource = DW_LIST_KIRLIMLAR;
            }
            treeList_KIRILIMLAR.ExpandAll();

            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet dsb = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("        SELECT ID ,ParentID, CHILD_INDEX,GUI,CHECKS,TEXT FROM   dbo.ADM_RAPOR_BASLIK    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}' order by ParentID,CHILD_INDEX", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU), conn) };
                adapter.Fill(dsb, "ADM_RAPOR_BASLIK");
                DataViewManager dvManager = new DataViewManager(dsb);
                DataView DWRAPOR_BASLIK = dvManager.CreateDataView(dsb.Tables[0]);
                treeList_BASLIKLAR.KeyFieldName = "ID";
                treeList_BASLIKLAR.ParentFieldName = "ParentID"; 
                treeList_BASLIKLAR.DataSource = DWRAPOR_BASLIK;

                 DataRowView newRow = DWRAPOR_BASLIK.AddNew();
                ////newRow["ID"] = KIRILIM_SAY;
                ////newRow["ParentID"] = -1;
                newRow["TEXT"] = "DAHIL_HARIC";
                ////newRow["NAME"] = fr._TEXT;
                ////newRow["PATH"] = fr._PATH;
                ////newRow["TYPE"] = "KIRILIM";
                ////newRow["CHECKS"] = false;
                newRow.EndEdit();
            }
            treeList_BASLIKLAR.ExpandAll(); 
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet dsFil = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("       SELECT ID ,ParentID, CHILD_INDEX,GUI,CHECKS,TEXT,NAME,TYPE,PATH FROM   dbo.ADM_RAPOR_FILITRE    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}' order by ParentID,PATH ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU), conn) };
                adapter.Fill(dsFil, "ADM_RAPOR_FILITRE");
                DataViewManager dvManager = new DataViewManager(dsFil);
                DW_LIST_FILITRE = dvManager.CreateDataView(dsFil.Tables[0]);
                treeList_FILITRELER.KeyFieldName = "ID";
                treeList_FILITRELER.ParentFieldName = "ParentID";
                treeList_FILITRELER.DataSource = DW_LIST_FILITRE;
            }
            treeList_FILITRELER.ExpandAll();
        }
       private void MN_CNT_COPY_Click(object sender, EventArgs e)
        {
            gridView_SABITLER.CopyToClipboard();
        }
        public void SABITLER_VE_OLCUMLER_TABLOSUNU_OKU()
        {  
            for (int i = 0; i < gridView_SABITLER.Columns.Count; i++)
            { 
                using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQL = " SELECT   * FROM   dbo.ADM_SECENEKLER where SECENEK="+gridView_SABITLER.Columns[i].Name ;
                    SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                    myConnection.Open();
                    SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            BASLIKLAR += string.Format("[{0}],", myReader["SECENEK"]);
                        } 
                    } 
                }
            }
        }

        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void MN_CNT_ROW_PASTE_Click(object sender, EventArgs e)
        {
 
            _GLOBAL_TARIFELER paste = new _GLOBAL_TARIFELER();
            paste._YAPISTIR(DW_LIST);
            
        }

        private void MN_CNT_HARIC_Click(object sender, EventArgs e)
        { 
            if (gridView_SABITLER.RowCount > 0)
            {
                int[] GETROW = gridView_SABITLER.GetSelectedRows();
                for (int i = 0; (i <= (GETROW.Length - 1)); i++)
                {
                    if (gridView_SABITLER.Columns["DAHIL_HARIC"] != null)
                    { 
                        DataRow DRW = gridView_SABITLER.GetDataRow(GETROW[i]); 
                        DRW["DAHIL_HARIC"] = "Hariç";  
                    }
                }
            }
            else { MessageBox.Show("Data Bulunamadı"); } 
            gridView_SABITLER.RefreshData(); 
        }

        private void MN_CNT_DAHIL_Click(object sender, EventArgs e)
        { 
            if (gridView_SABITLER.RowCount > 0)
            {
                int[] GETROW = gridView_SABITLER.GetSelectedRows();                
                for (int i = 0; (i <= (GETROW.Length - 1)); i++)
                { 
                    if (gridView_SABITLER.Columns["DAHIL_HARIC"] !=null)
                    {
                        DataRow DRW = gridView_SABITLER.GetDataRow(GETROW[i]);
                        DRW["DAHIL_HARIC"] = "Dahil";
                    }
                }                
            }
            else { MessageBox.Show("Data Bulunamadı"); } 
            gridView_SABITLER.RefreshData(); 
        } 
        private void MN_CNT_VER_HARIC_Click(object sender, EventArgs e)
        {
            if (gridView_FREE_TABLE_ALT.RowCount > 0)
            {
                int[] GETROW = gridView_FREE_TABLE_ALT.GetSelectedRows();
                for (int i = 0; (i <= (GETROW.Length - 1)); i++)
                {
                    if (gridView_FREE_TABLE_ALT.Columns["DAHIL_HARIC"] != null)
                    {
                        DataRow DRW = gridView_FREE_TABLE_ALT.GetDataRow(GETROW[i]);
                        DRW["DAHIL_HARIC"] = "Hariç";
                    }
                }
            }
            else { MessageBox.Show("Data Bulunamadı"); }
            gridView_FREE_TABLE_ALT.RefreshData(); 
        }

        private void MN_CNT_VER_DAHIL_Click(object sender, EventArgs e)
        {
            if (gridView_FREE_TABLE_ALT.RowCount > 0)
            {
                int[] GETROW = gridView_FREE_TABLE_ALT.GetSelectedRows();
                for (int i = 0; (i <= (GETROW.Length - 1)); i++)
                {
                    if (gridView_FREE_TABLE_ALT.Columns["DAHIL_HARIC"] != null)
                    {
                        DataRow DRW = gridView_FREE_TABLE_ALT.GetDataRow(GETROW[i]);
                        DRW["DAHIL_HARIC"] = "Dahil";
                    }
                }
            }
            else { MessageBox.Show("Data Bulunamadı"); }
            gridView_FREE_TABLE_ALT.RefreshData();  
        }

        private void MN_CNT_VERSIYON_Click(object sender, EventArgs e)
        {
            DataRow DR = gridView_SABITLER.GetDataRow(gridView_SABITLER.FocusedRowHandle);

            if (DR.Table.Columns["YAYIN_SINIFI"] == null) { MessageBox.Show("Versiyon çalışmalarında ( YAYIN SINIFI ) ilave etmeniz gerekiyor","Hata Uyarısı", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            string STR_YAYIN_SINIFI = DR["YAYIN_SINIFI"].ToString();
            string VER_QUERY = "";
            for (int i = 0; i < DR.Table.Columns.Count; i++)
            {
                if (DR.Table.Columns[i].ToString() != "GUID" && DR.Table.Columns[i].ToString() != "ALT" && DR.Table.Columns[i].ToString() != "ID" && DR.Table.Columns[i].ToString() != "DAHIL_HARIC")
                {
                    VER_QUERY += DR.Table.Columns[i].ToString() + "='" + DR[DR.Table.Columns[i].ToString()].ToString() + "' and ";
                }
            }
            VER_QUERY = TEMIZLE_ELEMAN(VER_QUERY, 4, "and ").ToString();
            TARIFELER.MASTER_FREE_VERSIYON fr = new TARIFELER.MASTER_FREE_VERSIYON(STR_YAYIN_SINIFI, VER_QUERY);
            fr.ShowDialog();
            if (fr.VERSIYON_LIST != null)
            {
                for (int i = 0; i < fr.VERSIYON_LIST.Count; i++)
                {
                    DataRowView newRow = DW_LIST_ALT.AddNew();
                    newRow["RAPOR_ID"] = _GLOBAL_PARAMETRELER._RAPOR_ID;
                    newRow["RAPOR_KODU"] = _GLOBAL_PARAMETRELER._RAPOR_KODU;
                    newRow["SIRKET_KODU"] = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    newRow["GUID"] = DR["GUID"];
                    newRow["VERSIYON"] = fr.VERSIYON_LIST[i].ToString();
                    newRow.EndEdit();
                }
                DR["ALT"] = "+";
            }
        }
        public string GetFullPath(TreeListNode node, string pathSeparator)
        {
            if (node == null) return "";
            string result = "";
            while (node != null)
            {
                result = pathSeparator + node.GetDisplayText(0) + result;
                node = node.ParentNode;
            }
            return result;
        }
        void selectChildren(TreeListNode parent, ArrayList selectedNodes)
        {
            IEnumerator en = parent.Nodes.GetEnumerator();
            TreeListNode child;
            while (en.MoveNext())
            {
                child = (TreeListNode)en.Current;
                selectedNodes.Add(child);
                if (child.HasChildren) selectChildren(child, selectedNodes);
            }
        }
 
        private string TEMIZLE_ELEMAN(string ALAN, int Deger, string TIP)
        {
            if (ALAN != null)
            {
                if (TIP != "")
                {
                    if (ALAN.Length > Deger)
                    {
                        if (ALAN.Substring(ALAN.Length - Deger, Deger) == TIP)
                        {
                            ALAN = ALAN.Substring(0, ALAN.Length - Deger);
                        }
                    }
                }
                else
                {
                    if (ALAN.Length > Deger)
                    {
                        ALAN = ALAN.Substring(0, ALAN.Length - Deger);
                    }
                } 
            }
            return ALAN;
        } 

        string SQL = "";
        private void ANAKIRILIM_FILITRE_CHEK_KONTROL(string TMP_QUERY)
        {
            string SELECT_FIELDS = "", GROUP_BY_FIELDS = "";
            for (int i = 0; i < gridView_SABITLER.Columns.Count; i++)
            {  using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQLD = string.Format(" SELECT   * FROM   dbo.ADM_SECENEKLER where FIELDS='{0}'", gridView_SABITLER.Columns[i].FieldName);
                    SqlCommand myCommand = new SqlCommand(SQLD, con) { CommandText = SQLD.ToString() };
                    con.Open();
                    SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    if (myReader.HasRows)
                    {
                        while (myReader.Read())
                        {
                            SELECT_FIELDS += string.Format("[{0}],", myReader["FIELDS"]);
                            GROUP_BY_FIELDS += string.Format("[{0}],", myReader["FIELDS"]);
                        }
                    }
                    else
                    {
                        if (gridView_SABITLER.Columns[i].FieldName != "GUID" && gridView_SABITLER.Columns[i].FieldName != "ALT")
                        {
                            SELECT_FIELDS += string.Format(" CAST('' AS nvarchar ) as [{0}],", gridView_SABITLER.Columns[i].FieldName);
                        }
                    }
                }
            }  
            GROUP_BY_FIELDS = TEMIZLE_ELEMAN(GROUP_BY_FIELDS, 1, ",").ToString();
            SELECT_FIELDS = TEMIZLE_ELEMAN(SELECT_FIELDS, 1, ",").ToString();
              
            string MECRA_TURU = "";
            if (TOOGLE_TELEVIZYON.IsOn) MECRA_TURU += " [YAYIN_SINIFI] = 'TELEVIZYON' or "; 
            if (TOOGLE_RADYO.IsOn) MECRA_TURU += " [YAYIN_SINIFI] = 'RADYO' or ";
            if (TOOGLE_DERGI.IsOn) MECRA_TURU += " [YAYIN_SINIFI] = 'DERGI' or ";
            if (TOOGLE_GAZETE.IsOn) MECRA_TURU += " [YAYIN_SINIFI] = 'GAZETE' or ";
            if (TOOGLE_SINEMA.IsOn) MECRA_TURU += " [YAYIN_SINIFI] = 'SINEMA' or ";
            if (TOOGLE_OUTDOOR.IsOn) MECRA_TURU += " [YAYIN_SINIFI] = 'OUTDOOR' or ";
            if (TOOGLE_INTERNET.IsOn) MECRA_TURU += " [YAYIN_SINIFI] = 'INTERNET' or ";

            MECRA_TURU = TEMIZLE_ELEMAN(MECRA_TURU, 3, "or ").ToString();

            SQL = string.Format("select CAST( newid() AS uniqueidentifier ) AS GUID,CAST('' AS nvarchar) AS ALT,{0} from [dbo].[{1}] WHERE {2} group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_TELEVIZYON + MECRA_TURU , GROUP_BY_FIELDS);

            TMP_QUERY_TELEVIZYON = TMP_QUERY_GAZETE = TMP_QUERY_DERGI = TMP_QUERY_SINEMA = TMP_QUERY_RADYO = TMP_QUERY_OUTDOOR = TMP_QUERY_INTERNET = "";
            QUERY_TELEVIZYON = QUERY_GAZETE = QUERY_DERGI = QUERY_SINEMA = QUERY_RADYO = QUERY_OUTDOOR = QUERY_INTERNET = "";
        }

  
        private void BTN_DATA_LISTE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (gridView_SABITLER.Columns["GUID"] == null)
            {
                GridColumn coln = gridView_SABITLER.Columns.AddVisible("GUID", string.Empty);
                coln.Caption = "GUID";
                coln.FieldName = "GUID";
                coln.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                coln.OptionsColumn.AllowEdit = false;
                coln.Visible = false;
                gridView_SABITLER.Columns.Add(coln);
            }

            if (gridView_SABITLER.Columns["ALT"] == null)
            {
                GridColumn coln = gridView_SABITLER.Columns.AddVisible("ALT", string.Empty);
                coln.Caption = "ALT";
                coln.FieldName = "ALT";
                coln.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                coln.OptionsColumn.AllowEdit = false;
                coln.Width = 40;
                coln.OptionsColumn.AllowEdit = false;
                gridView_SABITLER.Columns.Add(coln);
            }


            SQL = "";        
            RAPORU_BASLAT();

            if (SQL != "")
            {
                SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());                
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnection);
                da.SelectCommand.CommandTimeout = 0;
                DataSet ds = new DataSet();
                da.Fill(ds, "stock");
                DataViewManager dvManager = new DataViewManager(ds);
                DW_LIST = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_FREE_TABLE.DataSource = DW_LIST;
            }
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("SELECT * FROM  dbo.__MAS_EDT_GENEL_VERSION WHERE RAPOR_ID='{0}' and  RAPOR_KODU='{1}'  and SIRKET_KODU='{2}'  ", lbID.Caption, lbFILE_NAME.Caption.ToString(), _GLOBAL_PARAMETRELER._SIRKET_KODU), conn) };
                adapter.Fill(ds, "TRF_MASTER");
                DataViewManager dvManager = new DataViewManager(ds);
                DW_LIST_ALT = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_FREE_TABLE_ALT.DataSource = DW_LIST_ALT;
            }

        }
 
        private void RAPORU_BASLAT()
        { 
            ///
            /// RAPOR PARAMETRELERINI TEMİZLE
            ///  

            KIRILIM = KIRILIM_CAST = KIRILIM_FIELD = KIRILIM_TABLE_CREATE =
            OZEL_TANIMLAMA = OZEL_TANIMLAMA_CAST = OZEL_TANIMLAMA_FIELD = OZEL_TABLE_CREATE =
            BASLIK = BASLIK_CAST = BASLIK_FIELD = BASLIK_TABLE_CREATE =
            OLCUM = OLCUM_CAST = OLCUM_FIELD = OLCUM_TABLE_CREATE =
            FILITRE = FILITRE_CAST = FILITRE_FIELD = FILITRE_TABLE_CREATE = string.Empty;

            COLUMS_COUNT = 0;

            OZEL_TANIMLAMA = CAST_KIRILIM_TABLE_CREATE = BASLIKLAR = ANA_TEXT_TREE = TABLE_CREATE_FIELD_NAME = TABLE_CREATE_INSERT_QUERY = string.Empty;

            FIELD_SELECT = FIELD_GROUP_BY = FIELD_SUM =
            FIELD_TELEVIZYON = FIELD_TELEVIZYON_GROUP_BY = FIELD_TELEVIZYON_SUM =
            FIELD_GAZETE = FIELD_GAZETE_GROUP_BY = FIELD_GAZETE_SUM =
            FIELD_DERGI = FIELD_DERGI_GROUP_BY = FIELD_DERGI_SUM =
            FIELD_SINEMA = FIELD_SINEMA_GROUP_BY = FIELD_SINEMA_SUM =
            FIELD_RADYO = FIELD_RADYO_GROUP_BY = FIELD_RADYO_SUM =
            FIELD_OUTDOOR = FIELD_OUTDOOR_GROUP_BY = FIELD_OUTDOOR_SUM =
            FIELD_INTERNET = FIELD_INTERNET_GROUP_BY = FIELD_INTERNET_SUM = string.Empty;

            SABITLER_SELECT_NAME = PARENT_NAME = CAST_KIRILIM = CAST_FIELD = STATIC_NAME = STATIC_KIRLIMLAR = string.Empty; 
            QUERY = string.Empty;
            QUERY_TELEVIZYON = QUERY_GAZETE = QUERY_DERGI = QUERY_SINEMA = QUERY_RADYO = QUERY_OUTDOOR = QUERY_INTERNET = string.Empty;
            TMP_QUERY_TELEVIZYON = TMP_QUERY_GAZETE = TMP_QUERY_DERGI = TMP_QUERY_SINEMA = TMP_QUERY_RADYO = TMP_QUERY_OUTDOOR = TMP_QUERY_INTERNET = string.Empty;
            _CHANNEL = string.Empty;

            ////
            //// RAPORLANACAK ALANLARI SEÇ
            ////
 
                if (!TOOGLE_TELEVIZYON.IsOn && !TOOGLE_GAZETE.IsOn && !TOOGLE_DERGI.IsOn
                    && !TOOGLE_OUTDOOR.IsOn && !TOOGLE_SINEMA.IsOn && !TOOGLE_RADYO.IsOn && !TOOGLE_INTERNET.IsOn)
                {
                    MessageBox.Show("Mecra Türü Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return;
                }
                if (gridView_SABITLER.Columns.Count  < 1)
                {
                    MessageBox.Show("Rapor Başlıkları seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return;
                }  
                SqlConnection myCon = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                myCon.Open(); 
                ///
                /// Kırılımlı Rapor İçin Node Kontrol
                ///   
                if (treeList_KIRILIMLAR.Nodes.Count != 0)
                {
                    List<TreeListNode> nds = treeList_KIRILIMLAR.GetAllCheckedNodes();
                    foreach (TreeListNode node in nds)
                    {
                        if (node.Checked)
                        {
                            string PATH_ = GetFullPath(node, "/");
                            PATH_ = PATH_.Substring(1, PATH_.Length - 1);
                            string[] wordm = PATH_.Split('/');
                            if (COLUMS_COUNT < wordm.Length) COLUMS_COUNT = wordm.Length;
                        }
                    }
                    if (COLUMS_COUNT > 0)
                    {
                        for (int i = 1; i <= COLUMS_COUNT; i++)
                        {
                            KIRILIM += string.Format("[KIRILIM_{0}],", i);
                            KIRILIM_TABLE_CREATE += string.Format("[KIRILIM_{0}] [nvarchar] (70) NULL ,", i);
                        }
                    }
                }
                ///
                /// Seçilen Başlıkları Node Kontrol
                ///    
                for (int iX = 0; iX <= gridView_SABITLER.Columns.Count-1; iX++)
                {            
                  BASLIKLAR += string.Format("[{0}],", gridView_SABITLER.Columns[iX].FieldName);               
                }              

                if (BASLIKLAR.Length > 0) BASLIKLAR = BASLIKLAR.Substring(0, BASLIKLAR.Length - 1);
                ///
                /// Kırılımlı Rapor İçin Node Kontrol
                ///    
                string PATH_KIRILIMLAR = "";
                if (treeList_KIRILIMLAR.Nodes.Count != 0)
                { 
                    List<TreeListNode> ndsc = treeList_KIRILIMLAR.GetAllCheckedNodes();
                    foreach (TreeListNode nodes in ndsc)
                    {
                        if (nodes.Checked)
                        {
                           KIRILIM_CAST = "";
                           PATH_KIRILIMLAR = GetFullPath(nodes, "\\");
                           PATH_KIRILIMLAR = PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);
                           string[] wordm = PATH_KIRILIMLAR.Split('\\');
                           if (COLUMS_COUNT < wordm.Length) COLUMS_COUNT = wordm.Length;
                           if (COLUMS_COUNT > 0)
                           {
                                int NodeSay = 0;
                                for (int i = 1; i <= COLUMS_COUNT; i++)
                                {
                                    KIRILIM_CAST += string.Format(" CAST('{0}' AS nvarchar ) AS  [KIRILIM_{1}],", wordm[NodeSay], i);
                                    if (NodeSay < wordm.Length - 1) NodeSay++;
                                }
                           }
                          if (treeList_FILITRELER.Nodes.Count == 0)
                          {
                             ANAKIRILIM_FILITRE_CHEK_KONTROL("");
                          }
                          else
                          {
                            TreeListNode myNode = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == "KIRILIM#" + PATH_KIRILIMLAR; });
                            if (myNode != null)
                            {
                                ArrayList selectedNodes = new ArrayList();
                                selectChildren(myNode, selectedNodes); 
                                foreach (TreeListNode item in selectedNodes)
                                {
                                    if (item.GetValue("TYPE").ToString() == "KIRILIM")
                                    {
                                        if (QUERY_TELEVIZYON != "") TMP_QUERY_TELEVIZYON += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_TELEVIZYON, 4, "").ToString());
                                        if (QUERY_GAZETE != "") TMP_QUERY_GAZETE += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_GAZETE, 4, "").ToString());
                                        if (QUERY_DERGI != "") TMP_QUERY_DERGI += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_DERGI, 4, "").ToString());
                                        if (QUERY_SINEMA != "") TMP_QUERY_SINEMA += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_SINEMA, 4, "").ToString());
                                        if (QUERY_RADYO != "") TMP_QUERY_RADYO += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_RADYO, 4, "").ToString());
                                        if (QUERY_OUTDOOR != "") TMP_QUERY_OUTDOOR += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_OUTDOOR, 4, "").ToString());
                                        if (QUERY_INTERNET != "") TMP_QUERY_INTERNET += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_INTERNET, 4, "").ToString());

                                        if (TMP_QUERY_TELEVIZYON.Length > 0 || TMP_QUERY_GAZETE.Length > 0 || TMP_QUERY_DERGI.Length > 0 || TMP_QUERY_SINEMA.Length > 0 || TMP_QUERY_RADYO.Length > 0 || TMP_QUERY_OUTDOOR.Length > 0 || TMP_QUERY_INTERNET.Length > 0)
                                        {
                                            ANAKIRILIM_FILITRE_CHEK_KONTROL(myNode.GetDisplayText("TEXT").ToString());
                                        }
                                    }

                                    if (item.GetValue("TEXT").ToString().IndexOf("(Dahil)") != -1) DAHIL_HARIC = "Dahil";
                                    if (item.GetValue("TEXT").ToString().IndexOf("(Hariç)") != -1) DAHIL_HARIC = "Hariç";
                                    if (item.GetValue("TYPE").ToString() == "BASLIK")
                                    {
                                        if (QUERY_TELEVIZYON != "") TMP_QUERY_TELEVIZYON += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_TELEVIZYON, 4, "").ToString());
                                        if (QUERY_GAZETE != "") TMP_QUERY_GAZETE += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_GAZETE, 4, "").ToString());
                                        if (QUERY_DERGI != "") TMP_QUERY_DERGI += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_DERGI, 4, "").ToString());
                                        if (QUERY_SINEMA != "") TMP_QUERY_SINEMA += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_SINEMA, 4, "").ToString());
                                        if (QUERY_RADYO != "") TMP_QUERY_RADYO += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_RADYO, 4, "").ToString());
                                        if (QUERY_OUTDOOR != "") TMP_QUERY_OUTDOOR += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_OUTDOOR, 4, "").ToString());
                                        if (QUERY_INTERNET != "") TMP_QUERY_INTERNET += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_INTERNET, 4, "").ToString());

                                        QUERY_TELEVIZYON = QUERY_GAZETE = QUERY_DERGI = QUERY_SINEMA = QUERY_RADYO = QUERY_OUTDOOR = QUERY_INTERNET = "";

                                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                                        {
                                            SqlCommand myCommand = new SqlCommand(string.Format(" SELECT   * FROM   dbo.ADM_SECENEKLER where SECENEK='{0}'", item.GetValue("NAME").ToString()), myConnection);
                                            myConnection.Open();
                                            SqlDataReader myReader = myCommand.ExecuteReader();
                                            while (myReader.Read())
                                            {
                                                BASLIK_BUL = myReader["FIELDS"].ToString();
                                                if (myReader["TELEVIZYON"].ToString().IndexOf("cast") == -1) _TELEVIZYON = 1; else _TELEVIZYON = 0;
                                                if (myReader["RADYO"].ToString().IndexOf("cast") == -1) _RADYO = 1; else _RADYO = 0;
                                                if (myReader["GAZETE"].ToString().IndexOf("cast") == -1) _GAZETE = 1; else _GAZETE = 0;
                                                if (myReader["DERGI"].ToString().IndexOf("cast") == -1) _DERGI = 1; else _DERGI = 0;
                                                if (myReader["SINEMA"].ToString().IndexOf("cast") == -1) _SINEMA = 1; else _SINEMA = 0;
                                                if (myReader["INTERNET"].ToString().IndexOf("cast") == -1) _INTERNET = 1; else _INTERNET = 0;
                                                if (myReader["OUTDOOR"].ToString().IndexOf("cast") == -1) _OUTDOOR = 1; else _OUTDOOR = 0;
                                            }
                                        }
                                    }
                                    if (item.GetValue("TYPE").ToString() == "SATIR")
                                    {
                                        if (DAHIL_HARIC == "Dahil")
                                        {
                                            if (item.GetValue("TEXT").ToString().IndexOf("%") != -1)
                                            {
                                                if (_TELEVIZYON == 1) QUERY_TELEVIZYON += string.Format("{0} LIKE '{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_GAZETE == 1) QUERY_GAZETE += string.Format("{0} LIKE '{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_DERGI == 1) QUERY_DERGI += string.Format("{0} LIKE '{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_SINEMA == 1) QUERY_SINEMA += string.Format("{0} LIKE '{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_RADYO == 1) QUERY_RADYO += string.Format("{0} LIKE '{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_OUTDOOR == 1) QUERY_OUTDOOR += string.Format("{0} LIKE '{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_INTERNET == 1) QUERY_INTERNET += string.Format("{0} LIKE '{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                            }
                                            else
                                            {
                                                if (_TELEVIZYON == 1) QUERY_TELEVIZYON += string.Format("{0}='{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_GAZETE == 1) QUERY_GAZETE += string.Format("{0}='{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_DERGI == 1) QUERY_DERGI += string.Format("{0}='{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_SINEMA == 1) QUERY_SINEMA += string.Format("{0}='{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_RADYO == 1) QUERY_RADYO += string.Format("{0}='{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_OUTDOOR == 1) QUERY_OUTDOOR += string.Format("{0}='{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_INTERNET == 1) QUERY_INTERNET += string.Format("{0}='{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));
                                            }
                                        }

                                        if (DAHIL_HARIC == "Hariç")
                                        {
                                            if (item.GetValue("TEXT").ToString().IndexOf("%") != -1)
                                            {
                                                if (_TELEVIZYON == 1) QUERY_TELEVIZYON += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_GAZETE == 1) QUERY_GAZETE += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_DERGI == 1) QUERY_DERGI += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_SINEMA == 1) QUERY_SINEMA += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_RADYO == 1) QUERY_RADYO += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_OUTDOOR == 1) QUERY_OUTDOOR += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_INTERNET == 1) QUERY_INTERNET += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                            }
                                            else
                                            {
                                                if (_TELEVIZYON == 1) QUERY_TELEVIZYON += string.Format("{0}<>'{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_GAZETE == 1) QUERY_GAZETE += string.Format("{0}<>'{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_DERGI == 1) QUERY_DERGI += string.Format("{0}<>'{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_SINEMA == 1) QUERY_SINEMA += string.Format("{0}<>'{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_RADYO == 1) QUERY_RADYO += string.Format("{0}<>'{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_OUTDOOR == 1) QUERY_OUTDOOR += string.Format("{0}<>'{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                                if (_INTERNET == 1) QUERY_INTERNET += string.Format("{0}<>'{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));
                                            }
                                        }
                                    }
                                }

                                if (QUERY_TELEVIZYON != "") TMP_QUERY_TELEVIZYON += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_TELEVIZYON, 4, "").ToString());
                                if (QUERY_GAZETE != "") TMP_QUERY_GAZETE += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_GAZETE, 4, "").ToString());
                                if (QUERY_DERGI != "") TMP_QUERY_DERGI += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_DERGI, 4, "").ToString());
                                if (QUERY_SINEMA != "") TMP_QUERY_SINEMA += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_SINEMA, 4, "").ToString());
                                if (QUERY_RADYO != "") TMP_QUERY_RADYO += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_RADYO, 4, "").ToString());
                                if (QUERY_OUTDOOR != "") TMP_QUERY_OUTDOOR += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_OUTDOOR, 4, "").ToString());
                                if (QUERY_INTERNET != "") TMP_QUERY_INTERNET += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_INTERNET, 4, "").ToString());

                                ANAKIRILIM_FILITRE_CHEK_KONTROL(myNode.GetDisplayText("TEXT").ToString());
                            } 
                         }
                       }
                    } 
            }
            else
            { MessageBox.Show(" Lütfen Rapor seçiniz."); }
        }

        private void RAPOR_LIST_SABIT(string _BASLIKLAR,string _Where, string _BASLIKLAR_GROUPBY)
        {
            if (_Where != "" && _BASLIKLAR!="")
            {
                SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                SqlDataAdapter da = new SqlDataAdapter(string.Format(" SELECT  {0}  FROM   dbo._ADEX_INDEX_DATA Where {1}  GROUP BY {2}", _BASLIKLAR, _Where, BASLIKLAR_GROUPBY), myConnection);
                DataSet ds = new DataSet();
                da.Fill(ds, "stock");
                DataViewManager dvManager = new DataViewManager(ds);
                DW_LIST = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_FREE_TABLE.DataSource = DW_LIST;
            }
        }        
    
        private void TABLE_CREATE()
        {  
            using (SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format("IF EXISTS (SELECT * FROM dbo.sysobjects where id = object_id('[dbo].[__MAS_EDT_{0}_{1}]')) drop table [dbo].[__MAS_EDT_{0}_{1}]", lbID.Caption, lbFILE_NAME.Caption.ToString());
                SqlCommand myCommandTable = new SqlCommand(SQL) { Connection = myConnectionTable };
                myConnectionTable.Open();
                myCommandTable.ExecuteNonQuery();
                myCommandTable.Connection.Close();
                myConnectionTable.Close();
            }
            using (SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format("   CREATE TABLE [dbo].[__MAS_EDT_{0}_{1}] ( {2} ) ON [PRIMARY];", lbID.Caption, lbFILE_NAME.Caption.ToString(), " [ID] [int] IDENTITY(1,1) NOT NULL, "+BASLIK_TABLE_CREATE);
                SqlCommand myCommandTable = new SqlCommand(SQL) { Connection = myConnectionTable };
                myConnectionTable.Open();
                myCommandTable.ExecuteNonQuery();
                myCommandTable.Connection.Close();
                myConnectionTable.Close();
            } 
        } 


        private void BTN_KAYDET_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
            splashScreenManagers.ClosingDelay = 400;
            splashScreenManagers.ShowWaitForm();
            splashScreenManagers.SetWaitFormDescription("KAYDEDİLİYOR");

            string SELECT_MECRATURU = string.Empty;
            if (TOOGLE_TELEVIZYON.IsOn) SELECT_MECRATURU += "TELEVIZYON,";
            if (TOOGLE_GAZETE.IsOn) SELECT_MECRATURU += "GAZETE,";
            if (TOOGLE_DERGI.IsOn) SELECT_MECRATURU += "DERGI,";
            if (TOOGLE_SINEMA.IsOn) SELECT_MECRATURU += "SINEMA,";
            if (TOOGLE_RADYO.IsOn) SELECT_MECRATURU += "RADYO,";
            if (TOOGLE_INTERNET.IsOn) SELECT_MECRATURU += "INTERNET,";
            if (TOOGLE_OUTDOOR.IsOn) SELECT_MECRATURU += "OUTDOOR,";


            if (lbID.Caption != "")
            {
                SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                con.Open();
                if (DW_LIST != null)
                {
                    TARIFELER._GLOBAL_TARIFELER MAS = new TARIFELER._GLOBAL_TARIFELER();
                    // SATIR SİL
                    DW_LIST.RowStateFilter = DataViewRowState.Deleted;
                    if (DW_LIST.Count != 0)
                    {
                        for (int i = 0; i <= DW_LIST.Count - 1; i++)
                        {
                            DataRow DR = DW_LIST[i].Row;
                            MAS.MASTER_ROW_DELETE(con, DR,   lbFILE_NAME.Caption.ToString(), lbID.Caption);
                        }
                    }

                    // Yeni eklenmiş Satırları kaydet
                    DW_LIST.RowStateFilter = DataViewRowState.Added;
                    if (DW_LIST.Count != 0)
                    {
                        for (int i = 0; i <= DW_LIST.Count - 1; i++)
                        {
                            DataRow DR = DW_LIST[i].Row;
                            MAS.MASTER_ROW_ADD(con, DR,  lbFILE_NAME.Caption.ToString(), lbID.Caption);
                        }
                    }
                    // SATIR GUNCELLE 
                    DW_LIST.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    if (DW_LIST.Count != 0)
                    {
                        for (int i = 0; i <= DW_LIST.Count - 1; i++)
                        {
                            DataRow DR = DW_LIST[i].Row;
                            MAS.MASTER_ROW_UPDATE(con, DR,  lbFILE_NAME.Caption.ToString(),lbID.Caption);
                        }
                    }
                    DW_LIST.Table.AcceptChanges();
                    DW_LIST.RowStateFilter = DataViewRowState.CurrentRows;
                }

              
                if (DW_LIST_ALT != null)
                {
                    TARIFELER._GLOBAL_TARIFELER MAS = new TARIFELER._GLOBAL_TARIFELER();
                    // SATIR SİL
                    DW_LIST_ALT.RowStateFilter = DataViewRowState.Deleted;
                    if (DW_LIST_ALT.Count != 0)
                    {
                        for (int i = 0; i <= DW_LIST_ALT.Count - 1; i++)
                        {
                            DataRow DR = DW_LIST_ALT[i].Row;
                            MAS.VERSIYON_ROW_DELETE(con, DR, lbFILE_NAME.Caption.ToString(), lbID.Caption);
                        }
                    }

                    // Yeni eklenmiş Satırları kaydet
                    DW_LIST_ALT.RowStateFilter = DataViewRowState.Added;
                    if (DW_LIST_ALT.Count != 0)
                    {
                        for (int i = 0; i <= DW_LIST_ALT.Count - 1; i++)
                        {
                            DataRow DR = DW_LIST_ALT[i].Row;
                            MAS.VERSIYON_ROW_ADD (con, DR, lbFILE_NAME.Caption.ToString(), lbID.Caption);
                        }
                    }
                    // SATIR GUNCELLE 
                    DW_LIST_ALT.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    if (DW_LIST_ALT.Count != 0)
                    {
                        for (int i = 0; i <= DW_LIST_ALT.Count - 1; i++)
                        {
                            DataRow DR = DW_LIST_ALT[i].Row;
                            MAS.VERSIYON_ROW_UPDATE(con, DR, lbFILE_NAME.Caption.ToString(), lbID.Caption);
                        }
                    }
                    DW_LIST_ALT.Table.AcceptChanges();
                    DW_LIST_ALT.RowStateFilter = DataViewRowState.CurrentRows;
                }


            }
            if (lbID.Caption == "")
            { 
                if(treeList_FILITRELER.FocusedNode!=null) lbFILE_FILITRE.Caption = treeList_FILITRELER.FocusedNode.GetValue("TEXT").ToString();

                _TARIFE_KAYDET frm = new _TARIFE_KAYDET();
                frm.ShowDialog(); 
                lbFILE_NAME.Caption = frm._TXT_FILE_NAME.ToString();
                if (lbFILE_NAME.Caption.ToString() != "" && frm._BTN_TYPE == "Tamam")
                {
                    DateTime myDTStart = Convert.ToDateTime(DateTime.Now);
                    SqlConnection myConnectionKontrol = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                    string myInsertQueryKontrol = string.Format(" select * from dbo.TRF_TARIFELER_LISTESI Where  TARIFE_TURU='MASTER' AND  SIRKET_KODU='{0}' and MECRA_TURU='DERGI' and TARIFE_KODU= '{1}' ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, lbFILE_NAME.Caption);
                    SqlCommand myCommandKontrol = new SqlCommand(myInsertQueryKontrol) { Connection = myConnectionKontrol };
                    myConnectionKontrol.Open();
                    SqlDataReader myReaderKontrol = myCommandKontrol.ExecuteReader(CommandBehavior.CloseConnection);
                    if (myReaderKontrol.HasRows == false)
                    {
                        using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            conn.Open();
                            SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_TARIFELER_LISTESI(TARIFE_TURU,SIRKET_KODU, MECRA_TURU,TARIFE_KODU, TARIFE_OWNER,ISLEM_TARIHI,ISLEM_SAATI,BASLIKLAR,FILITRE_TEXT,FILITRE_PATH)  VALUES ( @TARIFE_TURU,@SIRKET_KODU, @MECRA_TURU,@TARIFE_KODU, @TARIFE_OWNER,@ISLEM_TARIHI,@ISLEM_SAATI ,@BASLIKLAR,@FILITRE_TEXT,@FILITRE_PATH) SELECT @@IDENTITY AS ID " };
                            myCmd.Parameters.Add("@TARIFE_TURU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_TURU"].Value = "MASTER";
                            myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                            myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = SELECT_MECRATURU;
                            myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = lbFILE_NAME.Caption;
                            myCmd.Parameters.Add("@TARIFE_OWNER", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_OWNER"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                            myCmd.Parameters.Add("@ISLEM_TARIHI", SqlDbType.DateTime); myCmd.Parameters["@ISLEM_TARIHI"].Value = myDTStart.ToString("dd.MM.yyyy").ToString();
                            myCmd.Parameters.Add("@ISLEM_SAATI", SqlDbType.DateTime); myCmd.Parameters["@ISLEM_SAATI"].Value = myDTStart.ToString("hh:MM:ss").ToString();
                            string _TEXT = "", _PATH = "";
                            List<TreeListNode> nodes = treeList_KIRILIMLAR.GetAllCheckedNodes();
                            foreach (TreeListNode node in nodes)
                            {
                                string PATH_KIRILIMLAR = GetFullPath(node, "\\");
                                PATH_KIRILIMLAR = PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);
                                _TEXT += node.GetDisplayText("TEXT") + ";"; _PATH += PATH_KIRILIMLAR + ";";
                            }

                            myCmd.Parameters.Add("@FILITRE_TEXT", SqlDbType.NVarChar); myCmd.Parameters["@FILITRE_TEXT"].Value = _TEXT;
                            myCmd.Parameters.Add("@FILITRE_PATH", SqlDbType.NVarChar); myCmd.Parameters["@FILITRE_PATH"].Value = _PATH;
                            string COLUMS_CAPTION = "";
                            for (int i = 0; i < gridView_SABITLER.Columns.Count; i++)
                            {
                                COLUMS_CAPTION += gridView_SABITLER.Columns[i].Caption + ";";
                            }
                            myCmd.Parameters.Add("@BASLIKLAR", SqlDbType.NVarChar); myCmd.Parameters["@BASLIKLAR"].Value = COLUMS_CAPTION;
                            myCmd.Connection = conn;
                            SqlDataReader myReader = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
                            while (myReader.Read())
                            {
                                lbID.Caption = myReader["ID"].ToString();
                            }
                            myReader.Close();
                            myCmd.Connection.Close();
                        }

                        for (int i = 0; i < gridView_SABITLER.Columns.Count; i++)
                        {
                            BASLIK_TABLE_CREATE += string.Format("[{0}] [nvarchar](250) NULL ,", gridView_SABITLER.Columns[i].FieldName);
                        }
                        if (BASLIK_TABLE_CREATE.Length > 0) BASLIK_TABLE_CREATE = BASLIK_TABLE_CREATE.Substring(0, BASLIK_TABLE_CREATE.Length - 1);

                        TABLE_CREATE();

                        SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        con.Open();
                        if (DW_LIST != null)
                        {
                            TARIFELER._GLOBAL_TARIFELER MAS = new TARIFELER._GLOBAL_TARIFELER();
                            // SATIR SİL
                            DW_LIST.RowStateFilter = DataViewRowState.Deleted;
                            if (DW_LIST.Count != 0)
                            {
                                for (int i = 0; i <= DW_LIST.Count - 1; i++)
                                {
                                    DataRow DR = DW_LIST[i].Row;
                                    DR.Delete();
                                }
                            }
                            DW_LIST.Table.AcceptChanges();

                            // Yeni eklenmiş Satırları kaydet
                            DW_LIST.RowStateFilter = DataViewRowState.CurrentRows;
                            if (DW_LIST.Count != 0)
                            {
                                for (int i = 0; i <= DW_LIST.Count - 1; i++)
                                {
                                    DataRow DR = DW_LIST[i].Row;
                                    MAS.MASTER_ROW_ADD(con, DR, lbFILE_NAME.Caption.ToString(), lbID.Caption);
                                }
                            }
                            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                DataSet ds = new DataSet();
                                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("SELECT * FROM dbo.[__MAS_EDT_{0}_{1}]", lbID.Caption, lbFILE_NAME.Caption.ToString()), conn) };
                                adapter.Fill(ds, "TRF_MASTER");
                                DataViewManager dvManager = new DataViewManager(ds);
                                DW_LIST = dvManager.CreateDataView(ds.Tables[0]);
                                gridControl_FREE_TABLE.DataSource = DW_LIST;
                            }
                            DW_LIST.Table.AcceptChanges();
                            DW_LIST.RowStateFilter = DataViewRowState.CurrentRows;
                        }

                            if (DW_LIST_ALT != null)
                            {
                                TARIFELER._GLOBAL_TARIFELER MAS = new TARIFELER._GLOBAL_TARIFELER();
                                // SATIR SİL
                                DW_LIST_ALT.RowStateFilter = DataViewRowState.Deleted;
                                if (DW_LIST_ALT.Count != 0)
                                {
                                    for (int i = 0; i <= DW_LIST_ALT.Count - 1; i++)
                                    {
                                        DataRow DR = DW_LIST_ALT[i].Row;
                                        MAS.VERSIYON_ROW_DELETE(con, DR, lbFILE_NAME.Caption.ToString(), lbID.Caption);
                                    }
                                }

                                // Yeni eklenmiş Satırları kaydet
                                DW_LIST_ALT.RowStateFilter = DataViewRowState.Added;
                                if (DW_LIST_ALT.Count != 0)
                                {
                                    for (int i = 0; i <= DW_LIST_ALT.Count - 1; i++)
                                    {
                                        DataRow DR = DW_LIST_ALT[i].Row;
                                        MAS.VERSIYON_ROW_ADD(con, DR, lbFILE_NAME.Caption.ToString(), lbID.Caption);
                                    }
                                } 
                                DW_LIST_ALT.Table.AcceptChanges();
                                DW_LIST_ALT.RowStateFilter = DataViewRowState.CurrentRows;
                            }

                        splashScreenManagers.SetWaitFormDescription("KAYDEDİLDİ");
                        if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                        MessageBox.Show("KAYDEDİLDİ", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                        MessageBox.Show("Bu kod daha önce kullanılmış! Lütfen farklı bir kod kullanınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                //    MessageBox.Show("Kaydedildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            } 
            if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm(); 
        }

        private void BTN_LISTE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TOOGLE_TELEVIZYON.IsOn = false;
            TOOGLE_GAZETE.IsOn = false;
            TOOGLE_DERGI.IsOn = false;
            TOOGLE_SINEMA.IsOn = false;
            TOOGLE_RADYO.IsOn = false;
            TOOGLE_OUTDOOR.IsOn = false;
            TOOGLE_INTERNET.IsOn = false;

            TARIFELER._TARIFE_LISTESI trf = new TARIFELER._TARIFE_LISTESI("MASTER","");
            trf.ShowDialog(); 
            if (trf._BTN_TYPE == "Tamam")
            {
                treeList_BASLIKLAR.Enabled = false;
                gridControl_FREE_TABLE.DataSource = null;
                gridView_SABITLER.Columns.Clear();
                gridControl_FREE_TABLE.Refresh();
                lbFILE_NAME.Caption = trf._TARIFE_KODU;
                lbID.Caption = trf._TARIFE_ID;
                if (trf._MECRA_TURLERI != null)
                {   
                    string[] Onez = trf._MECRA_TURLERI.Split(',');
                    for (int i = 0; i < Onez.Length - 1; i++)
                    {
                        if (Onez[i].Trim()=="TELEVIZYON") TOOGLE_TELEVIZYON.IsOn = true;
                        if (Onez[i].Trim() == "GAZETE") TOOGLE_GAZETE.IsOn = true;
                        if (Onez[i].Trim() == "DERGI") TOOGLE_DERGI.IsOn = true;
                        if (Onez[i].Trim() == "SINEMA") TOOGLE_SINEMA.IsOn = true;
                        if (Onez[i].Trim() == "RADYO") TOOGLE_RADYO.IsOn = true;
                        if (Onez[i].Trim() == "OUTDOOR") TOOGLE_OUTDOOR.IsOn = true;
                        if (Onez[i].Trim() == "INTERNET") TOOGLE_OUTDOOR.IsOn = true;
                    }
                }

                string[] Onesz = trf._FILITRE_PATH.Split(';');
                for (int i = 0; i < Onesz.Length; i++)
                { 
                    TreeListNode myNodex = treeList_KIRILIMLAR.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", Onesz[i]); });
                    if (myNodex != null)
                    {
                        myNodex.CheckState = CheckState.Checked;  
                    }
                } 

                if (_GLOBAL_PARAMETRELER._RAPOR_KODU != null)
                { 
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        DataSet ds = new DataSet();
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("SELECT * FROM     dbo.__MAS_EDT_{0}_{1}", lbID.Caption, lbFILE_NAME.Caption.ToString()), conn) };
                        adapter.Fill(ds, "TRF_MASTER");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DW_LIST = dvManager.CreateDataView(ds.Tables[0]);
                        gridControl_FREE_TABLE.DataSource = DW_LIST;
                        gridView_SABITLER.Columns["ID"].Visible = false;
                        gridView_SABITLER.Columns["GUID"].Visible = false;
                      //  gridView_SABITLER.Columns["NEW"].Visible = false;
                        //gridView_.Columns["DAHIL_HARIC"].Width = 25;
                        //gridView_.Columns["ALT"].Width = 20;
                    } 
                    if (trf._BASLIKLAR != null)
                    {
                       // tmp._BASLIKLAR = "ID;" + tmp._BASLIKLAR;
                        string[] Ones = trf._BASLIKLAR.Split(';');
                        for (int i = 0; i < Onesz.Length - 1; i++)
                        {
                            gridView_SABITLER.Columns[1+i].Caption = Ones[i].Trim();
                            gridView_SABITLER.Columns[1+i].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                        }
                    }  
                }
                else
                { 
                    MessageBox.Show("Rapor Kodu seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                } 
            } 
        }


        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check; 
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }

        private void BTN_YENI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //SABIT_LISTE();


            TOOGLE_TELEVIZYON.IsOn = false;
            TOOGLE_GAZETE.IsOn = false;
            TOOGLE_DERGI.IsOn = false;
            TOOGLE_SINEMA.IsOn = false;
            TOOGLE_RADYO.IsOn = false;
            TOOGLE_OUTDOOR.IsOn = false;
            TOOGLE_INTERNET.IsOn = false;

            treeList_BASLIKLAR.Enabled = true;
            gridControl_FREE_TABLE.DataSource = null;
            gridView_SABITLER.Columns.Clear();
            gridControl_FREE_TABLE.Refresh();
            lbFILE_FILITRE.Caption = "";
            lbID.Caption = "";
            lbFILE_NAME.Caption = "";
        }

        private void treeList_BASLIKLAR_DoubleClick(object sender, EventArgs e)
        {
             TreeList tree = sender as TreeList;
             TreeListHitInfo hi = tree.CalcHitInfo(tree.PointToClient(Control.MousePosition));
             if (hi.Node != null)
             {
                     string DURUMU = "OZEL";
                     string FIELD_NAME = ""; 
                     using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                     {
                         string SQL = string.Format(" SELECT   * FROM   dbo.ADM_SECENEKLER where  SECENEK='{0}'", hi.Node.GetValue(0));
                         SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                         myConnection.Open();
                         SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                         if (myReader.HasRows)
                         {
                             while (myReader.Read())
                             {
                                 if (myReader["ALT_BILGI"].ToString() == "VAR")
                                 {
                                     FIELD_NAME = myReader["FIELDS"].ToString();
                                     DURUMU = "GENEL";
                                 }
                                 else
                                 {
                                     MessageBox.Show("Bu seçeneğin içeriği mevcut değil.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                     return; 
                                 }
                             }
                         }
                         else
                         {
                             FIELD_NAME = hi.Node.GetValue(0).ToString();
                         }
                     }
                     for (int i = 0; i < gridView_SABITLER.Columns.Count; i++)
                     {
                         if (FIELD_NAME == gridView_SABITLER.Columns[i].FieldName) { MessageBox.Show("Bu alan Kullanılmış"); return; }
                     } 

                     GridColumn column = gridView_SABITLER.Columns.AddVisible(hi.Node.GetValue(0).ToString(), string.Empty);
                     column.Caption = hi.Node.GetValue(0).ToString();
                     column.FieldName = FIELD_NAME;
                     if (DURUMU == "GENEL")
                     {
                         column.OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                         column.OptionsColumn.AllowEdit = false; DURUMU = "OZEL";
                     }
                     gridView_SABITLER.Columns.Add(column); 
             }


        

        }
        private string ShowSaveFileDialog(string title, string filter)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                string name = Application.ProductName;
                int n = name.LastIndexOf(".") + 1;
                if (n > 0)
                    name = name.Substring(n, name.Length - n);
                dlg.Title = String.Format("Export To {0}", title);
                dlg.FileName = name;
                dlg.Filter = filter;
                if (dlg.ShowDialog() == DialogResult.OK)
                    return dlg.FileName;
            }
            return "";
        }
        private void BTN_EXCEL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { 

            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xlsx");
            if (fileName != "")
            {
                gridView_SABITLER.ExportToXlsx(fileName);
                OpenFile(fileName);
            }
        }
        private void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                    {
                        process.StartInfo.FileName = fileName;
                        process.StartInfo.Verb = "Open";
                        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                        process.Start();
                    }
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void gridView_SABITLER_PopupMenuShowing(object sender, DevExpress.XtraGrid.Views.Grid.PopupMenuShowingEventArgs e)
        { 
            //if (e.MenuType == DevExpress.XtraGrid.Views.Grid.GridMenuType.Row)
            //{
            //    e.Allow = false;
            //    GridView view = ((GridView)(sender));
            //    //popupMenu1.ShowPopup(view.GridControl.PointToScreen(e.Point));
            //    contextMenuStrips.Show(view.GridControl.PointToScreen(e.Point));
                
            //}
        }
     

        private void treeList_FILITRELER_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {
            lbFILE_FILITRE.Caption = treeList_FILITRELER.FocusedNode.GetValue("TEXT").ToString();
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridCell[] selectedCells = gridView_SABITLER.GetSelectedCells();
            gridView_SABITLER.BeginUpdate();
            try
            {
                for (int i = 0; (i <= (selectedCells.Length - 1)); i++)
                {
                    if (selectedCells[i].Column.OptionsColumn.AllowEdit)
                    {
                        gridView_SABITLER.SetRowCellValue(selectedCells[i].RowHandle, selectedCells[i].Column, Clipboard.GetText().ToString());
                    }
                }
            }
            finally
            {
                gridView_SABITLER.EndUpdate();
            }
        }
    }
}