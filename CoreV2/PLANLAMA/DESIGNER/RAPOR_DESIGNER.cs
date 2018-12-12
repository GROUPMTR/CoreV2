using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System.Collections;
using DevExpress.XtraTreeList.Columns;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using System.ComponentModel;
using System.Linq;

namespace CoreV2.PLANLAMA.DESIGNER
{
    public partial class RAPOR_DESIGNER : DevExpress.XtraEditors.XtraForm
    { 
        /// <summary>
        /// TREEVIEW 
        /// </summary>  
        public DataView DW_LIST_KIRLIMLAR, DW_LIST_BASLIK, DW_LIST_OLCUMLER, DW_LIST_FILITRE, DW_LIST_GENEL_FILITRE;  
        TreeListNode KIRILIM_NODE;

        string SELECT_SABIT_SECENEKLER = "", MASTER_SELECT = "", KEYWORD_SELECT = "", GENEL_SECENEKLER = "";

        int KIRILIM_SAY, BASLIK_SAY,OLCUM_SAY, FILITRE_SAY; 
        DevExpress.XtraGrid.GridControl grdCntrl_ = null;
        DevExpress.XtraGrid.Views.Grid.GridView gridView_ = null; 
        DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit re_WORD_MEMO = null;
        DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit re_FIELD_SELECT = null;
        DevExpress.XtraEditors.Repository.RepositoryItemComboBox re_FIELD_UPDATES = null; 
 
        string QUERY = "";
        DataView dvs;  
        _GLOBAL_PARAMETRELER GLOBAL = new _GLOBAL_PARAMETRELER();

        public _TARIFE_TEMP TrfSablon;
        public _MASTER_TEMP MstrSablon;
        public _KEYWORD_TEMP WordSablon;

        public string _BR_IDS = "", _BR_RAPOR_TIPI = "", _BR_RAPOR_KODU = "",  _FTP_ADRESI = "", _FTP_USERNAME = "", _FTP_PASSWORD = "";
        public bool _FTP_DURUMU = false;

        public RAPOR_DESIGNER(string BR_RAPOR_TIPI,string BR_IDS,string BR_RAPOR_KODU, bool FTP_DURUMU, string FTP_ADRESI, string FTP_USERNAME, string FTP_PASSWORD)
        {
            InitializeComponent();

            _BR_IDS = BR_IDS;
            _BR_RAPOR_TIPI = BR_RAPOR_TIPI;
            _BR_RAPOR_KODU = BR_RAPOR_KODU;

            _FTP_DURUMU = FTP_DURUMU;
            _FTP_ADRESI = FTP_ADRESI;
            _FTP_USERNAME = FTP_USERNAME;
            _FTP_PASSWORD = FTP_PASSWORD;

            // var lst = (this.ParentForm.Controls["BR_RAPOR_TYPE"]);
            //listBox.Items.Add("Test"); 
            //  this.ParentForm.Activate().BR_IDS = "1";

            TREE_LIST("EN"); 
            HEDEFKITLE_LISTESI();
            SECENEKLER_LIST("",0); 
          
            //  treeList_FILITRELER.Appearance.Row.Font = new Font(treeList_FILITRELER.Appearance.Row.Font.Name, treeList_FILITRELER.Appearance.Row.Font.Size-1, treeList_FILITRELER.Appearance.Row.Font.Style);
            treeList_FILITRELER.RowHeight = 17; 
            treeList_FILITRELER.ShowingEditor += new CancelEventHandler(treeList1_ShowingEditor); 
            treeList_GENEL_FILITRELER.RowHeight = 17; 

            TrfSablon = new CoreV2.PLANLAMA.DESIGNER._TARIFE_TEMP();
            MstrSablon = new CoreV2.PLANLAMA.DESIGNER._MASTER_TEMP();
            WordSablon = new CoreV2.PLANLAMA.DESIGNER._KEYWORD_TEMP();

            // xtraTab_TARIFELER
            // 
            xtraTab_TARIFELER.Controls.Add(TrfSablon);
            // 
            // TrfSablon
            // 
            TrfSablon.Dock = System.Windows.Forms.DockStyle.Fill;
            TrfSablon.Location = new System.Drawing.Point(0, 0);
            TrfSablon.Name = "TrfSablon";
            TrfSablon.Size = new System.Drawing.Size(1370, 898);
            TrfSablon.TabIndex = 0;
            TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedPageChanged += new DevExpress.XtraTab.TabPageChangedEventHandler(xtraTabControl_TARIFE_DETAY_SelectedPageChanged);
            
            // 
            // xtraTab_MASTER_MANANGEMENT
            // 
            xtraTab_MASTER_MANANGEMENT.Controls.Add(MstrSablon);
   
            // 
            // MstrSablon
            // 
            MstrSablon.Dock = System.Windows.Forms.DockStyle.Fill;
            MstrSablon.Location = new System.Drawing.Point(0, 0);
            MstrSablon.Name = "MstrSablon";
            MstrSablon.Size = new System.Drawing.Size(1370, 928);
            MstrSablon.TabIndex = 0;

            // 
            // xtraTab_WORD_MASTER
            // 
            xtraTab_WORD_MASTER.Controls.Add(WordSablon); 

            // 
            // WordSablon
            // 
            WordSablon.Dock = System.Windows.Forms.DockStyle.Fill;
            WordSablon.Location = new System.Drawing.Point(0, 0);
            WordSablon.Name = "WordSablon";
            WordSablon.Size = new System.Drawing.Size(1370, 928);
            WordSablon.TabIndex = 0;

            grpCntrl_Manager.SendToBack();
        }

        private void RAPOR_DESIGNER_Load(object sender, EventArgs e)
        {

            DateTime myDTStart = Convert.ToDateTime(DateTime.Now);
            DT_GNL_RPR_BIT_TARIHI.EditValue = myDTStart.ToString("dd.MM.yyyy").ToString();
            DT_BITIS_TARIHI.EditValue = myDTStart.ToString("dd.MM.yyyy").ToString();
            myDTStart = myDTStart.AddDays(-30);
            DT_GNL_RPR_BAS_TARIHI.EditValue = myDTStart.ToString("dd.MM.yyyy").ToString();
            DT_GNL_RPR_BAS_TARIHI.Refresh();
            DT_BASLANGIC_TARIHI.EditValue = myDTStart.ToString("dd.MM.yyyy").ToString();
            DT_BASLANGIC_TARIHI.EditValue = myDTStart.ToString("dd.MM.yyyy").ToString();
            DT_BASLANGIC_TARIHI.Refresh();
        }

