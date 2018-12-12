using System;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using DevExpress.XtraTreeList.Nodes;
using System.Collections;

namespace CoreV2.TARIFELER
{
    public partial class MASTER_KEYWORD : DevExpress.XtraEditors.XtraForm
    { 
        DataView RW = null;
        string BASLIKLAR = "", BASLIKLAR_GROUPBY = "";
      


        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManagers;

        DataView DW_LIST_KIRLIMLAR = null, DW_LIST = null, DW_LIST_FILITRE = null;
     
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
        string WHERES = string.Empty;
 


        public MASTER_KEYWORD()
        {
            InitializeComponent();
            SECENEKLER_LIST(_GLOBAL_PARAMETRELER._RAPOR_KODU); 
            DATA_READ("");
        }

        public void SECENEKLER_LIST(string RAPOR_KODU)
        {   using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
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
            //  treeList_KIRILIMLAR.NodesIterator.DoOperation(new InitialStateTreeListOperation());    
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
        private void DATA_READ(string _TARIFE_KODU)
        {
            SqlConnection MySqlConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter("SELECT  * FROM  dbo.TRF_KEYWORD  WHERE   TARIFE_KODU='" + _TARIFE_KODU + "' and SIRKET_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "'", MySqlConnection);
            DataSet MyDataSet = new DataSet();
            MySqlDataAdapter.Fill(MyDataSet, "TRF_INTERNET_MASTER");
            DataViewManager dvManager = new DataViewManager(MyDataSet);
            RW = dvManager.CreateDataView(MyDataSet.Tables[0]);
            gridControl_SECENEKLER.DataSource = RW;
            for (int i = 0; i <= 5; i++)
            { RW.AddNew(); } 
        } 
        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
        private void BTN_LISTE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //TARIFELER._TARIFE_LISTESI trf = new TARIFELER._TARIFE_LISTESI("KEYWORD");
            //trf.ShowDialog();
            //if (trf._BTN_TYPE == "Tamam") {

            //    if (_GLOBAL_PARAMETRELER._RAPOR_KODU != null)
            //    {
            //        lbFILE_NAME.Caption = trf._TARIFE_KODU; lbID.Caption = trf._ID; DATA_READ(trf._TARIFE_KODU);
            //    }    
            //    else
            //    { 
            //        MessageBox.Show("Rapor Kodu seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            //    }
            //}


            //this.ParentForm.GetType().InvokeMember("ZORUNLU_ALANLARI_ISARETLE", System.Reflection.BindingFlags.InvokeMethod, null, this.ParentForm, new object[] { TF._MECRA_TURU }); 





            TARIFELER._TARIFE_LISTESI trf = new TARIFELER._TARIFE_LISTESI("TARIFE","KEYWORD");
            trf.ShowDialog();
            if (trf._BTN_TYPE == "Tamam")
            {
                gridControl_SECENEKLER.DataSource = null;
                lbFILE_NAME.Caption = trf._TARIFE_KODU;
                lbID.Caption = trf._TARIFE_ID;
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
                    lbFILE_NAME.Caption = trf._TARIFE_KODU; lbID.Caption = trf._TARIFE_ID; DATA_READ(trf._TARIFE_KODU);



                    //using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    //{
                    //    DataSet ds = new DataSet();
                    //    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(string.Format("SELECT * FROM     dbo.__MAS_EDT_{0}_{1}", lbID.Caption, lbFILE_NAME.Caption.ToString()), conn) };
                    //    adapter.Fill(ds, "TRF_MASTER");
                    //    DataViewManager dvManager = new DataViewManager(ds);
                    //    DW_LIST = dvManager.CreateDataView(ds.Tables[0]);
                    //    gridControl_FREE_TABLE.DataSource = DW_LIST;
                    //    gridView_SABITLER.Columns["ID"].Visible = false;
                    //}


                    //if (trf._BASLIKLAR != null)
                    //{
                    //    // tmp._BASLIKLAR = "ID;" + tmp._BASLIKLAR;
                    //    string[] Ones = trf._BASLIKLAR.Split(';');
                    //    for (int i = 0; i < Onesz.Length - 1; i++)
                    //    {
                    //        gridView_SABITLER.Columns[1 + i].Caption = Ones[i].Trim();
                    //        gridView_SABITLER.Columns[1 + i].OptionsFilter.FilterPopupMode = DevExpress.XtraGrid.Columns.FilterPopupMode.CheckedList;
                    //    }
                    //}

                }
                else
                {
                    MessageBox.Show("Rapor Kodu seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        private void BTN_DATA_LISTE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
  
    
        }


        private void RAPORU_BASLAT(string BTN_DURUMU)
        {


            ///
            /// RAPOR PARAMETRELERINI TEMİZLE
            /// 

            KIRILIM = KIRILIM_CAST = KIRILIM_FIELD = KIRILIM_TABLE_CREATE = OZEL_TANIMLAMA = OZEL_TANIMLAMA_CAST = OZEL_TANIMLAMA_FIELD = OZEL_TABLE_CREATE =
            BASLIK = BASLIK_CAST = BASLIK_FIELD = BASLIK_TABLE_CREATE = OLCUM = OLCUM_CAST = OLCUM_FIELD = OLCUM_TABLE_CREATE = FILITRE = FILITRE_CAST = FILITRE_FIELD = FILITRE_TABLE_CREATE = string.Empty;

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
            if (gridView_SECENEKLER.RowCount < 1)
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

            DataRow DR = gridView_SECENEKLER.GetDataRow(gridView_SECENEKLER.FocusedRowHandle);
            if (DR != null)
            {
                BASLIKLAR += string.Format("[{0}],", DR["ALANLAR"]); 
            }
             

            if (BASLIKLAR.Length > 0) BASLIKLAR = BASLIKLAR.Substring(0, BASLIKLAR.Length - 1);
            ///
            /// Kırılımlı Rapor İçin Node Kontrol
            ///    
            string PATH_KIRILIMLAR = "";
            if (treeList_KIRILIMLAR.Nodes.Count != 0)
            {
                //List<TreeListNode> ndsc = RaporDesigner.treeList_KIRILIMLAR.GetNodeList();
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
                            ANAKIRILIM_FILITRE_CHEK_KONTROL("", BTN_DURUMU);
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
                                            ANAKIRILIM_FILITRE_CHEK_KONTROL(myNode.GetDisplayText("TEXT").ToString(), BTN_DURUMU);
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

                                ANAKIRILIM_FILITRE_CHEK_KONTROL(myNode.GetDisplayText("TEXT").ToString(), BTN_DURUMU);

                            }

                        }
                    }
                }



            }
            else
            { MessageBox.Show(" Lütfen Rapor seçiniz."); }
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
        private void ANAKIRILIM_FILITRE_CHEK_KONTROL(string TMP_QUERY,string BTN_TYPE)
        {

            string SELECT_FIELDS = "", GROUP_BY_FIELDS = "";
            DataRow DR = gridView_SECENEKLER.GetDataRow(gridView_SECENEKLER.FocusedRowHandle);
            if (DR != null)
            {
                string[] Onesz = DR["ALANLAR"].ToString().Split(',');
                    for (int i = 0; i < Onesz.Length; i++)
                    {
                        using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            string SQLD = string.Format(" SELECT   * FROM   dbo.ADM_SECENEKLER where SECENEK='{0}'", Onesz[i].ToString().Trim () );
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
                                SELECT_FIELDS += string.Format(" CAST('' AS nvarchar ) as [{0}],", Onesz[i].ToString().Trim());
                            }
                        }
                    }
            } 
         
