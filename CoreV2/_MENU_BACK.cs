using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoreV2
{
    public partial class _MENU_BACK : DevExpress.XtraEditors.XtraForm
    {
        public string _BTN_TYPE = "", SELECT_ID = "", SELECT_RAPOR_KODU = "", SELECT_TELEVIZYON = "", SELECT_RADYO = "",
                        SELECT_GAZETE = "", SELECT_DERGI = "", SELECT_SINEMA = "", SELECT_OUTDOOR = "", SELECT_INTERNET = "",
                        SELECT_SEKTOR = "", SELECT_MECRA_TURLERI = "", SELECT_SABIT_SECENEKLER = "", SELECT_RAPOR_SECENEKLER = "",
                        SELECT_MECRALAR = "", SELECT_RAPOR_ACIKLAMASI = "", SELECT_RAPOR_TURU = "O", SELECT_OWNER="",
                        SELECT_RAPOR_DURUMU = string.Empty, SELECT_FTP_ADRESI="", SELECT_FTP_USERNAME="", SELECT_FTP_PASSWORD="";

  

        public bool SELECT_OTUZ_SN_GRP = false, SELECT_ON_BES_DK_RATING = false, SELECT_FTP_DURUMU=false;

    

        public string _TXT_FILE_NAME = "", _TXT_ACIKLAMA = "", SELECT_RAPOR_PATH, SELECT_RAPOR_AUTO_SAVE;  
        string RLISTESI = string.Empty; 

        public _MENU_BACK(string SECENEK,string RAPOR_KODU)
        {
            InitializeComponent();  
            TXT_DFLT_RAPOR_KODU.Text = RAPOR_KODU; 
                CMB_FRK_FIRMA.Enabled = true;
                CMB_FRK_KULLANICI.Enabled = true;
                SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                SqlCommand myCommand = new SqlCommand("  select *  from  ADM_FIRMALAR    ", myConnection);
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                while (myReader.Read())
                {
                    CMB_FRK_FIRMA.Items.Add(myReader["FIRMA"].ToString());
                }  
            CMB_FRK_FIRMA.Text = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
            CMB_FRK_KULLANICI.Text = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
            RLISTESI = "TÜMÜ";
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            if (RLISTESI == "TÜMÜ") RAPOR_LIST_TUMU(); 

           backstageViewControls.SelectedTab =null;


            if (SECENEK == "LISTE") backstageViewControls.SelectedTab = bck_RAPORLAR;
            if (SECENEK == "KAYDET") backstageViewControls.SelectedTab = bck_KAYDET;
            if (SECENEK == "FARKLI_KAYDET") backstageViewControls.SelectedTab = bck_FARKLI_KAYDET;
            if (SECENEK == "HAKKINDA") backstageViewControls.SelectedTab = bck_HAKKINDA;
            if (SECENEK == "AYARLAR") backstageViewControls.SelectedTab = bck_AYARLAR;
            if (SECENEK == "RAPOR_PAYLAS") backstageViewControls.SelectedTab = bck_RAPOR_PAYLAS;
        }

        private void RAPOR_LIST_SABIT()
        {
            string SQL = " SELECT * FROM   dbo.ADM_RAPOR_DESIGNE  WHERE (DURUMU='SABIT') and  (SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') AND (OWNER_MAIL = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') order by  RAPOR_KODU  ";

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlDataAdapter da = new SqlDataAdapter(SQL, myConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "stock");
            DataViewManager dvManager = new DataViewManager(ds);
            DataView dv = dvManager.CreateDataView(ds.Tables[0]);
            gridControl_LISTE.DataSource = dv;
        }
        private void RAPOR_LIST_TUMU()
        {
            string SQL = string.Format(" SELECT * , DATEPART ( wk , ERISIM_TARIHI ) as  ERISIM_WEEK  FROM   dbo.ADM_RAPOR_DESIGNE  WHERE  (SIRKET_KODU = N'{0}') AND (OWNER_MAIL = N'{1}'   )  ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                   SQL +=string.Format(" UNION ALL  SELECT * , DATEPART ( wk , ERISIM_TARIHI ) as  ERISIM_WEEK  FROM   dbo.ADM_RAPOR_DESIGNE  WHERE  (RAPOR_TURU = N'{0}')    order by  RAPOR_KODU ", 'P');
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlDataAdapter da = new SqlDataAdapter(SQL, myConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "stock");
            DataViewManager dvManager = new DataViewManager(ds);
            DataView dv = dvManager.CreateDataView(ds.Tables[0]);
            gridControl_LISTE.DataSource = dv;
            gridView_LISTE.ExpandAllGroups();
        }
        private void gridControl_LISTE_Click(object sender, EventArgs e)
        {
            DataRow DR = gridView_LISTE.GetDataRow(gridView_LISTE.FocusedRowHandle);
            if (DR != null)
            {
                _BTN_TYPE = "Tamam";
                SELECT_ID = DR["ID"].ToString();
                SELECT_RAPOR_DURUMU = DR["DURUMU"].ToString();
                SELECT_RAPOR_KODU = DR["RAPOR_KODU"].ToString();
                SELECT_RAPOR_ACIKLAMASI = DR["ACIKLAMA"].ToString();
                SELECT_RAPOR_PATH = DR["EXPORT_FILE_ADRES"].ToString();
                SELECT_RAPOR_AUTO_SAVE = DR["AUTO_SAVE"].ToString();
                SELECT_RAPOR_TURU = DR["RAPOR_TURU"].ToString();
                SELECT_OWNER = DR["OWNER_MAIL"].ToString();
                SELECT_FTP_DURUMU = (bool)DR["FTP_DURUMU"];
                SELECT_FTP_ADRESI = DR["FTP_ADRESI"].ToString();
                SELECT_FTP_USERNAME = DR["FTP_USERNAME"].ToString();
                SELECT_FTP_PASSWORD = DR["FTP_PASSWORD"].ToString();
            }
        }
        private void gridControl_LISTE_DoubleClick(object sender, EventArgs e)
        {
      
            Close();
        }
        private void CMB_FRK_FIRMA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CMB_FRK_KULLANICI.Items.Clear();
            CMB_FRK_KULLANICI.Text = "";
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            string SQL = string.Format("  select *  from  ADM_KULLANICILAR   where FIRMA='{0}'", CMB_FRK_FIRMA.Text);
            SqlCommand myCommand = new SqlCommand(SQL, myConnection);      
            myConnection.Open();
            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            while (myReader.Read())
            {
                CMB_FRK_KULLANICI.Items.Add(myReader["EMAIL"].ToString());
            }
        }
        private void backstageViewControl1_BackButtonClick(object sender, EventArgs e)
        {
            _BTN_TYPE = "Close";
            Close();
        }

        private void bck_HAKKINDA_ItemPressed(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LBL_HEADER.Text = "Rapor Hakkında";
        }

        private void bck_RAPORLAR_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LBL_HEADER.Text = "Raporlar Listesi";
        }

        private void bck_FARKLI_KAYDET_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LBL_HEADER.Text = "Farklı Kaydet";
        }

        private void bck_KAYDET_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LBL_HEADER.Text = "Kaydet";
        }

        private void bck_AYARLAR_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LBL_HEADER.Text = "Ayarlar";
        }

        private void bck_HAKKINDA_SelectedChanged(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            LBL_HEADER.Text = "Rapor Hakkında";
        }

        private void bck_PROGRAMI_KAPAT_ItemClick(object sender, DevExpress.XtraBars.Ribbon.BackstageViewItemEventArgs e)
        {
            Application.ExitThread();
        } 

        private void BTN_TB_FARKLI_KAYDET_Click(object sender, EventArgs e)
        {
            if (TXT_FRK_RAPOR_KODU.Text.Length > 0)
            {
                if (_TXT_FILE_NAME != TXT_FRK_RAPOR_KODU.Text)
                {
                    _BTN_TYPE = "Tamam";
                    _TXT_FILE_NAME = TXT_FRK_RAPOR_KODU.Text;
                    _TXT_ACIKLAMA = TXT_FRK_ACIKLAMA.Text;
                    Close();
                }
                else
                {
                    MessageBox.Show("Farklı bir isim giriniz aynı isimle kaydedilemez.", "Uyarı",    MessageBoxButtons.YesNo, MessageBoxIcon.Information);                    
                }
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            if (TXT_FILE_NAME.Text.Length > 0)
            { 
                _BTN_TYPE = "Tamam";
                _TXT_FILE_NAME = TXT_FILE_NAME.Text;
                _TXT_ACIKLAMA = txt_RAPOR_ACIKLAMASI.Text;
                Close();

            }
        }



        private void BTN_RAPOR_SIL_Click(object sender, EventArgs e)
        {
     
            DataRow DR = gridView_LISTE.GetDataRow(gridView_LISTE.FocusedRowHandle);
            if (DR != null)
            {
                if (DR["DURUMU"].ToString() != "SABIT")
                {
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı",
                        MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            string SQL = string.Format(" DELETE  FROM  dbo.ADM_RAPOR_BASLIK WHERE  (RAPOR_KODU='{0}') AND   (SIRKET_KODU = N'{1}') ; ", DR["RAPOR_KODU"].ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                            SQL += string.Format(" DELETE  FROM  dbo.ADM_RAPOR_DESIGNE WHERE    (RAPOR_KODU='{0}') AND   (SIRKET_KODU = N'{1}') ;  ", DR["RAPOR_KODU"].ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                            SQL += string.Format(" DELETE  FROM  dbo.ADM_RAPOR_FILITRE WHERE    (RAPOR_KODU='{0}') AND   (SIRKET_KODU = N'{1}') ;  ", DR["RAPOR_KODU"].ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                            SQL += string.Format(" DELETE  FROM  dbo.ADM_RAPOR_KIRILIM WHERE    (RAPOR_KODU='{0}') AND   (SIRKET_KODU = N'{1}') ; ", DR["RAPOR_KODU"].ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                            SQL += string.Format(" DELETE  FROM  dbo.ADM_RAPOR_OLCUMLEME WHERE  (RAPOR_KODU='{0}') AND   (SIRKET_KODU = N'{1}')   ", DR["RAPOR_KODU"].ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                            SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                            myConnection.Open();
                            myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                            myConnection.Close();

                            DR.Delete();
                        }

                    }
                }
                else
                {
                    MessageBox.Show("Sabit Raporlar Silinemez", "Dikkat", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void BTN_RAPOR_TEMP_Click(object sender, EventArgs e)
        {
            DialogResult c = MessageBox.Show("Raporu Temp olarak değiştirmek önceki raporunuz silinecektir, eminmisiniz?", "Uyarı",
        MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (c == DialogResult.Yes)
            {
                using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQL = string.Format(" update   dbo.ADM_RAPOR_DESIGNE  set DURUMU='TEMP' WHERE  (RAPOR_KODU='{0}') AND   (SIRKET_KODU = N'{1}')   ", SELECT_RAPOR_KODU, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                    SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                    myConnection.Open();
                    myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    myConnection.Close();
                }

                //ParentForm. 
                //this.ParentForm.Invoke.barManagers.BR_RAPOR_TYPE 
                //this.ParentForm.GetType().InvokeMember("BR_RAPOR_TYPE"); 
                //   var asd=  ((Form)this.ParentForm); 
                //  this.ParentForm.GetType().Name["BR_RAPOR_TYPE"]. 

               // this.ParentForm.GetType().InvokeMember("_REFRESH", System.Reflection.BindingFlags.InvokeMethod, null, this.ParentForm, new object[] { "TEMP" });

                RAPOR_LIST_TUMU();
            }
        }

        private void BTN_RAPOR_SABITLE_Click(object sender, EventArgs e)
        {
            DialogResult c = MessageBox.Show("Raporu sabitlemek istediğinize eminmisiniz?", "Uyarı",
       MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (c == DialogResult.Yes)
            {
                using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQL = string.Format(" update   dbo.ADM_RAPOR_DESIGNE set  DURUMU='SABIT'  WHERE   (RAPOR_KODU='{0}') AND   (SIRKET_KODU = N'{1}')   ", SELECT_RAPOR_KODU, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                    SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                    myConnection.Open();
                    myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    myConnection.Close();
                }
               // this.ParentForm.GetType().InvokeMember("_REFRESH", System.Reflection.BindingFlags.InvokeMethod, null, this.ParentForm, new object[] { "SABIT" });
                RAPOR_LIST_TUMU();
            }
        }

        private void BTN_TB_PAYLAS_Click(object sender, EventArgs e)
        {
            if (TXT_SHR_RAPOR_KODU.Text.Length > 0)
            {
                if (_TXT_FILE_NAME != TXT_SHR_RAPOR_KODU.Text)
                {
                    _BTN_TYPE = "Tamam";
                    _TXT_FILE_NAME = TXT_SHR_RAPOR_KODU.Text;
                    _TXT_ACIKLAMA = TXT_SHR_RAPOR_ACIKLAMA.Text;
                    Close();
                }
                else
                {
                    MessageBox.Show("Farklı bir isim giriniz aynı isimle kaydedilemez.", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                }
            }
        }
    }
}