        private void HEDEFKITLE_LISTESI()
        {      
            CMB_TARGET.Properties.Items.Clear();
            CMB_TARGET.Properties.Items.Add("");
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            { 
                SqlCommand myCommand = new SqlCommand("SELECT   FIELDS, SECENEK FROM   dbo.ADM_SECENEKLER WHERE   (BASLIK = N'HEDEFKİTLELER')", myConnection)  ;
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReader.Read())
                {           
                    CMB_TARGET.Properties.Items.Add(myReader["SECENEK"].ToString());
                }
            }
        }
        private void BR_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }  

        private void TREE_LIST(string DIL)
        {  
            treeList_Sabitler.BeginUnboundLoad(); 
            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlCommand myCommands = new SqlCommand("SELECT   ROWID, BASLIK FROM   dbo.ADM_SECENEKLER GROUP BY BASLIK, ROWID ORDER BY ROWID;", myConnections);
                myConnections.Open();
                SqlDataReader myReaders = myCommands.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReaders.Read())
                { 
                    TreeListNode rootNode = treeList_Sabitler.AppendNode(new object[] { myReaders["BASLIK"].ToString(), myReaders["BASLIK"].ToString(),"","","","" }, null); 
                    using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        SqlCommand myCommand = new SqlCommand("SELECT *  FROM   dbo.ADM_SECENEKLER where BASLIK=@BASLIK ", myConnection);
                        myCommand.Parameters.AddWithValue("@BASLIK", myReaders["BASLIK"].ToString());
                        myConnection.Open();
                        SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        while (myReader.Read())
                        {
                            TreeListNode rode;
                            rode = treeList_Sabitler.AppendNode(new object[] { myReader["SECENEK"].ToString(), myReader["BASLIK"].ToString(), myReader["FIELDS"].ToString(), myReader["ALT_BILGI"].ToString(), myReader["TYPE"].ToString(),null, myReader["ISLEM_TIPI"].ToString() }, rootNode);
                            //if (myReader["SECENEK_EN"] != DBNull.Value)
                            //{
                            //    if (DIL == "TR") rode = treeList_Sabitler.AppendNode(new object[] { myReader["SECENEK"].ToString(), myReader["BASLIK"].ToString(), myReader["FIELDS"].ToString(), myReader["ALT_BILGI"].ToString(), myReader["TYPE"].ToString() }, rootNode);

                            //    if (DIL == "EN") rode = treeList_Sabitler.AppendNode(new object[] { myReader["SECENEK_EN"].ToString(), myReader["BASLIK"].ToString(), myReader["FIELDS"].ToString(), myReader["ALT_BILGI"].ToString(), myReader["TYPE"].ToString() }, rootNode);
                            //}
                            //else
                            //{
                            //     rode = treeList_Sabitler.AppendNode(new object[] { myReader["SECENEK"].ToString(), myReader["BASLIK"].ToString(), myReader["FIELDS"].ToString(), myReader["ALT_BILGI"].ToString(), myReader["TYPE"].ToString() }, rootNode);
                            //}
                        }
                    }
                }
            }
            treeList_Sabitler.ExpandAll(); 
            treeList_Sabitler.EndUnboundLoad();
        }
        public string GetFullPath(TreeListNode node, string pathSeparator)
        {
            if (node == null) return "";
            string result = "";
            while (node != null)
            {
                result = pathSeparator + node.GetDisplayText("TEXT") + result;
                node = node.ParentNode;
            }
            return result;
        }

        private void treeList_Sabitler_DoubleClick(object sender, EventArgs e)
        {
            if (_BR_RAPOR_TIPI == "SABIT") { MessageBox.Show("Sabitlenmiş raporlarda değişiklik yapamazsınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
             TreeList tree = sender as TreeList;
             TreeListHitInfo hi = tree.CalcHitInfo(tree.PointToClient(Control.MousePosition));
             if(hi.Node != null) 
             {  
                 if (hi.Node.ParentNode == null) return;
                 if (hi.Node.GetValue("TYPE").ToString () == "float")
                    {   
                        int rowHandle = GetRowHandleByColumnValue(gridView_OLCUMLEME, "TEXT", hi.Node.GetValue("BASLIK").ToString());
                        if (rowHandle != GridControl.InvalidRowHandle)
                        {
                            MessageBox.Show("Seçilen alan Ölçümlemeler alanında  mevcut  ( " + hi.Node.GetValue("BASLIK").ToString() + " )");
                        }
                        else
                        {
                            OLCUM_SAY++;
                            DataRowView newRow = DW_LIST_OLCUMLER.AddNew();
                            newRow["ID"] = OLCUM_SAY;
                            newRow["ParentID"] = -1;
                            newRow["CHILD_INDEX"] = DW_LIST_OLCUMLER.Count;
                            newRow["TEXT"] = hi.Node.GetValue("BASLIK").ToString();
                            newRow["ISLEM_TIPI"] = hi.Node.GetValue("ISLEM_TIPI").ToString(); //"Sum";
                            newRow["TYPE"] = hi.Node.GetValue("SECENEK").ToString();
                            newRow.EndEdit();
                        } 
                    }
                    else
                    { 
                        int rowHandle = GetRowHandleByColumnValue(gridView_BASLIKLAR, "TEXT", hi.Node.GetValue("BASLIK").ToString());
                        if (rowHandle != GridControl.InvalidRowHandle)
                        {
                            MessageBox.Show("Seçilen alan Başlıklar alanında  mevcut  ( " + hi.Node.GetValue("BASLIK").ToString()+" )");                            
                        }
                        else 
                        {
                            BASLIK_SAY++; 
                            DataRowView newRow = DW_LIST_BASLIK.AddNew();
                            newRow["ID"] = BASLIK_SAY;
                            newRow["ParentID"] =-1;
                            newRow["CHILD_INDEX"] = DW_LIST_BASLIK.Count;
                            newRow["TEXT"] = hi.Node.GetValue("BASLIK").ToString(); 
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
 
        private void CHEK_CLEAN()
        {
            TOOGLE_TELEVIZYON.IsOn= false;
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
            _BR_RAPOR_TIPI = BR_RAPOR_TIPI;
            _BR_IDS = BR_IDS;
            _BR_RAPOR_KODU = BR_RAPOR_KODU;

            TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Text = ""; TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Tag = "";
            TrfSablon.LBL_NONETV_TARIFE_KODU.Text = ""; TrfSablon.LBL_NONETV_TARIFE_KODU.Tag = "";
            TrfSablon.LBL_RADYO_TARIFE_KODU.Text = ""; TrfSablon.LBL_RADYO_TARIFE_KODU.Tag = "";
            TrfSablon.LBL_GAZETE_TARIFE_KODU.Text = ""; TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag = "";
            TrfSablon.LBL_DERGI_TARIFE_KODU.Text = ""; TrfSablon.LBL_DERGI_TARIFE_KODU.Tag = "";
            TrfSablon.LBL_SINEMA_TARIFE_KODU.Text = ""; TrfSablon.LBL_SINEMA_TARIFE_KODU.Tag = "";
            TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Text = ""; TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Tag = "";
            TrfSablon.LBL_INTERNET_TARIFE_KODU.Text = ""; TrfSablon.LBL_INTERNET_TARIFE_KODU.Tag = "";
            TrfSablon.LBL_SEKTOR_TARIFE_KODU.Text = ""; TrfSablon.LBL_SEKTOR_TARIFE_KODU.Tag = "";
            TrfSablon.LBL_PROGRAM_TARIFE_KODU.Text = ""; TrfSablon.LBL_PROGRAM_TARIFE_KODU.Tag = "";
            TrfSablon.LBL_ORANLAR_TARIFE_KODU.Text = ""; TrfSablon.LBL_ORANLAR_TARIFE_KODU.Tag = ""; 
            TrfSablon.gridControl_TARIFE.DataSource = null; TrfSablon.gridControl_RADYO.DataSource = null;           
            TrfSablon.gridControl_GAZETE.DataSource = null;  TrfSablon.gridControl_DERGI.DataSource = null;          
            TrfSablon.gridControl_SINEMA.DataSource = null; TrfSablon.gridControl_OUTDOOR.DataSource = null;           
            TrfSablon.gridControl_INTERNET.DataSource = null; TrfSablon.gridControl_SEKTOR.DataSource = null;           
            TrfSablon.gridControl_PROGRAM_TARIFESI.DataSource = null; TrfSablon.gridControl_NONE_MEASURED_TV.DataSource = null;           
            TrfSablon.gridControl_ORAN_TARIFESI.DataSource = null; 
            CHEK_CLEAN();
            MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.Clear();   MstrSablon.xtraTabControl_MASTER_MNG_DETAY.Refresh();         
            WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Clear();    WordSablon.xtraTabControl_WORD_MNG_DETAY.Refresh(); 

            SELECT_SABIT_SECENEKLER= MASTER_SELECT = KEYWORD_SELECT ="";
            KIRILIM_SAY=BASLIK_SAY=OLCUM_SAY =FILITRE_SAY=0;
            _FTP_DURUMU = false;
            _FTP_ADRESI = _FTP_USERNAME =_FTP_PASSWORD =""; 
 
            string SELECT_MECRA_TURLERI = "", SELECT_RAPOR_SECENEKLER = "";
            SELECT_SABIT_SECENEKLER = "";
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format(" SELECT  * FROM  dbo.ADM_RAPOR_DESIGNE WHERE    (SIRKET_KODU = N'{0}')   AND (RAPOR_KODU='{1}' AND ID='{2}' and OWNER_MAIL='{3}') ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _BR_RAPOR_KODU, _BR_IDS, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReader.Read())
                {
                      _GLOBAL_PARAMETRELER._RAPOR_ID = _BR_IDS = myReader["ID"].ToString();   
                       TrfSablon.TARIFE_KODU = myReader["TELEVIZYON"].ToString();
                       TrfSablon.TARIFE_ID= myReader["TELEVIZYON_REF"].ToString(); TrfSablon.TARIFE_CHANGE("TELEVIZYON"); 
                       TrfSablon.TARIFE_KODU = myReader["RADYO"].ToString();
                       TrfSablon.TARIFE_ID = myReader["RADYO_REF"].ToString(); TrfSablon.TARIFE_CHANGE("RADYO"); 
                       TrfSablon.TARIFE_KODU = myReader["GAZETE"].ToString();
                       TrfSablon.TARIFE_ID = myReader["GAZETE_REF"].ToString(); TrfSablon.TARIFE_CHANGE("GAZETE"); 
                       TrfSablon.TARIFE_KODU = myReader["DERGI"].ToString();
                       TrfSablon.TARIFE_ID = myReader["DERGI_REF"].ToString(); TrfSablon.TARIFE_CHANGE("DERGI"); 
                       TrfSablon.TARIFE_KODU = myReader["SINEMA"].ToString();
                       TrfSablon.TARIFE_ID = myReader["SINEMA_REF"].ToString(); TrfSablon.TARIFE_CHANGE("SINEMA"); 
                       TrfSablon.TARIFE_KODU = myReader["OUTDOOR"].ToString();
                       TrfSablon.TARIFE_ID = myReader["OUTDOOR_REF"].ToString(); TrfSablon.TARIFE_CHANGE("OUTDOOR"); 
                       TrfSablon.TARIFE_KODU = myReader["INTERNET"].ToString();
                       TrfSablon.TARIFE_ID = myReader["INTERNET_REF"].ToString(); TrfSablon.TARIFE_CHANGE("INTERNET"); 
                       TrfSablon.TARIFE_KODU = myReader["PROGRAM"].ToString();
                       TrfSablon.TARIFE_ID = myReader["PROGRAM_REF"].ToString(); TrfSablon.TARIFE_CHANGE("PROGRAM"); 
                       TrfSablon.TARIFE_KODU = myReader["SEKTOR"].ToString();
                       TrfSablon.TARIFE_ID = myReader["SEKTOR_REF"].ToString(); TrfSablon.TARIFE_CHANGE("SEKTOR"); 
                       TrfSablon.TARIFE_KODU = myReader["NONETV"].ToString();
                       TrfSablon.TARIFE_ID = myReader["NONETV_REF"].ToString(); TrfSablon.TARIFE_CHANGE("NONEGRP");  
                       SELECT_SABIT_SECENEKLER = myReader["SABIT_SECENEKLER"].ToString();
                       SELECT_MECRA_TURLERI = myReader["MECRA_TURLERI"].ToString();
                       SELECT_RAPOR_SECENEKLER = myReader["RAPOR_SECENEKLER"].ToString();  
                       MASTER_SELECT = myReader["MASTER_SELECT"].ToString();
                       KEYWORD_SELECT = myReader["KEYWORD_SELECT"].ToString();
                       KIRILIM_SAY =(int)myReader["KIRILIM_SAY"];
                       BASLIK_SAY = (int)myReader["BASLIK_SAY"];
                       OLCUM_SAY = (int)myReader["OLCUM_SAY"];
                       FILITRE_SAY = (int)myReader["FILITRE_SAY"];

                    _FTP_DURUMU =  (bool)myReader["FTP_DURUMU"]; 
                    _FTP_ADRESI = myReader["FTP_ADRESI"].ToString();
                    _FTP_USERNAME =  myReader["FTP_USERNAME"].ToString();
                    _FTP_PASSWORD =  myReader["FTP_PASSWORD"].ToString();

                    if (myReader["OTUZ_SN_GRP"].ToString() == "True") CHKBOX_OTUZSN_GRP.Checked = true; else CHKBOX_OTUZSN_GRP.Checked = false; 
                    if (myReader["BAS_TARIHI"] != DBNull.Value)
                    {
                        DateTime BAS_TARIHI = Convert.ToDateTime(myReader["BAS_TARIHI"].ToString());
                        DateTime BIT_TARIHI = Convert.ToDateTime(myReader["BIT_TARIHI"].ToString());
                        DT_GNL_RPR_BAS_TARIHI.EditValue = BAS_TARIHI.ToString("dd.MM.yyyy").ToString();
                        DT_GNL_RPR_BIT_TARIHI.EditValue = BIT_TARIHI.ToString("dd.MM.yyyy").ToString(); 
                        _GLOBAL_PARAMETRELER._START_DATE =  Convert.ToDateTime(DT_GNL_RPR_BAS_TARIHI.EditValue);
                        _GLOBAL_PARAMETRELER._END_DATE =  Convert.ToDateTime(DT_GNL_RPR_BIT_TARIHI.EditValue);
                    }
                }
            }   
            SECENEKLER_LIST(_BR_RAPOR_KODU, Convert.ToInt32(_GLOBAL_PARAMETRELER._RAPOR_ID)); 
            string[] wordm = SELECT_MECRA_TURLERI.ToString().Split(',');
            foreach (string word in wordm)
            {
                if (word.Trim() == "TELEVIZYON") TOOGLE_TELEVIZYON.IsOn = true;
                if (word.Trim() == "GAZETE") TOOGLE_GAZETE.IsOn = true;
                if (word.Trim() == "DERGI") TOOGLE_DERGI.IsOn = true;
                if (word.Trim() == "SINEMA") TOOGLE_SINEMA.IsOn = true;
                if (word.Trim() == "RADYO") TOOGLE_RADYO.IsOn = true;
                if (word.Trim() == "INTERNET") TOOGLE_INTERNET.IsOn = true;
                if (word.Trim() == "OUTDOOR") TOOGLE_OUTDOOR.IsOn = true; 
            } 
           string[] Masters = MASTER_SELECT.ToString().Split(';');
           foreach (string Mast in Masters)
            {
                if (Mast != "")
                {
                    using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string SQL = string.Format("SELECT * FROM  dbo.TRF_TARIFELER_LISTESI where   SIRKET_KODU='{0}'   AND (TARIFE_KODU='{1}'  and TARIFE_OWNER='{2}'   ) order by MECRA_TURU ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, Mast,_GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                        myConnection.Open();
                        SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        while (myReader.Read())
                        {
                            MstrSablon.MASTER_TABLE_READ(Mast, myReader["FILITRE_TEXT"].ToString(), myReader["ID"].ToString(), myReader["BASLIKLAR"].ToString(), myReader["MECRA_TURU"].ToString());
                        }
                    }
                }
            } 
            string[] words = KEYWORD_SELECT.ToString().Split(';');
            foreach (string word in words)
            {
                if (word != "") KEYWORD_GET(word);
            } 
            TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPageIndex = 0; 
        } 

        public void WORD_SECENEKLER_LIST(string RAPOR_KODU)
        {
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format("   SELECT ID ,ParentID, TEXT, CHILD_INDEX,GUI,CHECKS FROM   dbo.ADM_RAPOR_BASLIK    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}' order by ParentID,CHILD_INDEX", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU);
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


        private void KEYWORD_GET(string _TARIFE_KODU)
        {
            string _BASLIKLAR = "", _ID = "";
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format("SELECT * FROM  dbo.TRF_TARIFELER_LISTESI where   SIRKET_KODU='{0}'   AND (TARIFE_KODU='{1}'    ) order by MECRA_TURU ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU);
                SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReader.Read())
                {
                    _ID = myReader["ID"].ToString();
                    _BASLIKLAR = myReader["BASLIKLAR"].ToString();
                }
            }  
            string TAB_VARMI = "YOK";
            int TabCount = 0;
            for (int i = 0; i < WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count; i++)
            {
                if (WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[i].Name == _TARIFE_KODU) { TAB_VARMI = "VAR"; TabCount = i; }
            } 

            if (TAB_VARMI == "YOK")
            {
                grdCntrl_ = new DevExpress.XtraGrid.GridControl();
                gridView_ = new DevExpress.XtraGrid.Views.Grid.GridView();
                re_FIELD_SELECT = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
                re_WORD_MEMO = new DevExpress.XtraEditors.Repository.RepositoryItemMemoEdit();
                re_FIELD_UPDATES = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();

                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(string.Format("SELECT   ID, [ALANLAR],[KEYWORDS],[ALAN]  ,[DEGER]   FROM  dbo.TRF_KEYWORD  WHERE   TARIFE_KODU='{0}' and SIRKET_KODU='{1}'", _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA), conn);
 
                    adapter.Fill(ds, "TRF_MASTER");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                    grdCntrl_.DataSource = DW_LIST_TARIFE;
                }
                WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Add("");
                WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1].Controls.Add(grdCntrl_);
                WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1].Name = _TARIFE_KODU;
                WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1].Text = _TARIFE_KODU;
                WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1].Tag = _ID;
      

               // WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1].ContextMenuStrip = CNMN_TARIFE;
               //  xtraTabControl_MASTER_MNG_DETAY.TabPages[xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1].ContextMenuStrip = CNMN_TARIFE;
               // 
               // grdCntrl_List
               // 
                grdCntrl_.Dock = DockStyle.Fill;
                grdCntrl_.Location = new System.Drawing.Point(0, 0);
                grdCntrl_.MainView = gridView_;
                //grdCntrl_.MenuManager = this.barManagers;

                ContextMenuStrip CMS_ = (ContextMenuStrip)WordSablon.Controls["contextMenu_TELEVIZYON"];

                grdCntrl_.ContextMenuStrip = CMS_;

                grdCntrl_.Name = _TARIFE_KODU;//_FIELD_NAME;
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
                gridView_.Name = _TARIFE_KODU;///_FIELD_NAME; 
                gridView_.OptionsView.ShowGroupPanel = false;
                //gridView_.OptionsBehavior.Editable = false;
                gridView_.OptionsView.ColumnAutoWidth = true;
                gridView_.OptionsSelection.MultiSelect = true;
                gridView_.RowHeight = 100; 
                gridView_.Columns["ALANLAR"].ColumnEdit = re_FIELD_SELECT;
                gridView_.Columns["ALAN"].ColumnEdit = re_FIELD_UPDATES;
                gridView_.Columns["KEYWORDS"].ColumnEdit = re_WORD_MEMO; 
                gridView_.Columns["ID"].Visible = false;
                WORD_SECENEKLER_LIST(_GLOBAL_PARAMETRELER._RAPOR_KODU); 
            }
            else
            {
                var rtb = WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[TabCount].Controls[0];
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter(string.Format("SELECT ID,[ALANLAR],[KEYWORDS],[ALAN],[DEGER]  FROM  dbo.TRF_KEYWORD  WHERE   TARIFE_KODU='{0}' and SIRKET_KODU='{1}'", _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA), conn); 
                    adapter.Fill(ds, "TRF_MASTER");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                    DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
                    grd_.DataSource = DW_LIST_TARIFE;
                    gridView_.Columns["ID"].Visible = false;
                }
            } 
        }

        public void SECENEKLER_LIST(string RAPOR_KODU, int ID)
        { 
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    DataSet dsKr = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format(" SELECT ID ,ParentID, CHILD_INDEX,GUI,CHECKS,TEXT,NAME,TYPE,PATH FROM   dbo.ADM_RAPOR_KIRILIM    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}'  AND RAPOR_REF='{2}' AND OWNER_MAIL='{3}' order by ParentID,CHILD_INDEX", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU, ID,_GLOBAL_PARAMETRELER._KULLANICI_MAIL), conn) };                    
                    adapter.Fill(dsKr, "ADM_RAPOR_KIRILIM");
                    DataViewManager dvManager = new DataViewManager(dsKr);
                    DW_LIST_KIRLIMLAR = dvManager.CreateDataView(dsKr.Tables[0]);
                    treeList_KIRILIMLAR.KeyFieldName = "ID";
                    treeList_KIRILIMLAR.ParentFieldName = "ParentID"; 
                    treeList_KIRILIMLAR.DataSource = DW_LIST_KIRLIMLAR; 
                }
                treeList_KIRILIMLAR.ExpandAll();
                treeList_KIRILIMLAR.NodesIterator.DoOperation(new InitialStateTreeListOperation());          
  
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    DataSet dsb = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format(" SELECT ID ,ParentID, CHILD_INDEX,GUI,CHECKS,TEXT,ROWTYPE FROM   dbo.ADM_RAPOR_BASLIK    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}' AND RAPOR_KODU='{1}'  AND RAPOR_REF='{2}' AND OWNER_MAIL='{3}' group by  ID ,ParentID, CHILD_INDEX,GUI,CHECKS,TEXT,ROWTYPE order by ParentID,CHILD_INDEX", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU,ID, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), conn) };
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
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format(" SELECT ID ,ParentID, CHILD_INDEX,GUI,CHECKS,TEXT,ISLEM_TIPI,TYPE  FROM   dbo.ADM_RAPOR_OLCUMLEME    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}'  AND RAPOR_REF='{2}' AND OWNER_MAIL='{3}' group by  ID ,ParentID, CHILD_INDEX,GUI,CHECKS,TEXT,ISLEM_TIPI,TYPE order by ParentID,CHILD_INDEX", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU,ID, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), conn) };
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
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format(" SELECT ID ,ParentID, CHILD_INDEX,GUI,CHECKS,TEXT,NAME,TYPE,PATH FROM   dbo.ADM_RAPOR_FILITRE    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}'  AND RAPOR_REF='{2}' AND OWNER_MAIL='{3}' order by ParentID,PATH ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU, ID, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), conn) };
                    adapter.Fill(dsFil, "ADM_RAPOR_FILITRE");
                    DataViewManager dvManager = new DataViewManager(dsFil);
                    DW_LIST_FILITRE = dvManager.CreateDataView(dsFil.Tables[0]); 
                    treeList_FILITRELER.KeyFieldName = "ID";
                    treeList_FILITRELER.ParentFieldName = "ParentID"; 
                    treeList_FILITRELER.DataSource = DW_LIST_FILITRE; 
                }
                treeList_FILITRELER.ExpandAll(); 
                treeList_FILITRELER.NodesIterator.DoOperation(new InitialStateTreeListOperation());
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    DataSet dsFil = new DataSet();
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format(" SELECT ID ,ParentID, CHILD_INDEX,GUI,CHECKS,TEXT,NAME,TYPE,PATH FROM   dbo.ADM_RAPOR_GENEL_FILITRE    WHERE SIRKET_KODU='{0}' AND RAPOR_KODU='{1}'  AND RAPOR_REF='{2}' AND OWNER_MAIL='{3}'   order by ParentID,PATH ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, RAPOR_KODU, ID, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), conn) };
                    adapter.Fill(dsFil, "ADM_RAPOR_GENEL_FILITRE");
                    DataViewManager dvManager = new DataViewManager(dsFil);
                    DW_LIST_GENEL_FILITRE = dvManager.CreateDataView(dsFil.Tables[0]);
                    treeList_GENEL_FILITRELER.KeyFieldName = "ID";
                    treeList_GENEL_FILITRELER.ParentFieldName = "ParentID";
                    treeList_GENEL_FILITRELER.DataSource = DW_LIST_GENEL_FILITRE; 
                }
               treeList_GENEL_FILITRELER.ExpandAll();
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


        public string _SAVE_UPDATE(string _RAPOR_ID,string _RAPOR_KODU, string RAPOR_ACIKLAMASI, string ExportFileAdresi, string _KULLANICI_FIRMA, string _KULLANICI_MAIL)
        {
            _BR_IDS = _RAPOR_ID;
            string SELECT_MECRATURU = string.Empty;
            if (TOOGLE_TELEVIZYON.IsOn) SELECT_MECRATURU += "TELEVIZYON,";
            if (TOOGLE_GAZETE.IsOn) SELECT_MECRATURU += "GAZETE,";
            if (TOOGLE_DERGI.IsOn) SELECT_MECRATURU += "DERGI,";
            if (TOOGLE_SINEMA.IsOn) SELECT_MECRATURU += "SINEMA,";
            if (TOOGLE_RADYO.IsOn) SELECT_MECRATURU += "RADYO,";
            if (TOOGLE_INTERNET.IsOn) SELECT_MECRATURU += "INTERNET,";
            if (TOOGLE_OUTDOOR.IsOn) SELECT_MECRATURU += "OUTDOOR,";   

            if (SELECT_MECRATURU.Length >0) SELECT_MECRATURU = SELECT_MECRATURU.Remove(SELECT_MECRATURU.Length - 1); 
                  
            GENEL_SECENEKLER = ""; MASTER_SELECT = ""; KEYWORD_SELECT = "";
            string SABIT_SECENEKLER = "",RAPOR_SECENEKLER = "";   
            DateTime BAS_TARIHI = Convert.ToDateTime(DT_GNL_RPR_BAS_TARIHI.EditValue);
            DateTime BIT_TARIHI = Convert.ToDateTime(DT_GNL_RPR_BIT_TARIHI.EditValue);

            SqlConnection SQLCON = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SQLCON.Open(); 

            for (int i = 0; i <= MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.Count-1; i++)
            { 
              MASTER_SELECT += MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages[i].Name + ";"; 
            }
            for (int i = 0; i <= WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1; i++)
            {
              KEYWORD_SELECT += WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[i].Name + ";";
            }
            if (Convert.ToInt32(_BR_IDS) == 0)
            { 
                CLASS.DESIGNE_CLASS SAVE = new CLASS.DESIGNE_CLASS();
                var asd = SAVE._RAPOR_INSERT(_RAPOR_KODU, BAS_TARIHI, BIT_TARIHI,
                         TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Text, TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_NONETV_TARIFE_KODU.Text, TrfSablon.LBL_NONETV_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_RADYO_TARIFE_KODU.Text, TrfSablon.LBL_RADYO_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_GAZETE_TARIFE_KODU.Text, TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_DERGI_TARIFE_KODU.Text, TrfSablon.LBL_DERGI_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_SINEMA_TARIFE_KODU.Text, TrfSablon.LBL_SINEMA_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Text, TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_INTERNET_TARIFE_KODU.Text, TrfSablon.LBL_INTERNET_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_SEKTOR_TARIFE_KODU.Text, TrfSablon.LBL_SEKTOR_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_PROGRAM_TARIFE_KODU.Text, TrfSablon.LBL_PROGRAM_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_ORANLAR_TARIFE_KODU.Text, TrfSablon.LBL_ORANLAR_TARIFE_KODU.Tag.ToString(),
                         SABIT_SECENEKLER, RAPOR_SECENEKLER, SELECT_MECRATURU, CHKBOX_OTUZSN_GRP.Checked, RAPOR_ACIKLAMASI, MASTER_SELECT, KEYWORD_SELECT,
                         KIRILIM_SAY, BASLIK_SAY, OLCUM_SAY, FILITRE_SAY, ExportFileAdresi, _KULLANICI_FIRMA, _KULLANICI_MAIL,
                         _FTP_DURUMU,_FTP_ADRESI,_FTP_USERNAME,_FTP_PASSWORD 
                                           
                     ); 

                if (Convert.ToString(asd) != "0")
                {
                    _RAPOR_ID = Convert.ToString(asd);
                    _BR_RAPOR_KODU = _RAPOR_KODU;
                    _BR_IDS = _RAPOR_ID;
                } 
            }
            else
            {
                int ID = Convert.ToInt32(_BR_IDS);
                CLASS.DESIGNE_CLASS UPDATE = new CLASS.DESIGNE_CLASS();
                UPDATE._RAPOR_UPDATE(_RAPOR_KODU, BAS_TARIHI, BIT_TARIHI,
                        TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Text, TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString(),
                        TrfSablon.LBL_NONETV_TARIFE_KODU.Text, TrfSablon.LBL_NONETV_TARIFE_KODU.Tag.ToString(),
                        TrfSablon.LBL_RADYO_TARIFE_KODU.Text, TrfSablon.LBL_RADYO_TARIFE_KODU.Tag.ToString(),
                        TrfSablon.LBL_GAZETE_TARIFE_KODU.Text, TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag.ToString(),
                        TrfSablon.LBL_DERGI_TARIFE_KODU.Text, TrfSablon.LBL_DERGI_TARIFE_KODU.Tag.ToString(),
                        TrfSablon.LBL_SINEMA_TARIFE_KODU.Text, TrfSablon.LBL_SINEMA_TARIFE_KODU.Tag.ToString(),
                        TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Text, TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Tag.ToString(),
                        TrfSablon.LBL_INTERNET_TARIFE_KODU.Text, TrfSablon.LBL_INTERNET_TARIFE_KODU.Tag.ToString(),
                        TrfSablon.LBL_SEKTOR_TARIFE_KODU.Text, TrfSablon.LBL_SEKTOR_TARIFE_KODU.Tag.ToString(),
                        TrfSablon.LBL_PROGRAM_TARIFE_KODU.Text, TrfSablon.LBL_PROGRAM_TARIFE_KODU.Tag.ToString(),
                        TrfSablon.LBL_ORANLAR_TARIFE_KODU.Text, TrfSablon.LBL_ORANLAR_TARIFE_KODU.Tag.ToString(),
                        SABIT_SECENEKLER, RAPOR_SECENEKLER, SELECT_MECRATURU, CHKBOX_OTUZSN_GRP.Checked, ID, MASTER_SELECT, KEYWORD_SELECT,
                        KIRILIM_SAY, BASLIK_SAY, OLCUM_SAY, FILITRE_SAY, _KULLANICI_FIRMA, _KULLANICI_MAIL, ExportFileAdresi, _FTP_DURUMU, _FTP_ADRESI, _FTP_USERNAME, _FTP_PASSWORD
                  );
            }

            if (Convert.ToInt32(_BR_IDS) != 0)
            {
                SAVE_PROPERTIES("SAVE", _KULLANICI_FIRMA, _KULLANICI_MAIL);
                SQLCON.Close();
                TrfSablon.TARIFE_KAYDET();
                SECENEKLER_LIST(_RAPOR_KODU, Convert.ToInt32(_GLOBAL_PARAMETRELER._RAPOR_ID)); 
            }
            return _RAPOR_ID;
        }

  
        private void SAVE_PROPERTIES(string TYPE,string _KULLANICI_FIRMA, string _KULLANICI_MAIL)
        {
            SqlConnection SQLCON = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SQLCON.Open();
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
             //       SqlCommand Cmd = new SqlCommand() { CommandText = " DELETE FROM dbo.ADM_RAPOR_KIRILIM  WHERE GUI IN  ( " + GUIS + " )" };
                    SqlCommand Cmd = new SqlCommand() { CommandText = string.Format(" DELETE FROM dbo.ADM_RAPOR_KIRILIM  WHERE RAPOR_REF='{0}' AND OWNER_MAIL='{1}' AND GUI IN  ( {2} )", _BR_IDS, _KULLANICI_MAIL, GUIS ) };

                    Cmd.Connection = SQLCON;
                    Cmd.ExecuteNonQuery();
                }

                if (TYPE == "SAVE_AS" || TYPE == "RAPOR_PAYLAS") DW_LIST_KIRLIMLAR.RowStateFilter = DataViewRowState.CurrentRows; else DW_LIST_KIRLIMLAR.RowStateFilter = DataViewRowState.Added;
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

                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU,RAPOR_REF, RAPOR_KODU,ID, ParentID,CHILD_INDEX,TEXT,TYPE,PATH,CHECKS,NAME,OWNER_MAIL) VALUES ( @SIRKET_KODU, @RAPOR_REF,@RAPOR_KODU,@ID, @ParentID,@CHILD_INDEX,@TEXT,@TYPE,@PATH,@CHECKS,@NAME,@OWNER_MAIL) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_KIRILIM");                      
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA); 
                        cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", _BR_RAPOR_KODU.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@TYPE", drv["TYPE"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL); 
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
                DW_LIST_KIRLIMLAR.RowStateFilter = DataViewRowState.ModifiedCurrent;
                foreach (DataRowView drv in DW_LIST_KIRLIMLAR)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE  dbo.ADM_{0} SET  PATH=@PATH,ID=@ID,ParentID=@ParentID,CHILD_INDEX=@CHILD_INDEX,TYPE=@TYPE,CHECKS=@CHECKS,NAME=@NAME,TEXT=@TEXT  where  RAPOR_REF=@RAPOR_REF AND OWNER_MAIL=@OWNER_MAIL AND GUI=@GUI ", "RAPOR_KIRILIM");
                        cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@TYPE", drv["TYPE"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Parameters.AddWithValue("@GUI", drv["GUI"].ToString());
                        cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
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
                string GUIS = "";
                DW_LIST_BASLIK.RowStateFilter = DataViewRowState.Deleted;
                DataRow[] delRows = DW_LIST_BASLIK.Table.Select(null, null, DataViewRowState.Deleted);
                if (delRows.Length > 0)
                {
                    foreach (DataRow delRow in delRows)
                    {
                        if (delRow["GUI", DataRowVersion.Original].ToString() != "") GUIS += string.Format("'{0}',", delRow["GUI", DataRowVersion.Original]);
                    }
                    GUIS = GUIS.Substring(0, GUIS.Length - 1);
                    SqlCommand Cmd = new SqlCommand() { CommandText = string.Format(" DELETE FROM dbo.ADM_RAPOR_BASLIK  WHERE RAPOR_REF='{0}' AND  OWNER_MAIL='{1}' AND GUI IN  ( {2} )", _BR_IDS, _KULLANICI_MAIL, GUIS) };
                    Cmd.Connection = SQLCON;
                    Cmd.ExecuteNonQuery();
                }

                if (TYPE == "SAVE_AS" || TYPE == "RAPOR_PAYLAS") DW_LIST_BASLIK.RowStateFilter = DataViewRowState.CurrentRows; else DW_LIST_BASLIK.RowStateFilter = DataViewRowState.Added; 
                foreach (DataRowView drv in DW_LIST_BASLIK)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU,RAPOR_REF, RAPOR_KODU,ParentID, CHILD_INDEX,TEXT,ROWTYPE,OWNER_MAIL) VALUES ( @SIRKET_KODU, @RAPOR_REF,@RAPOR_KODU, @ParentID,@CHILD_INDEX,@TEXT,@ROWTYPE,@OWNER_MAIL) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_BASLIK");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);              
                        cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", _BR_RAPOR_KODU.ToString());
                        cmd.Parameters.AddWithValue("@ParentID", -1);
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@ROWTYPE", drv["ROWTYPE"].ToString());
                        cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }

                DW_LIST_BASLIK.RowStateFilter = DataViewRowState.ModifiedCurrent;
                foreach (DataRowView drv in DW_LIST_BASLIK)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE  dbo.ADM_{0} SET   CHILD_INDEX=@CHILD_INDEX, TEXT=@TEXT,ROWTYPE=@ROWTYPE  where RAPOR_REF=@RAPOR_REF and OWNER_MAIL=@OWNER_MAIL and GUI=@GUI ", "RAPOR_BASLIK");
                        cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString().Replace(",", "."));
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@ROWTYPE", drv["ROWTYPE"].ToString());
                        cmd.Parameters.AddWithValue("@GUI", drv["GUI"].ToString());
                        cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
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
                    SqlCommand Cmd = new SqlCommand() { CommandText = "DELETE    dbo.ADM_RAPOR_OLCUMLEME  where RAPOR_REF=@RAPOR_REF and OWNER_MAIL=@OWNER_MAIL and GUI=@GUI " };
                    //   Cmd.Parameters.AddWithValue("@ID", delRow["ID", DataRowVersion.Original].ToString());
                    Cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                    Cmd.Parameters.AddWithValue("@GUI", delRow["GUI", DataRowVersion.Original].ToString());
                    Cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
                    Cmd.Connection = SQLCON;
                    Cmd.ExecuteNonQuery();
                    //  delRow.Delete();
                }
                if (TYPE == "SAVE_AS" || TYPE == "RAPOR_PAYLAS") DW_LIST_OLCUMLER.RowStateFilter = DataViewRowState.CurrentRows; else DW_LIST_OLCUMLER.RowStateFilter = DataViewRowState.Added; 
                foreach (DataRowView drv in DW_LIST_OLCUMLER)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU, RAPOR_KODU,RAPOR_REF,ParentID, CHILD_INDEX,TEXT,ISLEM_TIPI,TYPE,OWNER_MAIL) VALUES ( @SIRKET_KODU, @RAPOR_KODU,@RAPOR_REF, @ParentID,@CHILD_INDEX,@TEXT,@ISLEM_TIPI,@TYPE,@OWNER_MAIL) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_OLCUMLEME");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", _BR_RAPOR_KODU.ToString());
                        cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                        cmd.Parameters.AddWithValue("@ParentID", -1);
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@ISLEM_TIPI", drv["ISLEM_TIPI"].ToString());
                        cmd.Parameters.AddWithValue("@TYPE", drv["TYPE"].ToString());
                        cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
                DW_LIST_OLCUMLER.RowStateFilter = DataViewRowState.ModifiedCurrent;
                foreach (DataRowView drv in DW_LIST_OLCUMLER)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE  dbo.ADM_{0} SET   CHILD_INDEX=@CHILD_INDEX,  TEXT=@TEXT ,ISLEM_TIPI=@ISLEM_TIPI,TYPE=@TYPE  where RAPOR_REF=@RAPOR_REF and OWNER_MAIL=@OWNER_MAIL and GUI=@GUI ", "RAPOR_OLCUMLEME");
                        cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@ISLEM_TIPI", drv["ISLEM_TIPI"].ToString());
                        cmd.Parameters.AddWithValue("@TYPE", drv["TYPE"].ToString());
                        cmd.Parameters.AddWithValue("@GUI", drv["GUI"].ToString());
                        cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
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
                    SqlCommand Cmd = new SqlCommand() { CommandText = "DELETE    dbo.ADM_RAPOR_FILITRE  where RAPOR_REF=@RAPOR_REF AND OWNER_MAIL=@OWNER_MAIL and  GUI=@GUI " };
                    Cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                    Cmd.Parameters.AddWithValue("@GUI", delRow["GUI", DataRowVersion.Original].ToString());
                    Cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
                    Cmd.Connection = SQLCON;
                    Cmd.ExecuteNonQuery();
                }
                if (TYPE == "SAVE_AS" || TYPE == "RAPOR_PAYLAS") DW_LIST_FILITRE.RowStateFilter = DataViewRowState.CurrentRows; else DW_LIST_FILITRE.RowStateFilter = DataViewRowState.Added; 
                foreach (DataRowView drv in DW_LIST_FILITRE)
                {
                    if (SQLCON.State == ConnectionState.Closed) SQLCON.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU,RAPOR_REF, RAPOR_KODU,ID, ParentID,CHILD_INDEX,TEXT,TYPE, PATH,CHECKS,NAME,OWNER_MAIL) VALUES ( @SIRKET_KODU,@RAPOR_REF, @RAPOR_KODU,@ID, @ParentID,@CHILD_INDEX,@TEXT,@TYPE,@PATH,@CHECKS,@NAME,@OWNER_MAIL) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_FILITRE");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);                    
                        cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", _BR_RAPOR_KODU.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@TYPE", drv["TYPE"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"].ToString());
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
                DW_LIST_FILITRE.RowStateFilter = DataViewRowState.ModifiedCurrent;
                foreach (DataRowView drv in DW_LIST_FILITRE)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE  dbo.ADM_{0} SET  PATH=@PATH,ID=@ID,ParentID=@ParentID,CHILD_INDEX=@CHILD_INDEX,CHECKS=@CHECKS,NAME=@NAME,TEXT=@TEXT,TYPE=@TYPE  where RAPOR_REF=@RAPOR_REF and OWNER_MAIL=@OWNER_MAIL and GUI=@GUI ", "RAPOR_FILITRE");
                        cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@TYPE", drv["TYPE"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"].ToString());
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Parameters.AddWithValue("@GUI", drv["GUI"].ToString());
                        cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            DW_LIST_FILITRE.RowStateFilter = DataViewRowState.CurrentRows;
            DW_LIST_FILITRE.Table.AcceptChanges();
            /// 
            /// <summary>
            /// DW_LIST_GENEL_FILITRE 
            /// </summary>   ;
            /// 
            //// Satır Sil  
            if (DW_LIST_GENEL_FILITRE != null)
            {
                DW_LIST_GENEL_FILITRE.RowStateFilter = DataViewRowState.Deleted;
                DataRow[] delRows = DW_LIST_GENEL_FILITRE.Table.Select(null, null, DataViewRowState.Deleted);
                foreach (DataRow delRow in delRows)
                {
                    SqlCommand Cmd = new SqlCommand() { CommandText = "DELETE    dbo.ADM_RAPOR_GENEL_FILITRE  where  RAPOR_REF=@RAPOR_REF and OWNER_MAIL=@OWNER_MAIL and  GUI=@GUI " };
                    Cmd.Parameters.AddWithValue("@GUI", delRow["GUI", DataRowVersion.Original].ToString());
                    Cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                    Cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
                    Cmd.Connection = SQLCON;
                    Cmd.ExecuteNonQuery();
                }
                if (TYPE == "SAVE_AS" || TYPE == "RAPOR_PAYLAS") DW_LIST_GENEL_FILITRE.RowStateFilter = DataViewRowState.CurrentRows; else DW_LIST_GENEL_FILITRE.RowStateFilter = DataViewRowState.Added; 
                foreach (DataRowView drv in DW_LIST_GENEL_FILITRE)
                {
                    if (SQLCON.State == ConnectionState.Closed) SQLCON.Open();
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("INSERT INTO dbo.ADM_{0} (SIRKET_KODU,RAPOR_REF, RAPOR_KODU,ID, ParentID,CHILD_INDEX,TEXT,TYPE,PATH,CHECKS,NAME,OWNER_MAIL) VALUES ( @SIRKET_KODU,@RAPOR_REF, @RAPOR_KODU,@ID, @ParentID,@CHILD_INDEX,@TEXT,@TYPE,@PATH,@CHECKS,@NAME,@OWNER_MAIL) SELECT * FROM  ADM_{0}  WHERE ID=@@IDENTITY ", "RAPOR_GENEL_FILITRE");
                        cmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);
                        cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                        cmd.Parameters.AddWithValue("@RAPOR_KODU", _BR_RAPOR_KODU.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@TYPE", drv["TYPE"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
                DW_LIST_GENEL_FILITRE.RowStateFilter = DataViewRowState.ModifiedCurrent;
                foreach (DataRowView drv in DW_LIST_GENEL_FILITRE)
                {
                    using (SqlCommand cmd = new SqlCommand())
                    {
                        cmd.CommandText = string.Format("UPDATE  dbo.ADM_{0} SET  PATH=@PATH,ID=@ID,ParentID=@ParentID,CHILD_INDEX=@CHILD_INDEX,CHECKS=@CHECKS,NAME=@NAME,TEXT=@TEXT,TYPE=@TYPE  where RAPOR_REF=@RAPOR_REF and OWNER_MAIL=@OWNER_MAIL and  GUI=@GUI ", "RAPOR_GENEL_FILITRE");
                        cmd.Parameters.AddWithValue("@RAPOR_REF", _BR_IDS.ToString());
                        cmd.Parameters.AddWithValue("@ID", drv["ID"].ToString());
                        cmd.Parameters.AddWithValue("@ParentID", drv["ParentID"].ToString());
                        cmd.Parameters.AddWithValue("@CHILD_INDEX", drv["CHILD_INDEX"].ToString());
                        cmd.Parameters.AddWithValue("@TEXT", drv["TEXT"].ToString());
                        cmd.Parameters.AddWithValue("@TYPE", drv["TYPE"].ToString());
                        cmd.Parameters.AddWithValue("@PATH", drv["PATH"].ToString());
                        cmd.Parameters.AddWithValue("@CHECKS", drv["CHECKS"]);
                        cmd.Parameters.AddWithValue("@NAME", drv["NAME"].ToString());
                        cmd.Parameters.AddWithValue("@GUI", drv["GUI"].ToString());
                        cmd.Parameters.AddWithValue("@OWNER_MAIL", _KULLANICI_MAIL);
                        cmd.Connection = SQLCON;
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            DW_LIST_GENEL_FILITRE.RowStateFilter = DataViewRowState.CurrentRows;
            DW_LIST_GENEL_FILITRE.Table.AcceptChanges();
        }

        public void _SAVE_AS(string _RAPOR_KODU, string RAPOR_ACIKLAMASI,string _KULLANICI_FIRMA, string _KULLANICI_MAIL, string ExportFileAdresi)
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
            DateTime BAS_TARIHI = Convert.ToDateTime(DT_GNL_RPR_BAS_TARIHI.EditValue);
            DateTime BIT_TARIHI = Convert.ToDateTime(DT_GNL_RPR_BIT_TARIHI.EditValue);        

            for (int i = 0; i <= MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1; i++)
            {
                MASTER_SELECT += MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages[i].Name + ";";
            } 
            for (int i = 0; i <= WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1; i++)
            {
                KEYWORD_SELECT += WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[i].Name + ";";
            }   
            if (Convert.ToInt32(_BR_IDS) != 0)
            {
                CLASS.DESIGNE_CLASS SAVE = new CLASS.DESIGNE_CLASS();
                var asd = SAVE._RAPOR_INSERT(_RAPOR_KODU, BAS_TARIHI, BIT_TARIHI,
                         TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Text, TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_NONETV_TARIFE_KODU.Text, TrfSablon.LBL_NONETV_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_RADYO_TARIFE_KODU.Text, TrfSablon.LBL_RADYO_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_GAZETE_TARIFE_KODU.Text, TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_DERGI_TARIFE_KODU.Text, TrfSablon.LBL_DERGI_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_SINEMA_TARIFE_KODU.Text, TrfSablon.LBL_SINEMA_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Text, TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_INTERNET_TARIFE_KODU.Text, TrfSablon.LBL_INTERNET_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_SEKTOR_TARIFE_KODU.Text, TrfSablon.LBL_SEKTOR_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_PROGRAM_TARIFE_KODU.Text, TrfSablon.LBL_PROGRAM_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_ORANLAR_TARIFE_KODU.Text, TrfSablon.LBL_ORANLAR_TARIFE_KODU.Tag.ToString(),
                         SABIT_SECENEKLER, RAPOR_SECENEKLER, SELECT_MECRATURU, CHKBOX_OTUZSN_GRP.Checked, RAPOR_ACIKLAMASI, MASTER_SELECT, KEYWORD_SELECT,
                         KIRILIM_SAY, BASLIK_SAY, OLCUM_SAY, FILITRE_SAY, ExportFileAdresi, _KULLANICI_FIRMA, _KULLANICI_MAIL,_FTP_DURUMU , _FTP_ADRESI ,_FTP_USERNAME ,_FTP_PASSWORD                          
                     );

                if (Convert.ToInt16(asd) != 0)
                { 
                    _BR_RAPOR_KODU = _RAPOR_KODU;
                    _BR_IDS = Convert.ToString(asd); 
                }

                SAVE_PROPERTIES("SAVE_AS", _KULLANICI_FIRMA, _KULLANICI_MAIL);
            }
           
        }
         
        public void _RAPOR_PAYLAS(string _RAPOR_KODU, string RAPOR_ACIKLAMASI, string _KULLANICI_FIRMA, string _KULLANICI_MAIL, string ExportFileAdresi)
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
            DateTime BAS_TARIHI = Convert.ToDateTime(DT_GNL_RPR_BAS_TARIHI.EditValue);
            DateTime BIT_TARIHI = Convert.ToDateTime(DT_GNL_RPR_BIT_TARIHI.EditValue);

            for (int i = 0; i <= MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.Count - 1; i++)
            {
                MASTER_SELECT += MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages[i].Name + ";";
            }
            for (int i = 0; i <= WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count - 1; i++)
            {
                KEYWORD_SELECT += WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[i].Name + ";";
            }
            if (Convert.ToInt32(_BR_IDS) != 0)
            {
                CLASS.DESIGNE_CLASS SAVE = new CLASS.DESIGNE_CLASS();
                var asd = SAVE._RAPOR_INSERT(_RAPOR_KODU, BAS_TARIHI, BIT_TARIHI,
                         TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Text, TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_NONETV_TARIFE_KODU.Text, TrfSablon.LBL_NONETV_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_RADYO_TARIFE_KODU.Text, TrfSablon.LBL_RADYO_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_GAZETE_TARIFE_KODU.Text, TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_DERGI_TARIFE_KODU.Text, TrfSablon.LBL_DERGI_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_SINEMA_TARIFE_KODU.Text, TrfSablon.LBL_SINEMA_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Text, TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_INTERNET_TARIFE_KODU.Text, TrfSablon.LBL_INTERNET_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_SEKTOR_TARIFE_KODU.Text, TrfSablon.LBL_SEKTOR_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_PROGRAM_TARIFE_KODU.Text, TrfSablon.LBL_PROGRAM_TARIFE_KODU.Tag.ToString(),
                         TrfSablon.LBL_ORANLAR_TARIFE_KODU.Text, TrfSablon.LBL_ORANLAR_TARIFE_KODU.Tag.ToString(),
                         SABIT_SECENEKLER, RAPOR_SECENEKLER, SELECT_MECRATURU, CHKBOX_OTUZSN_GRP.Checked, RAPOR_ACIKLAMASI, MASTER_SELECT, KEYWORD_SELECT,
                         KIRILIM_SAY, BASLIK_SAY, OLCUM_SAY, FILITRE_SAY, ExportFileAdresi, _KULLANICI_FIRMA, _KULLANICI_MAIL,

                         _FTP_DURUMU, _FTP_ADRESI, _FTP_USERNAME, _FTP_PASSWORD
                     );

                if (Convert.ToInt16(asd) != 0)
                {
                    _BR_RAPOR_KODU = _RAPOR_KODU;
                    _BR_IDS = Convert.ToString(asd);
                } 
                SAVE_PROPERTIES("RAPOR_PAYLAS", _KULLANICI_FIRMA, _KULLANICI_MAIL);
            }

        }

        public void ZORUNLU_ALANLARI_ISARETLE(string MECRA_TURU)
        {
            string[,] words = null;
            string[,] words_OLCUM = null;
            if (MECRA_TURU == "TELEVIZYON")
            { 
                if (TrfSablon.gridView_TELEVIZYON.RowCount > 0)
                {
                    for (int iX = 1; iX <= TrfSablon.gridView_TELEVIZYON.RowCount - 1; iX++)
                    {   DataRow dw = TrfSablon.gridView_TELEVIZYON.GetDataRow(iX); 
                        string strFilters = String.Format(@" TEXT='{0}'",dw["TARGET"].ToString()); 
                        DataRow[] dr = DW_LIST_OLCUMLER.Table.Select(strFilters);
                        if (dr.Length == 0)
                        {
                            OLCUM_SAY++;
                            DataRowView newRow = DW_LIST_OLCUMLER.AddNew();
                            newRow["ID"] = OLCUM_SAY;
                            newRow["ParentID"] = -1;
                            newRow["CHILD_INDEX"] = DW_LIST_OLCUMLER.Count; 
                            newRow["TEXT"] = dw["TARGET"].ToString(); 
                            newRow.EndEdit();
                        }
                    }
                }
            }

            if (MECRA_TURU == "NONEGRP")
            {
                if (TrfSablon.gridView_NONE_MEASURED_TELEVIZYON.RowCount > 0)
                {
                    for (int iX = 1; iX <= TrfSablon.gridView_NONE_MEASURED_TELEVIZYON.RowCount - 1; iX++)
                    {
                        DataRow dw = TrfSablon.gridView_NONE_MEASURED_TELEVIZYON.GetDataRow(iX);
                        string strFilters = String.Format(@" TEXT='{0}'", dw["TARGET"].ToString());
                        DataRow[] dr = DW_LIST_OLCUMLER.Table.Select(strFilters);
                        if (dr.Length == 0)
                        {
                            OLCUM_SAY++;
                            DataRowView newRow = DW_LIST_OLCUMLER.AddNew();
                            newRow["ID"] = OLCUM_SAY;
                            newRow["ParentID"] = -1;
                            newRow["CHILD_INDEX"] = DW_LIST_OLCUMLER.Count;
                            newRow["TEXT"] = dw["TARGET"].ToString();
                            newRow.EndEdit();
                        }
                    }
                }
            }

            if (MECRA_TURU == "TELEVIZYON") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "BAŞLANGIÇ"} };
            if (MECRA_TURU == "RADYO") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU", "BAŞLANGIÇ" } };
            if (MECRA_TURU == "GAZETE") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "SAYFA GRUBU", "MEDYA", "ANA YAYIN", "MECRA KODU" } };
            if (MECRA_TURU == "DERGI") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU" } };
            if (MECRA_TURU == "SINEMA") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU", "İLİ", "BÖLGE"  } };
            if (MECRA_TURU == "OUTDOOR") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU", "İLİ", "BÖLGE", "ÜNİTE"} };
            if (MECRA_TURU == "INTERNET") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU"} };
            if (MECRA_TURU == "SEKTOR") words = new string[,] { { "TARİH", "YAYIN TÜRÜ", "YAYIN SINIFI", "ANA YAYIN", "MECRA KODU" } };

            if (words != null)
            {
                foreach (string word in words)
                {
                    string strFilters = String.Format(@" TEXT='{0}'",word.ToString()); 
                    DataRow[] dr = DW_LIST_BASLIK.Table.Select(strFilters);
                    if (dr.Length == 0)
                    { 
                        BASLIK_SAY++;
                        DataRowView newRow = DW_LIST_BASLIK.AddNew();
                        newRow["ID"] = BASLIK_SAY;
                        newRow["ParentID"] = -1;
                        newRow["CHILD_INDEX"] = DW_LIST_BASLIK.Count; 
                        newRow["TEXT"] = word; 
                        newRow.EndEdit();
                    }
                }
            }

            if (MECRA_TURU == "TELEVIZYON") words_OLCUM = new string[,] { { "Tarife" } };
            if (MECRA_TURU == "RADYO") words_OLCUM = new string[,] { { "Tarife" } }; 
            if (MECRA_TURU == "DERGI") words_OLCUM = new string[,] { { "Tarife" } };
            if (MECRA_TURU == "SINEMA") words = new string[,] { { "Tarife" } }; 
            if (MECRA_TURU == "GAZETE") words_OLCUM = new string[,] { { "EBAT-KUTU", "SANTIM", "NET KUTU TABLOID STCM", "Tarife" } }; 
            if (MECRA_TURU == "SINEMA") words_OLCUM = new string[,] { { "BIRIM SÜRE","Tarife" } };
            if (MECRA_TURU == "OUTDOOR") words_OLCUM = new string[,] { { "FREKANS", "Tarife" } }; 
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
                        newRow["TEXT"] = word;
                        newRow["ISLEM_TIPI"] = "Avg";
                        newRow.EndEdit();
                    }
                }
            }  
        }

        private void RAPOR_DESIGNER_FormClosed(object sender, FormClosedEventArgs e)
        {
            _MASTERS.RaporDesigner = null;
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
            KIRILIMLAR.EKLE fr = new KIRILIMLAR.EKLE();
            fr.Text = "ÖZEL KOLON EKLE";
            fr.ShowDialog();
            if (fr._DURUMU == "TAMAM")
            {
                if (fr._TEXT != "")
                {
                    BASLIK_SAY++;
                    DataRowView newRow = DW_LIST_BASLIK.AddNew();
                    newRow["ID"] = BASLIK_SAY;
                    newRow["ParentID"] = -1;
                    newRow["CHILD_INDEX"] = DW_LIST_BASLIK.Count;
                    newRow["TEXT"] = fr._TEXT;//.ToString().Replace("\\","").Replace("%","").Replace("&","").Replace("); 
                    newRow["ROWTYPE"] = "SERBEST"; 
                    newRow.EndEdit(); 
                }
            }  
        }  
        private void BTN_BAS_SECILEN_SIL_Click(object sender, EventArgs e)
        { 
            if (_BR_RAPOR_TIPI != "SABIT")
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
            else
            {
                MessageBox.Show("Sabitlenmiş raporlarda değişiklik yapamazsınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }  
        } 

        private void BTN_OLCUM_SECILEN_SIL_Click(object sender, EventArgs e)
        {
            if (_BR_RAPOR_TIPI != "SABIT")
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
            else
            {
                MessageBox.Show("Sabitlenmiş raporlarda değişiklik yapamazsınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        } 
  
        private void BTN_FILITRE_SECILEN_SIL_Click(object sender, EventArgs e)
        {
            TreeListNode node = treeList_FILITRELER.FocusedNode;
            if (node == null) return;
            if (node.HasChildren)
            {
                for (int xi = node.Nodes.Count - 1; xi >= 0; xi--)
                {
                    treeList_FILITRELER.Nodes.Remove(node.Nodes[xi]);
                }
                if (node.ParentNode == null) return;
                node.ParentNode.Nodes.Remove(node);

             

            //  SetCheckedChildNodes(node.ParentNode);
            //for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
            //{
            //    //state = (CheckState)node.ParentNode.Nodes[i].CheckState;
            //    //if (!check.Equals(state))
            //    //{
            //    //    b = !b;
            //    //    break;
            //    //}
            //}

                //foreach (TreeListNode nde in node.Nodes)
                //{
                //    treeList_FILITRELER.Nodes.Remove(nde);
                //}
                //  node.ParentNode.Nodes.Remove(node);
            }
            else
            { treeList_FILITRELER.Nodes.Remove(node); }
      
        }
        private void SetCheckedChildNodes(TreeListNode node )
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

        private void MN_LEAF_RENAME_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView_BASLIKLAR.GetDataRow(Convert.ToInt32(gridView_BASLIKLAR.FocusedRowHandle));       
            if (dr != null)
            {               
                    TEXT.DEGISTIR fr = new TEXT.DEGISTIR();
                    fr.txt_KIRILIM_ESKI_KOD.Text = dr["TEXT"].ToString();
                    fr.ShowDialog();
                if (fr._TEXT_NEW!=null && fr._TEXT_NEW!="") { dr["TEXT"] = fr._TEXT_NEW; }
            } 
        } 

        private void xtraTabControl_TARIFE_DETAY_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {
           

            CMB_TARGET.Enabled = false; BTN_TARIFE_ATA.Enabled = false;
            DT_BASLANGIC_TARIHI.Enabled = false; BTN_BAS_TARIHI_ATA.Enabled = false;
            DT_BITIS_TARIHI.Enabled = false; BTN_BITIS_TARIHI_ATA.Enabled = false;
            CMB_HESAP_TURU.Enabled = false; BTN_HESAPLAMA_TURU_ATA.Enabled = false;
            TXT_BIRIM_FIYAT.Enabled = false; BTN_FIYAT_ATA.Enabled = false;
            TXT_BAS_SAAT.Enabled = false; BTN_BAS_SAATI_ATA.Enabled = false; 
            TXT_BIT_SAAT.Enabled = false; BTN_BITIS_SAATI_ATA.Enabled = false;
            CMB_HESAP_TURU.Text = "";
            CMB_HESAP_TURU.Properties.Items.Clear();


            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon"  )
            {
                CMB_HESAP_TURU.Properties.Items.Add("Cpp"); CMB_HESAP_TURU.Properties.Items.Add("Süre"); CMB_HESAP_TURU.Properties.Items.Add("Ratecard"); 
            } 
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            {
                CMB_HESAP_TURU.Properties.Items.Add("Süre"); CMB_HESAP_TURU.Properties.Items.Add("Ratecard"); 
            } 
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
            {
                CMB_HESAP_TURU.Properties.Items.Add("Stcm"); CMB_HESAP_TURU.Properties.Items.Add("Kutu"); CMB_HESAP_TURU.Properties.Items.Add("Ratecard");
            } 
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
            {
              CMB_HESAP_TURU.Properties.Items.Add("Sayfa"); CMB_HESAP_TURU.Properties.Items.Add("Ratecard");
            }
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
            {
                CMB_HESAP_TURU.Properties.Items.Add("Frekans"); CMB_HESAP_TURU.Properties.Items.Add("Ratecard");
            }

            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Internet")
            {
                CMB_HESAP_TURU.Properties.Items.Add("Adet"); CMB_HESAP_TURU.Properties.Items.Add("Ratecard");
            }

            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
            {
                CMB_HESAP_TURU.Properties.Items.Add("Süre"); CMB_HESAP_TURU.Properties.Items.Add("Ratecard");
            } 


            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon" || TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp" )
            {
                CMB_TARGET.Enabled = true; BTN_TARIFE_ATA.Enabled = true;
                DT_BASLANGIC_TARIHI.Enabled = true; BTN_BAS_TARIHI_ATA.Enabled = true;
                DT_BITIS_TARIHI.Enabled = true; BTN_BITIS_TARIHI_ATA.Enabled = true;
                CMB_HESAP_TURU.Enabled = true; BTN_HESAPLAMA_TURU_ATA.Enabled = true;
                TXT_BIRIM_FIYAT.Enabled = true; BTN_FIYAT_ATA.Enabled = true;
                TXT_BAS_SAAT.Enabled = true; BTN_BAS_SAATI_ATA.Enabled = true;
                TXT_BIT_SAAT.Enabled = true; BTN_BITIS_SAATI_ATA.Enabled = true;
            }
            else
            {
                DT_BASLANGIC_TARIHI.Enabled = true;
                BTN_BAS_TARIHI_ATA.Enabled = true;
                DT_BITIS_TARIHI.Enabled = true;
                BTN_BITIS_TARIHI_ATA.Enabled = true;
                CMB_HESAP_TURU.Enabled = true;
                BTN_HESAPLAMA_TURU_ATA.Enabled = true;
                TXT_BIRIM_FIYAT.Enabled = true;
                BTN_FIYAT_ATA.Enabled = true;
            }
        }
        
        private void SECENEKLER_ATAMASI(string SELECT_BUTTON)
        {
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
            {
                int[] GETROW = TrfSablon.gridView_TELEVIZYON.GetSelectedRows();
                for (int i = 0; i < GETROW.Length; i++)
                { 
                    DataRow dr = TrfSablon.gridView_TELEVIZYON.GetDataRow(Convert.ToInt32(GETROW[i]));
                    if (DT_BASLANGIC_TARIHI.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_TARIHI") dr["BASLANGIC_TARIHI"] = DT_BASLANGIC_TARIHI.EditValue.ToString();
                    if (DT_BITIS_TARIHI.EditValue != null) if (SELECT_BUTTON == "BITIS_TARIHI") dr["BITIS_TARIHI"] = DT_BITIS_TARIHI.EditValue.ToString();
                    if (TXT_BIRIM_FIYAT.EditValue != null) if (SELECT_BUTTON == "BIRIM_FIYAT") dr["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.EditValue.ToString();
                    if (TXT_BAS_SAAT.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_SAATI") dr["BASLANGIC_SAATI"] = TXT_BAS_SAAT.EditValue.ToString();
                    if (TXT_BIT_SAAT.EditValue != null) if (SELECT_BUTTON == "BITIS_SAATI") dr["BITIS_SAATI"] = TXT_BIT_SAAT.EditValue.ToString();
                    if (CMB_HESAP_TURU.EditValue != null) if (SELECT_BUTTON == "HESAPLANMA_TURU") dr["HESAPLANMA_TURU"] = CMB_HESAP_TURU.EditValue.ToString();
                }
            }

            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
            {
                int[] GETROW = TrfSablon.gridView_NONE_MEASURED_TELEVIZYON.GetSelectedRows();
                for (int i = 0; i < GETROW.Length; i++)
                {
                    DataRow dr = TrfSablon.gridView_NONE_MEASURED_TELEVIZYON.GetDataRow(Convert.ToInt32(GETROW[i]));
                    if (DT_BASLANGIC_TARIHI.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_TARIHI") dr["BASLANGIC_TARIHI"] = DT_BASLANGIC_TARIHI.EditValue.ToString();
                    if (DT_BITIS_TARIHI.EditValue != null) if (SELECT_BUTTON == "BITIS_TARIHI") dr["BITIS_TARIHI"] = DT_BITIS_TARIHI.EditValue.ToString();
                    if (TXT_BIRIM_FIYAT.EditValue != null) if (SELECT_BUTTON == "BIRIM_FIYAT") dr["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.EditValue.ToString();
                    if (TXT_BAS_SAAT.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_SAATI") dr["BASLANGIC_SAATI"] = TXT_BAS_SAAT.EditValue.ToString();
                    if (TXT_BIT_SAAT.EditValue != null) if (SELECT_BUTTON == "BITIS_SAATI") dr["BITIS_SAATI"] = TXT_BIT_SAAT.EditValue.ToString();
                    if (CMB_HESAP_TURU.EditValue != null) if (SELECT_BUTTON == "HESAPLANMA_TURU") dr["HESAPLANMA_TURU"] = CMB_HESAP_TURU.EditValue.ToString();
                }
            }

            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            {
                int[] GETROW = TrfSablon.gridView_RADYO.GetSelectedRows();
                for (int i = 0; i < GETROW.Length; i++)
                {
                    DataRow dr = TrfSablon.gridView_RADYO.GetDataRow(Convert.ToInt32(GETROW[i]));
                    if (DT_BASLANGIC_TARIHI.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_TARIHI") dr["BASLANGIC_TARIHI"] = DT_BASLANGIC_TARIHI.EditValue.ToString();
                    if (DT_BITIS_TARIHI.EditValue != null) if (SELECT_BUTTON == "BITIS_TARIHI") dr["BITIS_TARIHI"] = DT_BITIS_TARIHI.EditValue.ToString();
                    if (TXT_BIRIM_FIYAT.EditValue != null) if (SELECT_BUTTON == "BIRIM_FIYAT") dr["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.EditValue.ToString();
                    if (TXT_BAS_SAAT.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_SAATI") dr["BASLANGIC_SAATI"] = TXT_BAS_SAAT.EditValue.ToString();
                    if (TXT_BIT_SAAT.EditValue != null) if (SELECT_BUTTON == "BITIS_SAATI") dr["BITIS_SAATI"] = TXT_BIT_SAAT.EditValue.ToString();
                    if (CMB_HESAP_TURU.EditValue != null) if (SELECT_BUTTON == "HESAPLANMA_TURU") dr["HESAPLANMA_TURU"] = CMB_HESAP_TURU.EditValue.ToString();
                }
            }

            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
            {
                int[] GETROW = TrfSablon.gridView_GAZETE.GetSelectedRows();
                for (int i = 0; i < GETROW.Length; i++)
                {
                    DataRow dr = TrfSablon.gridView_GAZETE.GetDataRow(Convert.ToInt32(GETROW[i]));
                    if (DT_BASLANGIC_TARIHI.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_TARIHI") dr["BASLANGIC_TARIHI"] = DT_BASLANGIC_TARIHI.EditValue.ToString();
                    if (DT_BITIS_TARIHI.EditValue != null) if (SELECT_BUTTON == "BITIS_TARIHI") dr["BITIS_TARIHI"] = DT_BITIS_TARIHI.EditValue.ToString();
                    if (TXT_BIRIM_FIYAT.EditValue != null) if (SELECT_BUTTON == "BIRIM_FIYAT") dr["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.EditValue.ToString();
                    if (CMB_HESAP_TURU.EditValue != null) if (SELECT_BUTTON == "HESAPLANMA_TURU") dr["HESAPLANMA_TURU"] = CMB_HESAP_TURU.EditValue.ToString();
                }
            }

            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
            {
                int[] GETROW = TrfSablon.gridView_DERGI.GetSelectedRows();
                for (int i = 0; i < GETROW.Length; i++)
                {
                    DataRow dr = TrfSablon.gridView_DERGI.GetDataRow(Convert.ToInt32(GETROW[i]));
                    if (DT_BASLANGIC_TARIHI.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_TARIHI") dr["BASLANGIC_TARIHI"] = DT_BASLANGIC_TARIHI.EditValue.ToString();
                    if (DT_BITIS_TARIHI.EditValue != null) if (SELECT_BUTTON == "BITIS_TARIHI") dr["BITIS_TARIHI"] = DT_BITIS_TARIHI.EditValue.ToString();
                    if (TXT_BIRIM_FIYAT.EditValue != null) if (SELECT_BUTTON == "BIRIM_FIYAT") dr["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.EditValue.ToString();
                    if (CMB_HESAP_TURU.EditValue != null) if (SELECT_BUTTON == "HESAPLANMA_TURU") dr["HESAPLANMA_TURU"] = CMB_HESAP_TURU.EditValue.ToString();
                }
            }

            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
            {
                int[] GETROW = TrfSablon.gridView_OUTDOOR.GetSelectedRows();
                for (int i = 0; i < GETROW.Length; i++)
                {
                    DataRow dr = TrfSablon.gridView_OUTDOOR.GetDataRow(Convert.ToInt32(GETROW[i]));
                    if (DT_BASLANGIC_TARIHI.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_TARIHI") dr["BASLANGIC_TARIHI"] = DT_BASLANGIC_TARIHI.EditValue.ToString();
                    if (DT_BITIS_TARIHI.EditValue != null) if (SELECT_BUTTON == "BITIS_TARIHI") dr["BITIS_TARIHI"] = DT_BITIS_TARIHI.EditValue.ToString();
                    if (TXT_BIRIM_FIYAT.EditValue != null) if (SELECT_BUTTON == "BIRIM_FIYAT") dr["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.EditValue.ToString();
                    if (CMB_HESAP_TURU.EditValue != null) if (SELECT_BUTTON == "HESAPLANMA_TURU") dr["HESAPLANMA_TURU"] = CMB_HESAP_TURU.EditValue.ToString();
                }
            }
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
            {
                int[] GETROW = TrfSablon.gridView_SINEMA.GetSelectedRows();
                for (int i = 0; i < GETROW.Length; i++)
                {
                    DataRow dr = TrfSablon.gridView_SINEMA.GetDataRow(Convert.ToInt32(GETROW[i]));
                    if (DT_BASLANGIC_TARIHI.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_TARIHI") dr["BASLANGIC_TARIHI"] = DT_BASLANGIC_TARIHI.EditValue.ToString();
                    if (DT_BITIS_TARIHI.EditValue != null) if (SELECT_BUTTON == "BITIS_TARIHI") dr["BITIS_TARIHI"] = DT_BITIS_TARIHI.EditValue.ToString();
                    if (TXT_BIRIM_FIYAT.EditValue != null) if (SELECT_BUTTON == "BIRIM_FIYAT") dr["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.EditValue.ToString();
                    if (TXT_BAS_SAAT.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_SAATI") dr["BASLANGIC_SAATI"] = TXT_BAS_SAAT.EditValue.ToString();
                    if (TXT_BIT_SAAT.EditValue != null) if (SELECT_BUTTON == "BITIS_SAATI") dr["BITIS_SAATI"] = TXT_BIT_SAAT.EditValue.ToString();
                    if (CMB_HESAP_TURU.EditValue != null) if (SELECT_BUTTON == "HESAPLANMA_TURU") dr["HESAPLANMA_TURU"] = CMB_HESAP_TURU.EditValue.ToString();
                }
            }


            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Program")
            {
                int[] GETROW = TrfSablon.gridView_TARIFE_PROGRAM.GetSelectedRows();
                for (int i = 0; i < GETROW.Length; i++)
                {
                    DataRow dr = TrfSablon.gridView_TARIFE_PROGRAM.GetDataRow(Convert.ToInt32(GETROW[i]));
                    if (DT_BASLANGIC_TARIHI.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_TARIHI") dr["BASLANGIC_TARIHI"] = DT_BASLANGIC_TARIHI.EditValue.ToString();
                    if (DT_BITIS_TARIHI.EditValue != null) if (SELECT_BUTTON == "BITIS_TARIHI") dr["BITIS_TARIHI"] = DT_BITIS_TARIHI.EditValue.ToString();
                    if (TXT_BIRIM_FIYAT.EditValue != null) if (SELECT_BUTTON == "BIRIM_FIYAT") dr["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.EditValue.ToString();
                    if (TXT_BAS_SAAT.EditValue != null) if (SELECT_BUTTON == "BASLANGIC_SAATI") dr["BASLANGIC_SAATI"] = TXT_BAS_SAAT.EditValue.ToString();
                    if (TXT_BIT_SAAT.EditValue != null) if (SELECT_BUTTON == "BITIS_SAATI") dr["BITIS_SAATI"] = TXT_BIT_SAAT.EditValue.ToString();
                    if (CMB_HESAP_TURU.EditValue != null) if (SELECT_BUTTON == "HESAPLANMA_TURU") dr["HESAPLANMA_TURU"] = CMB_HESAP_TURU.EditValue.ToString();
                }
            }
        } 

        private TreeListNode GetDragNode(IDataObject data)
        {
            return (TreeListNode)data.GetData(typeof(TreeListNode));
        }

        private void OnDragEnter(object sender, DragEventArgs e)
        {
            TreeList list = (TreeList)sender;            
            TreeListNode node = GetDragNode(e.Data);
            if (node.GetValue("TYPE").ToString()  == "BASLIK")
            {
                if (node != null && node.TreeList != list)
                    e.Effect = DragDropEffects.Copy;
            }
        } 

    void treeList1_ShowingEditor(object sender, CancelEventArgs e)
    {
        e.Cancel = true;
    }
    private void OnDragDrop(object sender, DragEventArgs e)
        {
            TreeListNode node = GetDragNode(e.Data);
            if (node == null) return;
            TreeList list = (TreeList)sender; 
            TreeListHitInfo info = list.CalcHitInfo(list.PointToClient(new Point(e.X, e.Y)));
            if (info.Node.GetValue("TYPE").ToString() == "BASLIK" || info.Node.GetValue("TYPE").ToString() == "KIRILIM")
            {
               string FINDER= info.Node.ParentNode == null ? string.Format("{0}#{1}\\{2}", node.GetValue("TYPE"), info.Node.GetValue("TEXT"), node.GetValue("TEXT")) : string.Format("{0}#{1}\\{2}\\{3}", node.GetValue("TYPE"), info.Node.ParentNode.GetValue("TEXT"), info.Node.GetValue("TEXT"), node.GetValue("TEXT"));
                TreeListNode myNode = treeList_KIRILIMLAR.FindNode((nde) => { return nde["PATH"].ToString() == FINDER; });
                if (myNode == null)
                {
                    node["ParentID"] = info.Node.ParentNode == null ? -1 : info.Node.ParentNode.GetValue("ID");
                    node["PATH"] = info.Node.ParentNode == null ? string.Format("{0}#{1}\\{2}", node.GetValue("TYPE"), info.Node.GetValue("TEXT"), node.GetValue("TEXT")) : string.Format("{0}#{1}\\{2}\\{3}", node.GetValue("TYPE"), info.Node.ParentNode.GetValue("TEXT"), info.Node.GetValue("TEXT"), node.GetValue("TEXT"));
                    if (list == node.TreeList) return;
                }
                else { e.Effect = DragDropEffects.None; }
            }
            else
            {
                e.Effect = DragDropEffects.None;
                //list = null;
                //node = null;
               // if (list == node.TreeList) return;
            }

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
            fr.Text = "KIRILIM EKLE";
            fr.ShowDialog(); 
            if (fr._DURUMU == "TAMAM")
            {
                if (fr._TEXT != "")
                {
                    TreeListNode myNode = treeList_KIRILIMLAR.FindNode((node) => { return node["PATH"].ToString() == fr._PATH; });
                    if (myNode == null)
                    {
                        KIRILIM_SAY++;
                        DataRowView newRow = DW_LIST_KIRLIMLAR.AddNew();
                        newRow["ID"] = KIRILIM_SAY;
                        newRow["ParentID"] = -1;
                        newRow["TEXT"] = fr._TEXT;
                        newRow["NAME"] = fr._TEXT;
                        newRow["PATH"] = fr._PATH;
                        newRow["TYPE"] = "KIRILIM";
                        newRow["CHECKS"] = false;
                        newRow.EndEdit();
                    }
                } 
            } 
        } 
   
        private void BTN_KIRILIM_UP_Click(object sender, EventArgs e)
        {
            //TreeViews txr = new TreeViews();
          //  txr.UP(treeView_ANA_TEXT, treeView_ANA_TEXT);  
        }

        private void BTN_KIRILIM_DOWN_Click(object sender, EventArgs e)
        {
           // TreeViews txr = new TreeViews();
         //   txr.DOWN(treeView_ANA_TEXT, treeView_ANA_TEXT);  
        }

        private void BTN_KIRILIM_SECILEN_SIL_Click(object sender, EventArgs e)
        {
            if (treeList_KIRILIMLAR.FocusedNode == null) return;
            if (_BR_RAPOR_TIPI != "SABIT")
            {
                TreeListNode node = treeList_KIRILIMLAR.FocusedNode;
                string PATH_KIRILIMLAR = GetFullPath(node, "\\");
                PATH_KIRILIMLAR = "KIRILIM#" + PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1); 
                TreeListNode myNode = treeList_FILITRELER.FindNode((nd) => { return nd["PATH"].ToString() == PATH_KIRILIMLAR; });
                if (myNode == null)
                {
                    if (node.ParentNode != null)
                        node.ParentNode.Nodes.Remove(node);
                    else
                        treeList_KIRILIMLAR.Nodes.Remove(node);
                }
                else
                { MessageBox.Show("Bu Kırılımın Filitresi Var!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else
            {
                MessageBox.Show("Sabitlenmiş raporlarda değişiklik yapamazsınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
        public TreeNode m_previousSelectedNode = null;
     
        private void FILITRELER_KONTROL_MOVE_KONTROL(TreeNode treeNode)
        {
             SqlConnection SQLCON = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
             SQLCON.Open();
             SqlCommand Cmd = new SqlCommand() { CommandText = "DELETE    dbo.ADM_RAPOR_FILITRE  where GUI=@GUI " };
             Cmd.Parameters.AddWithValue("@GUI", treeNode.Tag);
             Cmd.Connection = SQLCON;
             Cmd.ExecuteNonQuery();
             SQLCON.Close();
             treeNode.Tag =null;
                foreach (TreeNode nt in treeNode.Nodes)
                {
                    FILITRELER_KONTROL_MOVE_KONTROL(nt);
                }
        }   
        private void dtBAS_TARIHI_EditValueChanged(object sender, EventArgs e)
        {
            _GLOBAL_PARAMETRELER._START_DATE = Convert.ToDateTime(DT_GNL_RPR_BAS_TARIHI.EditValue); 
        } 
        private void dtBIT_TARIHI_EditValueChanged(object sender, EventArgs e)
        {
            _GLOBAL_PARAMETRELER._END_DATE = Convert.ToDateTime(DT_GNL_RPR_BIT_TARIHI.EditValue);
        }

        private void MN_ANA_LEAF_RENAME_Click(object sender, EventArgs e)
        {
            if (treeList_KIRILIMLAR.FocusedNode == null) return;
            if (_BR_RAPOR_TIPI != "SABIT")
            {
                TreeListNode node = treeList_KIRILIMLAR.FocusedNode; 
                string PATH_KIRILIMLAR = GetFullPath(node, "\\");
                PATH_KIRILIMLAR = "KIRILIM#" + PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);
                TreeListNode myNode = treeList_FILITRELER.FindNode((nd) => { return nd["PATH"].ToString() == PATH_KIRILIMLAR; });
                if (myNode == null)
                {
                    TEXT.DEGISTIR fr = new TEXT.DEGISTIR();
                    fr.txt_KIRILIM_ESKI_KOD.Text = node.GetDisplayText("TEXT").ToString();
                    //fr.BR_RAPOR_KODU.Caption = BR_RAPOR_KODU.EditValue.ToString();
                    fr.ShowDialog();
                    if (fr._TEXT_NEW != null && fr._TEXT_NEW != "")
                    {
                        node.SetValue("TEXT", fr._TEXT_NEW);
                        node.SetValue("PATH", "KIRILIM#" + fr._TEXT_NEW);
                    }
                }
                else
                { MessageBox.Show("Bu kırılımın filitresi oluşturlumuş isimdeğişikliği yapılamaz!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning); }
            }
            else
            {
                MessageBox.Show("Sabitlenmiş raporlarda değişiklik yapamazsınız!", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }  
        } 
  
        private void treeList_KIRILIMLAR_BeforeCheckNode(object sender, CheckNodeEventArgs e)
        {
            e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
        }
         
        private void treeList_KIRILIMLAR_FocusedNodeChanged(object sender, FocusedNodeChangedEventArgs e)
        { 
            KIRILIM_NODE = e.Node; 
        }

        private void treeList_KIRILIMLAR_AfterCheckNode(object sender, NodeEventArgs e)
        {
            //  SetCheckedChildNodes(e.Node, e.Node.CheckState); 
            //  e.Node.SetValue("CHECKS", e.Node.Checked);

            string PATH_KIRILIMLAR = GetFullPath(e.Node, "\\");
            PATH_KIRILIMLAR = PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);
            TreeListNode myNodex = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
            if (myNodex != null)
            {
                //e.State = (e.PrevState == CheckState.Checked ? CheckState.Unchecked : CheckState.Checked);
                DataRow[] result = DW_LIST_KIRLIMLAR.Table.Select("PATH = 'KIRILIM#"+PATH_KIRILIMLAR+"'");
                foreach (DataRow row in result)
                {
                    row["CHECKS"] = e.Node.Checked; 
                } 
                DataRow[] reult = DW_LIST_FILITRE.Table.Select("PATH = 'KIRILIM#" + PATH_KIRILIMLAR + "'");
                foreach (DataRow row in reult)
                {
                    row["CHECKS"] = e.Node.Checked;
                }
                if (e.Node.Checked)
                {
                    e.Node.SetValue("CHECKS", e.Node.Checked);
                    myNodex.Checked = true;
                    myNodex.SetValue("CHECKS", true);//
                    myNodex["CHECKS"] = true;// node.UncheckAll();
                    SetCheckedChildNodes(myNodex, e.Node.CheckState);
                }
                else
                {
                    e.Node.SetValue("CHECKS", e.Node.Checked);
                    myNodex.Checked = false;
                    myNodex.SetValue("CHECKS", false);// 
                    myNodex["CHECKS"] = false;// node.CheckAll();
                    SetCheckedChildNodes(myNodex, e.Node.CheckState);                    
                }
            }
        }

        private void SetCheckedChildNodes(TreeListNode node, CheckState check)
        {
            for (int i = 0; i < node.Nodes.Count; i++)
            {
                DataRow[] result = DW_LIST_FILITRE.Table.Select("PATH = '"+ node["PATH"].ToString () + "'");
                foreach (DataRow row in result)
                {
                   if (check.ToString()=="Checked") row["CHECKS"] = true;
                   if (check.ToString() != "Checked") row["CHECKS"] = false;
                }

                node.Nodes[i].CheckState = check;
                node.Nodes[i].SetValue("CHECKS", check);
                //node.Nodes[i]["CHECKS"] = check;
                SetCheckedChildNodes(node.Nodes[i], check);
            }
        }
        private void SetCheckedParentNodes(TreeListNode node, CheckState check)
        {
            if (node.ParentNode != null)
            {
                bool b = false;
                CheckState state;
                for (int i = 0; i < node.ParentNode.Nodes.Count; i++)
                {
                    state = (CheckState)node.ParentNode.Nodes[i].CheckState;
                    if (!check.Equals(state))
                    {
                        b = !b;
                        break;
                    }
                }
                node.ParentNode.CheckState = b ? CheckState.Indeterminate : check;
                SetCheckedParentNodes(node.ParentNode, check);
            }
        } 

        private void gridView_BASLIKLAR_DoubleClick(object sender, EventArgs e)
        {
            QUERY = "";
            string TMP_QUERY = "";
            if (treeList_KIRILIMLAR.FocusedNode == null) { MessageBox.Show("Kırılım Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return; }; 
            DataRow DR = gridView_BASLIKLAR.GetDataRow(gridView_BASLIKLAR.FocusedRowHandle);
            string DAHIL_HARIC = " (Dahil)", _FIELD_NAME = "", _FIELD_KONTROL_NAME = "", GROUP_BY = ""; 

            if (!TOOGLE_TELEVIZYON.IsOn && !TOOGLE_GAZETE.IsOn && !TOOGLE_DERGI.IsOn && !TOOGLE_OUTDOOR.IsOn && !TOOGLE_SINEMA.IsOn && !TOOGLE_RADYO.IsOn && !TOOGLE_INTERNET.IsOn)
            { MessageBox.Show("Mecra Türü Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return; } 
 
            if (DR != null)
            {
                using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQL = string.Format(" SELECT  [FIELDS] FROM  dbo.ADM_SECENEKLER WHERE    (SECENEK = N'{0}')", DR["TEXT"].ToString ());
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
         
                TreeListNode myNode = treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() ==   "KIRILIM#" + PATH_KIRILIMLAR; });
                if (myNode != null)
                {
                    _FIELD_KONTROL_NAME = ""; GROUP_BY = "";
                    foreach (TreeListNode item in myNode.Nodes)
                    { 
                        if (item.GetValue("TYPE").ToString ()=="BASLIK")
                        {
                            if (TMP_QUERY != "") { if (TMP_QUERY.Length > 0) { QUERY += string.Format("({0}) and ", TMP_QUERY.Substring(0, TMP_QUERY.Length - 3));    TMP_QUERY = ""; } }
                             
                            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                string SQL = string.Format(" SELECT  [FIELDS] FROM  dbo.ADM_SECENEKLER WHERE    (SECENEK = N'{0}')", item.GetValue("TEXT").ToString().Replace("(Dahil)", "").Replace("(Hariç)", ""));
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
                  
                            foreach (TreeListNode txn in item.Nodes)
                            {
                               
                                    if (item.GetValue("TEXT").ToString().IndexOf("(Dahil)") != -1) DAHIL_HARIC = "Dahil";
                                    if (item.GetValue("TEXT").ToString().IndexOf("(Hariç)") != -1) DAHIL_HARIC = "Hariç"; 
                                     
                                        if (txn.GetValue("TYPE").ToString().IndexOf("SATIR") != -1)
                                        {
                                            if (DAHIL_HARIC == "Dahil")
                                            {
                                                if (txn.GetValue(1).ToString().IndexOf("%") != -1)
                                                { TMP_QUERY += _FIELD_KONTROL_NAME + " LIKE '" + txn.GetValue("TEXT").ToString() + "' OR "; }
                                                else
                                                { TMP_QUERY += string.Format("{0}='{1}' OR ", _FIELD_KONTROL_NAME, txn.GetValue("TEXT")); }
                                            }
                                            if (DAHIL_HARIC == "Hariç")
                                            {
                                                if (txn.GetValue(1).ToString().IndexOf("%") != -1)
                                                { TMP_QUERY += _FIELD_KONTROL_NAME + " NOT LIKE '" + txn.GetValue("TEXT").ToString() + "' OR "; }
                                                else
                                                { TMP_QUERY += _FIELD_KONTROL_NAME + "<>'" + txn.GetValue("TEXT").ToString() + "' OR "; }
                                            }
                                        }  
                                
                            }
                     
                    }
                    if (TMP_QUERY.Length > 0) QUERY += string.Format("({0})  AND ", TMP_QUERY.Substring(0, TMP_QUERY.Length - 4));
                }
                    GROUP_BY = GROUP_BY + _FIELD_NAME;
                if (QUERY.Length > 0) QUERY = QUERY.Substring(0, QUERY.Length - 4);

                if (_FIELD_NAME != "")
                {
                    FILITRELER.GENEL_FILITRELER todo = new FILITRELER.GENEL_FILITRELER(_FIELD_NAME, _FIELD_NAME, QUERY, TOOGLE_TELEVIZYON.IsOn, TOOGLE_GAZETE.IsOn, TOOGLE_DERGI.IsOn, TOOGLE_OUTDOOR.IsOn, TOOGLE_SINEMA.IsOn, TOOGLE_RADYO.IsOn, TOOGLE_INTERNET.IsOn);
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
                                    TreeListNode Header = myNodex.TreeList.FindNode((node) => { return  node["PATH"].ToString() == "BASLIK#" + PATH_KIRILIMLAR + "\\" + DR["TEXT"].ToString() + DAHIL_HARIC; }); 
                                    if (Header == null)
                                    {
                                        FILITRE_SAY++;
                                        DataRowView newParent = DW_LIST_FILITRE.AddNew();
                                        newParent["ID"] = FILITRE_SAY;
                                        newParent["ParentID"] = myNodex["ID"]; 
                                        newParent["TEXT"] = string.Format("{0}{1}", DR["TEXT"].ToString(), DAHIL_HARIC);
                                        newParent["TYPE"] = "BASLIK";
                                        newParent["NAME"] = DR["TEXT"];
                                        newParent["PATH"] = string.Format("BASLIK#{0}\\{1}{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString(), DAHIL_HARIC); 
                                        newParent.EndEdit(); 
                                        for (int xi = dvs.Count - 1; xi >= 0; xi--)
                                        {
                                            FILITRE_SAY++;
                                            DataRowView newRow = DW_LIST_FILITRE.AddNew();
                                            newRow["ID"] = FILITRE_SAY;
                                            newRow["ParentID"] = newParent["ID"]; 
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
                                            newRow["ParentID"] = Header["ID"]; 
                                            newRow["TEXT"] = dvs.Table.Rows[xi][6].ToString();
                                            newRow["TYPE"] = "SATIR";
                                            newRow["PATH"] = string.Format("SATIR#{0}\\{1}\\{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString() + DAHIL_HARIC, dvs.Table.Rows[xi][6]); 
                                            newRow.EndEdit();
                                        }
                                    }
                                }
                            }
                        }

                       // treeList_FILITRELER.Refresh();


                    }

                    if (todo._FILITRE_NEREYE == "GENEL")
                    { 
                        if (dvs.Count > 0)
                        {
                            TreeListNode Header = treeList_GENEL_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == "BASLIK#" + PATH_KIRILIMLAR + "\\" + DR["TEXT"].ToString() + DAHIL_HARIC; });
                            if (Header == null)
                            { 
                                FILITRE_SAY++;
                                DataRowView newParent = DW_LIST_GENEL_FILITRE.AddNew();
                                newParent["ID"] = FILITRE_SAY;
                                newParent["ParentID"] = -1;
                                newParent["TEXT"] = string.Format("{0}{1}", DR["TEXT"].ToString(), DAHIL_HARIC);
                                newParent["TYPE"] = "BASLIK";
                                newParent["NAME"] = DR["TEXT"];
                                newParent["PATH"] = string.Format("BASLIK#{0}\\{1}{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString(), DAHIL_HARIC);
                                newParent.EndEdit();
                                for (int xi = dvs.Count - 1; xi >= 0; xi--)
                                {
                                    FILITRE_SAY++;
                                    DataRowView newRow = DW_LIST_GENEL_FILITRE.AddNew();
                                    newRow["ID"] = FILITRE_SAY;
                                    newRow["ParentID"] = newParent["ID"];
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
                                    DataRowView newRow = DW_LIST_GENEL_FILITRE.AddNew();
                                    newRow["ID"] = FILITRE_SAY;
                                    newRow["ParentID"] = Header["ID"];
                                    newRow["TEXT"] = dvs.Table.Rows[xi][6].ToString();
                                    newRow["TYPE"] = "SATIR";
                                    newRow["PATH"] = string.Format("SATIR#{0}\\{1}\\{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString() + DAHIL_HARIC, dvs.Table.Rows[xi][6]);
                                    newRow.EndEdit();
                                }
                            } 
                        }
                    }
                }
            }
        } 

        private void MN_OZEL_FILITRE_EKLE_Click(object sender, EventArgs e)
        {
            string _OZEL_GENEL = "OZEL";
            FILITRELER.OZEL_FILITRE fr = new FILITRELER.OZEL_FILITRE();
            fr.ShowDialog();
            if (fr._DURUMU == "TAMAM")
            {
                if (fr._TEXT != "")
                {
                    fr._TEXT = fr._TEXT.Replace("Ö", "O").Replace("Ş", "S").Replace("İ", "I").Replace ("Ğ","G").Replace ("Ü","U").Replace("Ç","C") ;
                    _OZEL_GENEL = fr._OZEL_GENEL;
                    if (treeList_KIRILIMLAR.FocusedNode == null) { MessageBox.Show("Kırılım Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return; };
                    DataRow DR = gridView_BASLIKLAR.GetDataRow(gridView_BASLIKLAR.FocusedRowHandle);
                    string DAHIL_HARIC = fr._DAHIL_HARIC;   
                    string _FIELD_NAME = "", _FIELD_KONTROL_NAME = "", GROUP_BY = "";
                    QUERY = "";
                    if (!TOOGLE_TELEVIZYON.IsOn && !TOOGLE_GAZETE.IsOn && !TOOGLE_DERGI.IsOn && !TOOGLE_OUTDOOR.IsOn && !TOOGLE_SINEMA.IsOn && !TOOGLE_RADYO.IsOn && !TOOGLE_INTERNET.IsOn)
                    { MessageBox.Show("Mecra Türü Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return; }
                    if (DR != null)
                    {   using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
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

                                if (item.GetValue("TYPE").ToString ()=="BASLIK")
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

                                if (item.GetValue("TEXT").ToString().Replace(" (Dahil)", "").Replace(" (Hariç)", "") == DR["TEXT"].ToString())
                                {
                                    foreach (TreeListNode txn in item.Nodes)
                                    {
                                        
                                            if (item.GetValue(0).ToString().IndexOf("(Dahil)") != -1)
                                            {
                                                if (_FIELD_NAME == _FIELD_KONTROL_NAME)
                                                {
                                                    QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue("TEXT"));
                                                }
                                                else
                                                {
                                                    if (txn.GetValue(0).ToString().IndexOf("%") != -1)
                                                    {
                                                        QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue("TEXT"));
                                                    }
                                                    else
                                                    {
                                                        QUERY += string.Format("{0} = '{1}' OR ", _FIELD_KONTROL_NAME, txn.GetValue("TEXT"));
                                                    }
                                                }
                                            }

                                            if (item.GetValue(0).ToString().IndexOf("(Hariç)") != -1)
                                            {

                                                if (_FIELD_NAME == _FIELD_KONTROL_NAME)
                                                {
                                                    QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue("TEXT"));
                                                }
                                                else
                                                {
                                                    if (txn.GetValue(0).ToString().IndexOf("%") != -1)
                                                    {
                                                        QUERY += string.Format("{0} NOT LIKE '{1}' AND ", _FIELD_KONTROL_NAME, txn.GetValue("TEXT"));
                                                    }
                                                    else
                                                    {
                                                        QUERY += string.Format("{0} = '{1}' OR ", _FIELD_KONTROL_NAME, txn.GetValue("TEXT"));
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
                                                    newRow["TYPE"] = "KIRILIM";
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

                                    if (_OZEL_GENEL == "OZEL")
                                    {
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
                                                newParent["PATH"] = string.Format("BASLIK#{0}\\{1}{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString(), DAHIL_HARIC);
                                                newParent["NAME"] = string.Format("{0}",DR["TEXT"].ToString());
                                                newParent.EndEdit();
                                                FILITRE_SAY++;
                                                DataRowView newRow = DW_LIST_FILITRE.AddNew();
                                                newRow["ID"] = FILITRE_SAY;
                                                newRow["ParentID"] = newParent["ID"];
                                                newRow["TEXT"] = fr._TEXT;
                                                newRow["TYPE"] = "SATIR";
                                                newRow["PATH"] = string.Format("SATIR#{0}\\{1}\\{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString() + DAHIL_HARIC, fr._TEXT);
                            
                                            newRow.EndEdit();
                                            }
                                            else
                                            {
                                                FILITRE_SAY++;
                                                DataRowView newRow = DW_LIST_FILITRE.AddNew();
                                                newRow["ID"] = FILITRE_SAY;
                                                newRow["ParentID"] = Header["ID"];
                                                newRow["TEXT"] = fr._TEXT;
                                                newRow["TYPE"] = "SATIR";
                                                newRow["PATH"] = string.Format("SATIR#{0}\\{1}\\{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString() + DAHIL_HARIC, fr._TEXT);
                                   
                                                newRow.EndEdit();
                                            }
                                        }
                                    } 
                                    
                                    if (_OZEL_GENEL == "GENEL")
                                    {  
                                             TreeListNode Header = treeList_GENEL_FILITRELER.FindNode((node) => { return node["NAME"].ToString() == "BASLIK-" + PATH_KIRILIMLAR + "\\" + DR["TEXT"].ToString() + DAHIL_HARIC; });

                                            if (Header == null)
                                            {
                                                FILITRE_SAY++;
                                                DataRowView newParent = DW_LIST_GENEL_FILITRE.AddNew();
                                                newParent["ID"] = FILITRE_SAY;
                                                newParent["ParentID"] = -1;// myNodex["ID"];
                                                newParent["TYPE"] = "BASLIK";
                                                newParent["TEXT"] = string.Format("{0}{1}", DR["TEXT"].ToString(), DAHIL_HARIC); 
                                                newParent["PATH"] = string.Format("{0}\\{1}{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString(), DAHIL_HARIC); 
                                                newParent["NAME"] = string.Format("BASLIK#{0}\\{1}{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString(), DAHIL_HARIC);
                                                newParent.EndEdit();
                                                FILITRE_SAY++;
                                                DataRowView newRow = DW_LIST_GENEL_FILITRE.AddNew();
                                                newRow["ID"] = FILITRE_SAY;
                                                newRow["ParentID"] = newParent["ID"];
                                                newRow["TEXT"] = fr._TEXT;
                                                newRow["TYPE"] = "SATIR";
                                                newRow["PATH"] = string.Format("{0}\\{1}\\{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString() + DAHIL_HARIC, fr._TEXT); 
                                                newRow["NAME"] = string.Format("SATIR#{0}\\{1}", PATH_KIRILIMLAR, fr._TEXT);
                                                newRow.EndEdit();

                                            }
                                            else
                                            {
                                                FILITRE_SAY++;
                                                DataRowView newRow = DW_LIST_GENEL_FILITRE.AddNew();
                                                newRow["ID"] = FILITRE_SAY;
                                                newRow["ParentID"] = Header["ID"];
                                                newRow["TEXT"] = fr._TEXT;
                                                newRow["TYPE"] = "SATIR";
                                                newRow["PATH"] = string.Format("{0}\\{1}\\{2}", PATH_KIRILIMLAR, DR["TEXT"].ToString() + DAHIL_HARIC, fr._TEXT);//PATH_KIRILIMLAR;
                                                newRow["NAME"] = string.Format("SATIR#{0}\\{1}", PATH_KIRILIMLAR, fr._TEXT);
                                                newRow.EndEdit();
                                            }                                       
                                } 
                            }
                        }
                    } 
                }
            }  
        }
  
        private void gridView_BASLIKLAR_RowStyle(object sender, RowStyleEventArgs e)
        {  
            GridView View = sender as GridView;
            if (e.RowHandle >= 0)
            {
                string category = View.GetRowCellDisplayText(e.RowHandle, View.Columns["ROWTYPE"]);
                if (category == "SERBEST")
                {
                    e.Appearance.BackColor = Color.Yellow;
                    e.Appearance.BackColor2 = Color.LightYellow;
                }
            }
        }

        private void MN_FL_GRUPLA_Click(object sender, EventArgs e)
        {
            treeList_FILITRELER.CollapseAll();
        }

        private void MN_FL_LISTELE_Click(object sender, EventArgs e)
        {
            treeList_FILITRELER.ExpandAll();
        } 

        private void treeList_FILITRELER_AfterCheckNode(object sender, NodeEventArgs e)
        {
            e.Node.SetValue("CHECKS", e.Node.Checked);
        }
        private void MN_SB_GRUPLA_Click(object sender, EventArgs e)
        {
            treeList_Sabitler.CollapseAll();
        }
        private void MN_SB_LISTELE_Click(object sender, EventArgs e)
        {
            treeList_Sabitler.ExpandAll();
        }

        private void CMB_DIL_SelectedIndexChanged(object sender, EventArgs e)
        {
            _GLOBAL_PARAMETRELER._DIL = CMB_DIL.Text;
        } 
        private void OnFilterNode(object sender, FilterNodeEventArgs e)
        {
            List<TreeListColumn> filteredColumns = e.Node.TreeList.Columns.Cast<TreeListColumn>(
                ).Where(c => c.FilterInfo.AutoFilterRowValue != null).ToList();
            if (filteredColumns.Count == 0) return;
            e.Handled = true;
            e.Node.Visible = filteredColumns.Any(c => IsNodeMatchFilter(e.Node, c));
        }

        static bool IsNodeMatchFilter(TreeListNode node, TreeListColumn column)
        {
            string filterValue = column.FilterInfo.AutoFilterRowValue.ToString().ToUpper();
            if (node.GetDisplayText(column).StartsWith(filterValue)) return true;
            foreach (TreeListNode n in node.Nodes)
                if (IsNodeMatchFilter(n, column)) return true;
            return false;
        } 

        private void BTN_GENEL_FILITRE_Click(object sender, EventArgs e)
        {
            TreeListNode node = treeList_GENEL_FILITRELER.FocusedNode;
            if (node == null) return;
            if (node.HasChildren)
            { 
                node.ParentNode.Nodes.Remove(node);
            }
            else
            { treeList_GENEL_FILITRELER.Nodes.Remove(node); }
        }

        private void BTN_TARIFE_ATA_Click(object sender, EventArgs e)
        {
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
            {
                int[] GETROW = TrfSablon.gridView_TELEVIZYON.GetSelectedRows();
                for (int i = 0; i < GETROW.Length; i++)
                {
                    DataRow dr = TrfSablon.gridView_TELEVIZYON.GetDataRow(Convert.ToInt32(GETROW[i]));
                    dr["TARGET"] = CMB_TARGET.EditValue.ToString();
                }
            }
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
            {
                int[] GETROW = TrfSablon.gridView_NONE_MEASURED_TELEVIZYON.GetSelectedRows();
                for (int i = 0; i < GETROW.Length; i++)
                {
                    DataRow dr = TrfSablon.gridView_NONE_MEASURED_TELEVIZYON.GetDataRow(Convert.ToInt32(GETROW[i]));
                    dr["TARGET"] = CMB_TARGET.EditValue.ToString();
                }
            }
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Internet") TrfSablon.TARIFE_CHANGE("INTERNET");
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sektor") TrfSablon.TARIFE_CHANGE("SEKTOR");
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Program") TrfSablon.TARIFE_CHANGE("PROGRAM");
            if (TrfSablon.xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar") TrfSablon.TARIFE_CHANGE("ORAN");
        }

        private void BTN_BAS_TARIHI_ATA_Click(object sender, EventArgs e)
        {
            SECENEKLER_ATAMASI("BASLANGIC_TARIHI");
        }

        private void BTN_BITIS_TARIHI_ATA_Click(object sender, EventArgs e)
        {
            SECENEKLER_ATAMASI("BITIS_TARIHI");
        }

        private void BTN_HESAPLAMA_TURU_ATA_Click(object sender, EventArgs e)
        {
            SECENEKLER_ATAMASI("HESAPLANMA_TURU");
        }

        private void BTN_FIYAT_ATA_Click(object sender, EventArgs e)
        {
            SECENEKLER_ATAMASI("BIRIM_FIYAT"); 
        }

        private void BTN_BAS_SAATI_ATA_Click(object sender, EventArgs e)
        {
            SECENEKLER_ATAMASI("BASLANGIC_SAATI"); 
        }

        private void BTN_BITIS_SAATI_ATA_Click(object sender, EventArgs e)
        {
            SECENEKLER_ATAMASI("BITIS_SAATI"); 
        }

        private void TOGGLE_GENEL_FILITRE_EditValueChanged(object sender, EventArgs e)
        {
            if (TOGGLE_GENEL_FILITRE.IsOn) DCK_GENEL_FILITRE.Visibility = DevExpress.XtraBars.Docking.DockVisibility.Visible; else DCK_GENEL_FILITRE.Visibility = DevExpress.XtraBars.Docking.DockVisibility.AutoHide;
        } 
    }
}