            if (DR != null)
            {
                using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQLD = string.Format(" SELECT   * FROM   dbo.ADM_SECENEKLER where SECENEK='{0}'", DR["ALAN"].ToString().Trim());
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
                        if (BTN_TYPE == "ICERMEZ") SELECT_FIELDS += string.Format(" CAST('' AS nvarchar ) as [{0}],", DR["ALAN"].ToString().Trim());
                        if (BTN_TYPE == "ICERIR") SELECT_FIELDS += string.Format(" CAST('" + DR["DEGER"].ToString() + "' AS nvarchar ) as [{0}],", DR["ALAN"].ToString().Trim());
                    }
                }
             }

            WHERES = "";
           string  TMP_WHERES = "";

            if (DR != null)
            {
                string[] Onesz = DR["ALANLAR"].ToString().Split(',');
                for (int i = 0; i < Onesz.Length; i++)
                {
                    using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string SQLD = string.Format(" SELECT   * FROM   dbo.ADM_SECENEKLER where SECENEK='{0}'", Onesz[i].ToString().Trim());
                        SqlCommand myCommand = new SqlCommand(SQLD, con) { CommandText = SQLD.ToString() };
                        con.Open();
                        SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        if (myReader.HasRows)
                        {
                            while (myReader.Read())
                            {
                                if (TMP_WHERES != "") {
                                    WHERES = TEMIZLE_ELEMAN(TMP_WHERES, 4, "AND ").ToString();
                                    WHERES = TEMIZLE_ELEMAN(WHERES, 3, "OR ").ToString();
                                    WHERES = "(" + WHERES + ")"; TMP_WHERES = "";
                                }
                                string[] Onesy = DR["KEYWORDS"].ToString().Split(',');
                                for (int Xi = 0; Xi < Onesy.Length; Xi++)
                                {
                                    if (BTN_TYPE=="ICERMEZ") TMP_WHERES += string.Format("{0} NOT LIKE '{1}' AND ", myReader["FIELDS"], "%" + Onesy[Xi].ToString().Trim() + "%");
                                    if (BTN_TYPE == "ICERIR") TMP_WHERES += string.Format("{0} LIKE '{1}' OR ", myReader["FIELDS"], "%" + Onesy[Xi].ToString().Trim() + "%"); 
                                    
                                }
                            }
                        }
 
                    }
                }
                if (TMP_WHERES != "")
                {
                    TMP_WHERES = TEMIZLE_ELEMAN(TMP_WHERES, 4, "AND ").ToString();
                    TMP_WHERES = TEMIZLE_ELEMAN(TMP_WHERES, 3, "OR ").ToString();
                }

                WHERES += "   (" + TMP_WHERES + ")";

            } 
         
 
            GROUP_BY_FIELDS = TEMIZLE_ELEMAN(GROUP_BY_FIELDS, 1, ",").ToString();
            SELECT_FIELDS = TEMIZLE_ELEMAN(SELECT_FIELDS, 1, ",").ToString();
            
         
            if (TOOGLE_TELEVIZYON.IsOn)
            {
                if (SQL != "") SQL += " UNION   ";
                SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_TELEVIZYON + WHERES + " AND [YAYIN_SINIFI]='TELEVIZYON' ", GROUP_BY_FIELDS);
            }
            if (TOOGLE_RADYO.IsOn)
            {
                if (SQL != "") SQL += " UNION   ";
                SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_RADYO + WHERES + " AND [YAYIN_SINIFI]='RADYO' ", GROUP_BY_FIELDS);
            }
            if (TOOGLE_DERGI.IsOn)
            {
                if (SQL != "") SQL += " UNION   ";
                SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_DERGI + WHERES + " AND [YAYIN_SINIFI]='DERGI' ", GROUP_BY_FIELDS);
            }
            if (TOOGLE_GAZETE.IsOn)
            {
                if (SQL != "") SQL += " UNION   ";
                SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_GAZETE + WHERES + " AND [YAYIN_SINIFI]='GAZETE' ", GROUP_BY_FIELDS);
            }
            if (TOOGLE_SINEMA.IsOn)
            {
                if (SQL != "") SQL += " UNION ALL ";
                SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_SINEMA + WHERES + " AND [YAYIN_SINIFI]='SINEMA' ", GROUP_BY_FIELDS);
            }
            if (TOOGLE_OUTDOOR.IsOn)
            {
                if (SQL != "") SQL += " UNION   ";
                SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_OUTDOOR + WHERES + " AND [YAYIN_SINIFI]='OUTDOOR' ", GROUP_BY_FIELDS);
            }
            if (TOOGLE_INTERNET.IsOn)
            {
                if (SQL != "") SQL += " UNION   ";
                SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_INTERNET + WHERES + " AND [YAYIN_SINIFI]='INTERNET' ", GROUP_BY_FIELDS);
            }

        
            TMP_QUERY_TELEVIZYON = TMP_QUERY_GAZETE = TMP_QUERY_DERGI = TMP_QUERY_SINEMA = TMP_QUERY_RADYO = TMP_QUERY_OUTDOOR = TMP_QUERY_INTERNET = "";
            QUERY_TELEVIZYON = QUERY_GAZETE = QUERY_DERGI = QUERY_SINEMA = QUERY_RADYO = QUERY_OUTDOOR = QUERY_INTERNET = ""; WHERES = "";
        }

        private void BTN_KAYDET_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //int index = xtraTabControl_MASTER_MNG_DETAY.SelectedTabPageIndex;
            //var rtb = xtraTabControl_MASTER_MNG_DETAY.TabPages[index].Controls[0];
            //DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
            //System.Data.DataView RW = (System.Data.DataView)grd_.DataSource;
            
            TARIFELER._GLOBAL_TARIFELER WORD = new TARIFELER._GLOBAL_TARIFELER();

            if (lbID.Caption != "")
            {
                SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                con.Open();
                if (RW != null)
                { 
                    // SATIR SİL
                    RW.RowStateFilter = DataViewRowState.Deleted;
                    if (RW.Count != 0)
                    {
                        for (int i = 0; i <= RW.Count - 1; i++)
                        {
                            DataRow DR = RW[i].Row;
                            WORD.KEYWORD_ROW_DELETE(con, DR, lbFILE_NAME.Caption,lbID.Caption);
                        }
                    }

                    // Yeni eklenmiş Satırları kaydet
                    RW.RowStateFilter = DataViewRowState.Added;
                    if (RW.Count != 0)
                    {
                        for (int i = 0; i <= RW.Count - 1; i++)
                        {
                            DataRow DR = RW[i].Row;
                            WORD.KEYWORD_ROW_ADD(con, DR, lbFILE_NAME.Caption,lbID.Caption);
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
                                WORD.KEYWORD_ROW_ADD(con, DR, lbFILE_NAME.Caption,lbID.Caption);
                            }
                            else
                            {
                                WORD.KEYWORD_ROW_UPDATE(con, DR, lbFILE_NAME.Caption,lbID.Caption);
                            }
                        }
                    }
                    RW.Table.AcceptChanges();
                    RW.RowStateFilter = DataViewRowState.CurrentRows;     
                    DATA_READ(lbFILE_NAME.Caption.ToString()); 
                }
            }
            if (lbID.Caption == "")
            {
                _TARIFE_KAYDET frm = new _TARIFE_KAYDET();
                frm.ShowDialog();
                lbFILE_NAME.Caption = frm._TXT_FILE_NAME.ToString();
                if (lbFILE_NAME.Caption.ToString() != "" && frm._BTN_TYPE == "Tamam")
                {
                    DateTime myDTStart = Convert.ToDateTime(DateTime.Now);
                    SqlConnection myConnectionKontrol = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                    string myInsertQueryKontrol = " select * from dbo.TRF_TARIFELER_LISTESI Where  TARIFE_TURU='KEYWORD' AND  SIRKET_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "' and TARIFE_KODU= '" + lbFILE_NAME.Caption + "' ";
                    SqlCommand myCommandKontrol = new SqlCommand(myInsertQueryKontrol);
                    myCommandKontrol.Connection = myConnectionKontrol;
                    myConnectionKontrol.Open();
                    SqlDataReader myReaderKontrol = myCommandKontrol.ExecuteReader(CommandBehavior.CloseConnection);
                    if (myReaderKontrol.HasRows == false)
                    {
                        using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            conn.Open();
                            SqlCommand myCmd = new SqlCommand();
                            myCmd.CommandText = "INSERT INTO dbo.TRF_TARIFELER_LISTESI(TARIFE_TURU,SIRKET_KODU, MECRA_TURU,TARIFE_KODU, TARIFE_OWNER,ISLEM_TARIHI,ISLEM_SAATI,FILITRE_TEXT,FILITRE_PATH)  VALUES ( @TARIFE_TURU,@SIRKET_KODU, @MECRA_TURU,@TARIFE_KODU, @TARIFE_OWNER,@ISLEM_TARIHI,@ISLEM_SAATI,@FILITRE_TEXT,@FILITRE_PATH ) SELECT @@IDENTITY AS ID ";
                            myCmd.Parameters.Add("@TARIFE_TURU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_TURU"].Value = "KEYWORD";
                            myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                            myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "KEYWORD";
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
                            myCmd.Connection = conn;
                            SqlDataReader myReader = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
                            while (myReader.Read())
                            {
                                lbID.Caption = myReader["ID"].ToString();
                            }
                            myReader.Close();
                            myCmd.Connection.Close();
                        }
                        SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        con.Open();
                        if (RW != null)
                        { 
                            // SATIR SİL
                            RW.RowStateFilter = DataViewRowState.Deleted;
                            if (RW.Count != 0)
                            {
                                for (int i = 0; i <= RW.Count - 1; i++)
                                {
                                    DataRow DR = RW[i].Row;
                                    DR.Delete();
                                }
                            }
                            RW.Table.AcceptChanges();
                            // Yeni eklenmiş Satırları kaydet
                            RW.RowStateFilter = DataViewRowState.CurrentRows;
                            if (RW.Count != 0)
                            {
                                for (int i = 0; i <= RW.Count - 1; i++)
                                {
                                    DataRow DR = RW[i].Row;
                                    WORD.KEYWORD_ROW_ADD(con, DR, lbFILE_NAME.Caption,lbID.Caption);
                                }
                            }
                            DATA_READ(lbFILE_NAME.Caption.ToString()); 
                            RW.Table.AcceptChanges();
                            RW.RowStateFilter = DataViewRowState.CurrentRows;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Bu kod daha önce kullanılmış! Lütfen farklı bir kod kullanınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                //else
                //    MessageBox.Show("Tarife kodu giriniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }  
        }
 
        private void re_BTN_KONTROL_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SQL = "";
            RAPORU_BASLAT("ICERIR");

            if (SQL != "")
            {
                SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());              
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnection);
                da.SelectCommand.CommandTimeout = 0;
                DataSet ds = new DataSet();
                da.Fill(ds, "stock");
                DataViewManager dvManager = new DataViewManager(ds);
                DW_LIST = dvManager.CreateDataView(ds.Tables[0]);
                GRD_DATA_KONTROL.DataSource = DW_LIST;
            } 
        }
        private void BTN_YENI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DATA_READ("");
        }

        private void re_BTN_UYGUN_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            SQL = "";
            RAPORU_BASLAT("ICERMEZ");  

            if (SQL != "")
            {
                SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnection);
                DataSet ds = new DataSet();
                da.Fill(ds, "stock");
                DataViewManager dvManager = new DataViewManager(ds);
                DW_LIST = dvManager.CreateDataView(ds.Tables[0]);
                GRD_DATA_KONTROL.DataSource = DW_LIST;
            } 
        } 
    }
}