using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System.Collections;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace CoreV2.PLANLAMA.VIEWER
{
    public partial class RAPOR_VIEWER : DevExpress.XtraEditors.XtraForm
    {  /// <summary>
        /// TREEVIEW 
        /// </summary>  
        public DataView DW_LIST_KIRLIMLAR, DW_LIST_BASLIK, DW_LIST_OLCUMLER, DW_LIST_FILITRE, DW_LIST_GENEL_FILITRE;


        TreeListNode KIRILIM_NODE;

        string SELECT_SABIT_SECENEKLER = "", GAZETE_TARIFE_TURU = "STANDART_TARIFE", GAZETE_TARIFE_SECENEK = "MECRA", MASTER_SELECT = "", KEYWORD_SELECT = ""; 
        int KIRILIM_SAY, BASLIK_SAY, OLCUM_SAY, FILITRE_SAY; 
        DevExpress.XtraGrid.GridControl grdCntrl_ = null;
        DevExpress.XtraGrid.Views.Grid.GridView gridView_ = null; 
        DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit re_WORD_MEMO = null;
        DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit re_FIELD_SELECT = null;
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox re_FIELD_UPDATES = null;

        DataTable Tb;
        DataView dv_CODE_LIST;
        string QUERY = "";
        DataView dvs;

        _GLOBAL_PARAMETRELER GLOBAL = new _GLOBAL_PARAMETRELER();

        string _BR_IDS = "", _BR_RAPOR_TIPI = "", _BR_RAPOR_KODU = "";

        public RAPOR_VIEWER()
        {
            InitializeComponent();

            BR_IDS.Caption = null;
            BR_RAPOR_KODU.Caption = null;
            DateTime myDTStart = Convert.ToDateTime(DateTime.Now);
            dtBAS_TARIHI.EditValue = myDTStart.AddDays(-30).ToString("MM.yyyy").ToString();
            dtBIT_TARIHI.EditValue = myDTStart.ToString("MM.yyyy").ToString(); 
            HEDEFKITLE_LISTESI();
            SECENEKLER_LIST("");
        }
        private void HEDEFKITLE_LISTESI()
        {
            re_TARGET_LIST.Items.Clear();
            re_TARGET_LIST.Items.Add("");
            //CMB_TARGET.Properties.Items.Clear();
            //CMB_TARGET.Properties.Items.Add("");
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlCommand myCommand = new SqlCommand("SELECT   FIELDS, SECENEK FROM   dbo.ADM_SECENEKLER WHERE   (BASLIK = N'HEDEFKİTLELER')", myConnection);
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReader.Read())
                {
                    re_TARGET_LIST.Items.Add(myReader["SECENEK"].ToString());
                    //CMB_TARGET.Properties.Items.Add(myReader["SECENEK"].ToString());
                }
            }
        }
        private void BR_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
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

        private void treeList_Sabitler_DoubleClick(object sender, EventArgs e)
        {
            TreeList tree = sender as TreeList;
            TreeListHitInfo hi = tree.CalcHitInfo(tree.PointToClient(Control.MousePosition));
            if (hi.Node != null)
            {
                if (hi.Node.ParentNode == null) return;
                if (hi.Node.GetValue(1).ToString() == "Ölçümler" || hi.Node.GetValue(1).ToString() == "HEDEFKİTLELER")
                {
                    //TreeListNode myNode =  treeList_OLCUMLER.FindNode((node) => { return node["TEXT"].ToString() == hi.Node.GetValue(0).ToString(); });
                    //if (myNode == null)
                    //{
                    int rowHandle = GetRowHandleByColumnValue(gridView_OLCUMLEME, "TEXT", hi.Node.GetValue(0).ToString());
                    if (rowHandle != GridControl.InvalidRowHandle)
                    {
                        //gridView1.FocusedColumn = gridView1.Columns.ColumnByFieldName("CustomerID");
                        //gridView1.FocusedRowHandle = rowHandle;
                        //if (gridView1.IsRowVisible(rowHandle) == RowVisibleState.Hidden)
                        //    gridView1.MakeRowVisible(rowHandle, false);
                        //gridView1.ShowEditor();
                    }
                    else
                    {
                        OLCUM_SAY++;
                        DataRowView newRow = DW_LIST_OLCUMLER.AddNew();
                        newRow["ID"] = OLCUM_SAY;
                        newRow["ParentID"] = -1;
                        newRow["CHILD_INDEX"] = DW_LIST_OLCUMLER.Count;
                        newRow["TEXT"] = hi.Node.GetValue(0).ToString();
                        newRow["PATH"] = GetFullPath(hi.Node, "/");
                        newRow["NAME"] = hi.Node.GetValue(0).ToString();
                        newRow.EndEdit();
                    }
                }
                else
                {


                    int rowHandle = GetRowHandleByColumnValue(gridView_BASLIKLAR, "TEXT", hi.Node.GetValue(0).ToString());
                    if (rowHandle != GridControl.InvalidRowHandle)
                    {
                        //gridView1.FocusedColumn = gridView1.Columns.ColumnByFieldName("CustomerID");
                        //gridView1.FocusedRowHandle = rowHandle;
                        //if (gridView1.IsRowVisible(rowHandle) == RowVisibleState.Hidden)
                        //    gridView1.MakeRowVisible(rowHandle, false);
                        //gridView1.ShowEditor();
                    }
                    else
                    //    MessageBox.Show("Not found!");


                    //TreeListNode myNode = treeList_BASLIKLAR.FindNode((node) => { return node["TEXT"].ToString() == hi.Node.GetValue(0).ToString(); });
                    //if (myNode == null)
                    {
                        BASLIK_SAY++;
                        DataRowView newRow = DW_LIST_BASLIK.AddNew();
                        newRow["ID"] = BASLIK_SAY;
                        newRow["ParentID"] = -1;
                        newRow["CHILD_INDEX"] = DW_LIST_BASLIK.Count;
                        newRow["TEXT"] = hi.Node.GetValue(0).ToString();
                        newRow["PATH"] = GetFullPath(hi.Node, "/");
                        newRow["NAME"] = hi.Node.GetValue(0).ToString();
                        newRow.EndEdit();
                    }
                }
            }
        }

        private int GetRowHandleByColumnValue(GridView view, string ColumnFieldName, object value)
        {
            int result = GridControl.InvalidRowHandle;
            for (int i = 0; i < view.RowCount; i++)
                if (view.GetDataRow(i)[ColumnFieldName].Equals(value))
                    return i;
            return result;
        }
        private void CHKBOX_ALL_SELECT_CheckedChanged(object sender, EventArgs e)
        {

        }


        private void CHEK_CLEAN()
        {
        //    TOGGLE_FILITRE.Enabled = false;         
            TOGGLE_TARIFE.Enabled = false;            
            TOGGLE_MASTER.Enabled = false;
            TOGGLE_WORD.Enabled = false;
            TOGGLE_GENEL_FILITRE.Enabled = false;

            TOGGLE_FILITRE.IsOn = false;
            TOGGLE_TARIFE.IsOn = false;
            TOGGLE_MASTER.IsOn = false;
            TOGGLE_WORD.IsOn = false;
            TOGGLE_GENEL_FILITRE.IsOn = false;


            TOOGLE_TELEVIZYON.Enabled = false;
            TOOGLE_GAZETE.Enabled = false;
            TOOGLE_DERGI.Enabled = false;
            TOOGLE_SINEMA.Enabled = false;
            TOOGLE_RADYO.Enabled = false;
            TOOGLE_INTERNET.Enabled = false;
            TOOGLE_OUTDOOR.Enabled = false;
            CHKBOX_OTUZSN_GRP.Checked = false;

            TOOGLE_TELEVIZYON.IsOn = false;
            TOOGLE_GAZETE.IsOn = false;
            TOOGLE_DERGI.IsOn = false;
            TOOGLE_SINEMA.IsOn = false;
            TOOGLE_RADYO.IsOn = false;
            TOOGLE_INTERNET.IsOn = false;
            TOOGLE_OUTDOOR.IsOn = false;
            CHKBOX_OTUZSN_GRP.Checked = false;
        }

        public void RAPOR_HEADER_READ(string BR_RAPOR_TIPI, string BR_IDS, string BR_RAPOR_KODU)
        {

            CHEK_CLEAN();

            //for (int i = 0; i < MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.Count; i++)
            //{
            //    MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.RemoveAt(i);
            //}

            //for (int i = 0; i < WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count; i++)
            //{
            //    WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.RemoveAt(i);
            //}

            string SELECT_MECRA_TURLERI = "", SELECT_RAPOR_SECENEKLER = "";
            SELECT_SABIT_SECENEKLER = "";
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format(" SELECT  * FROM  dbo.ADM_RAPOR_DESIGNE WHERE    (SIRKET_KODU = N'{0}')   AND (RAPOR_KODU='{1}') ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, BR_RAPOR_KODU);
                SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReader.Read())
                {
                    _GLOBAL_PARAMETRELER._RAPOR_ID = myReader["ID"].ToString();
                    _BR_IDS = BR_IDS;
                    _BR_RAPOR_TIPI = BR_RAPOR_TIPI;
                    _BR_RAPOR_KODU = BR_RAPOR_KODU;

                    SELECT_SABIT_SECENEKLER = myReader["SABIT_SECENEKLER"].ToString();
                    SELECT_MECRA_TURLERI = myReader["MECRA_TURLERI"].ToString();
                    SELECT_RAPOR_SECENEKLER = myReader["RAPOR_SECENEKLER"].ToString();
                    MASTER_SELECT = myReader["MASTER_SELECT"].ToString();
                    KEYWORD_SELECT = myReader["KEYWORD_SELECT"].ToString();
                    KIRILIM_SAY = (int)myReader["KIRILIM_SAY"];
                    BASLIK_SAY = (int)myReader["BASLIK_SAY"];
                    OLCUM_SAY = (int)myReader["OLCUM_SAY"];
                    FILITRE_SAY = (int)myReader["FILITRE_SAY"];

                    if (myReader["OTUZ_SN_GRP"].ToString() == "True") CHKBOX_OTUZSN_GRP.Checked = true; else CHKBOX_OTUZSN_GRP.Checked = false;
                    if (myReader["BAS_TARIHI"] != DBNull.Value)
                    {
                        DateTime BAS_TARIHI = Convert.ToDateTime(myReader["BAS_TARIHI"].ToString());
                        DateTime BIT_TARIHI = Convert.ToDateTime(myReader["BIT_TARIHI"].ToString());
                        dtBAS_TARIHI.EditValue = BAS_TARIHI.ToString("MM.yyyy").ToString();
                        dtBIT_TARIHI.EditValue = BIT_TARIHI.ToString("MM.yyyy").ToString();
                        _GLOBAL_PARAMETRELER._START_DATE = Convert.ToDateTime(dtBAS_TARIHI.EditValue);
                        _GLOBAL_PARAMETRELER._END_DATE = Convert.ToDateTime(dtBIT_TARIHI.EditValue);
                    }
                }
            }

            SECENEKLER_LIST(BR_RAPOR_KODU); 

            string[] wordm = SELECT_MECRA_TURLERI.ToString().Split(',');
            foreach (string word in wordm)
            {
                if (word.Trim() == "TELEVIZYON") { TOOGLE_TELEVIZYON.IsOn = true; TOOGLE_TELEVIZYON.Enabled = true; }
                if (word.Trim() == "GAZETE") { TOOGLE_GAZETE.IsOn = true; TOOGLE_GAZETE.Enabled = true; }
                if (word.Trim() == "DERGI") { TOOGLE_DERGI.IsOn = true; TOOGLE_DERGI.Enabled = true; }
                if (word.Trim() == "SINEMA") { TOOGLE_SINEMA.IsOn = true; TOOGLE_SINEMA.Enabled = true; }
                if (word.Trim() == "RADYO") { TOOGLE_RADYO.IsOn = true; TOOGLE_RADYO.Enabled = true; }
                if (word.Trim() == "INTERNET") { TOOGLE_INTERNET.IsOn = true; TOOGLE_INTERNET.Enabled = true; }
                if (word.Trim() == "OUTDOOR") { TOOGLE_OUTDOOR.IsOn = true; TOOGLE_OUTDOOR.Enabled = true; }
            }

        }


        public void WORD_SECENEKLER_LIST(string RAPOR_KODU)
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

 
        string GENEL_SECENEKLER = "";
        public void SECENEKLER_LIST(string RAPOR_KODU)
        {
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet dsKr = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("       SELECT ID ,ParentID, TEXT, CHILD_INDEX,GUI,PATH,CHECKS,NAME FROM   dbo.ADM_RAPOR_KIRILIM    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}' order by ParentID,CHILD_INDEX", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU), conn) };
                adapter.Fill(dsKr, "ADM_RAPOR_KIRILIM");
                DataViewManager dvManager = new DataViewManager(dsKr);
                DW_LIST_KIRLIMLAR = dvManager.CreateDataView(dsKr.Tables[0]);
                treeList_KIRILIMLAR.KeyFieldName = "ID";
                treeList_KIRILIMLAR.ParentFieldName = "ParentID";
                treeList_KIRILIMLAR.DataSource = DW_LIST_KIRLIMLAR;
            }
            treeList_KIRILIMLAR.ExpandAll();
         //   treeList_KIRILIMLAR.NodesIterator.DoOperation(new InitialStateTreeListOperation());
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet dsb = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("       SELECT ID ,ParentID, TEXT, CHILD_INDEX,GUI,CHECKS FROM   dbo.ADM_RAPOR_BASLIK    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}' order by ParentID,CHILD_INDEX", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU), conn) };
                adapter.Fill(dsb, "ADM_RAPOR_BASLIK");
                DataViewManager dvManager = new DataViewManager(dsb);
                DW_LIST_BASLIK = dvManager.CreateDataView(dsb.Tables[0]);
                gridControl_BASLIKLAR.DataSource = DW_LIST_BASLIK;
                gridView_BASLIKLAR.Columns[OrderFieldName].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridView_BASLIKLAR.OptionsCustomization.AllowSort = false;
                gridView_BASLIKLAR.OptionsView.ShowGroupPanel = false;
            }
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet dsOl = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("       SELECT ID ,ParentID, TEXT, CHILD_INDEX,GUI,CHECKS FROM   dbo.ADM_RAPOR_OLCUMLEME    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}' order by ParentID,CHILD_INDEX", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU), conn) };
                adapter.Fill(dsOl, "ADM_RAPOR_OLCUMLEME");
                DataViewManager dvManager = new DataViewManager(dsOl);
                DW_LIST_OLCUMLER = dvManager.CreateDataView(dsOl.Tables[0]);
                gridControl_OLCUMLEME.DataSource = DW_LIST_OLCUMLER;
                gridView_OLCUMLEME.Columns[OrderFieldName].SortOrder = DevExpress.Data.ColumnSortOrder.Ascending;
                gridView_OLCUMLEME.OptionsCustomization.AllowSort = false;
                gridView_OLCUMLEME.OptionsView.ShowGroupPanel = false;
            }
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet dsFil = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("       SELECT top 0 ID ,ParentID, CHILD_INDEX,GUI,CHECKS,TEXT,NAME,TYPE,PATH  FROM   dbo.ADM_RAPOR_FILITRE    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}' order by ParentID,PATH ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU), conn) };
                 
                adapter.Fill(dsFil, "ADM_RAPOR_FILITRE");
                DataViewManager dvManager = new DataViewManager(dsFil);
                DW_LIST_FILITRE = dvManager.CreateDataView(dsFil.Tables[0]);
                treeList_FILITRELER.KeyFieldName = "ID";
                treeList_FILITRELER.ParentFieldName = "ParentID";
                treeList_FILITRELER.DataSource = DW_LIST_FILITRE;
            }
            treeList_FILITRELER.ExpandAll(); 
        }

        public class InitialStateTreeListOperation : TreeListOperation
        {
            public override void Execute(DevExpress.XtraTreeList.Nodes.TreeListNode node)
            {
                if (node["CHECKS"] == DBNull.Value) node["CHECKS"] = false;
                node.Checked = (bool)node["CHECKS"];
                //DataItem item = (DataItem)node.TreeList.GetDataRecordByNode(node);
                //node.Checked = item.Checked;
            }
        }


        public void _SAVE_UPDATE(string _RAPOR_KODU, string RAPOR_ACIKLAMASI)
        {

            string SELECT_MECRATURU = string.Empty;
            if (TOOGLE_TELEVIZYON.IsOn) SELECT_MECRATURU += "TELEVIZYON,";
            if (TOOGLE_GAZETE.IsOn) SELECT_MECRATURU += "GAZETE,";
            if (TOOGLE_DERGI.IsOn) SELECT_MECRATURU += "DERGI,";
            if (TOOGLE_SINEMA.IsOn) SELECT_MECRATURU += "SINEMA,";
            if (TOOGLE_RADYO.IsOn) SELECT_MECRATURU += "RADYO,";
            if (TOOGLE_INTERNET.IsOn) SELECT_MECRATURU += "INTERNET,";
            if (TOOGLE_OUTDOOR.IsOn) SELECT_MECRATURU += "OUTDOOR,";

            if (SELECT_MECRATURU.Length > 0) SELECT_MECRATURU = SELECT_MECRATURU.Remove(SELECT_MECRATURU.Length - 1);

            GENEL_SECENEKLER = ""; MASTER_SELECT = ""; KEYWORD_SELECT = "";
            string SABIT_SECENEKLER = "", RAPOR_SECENEKLER = "";
            DateTime BAS_TARIHI = Convert.ToDateTime(dtBAS_TARIHI.EditValue);
            DateTime BIT_TARIHI = Convert.ToDateTime(dtBIT_TARIHI.EditValue);

            SqlConnection SQLCON = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SQLCON.Open();

            //for (int i = 0; i <= MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1; i++)
            //{
            //    MASTER_SELECT += MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages[i].Name + ";";
            //}

            //for (int i = 0; i <= WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1; i++)
            //{
            //    KEYWORD_SELECT += WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[i].Name + ";";
            //}

            if (BR_IDS.Caption == "")
            {
                //CLASS.DESIGNE_CLASS SAVE = new CLASS.DESIGNE_CLASS();
                //var asd = SAVE._RAPOR_INSERT(_RAPOR_KODU, BAS_TARIHI, BIT_TARIHI,
                //     TrfSablon.TELEVIZYON_TARIFESI.Text, TrfSablon.RADYO_TARIFESI.Text, TrfSablon.GAZETE_TARIFESI.Text,
                //     TrfSablon.LBL_DERGI_TARIFE_KODU.Text, TrfSablon.SINEMA_TARIFESI.Text, TrfSablon.OUTDOOR_TARIFESI.Text, TrfSablon.INTERNET_TARIFESI.Text, TrfSablon.SEKTOR_TARIFESI.Text,
                //     SABIT_SECENEKLER, RAPOR_SECENEKLER, SELECT_MECRATURU, CHKBOX_OTUZSN_GRP.Checked, RAPOR_ACIKLAMASI, MASTER_SELECT, KEYWORD_SELECT,
                //     KIRILIM_SAY, BASLIK_SAY, OLCUM_SAY, FILITRE_SAY,
                //     _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL
                //     );

                //if (Convert.ToString(asd) != "0")
                //{
                //    BR_IDS.Caption = Convert.ToString(asd);
                //    BR_RAPOR_KODU.Caption = _RAPOR_KODU;
                //}
                //PROGRAM_TARIFESI ,NONE_TV_TARIFESI ,ORANLAR_TARIFESI
            }
            else
            {
                //int ID = Convert.ToInt32(BR_IDS.Caption);
                //CLASS.DESIGNE_CLASS UPDATE = new CLASS.DESIGNE_CLASS();
                //UPDATE._RAPOR_UPDATE(_RAPOR_KODU, BAS_TARIHI, BIT_TARIHI,
                //     TrfSablon.TELEVIZYON_TARIFESI.Text, TrfSablon.RADYO_TARIFESI.Text, TrfSablon.GAZETE_TARIFESI.Text,
                //    TrfSablon.LBL_DERGI_TARIFE_KODU.Text, TrfSablon.SINEMA_TARIFESI.Text, TrfSablon.OUTDOOR_TARIFESI.Text, TrfSablon.INTERNET_TARIFESI.Text, TrfSablon.SEKTOR_TARIFESI.Text,
                //     SABIT_SECENEKLER, RAPOR_SECENEKLER, SELECT_MECRATURU, CHKBOX_OTUZSN_GRP.Checked, ID, MASTER_SELECT, KEYWORD_SELECT,
                //          KIRILIM_SAY, BASLIK_SAY, OLCUM_SAY, FILITRE_SAY
                //     );
            }



            for (int iX = 0; iX <= gridView_BASLIKLAR.RowCount - 1; iX++)
            {
                DataRow DR = gridView_BASLIKLAR.GetDataRow(iX);
                DR["CHILD_INDEX"] = iX + 1;
            }

            for (int iX = 0; iX <= gridView_OLCUMLEME.RowCount - 1; iX++)
            {
                DataRow DR = gridView_OLCUMLEME.GetDataRow(iX);
                DR["CHILD_INDEX"] = iX + 1;
            }


            /// <summary>
            /// DW_LIST_ANA_KIRLIM 
            /// </summary>  
            /// 
            //// Satır Sil 
            if (DW_LIST_KIRLIMLAR != null)
            {
                //DW_LIST_KIRLIMLAR.RowStateFilter = DataViewRowState.Deleted;
                //DataRow[] delRows = DW_LIST_KIRLIMLAR.Table.Select(null, null, DataViewRowState.Deleted);
                //foreach (DataRow delRow in delRows)
                //{
                //    SqlCommand Cmd = new SqlCommand() { CommandText = "DELETE    dbo.ADM_RAPOR_KIRILIM  where GUI=@GUI " };
                //    Cmd.Parameters.AddWithValue("@GUI", delRow["GUI", DataRowVersion.Original].ToString());
                //    Cmd.Connection = SQLCON;
                //    Cmd.ExecuteNonQuery();
                //   // delRow.Delete();
                //}  

                string GUIS = "";
                DW_LIST_KIRLIMLAR.RowStateFilter = DataViewRowState.Deleted;
                DataRow[] delRows = DW_LIST_KIRLIMLAR.Table.Select(null, null, DataViewRowState.Deleted);
                if (delRows.Length > 0)
                {
                    foreach (DataRow delRow in delRows)
                    {
                        if (delRow["GUI", DataRowVersion.Original].ToString() != "") GUIS += "'" + delRow["GUI", DataRowVersion.Original].ToString() + "',";
                    }
                    GUIS = GUIS.Substring(0, GUIS.Length - 1);
                    SqlCommand Cmd = new SqlCommand() { CommandText = " DELETE FROM dbo.ADM_RAPOR_KIRILIM  WHERE GUI IN  ( " + GUIS + " )" };
                    Cmd.Connection = SQLCON;
                    Cmd.ExecuteNonQuery();
                }

                DW_LIST_KIRLIMLAR.RowStateFilter = DataViewRowState.Added;

                foreach (DataRowView drv in DW_LIST_KIRLIMLAR)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        //int ogeSayisi = 5;
                        //int toplamOgeSayisi = 13;
                        //ogeSayisi = ogeSayisi == toplamOgeSayisi ? toplamOgeSayisi : ogeSayisi;  
                        //  deger = kosul ? true : false;  
                        //   if (n.Parent != null) cmd.Parameters.Add("@ParentID", parentID); else cmd.Parameters.Add("@ParentID", "0");

                        //   int deger = drv["ParentID"] == null ? -1 : parentID;

                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU, RAPOR_KODU,ID, ParentID,CHILD_INDEX,TEXT,PATH,CHECKS,NAME) VALUES ( @SIRKET_KODU, @RAPOR_KODU,@ID, @ParentID,@CHILD_INDEX,@TEXT,@PATH,@CHECKS,@NAME) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_KIRILIM");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", BR_RAPOR_KODU.Caption.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }

                DW_LIST_KIRLIMLAR.RowStateFilter = DataViewRowState.ModifiedCurrent;

                foreach (DataRowView drv in DW_LIST_KIRLIMLAR)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE  dbo.ADM_{0} SET  PATH=@PATH,ID=@ID,ParentID=@ParentID,CHILD_INDEX=@CHILD_INDEX,CHECKS=@CHECKS,NAME=@NAME,TEXT=@TEXT  where  GUI=@GUI ", "RAPOR_KIRILIM");
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Parameters.AddWithValue("@GUI", drv["GUI"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            DW_LIST_KIRLIMLAR.RowStateFilter = DataViewRowState.CurrentRows;
            DW_LIST_KIRLIMLAR.Table.AcceptChanges();

            /// 
            /// <summary>
            /// DW_LIST_RAPOR_BASLIK 
            /// </summary>  
            ///  
            //// Satır Sil  
            if (DW_LIST_BASLIK != null)
            {
                //DW_LIST_BASLIK.RowStateFilter = DataViewRowState.Deleted;
                //DataRow[] delRows = DW_LIST_BASLIK.Table.Select(null, null, DataViewRowState.Deleted);
                //foreach (DataRow delRow in delRows)
                //{
                //    SqlCommand Cmd = new SqlCommand() { CommandText = "DELETE    dbo.ADM_RAPOR_BASLIK  where  GUI=@GUI" };
                //    Cmd.Parameters.AddWithValue("@GUI", delRow["GUI", DataRowVersion.Original].ToString());
                //    Cmd.Connection = SQLCON;
                //    Cmd.ExecuteNonQuery();
                // //   delRow.Delete();
                //}



                string GUIS = "";
                DW_LIST_BASLIK.RowStateFilter = DataViewRowState.Deleted;
                DataRow[] delRows = DW_LIST_BASLIK.Table.Select(null, null, DataViewRowState.Deleted);
                if (delRows.Length > 0)
                {
                    foreach (DataRow delRow in delRows)
                    {
                        if (delRow["GUI", DataRowVersion.Original].ToString() != "") GUIS += "'" + delRow["GUI", DataRowVersion.Original].ToString() + "',";
                    }
                    GUIS = GUIS.Substring(0, GUIS.Length - 1);
                    SqlCommand Cmd = new SqlCommand() { CommandText = " DELETE FROM dbo.ADM_RAPOR_BASLIK  WHERE GUI IN  ( " + GUIS + " )" };
                    Cmd.Connection = SQLCON;
                    Cmd.ExecuteNonQuery();
                }


                DW_LIST_BASLIK.RowStateFilter = DataViewRowState.Added;
                foreach (DataRowView drv in DW_LIST_BASLIK)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU, RAPOR_KODU,ParentID, CHILD_INDEX,TEXT,NAME) VALUES ( @SIRKET_KODU, @RAPOR_KODU, @ParentID,@CHILD_INDEX,@TEXT,@NAME) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_BASLIK");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", BR_RAPOR_KODU.Caption.ToString());
                        cmd.Parameters.AddWithValue("@ParentID", -1);
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }

                DW_LIST_BASLIK.RowStateFilter = DataViewRowState.ModifiedCurrent;
                foreach (DataRowView drv in DW_LIST_BASLIK)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE  dbo.ADM_{0} SET   CHILD_INDEX=@CHILD_INDEX, NAME=@NAME,TEXT=@TEXT  where  GUI=@GUI ", "RAPOR_BASLIK");
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Parameters.AddWithValue("@GUI", drv["GUI"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            DW_LIST_BASLIK.RowStateFilter = DataViewRowState.CurrentRows;
            DW_LIST_BASLIK.Table.AcceptChanges();

            /// 
            /// <summary>
            /// DW_LIST_RAPOR_OLCUMLER 
            /// </summary>  DW_LIST_RAPOR_FILITRE;
            /// 

            //// Satır Sil


            if (DW_LIST_OLCUMLER != null)
            {
                DW_LIST_OLCUMLER.RowStateFilter = DataViewRowState.Deleted;
                DataRow[] delRows = DW_LIST_OLCUMLER.Table.Select(null, null, DataViewRowState.Deleted);
                foreach (DataRow delRow in delRows)
                {
                    SqlCommand Cmd = new SqlCommand() { CommandText = "DELETE    dbo.ADM_RAPOR_OLCUMLEME  where GUI=@GUI " };
                    //   Cmd.Parameters.AddWithValue("@ID", delRow["ID", DataRowVersion.Original].ToString());
                    Cmd.Parameters.AddWithValue("@GUI", delRow["GUI", DataRowVersion.Original].ToString());
                    Cmd.Connection = SQLCON;
                    Cmd.ExecuteNonQuery();
                    //  delRow.Delete();
                }

                DW_LIST_OLCUMLER.RowStateFilter = DataViewRowState.Added;

                foreach (DataRowView drv in DW_LIST_OLCUMLER)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU, RAPOR_KODU,ParentID, CHILD_INDEX,TEXT,NAME) VALUES ( @SIRKET_KODU, @RAPOR_KODU, @ParentID,@CHILD_INDEX,@TEXT,@NAME) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_OLCUMLEME");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", BR_RAPOR_KODU.Caption.ToString());
                        cmd.Parameters.AddWithValue("@ParentID", -1);
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
                DW_LIST_OLCUMLER.RowStateFilter = DataViewRowState.ModifiedCurrent;
                foreach (DataRowView drv in DW_LIST_OLCUMLER)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE  dbo.ADM_{0} SET   CHILD_INDEX=@CHILD_INDEX, NAME=@NAME,TEXT=@TEXT  where  GUI=@GUI ", "RAPOR_OLCUMLEME");
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Parameters.AddWithValue("@GUI", drv["GUI"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            DW_LIST_OLCUMLER.RowStateFilter = DataViewRowState.CurrentRows;
            DW_LIST_OLCUMLER.Table.AcceptChanges();

            /// 
            /// <summary>
            /// DW_LIST_RAPOR_FILITRE 
            /// </summary>   ;
            /// 
            //// Satır Sil  
            if (DW_LIST_FILITRE != null)
            {
                DW_LIST_FILITRE.RowStateFilter = DataViewRowState.Deleted;
                DataRow[] delRows = DW_LIST_FILITRE.Table.Select(null, null, DataViewRowState.Deleted);
                foreach (DataRow delRow in delRows)
                {
                    SqlCommand Cmd = new SqlCommand() { CommandText = "DELETE    dbo.ADM_RAPOR_FILITRE  where GUI=@GUI " };
                    Cmd.Parameters.AddWithValue("@GUI", delRow["GUI", DataRowVersion.Original].ToString());
                    Cmd.Connection = SQLCON;
                    Cmd.ExecuteNonQuery();
                }

                DW_LIST_FILITRE.RowStateFilter = DataViewRowState.Added;

                foreach (DataRowView drv in DW_LIST_FILITRE)
                {
                    if (SQLCON.State == ConnectionState.Closed) SQLCON.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU, RAPOR_KODU,ID, ParentID,CHILD_INDEX,TEXT,PATH,CHECKS,NAME) VALUES ( @SIRKET_KODU, @RAPOR_KODU,@ID, @ParentID,@CHILD_INDEX,@TEXT,@PATH,@CHECKS,@NAME) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_FILITRE");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", BR_RAPOR_KODU.Caption.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
                DW_LIST_FILITRE.RowStateFilter = DataViewRowState.ModifiedCurrent;
                foreach (DataRowView drv in DW_LIST_FILITRE)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE  dbo.ADM_{0} SET  PATH=@PATH,ID=@ID,ParentID=@ParentID,CHILD_INDEX=@CHILD_INDEX,CHECKS=@CHECKS,NAME=@NAME,TEXT=@TEXT  where  GUI=@GUI ", "RAPOR_FILITRE");
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Parameters.AddWithValue("@GUI", drv["GUI"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            DW_LIST_FILITRE.RowStateFilter = DataViewRowState.CurrentRows;
            DW_LIST_FILITRE.Table.AcceptChanges();



            /// 
            /// <summary>
            /// DW_LIST_RAPOR_FILITRE 
            /// </summary>   ;
            /// 
            //// Satır Sil  
            if (DW_LIST_GENEL_FILITRE != null)
            {
                DW_LIST_GENEL_FILITRE.RowStateFilter = DataViewRowState.Deleted;
                DataRow[] delRows = DW_LIST_GENEL_FILITRE.Table.Select(null, null, DataViewRowState.Deleted);
                foreach (DataRow delRow in delRows)
                {
                    SqlCommand Cmd = new SqlCommand() { CommandText = "DELETE    dbo.ADM_RAPOR_GENEL_FILITRE  where GUI=@GUI " };
                    Cmd.Parameters.AddWithValue("@GUI", delRow["GUI", DataRowVersion.Original].ToString());
                    Cmd.Connection = SQLCON;
                    Cmd.ExecuteNonQuery();
                }

                DW_LIST_GENEL_FILITRE.RowStateFilter = DataViewRowState.Added;

                foreach (DataRowView drv in DW_LIST_GENEL_FILITRE)
                {
                    if (SQLCON.State == ConnectionState.Closed) SQLCON.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU, RAPOR_KODU,ID, ParentID,CHILD_INDEX,TEXT,PATH,CHECKS,NAME) VALUES ( @SIRKET_KODU, @RAPOR_KODU,@ID, @ParentID,@CHILD_INDEX,@TEXT,@PATH,@CHECKS,@NAME) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_GENEL_FILITRE");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", BR_RAPOR_KODU.Caption.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
                DW_LIST_GENEL_FILITRE.RowStateFilter = DataViewRowState.ModifiedCurrent;
                foreach (DataRowView drv in DW_LIST_GENEL_FILITRE)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE  dbo.ADM_{0} SET  PATH=@PATH,ID=@ID,ParentID=@ParentID,CHILD_INDEX=@CHILD_INDEX,CHECKS=@CHECKS,NAME=@NAME,TEXT=@TEXT  where  GUI=@GUI ", "RAPOR_GENEL_FILITRE");
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Parameters.AddWithValue("@GUI", drv["GUI"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            DW_LIST_GENEL_FILITRE.RowStateFilter = DataViewRowState.CurrentRows;
            DW_LIST_GENEL_FILITRE.Table.AcceptChanges();


            SQLCON.Close();
         
            SECENEKLER_LIST(_RAPOR_KODU);
        }



        public void _SAVE_AS(string _RAPOR_KODU, string RAPOR_ACIKLAMASI, string _FIRMA, string _KULLANICI_MAIL)
        {

            string SELECT_MECRATURU = string.Empty;
            if (TOOGLE_TELEVIZYON.IsOn) SELECT_MECRATURU += "TELEVIZYON,";
            if (TOOGLE_GAZETE.IsOn) SELECT_MECRATURU += "GAZETE,";
            if (TOOGLE_DERGI.IsOn) SELECT_MECRATURU += "DERGI,";
            if (TOOGLE_SINEMA.IsOn) SELECT_MECRATURU += "SINEMA,";
            if (TOOGLE_RADYO.IsOn) SELECT_MECRATURU += "RADYO,";
            if (TOOGLE_INTERNET.IsOn) SELECT_MECRATURU += "INTERNET,";
            if (TOOGLE_OUTDOOR.IsOn) SELECT_MECRATURU += "OUTDOOR,";

            if (SELECT_MECRATURU.Length > 0) SELECT_MECRATURU = SELECT_MECRATURU.Remove(SELECT_MECRATURU.Length - 1);

            GENEL_SECENEKLER = ""; MASTER_SELECT = ""; KEYWORD_SELECT = "";
            string SABIT_SECENEKLER = "", RAPOR_SECENEKLER = "";
            DateTime BAS_TARIHI = Convert.ToDateTime(dtBAS_TARIHI.EditValue);
            DateTime BIT_TARIHI = Convert.ToDateTime(dtBIT_TARIHI.EditValue);



            //for (int i = 0; i <= MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1; i++)
            //{
            //    MASTER_SELECT += MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages[i].Name + ";";
            //}

            //for (int i = 0; i <= WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1; i++)
            //{
            //    KEYWORD_SELECT += WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[i].Name + ";";
            //}

            if (BR_IDS.Caption != "")
            {

                //CLASS.DESIGNE_CLASS SAVE = new CLASS.DESIGNE_CLASS();
                //var asd = SAVE._RAPOR_INSERT(_RAPOR_KODU, BAS_TARIHI, BIT_TARIHI,
                //    TrfSablon.TELEVIZYON_TARIFESI.Text, TrfSablon.RADYO_TARIFESI.Text, TrfSablon.GAZETE_TARIFESI.Text,
                //    TrfSablon.LBL_DERGI_TARIFE_KODU.Text, TrfSablon.SINEMA_TARIFESI.Text, TrfSablon.OUTDOOR_TARIFESI.Text, TrfSablon.INTERNET_TARIFESI.Text, TrfSablon.SEKTOR_TARIFESI.Text,
                //    SABIT_SECENEKLER, RAPOR_SECENEKLER, SELECT_MECRATURU, CHKBOX_OTUZSN_GRP.Checked, RAPOR_ACIKLAMASI, MASTER_SELECT, KEYWORD_SELECT,
                //    KIRILIM_SAY, BASLIK_SAY, OLCUM_SAY, FILITRE_SAY
                //    , _FIRMA, _KULLANICI_MAIL);


                //BR_IDS.Caption = Convert.ToString(asd);
                //BR_RAPOR_KODU.Caption = _RAPOR_KODU;

                //PROGRAM_TARIFESI ,NONE_TV_TARIFESI ,ORANLAR_TARIFESI
            }




            for (int iX = 0; iX <= gridView_BASLIKLAR.RowCount - 1; iX++)
            {
                DataRow DR = gridView_BASLIKLAR.GetDataRow(iX);
                DR["CHILD_INDEX"] = iX + 1;
            }

            for (int iX = 0; iX <= gridView_OLCUMLEME.RowCount - 1; iX++)
            {
                DataRow DR = gridView_OLCUMLEME.GetDataRow(iX);
                DR["CHILD_INDEX"] = iX + 1;
            }
            SqlConnection SQLCON = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SQLCON.Open();


            /// <summary>
            /// DW_LIST_ANA_KIRLIM 
            /// </summary>  
            /// 
            //// Satır Sil 
            if (DW_LIST_KIRLIMLAR != null)
            {
                foreach (DataRowView drv in DW_LIST_KIRLIMLAR)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU, RAPOR_KODU,ID, ParentID,CHILD_INDEX,TEXT,PATH,CHECKS,NAME) VALUES ( @SIRKET_KODU, @RAPOR_KODU,@ID, @ParentID,@CHILD_INDEX,@TEXT,@PATH,@CHECKS,@NAME) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_KIRILIM");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", BR_RAPOR_KODU.Caption.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }

            }


            /// 
            /// <summary>
            /// DW_LIST_RAPOR_BASLIK 
            /// </summary>  
            ///  
            //// Satır Sil  
            if (DW_LIST_BASLIK != null)
            {

                foreach (DataRowView drv in DW_LIST_BASLIK)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU, RAPOR_KODU,ParentID, CHILD_INDEX,TEXT,NAME) VALUES ( @SIRKET_KODU, @RAPOR_KODU, @ParentID,@CHILD_INDEX,@TEXT,@NAME) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_BASLIK");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", BR_RAPOR_KODU.Caption.ToString());
                        cmd.Parameters.AddWithValue("@ParentID", -1);
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }


            }


            /// 
            /// <summary>
            /// DW_LIST_RAPOR_OLCUMLER 
            /// </summary>  DW_LIST_RAPOR_FILITRE;
            /// 

            //// Satır Sil


            if (DW_LIST_OLCUMLER != null)
            {


                foreach (DataRowView drv in DW_LIST_OLCUMLER)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU, RAPOR_KODU,ParentID, CHILD_INDEX,TEXT,NAME) VALUES ( @SIRKET_KODU, @RAPOR_KODU, @ParentID,@CHILD_INDEX,@TEXT,@NAME) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_OLCUMLEME");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", BR_RAPOR_KODU.Caption.ToString());
                        cmd.Parameters.AddWithValue("@ParentID", -1);
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }

            }


            /// 
            /// <summary>
            /// DW_LIST_RAPOR_FILITRE 
            /// </summary>   ;
            /// 
            //// Satır Sil  
            if (DW_LIST_FILITRE != null)
            {


                foreach (DataRowView drv in DW_LIST_FILITRE)
                {
                    if (SQLCON.State == ConnectionState.Closed) SQLCON.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU, RAPOR_KODU,ID, ParentID,CHILD_INDEX,TEXT,PATH,CHECKS,NAME) VALUES ( @SIRKET_KODU, @RAPOR_KODU,@ID, @ParentID,@CHILD_INDEX,@TEXT,@PATH,@CHECKS,@NAME) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_FILITRE");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", BR_RAPOR_KODU.Caption.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }

            }




            /// 
            /// <summary>
            /// DW_LIST_RAPOR_FILITRE 
            /// </summary>   ;
            /// 
            //// Satır Sil  
            if (DW_LIST_GENEL_FILITRE != null)
            {


                foreach (DataRowView drv in DW_LIST_GENEL_FILITRE)
                {
                    if (SQLCON.State == ConnectionState.Closed) SQLCON.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU, RAPOR_KODU,ID, ParentID,CHILD_INDEX,TEXT,PATH,CHECKS,NAME) VALUES ( @SIRKET_KODU, @RAPOR_KODU,@ID, @ParentID,@CHILD_INDEX,@TEXT,@PATH,@CHECKS,@NAME) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_GENEL_FILITRE");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", BR_RAPOR_KODU.Caption.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            SQLCON.Close();
            //      TrfSablon.TARIFE_KAYDET();
            //   SECENEKLER_LIST(_RAPOR_KODU);
        }


        private void TOOL_BTN_MASTER_KOPYALA(object sender, EventArgs e)
        {

        }
        public void ZORUNLU_ALANLARI_ISARETLE(string MECRA_TURU)
        {
            string[,] words = null;
            string[,] words_OLCUM = null;

            if (MECRA_TURU == "TELEVIZYON") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "BAŞLANGIÇ"} };
            if (MECRA_TURU == "RADYO") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU", "BAŞLANGIÇ" } };
            if (MECRA_TURU == "GAZETE") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "SAYFA GRUBU", "MEDYA", "ANA YAYIN", "MECRA KODU" } };
            if (MECRA_TURU == "DERGI") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU" } };
            if (MECRA_TURU == "SINEMA") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU", "İLİ", "BÖLGE" } };
            if (MECRA_TURU == "OUTDOOR") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU", "İLİ", "BÖLGE", "ÜNİTE" } };
            if (MECRA_TURU == "INTERNET") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU" } };
            if (MECRA_TURU == "SEKTOR") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU" } };

            if (words != null)
            {
                foreach (string word in words)
                {
                    string strFilters = String.Format(@" TEXT='{0}'", word.ToString());
                    DataRow[] dr = DW_LIST_BASLIK.Table.Select(strFilters);
                    if (dr.Length == 0)
                    {
                        BASLIK_SAY++;
                        DataRowView newRow = DW_LIST_BASLIK.AddNew();
                        newRow["ID"] = BASLIK_SAY;
                        newRow["ParentID"] = -1;
                        newRow["CHILD_INDEX"] = DW_LIST_BASLIK.Count;
                        newRow["PATH"] = word;
                        newRow["TEXT"] = word;
                        newRow["NAME"] = word;
                        newRow.EndEdit();
                    }
                }
            }

          //  if (MECRA_TURU == "TELEVIZYON") words = new string[,] { { "TİME İNT" } };
            // if (MECRA_TURU == "RADYO") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU", "BAŞLANGIÇ" } };
            if (MECRA_TURU == "GAZETE") words_OLCUM = new string[,] { { "EBAT-KUTU", "SANTIM" } };
            // if (MECRA_TURU == "DERGI") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU" } };
            if (MECRA_TURU == "SINEMA") words_OLCUM = new string[,] { { "BIRIM SÜRE" } };
            if (MECRA_TURU == "OUTDOOR") words_OLCUM = new string[,] { { "FREKANS" } };


            if (words_OLCUM != null)
            {
                foreach (string word in words_OLCUM)
                {
                    string strFilters = String.Format(@" TEXT='{0}'", word.ToString());
                    DataRow[] dr = DW_LIST_OLCUMLER.Table.Select(strFilters);
                    if (dr.Length == 0)
                    {
                        BASLIK_SAY++;
                        DataRowView newRow = DW_LIST_OLCUMLER.AddNew();
                        newRow["ID"] = BASLIK_SAY;
                        newRow["ParentID"] = -1;
                        newRow["CHILD_INDEX"] = DW_LIST_OLCUMLER.Count;
                        newRow["PATH"] = word;
                        newRow["TEXT"] = word;
                        newRow["NAME"] = word;
                        newRow.EndEdit();
                    }
                }
            }



        }

        DataRow DROW;
        private void RAPOR_DESIGNER_FormClosed(object sender, FormClosedEventArgs e)
        {
            _MASTERS.RaporDesigner = null;
        }

        private void RAPOR_DESIGNER_Load(object sender, EventArgs e)
        {
            //TreeListNode codeNode = treeList1.AppendNode(null,null);
            //codeNode.SetValue("name", "code");

            //TreeListNode doc = treeList1.AppendNode(null, null);
            //doc.SetValue("name", "doc");

            //TreeListNode video = treeList1.AppendNode(null, null);
            //video.SetValue("name", "video");  
        }
        const string OrderFieldName = "CHILD_INDEX";

        private void BTN_BAS_UP_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = gridView_BASLIKLAR;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index <= 0) return;
            DataRow row1 = view.GetDataRow(index);
            DataRow row2 = view.GetDataRow(index - 1);
            object val1 = row1[OrderFieldName];
            object val2 = row2[OrderFieldName];
            row1[OrderFieldName] = val2;
            row2[OrderFieldName] = val1;
            view.FocusedRowHandle = index - 1;
        }

        private void BTN_BAS_DOWN_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = gridView_BASLIKLAR;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index >= view.DataRowCount - 1) return;
            DataRow row1 = view.GetDataRow(index);
            DataRow row2 = view.GetDataRow(index + 1);
            object val1 = row1[OrderFieldName];
            object val2 = row2[OrderFieldName];
            row1[OrderFieldName] = val2;
            row2[OrderFieldName] = val1;
            view.FocusedRowHandle = index + 1;
        }

        private void MN_LEAF_EKLE_Click(object sender, EventArgs e)
        {

        }
        private void BTN_BAS_SECILEN_SIL_Click(object sender, EventArgs e)
        {
            int[] GETROW = gridView_BASLIKLAR.GetSelectedRows();
            for (int i = GETROW.Length - 1; i >= 0; i--)
            {
                DataRow drs = gridView_BASLIKLAR.GetDataRow(Convert.ToUInt16(GETROW[i]));
                if (drs != null)
                {
                    drs.Delete();
                }
            }
        }

        private void BTN_OLCUM_SECILEN_SIL_Click(object sender, EventArgs e)
        {
            int[] GETROW = gridView_OLCUMLEME.GetSelectedRows();
            for (int i = GETROW.Length - 1; i >= 0; i--)
            {
                DataRow drs = gridView_OLCUMLEME.GetDataRow(Convert.ToUInt16(GETROW[i]));
                if (drs != null)
                {
                    drs.Delete();
                }
            }
        }
        private void SetCheckedChildNodes(TreeListNode node)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                treeList_FILITRELER.Nodes.Remove(node.Nodes[i]);
                SetCheckedChildNodes(node.Nodes[i]);
            }
        }
        private void BTN_OLCUM_UP_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = gridView_OLCUMLEME;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index <= 0) return;
            DataRow row1 = view.GetDataRow(index);
            DataRow row2 = view.GetDataRow(index - 1);
            object val1 = row1[OrderFieldName];
            object val2 = row2[OrderFieldName];
            row1[OrderFieldName] = val2;
            row2[OrderFieldName] = val1;
            view.FocusedRowHandle = index - 1; 
        }
        private void BTN_OLCUM_DOWN_Click(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view = gridView_OLCUMLEME;
            view.GridControl.Focus();
            int index = view.FocusedRowHandle;
            if (index >= view.DataRowCount - 1) return;
            DataRow row1 = view.GetDataRow(index);
            DataRow row2 = view.GetDataRow(index + 1);
            object val1 = row1[OrderFieldName];
            object val2 = row2[OrderFieldName];
            row1[OrderFieldName] = val2;
            row2[OrderFieldName] = val1;
            view.FocusedRowHandle = index + 1;
        }

        private void BTN_FILITRE_UP_Click(object sender, EventArgs e)
        {
            //TreeViews txr = new TreeViews();
            //  txr.UP(treeView_FILITRELER, treeView_FILITRELER);  
        }
        private void BTN_FILITRE_DOWN_Click(object sender, EventArgs e)
        {
          //  TreeViews txr = new TreeViews();
            //  txr.DOWN(treeView_FILITRELER, treeView_FILITRELER);  
        }
        private void MN_LEAF_RENAME_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView_BASLIKLAR.GetDataRow(Convert.ToInt32(gridView_BASLIKLAR.FocusedRowHandle));
            if (dr != null)
            {
                TEXT.DEGISTIR fr = new TEXT.DEGISTIR();
                fr.txt_KIRILIM_ESKI_KOD.Text = dr["TEXT"].ToString();
                fr.ShowDialog();
                dr["TEXT"] = fr._TEXT_NEW;
            }
        } 
   
        private void xtraTabControl_TARIFE_DETAY_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
            //FDR_CM_TARGET.Enabled = false; FDR_BTN_HEDEFKITLE_ATA.Enabled = false;
            //BR_BASLANGIC_TARIHI.Enabled = false; FDR_BTN_BASLANGIC_TARIHI_ATA.Enabled = false;
            //BR_BITIS_TARIHI.Enabled = false; FDR_BTN_BITIS_TARIHI_ATA.Enabled = false;
            //BR_HESAP_TURU.Enabled = false; FDR_BTN_HESAPLAMA_TURU_ATA.Enabled = false;
            //BR_BIRIM_FIYAT.Enabled = false; FDR_BTN_FIYAT_ATA.Enabled = false;
            //BR_BAS_SAATI.Enabled = false; FDR_BTN_BAS_SAAATI_ATA.Enabled = false;
            //BR_BIT_SAATI.Enabled = false; FDR_BTN_BITIS_SAATI_ATA.Enabled = false;
            //if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon" || TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp" || TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            //{
            //    FDR_CM_TARGET.Enabled = true; FDR_BTN_HEDEFKITLE_ATA.Enabled = true;
            //    BR_BASLANGIC_TARIHI.Enabled = true; FDR_BTN_BASLANGIC_TARIHI_ATA.Enabled = true;
            //    BR_BITIS_TARIHI.Enabled = true; FDR_BTN_BITIS_TARIHI_ATA.Enabled = true;
            //    BR_HESAP_TURU.Enabled = true; FDR_BTN_HESAPLAMA_TURU_ATA.Enabled = true;
            //    BR_BIRIM_FIYAT.Enabled = true; FDR_BTN_FIYAT_ATA.Enabled = true;
            //    BR_BAS_SAATI.Enabled = true; FDR_BTN_BAS_SAAATI_ATA.Enabled = true;
            //    BR_BIT_SAATI.Enabled = true; FDR_BTN_BITIS_SAATI_ATA.Enabled = true;
            //}
            //else
            //{
            //    BR_BASLANGIC_TARIHI.Enabled = true;
            //    FDR_BTN_BASLANGIC_TARIHI_ATA.Enabled = true;
            //    BR_BITIS_TARIHI.Enabled = true;
            //    FDR_BTN_BITIS_TARIHI_ATA.Enabled = true;
            //    BR_HESAP_TURU.Enabled = true;
            //    FDR_BTN_HESAPLAMA_TURU_ATA.Enabled = true;
            //    BR_BIRIM_FIYAT.Enabled = true;
            //    FDR_BTN_FIYAT_ATA.Enabled = true;
            //} 
        }
        private TreeListNode GetDragNode(IDataObject data)
        {
            return (TreeListNode)data.GetData(typeof(TreeListNode));
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            TreeList list = (TreeList)sender;
            TreeListNode node = GetDragNode(e.Data);
            if (node != null && node.TreeList != list)
                e.Effect = DragDropEffects.Copy;
        }

        private void OnDragDrop(object sender, DragEventArgs e)
        {
            TreeListNode node = GetDragNode(e.Data);
            if (node == null) return;
            TreeList list = (TreeList)sender;
            TreeListHitInfo info = list.CalcHitInfo(list.PointToClient(new Point(e.X, e.Y)));
            node["ParentID"] = info.Node.ParentNode == null ? -1 : info.Node.ParentNode.GetValue("ID");
            if (list == node.TreeList) return;
            //TreeListHitInfo info = list.CalcHitInfo(list.PointToClient(new Point(e.X, e.Y)));
            //node["ParentID"] = info.Node == null ? -1 : info.Node.Id;
            //  list, node, info.Node == null ? -1 : info.Node.Id
            // InsertBrunch(list, node, info.Node == null ? -1 : info.Node.Id);
        }

        private void InsertBrunch(TreeList list, TreeListNode node, int parent)
        {
            ArrayList data = new ArrayList();
            foreach (TreeListColumn column in node.TreeList.Columns)
                data.Add(node[column]);
            parent = list.AppendNode(data.ToArray(), parent).Id;
            if (node.HasChildren)
                foreach (TreeListNode n in node.Nodes)
                    InsertBrunch(list, n, parent);
        }

        private void MN_ANA_LEAF_EKLE_Click(object sender, EventArgs e)
        {
            KIRILIMLAR.EKLE fr = new KIRILIMLAR.EKLE();
            fr.ShowDialog();
            if (fr._DURUMU == "TAMAM")
            {
                if (fr._TEXT != "")
                {
                    TreeListNode myNode = treeList_KIRILIMLAR.FindNode((node) => { return node["BASLIK"].ToString() == fr._PATH; });
                    if (myNode == null)
                    {
                        KIRILIM_SAY++;
                        DataRowView newRow = DW_LIST_KIRLIMLAR.AddNew();
                        newRow["ID"] = KIRILIM_SAY;
                        newRow["ParentID"] = -1;
                        newRow["TEXT"] = fr._TEXT;
                        newRow["PATH"] = fr._PATH;
                        newRow["CHECKS"] = false;
                        newRow.EndEdit();
                    }
                }
            }
        }

        private void BTN_KIRILIM_UP_Click(object sender, EventArgs e)
        {
          //  TreeViews txr = new TreeViews();
            //  txr.UP(treeView_ANA_TEXT, treeView_ANA_TEXT);  
        }

        private void BTN_KIRILIM_DOWN_Click(object sender, EventArgs e)
        {
           // TreeViews txr = new TreeViews();
            //   txr.DOWN(treeView_ANA_TEXT, treeView_ANA_TEXT);  
        }

        private void BTN_KIRILIM_SECILEN_SIL_Click(object sender, EventArgs e)
        {
            if (BR_RAPOR_TIPI.Caption != "SABIT")
            {
                TreeListNode node = treeList_KIRILIMLAR.FocusedNode;
                if (node.ParentNode != null)
                    node.ParentNode.Nodes.Remove(node);
                else
                    treeList_KIRILIMLAR.Nodes.Remove(node);
            }
            else
            {
                MessageBox.Show("Sabitlenmiş raporlarda değişiklik yapamazsınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public TreeNode m_previousSelectedNode = null;


        private void TXT_BASLIK_FIND_KeyPress(object sender, KeyPressEventArgs e)
        {
            //if (Convert.ToInt32(e.KeyChar) == 13)
            //{ 
            //    TreeNode[] tn = TreeView_Sabitler.Nodes.Find(TXT_BASLIK_FIND.Text, true); 
            //    for (int i = 0; i < tn.Length; i++)
            //    { 
            //        TreeView_Sabitler.SelectedNode = tn[i]; 
            //        TreeView_Sabitler.SelectedNode.BackColor = Color.Yellow; 
            //    }  

            //}
        }



        private void FILITRELER_KONTROL_MOVE_KONTROL(TreeNode treeNode)
        {
            SqlConnection SQLCON = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SQLCON.Open();
            SqlCommand Cmd = new SqlCommand() { CommandText = "DELETE    dbo.ADM_RAPOR_FILITRE  where GUI=@GUI " };
            Cmd.Parameters.AddWithValue("@GUI", treeNode.Tag);
            Cmd.Connection = SQLCON;
            Cmd.ExecuteNonQuery();
            SQLCON.Close();
            treeNode.Tag = null;
            foreach (TreeNode nt in treeNode.Nodes)
            {
                FILITRELER_KONTROL_MOVE_KONTROL(nt);
            }
        }
        private void xtraTabPage2_Paint(object sender, PaintEventArgs e)
        {

        }
        private void dtBAS_TARIHI_EditValueChanged(object sender, EventArgs e)
        {
            _GLOBAL_PARAMETRELER._START_DATE = Convert.ToDateTime(dtBAS_TARIHI.EditValue);
        }
        private void dtBIT_TARIHI_EditValueChanged(object sender, EventArgs e)
        {
            _GLOBAL_PARAMETRELER._END_DATE = Convert.ToDateTime(dtBIT_TARIHI.EditValue);
        }

        private void MN_ANA_LEAF_RENAME_Click(object sender, EventArgs e)
        { 
 
        }


        private void treeList_KIRILIMLAR_Click(object sender, EventArgs e)
        { 
        }
        private void treeList_KIRILIMLAR_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            TreeListNode nod = e.Node;
            if (nod.Checked)
            {
                e.Node["CHECKS"] = false;
            }
            else
            {
                e.Node["CHECKS"] = true;
            }
            string PATH_KIRILIMLAR = GetFullPath(e.Node, "\\");
            PATH_KIRILIMLAR = PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);
            TreeListNode myNodex = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
            if (myNodex != null)
            {
                if (e.Node.Checked)
                {
                    myNodex.SetValue("CHECKS", true);// myNodex["CHECKS"] = false;// node.UncheckAll();
                }
                else
                {
                    myNodex.SetValue("CHECKS", false);// myNodex["CHECKS"] = true;// node.CheckAll();
                }
            }
        }

        private void treeList_KIRILIMLAR_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        {

            KIRILIM_NODE = e.Node;
 
        }

        private void treeList_KIRILIMLAR_AfterCheckNode(object sender, NodeEventArgs e)
        {
            //e.Node.SetValue("CHECKS", e.Node.Checked);
 

            //string PATH_KIRILIMLAR = GetFullPath(e.Node, "\\");
            //PATH_KIRILIMLAR = PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);
            //TreeListNode myNodex = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
            //if (myNodex != null)
            //{
            //    if (e.Node.Checked)
            //    {
            //        myNodex.SetValue("CHECKS", true);// myNodex["CHECKS"] = false;// node.UncheckAll();
            //    }
            //    else
            //    {
            //        myNodex.SetValue("CHECKS", false);// myNodex["CHECKS"] = true;// node.CheckAll();
            //    }
            //}





            SetCheckedChildNodes(e.Node, e.Node.CheckState);
            //  SetCheckedParentNodes(e.Node, e.Node.CheckState); 

            e.Node.SetValue("CHECKS", e.Node.Checked);
            e.Node.SetValue("CHECKS", e.Node.Checked);

            string PATH_KIRILIMLAR = GetFullPath(e.Node, "\\");
            PATH_KIRILIMLAR = PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);
            TreeListNode myNodex = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
            if (myNodex != null)
            {
                // e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);

                if (e.Node.Checked)
                {
                    myNodex.Checked = true;
                    myNodex.SetValue("CHECKS", true);// myNodex["CHECKS"] = false;// node.UncheckAll();
                    SetCheckedChildNodes(myNodex, e.Node.CheckState);
                }
                else
                {
                    myNodex.Checked = false;
                    myNodex.SetValue("CHECKS", false);// myNodex["CHECKS"] = true;// node.CheckAll();
                    SetCheckedChildNodes(myNodex, e.Node.CheckState);
                }
            }


        }


        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                node.Nodes[i].CheckState = check;
                node.Nodes[i].SetValue("CHECKS", check);
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }

        private void xtraTabControl_KURALLAR_Click(object sender, EventArgs e)
        {

        }

        private void re_ItemToggleSwitch_SpotTipi_EditValueChanged(object sender, EventArgs e)
        {
            //DataRow dr = gridView_SPOT_TIPI.GetDataRow(gridView_SPOT_TIPI.FocusedRowHandle);
            //dr["DURUM"] = "True";
            //dr.EndEdit(); 
        }

        private void gridView_BASLIKLAR_DoubleClick(object sender, EventArgs e)
        {
            QUERY = "";
            if (treeList_KIRILIMLAR.FocusedNode == null) { MessageBox.Show("Kırılım Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return; };
            DataRow DR = gridView_BASLIKLAR.GetDataRow(gridView_BASLIKLAR.FocusedRowHandle);
            string DAHIL_HARIC = " (Dahil)", _FIELD_NAME = "", _FIELD_KONTROL_NAME = "", GROUP_BY = "";

            if (!TOOGLE_TELEVIZYON.IsOn && !TOOGLE_GAZETE.IsOn && !TOOGLE_DERGI.IsOn && !TOOGLE_OUTDOOR.IsOn && !TOOGLE_SINEMA.IsOn && !TOOGLE_RADYO.IsOn && !TOOGLE_INTERNET.IsOn)
            { MessageBox.Show("Mecra Türü Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return; }

            if (DR != null)
            { 
                _FIELD_NAME = "[" + DR["TEXT"].ToString() + "]";
                string PATH_KIRILIMLAR = GetFullPath(treeList_KIRILIMLAR.FocusedNode, "\\");
                PATH_KIRILIMLAR = PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);
                TreeListNode myNode = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
                if (myNode != null)
                {
                    _FIELD_KONTROL_NAME = ""; GROUP_BY = "";
                    foreach (TreeListNode item in myNode.Nodes)
                    {
                        if (item.GetValue("TYPE").ToString() == "BASLIK")
                        {
                            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                string SQL = string.Format(" SELECT  * FROM         dbo.ADM_SECENEKLER WHERE    (SECENEK = N'{0}')", item.GetValue(0).ToString().Replace("(Dahil)", "").Replace("(Hariç)", ""));
                                SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                                myConnection.Open();
                                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                                while (myReader.Read())
                                {
                                    _FIELD_KONTROL_NAME = "["+myReader["SECENEK"].ToString()+"]";
                                    GROUP_BY += "["+myReader["SECENEK"].ToString() + "],";
                                }
                            }
                        }


                        foreach (TreeListNode txn in item.Nodes)
                        {
                            if (TOGGLE_FILITRE.IsOn)
                            {
                                if (item.GetValue("TEXT").ToString().IndexOf("(Dahil)") != -1) DAHIL_HARIC = "Dahil";
                                if (item.GetValue("TEXT").ToString().IndexOf("(Hariç)") != -1) DAHIL_HARIC = "Hariç";

                                if (txn.GetValue("TYPE").ToString().IndexOf("SATIR") != -1)
                                {
                                    if (DAHIL_HARIC == "Dahil")
                                    {
                                        if (txn.GetValue(1).ToString().IndexOf("%") != -1)
                                        { QUERY += _FIELD_KONTROL_NAME + " LIKE '" + txn.GetValue("TEXT").ToString() + "' OR "; }
                                        else
                                        { QUERY += string.Format("{0}='{1}' OR ", _FIELD_KONTROL_NAME, txn.GetValue("TEXT")); }
                                    }
                                    if (DAHIL_HARIC == "Hariç")
                                    {
                                        if (txn.GetValue(1).ToString().IndexOf("%") != -1)
                                        { QUERY += _FIELD_KONTROL_NAME + " NOT LIKE '" + txn.GetValue("TEXT").ToString() + "' OR "; }
                                        else
                                        { QUERY += _FIELD_KONTROL_NAME + "<>'" + txn.GetValue("TEXT").ToString() + "' OR "; }
                                    }
                                }
                            }
                        }



                        //if (item.GetValue(0).ToString().Replace(" (Dahil)", "").Replace(" (Hariç)", "") == DR["TEXT"].ToString())
                        //{
                        //    foreach (TreeListNode txn in item.Nodes)
                        //    {
                        //        if (TOGGLE_FILITRE.IsOn)
                        //        {
                        //            if (item.GetValue(0).ToString().IndexOf("(Dahil)") != -1)
                        //            {
                        //                if (_FIELD_NAME == _FIELD_KONTROL_NAME)
                        //                {
                        //                    QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                        //                }
                        //                else
                        //                {
                        //                    if (txn.GetValue(0).ToString().IndexOf("%") != -1)
                        //                    {
                        //                        QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                        //                    }
                        //                    else
                        //                    {
                        //                        QUERY += string.Format("{0} = '{1}' OR ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                        //                    }
                        //                }
                        //            }

                        //            if (item.GetValue(0).ToString().IndexOf("(Hariç)") != -1)
                        //            {

                        //                if (_FIELD_NAME == _FIELD_KONTROL_NAME)
                        //                {
                        //                    QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                        //                }
                        //                else
                        //                {
                        //                    if (txn.GetValue(0).ToString().IndexOf("%") != -1)
                        //                    {
                        //                        QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                        //                    }
                        //                    else
                        //                    {
                        //                        QUERY += string.Format("{0} = '{1}' OR ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                        //                    }
                        //                }
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    if (QUERY.Length > 0) QUERY = string.Format("({0})  AND ", QUERY.Substring(0, QUERY.Length - 4));
                }
                GROUP_BY = GROUP_BY + _FIELD_NAME;
                if (QUERY.Length > 0) QUERY = QUERY.Substring(0, QUERY.Length - 4);

                if (_FIELD_NAME != "")
                {


                    FILITRELER.WIEW_FILTER todo = new FILITRELER.WIEW_FILTER("__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "_LNK_" + BR_RAPOR_KODU.Caption, _FIELD_NAME, _FIELD_NAME, QUERY, TOOGLE_TELEVIZYON.IsOn, TOOGLE_GAZETE.IsOn, TOOGLE_DERGI.IsOn, TOOGLE_OUTDOOR.IsOn, TOOGLE_SINEMA.IsOn, TOOGLE_RADYO.IsOn, TOOGLE_INTERNET.IsOn);
                    todo.ShowDialog();

                    dvs = new DataView();
                    dvs = todo.dvSELECT;

                    if (todo._SATIRLAR_NEREYE == "DAHIL") DAHIL_HARIC = " (Dahil)";
                    if (todo._SATIRLAR_NEREYE == "HARIC") DAHIL_HARIC = " (Hariç)";

                    if (dvs == null) return;


                    if (todo._FILITRE_NEREYE == "OZEL")
                    {
                        if (dvs.Count > 0)
                        {
                            if (treeList_KIRILIMLAR.FocusedNode != null)
                            {

                                TreeListNode myNodes = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
                                if (myNodes == null)
                                {
                                    string pkat = PATH_KIRILIMLAR.ToString();
                                    string[] Onsx = pkat.Split('\\');
                                    if (Onsx[Onsx.Length - 1].ToString() != string.Empty)
                                    {
                                        int prntID = 0;
                                        PATH_KIRILIMLAR = Onsx[0];
                                        for (int i = 0; i < Onsx.Length; i++)
                                        {
                                            if (PATH_KIRILIMLAR != Onsx[i]) PATH_KIRILIMLAR += "\\" + Onsx[i];
                                            TreeListNode myNod = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
                                            if (myNod == null)
                                            {
                                                FILITRE_SAY++;
                                                DataRowView newRow = DW_LIST_FILITRE.AddNew();
                                                newRow["ID"] = FILITRE_SAY;
                                                newRow["ParentID"] = prntID;
                                                newRow["TYPE"] = "KIRILIM";
                                                newRow["TEXT"] = Onsx[i];
                                                newRow["CHECKS"] = true;
                                                newRow["PATH"] = string.Format("KIRILIM#{0}", PATH_KIRILIMLAR);
                                                newRow.EndEdit();
                                                prntID = (Int32)newRow["ID"];
                                            }
                                            else
                                            {
                                                prntID = (Int32)myNod["ID"];
                                            }
                                        }
                                    }
                                }


                                TreeListNode myNodex = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
                                if (myNodex != null)
                                {
                                    TreeListNode Header = myNodex.TreeList.FindNode((node) => { return node["PATH"].ToString() == "BASLIK#" + PATH_KIRILIMLAR + "\\" + DR["TEXT"].ToString() + DAHIL_HARIC; });
                                    if (Header == null)
                                    {
                                        FILITRE_SAY++;
                                        DataRowView newParent = DW_LIST_FILITRE.AddNew();
                                        newParent["ID"] = FILITRE_SAY;
                                        newParent["ParentID"] = myNodex["ID"];
                                        newParent["TEXT"] = string.Format("{0}{1}", DR["TEXT"].ToString(), DAHIL_HARIC);
                                        newParent["TYPE"] = "BASLIK";
                                        newParent["NAME"] = DR["TEXT"];
                                        newParent["CHECKS"] = true;
                                        newParent["PATH"] = string.Format("BASLIK#{0}\\{1}{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString(), DAHIL_HARIC);
                                        newParent.EndEdit();
                                        for (int xi = dvs.Count - 1; xi >= 0; xi--)
                                        {
                                            FILITRE_SAY++;
                                            DataRowView newRow = DW_LIST_FILITRE.AddNew();
                                            newRow["ID"] = FILITRE_SAY;
                                            newRow["ParentID"] = newParent["ID"];
                                            newRow["CHECKS"] = true;
                                            newRow["TEXT"] = dvs.Table.Rows[xi][6].ToString();
                                            newRow["TYPE"] = "SATIR";
                                            newRow["PATH"] = string.Format("SATIR#{0}\\{1}\\{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString() + DAHIL_HARIC, dvs.Table.Rows[xi][6]);
                                            newRow.EndEdit();
                                        }
                                    }
                                    else
                                    {
                                        for (int xi = dvs.Count - 1; xi >= 0; xi--)
                                        {
                                            FILITRE_SAY++;
                                            DataRowView newRow = DW_LIST_FILITRE.AddNew();
                                            newRow["ID"] = FILITRE_SAY;
                                            newRow["CHECKS"] = true;
                                            newRow["ParentID"] = Header["ID"];
                                            newRow["TEXT"] = dvs.Table.Rows[xi][6].ToString();
                                            newRow["TYPE"] = "SATIR";
                                            newRow["CHECKS"] = true;
                                            newRow["PATH"] = string.Format("SATIR#{0}\\{1}\\{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString() + DAHIL_HARIC, dvs.Table.Rows[xi][6]);
                                            newRow.EndEdit();
                                        }
                                    }
                                }
                            }
                        }
                    }

                   
                }
            }
        }

        private void gridControl_OLCUMLEME_Click(object sender, EventArgs e)
        {

        }

        private void MN_OZEL_FILITRE_EKLE_Click(object sender, EventArgs e)
        {
            FILITRELER.OZEL_FILITRE fr = new FILITRELER.OZEL_FILITRE();
            fr.ShowDialog();
            if (fr._DURUMU == "TAMAM")
            {
                if (fr._TEXT != "")
                {

                    fr._TEXT = fr._TEXT.Replace(" ", "").Replace("Ş", "S").Replace("İ", "I");


                    if (treeList_KIRILIMLAR.FocusedNode == null) { MessageBox.Show("Kırılım Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return; };

                    DataRow DR = gridView_BASLIKLAR.GetDataRow(gridView_BASLIKLAR.FocusedRowHandle);
                    string DAHIL_HARIC = fr._DAHIL_HARIC;
                    string _FIELD_NAME = "", _FIELD_KONTROL_NAME = "", GROUP_BY = "";
                    QUERY = "";

                    if (!TOOGLE_TELEVIZYON.IsOn && !TOOGLE_GAZETE.IsOn && !TOOGLE_DERGI.IsOn && !TOOGLE_OUTDOOR.IsOn && !TOOGLE_SINEMA.IsOn && !TOOGLE_RADYO.IsOn && !TOOGLE_INTERNET.IsOn)
                    { MessageBox.Show("Mecra Türü Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return; }


                    if (DR != null)
                    {
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            string SQL = string.Format(" SELECT  [FIELDS] FROM         dbo.ADM_SECENEKLER WHERE    (SECENEK = N'{0}')", DR["TEXT"].ToString());
                            SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                            myConnection.Open();
                            SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                            while (myReader.Read())
                            {
                                _FIELD_NAME = myReader["FIELDS"].ToString();
                            }
                        }

                        string PATH_KIRILIMLAR = GetFullPath(treeList_KIRILIMLAR.FocusedNode, "\\");
                        PATH_KIRILIMLAR = PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);


                        TreeListNode myNode = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
                        if (myNode != null)
                        {
                            _FIELD_KONTROL_NAME = ""; GROUP_BY = "";
                            foreach (TreeListNode item in myNode.Nodes)
                            {

                                if (item.GetValue("NAME").ToString().IndexOf("BASLIK-") != -1)
                                {
                                    using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                                    {
                                        string SQL = string.Format(" SELECT  [FIELDS] FROM         dbo.ADM_SECENEKLER WHERE    (SECENEK = N'{0}')", item.GetValue(0).ToString().Replace("(Dahil)", "").Replace("(Hariç)", ""));
                                        SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                                        myConnection.Open();
                                        SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                                        while (myReader.Read())
                                        {
                                            _FIELD_KONTROL_NAME = myReader["FIELDS"].ToString();
                                            GROUP_BY += myReader["FIELDS"].ToString() + ",";
                                        }
                                    }
                                }

                                if (item.GetValue(0).ToString().Replace(" (Dahil)", "").Replace(" (Hariç)", "") == DR["TEXT"].ToString())
                                {
                                    foreach (TreeListNode txn in item.Nodes)
                                    {
                                        if (TOGGLE_FILITRE.IsOn)
                                        {
                                            if (item.GetValue(0).ToString().IndexOf("(Dahil)") != -1)
                                            {
                                                if (_FIELD_NAME == _FIELD_KONTROL_NAME)
                                                {
                                                    QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                                                }
                                                else
                                                {
                                                    if (txn.GetValue(0).ToString().IndexOf("%") != -1)
                                                    {
                                                        QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                                                    }
                                                    else
                                                    {
                                                        QUERY += string.Format("{0} = '{1}' OR ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                                                    }

                                                }
                                            }

                                            if (item.GetValue(0).ToString().IndexOf("(Hariç)") != -1)
                                            {

                                                if (_FIELD_NAME == _FIELD_KONTROL_NAME)
                                                {
                                                    QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                                                }
                                                else
                                                {
                                                    if (txn.GetValue(0).ToString().IndexOf("%") != -1)
                                                    {
                                                        QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                                                    }
                                                    else
                                                    {
                                                        QUERY += string.Format("{0} = '{1}' OR ", _FIELD_KONTROL_NAME, txn.GetValue(0));
                                                    }
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (QUERY.Length > 0) QUERY = string.Format("({0})  AND ", QUERY.Substring(0, QUERY.Length - 4));
                        }
                        GROUP_BY = GROUP_BY + _FIELD_NAME;
                        if (QUERY.Length > 0) QUERY = QUERY.Substring(0, QUERY.Length - 4);

                        if (_FIELD_NAME != "")
                        {

                            if (treeList_KIRILIMLAR.FocusedNode != null)
                            {
                                TreeListNode myNodes = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
                                if (myNodes == null)
                                {
                                    string pkat = PATH_KIRILIMLAR.ToString();
                                    string[] Onsx = pkat.Split('\\');
                                    if (Onsx[Onsx.Length - 1].ToString() != string.Empty)
                                    {
                                        int prntID = 0;
                                          PATH_KIRILIMLAR = Onsx[0];
                                        for (int i = 0; i < Onsx.Length; i++)
                                        {
                                            if (PATH_KIRILIMLAR != Onsx[i]) PATH_KIRILIMLAR += "\\" + Onsx[i];
                                            TreeListNode myNod = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
                                            if (myNod == null)
                                            {
                                                FILITRE_SAY++;
                                                DataRowView newRow = DW_LIST_FILITRE.AddNew();
                                                newRow["ID"] = FILITRE_SAY;
                                                newRow["ParentID"] = prntID;
                                                newRow["TEXT"] = Onsx[i];
                                                newRow["PATH"] = PATH_KIRILIMLAR;
                                                newRow["NAME"] = string.Format("KIRILIM#{0}", PATH_KIRILIMLAR);
                                                newRow.EndEdit();
                                                prntID = (Int32)newRow["ID"];
                                            }
                                            else
                                            {
                                                prntID = (Int32)myNod["ID"];
                                            }

                                        }
                                    }
                                }

                                TreeListNode myNodex = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
                                if (myNodex != null)
                                {
                                    TreeListNode Header = myNodex.TreeList.FindNode((node) => { return node["PATH"].ToString() == "BASLIK#" + PATH_KIRILIMLAR + "\\" + DR["TEXT"].ToString() + DAHIL_HARIC; });

                                    if (Header == null)
                                    {
                                        FILITRE_SAY++;
                                        DataRowView newParent = DW_LIST_FILITRE.AddNew();
                                        newParent["ID"] = FILITRE_SAY;
                                        newParent["ParentID"] = myNodex["ID"];
                                        newParent["TEXT"] = string.Format("{0}{1}", DR["TEXT"].ToString(), DAHIL_HARIC);// treeList_BASLIKLAR.FocusedNode.GetValue(0) + DAHIL_HARIC;
                                        newParent["PATH"] = string.Format("{0}\\{1}{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString(), DAHIL_HARIC);// PATH_KIRILIMLAR;
                                        newParent["NAME"] = string.Format("BASLIK#{0}\\{1}{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString(), DAHIL_HARIC);
                                        newParent.EndEdit();

                                        FILITRE_SAY++;
                                        DataRowView newRow = DW_LIST_FILITRE.AddNew();
                                        newRow["ID"] = FILITRE_SAY;
                                        newRow["ParentID"] = newParent["ID"];
                                        newRow["TEXT"] = fr._TEXT;//treeList_BASLIKLAR.FocusedNode.GetValue(0)  + 
                                        newRow["PATH"] = string.Format("{0}\\{1}\\{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString() + DAHIL_HARIC, fr._TEXT);// PATH_KIRILIMLAR;
                                        newRow["NAME"] = string.Format("SATIR#{0}\\{1}", PATH_KIRILIMLAR, fr._TEXT);
                                        newRow.EndEdit();

                                    }
                                    else
                                    {
                                        FILITRE_SAY++;
                                        DataRowView newRow = DW_LIST_FILITRE.AddNew();
                                        newRow["ID"] = FILITRE_SAY;
                                        newRow["ParentID"] = Header["ID"];
                                        newRow["TEXT"] = fr._TEXT;
                                        newRow["PATH"] = string.Format("{0}\\{1}\\{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString() + DAHIL_HARIC, fr._TEXT);//PATH_KIRILIMLAR;
                                        newRow["NAME"] = string.Format("SATIR-{0}\\{1}", PATH_KIRILIMLAR, fr._TEXT);
                                        newRow.EndEdit();
                                    }
                                }
                            }
                        }
                    }

                }
            }
        }

        private void toggleSwitch1_Toggled(object sender, EventArgs e)
        {

        }

        private void TOGGLE_WORD_Toggled(object sender, EventArgs e)
        {

        }

        private void BTN_GENEL_FILITRE_Click(object sender, EventArgs e)
        {
            //TreeListNode node = treeList_GENEL_FILITRELER.FocusedNode;
            //if (node == null) return;
            //if (node.HasChildren)
            //{ 
            //    node.ParentNode.Nodes.Remove(node);
            //}
            //else
            //{ treeList_GENEL_FILITRELER.Nodes.Remove(node); }
        }

        private void BTN_TARIFE_ATA_Click(object sender, EventArgs e)
        {
          
        }

        private void BTN_BAS_TARIHI_ATA_Click(object sender, EventArgs e)
        {
           
        }

        private void BTN_BITIS_TARIHI_ATA_Click(object sender, EventArgs e)
        {
           
        }

        private void BTN_HESAPLAMA_TURU_ATA_Click(object sender, EventArgs e)
        {
            
        }

        private void BTN_FIYAT_ATA_Click(object sender, EventArgs e)
        {
          
        }

        private void BTN_BAS_SAATI_ATA_Click(object sender, EventArgs e)
        {
         
        }

        private void BTN_BITIS_SAATI_ATA_Click(object sender, EventArgs e)
        {
             
        }

        private void TOGGLE_GENEL_FILITRE_EditValueChanged(object sender, EventArgs e)
        {
            //if (TOGGLE_GENEL_FILITRE.IsOn) DCK_GENEL_FILITRE.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible; else DCK_GENEL_FILITRE.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;

        }

        private void BTN_FILITRE_SECILEN_SIL_Click_1(object sender, EventArgs e)
        {
            TreeListNode node = treeList_FILITRELER.FocusedNode;
            if (node == null) return;
            if (node.HasChildren)
            { 
                for (int xi = node.Nodes.Count - 1; xi >= 0; xi--)
                {
                    treeList_FILITRELER.Nodes.Remove(node.Nodes[xi]);
                } 
            }
            else
            { treeList_FILITRELER.Nodes.Remove(node); }
        }

        private void RAPOR_VIEWER_FormClosed(object sender, FormClosedEventArgs e)
        {
            _MASTERS.RaporViewer = null;
        }
    }
}