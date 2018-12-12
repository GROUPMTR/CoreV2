using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoreV2.TARIFELER
{
    public partial class _TARIFE_LISTESI : DevExpress.XtraEditors.XtraForm
    {
        public string _TARIFE_TURU, _MECRA_TURU, _TARIFE_KODU, _TARIFE_ID, _BASLIKLAR,_MECRA_TURLERI, _FILITRE_TEXT, _FILITRE_PATH, _BTN_TYPE; 
        public _TARIFE_LISTESI(string TARIFE_TURU, string MECRA_TURU)
        {
            InitializeComponent(); 
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            if (TARIFE_TURU == "MASTER") this.Text = "Master Tarife Listesi"; else this.Text = "Mecra Tarife Listesi";

            _TARIFE_TURU = TARIFE_TURU;  
            
            
                  
            _TARIFELER(TARIFE_TURU, MECRA_TURU);

        }

        private void _TARIFELER(string TARIFE_TURU, string MECRA_TURU)
        {
            string SQL = "";
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet ds = new DataSet();


                //if (TARIFE_TURU != "MASTER" || TARIFE_TURU != "KEYWORD")
                //    SQL = string.Format("SELECT * FROM  dbo.TRF_TARIFELER_LISTESI where TARIFE_TURU='{0}' AND  SIRKET_KODU='{1}' AND TARIFE_OWNER='{2}' AND MECRA_TURU='{3}' order by MECRA_TURU ", TARIFE_TURU, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL, MECRA_TURU);



                if (TARIFE_TURU == "MASTER")
                { SQL = string.Format("SELECT * FROM  dbo.TRF_TARIFELER_LISTESI where TARIFE_TURU='{0}' AND  SIRKET_KODU='{1}' AND TARIFE_OWNER='{2}' AND MECRA_TURU='{3}' order by MECRA_TURU ", TARIFE_TURU, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL, MECRA_TURU);
                }
                if (TARIFE_TURU == "KEYWORD")
                {
                    SQL = string.Format("SELECT * FROM  dbo.TRF_TARIFELER_LISTESI where TARIFE_TURU='{0}' AND  SIRKET_KODU='{1}' AND TARIFE_OWNER='{2}' AND MECRA_TURU='{3}' order by MECRA_TURU ", TARIFE_TURU, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL, MECRA_TURU);

                }
                if (TARIFE_TURU != "MASTER" || TARIFE_TURU != "KEYWORD")
                {
                    SQL = string.Format("SELECT * FROM  dbo.TRF_TARIFELER_LISTESI where TARIFE_TURU='{0}' AND  SIRKET_KODU='{1}' AND TARIFE_OWNER='{2}'  order by MECRA_TURU ", TARIFE_TURU, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                }


                //SQL = string.Format("SELECT * FROM  dbo.TRF_TARIFELER_LISTESI where TARIFE_TURU='{0}' AND  SIRKET_KODU='{1}' AND TARIFE_OWNER='{2}' AND MECRA_TURU='{3}' order by MECRA_TURU ", TARIFE_TURU , _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL , MECRA_TURU);
                //else



                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(SQL, conn) };
                adapter.Fill(ds, "TRF_TARIFELER");
                DataViewManager dvManager = new DataViewManager(ds);
                DataView dv_TVTARIFESI = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_TARIFE_LISTESI.DataSource = dv_TVTARIFESI; 
            }
   
        }

        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _BTN_TYPE = "Close";
            Close();
        }

        private void BTN_DELETE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information); 
            if (c == DialogResult.Yes)
            {
                if (_TARIFE_TURU == "MASTER")
                {
                    using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string SQL = " DELETE  FROM  dbo.TRF_TARIFELER_LISTESI WHERE (ID='" + lbID.Caption + "') AND  (TARIFE_KODU='" + _TARIFE_KODU + "') AND  (TARIFE_OWNER = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') AND (SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') ;";
                               SQL += string.Format("IF EXISTS (SELECT * FROM dbo.sysobjects where id = object_id('[dbo].[__MAS_EDT_{0}_{1}]')) drop table [dbo].[__MAS_EDT_{0}_{1}]", lbID.Caption, _TARIFE_KODU);

                        SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                        myConnection.Open();
                        myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        myConnection.Close();
                    }
                }
                else
                {
                    using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string SQL = " DELETE  FROM  dbo.TRF_TARIFELER_LISTESI WHERE (ID='" + lbID.Caption + "') AND  (TARIFE_KODU='" + _TARIFE_KODU + "')  AND  (TARIFE_OWNER = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') AND (SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') ";
                              SQL += " DELETE  FROM  dbo.TRF_" + _MECRA_TURU + " WHERE (TARIFE_KODU='" + _TARIFE_KODU + "')    AND (SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "')";
                        SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                        myConnection.Open();
                        myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                        myConnection.Close();
                    }

                }
                _TARIFELER(_TARIFE_TURU, _MECRA_TURU);
            }
        } 
            
        private void gridControl_TARIFE_LISTESI_Click(object sender, EventArgs e)
        {
            DataRow dr = gridView_TARIFE_LISTESI.GetFocusedDataRow();
            if (dr != null)
            {
                _TARIFE_KODU = dr["TARIFE_KODU"].ToString();
                lbID.Caption = dr["ID"].ToString();
                lbCODE.Caption = dr["TARIFE_KODU"].ToString();
                _MECRA_TURU = dr["MECRA_TURU"].ToString();
                _TARIFE_ID = dr["ID"].ToString();
                _BASLIKLAR = dr["BASLIKLAR"].ToString();
                _MECRA_TURLERI = dr["MECRA_TURU"].ToString();
                TARIFE_CHANGE(_MECRA_TURU);
            }
        }


        public void TARIFE_CHANGE(string MECRA_TURU)
        {
            gridView.Columns.Clear();
            gridControl_TARIFE.DataSource = null;
            if (MECRA_TURU == "TELEVIZYON")
            {


                string query = string.Format("SELECT   top 100 *   FROM  dbo.TRF_TELEVIZYON where  SIRKET_KODU='{0}' and TARIFE_KODU='{1}' and KULLANICI_KODU='{2}' order by MECRA_KODU", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU,_GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                        adapter.Fill(ds, "TRF_TELEVIZYON");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                        gridControl_TARIFE.DataSource = DW_LIST_TARIFE;
                    }
              
      

            }
            if (MECRA_TURU == "NONETELEVIZYON")
            {
              
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string query = string.Format("  SELECT  top 100 * FROM     dbo.TRF_TELEVIZYON_NONE_MEASURED where  SIRKET_KODU='{0}' and TARIFE_KODU='{1}'  and KULLANICI_KODU='{2}'  ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                        adapter.Fill(ds, "TRF_TELEVIZYON_NONE_MEASURED");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                        gridControl_TARIFE.DataSource = DW_LIST_TARIFE;
                    }
              
                
            }
            if (MECRA_TURU == "RADYO")
            {
 
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string query = string.Format("      SELECT   top 100 *  FROM         dbo.TRF_RADYO where  SIRKET_KODU='{0}' and TARIFE_KODU='{1}'  and KULLANICI_KODU='{2}' ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                        adapter.Fill(ds, "TRF_RADYO");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                        gridControl_TARIFE.DataSource = DW_LIST_TARIFE;
                    }

                
              
            }
            if (MECRA_TURU == "GAZETE")
            {
                DataView DW_LIST_TARIFE;
               

                string STR_SECENEKLER_BIR = "", GAZETE_TARIFE_TURU = "STANDART_TARIFE";
                string STR_SECENEKLER_IKI = "", GAZETE_TARIFE_SECENEK="";
             
                    for (int i = 0; i <= gridView.Columns.Count - 1; i++)
                    {
                        gridView.Columns[i].Visible = false;
                    }

                    using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string SQL = string.Format(" SELECT top 100 * FROM         dbo.TRF_TARIFELER_LISTESI WHERE   TARIFE_OWNER='{0}' and  (SIRKET_KODU = N'{1}') AND (MECRA_TURU = N'GAZETE') and TARIFE_KODU='{2}' ", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU);
                        SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                        myConnection.Open();
                        SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);

                        while (myReader.Read())
                        {
                            //  Mecra Kodu	Yayın Türü	Hesap Türü	BİRİM FİYAT/FAKTÖR	Pzt	Sal	Çar	Per	Cum	Cmt	Paz	 +/-	BAŞLANGIÇ TARİHİ	BİTİŞ TARİHİ

                            if (myReader["GUNLUK_TARIFE"] == DBNull.Value || myReader["GUNLUK_TARIFE"].ToString() == "False")
                            {
                                STR_SECENEKLER_IKI = "BIRIM_FIYAT,";
                                GAZETE_TARIFE_TURU = "STANDART_TARIFE";
                            }
                            else
                            {
                                STR_SECENEKLER_IKI = "BIRIM_FIYAT,PAZARTESI,SALI,CARSAMBA,PERSEMBE,CUMA,CUMARTESI,PAZAR, ";
                                GAZETE_TARIFE_TURU = "GUNLUK_TARIFE";
                            }
                            string TYPE = myReader["SELECT_TYPE"].ToString();
                            if (TYPE == "MECRA") { STR_SECENEKLER_BIR = "MECRA_KODU,HESAPLANMA_TURU,"; GAZETE_TARIFE_SECENEK = "MECRA"; }
                            if (TYPE == "MECRA_SAYFA") { STR_SECENEKLER_BIR = "MECRA_KODU,SAYFA_GRUBU,HESAPLANMA_TURU,"; GAZETE_TARIFE_SECENEK = "MECRA_SAYFA"; }
                            if (TYPE == "MECRA_SAYFA_YAYINTURU") { STR_SECENEKLER_BIR = "MECRA_KODU,SAYFA_GRUBU,YAYIN_TURU,HESAPLANMA_TURU,"; GAZETE_TARIFE_SECENEK = "MECRA_SAYFA_YAYINTURU"; }
                        }

                        string qury = String.Format("{0}{1}BASLANGIC_TARIHI,BITIS_TARIHI", STR_SECENEKLER_BIR, STR_SECENEKLER_IKI);
                        DataSet ds = new DataSet();
                        using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            string querys = String.Format(" SELECT  top 100 ID, {0} {1}  BASLANGIC_TARIHI , BITIS_TARIHI   FROM     dbo.TRF_GAZETE where  SIRKET_KODU='{2}' and TARIFE_KODU='{3}'  and KULLANICI_KODU='{4}'", STR_SECENEKLER_BIR, STR_SECENEKLER_IKI, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                            SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(querys, conn) };
                            adapter.Fill(ds, "TRF_GAZETE");
                            DataViewManager dvManager = new DataViewManager(ds);
                            DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                        }
                        gridControl_TARIFE.DataSource = DW_LIST_TARIFE;
                        gridView.RefreshData();

                        string[] Onesz = qury.Split(',');
                        for (int i = 0; i < Onesz.Length; i++)
                        {
                            gridView.Columns[Onesz[i].Trim()].VisibleIndex = i;
                            gridView.Columns[Onesz[i].Trim()].Visible = true;
                        }
                    }
             

         
            }
            if (MECRA_TURU == "DERGI")
            {
             
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string query = string.Format("      SELECT   top 100 *  FROM         dbo.TRF_DERGI where  SIRKET_KODU='{0}' and TARIFE_KODU='{1}'  and KULLANICI_KODU='{2}' ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                        adapter.Fill(ds, "TRF_DERGI");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                        gridControl_TARIFE.DataSource = DW_LIST_TARIFE;
                    }
                
              
 
            }
            if (MECRA_TURU == "SINEMA")
            {
           
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string query = string.Format(" SELECT  top 100 *  FROM     dbo.TRF_SINEMA where  SIRKET_KODU='{0}' and TARIFE_KODU='{1}'  and KULLANICI_KODU='{2}' ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                        adapter.Fill(ds, "TRF_SINEMA");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                        gridControl_TARIFE.DataSource = DW_LIST_TARIFE;
                    }
              
            }

            if (MECRA_TURU == "OUTDOOR")
            {
              
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string query = string.Format("SELECT  top 100  *  FROM  dbo.TRF_OUTDOOR where SIRKET_KODU='{0}' and TARIFE_KODU='{1}'  and KULLANICI_KODU='{2}' ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                        adapter.Fill(ds, "TRF_OUTDOOR");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                        gridControl_TARIFE.DataSource = DW_LIST_TARIFE;
                    }
                
            }
            if (MECRA_TURU == "INTERNET")
            {
             
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string query = string.Format("      SELECT  top 100  *  FROM     dbo.TRF_INTERNET where  SIRKET_KODU='{0}' and TARIFE_KODU='{1}'  and KULLANICI_KODU='{2}' ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                        adapter.Fill(ds, "TRF_INTERNET");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                        gridControl_TARIFE.DataSource = DW_LIST_TARIFE;
                    }
               
            }
            if (MECRA_TURU == "SEKTOR")
            {
            
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string query = string.Format("      SELECT  top 100  *  FROM     dbo.TRF_SEKTOR where  SIRKET_KODU='{0}' and TARIFE_KODU='{1}'  and KULLANICI_KODU='{2}' ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                        adapter.Fill(ds, "TRF_SEKTOR");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                        gridControl_TARIFE.DataSource = DW_LIST_TARIFE;
                    }
           
            }
            if (MECRA_TURU == "PROGRAM")
            {
   
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string query = string.Format("  SELECT  top 100  * FROM     dbo.TRF_PROGRAM_TARIFESI where  SIRKET_KODU='{0}' and TARIFE_KODU='{1}'  and KULLANICI_KODU='{2}' ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                        adapter.Fill(ds, "TRF_ORANLAR");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                        gridControl_TARIFE.DataSource = DW_LIST_TARIFE;

                    }
             
            }
            if (MECRA_TURU == "ORAN")
            {
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string query = string.Format("  SELECT top 100 * FROM     dbo.TRF_ORANLAR where  SIRKET_KODU='{0}' and TARIFE_KODU='{1}'  and KULLANICI_KODU='{2}'  ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                        adapter.Fill(ds, "TRF_ORANLAR");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DataView DW_LIST_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                        gridControl_TARIFE.DataSource = DW_LIST_TARIFE;
                    }
            }

            if (gridView.Columns.Count > 5)
            {
                for (int i = 0; i <= 5; i++)
                { gridView.Columns[i].Visible = false; }
            }
        }

        private void gridControl_TARIFE_LISTESI_DoubleClick(object sender, EventArgs e)
        {
          
            DataRow dr = gridView_TARIFE_LISTESI.GetFocusedDataRow();
            if (dr != null)
            { 
                    lbID.Caption = dr["ID"].ToString();
                    lbCODE.Caption = dr["TARIFE_KODU"].ToString();
                    _TARIFE_KODU = dr["TARIFE_KODU"].ToString();
                    _MECRA_TURU = dr["MECRA_TURU"].ToString();
                    _TARIFE_ID = dr["ID"].ToString();
                    _BASLIKLAR = dr["BASLIKLAR"].ToString();
                    _FILITRE_TEXT = dr["FILITRE_TEXT"].ToString();
                    _FILITRE_PATH = dr["FILITRE_PATH"].ToString(); 
         
            } 
            _BTN_TYPE = "Tamam";
            Close(); 
        } 
        private void BTN_TAMAM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _BTN_TYPE = "Tamam";
            Close(); 
        } 
    }
}