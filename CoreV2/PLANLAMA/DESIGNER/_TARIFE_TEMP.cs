using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoreV2.PLANLAMA.DESIGNER
{
    public partial class _TARIFE_TEMP : UserControl
    {
        /// <summary>
        /// TARIFE DATA VIEW 
        /// </summary>
        public DataView DW_LIST_TELEVIZYON_TARIFE, DW_LIST_NONE_MEASURED_TV, DW_LIST_RADYO_TARIFE, DW_LIST_GAZETE_TARIFE, DW_LIST_DERGI_TARIFE,
                        DW_LIST_SINEMA_TARIFE, DW_LIST_OUTDOOR_TARIFE, DW_LIST_INTERNET_TARIFE, DW_LIST_SEKTOR_TARIFE, DW_LIST_PROGRAM_TARIFESI, DW_LIST_ORANLAR;


        TARIFELER._GLOBAL_TARIFELER GLOBAL = new TARIFELER._GLOBAL_TARIFELER();// GAZETE_TARIFE_TURU = "STANDART_TARIFE"
        public string TARIFE_KODU = "", TARIFE_ID = "", SELECT_SABIT_SECENEKLER = "", GAZETE_TARIFE_SECENEK = "MECRA";

        //private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManagers;  
        public _TARIFE_TEMP()
        {
            InitializeComponent();
            HEDEFKITLE_LISTESI();

        }
        private void openToolStripButton_Click(object sender, EventArgs e)
        {
            string MECRA_TURU = "";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon") MECRA_TURU = "TELEVIZYON";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp") MECRA_TURU = "NONEGRP";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo") MECRA_TURU = "RADYO";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete") MECRA_TURU = "GAZETE";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi") MECRA_TURU = "DERGI";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema") MECRA_TURU = "SINEMA";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor") MECRA_TURU = "OUTDOOR";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Program") MECRA_TURU = "PROGRAM";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar") MECRA_TURU = "ORANLAR";

            TARIFELER._TARIFE_LISTESI TF = new TARIFELER._TARIFE_LISTESI("TARIFE", MECRA_TURU);
            TF.ShowDialog();
            TARIFE_KODU = TF._TARIFE_KODU;
            TARIFE_ID = TF._TARIFE_ID;
            TARIFE_CHANGE(TF._MECRA_TURU);
            this.ParentForm.GetType().InvokeMember("ZORUNLU_ALANLARI_ISARETLE", System.Reflection.BindingFlags.InvokeMethod, null, this.ParentForm, new object[] { TF._MECRA_TURU });
        }
        private void HEDEFKITLE_LISTESI()
        {
            re_TV_TARGET.Items.Clear();
            re_TV_TARGET.Items.Add("");
            using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlCommand myCommand = new SqlCommand("SELECT     FIELDS, SECENEK FROM         dbo.ADM_SECENEKLER WHERE     (BASLIK = N'HEDEFKİTLELER')", con);
                con.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReader.Read())
                {
                    re_TV_TARGET.Items.Add(myReader["SECENEK"].ToString());
                }
            }
        }

        private void CNT_KAYDET_Click(object sender, EventArgs e)
        {
            TARIFE_KAYDET();
        }
        private void saveToolStripButton_Click(object sender, EventArgs e)
        {
            TARIFE_KAYDET();
        }

        private void CNT_KOPYALA_Click(object sender, EventArgs e)
        {
            TARIFE_COPY();
        }
        private void copyToolStripButton_Click(object sender, EventArgs e)
        {
            TARIFE_COPY();
        }
        private void pasteToolStripButton_Click(object sender, EventArgs e)
        {
            TARIFE_YAPISTIR();
        }

        private void CNT_YAPISTIR_Click(object sender, EventArgs e)
        {
            TARIFE_YAPISTIR();
        }

        private void CNT_SATIR_SIL_Click(object sender, EventArgs e)
        {
            TARIFE_SATIR_SIL();
        }

        private void NewToolStripButton_Click(object sender, EventArgs e)
        {
            string MECRA_TURU = "";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
            {
                MECRA_TURU = "TELEVIZYON";
                TARIFELER.TELEVIZYON TF = new TARIFELER.TELEVIZYON();
                TF.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TF.ShowDialog();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
            {
                MECRA_TURU = "NONEGRP";
                TARIFELER.NONE_GRP TF = new TARIFELER.NONE_GRP();
                TF.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TF.ShowDialog();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            {
                MECRA_TURU = "RADYO";
                TARIFELER.RADYO TF = new TARIFELER.RADYO();
                TF.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TF.ShowDialog();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
            {
                MECRA_TURU = "GAZETE";
                TARIFELER.GAZETE TF = new TARIFELER.GAZETE();
                TF.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TF.ShowDialog();
            }


            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
            {
                MECRA_TURU = "DERGI";
                TARIFELER.DERGI TF = new TARIFELER.DERGI();
                TF.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TF.ShowDialog();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
            {
                MECRA_TURU = "SINEMA";
                TARIFELER.SINEMA TF = new TARIFELER.SINEMA();

                TF.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TF.ShowDialog();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
            {
                MECRA_TURU = "OUTDOOR";
                TARIFELER.OUTDOOR TF = new TARIFELER.OUTDOOR();
                TF.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TF.ShowDialog();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Program")
            {
                MECRA_TURU = "PROGRAM";
                TARIFELER.PROGRAM TF = new TARIFELER.PROGRAM();
                TF.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TF.ShowDialog();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar")
            {
                MECRA_TURU = "ORANLAR";
                TARIFELER.ORANLAR TF = new TARIFELER.ORANLAR();
                TF.WindowState = System.Windows.Forms.FormWindowState.Maximized;
                TF.ShowDialog();
            }

            //TARIFELER._TARIFE_LISTESI TF = new TARIFELER._TARIFE_LISTESI("TARIFE", MECRA_TURU);
            //TF.ShowDialog();
            //TARIFE_KODU = TF._TARIFE_KODU;
            //TARIFE_ID = TF._TARIFE_ID;
            //TARIFE_CHANGE(TF._MECRA_TURU);


        }

        private void helpToolStripButton_Click(object sender, EventArgs e)
        {
            INFO.INFO_PAGE PG = new INFO.INFO_PAGE();
            PG.ShowDialog();
        }

        private void xtraTabControl_TARIFE_DETAY_SelectedPageChanged(object sender, DevExpress.XtraTab.TabPageChangedEventArgs e)
        {

        }

        private void saveAllToolStripButton_Click(object sender, EventArgs e)
        {
            TARIFE_KAYDET_TUMUNU();
        }

        private void cutToolStripButton_Click(object sender, EventArgs e)
        {
            TARIFE_SATIR_SIL();
        }

        private void CNT_TUM_TARIFEYI_SIL_Click_1(object sender, EventArgs e)
        {
            TUMUNU_SIL();
        }

        private void CNT_KODU_DEGISTIR_Click(object sender, EventArgs e)
        {
            DateTime myDTStart = Convert.ToDateTime(DateTime.Now);
            TARIFELER._TARIFE_KODU_DEGISTIR frm = new TARIFELER._TARIFE_KODU_DEGISTIR();

            string MECRA_TURU = "";
            string TARIFE_TABLOSU = "";
            string TARIFE_ID = "";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage == null) return;
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
            {
                if (LBL_TELEVIZYON_TARIFE_KODU.Text.Length > 0)
                {
                    MECRA_TURU = "TELEVIZYON";
                    TARIFE_TABLOSU = "TRF_TELEVIZYON";
                    frm.TXT_FILE_NAME_ESKI.Text = LBL_TELEVIZYON_TARIFE_KODU.Text;
                    TARIFE_ID = LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString();
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
            {
                if (LBL_NONETV_TARIFE_KODU.Text.Length > 0)
                {
                    MECRA_TURU = "NONEGRP";
                    TARIFE_TABLOSU = "TRF_TELEVIZYON_NONE_MEASURED";
                    frm.TXT_FILE_NAME_ESKI.Text = LBL_NONETV_TARIFE_KODU.Text;
                    TARIFE_ID = LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString();
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            {
                if (LBL_RADYO_TARIFE_KODU.Text.Length > 0)
                {
                    MECRA_TURU = "RADYO";
                    TARIFE_TABLOSU = "TRF_RADYO";
                    frm.TXT_FILE_NAME_ESKI.Text = LBL_RADYO_TARIFE_KODU.Text;
                    TARIFE_ID = LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString();
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
            {
                if (LBL_GAZETE_TARIFE_KODU.Text.Length > 0)
                {
                    MECRA_TURU = "GAZETE";
                    TARIFE_TABLOSU = "TRF_GAZETE";
                    frm.TXT_FILE_NAME_ESKI.Text = LBL_GAZETE_TARIFE_KODU.Text;
                    TARIFE_ID = LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString();
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
            {
                if (LBL_DERGI_TARIFE_KODU.Text.Length > 0)
                {
                    MECRA_TURU = "DERGI";
                    TARIFE_TABLOSU = "TRF_DERGI";
                    frm.TXT_FILE_NAME_ESKI.Text = LBL_DERGI_TARIFE_KODU.Text;
                    TARIFE_ID = LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString();
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
            {
                if (LBL_SINEMA_TARIFE_KODU.Text.Length > 0)
                {
                    MECRA_TURU = "SINEMA";
                    TARIFE_TABLOSU = "TRF_SINEMA";
                    frm.TXT_FILE_NAME_ESKI.Text = LBL_SINEMA_TARIFE_KODU.Text;
                    TARIFE_ID = LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString();
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
            {
                if (LBL_OUTDOOR_TARIFE_KODU.Text.Length > 0)
                {
                    MECRA_TURU = "OUTDOOR";
                    TARIFE_TABLOSU = "TRF_OUTDOOR";
                    frm.TXT_FILE_NAME_ESKI.Text = LBL_OUTDOOR_TARIFE_KODU.Text;
                    TARIFE_ID = LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString();
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar")
            {
                if (LBL_ORANLAR_TARIFE_KODU.Text.Length > 0)
                {
                    MECRA_TURU = "ORANLAR";
                    TARIFE_TABLOSU = "TRF_ORANLAR";
                    frm.TXT_FILE_NAME_ESKI.Text = LBL_ORANLAR_TARIFE_KODU.Text;
                    TARIFE_ID = LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString();
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Program")
            {
                if (LBL_PROGRAM_TARIFE_KODU.Text.Length > 0)
                {
                    MECRA_TURU = "PROGRAM";
                    TARIFE_TABLOSU = "TRF_PROGRAM_TARIFESI";
                    frm.TXT_FILE_NAME_ESKI.Text = LBL_PROGRAM_TARIFE_KODU.Text;
                    TARIFE_ID = LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString();
                }
            }
            frm.ShowDialog();
            if (frm._BTN_TYPE != "Close")
            {


                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    SqlCommand mCmd = new SqlCommand(" Update dbo.TRF_TARIFELER_LISTESI SET TARIFE_KODU=@TARIFE_KODU,ISLEM_TARIHI=@ISLEM_TARIHI,ISLEM_SAATI=@ISLEM_SAATI   WHERE SIRKET_KODU=@SIRKET_KODU and  MECRA_TURU=@MECRA_TURU AND TARIFE_KODU=@TARIFE_KODU_ESKI  and ID=@ID ", conn);
                    conn.Open();

                    mCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); mCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    mCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); mCmd.Parameters["@MECRA_TURU"].Value = MECRA_TURU;
                    mCmd.Parameters.Add("@ID", SqlDbType.Int); mCmd.Parameters["@ID"].Value = TARIFE_ID;
                    mCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); mCmd.Parameters["@TARIFE_KODU"].Value = frm.TXT_FILE_NAME_YENI.Text;
                    mCmd.Parameters.Add("@TARIFE_KODU_ESKI", SqlDbType.NVarChar); mCmd.Parameters["@TARIFE_KODU_ESKI"].Value = frm.TXT_FILE_NAME_ESKI.Text;
                    mCmd.Parameters.Add("@TARIFE_OWNER", SqlDbType.NVarChar); mCmd.Parameters["@TARIFE_OWNER"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                    mCmd.Parameters.Add("@ISLEM_TARIHI", SqlDbType.DateTime); mCmd.Parameters["@ISLEM_TARIHI"].Value = myDTStart.ToString("dd.MM.yyyy").ToString();
                    mCmd.Parameters.Add("@ISLEM_SAATI", SqlDbType.Time); mCmd.Parameters["@ISLEM_SAATI"].Value = myDTStart.ToString("hh:MM:ss").ToString();
                    mCmd.CommandTimeout = 0;
                    mCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();
                }



                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    SqlCommand mCmd = new SqlCommand(" Update dbo." + TARIFE_TABLOSU + " SET TARIFE_KODU=@TARIFE_KODU   WHERE SIRKET_KODU=@SIRKET_KODU and  TARIFE_KODU=@TARIFE_KODU_ESKI and TARIFE_REF=@TARIFE_REF ", conn);
                    conn.Open();
                    mCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); mCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    mCmd.Parameters.Add("@TARIFE_REF", SqlDbType.Int); mCmd.Parameters["@TARIFE_REF"].Value = TARIFE_ID;
                    mCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); mCmd.Parameters["@TARIFE_KODU"].Value = frm.TXT_FILE_NAME_YENI.Text;
                    mCmd.Parameters.Add("@TARIFE_KODU_ESKI", SqlDbType.NVarChar); mCmd.Parameters["@TARIFE_KODU_ESKI"].Value = frm.TXT_FILE_NAME_ESKI.Text;
                    mCmd.CommandTimeout = 0;
                    mCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();
                }
            }
        }

        private void TOOL_SHARE_TARIFE_Click(object sender, EventArgs e)
        {
            string _TARIFE_ID = "0";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage == null) return;

            TARIFELER._GLOBAL_TARIFELER GLOBALTARIFELER = new TARIFELER._GLOBAL_TARIFELER();
            TARIFELER._TARIFE_PAYLAS frm = new TARIFELER._TARIFE_PAYLAS();  
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon") { frm.TXT_FILE_NAME.Text = LBL_TELEVIZYON_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp") { frm.TXT_FILE_NAME.Text = LBL_NONETV_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo") { frm.TXT_FILE_NAME.Text = LBL_RADYO_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete") { frm.TXT_FILE_NAME.Text = LBL_GAZETE_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi") { frm.TXT_FILE_NAME.Text = LBL_DERGI_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema") { frm.TXT_FILE_NAME.Text = LBL_SINEMA_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor") { frm.TXT_FILE_NAME.Text = LBL_OUTDOOR_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar") { frm.TXT_FILE_NAME.Text = LBL_ORANLAR_TARIFE_KODU.Text; }

             if (frm.TXT_FILE_NAME.Text.Length > 0)
             {
                frm.ShowDialog();
                if (frm._BTN_TYPE != "Close")
                {
                    if (frm.TXT_FILE_NAME.Text.Length > 0)
                    {
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
                        {
                            _TARIFE_ID = GLOBALTARIFELER.TARIFE_PAYLAS(frm.TXT_FILE_NAME.Text, "TELEVIZYON", "TARIFE", false, "", frm.CMB_FRK_FIRMA.Text, frm.CMB_FRK_KULLANICI.Text).ToString();
                        }
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
                        {
                            _TARIFE_ID = GLOBALTARIFELER.TARIFE_PAYLAS(frm.TXT_FILE_NAME.Text, "NON GRP", "TARIFE", false, "", frm.CMB_FRK_FIRMA.Text, frm.CMB_FRK_KULLANICI.Text).ToString();
                        }
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
                        {
                            _TARIFE_ID = GLOBALTARIFELER.TARIFE_PAYLAS(frm.TXT_FILE_NAME.Text, "RADYO", "TARIFE", false, "", frm.CMB_FRK_FIRMA.Text, frm.CMB_FRK_KULLANICI.Text).ToString();
                        } 
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
                        {
                            _TARIFE_ID = GLOBALTARIFELER.TARIFE_PAYLAS(frm.TXT_FILE_NAME.Text, "GAZETE", "TARIFE", false, "", frm.CMB_FRK_FIRMA.Text, frm.CMB_FRK_KULLANICI.Text).ToString();
                        } 
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
                        {
                            _TARIFE_ID = GLOBALTARIFELER.TARIFE_PAYLAS(frm.TXT_FILE_NAME.Text, "DERGI", "TARIFE", false, "", frm.CMB_FRK_FIRMA.Text, frm.CMB_FRK_KULLANICI.Text).ToString();
                        } 
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
                        {
                            _TARIFE_ID = GLOBALTARIFELER.TARIFE_PAYLAS(frm.TXT_FILE_NAME.Text, "SINEMA", "TARIFE", false, "", frm.CMB_FRK_FIRMA.Text, frm.CMB_FRK_KULLANICI.Text).ToString();
                        } 
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
                        {
                            _TARIFE_ID = GLOBALTARIFELER.TARIFE_PAYLAS(frm.TXT_FILE_NAME.Text, "OUTDOOR", "TARIFE", false, "", frm.CMB_FRK_FIRMA.Text, frm.CMB_FRK_KULLANICI.Text).ToString();
                        } 
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar")
                        {
                            _TARIFE_ID = GLOBALTARIFELER.TARIFE_PAYLAS(frm.TXT_FILE_NAME.Text, "ORANLAR", "TARIFE", false, "", frm.CMB_FRK_FIRMA.Text, frm.CMB_FRK_KULLANICI.Text).ToString();
                        }
                        TARIFE_PAYLAS_KAYDET(frm._TXT_FILE_NAME,_TARIFE_ID,frm.CMB_FRK_FIRMA.Text, frm.CMB_FRK_KULLANICI.Text);
                    }
                }
            }
        }

        private void TARIFE_LOG(string ISLEM_TURU, string ISLEM_DETAYI)
        {
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DateTime RUN_DATE = DateTime.Now;// , ISLEM_BITIS_TARIHI, ISLEM_BITIS_SAATI, TABLO_ID, TABLO_ADI
                string SQL = " INSERT INTO [dbo].[ADM_LOG_TABLE] (ISLEM_TURU, RAPOR_KODU, ISLEM_DETAYI, ISLEM_TARIHI, ISLEM_SAATI, ISLEMI_YAPAN) " +
                             " VALUES ('" + ISLEM_TURU + "','" + _GLOBAL_PARAMETRELER._RAPOR_KODU + "','" + ISLEM_DETAYI + "','" + RUN_DATE.ToString("yyyy.MM.dd") + "','" + RUN_DATE.ToString("HH:mm:ss") + "','" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "')";
                SqlCommand command = new SqlCommand(SQL, conn);
                conn.Open();
                command.CommandTimeout = 0;
                command.ExecuteReader(CommandBehavior.CloseConnection);
                conn.Close();
            }
        }
        public void TARIFE_KAYDET()
        {
            //splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms),true,true);
            //splashScreenManagers.ClosingDelay = 400;
            //splashScreenManagers.ShowWaitForm();
            //splashScreenManagers.SetWaitFormDescription("KAYDEDİLİYOR");


            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage == null) return;
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
            {
                if (LBL_TELEVIZYON_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_TELEVIZYON_TARIFE != null)
                    {  // SATIR SİL
                        DW_LIST_TELEVIZYON_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                        TARIFE_LOG("TARIFE", "TELEVIZYON " + DW_LIST_TELEVIZYON_TARIFE.Count.ToString() + " Satır Silindi");
                        GLOBAL.TELEVIZYON_ROW_DELETE(DW_LIST_TELEVIZYON_TARIFE, LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR EKLE
                        DW_LIST_TELEVIZYON_TARIFE.RowStateFilter = DataViewRowState.Added;
                        TARIFE_LOG("TARIFE", "TELEVIZYON " + DW_LIST_TELEVIZYON_TARIFE.Count.ToString() + " Satır Eklendi");
                        GLOBAL.TELEVIZYON_ROW_ADD(DW_LIST_TELEVIZYON_TARIFE, LBL_TELEVIZYON_TARIFE_KODU.Text, LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR GUNCELLE
                        DW_LIST_TELEVIZYON_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                        TARIFE_LOG("TARIFE", "TELEVIZYON " + DW_LIST_TELEVIZYON_TARIFE.Count.ToString() + " Satır Güncellendi");
                        GLOBAL.TELEVIZYON_ROW_UPDATE(DW_LIST_TELEVIZYON_TARIFE, LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        DW_LIST_TELEVIZYON_TARIFE.Table.AcceptChanges();
                        DW_LIST_TELEVIZYON_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
            {
                if (LBL_NONETV_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_NONE_MEASURED_TV != null)
                    {  // SATIR SİL
                        DW_LIST_NONE_MEASURED_TV.RowStateFilter = DataViewRowState.Deleted;
                        TARIFE_LOG("TARIFE", "NONE_MEASURED " + DW_LIST_NONE_MEASURED_TV.Count.ToString() + " Satır Silindi");
                        GLOBAL.TELEVIZYON_NONE_MEASURED_ROW_DELETE(DW_LIST_NONE_MEASURED_TV, LBL_NONETV_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR EKLE
                        DW_LIST_NONE_MEASURED_TV.RowStateFilter = DataViewRowState.Added;
                        TARIFE_LOG("TARIFE", "NONE_MEASURED " + DW_LIST_NONE_MEASURED_TV.Count.ToString() + " Satır Eklendi");
                        GLOBAL.TELEVIZYON_NONE_MEASURED_ROW_ADD(DW_LIST_NONE_MEASURED_TV, LBL_NONETV_TARIFE_KODU.Text, LBL_NONETV_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR GUNCELLE
                        DW_LIST_NONE_MEASURED_TV.RowStateFilter = DataViewRowState.ModifiedOriginal;
                        TARIFE_LOG("TARIFE", "NONE_MEASURED " + DW_LIST_NONE_MEASURED_TV.Count.ToString() + " Satır Güncellendi");
                        GLOBAL.TELEVIZYON_NONE_MEASURED_ROW_UPDATE(DW_LIST_NONE_MEASURED_TV, LBL_NONETV_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                        DW_LIST_NONE_MEASURED_TV.Table.AcceptChanges();
                        DW_LIST_NONE_MEASURED_TV.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            {
                if (LBL_RADYO_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_RADYO_TARIFE != null)
                    {// SATIR SİL
                        DW_LIST_RADYO_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                        TARIFE_LOG("TARIFE", "RADYO " + DW_LIST_RADYO_TARIFE.Count.ToString() + " Satır Silindi");
                        GLOBAL.RADYO_ROW_DELETE(DW_LIST_RADYO_TARIFE, LBL_RADYO_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR EKLE
                        DW_LIST_RADYO_TARIFE.RowStateFilter = DataViewRowState.Added;
                        TARIFE_LOG("TARIFE", "RADYO " + DW_LIST_RADYO_TARIFE.Count.ToString() + " Satır Eklendi");
                        GLOBAL.RADYO_ROW_ADD(DW_LIST_RADYO_TARIFE, LBL_RADYO_TARIFE_KODU.Text, LBL_RADYO_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR GUNCELLE
                        DW_LIST_RADYO_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                        TARIFE_LOG("TARIFE", "RADYO " + DW_LIST_RADYO_TARIFE.Count.ToString() + " Satır Güncellendi");
                        GLOBAL.RADYO_ROW_UPDATE(DW_LIST_RADYO_TARIFE, LBL_RADYO_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                        DW_LIST_RADYO_TARIFE.Table.AcceptChanges();
                        DW_LIST_RADYO_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
            {
                if (LBL_GAZETE_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_GAZETE_TARIFE != null)
                    {// SATIR SİL
                        DW_LIST_GAZETE_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                        TARIFE_LOG("TARIFE", "GAZETE " + DW_LIST_GAZETE_TARIFE.Count.ToString() + " Satır Silindi");
                        GLOBAL.GAZETE_ROW_DELETE(DW_LIST_GAZETE_TARIFE, LBL_GAZETE_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR EKLE
                        DW_LIST_GAZETE_TARIFE.RowStateFilter = DataViewRowState.Added;
                        TARIFE_LOG("TARIFE", "GAZETE " + DW_LIST_GAZETE_TARIFE.Count.ToString() + " Satır Eklendi");
                        //  string GAZETE_TARIFE_SECENEK,string GUNLUK 
                        GLOBAL.GAZETE_ROW_ADD(DW_LIST_GAZETE_TARIFE, LBL_GAZETE_TARIFE_KODU.Text, GAZETE_TARIFE_SECENEK, LBL_TARIFE_GUNLUK_STANDART.Text, LBL_GAZETE_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR GUNCELLE
                        DW_LIST_GAZETE_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                        TARIFE_LOG("TARIFE", "GAZETE " + DW_LIST_GAZETE_TARIFE.Count.ToString() + " Satır Güncellendi");
                        GLOBAL.GAZETE_ROW_UPDATE(DW_LIST_GAZETE_TARIFE, LBL_GAZETE_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                        DW_LIST_GAZETE_TARIFE.Table.AcceptChanges();
                        DW_LIST_GAZETE_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
            {
                if (LBL_DERGI_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_DERGI_TARIFE != null)
                    { // SATIR SİL
                        DW_LIST_DERGI_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                        TARIFE_LOG("TARIFE", "DERGI " + DW_LIST_DERGI_TARIFE.Count.ToString() + " Satır Silindi");
                        GLOBAL.DERGI_ROW_DELETE(DW_LIST_DERGI_TARIFE, LBL_DERGI_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR EKLE
                        DW_LIST_DERGI_TARIFE.RowStateFilter = DataViewRowState.Added;
                        TARIFE_LOG("TARIFE", "DERGI " + DW_LIST_DERGI_TARIFE.Count.ToString() + " Satır Eklendi");
                        GLOBAL.DERGI_ROW_ADD(DW_LIST_DERGI_TARIFE, LBL_DERGI_TARIFE_KODU.Text, LBL_DERGI_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR GUNCELLE
                        DW_LIST_DERGI_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                        TARIFE_LOG("TARIFE", "DERGI " + DW_LIST_DERGI_TARIFE.Count.ToString() + " Satır Güncellendi");
                        GLOBAL.DERGI_ROW_UPDATE(DW_LIST_DERGI_TARIFE, LBL_DERGI_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                        DW_LIST_DERGI_TARIFE.Table.AcceptChanges();
                        DW_LIST_DERGI_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
            {
                if (LBL_SINEMA_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_SINEMA_TARIFE != null)
                    { // SATIR SİL
                        DW_LIST_SINEMA_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                        TARIFE_LOG("TARIFE", "SINEMA " + DW_LIST_SINEMA_TARIFE.Count.ToString() + " Satır Silindi");
                        GLOBAL.SINEMA_ROW_DELETE(DW_LIST_SINEMA_TARIFE, LBL_SINEMA_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR EKLE
                        DW_LIST_SINEMA_TARIFE.RowStateFilter = DataViewRowState.Added;
                        TARIFE_LOG("TARIFE", "SINEMA " + DW_LIST_SINEMA_TARIFE.Count.ToString() + " Satır Eklendi");
                        GLOBAL.SINEMA_ROW_ADD(DW_LIST_SINEMA_TARIFE, LBL_SINEMA_TARIFE_KODU.Text, LBL_SINEMA_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR GUNCELLE
                        DW_LIST_SINEMA_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                        TARIFE_LOG("TARIFE", "SINEMA " + DW_LIST_SINEMA_TARIFE.Count.ToString() + " Satır Güncellendi");
                        GLOBAL.SINEMA_ROW_UPDATE(DW_LIST_SINEMA_TARIFE, LBL_SINEMA_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                        DW_LIST_SINEMA_TARIFE.Table.AcceptChanges();
                        DW_LIST_SINEMA_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
            {
                if (LBL_OUTDOOR_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_OUTDOOR_TARIFE != null)
                    { // SATIR SİL
                        DW_LIST_OUTDOOR_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                        TARIFE_LOG("TARIFE", "OUTDOOR " + DW_LIST_OUTDOOR_TARIFE.Count.ToString() + " Satır Silindi");
                        GLOBAL.OUTDOOR_ROW_DELETE(DW_LIST_OUTDOOR_TARIFE, LBL_OUTDOOR_TARIFE_KODU.Tag.ToString(),_GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR EKLE
                        DW_LIST_OUTDOOR_TARIFE.RowStateFilter = DataViewRowState.Added;
                        TARIFE_LOG("TARIFE", "OUTDOOR " + DW_LIST_OUTDOOR_TARIFE.Count.ToString() + " Satır Eklendi");
                        GLOBAL.OUTDOOR_ROW_ADD(DW_LIST_OUTDOOR_TARIFE, LBL_OUTDOOR_TARIFE_KODU.Text, LBL_OUTDOOR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR GUNCELLE
                        DW_LIST_OUTDOOR_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                        TARIFE_LOG("TARIFE", "OUTDOOR " + DW_LIST_OUTDOOR_TARIFE.Count.ToString() + " Satır Güncellendi");
                        GLOBAL.OUTDOOR_ROW_UPDATE(DW_LIST_OUTDOOR_TARIFE, LBL_OUTDOOR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                        DW_LIST_OUTDOOR_TARIFE.Table.AcceptChanges();
                        DW_LIST_OUTDOOR_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar")
            {
                if (LBL_ORANLAR_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_ORANLAR != null)
                    {   // SATIR SİL
                        DW_LIST_ORANLAR.RowStateFilter = DataViewRowState.Deleted;
                        TARIFE_LOG("TARIFE", "ORANLAR " + DW_LIST_ORANLAR.Count.ToString() + " Satır Silindi");
                        GLOBAL.ORANLAR_ROW_DELETE(DW_LIST_ORANLAR, LBL_ORANLAR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR EKLE
                        DW_LIST_ORANLAR.RowStateFilter = DataViewRowState.Added;
                        TARIFE_LOG("TARIFE", "ORANLAR " + DW_LIST_ORANLAR.Count.ToString() + " Satır Eklendi");
                        GLOBAL.ORANLAR_ROW_ADD(DW_LIST_ORANLAR, LBL_ORANLAR_TARIFE_KODU.Text, LBL_ORANLAR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR GUNCELLE
                        DW_LIST_ORANLAR.RowStateFilter = DataViewRowState.ModifiedOriginal;
                        TARIFE_LOG("TARIFE", "ORANLAR " + DW_LIST_ORANLAR.Count.ToString() + " Satır Güncellendi");
                        GLOBAL.ORANLAR_ROW_UPDATE(DW_LIST_ORANLAR, LBL_ORANLAR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                        DW_LIST_ORANLAR.Table.AcceptChanges();
                        DW_LIST_ORANLAR.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Program")
            {
                if (LBL_PROGRAM_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_PROGRAM_TARIFESI != null)
                    {   // SATIR SİL
                        DW_LIST_PROGRAM_TARIFESI.RowStateFilter = DataViewRowState.Deleted;
                        TARIFE_LOG("TARIFE", "PROGRAM " + DW_LIST_PROGRAM_TARIFESI.Count.ToString() + " Satır Silindi");
                        GLOBAL.PROGRAM_ROW_DELETE(DW_LIST_PROGRAM_TARIFESI, LBL_PROGRAM_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR EKLE
                        DW_LIST_PROGRAM_TARIFESI.RowStateFilter = DataViewRowState.Added;
                        TARIFE_LOG("TARIFE", "PROGRAM " + DW_LIST_PROGRAM_TARIFESI.Count.ToString() + " Satır Eklendi");
                        GLOBAL.PROGRAM_ROW_ADD(DW_LIST_PROGRAM_TARIFESI, LBL_PROGRAM_TARIFE_KODU.Text, LBL_PROGRAM_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        // SATIR GUNCELLE
                        DW_LIST_PROGRAM_TARIFESI.RowStateFilter = DataViewRowState.ModifiedOriginal;
                        TARIFE_LOG("TARIFE", "PROGRAM " + DW_LIST_PROGRAM_TARIFESI.Count.ToString() + " Satır Güncellendi");
                        GLOBAL.PROGRAM_ROW_UPDATE(DW_LIST_PROGRAM_TARIFESI, LBL_PROGRAM_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                        DW_LIST_PROGRAM_TARIFESI.Table.AcceptChanges();
                        DW_LIST_PROGRAM_TARIFESI.RowStateFilter = DataViewRowState.CurrentRows;
                    }
                }
            }

        }


        public void TARIFE_KAYDET_TUMUNU()
        {
            if (LBL_TELEVIZYON_TARIFE_KODU.Text.Length > 0)
            {
                if (DW_LIST_TELEVIZYON_TARIFE != null)
                {  // SATIR SİL
                    DW_LIST_TELEVIZYON_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                    TARIFE_LOG("TARIFE", "TELEVIZYON " + DW_LIST_TELEVIZYON_TARIFE.Count.ToString() + " Satır Silindi");
                    GLOBAL.TELEVIZYON_ROW_DELETE(DW_LIST_TELEVIZYON_TARIFE, LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR EKLE
                    DW_LIST_TELEVIZYON_TARIFE.RowStateFilter = DataViewRowState.Added;
                    TARIFE_LOG("TARIFE", "TELEVIZYON " + DW_LIST_TELEVIZYON_TARIFE.Count.ToString() + " Satır Eklendi");
                    GLOBAL.TELEVIZYON_ROW_ADD(DW_LIST_TELEVIZYON_TARIFE, LBL_TELEVIZYON_TARIFE_KODU.Text, LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR GUNCELLE
                    DW_LIST_TELEVIZYON_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    TARIFE_LOG("TARIFE", "TELEVIZYON " + DW_LIST_TELEVIZYON_TARIFE.Count.ToString() + " Satır Güncellendi");
                    GLOBAL.TELEVIZYON_ROW_UPDATE(DW_LIST_TELEVIZYON_TARIFE, LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    DW_LIST_TELEVIZYON_TARIFE.Table.AcceptChanges();
                    DW_LIST_TELEVIZYON_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                }
            }
            if (LBL_NONETV_TARIFE_KODU.Text.Length > 0)
            {
                if (DW_LIST_NONE_MEASURED_TV != null)
                {  // SATIR SİL
                    DW_LIST_NONE_MEASURED_TV.RowStateFilter = DataViewRowState.Deleted;
                    TARIFE_LOG("TARIFE", "NONE_MEASURED " + DW_LIST_NONE_MEASURED_TV.Count.ToString() + " Satır Silindi");
                    GLOBAL.TELEVIZYON_NONE_MEASURED_ROW_DELETE(DW_LIST_NONE_MEASURED_TV, LBL_NONETV_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR EKLE
                    DW_LIST_NONE_MEASURED_TV.RowStateFilter = DataViewRowState.Added;
                    TARIFE_LOG("TARIFE", "NONE_MEASURED " + DW_LIST_NONE_MEASURED_TV.Count.ToString() + " Satır Eklendi");
                    GLOBAL.TELEVIZYON_NONE_MEASURED_ROW_ADD(DW_LIST_NONE_MEASURED_TV, LBL_NONETV_TARIFE_KODU.Text, LBL_NONETV_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR GUNCELLE
                    DW_LIST_NONE_MEASURED_TV.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    TARIFE_LOG("TARIFE", "NONE_MEASURED " + DW_LIST_NONE_MEASURED_TV.Count.ToString() + " Satır Güncellendi");
                    GLOBAL.TELEVIZYON_NONE_MEASURED_ROW_UPDATE(DW_LIST_NONE_MEASURED_TV, LBL_NONETV_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                    DW_LIST_NONE_MEASURED_TV.Table.AcceptChanges();
                    DW_LIST_NONE_MEASURED_TV.RowStateFilter = DataViewRowState.CurrentRows;
                }
            }
            if (LBL_RADYO_TARIFE_KODU.Text.Length > 0)
            {
                if (DW_LIST_RADYO_TARIFE != null)
                {// SATIR SİL
                    DW_LIST_RADYO_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                    TARIFE_LOG("TARIFE", "RADYO " + DW_LIST_RADYO_TARIFE.Count.ToString() + " Satır Silindi");
                    GLOBAL.RADYO_ROW_DELETE(DW_LIST_RADYO_TARIFE, LBL_RADYO_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR EKLE
                    DW_LIST_RADYO_TARIFE.RowStateFilter = DataViewRowState.Added;
                    TARIFE_LOG("TARIFE", "RADYO " + DW_LIST_RADYO_TARIFE.Count.ToString() + " Satır Eklendi");
                    GLOBAL.RADYO_ROW_ADD(DW_LIST_RADYO_TARIFE, LBL_RADYO_TARIFE_KODU.Text, LBL_RADYO_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR GUNCELLE
                    DW_LIST_RADYO_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    TARIFE_LOG("TARIFE", "RADYO " + DW_LIST_RADYO_TARIFE.Count.ToString() + " Satır Güncellendi");
                    GLOBAL.RADYO_ROW_UPDATE(DW_LIST_RADYO_TARIFE, LBL_RADYO_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                    DW_LIST_RADYO_TARIFE.Table.AcceptChanges();
                    DW_LIST_RADYO_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                }
            }
            if (LBL_GAZETE_TARIFE_KODU.Text.Length > 0)
            {
                if (DW_LIST_GAZETE_TARIFE != null)
                {// SATIR SİL
                    DW_LIST_GAZETE_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                    TARIFE_LOG("TARIFE", "GAZETE " + DW_LIST_GAZETE_TARIFE.Count.ToString() + " Satır Silindi");
                    GLOBAL.GAZETE_ROW_DELETE(DW_LIST_GAZETE_TARIFE, LBL_GAZETE_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR EKLE
                    DW_LIST_GAZETE_TARIFE.RowStateFilter = DataViewRowState.Added;
                    TARIFE_LOG("TARIFE", "GAZETE " + DW_LIST_GAZETE_TARIFE.Count.ToString() + " Satır Eklendi");
                    //  string GAZETE_TARIFE_SECENEK,string GUNLUK 
                    GLOBAL.GAZETE_ROW_ADD(DW_LIST_GAZETE_TARIFE, LBL_GAZETE_TARIFE_KODU.Text, GAZETE_TARIFE_SECENEK, LBL_TARIFE_GUNLUK_STANDART.Text, LBL_GAZETE_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR GUNCELLE
                    DW_LIST_GAZETE_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    TARIFE_LOG("TARIFE", "GAZETE " + DW_LIST_GAZETE_TARIFE.Count.ToString() + " Satır Güncellendi");
                    GLOBAL.GAZETE_ROW_UPDATE(DW_LIST_GAZETE_TARIFE, LBL_GAZETE_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                    DW_LIST_GAZETE_TARIFE.Table.AcceptChanges();
                    DW_LIST_GAZETE_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                }
            }
            if (LBL_DERGI_TARIFE_KODU.Text.Length > 0)
            {
                if (DW_LIST_DERGI_TARIFE != null)
                { // SATIR SİL
                    DW_LIST_DERGI_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                    TARIFE_LOG("TARIFE", "DERGI " + DW_LIST_DERGI_TARIFE.Count.ToString() + " Satır Silindi");
                    GLOBAL.DERGI_ROW_DELETE(DW_LIST_DERGI_TARIFE, LBL_DERGI_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR EKLE
                    DW_LIST_DERGI_TARIFE.RowStateFilter = DataViewRowState.Added;
                    TARIFE_LOG("TARIFE", "DERGI " + DW_LIST_DERGI_TARIFE.Count.ToString() + " Satır Eklendi");
                    GLOBAL.DERGI_ROW_ADD(DW_LIST_DERGI_TARIFE, LBL_DERGI_TARIFE_KODU.Text, LBL_DERGI_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR GUNCELLE
                    DW_LIST_DERGI_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    TARIFE_LOG("TARIFE", "DERGI " + DW_LIST_DERGI_TARIFE.Count.ToString() + " Satır Güncellendi");
                    GLOBAL.DERGI_ROW_UPDATE(DW_LIST_DERGI_TARIFE, LBL_DERGI_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                    DW_LIST_DERGI_TARIFE.Table.AcceptChanges();
                    DW_LIST_DERGI_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                }
            }
            if (LBL_SINEMA_TARIFE_KODU.Text.Length > 0)
            {
                if (DW_LIST_SINEMA_TARIFE != null)
                { // SATIR SİL
                    DW_LIST_SINEMA_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                    TARIFE_LOG("TARIFE", "SINEMA " + DW_LIST_SINEMA_TARIFE.Count.ToString() + " Satır Silindi");
                    GLOBAL.SINEMA_ROW_DELETE(DW_LIST_SINEMA_TARIFE, LBL_SINEMA_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR EKLE
                    DW_LIST_SINEMA_TARIFE.RowStateFilter = DataViewRowState.Added;
                    TARIFE_LOG("TARIFE", "SINEMA " + DW_LIST_SINEMA_TARIFE.Count.ToString() + " Satır Eklendi");
                    GLOBAL.SINEMA_ROW_ADD(DW_LIST_SINEMA_TARIFE, LBL_SINEMA_TARIFE_KODU.Text, LBL_SINEMA_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR GUNCELLE
                    DW_LIST_SINEMA_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    TARIFE_LOG("TARIFE", "SINEMA " + DW_LIST_SINEMA_TARIFE.Count.ToString() + " Satır Güncellendi");
                    GLOBAL.SINEMA_ROW_UPDATE(DW_LIST_SINEMA_TARIFE, LBL_SINEMA_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                    DW_LIST_SINEMA_TARIFE.Table.AcceptChanges();
                    DW_LIST_SINEMA_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                }
            }
            if (LBL_OUTDOOR_TARIFE_KODU.Text.Length > 0)
            {
                if (DW_LIST_OUTDOOR_TARIFE != null)
                { // SATIR SİL
                    DW_LIST_OUTDOOR_TARIFE.RowStateFilter = DataViewRowState.Deleted;
                    TARIFE_LOG("TARIFE", "OUTDOOR " + DW_LIST_OUTDOOR_TARIFE.Count.ToString() + " Satır Silindi");
                    GLOBAL.OUTDOOR_ROW_DELETE(DW_LIST_OUTDOOR_TARIFE, LBL_OUTDOOR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR EKLE
                    DW_LIST_OUTDOOR_TARIFE.RowStateFilter = DataViewRowState.Added;
                    TARIFE_LOG("TARIFE", "OUTDOOR " + DW_LIST_OUTDOOR_TARIFE.Count.ToString() + " Satır Eklendi");
                    GLOBAL.OUTDOOR_ROW_ADD(DW_LIST_OUTDOOR_TARIFE, LBL_OUTDOOR_TARIFE_KODU.Text, LBL_OUTDOOR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR GUNCELLE
                    DW_LIST_OUTDOOR_TARIFE.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    TARIFE_LOG("TARIFE", "OUTDOOR " + DW_LIST_OUTDOOR_TARIFE.Count.ToString() + " Satır Güncellendi");
                    GLOBAL.OUTDOOR_ROW_UPDATE(DW_LIST_OUTDOOR_TARIFE, LBL_OUTDOOR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                    DW_LIST_OUTDOOR_TARIFE.Table.AcceptChanges();
                    DW_LIST_OUTDOOR_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                }
            }
            if (LBL_ORANLAR_TARIFE_KODU.Text.Length > 0)
            {
                if (DW_LIST_ORANLAR != null)
                {   // SATIR SİL
                    DW_LIST_ORANLAR.RowStateFilter = DataViewRowState.Deleted;
                    TARIFE_LOG("TARIFE", "ORANLAR " + DW_LIST_ORANLAR.Count.ToString() + " Satır Silindi");
                    GLOBAL.ORANLAR_ROW_DELETE(DW_LIST_ORANLAR, LBL_ORANLAR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR EKLE
                    DW_LIST_ORANLAR.RowStateFilter = DataViewRowState.Added;
                    TARIFE_LOG("TARIFE", "ORANLAR " + DW_LIST_ORANLAR.Count.ToString() + " Satır Eklendi");
                    GLOBAL.ORANLAR_ROW_ADD(DW_LIST_ORANLAR, LBL_ORANLAR_TARIFE_KODU.Text, LBL_ORANLAR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR GUNCELLE
                    DW_LIST_ORANLAR.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    TARIFE_LOG("TARIFE", "ORANLAR " + DW_LIST_ORANLAR.Count.ToString() + " Satır Güncellendi");
                    GLOBAL.ORANLAR_ROW_UPDATE(DW_LIST_ORANLAR, LBL_ORANLAR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                    DW_LIST_ORANLAR.Table.AcceptChanges();
                    DW_LIST_ORANLAR.RowStateFilter = DataViewRowState.CurrentRows;
                }
            }
            if (LBL_PROGRAM_TARIFE_KODU.Text.Length > 0)
            {
                if (DW_LIST_PROGRAM_TARIFESI != null)
                {   // SATIR SİL
                    DW_LIST_PROGRAM_TARIFESI.RowStateFilter = DataViewRowState.Deleted;
                    TARIFE_LOG("TARIFE", "PROGRAM " + DW_LIST_PROGRAM_TARIFESI.Count.ToString() + " Satır Silindi");
                    GLOBAL.PROGRAM_ROW_DELETE(DW_LIST_PROGRAM_TARIFESI, LBL_PROGRAM_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR EKLE
                    DW_LIST_PROGRAM_TARIFESI.RowStateFilter = DataViewRowState.Added;
                    TARIFE_LOG("TARIFE", "PROGRAM " + DW_LIST_PROGRAM_TARIFESI.Count.ToString() + " Satır Eklendi");
                    GLOBAL.PROGRAM_ROW_ADD(DW_LIST_PROGRAM_TARIFESI, LBL_PROGRAM_TARIFE_KODU.Text, LBL_PROGRAM_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    // SATIR GUNCELLE
                    DW_LIST_PROGRAM_TARIFESI.RowStateFilter = DataViewRowState.ModifiedOriginal;
                    TARIFE_LOG("TARIFE", "PROGRAM " + DW_LIST_PROGRAM_TARIFESI.Count.ToString() + " Satır Güncellendi");
                    GLOBAL.PROGRAM_ROW_UPDATE(DW_LIST_PROGRAM_TARIFESI, LBL_PROGRAM_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                    DW_LIST_PROGRAM_TARIFESI.Table.AcceptChanges();
                    DW_LIST_PROGRAM_TARIFESI.RowStateFilter = DataViewRowState.CurrentRows;
                }
            }
        }


        private void TARIFE_SATIR_SIL()
        {
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
            {
                if (gridView_TELEVIZYON.RowCount > 0)
                {
                    int[] GETROW = gridView_TELEVIZYON.GetSelectedRows();
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        for (int i = GETROW.Length - 1; i >= 0; i--)
                        {
                            DW_LIST_TELEVIZYON_TARIFE.Delete(Convert.ToUInt16(GETROW[i]));
                        }
                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }

                gridView_TELEVIZYON.RefreshData();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            {
                if (gridView_RADYO.RowCount > 0)
                {
                    int[] GETROW = gridView_RADYO.GetSelectedRows();
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        for (int i = GETROW.Length - 1; i >= 0; i--)
                        {
                            DW_LIST_RADYO_TARIFE.Delete(Convert.ToUInt16(GETROW[i]));
                        }
                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_RADYO.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
            {
                if (gridView_GAZETE.RowCount > 0)
                {
                    int[] GETROW = gridView_GAZETE.GetSelectedRows();
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        for (int i = GETROW.Length - 1; i >= 0; i--)
                        {
                            DW_LIST_GAZETE_TARIFE.Delete(Convert.ToUInt16(GETROW[i]));
                        }
                    }
                }
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
            {
                if (gridView_DERGI.RowCount > 0)
                {
                    int[] GETROW = gridView_DERGI.GetSelectedRows();

                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        for (int i = GETROW.Length - 1; i >= 0; i--)
                        {
                            DW_LIST_DERGI_TARIFE.Delete(Convert.ToUInt16(GETROW[i]));
                        }
                    }
                }

                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_DERGI.RefreshData();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
            {
                if (gridView_OUTDOOR.RowCount > 0)
                {
                    int[] GETROW = gridView_OUTDOOR.GetSelectedRows();

                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                    if (c == DialogResult.Yes)
                    {
                        for (int i = GETROW.Length - 1; i >= 0; i--)
                        {
                            DW_LIST_OUTDOOR_TARIFE.Delete(Convert.ToUInt16(GETROW[i]));
                        }
                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_OUTDOOR.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
            {
                if (gridView_SINEMA.RowCount > 0)
                {
                    int[] GETROW = gridView_SINEMA.GetSelectedRows();
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        for (int i = GETROW.Length - 1; i >= 0; i--)
                        {
                            DW_LIST_SINEMA_TARIFE.Delete(Convert.ToUInt16(GETROW[i]));
                        }
                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_SINEMA.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Internet")
            {
                if (gridView_INTERNET.RowCount > 0)
                {
                    int[] GETROW = gridView_INTERNET.GetSelectedRows();
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        for (int i = GETROW.Length - 1; i >= 0; i--)
                        {
                            DW_LIST_INTERNET_TARIFE.Delete(Convert.ToUInt16(GETROW[i]));
                        }
                    }

                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_INTERNET.RefreshData();
            }


            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
            {

                if (gridView_NONE_MEASURED_TELEVIZYON.RowCount > 0)
                {
                    int[] GETROW = gridView_NONE_MEASURED_TELEVIZYON.GetSelectedRows();
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        for (int i = GETROW.Length - 1; i >= 0; i--)
                        {
                            DW_LIST_NONE_MEASURED_TV.Delete(Convert.ToUInt16(GETROW[i]));
                        }
                    }

                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_NONE_MEASURED_TELEVIZYON.RefreshData();
            }


            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Program")
            {
                if (gridView_TARIFE_PROGRAM.RowCount > 0)
                {
                    int[] GETROW = gridView_TARIFE_PROGRAM.GetSelectedRows();
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        for (int i = GETROW.Length - 1; i >= 0; i--)
                        {
                            DW_LIST_PROGRAM_TARIFESI.Delete(Convert.ToUInt16(GETROW[i]));
                        }
                    }

                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_TARIFE_PROGRAM.RefreshData();
            }

        }
        private void TARIFE_COPY()
        {
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon") { if (gridView_TELEVIZYON.RowCount > 0) gridView_TELEVIZYON.CopyToClipboard(); }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo") { if (gridView_RADYO.RowCount > 0) gridView_RADYO.CopyToClipboard(); }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete") { if (gridView_GAZETE.RowCount > 0) gridView_GAZETE.CopyToClipboard(); }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi") { if (gridView_DERGI.RowCount > 0) gridView_DERGI.CopyToClipboard(); }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema") { if (gridView_SINEMA.RowCount > 0) gridView_SINEMA.CopyToClipboard(); }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor") { if (gridView_OUTDOOR.RowCount > 0) gridView_OUTDOOR.CopyToClipboard(); }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Internet") { if (gridView_INTERNET.RowCount > 0) gridView_INTERNET.CopyToClipboard(); }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Program") { if (gridView_TARIFE_PROGRAM.RowCount > 0) gridView_TARIFE_PROGRAM.CopyToClipboard(); }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp") { if (gridView_NONE_MEASURED_TELEVIZYON.RowCount > 0) gridView_NONE_MEASURED_TELEVIZYON.CopyToClipboard(); }
        }
        private void TARIFE_YAPISTIR()
        {
            DateTime BAS_TARIHI = _GLOBAL_PARAMETRELER._START_DATE;//= Convert.ToDateTime(dtBAS_TARIHI.EditValue);
            DateTime BIT_TARIHI = _GLOBAL_PARAMETRELER._END_DATE;//= Convert.ToDateTime(dtBIT_TARIHI.EditValue);
            //DateTime BAS_TARIHI = Convert.ToDateTime(dtBAS_TARIHI.EditValue);
            //DateTime BIT_TARIHI = Convert.ToDateTime(dtBIT_TARIHI.EditValue);
            //dtBAS_TARIHI.EditValue = BAS_TARIHI.ToString("dd.MM.yyyy").ToString();
            //dtBIT_TARIHI.EditValue = BIT_TARIHI.ToString("dd.MM.yyyy").ToString();
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
            {
                if (LBL_TELEVIZYON_TARIFE_KODU.Text.Length > 0)
                {
                    CLASS.DESIGNE_CLASS paste = new CLASS.DESIGNE_CLASS();
                    paste.TARIFEYI_YAPISTIR("Televizyon", DW_LIST_TELEVIZYON_TARIFE, BAS_TARIHI, BIT_TARIHI);

                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_TELEVIZYON.RefreshData();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
            {
                if (LBL_NONETV_TARIFE_KODU.Text.Length > 0)
                {
                    CLASS.DESIGNE_CLASS paste = new CLASS.DESIGNE_CLASS();
                    paste.TARIFEYI_YAPISTIR("None Tv Grp", DW_LIST_NONE_MEASURED_TV, BAS_TARIHI, BIT_TARIHI);
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_NONE_MEASURED_TELEVIZYON.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            {
                if (LBL_RADYO_TARIFE_KODU.Text.Length > 0)
                {
                    CLASS.DESIGNE_CLASS paste = new CLASS.DESIGNE_CLASS();
                    paste.TARIFEYI_YAPISTIR("Radyo", DW_LIST_RADYO_TARIFE, BAS_TARIHI, BIT_TARIHI);
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_RADYO.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
            {
                if (LBL_GAZETE_TARIFE_KODU.Text.Length > 0)
                {
                    CLASS.DESIGNE_CLASS paste = new CLASS.DESIGNE_CLASS();
                    paste.TARIFEYI_YAPISTIR("Gazete", DW_LIST_GAZETE_TARIFE, BAS_TARIHI, BIT_TARIHI);
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_DERGI.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
            {
                if (LBL_DERGI_TARIFE_KODU.Text.Length > 0)
                {
                    CLASS.DESIGNE_CLASS paste = new CLASS.DESIGNE_CLASS();
                    paste.TARIFEYI_YAPISTIR("Dergi", DW_LIST_DERGI_TARIFE, BAS_TARIHI, BIT_TARIHI);
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_DERGI.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
            {
                if (LBL_OUTDOOR_TARIFE_KODU.Text.Length > 0)
                {
                    CLASS.DESIGNE_CLASS paste = new CLASS.DESIGNE_CLASS();
                    paste.TARIFEYI_YAPISTIR("Outdoor", DW_LIST_OUTDOOR_TARIFE, BAS_TARIHI, BIT_TARIHI);
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_OUTDOOR.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
            {
                if (LBL_SINEMA_TARIFE_KODU.Text.Length > 0)
                {
                    CLASS.DESIGNE_CLASS paste = new CLASS.DESIGNE_CLASS();
                    paste.TARIFEYI_YAPISTIR("Sinema", DW_LIST_SINEMA_TARIFE, BAS_TARIHI, BIT_TARIHI);
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_SINEMA.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Internet")
            {
                if (LBL_INTERNET_TARIFE_KODU.Text.Length > 0)
                {
                    CLASS.DESIGNE_CLASS paste = new CLASS.DESIGNE_CLASS();
                    paste.TARIFEYI_YAPISTIR("Internet", DW_LIST_INTERNET_TARIFE, BAS_TARIHI, BIT_TARIHI);
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_INTERNET.RefreshData();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Program")
            {
                if (LBL_PROGRAM_TARIFE_KODU.Text.Length > 0)
                {
                    CLASS.DESIGNE_CLASS paste = new CLASS.DESIGNE_CLASS();
                    paste.TARIFEYI_YAPISTIR("Program", DW_LIST_PROGRAM_TARIFESI, BAS_TARIHI, BIT_TARIHI);
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                gridView_TARIFE_PROGRAM.RefreshData();
            }
        }
        private void TUMUNU_SIL()
        {
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
            {
                if (LBL_TELEVIZYON_TARIFE_KODU.Text.Length > 0)
                {
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        if (LBL_TELEVIZYON_TARIFE_KODU.Text == null) return;
                        GLOBAL.TELEVIZYON_TARIFE_ROW_ALL_DELETE(LBL_TELEVIZYON_TARIFE_KODU.Text.ToString(), LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        gridControl_TARIFE.DataSource = DW_LIST_TELEVIZYON_TARIFE;
                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }

                DW_LIST_TELEVIZYON_TARIFE.Table.Rows.Clear();
                DW_LIST_TELEVIZYON_TARIFE.Table.AcceptChanges();
                gridControl_TARIFE.DataSource = DW_LIST_TELEVIZYON_TARIFE;
                gridView_TELEVIZYON.RefreshData();

            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
            {
                if (LBL_NONETV_TARIFE_KODU.Text.Length > 0)
                {
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        if (LBL_NONETV_TARIFE_KODU.Text == null) return;
                        GLOBAL.TELEVIZYON_NONE_MEASURED_TARIFE_ROW_ALL_DELETE(LBL_NONETV_TARIFE_KODU.Text.ToString(), LBL_NONETV_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        gridControl_NONE_MEASURED_TV.DataSource = DW_LIST_NONE_MEASURED_TV;

                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }

                DW_LIST_NONE_MEASURED_TV.Table.Rows.Clear();
                DW_LIST_NONE_MEASURED_TV.Table.AcceptChanges();
                gridControl_NONE_MEASURED_TV.DataSource = DW_LIST_NONE_MEASURED_TV;
                gridView_NONE_MEASURED_TELEVIZYON.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            {
                if (LBL_RADYO_TARIFE_KODU.Text.Length > 0)
                {
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        if (LBL_RADYO_TARIFE_KODU.Text == null) return;
                        GLOBAL.RADYO_TARIFE_ROW_ALL_DELETE(LBL_RADYO_TARIFE_KODU.Text.ToString(), LBL_RADYO_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        gridControl_RADYO.DataSource = DW_LIST_RADYO_TARIFE;
                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                DW_LIST_RADYO_TARIFE.Table.Rows.Clear();
                DW_LIST_RADYO_TARIFE.Table.AcceptChanges();
                gridControl_RADYO.DataSource = DW_LIST_RADYO_TARIFE;
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
            {
                if (LBL_GAZETE_TARIFE_KODU.Text.Length > 0)
                {
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        if (LBL_GAZETE_TARIFE_KODU.Text == null) return;
                        GLOBAL.GAZETE_TARIFE_ROW_ALL_DELETE(LBL_GAZETE_TARIFE_KODU.Text, LBL_GAZETE_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        gridControl_GAZETE.DataSource = DW_LIST_GAZETE_TARIFE;
                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }

                DW_LIST_GAZETE_TARIFE.Table.Rows.Clear();
                DW_LIST_GAZETE_TARIFE.Table.AcceptChanges();
                gridControl_GAZETE.DataSource = DW_LIST_GAZETE_TARIFE;
                gridView_GAZETE.RefreshData();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
            {
                if (LBL_DERGI_TARIFE_KODU.Text.Length > 0)
                {
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        if (LBL_DERGI_TARIFE_KODU.Text == null) return;
                        GLOBAL.DERGI_TARIFE_ROW_ALL_DELETE(LBL_DERGI_TARIFE_KODU.Text.ToString(), LBL_DERGI_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        gridControl_DERGI.DataSource = DW_LIST_DERGI_TARIFE;
                    }
                }

                else { MessageBox.Show("TARİFE SEÇİNİZ"); }

                DW_LIST_DERGI_TARIFE.Table.Rows.Clear();
                DW_LIST_DERGI_TARIFE.Table.AcceptChanges();
                gridControl_DERGI.DataSource = DW_LIST_DERGI_TARIFE;
                gridView_DERGI.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
            {
                if (LBL_OUTDOOR_TARIFE_KODU.Text.Length > 0)
                {
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        if (LBL_OUTDOOR_TARIFE_KODU.Text == null) return;
                        GLOBAL.OUTDOOR_TARIFE_ROW_ALL_DELETE(LBL_OUTDOOR_TARIFE_KODU.Text.ToString(), LBL_OUTDOOR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        gridControl_OUTDOOR.DataSource = DW_LIST_OUTDOOR_TARIFE;
                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                DW_LIST_OUTDOOR_TARIFE.Table.Rows.Clear();
                DW_LIST_OUTDOOR_TARIFE.Table.AcceptChanges();
                gridControl_OUTDOOR.DataSource = DW_LIST_OUTDOOR_TARIFE;
                gridView_OUTDOOR.RefreshData();
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
            {
                if (LBL_SINEMA_TARIFE_KODU.Text.Length > 0)
                {
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        if (LBL_SINEMA_TARIFE_KODU.Text == null) return;
                        GLOBAL.SINEMA_TARIFE_ROW_ALL_DELETE(LBL_SINEMA_TARIFE_KODU.Text.ToString(), LBL_SINEMA_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        gridControl_SINEMA.DataSource = DW_LIST_SINEMA_TARIFE;
                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }

                DW_LIST_SINEMA_TARIFE.Table.Rows.Clear();
                DW_LIST_SINEMA_TARIFE.Table.AcceptChanges();
                gridControl_SINEMA.DataSource = DW_LIST_SINEMA_TARIFE;
                gridView_SINEMA.RefreshData();

            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Internet")
            {
                if (LBL_INTERNET_TARIFE_KODU.Text.Length > 0)
                {
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        if (LBL_INTERNET_TARIFE_KODU.Text == null) return;
                        GLOBAL.INTERNET_TARIFE_ROW_ALL_DELETE(LBL_INTERNET_TARIFE_KODU.Text.ToString(), LBL_INTERNET_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        gridControl_INTERNET.DataSource = DW_LIST_INTERNET_TARIFE;
                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                DW_LIST_INTERNET_TARIFE.Table.Rows.Clear();
                DW_LIST_INTERNET_TARIFE.Table.AcceptChanges();
                gridControl_INTERNET.DataSource = DW_LIST_INTERNET_TARIFE;
                gridView_INTERNET.RefreshData();
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Program")
            {
                if (LBL_PROGRAM_TARIFE_KODU.Text.Length > 0)
                {
                    DialogResult c = MessageBox.Show("Silmek istediğinize eminmisiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (c == DialogResult.Yes)
                    {
                        if (LBL_PROGRAM_TARIFE_KODU.Text == null) return;
                        GLOBAL.PROGRAM_TARIFE_ROW_ALL_DELETE(LBL_PROGRAM_TARIFE_KODU.Text.ToString(), LBL_PROGRAM_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        gridControl_INTERNET.DataSource = DW_LIST_PROGRAM_TARIFESI;
                    }
                }
                else { MessageBox.Show("TARİFE SEÇİNİZ"); }
                DW_LIST_PROGRAM_TARIFESI.Table.Rows.Clear();
                DW_LIST_PROGRAM_TARIFESI.Table.AcceptChanges();
                gridControl_PROGRAM_TARIFESI.DataSource = DW_LIST_PROGRAM_TARIFESI;
                gridView_TARIFE_PROGRAM.RefreshData();
            }
        }
        public void TARIFE_CHANGE(string MECRA_TURU)
        {
            if (TARIFE_KODU == "") return;

            if (MECRA_TURU == "TELEVIZYON")
            {
                LBL_TELEVIZYON_TARIFE_KODU.Text = TARIFE_KODU;
                LBL_TELEVIZYON_TARIFE_KODU.Tag = TARIFE_ID;
                DW_LIST_TELEVIZYON_TARIFE = null; 
                using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQL = string.Format(" SELECT  * FROM         dbo.TRF_TARIFELER_LISTESI WHERE   TARIFE_OWNER='{0}' and  SIRKET_KODU = N'{1}' AND MECRA_TURU = N'TELEVIZYON' and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_TELEVIZYON_TARIFE_KODU.Text);
                    SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                    myConnection.Open();
                    SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (myReader.Read())
                    {
                        LBL_TELEVIZYON_TARIFE_TURU.Text = myReader["SELECT_TYPE"].ToString(); 
                        if (LBL_TELEVIZYON_TARIFE_TURU.Text == "MECRA_VE_SPOTTURU_DETYAY") gridView_TELEVIZYON.Columns["SPOT_TIPI_DETAY"].Visible = true; else gridView_TELEVIZYON.Columns["SPOT_TIPI_DETAY"].Visible = false; 
                    }
                } 
                string query = string.Format("SELECT  *   FROM  dbo.TRF_TELEVIZYON where  KULLANICI_KODU='{0}' and SIRKET_KODU='{1}' and TARIFE_KODU='{2}' order by MECRA_KODU", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_TELEVIZYON_TARIFE_KODU.Text);
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                    adapter.Fill(ds, "TRF_TELEVIZYON");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DW_LIST_TELEVIZYON_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                }
                gridControl_TARIFE.DataSource = DW_LIST_TELEVIZYON_TARIFE;
                xtraTabControl_TARIFE_DETAY.SelectedTabPage = xtraTab_Televizyon;
            }
            if (MECRA_TURU == "NONEGRP")
            {
                LBL_NONETV_TARIFE_KODU.Text = TARIFE_KODU;
                LBL_NONETV_TARIFE_KODU.Tag = TARIFE_ID;
                DW_LIST_NONE_MEASURED_TV = null;

                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string query = string.Format("  SELECT * FROM     dbo.TRF_TELEVIZYON_NONE_MEASURED where KULLANICI_KODU='{0}' and SIRKET_KODU='{1}' and TARIFE_KODU='{2}' ", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_NONETV_TARIFE_KODU.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                    adapter.Fill(ds, "TRF_TELEVIZYON_NONE_MEASURED");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DW_LIST_NONE_MEASURED_TV = dvManager.CreateDataView(ds.Tables[0]);
                }
                gridControl_NONE_MEASURED_TV.DataSource = DW_LIST_NONE_MEASURED_TV;
                xtraTabControl_TARIFE_DETAY.SelectedTabPage = xtraTab_NoneGrp;
            }
            if (MECRA_TURU == "RADYO")
            {
                LBL_RADYO_TARIFE_KODU.Text = TARIFE_KODU;
                LBL_RADYO_TARIFE_KODU.Tag = TARIFE_ID;
                DW_LIST_RADYO_TARIFE = null;

                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string query = string.Format("      SELECT  *  FROM         dbo.TRF_RADYO where KULLANICI_KODU='{0}' and SIRKET_KODU='{1}' and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_RADYO_TARIFE_KODU.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                    adapter.Fill(ds, "TRF_RADYO");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DW_LIST_RADYO_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                }
                gridControl_RADYO.DataSource = DW_LIST_RADYO_TARIFE;
                xtraTabControl_TARIFE_DETAY.SelectedTabPage = xtraTab_Radyo;
            }
            if (MECRA_TURU == "GAZETE")
            {
                LBL_GAZETE_TARIFE_KODU.Text = TARIFE_KODU;
                LBL_GAZETE_TARIFE_KODU.Tag = TARIFE_ID;
                string STR_SECENEKLER_BIR = "", STR_SECENEKLER_IKI = "";
                DW_LIST_GAZETE_TARIFE = null;
                for (int i = 0; i <= gridView_GAZETE.Columns.Count - 1; i++)
                {
                    gridView_GAZETE.Columns[i].Visible = false;
                }
                using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQL = string.Format(" SELECT  * FROM         dbo.TRF_TARIFELER_LISTESI WHERE   TARIFE_OWNER='{0}' and  SIRKET_KODU = N'{1}' AND MECRA_TURU = N'GAZETE' and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_GAZETE_TARIFE_KODU.Text);
                    SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                    myConnection.Open();
                    SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (myReader.Read())
                    {
                        //  Mecra Kodu	Yayın Türü	Hesap Türü	BİRİM FİYAT/FAKTÖR	Pzt	Sal	Çar	Per	Cum	Cmt	Paz	 +/-	BAŞLANGIÇ TARİHİ	BİTİŞ TARİHİ
                        if (myReader["GUNLUK_TARIFE"] == DBNull.Value || myReader["GUNLUK_TARIFE"].ToString() == "False")
                        {
                            STR_SECENEKLER_IKI = "BIRIM_FIYAT,";
                            LBL_TARIFE_GUNLUK_STANDART.Text = "STANDART_TARIFE";
                        }
                        else
                        {
                            STR_SECENEKLER_IKI = "BIRIM_FIYAT,CUMARTESI,PAZAR, ";
                            LBL_TARIFE_GUNLUK_STANDART.Text = "GUNLUK_TARIFE";
                        }
                        LBL_GAZETE_TARIFE_TURU.Text = myReader["SELECT_TYPE"].ToString();
                        LBL_HESAPLAMA_TURU_GAZETE.Text = myReader["HESAPLAMA_TURU"].ToString();

                        if (myReader["SELECT_TYPE"].ToString() == "MECRA") { STR_SECENEKLER_BIR = "MECRA_KODU,HESAPLANMA_TURU,"; GAZETE_TARIFE_SECENEK = "MECRA"; }
                        if (myReader["SELECT_TYPE"].ToString() == "MECRA_SAYFA") { STR_SECENEKLER_BIR = "MECRA_KODU,SAYFA_GRUBU,HESAPLANMA_TURU,"; GAZETE_TARIFE_SECENEK = "MECRA_SAYFA"; }
                        if (myReader["SELECT_TYPE"].ToString() == "MECRA_SAYFA_YAYINTURU") { STR_SECENEKLER_BIR = "MECRA_KODU,SAYFA_GRUBU,YAYIN_TURU,HESAPLANMA_TURU,"; GAZETE_TARIFE_SECENEK = "MECRA_SAYFA_YAYINTURU"; }
                    }

                    string qury = String.Format("{0}{1}BASLANGIC_TARIHI,BITIS_TARIHI", STR_SECENEKLER_BIR, STR_SECENEKLER_IKI);
                    DataSet ds = new DataSet();
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        //string querys = String.Format(" SELECT ID, {0} {1}  BASLANGIC_TARIHI , BITIS_TARIHI   FROM     dbo.TRF_GAZETE where  KULLANICI_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_NAME + "' and SIRKET_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "' and TARIFE_KODU='" + LBL_GAZETE_TARIFE_KODU.Text.ToString() + "'", STR_SECENEKLER_BIR, STR_SECENEKLER_IKI);
                        string querys = string.Format(" SELECT   ID, {0} {1}  BASLANGIC_TARIHI , BITIS_TARIHI   FROM   dbo.TRF_GAZETE where KULLANICI_KODU='{2}' and  SIRKET_KODU='{3}' and TARIFE_KODU='{4}'", STR_SECENEKLER_BIR, STR_SECENEKLER_IKI, _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_GAZETE_TARIFE_KODU.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(querys, conn) };
                        adapter.Fill(ds, "TRF_GAZETE");
                        DataViewManager dvManager = new DataViewManager(ds);
                        DW_LIST_GAZETE_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                    }
                    gridControl_GAZETE.DataSource = DW_LIST_GAZETE_TARIFE;
                    gridView_GAZETE.RefreshData();
                    string[] Onesz = qury.Split(',');
                    for (int i = 0; i < Onesz.Length; i++)
                    {
                        gridView_GAZETE.Columns[Onesz[i].Trim()].VisibleIndex = i;
                        gridView_GAZETE.Columns[Onesz[i].Trim()].Visible = true;
                    }
                }
                xtraTabControl_TARIFE_DETAY.SelectedTabPage = xtraTab_Gazete;
            }
            if (MECRA_TURU == "DERGI")
            {
                LBL_DERGI_TARIFE_KODU.Text = TARIFE_KODU;
                LBL_DERGI_TARIFE_KODU.Tag = TARIFE_ID;
                DW_LIST_DERGI_TARIFE = null;
                using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQL = string.Format(" SELECT  * FROM         dbo.TRF_TARIFELER_LISTESI WHERE   TARIFE_OWNER='{0}' and  SIRKET_KODU = N'{1}' AND MECRA_TURU = N'DERGI' and TARIFE_KODU='{2}' and ID='{3}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_DERGI_TARIFE_KODU.Text, TARIFE_ID);
                    SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                    myConnection.Open();
                    SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (myReader.Read())
                    {
                        LBL_HESAPLAMA_TURU_DERGI.Text = myReader["HESAPLAMA_TURU"].ToString();
                    }
                }
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string query = string.Format("      SELECT  *  FROM         dbo.TRF_DERGI where KULLANICI_KODU='{0}' and  SIRKET_KODU='{1}' and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_DERGI_TARIFE_KODU.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                    adapter.Fill(ds, "TRF_DERGI");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DW_LIST_DERGI_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                }
                gridControl_DERGI.DataSource = DW_LIST_DERGI_TARIFE;
                xtraTabControl_TARIFE_DETAY.SelectedTabPage = xtraTab_Dergi;
            }
            if (MECRA_TURU == "SINEMA")
            {
                LBL_SINEMA_TARIFE_KODU.Text = TARIFE_KODU;
                LBL_SINEMA_TARIFE_KODU.Tag = TARIFE_ID;
                DW_LIST_SINEMA_TARIFE = null;
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string query = string.Format(" SELECT  *  FROM     dbo.TRF_SINEMA where KULLANICI_KODU='{0}' and SIRKET_KODU='{1}' and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_SINEMA_TARIFE_KODU.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                    adapter.Fill(ds, "TRF_SINEMA");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DW_LIST_SINEMA_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                }
                gridControl_SINEMA.DataSource = DW_LIST_SINEMA_TARIFE;
                xtraTabControl_TARIFE_DETAY.SelectedTabPage = xtraTab_Sinema;
            }
            if (MECRA_TURU == "OUTDOOR")
            {
                LBL_OUTDOOR_TARIFE_KODU.Text = TARIFE_KODU;
                LBL_OUTDOOR_TARIFE_KODU.Tag = TARIFE_ID;
                DW_LIST_OUTDOOR_TARIFE = null;
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string query = string.Format("SELECT  *  FROM  dbo.TRF_OUTDOOR where KULLANICI_KODU='{0}' and  SIRKET_KODU='{1}' and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_OUTDOOR_TARIFE_KODU.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                    adapter.Fill(ds, "TRF_OUTDOOR");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DW_LIST_OUTDOOR_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                }
                gridControl_OUTDOOR.DataSource = DW_LIST_OUTDOOR_TARIFE;
                xtraTabControl_TARIFE_DETAY.SelectedTabPage = xtraTab_Outdoor;
            }
            if (MECRA_TURU == "INTERNET")
            {
                LBL_INTERNET_TARIFE_KODU.Text = TARIFE_KODU;
                LBL_INTERNET_TARIFE_KODU.Tag = TARIFE_ID;
                DW_LIST_INTERNET_TARIFE = null;
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string query = string.Format("      SELECT  *  FROM     dbo.TRF_INTERNET where  KULLANICI_KODU='{0}' and  SIRKET_KODU='{1}' and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_INTERNET_TARIFE_KODU.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                    adapter.Fill(ds, "TRF_INTERNET");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DW_LIST_INTERNET_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                }
                gridControl_INTERNET.DataSource = DW_LIST_INTERNET_TARIFE;
                xtraTabControl_TARIFE_DETAY.SelectedTabPage = xtraTab_Internet;
            }
            if (MECRA_TURU == "SEKTOR")
            {
                LBL_SEKTOR_TARIFE_KODU.Text = TARIFE_KODU;
                LBL_SEKTOR_TARIFE_KODU.Tag = TARIFE_ID;
                DW_LIST_SEKTOR_TARIFE = null;
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string query = string.Format("      SELECT  *  FROM     dbo.TRF_SEKTOR where KULLANICI_KODU='{0}' and  SIRKET_KODU='{1}' and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_SEKTOR_TARIFE_KODU.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                    adapter.Fill(ds, "TRF_SEKTOR");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DW_LIST_SEKTOR_TARIFE = dvManager.CreateDataView(ds.Tables[0]);
                    gridControl_SEKTOR.DataSource = DW_LIST_SEKTOR_TARIFE;
                }
                gridControl_SEKTOR.DataSource = DW_LIST_SEKTOR_TARIFE;
                xtraTabControl_TARIFE_DETAY.SelectedTabPage = xtraTab_Sektor;
            }
            if (MECRA_TURU == "PROGRAM")
            {
                LBL_PROGRAM_TARIFE_KODU.Text = TARIFE_KODU;
                LBL_PROGRAM_TARIFE_KODU.Tag = TARIFE_ID;
                DW_LIST_PROGRAM_TARIFESI = null;
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string query = string.Format("  SELECT * FROM     dbo.TRF_PROGRAM_TARIFESI where  KULLANICI_KODU='{0}' and  SIRKET_KODU='{1}' and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_PROGRAM_TARIFE_KODU.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                    adapter.Fill(ds, "TRF_ORANLAR");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DW_LIST_PROGRAM_TARIFESI = dvManager.CreateDataView(ds.Tables[0]);
                }
                gridControl_PROGRAM_TARIFESI.DataSource = DW_LIST_PROGRAM_TARIFESI;
                xtraTabControl_TARIFE_DETAY.SelectedTabPage = xtraTab_Program;
            }
            if (MECRA_TURU == "ORANLAR")
            {
                LBL_ORANLAR_TARIFE_KODU.Text = TARIFE_KODU;
                LBL_ORANLAR_TARIFE_KODU.Tag = TARIFE_ID;
                DW_LIST_ORANLAR = null;
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string query = string.Format("  SELECT * FROM     dbo.TRF_ORANLAR where  KULLANICI_KODU='{0}' and  SIRKET_KODU='{1}' and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, LBL_ORANLAR_TARIFE_KODU.Text);
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                    adapter.Fill(ds, "TRF_ORANLAR");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DW_LIST_ORANLAR = dvManager.CreateDataView(ds.Tables[0]);
                }
                gridControl_ORAN_TARIFESI.DataSource = DW_LIST_ORANLAR;
                xtraTabControl_TARIFE_DETAY.SelectedTabPage = xtraTab_OranTarifesi;
            }
        }
        public void TARIFE_FARKLI_KAYDET(string _TARIFE_KODU ,string  _TARIFE_ID)
        {
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage == null) return;
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
            {
              
                    if (DW_LIST_TELEVIZYON_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_TELEVIZYON_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.TELEVIZYON_ROW_ADD(DW_LIST_TELEVIZYON_TARIFE, _TARIFE_KODU, _TARIFE_ID, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    }
               
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
            {
                 
                    if (DW_LIST_NONE_MEASURED_TV != null)
                    {
                        // SATIR EKLE
                        DW_LIST_NONE_MEASURED_TV.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.TELEVIZYON_NONE_MEASURED_ROW_ADD(DW_LIST_NONE_MEASURED_TV, _TARIFE_KODU, _TARIFE_ID, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    }
                
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            {
                
                    if (DW_LIST_RADYO_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_RADYO_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.RADYO_ROW_ADD(DW_LIST_RADYO_TARIFE, _TARIFE_KODU, _TARIFE_ID, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    }
               
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
            {
                
                    if (DW_LIST_GAZETE_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_GAZETE_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.GAZETE_ROW_ADD(DW_LIST_GAZETE_TARIFE, _TARIFE_KODU, GAZETE_TARIFE_SECENEK, LBL_TARIFE_GUNLUK_STANDART.Text, _TARIFE_ID, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    }
                
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
            {
               
                    if (DW_LIST_DERGI_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_DERGI_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.DERGI_ROW_ADD(DW_LIST_DERGI_TARIFE, _TARIFE_KODU, _TARIFE_ID, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    }
                
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
            {
              
                    if (DW_LIST_SINEMA_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_SINEMA_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.SINEMA_ROW_ADD(DW_LIST_SINEMA_TARIFE, _TARIFE_KODU, _TARIFE_ID, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    }
              
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
            {
                
                    if (DW_LIST_OUTDOOR_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_OUTDOOR_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.OUTDOOR_ROW_ADD(DW_LIST_OUTDOOR_TARIFE, _TARIFE_KODU, _TARIFE_ID, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    }
               
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar")
            {
              
                    if (DW_LIST_ORANLAR != null)
                    {
                        // SATIR EKLE
                        DW_LIST_ORANLAR.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.ORANLAR_ROW_ADD(DW_LIST_ORANLAR, _TARIFE_KODU, LBL_ORANLAR_TARIFE_KODU.Tag.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                    }
              
            }
        }


        public void TARIFE_PAYLAS_KAYDET(string _TARIFE_KODU, string _TARIFE_ID, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage == null) return;
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
            {
                if (LBL_TELEVIZYON_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_TELEVIZYON_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_TELEVIZYON_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.TELEVIZYON_ROW_ADD(DW_LIST_TELEVIZYON_TARIFE, _TARIFE_KODU, _TARIFE_ID, _KULLANICI_FIRMA, _KULLANICI_NAME);
                    }
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
            {
                if (LBL_NONETV_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_NONE_MEASURED_TV != null)
                    {
                        // SATIR EKLE
                        DW_LIST_NONE_MEASURED_TV.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.TELEVIZYON_NONE_MEASURED_ROW_ADD(DW_LIST_NONE_MEASURED_TV, _TARIFE_KODU, _TARIFE_ID, _KULLANICI_FIRMA, _KULLANICI_NAME);
                    }
                }
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            {
                if (LBL_RADYO_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_RADYO_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_RADYO_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.RADYO_ROW_ADD(DW_LIST_RADYO_TARIFE, _TARIFE_KODU, _TARIFE_ID, _KULLANICI_FIRMA, _KULLANICI_NAME);
                    }
                }
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
            {
                if (LBL_GAZETE_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_GAZETE_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_GAZETE_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.GAZETE_ROW_ADD(DW_LIST_GAZETE_TARIFE, _TARIFE_KODU, GAZETE_TARIFE_SECENEK, LBL_TARIFE_GUNLUK_STANDART.Text, _TARIFE_ID, _KULLANICI_FIRMA, _KULLANICI_NAME);
                    }
                }
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
            {
                if (LBL_DERGI_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_DERGI_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_DERGI_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.DERGI_ROW_ADD(DW_LIST_DERGI_TARIFE, _TARIFE_KODU, _TARIFE_ID, _KULLANICI_FIRMA, _KULLANICI_NAME);
                    }
                }
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
            {
                if (LBL_SINEMA_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_SINEMA_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_SINEMA_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.SINEMA_ROW_ADD(DW_LIST_SINEMA_TARIFE, _TARIFE_KODU, _TARIFE_ID, _KULLANICI_FIRMA, _KULLANICI_NAME);
                    }
                }
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
            {
                if (LBL_OUTDOOR_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_OUTDOOR_TARIFE != null)
                    {
                        // SATIR EKLE
                        DW_LIST_OUTDOOR_TARIFE.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.OUTDOOR_ROW_ADD(DW_LIST_OUTDOOR_TARIFE, _TARIFE_KODU, _TARIFE_ID, _KULLANICI_FIRMA, _KULLANICI_NAME);
                    }
                }
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar")
            {
                if (LBL_ORANLAR_TARIFE_KODU.Text.Length > 0)
                {
                    if (DW_LIST_ORANLAR != null)
                    {
                        // SATIR EKLE
                        DW_LIST_ORANLAR.RowStateFilter = DataViewRowState.CurrentRows;
                        GLOBAL.ORANLAR_ROW_ADD(DW_LIST_ORANLAR, _TARIFE_KODU, _TARIFE_ID, _KULLANICI_FIRMA, _KULLANICI_NAME);
                    }
                }
            }
        }



        private void newToolStripButton_Click(object sender, EventArgs e)
        {

        }

        private void xtraTabControl_TARIFE_DETAY_CloseButtonClick(object sender, EventArgs e)
        {
            int index = xtraTabControl_TARIFE_DETAY.SelectedTabPageIndex;
            xtraTabControl_TARIFE_DETAY.TabPages.RemoveAt(xtraTabControl_TARIFE_DETAY.SelectedTabPageIndex);
            xtraTabControl_TARIFE_DETAY.SelectedTabPageIndex = index;
        }

        private void closeToolStripButton_Click(object sender, EventArgs e)
        {
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage == null) return;

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
            {

                LBL_TELEVIZYON_TARIFE_KODU.Text = ""; LBL_TELEVIZYON_TARIFE_KODU.Tag = "";
                gridControl_TARIFE.DataSource = DW_LIST_TELEVIZYON_TARIFE = null;
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
            {
                LBL_NONETV_TARIFE_KODU.Text = ""; LBL_NONETV_TARIFE_KODU.Tag = "";
                gridControl_NONE_MEASURED_TV.DataSource = DW_LIST_NONE_MEASURED_TV = null;
            }
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
            {
                LBL_RADYO_TARIFE_KODU.Text = ""; LBL_RADYO_TARIFE_KODU.Tag = "";
                gridControl_RADYO.DataSource = DW_LIST_RADYO_TARIFE = null;
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
            {
                LBL_GAZETE_TARIFE_KODU.Text = ""; LBL_GAZETE_TARIFE_KODU.Tag = "";
                gridControl_GAZETE.DataSource = DW_LIST_GAZETE_TARIFE = null;
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
            {
                LBL_DERGI_TARIFE_KODU.Text = ""; LBL_DERGI_TARIFE_KODU.Tag = "";
                gridControl_DERGI.DataSource = DW_LIST_DERGI_TARIFE = null;
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
            {
                LBL_SINEMA_TARIFE_KODU.Text = ""; LBL_SINEMA_TARIFE_KODU.Tag = "";
                gridControl_SINEMA.DataSource = DW_LIST_SINEMA_TARIFE = null;
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
            {
                LBL_OUTDOOR_TARIFE_KODU.Text = ""; LBL_OUTDOOR_TARIFE_KODU.Tag = "";
                gridControl_OUTDOOR.DataSource = DW_LIST_OUTDOOR_TARIFE = null;
            }

            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar")
            {
                LBL_ORANLAR_TARIFE_KODU.Text = ""; LBL_ORANLAR_TARIFE_KODU.Tag = "";
                gridControl_ORAN_TARIFESI.DataSource = DW_LIST_ORANLAR = null;
            }
            //if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();         
        }

        private void saveasToolStripButton_Click(object sender, EventArgs e)
        {
            string _TARIFE_ID = "0";
            if (xtraTabControl_TARIFE_DETAY.SelectedTabPage == null) return;

            TARIFELER._GLOBAL_TARIFELER GLOBALTARIFELER = new TARIFELER._GLOBAL_TARIFELER();
            TARIFELER._TARIFE_FARKLI_KAYDET frm = new TARIFELER._TARIFE_FARKLI_KAYDET();


          
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon") { frm.TXT_FILE_NAME_ESKI_KODU.Text = LBL_TELEVIZYON_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp") { frm.TXT_FILE_NAME_ESKI_KODU.Text = LBL_NONETV_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo") { frm.TXT_FILE_NAME_ESKI_KODU.Text = LBL_RADYO_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete") { frm.TXT_FILE_NAME_ESKI_KODU.Text = LBL_GAZETE_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi") { frm.TXT_FILE_NAME_ESKI_KODU.Text = LBL_DERGI_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema") { frm.TXT_FILE_NAME_ESKI_KODU.Text = LBL_SINEMA_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor") { frm.TXT_FILE_NAME_ESKI_KODU.Text = LBL_OUTDOOR_TARIFE_KODU.Text; }
                if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar") { frm.TXT_FILE_NAME_ESKI_KODU.Text = LBL_ORANLAR_TARIFE_KODU.Text; }

             if (frm.TXT_FILE_NAME_ESKI_KODU.Text.Length > 0)
             {   frm.ShowDialog();
                if (frm._BTN_TYPE != "Close")
                {
                    if (frm._TXT_FILE_NAME.Length > 0)
                    {
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Televizyon")
                        { 
                            _TARIFE_ID = GLOBALTARIFELER.FARKLI_KAYDET(frm._TXT_FILE_NAME, "TELEVIZYON", "TARIFE", false, "", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL).ToString(); ;
                        }
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "None Tv Grp")
                        { 
                            _TARIFE_ID = GLOBALTARIFELER.FARKLI_KAYDET(frm._TXT_FILE_NAME, "NON GRP", "TARIFE", false, "", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL).ToString(); ;
                        }
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Radyo")
                        { 
                            _TARIFE_ID = GLOBALTARIFELER.FARKLI_KAYDET(frm._TXT_FILE_NAME, "RADYO", "TARIFE", false, "", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL).ToString(); ;
                        } 
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Gazete")
                        { 
                            _TARIFE_ID = GLOBALTARIFELER.FARKLI_KAYDET(frm._TXT_FILE_NAME, "GAZETE", "TARIFE", false, "", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL).ToString(); ;
                        } 
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Dergi")
                        { 
                            _TARIFE_ID = GLOBALTARIFELER.FARKLI_KAYDET(frm._TXT_FILE_NAME, "DERGI", "TARIFE", false, "", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL).ToString(); ;
                        } 
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Sinema")
                        { 
                            _TARIFE_ID = GLOBALTARIFELER.FARKLI_KAYDET(frm._TXT_FILE_NAME, "SINEMA", "TARIFE", false, "", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL).ToString(); ;
                        } 
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Outdoor")
                        { 
                            _TARIFE_ID = GLOBALTARIFELER.FARKLI_KAYDET(frm._TXT_FILE_NAME, "OUTDOOR", "TARIFE", false, "", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL).ToString(); ;
                        } 
                        if (xtraTabControl_TARIFE_DETAY.SelectedTabPage.Text.ToString() == "Oranlar")
                        { 
                            _TARIFE_ID = GLOBALTARIFELER.FARKLI_KAYDET(frm._TXT_FILE_NAME, "ORANLAR", "TARIFE", false, "", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL).ToString(); ;
                        }

                        TARIFE_FARKLI_KAYDET(frm._TXT_FILE_NAME, _TARIFE_ID); 
                    }
                }
            }
        }

    }
}