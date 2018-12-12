using System;
using System.Data;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data.SqlClient;
using DevExpress.XtraTreeList.Nodes;
using System.Collections;
using DevExpress.Spreadsheet;
using System.IO;
using System.Net;
using System.Text;

namespace CoreV2
{
    public partial class _MASTERS : DevExpress.XtraBars.Ribbon.RibbonForm
    {  
        int childCount = 0;
        public static CoreV2.PLANLAMA.DESIGNER.RAPOR_DESIGNER RaporDesigner;
        public static CoreV2.PLANLAMA.VIEWER.RAPOR_VIEWER RaporViewer;
         

        ArrayList KIRILIM_NAME = new ArrayList();
        ArrayList KIRILIM_CAST = new ArrayList();
        ArrayList TEMP_TABLE_CREATE = new ArrayList();
        ArrayList INSERT_HEADER = new ArrayList();        
        ArrayList SELECT_TELEVIZYON = new ArrayList();
        ArrayList SELECT_GAZETE = new ArrayList();
        ArrayList SELECT_DERGI = new ArrayList();
        ArrayList SELECT_SINEMA = new ArrayList();
        ArrayList SELECT_RADYO = new ArrayList();
        ArrayList SELECT_OUTDOOR = new ArrayList();
        ArrayList SELECT_INTERNET = new ArrayList();
        ArrayList SELECT_ONBES_DK = new ArrayList();         
        SqlDataReader myReaderData;
        DataTable MyTable;

        string QUERY;

        string TMP_QUERY_TELEVIZYON = string.Empty, TMP_QUERY_GAZETE = string.Empty, TMP_QUERY_DERGI = string.Empty, TMP_QUERY_SINEMA = string.Empty, TMP_QUERY_RADYO = string.Empty, TMP_QUERY_OUTDOOR = string.Empty, TMP_QUERY_INTERNET = string.Empty,
                   QUERY_TELEVIZYON = string.Empty, QUERY_GAZETE = string.Empty, QUERY_DERGI = string.Empty, QUERY_SINEMA = string.Empty, QUERY_RADYO = string.Empty, QUERY_OUTDOOR = string.Empty, QUERY_INTERNET = string.Empty;

        string DAHIL_HARIC = string.Empty, SABITLER_SELECT_NAME = string.Empty;       
        string OZEL_TANIMLAMA = string.Empty, OZEL_TANIMLAMA_CAST = string.Empty, OZEL_TANIMLAMA_FIELD = string.Empty, OZEL_TABLE_CREATE = string.Empty;
        string BASLIK = string.Empty, BASLIK_FIELD = string.Empty;
        string OLCUM = string.Empty, OLCUM_CAST = string.Empty, OLCUM_FIELD = string.Empty, OLCUM_TABLE_CREATE = string.Empty;
        string FILITRE = string.Empty, FILITRE_CAST = string.Empty, FILITRE_FIELD = string.Empty, FILITRE_TABLE_CREATE = string.Empty;
        string TABLE_CREATE_FIELD_NAME = string.Empty, TABLE_CREATE_INSERT_QUERY = string.Empty;
        int _TELEVIZYON = 0, _RADYO = 0, _GAZETE = 0, _DERGI = 0, _SINEMA = 0, _INTERNET = 0, _OUTDOOR = 0;
        string _CHANNEL = string.Empty;
        string TABLO_DURUMU = string.Empty; 
        string BASLIK_KONTROL = string.Empty;
        string RAPOR_SAHIBI = "SAHIP"; 

        int COLUMS_COUNT = 0; 
        private DevExpress.XtraSplashScreen.SplashScreenManager splashScreenManagers;
        private void MASTER_RIBBON_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            // string a = e.Item.Name[0].ToString();
        }

        private void MASTER_RIBBON_Click(object sender, EventArgs e)
        {
            //  string a = e.ToString();

        }

        private void BTN_RAPORU_YENIDEN_GONDER_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RAPORU_YENIDEN_EXEL_AKTAR();
        }

        private void MASTER_RIBBON_SelectedPageChanging(object sender, DevExpress.XtraBars.Ribbon.RibbonPageChangingEventArgs e)
        {
            //  if (e.Page.Text == "Dosya") { RAPOR_LOAD(); MASTER_RIBBON.SelectedPage = RIB_DESIGNER; } 
        } 
        public _MASTERS()
        {
            InitializeComponent();
            MASTER_RIBBON.SelectedPage = RIB_DESIGNER;
            if (_GLOBAL_PARAMETRELER._KULLANICI_MAIL == null)
            { _LOGIN frm = new _LOGIN();
                frm.ShowDialog();
                BR_KULLANICI_KODU.Caption = frm.TXT_USERNAME.Text;
            }
            childCount++;
            RaporDesigner = new PLANLAMA.DESIGNER.RAPOR_DESIGNER("", "", "",false,"","","");
            RaporDesigner.Text = string.Format("{0}-{1}", "Yeni Rapor Designer", childCount);
            RaporDesigner.MdiParent = this;
            RaporDesigner.Show();
            YENI_RAPOR();
        }

        public void _REFRESH(string DURUMU)
        {
            BR_RAPOR_TYPE.Caption = DURUMU;
        }

        private void BTN_DASH_DESIGNER_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //childCount++;
            //RAPORLA.GENEL_RAPOR_DESIGNE designe = new RAPORLA.GENEL_RAPOR_DESIGNE();
            //designe.Text = string.Format("{0}-{1}", "Designer", childCount);
            //designe.MdiParent = this;
            //designe.Show();
        }

        private void BTN_DASH_VIEW_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //childCount++;
            //RAPORLA.GENEL_RAPOR_VIEW VIEW = new RAPORLA.GENEL_RAPOR_VIEW() { Text = string.Format("{0}-{1}", "VIEW", childCount), MdiParent = this };
            //VIEW.Show();
        }
        private void BTN_YENI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            YENI_RAPOR();
        }
        void YENI_RAPOR()
        {
            splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
            splashScreenManagers.ClosingDelay = 300;
            splashScreenManagers.ShowWaitForm();
            splashScreenManagers.SetWaitFormDescription("Rapor Ekrana Yükleniyor.");
            RaporDesigner.Text = "Yeni Rapor";
            BR_RAPOR_KODU.Caption = "";
            BR_RAPOR_ID.Caption = "";
            BR_RAPOR_TYPE.Caption = "TEMP";

         //   BTN_FILE_SAVE_ADRESS.EditValue = "C:\\temp\\CoreRapor.xlsx";

          //  BR_RAPOR_SAVE_PATH.Caption = "C:\\temp\\CoreRapor"+ DateTime.Now.ToString().Replace(".", "_").Replace(":", "_") + ".xlsx";
            BR_RAPOR_AUTO_SAVE.EditValue = true;
            BR_RAPOR_ACIKLAMA.EditValue = "";
            _GLOBAL_PARAMETRELER._RAPOR_KODU = "";
            RaporDesigner.CHKBOX_OTUZSN_GRP.Checked = false;
            RaporDesigner.RAPOR_HEADER_READ("TEMP", "", "");
            splashScreenManagers.SetWaitFormDescription("Rapor Ekrna Yüklendi.");
            if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
        }
        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult c = MessageBox.Show("Programı Kapatmak istediğinize emin misiniz?", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (c == DialogResult.Yes)
            {
                Close();
            }
        }

        private void BTN_ADEX_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            DATA_YUKLE.ADEX_DATA_YUKLE todo = new DATA_YUKLE.ADEX_DATA_YUKLE();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }
        private void BTN_EDGE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //childCount++;
            //DATA_YUKLE.GRP_DATA_YUKLE todo = new DATA_YUKLE.GRP_DATA_YUKLE();
            //todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            //todo.MdiParent = this;
            //todo.Show();
        }
        private void BTN_ON_BES_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            DATA_YUKLE.ONBES_DK_DATA_YUKLE todo = new DATA_YUKLE.ONBES_DK_DATA_YUKLE();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }

        private void BTN_LISTE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            RAPOR_LIST_LOAD();
        
        }
        private void RAPOR_LIST_LOAD()
        {
            FROM_KONTROL();
            _MENU_BACK tr = new _MENU_BACK("LISTE","");
            tr.ShowDialog();
            if (tr._BTN_TYPE == "Tamam")
            {
                splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true); 
                splashScreenManagers.ShowWaitForm();
                splashScreenManagers.SetWaitFormDescription("Rapor Ekrana Yükleniyor.");
                if (tr.SELECT_RAPOR_DURUMU == "SABIT")
                {
                    BR_RAPOR_TYPE.Caption = "SABIT";
                    _GLOBAL_PARAMETRELER._SABIT_TABLO_ADI = string.Format("{0}_LNK_{1}", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, tr.SELECT_RAPOR_KODU);
                }

                RaporDesigner.Text = tr.SELECT_RAPOR_KODU;
                BR_RAPOR_KODU.Caption = tr.SELECT_RAPOR_KODU;
                BR_RAPOR_ID.Caption = tr.SELECT_ID;
                BR_RAPOR_TYPE.Caption = tr.SELECT_RAPOR_DURUMU;

              //  BR_RAPOR_SAVE_PATH.Caption = "C:\\temp\\CoreRapor" + DateTime.Now.ToString().Replace(".", "_").Replace(":", "_") + ".xlsx";

                //    BR_RAPOR_SAVE_PATH.Caption = tr.SELECT_RAPOR_PATH;
                BR_RAPOR_AUTO_SAVE.EditValue = tr.SELECT_RAPOR_AUTO_SAVE;
                re_CHK_AUTO_SAVE.ValueChecked = tr.SELECT_RAPOR_AUTO_SAVE;
                BR_RAPOR_ACIKLAMA.EditValue = tr.SELECT_RAPOR_ACIKLAMASI;
                BR_RAPOR_TIPI.Caption = tr.SELECT_RAPOR_TURU;

                if (_GLOBAL_PARAMETRELER._KULLANICI_MAIL == tr.SELECT_OWNER) { RAPOR_SAHIBI = "SAHIP"; } else { RAPOR_SAHIBI = "YABANCI"; } 

                _GLOBAL_PARAMETRELER._RAPOR_KODU = tr.SELECT_RAPOR_KODU;
                RaporDesigner.CHKBOX_OTUZSN_GRP.Checked = false;
                RaporDesigner.RAPOR_HEADER_READ(tr.SELECT_RAPOR_DURUMU, tr.SELECT_ID, tr.SELECT_RAPOR_KODU);
          
                splashScreenManagers.SetWaitFormDescription("Rapor Ekrna Yüklendi.");
                if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
            }
        }
        private void FROM_KONTROL()
        {
            BR_RAPOR_TYPE.Caption = "TEMP";
            if (RaporDesigner == null)
            {
                childCount++;
                RaporDesigner = new PLANLAMA.DESIGNER.RAPOR_DESIGNER(BR_RAPOR_TYPE.Caption, BR_RAPOR_ID.Caption, BR_RAPOR_KODU.Caption,false,"","","");
                RaporDesigner.Text = string.Format("{0}-{1}", "Yeni Rapor Designer", childCount);
                RaporDesigner.MdiParent = this;
                RaporDesigner.Show();
            }
        }

        private void BR_GAZETE_TARIFE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.GAZETE todo = new TARIFELER.GAZETE();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();

        }

        private void BR_TELEVIZYON_TARIFE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.TELEVIZYON todo = new TARIFELER.TELEVIZYON();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }

        private void BR_DERGI_TARIFE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.DERGI todo = new TARIFELER.DERGI();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }

        private void BR_NONE_GRP_TARIFE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.NONE_GRP todo = new TARIFELER.NONE_GRP();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }

        private void BR_RADYO_TARIFE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.RADYO todo = new TARIFELER.RADYO();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }

        private void BR_OUTDOOR_TARIFE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.OUTDOOR todo = new TARIFELER.OUTDOOR();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }

        private void BR_INTERNET_TARIFE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.INTERNET todo = new TARIFELER.INTERNET();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }

        private void BR_SINEMA_TARIFE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.SINEMA todo = new TARIFELER.SINEMA();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }

        private void BR_SEKTOR_TARIFE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.SEKTOR todo = new TARIFELER.SEKTOR();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }

        private void BR_PROGRAM_TARIFE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.PROGRAM todo = new TARIFELER.PROGRAM();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }

        private void BR_ORAN_TARIFE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.ORANLAR todo = new TARIFELER.ORANLAR();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }

        private void BTN_VW_LISTE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            BR_RAPOR_TYPE.Caption = "SABIT";
            if (RaporViewer == null)
            {
                childCount++;
                RaporViewer = new PLANLAMA.VIEWER.RAPOR_VIEWER();
                RaporViewer.Text = string.Format("{0}-{1}", "Yeni Rapor Önizleme", childCount);
                RaporViewer.MdiParent = this;
                RaporViewer.Show();
            }


            _MENU_BACK tr = new _MENU_BACK("LISTE","");
            tr.ShowDialog();
            if (tr._BTN_TYPE == "Tamam")
            {
                splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
                splashScreenManagers.ClosingDelay = 300;
                splashScreenManagers.ShowWaitForm();
                splashScreenManagers.SetWaitFormDescription("Rapor Ekrana Yükleniyor.");
                if (tr.SELECT_RAPOR_DURUMU == "SABIT")
                {
                    BR_RAPOR_TYPE.Caption = "SABIT";
                    _GLOBAL_PARAMETRELER._SABIT_TABLO_ADI = string.Format("{0}_LNK_{1}", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, tr.SELECT_RAPOR_KODU);
                }
                RaporViewer.Text = tr.SELECT_RAPOR_KODU;
                RaporViewer.BR_RAPOR_KODU.Caption = tr.SELECT_RAPOR_KODU;
                BR_RAPOR_ID.Caption = tr.SELECT_ID;
                BR_RAPOR_TYPE.Caption = tr.SELECT_RAPOR_DURUMU;
                //BR_RAPOR_SAVE_PATH.Caption = tr.SELECT_RAPOR_PATH;
              //  BR_RAPOR_SAVE_PATH.Caption = "C:\\temp\\CoreRapor" + DateTime.Now.ToString().Replace(".", "_").Replace(":", "_") + ".xlsx";
                BR_RAPOR_AUTO_SAVE.EditValue = tr.SELECT_RAPOR_AUTO_SAVE;
                BR_RAPOR_ACIKLAMA.EditValue = tr.SELECT_RAPOR_ACIKLAMASI;
                _GLOBAL_PARAMETRELER._RAPOR_KODU = tr.SELECT_RAPOR_KODU;
                RaporViewer.CHKBOX_OTUZSN_GRP.Checked = false;
                RaporViewer.RAPOR_HEADER_READ(tr.SELECT_RAPOR_DURUMU, tr.SELECT_ID, tr.SELECT_RAPOR_KODU);
                splashScreenManagers.SetWaitFormDescription("Rapor Ekrna Yüklendi.");
                if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
            }
        }

        private void BTN_VW_YENI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            RaporViewer = new PLANLAMA.VIEWER.RAPOR_VIEWER();
            RaporViewer.Text = string.Format("{0}-{1}", "Yeni Rapor Önizleme", childCount);
            RaporViewer.MdiParent = this;
            RaporViewer.Show();
        }
        private void BTN_ONBES_DK_RAPOR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            ONBESDKGRP todo = new ONBESDKGRP();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }
        private void BTN_TARIFE_KONTROL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DateTime RUN_DATE = DateTime.Now;
                string SQL = " INSERT INTO [dbo].[ADM_LOG_TABLE] (ISLEM_TURU,RAPOR_KODU,ISLEM_DETAYI,ISLEM_TARIHI,ISLEM_SAATI,ISLEMI_YAPAN) VALUES ('TARIFE','" + _GLOBAL_PARAMETRELER._RAPOR_KODU + "','TARIFE KONTROL','" + RUN_DATE.ToString("yyyy.MM.dd") + "','" + RUN_DATE.ToString("HH:mm:ss") + "','" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "')";
                SqlCommand command = new SqlCommand(SQL, conn);
                conn.Open(); 
                command.ExecuteReader(CommandBehavior.CloseConnection);
                conn.Close();
            }


            TARIFELER.MASTER_KONTROL fr = new TARIFELER.MASTER_KONTROL();
            fr.ShowDialog();

            if (fr._DURUMU == "TAMAM")
            {
                splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
                splashScreenManagers.ClosingDelay = 500;
                splashScreenManagers.ShowWaitForm();
                if (RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Text.Length > 0 )
                {
                    using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                    {
                        SqlCommand commands = new SqlCommand(string.Format(" SELECT  * FROM ( select ANA_YAYIN  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='TELEVIZYON'  group by ANA_YAYIN  EXCEPT  SELECT MECRA_KODU FROM   dbo.TRF_TELEVIZYON  where TARIFE_KODU ='{0}' and  TARIFE_KODU ='{0}' and TARIFE_REF='{1}' and SIRKET_KODU='{2}' and KULLANICI_KODU='{3}' ) x ORDER BY ANA_YAYIN ", RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Tag,_GLOBAL_PARAMETRELER._KULLANICI_FIRMA,_GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);
                        commands.CommandTimeout = 0;
                        connection.Open();
                        SqlDataReader reader = commands.ExecuteReader();
                        SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());                        
                        myConnectionTable.Open();
                        Int32 ixm = 0;
                        while (reader.Read())
                        {
                            splashScreenManagers.SetWaitFormDescription("TELEVIZYON EKLENIYOR " + ixm++);
                            DataRowView rowView = RaporDesigner.TrfSablon.DW_LIST_TELEVIZYON_TARIFE.AddNew();
                            rowView["MECRA_KODU"] = reader["ANA_YAYIN"].ToString();
                            rowView.EndEdit();
                        }
                      //  RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_DURUMU.Text = "KONTOL_EDILDI";
                        myConnectionTable.Close();
                        reader.Close();
                    }
                }

                if (RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Text.Length > 0 )
                {

                    using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                    {
                        SqlCommand commands =null;
                        if (RaporDesigner.TrfSablon.LBL_HESAPLAMA_TURU_GAZETE.Text == "MEDYA")
                        { 
                            if (RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_TURU.Text == "MECRA_SAYFA")
                            { 
                                commands = new SqlCommand(string.Format(" SELECT  * FROM ( select MEDYA,SAYFA_GRUBU  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='GAZETE'  group by MEDYA,SAYFA_GRUBU  EXCEPT  SELECT MECRA_KODU,SAYFA_GRUBU FROM   dbo.TRF_GAZETE   where TARIFE_KODU ='{0}' and  TARIFE_KODU ='{0}' and TARIFE_REF='{1}' and SIRKET_KODU='{2}' and KULLANICI_KODU='{3}' ) x ORDER BY MEDYA,SAYFA_GRUBU ", RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);
                            } 
                            if (RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_TURU.Text == "MECRA")
                            {

                                commands = new SqlCommand(string.Format(" SELECT  * FROM ( select MEDYA  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='GAZETE'  group by MEDYA  EXCEPT  SELECT MECRA_KODU FROM   dbo.TRF_GAZETE   where TARIFE_KODU ='{0}' and  TARIFE_KODU ='{0}' and TARIFE_REF='{1}' and SIRKET_KODU='{2}' and KULLANICI_KODU='{3}' ) x ORDER BY MEDYA ", RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);
                            }   
                        }

                        if (RaporDesigner.TrfSablon.LBL_HESAPLAMA_TURU_GAZETE.Text == "ANA_YAYIN")
                        {

                            if (RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_TURU.Text == "MECRA_SAYFA")
                            {
                                commands = new SqlCommand(string.Format(" SELECT  * FROM ( select ANA_YAYIN as MEDYA,SAYFA_GRUBU  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='GAZETE'  group by ANA_YAYIN,SAYFA_GRUBU  EXCEPT  SELECT MECRA_KODU FROM   dbo.TRF_GAZETE   where TARIFE_KODU ='{0}' and  TARIFE_KODU ='{0}' and TARIFE_REF='{1}' and SIRKET_KODU='{2}' and KULLANICI_KODU='{3}' ) x ORDER BY MEDYA,SAYFA_GRUBU ", RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);
                            }
                            if (RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_TURU.Text == "MECRA")
                            {
                                commands = new SqlCommand(string.Format(" SELECT  * FROM ( select ANA_YAYIN as MEDYA  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='GAZETE'  group by ANA_YAYIN  EXCEPT  SELECT MECRA_KODU FROM   dbo.TRF_GAZETE   where TARIFE_KODU ='{0}' and  TARIFE_KODU ='{0}' and TARIFE_REF='{1}' and SIRKET_KODU='{2}' and KULLANICI_KODU='{3}' ) x ORDER BY MEDYA ", RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);
                            }                         
                        }

                        commands.CommandTimeout = 0;
                        connection.Open();
                        SqlDataReader reader = commands.ExecuteReader();
                        SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        myConnectionTable.Open();
                        Int32 ixm = 0;
                        while (reader.Read())
                        {
                            splashScreenManagers.SetWaitFormDescription("GAZETELER EKLENIYOR " + ixm++);
                            DataRowView rowView = RaporDesigner.TrfSablon.DW_LIST_GAZETE_TARIFE.AddNew();
                            rowView["MECRA_KODU"] = reader["MEDYA"].ToString();

                            if (RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_TURU.Text == "MECRA_SAYFA") rowView["SAYFA_GRUBU"] = reader["SAYFA_GRUBU"].ToString();


                            rowView.EndEdit();
                        }
                        RaporDesigner.TrfSablon.GAZETE_TARIFESI.Tag = "KONTOL_EDILDI";
                        myConnectionTable.Close();
                        reader.Close();
                    }
                }

                if (RaporDesigner.TrfSablon.LBL_DERGI_TARIFE_KODU.Text.Length > 0 )
                {
                    using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                    {
                        SqlCommand commands = null;
                        if (RaporDesigner.TrfSablon.LBL_HESAPLAMA_TURU_DERGI.Text == "MEDYA")
                        {   
                            commands = new SqlCommand(string.Format(" SELECT  * FROM ( select MEDYA  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='DERGI'  group by MEDYA  EXCEPT  SELECT MECRA_KODU FROM   dbo.TRF_DERGI   where TARIFE_KODU = '{0}' and TARIFE_KODU = '{0}' and TARIFE_REF = '{1}' and SIRKET_KODU = '{2}' and KULLANICI_KODU = '{3}' ) x ORDER BY MEDYA ", RaporDesigner.TrfSablon.LBL_DERGI_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_DERGI_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);
                        }
                        if (RaporDesigner.TrfSablon.LBL_HESAPLAMA_TURU_DERGI.Text == "ANA_YAYIN")
                        {
                            commands = new SqlCommand(string.Format(" SELECT  * FROM ( select ANA_YAYIN as MEDYA  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='DERGI'  group by ANA_YAYIN  EXCEPT  SELECT MECRA_KODU FROM   dbo.TRF_DERGI   where TARIFE_KODU = '{0}' and TARIFE_KODU = '{0}' and TARIFE_REF = '{1}' and SIRKET_KODU = '{2}' and KULLANICI_KODU = '{3}' ) x ORDER BY MEDYA ", RaporDesigner.TrfSablon.LBL_DERGI_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_DERGI_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);                            
                        }
                      //  SqlCommand commands = new SqlCommand(string.Format(" SELECT  * FROM ( select MEDYA  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='DERGI'  group by MEDYA  EXCEPT  SELECT MECRA_KODU FROM   dbo.TRF_DERGI   where TARIFE_KODU = '{0}' and TARIFE_KODU = '{0}' and TARIFE_REF = '{1}' and SIRKET_KODU = '{2}' and KULLANICI_KODU = '{3}' ) x ORDER BY MEDYA ", RaporDesigner.TrfSablon.LBL_DERGI_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_DERGI_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_NAME), connection);
                        commands.CommandTimeout = 0;
                        connection.Open();
                        SqlDataReader reader = commands.ExecuteReader();
                        SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        myConnectionTable.Open();
                        Int32 ixm = 0;
                        while (reader.Read())
                        {
                            splashScreenManagers.SetWaitFormDescription("DERGI EKLENIYOR " + ixm++);
                            DataRowView rowView = RaporDesigner.TrfSablon.DW_LIST_DERGI_TARIFE.AddNew();
                            rowView["MECRA_KODU"] = reader["MEDYA"].ToString();
                            rowView.EndEdit();
                        }
                       // RaporDesigner.TrfSablon.LBL_DERGI_TARIFE_KODU.Tag = "KONTOL_EDILDI";
                        myConnectionTable.Close();
                        reader.Close();
                    }
                }

                if (RaporDesigner.TrfSablon.LBL_RADYO_TARIFE_KODU.Text.Length > 0 )
                {
                    using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                    { 
                        SqlCommand commands = new SqlCommand(string.Format(" SELECT  * FROM ( select MEDYA  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='RADYO'  group by MEDYA  EXCEPT  SELECT MECRA_KODU FROM   dbo.TRF_RADYO    where TARIFE_KODU = '{0}' and TARIFE_KODU = '{0}' and TARIFE_REF = '{1}' and SIRKET_KODU = '{2}' and KULLANICI_KODU = '{3}' ) x ORDER BY MEDYA ", RaporDesigner.TrfSablon.LBL_RADYO_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_RADYO_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);
                        commands.CommandTimeout = 0;
                        connection.Open();
                        SqlDataReader reader = commands.ExecuteReader();
                        SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        myConnectionTable.Open();
                        Int32 ixm = 0;
                        while (reader.Read())
                        {
                            splashScreenManagers.SetWaitFormDescription("RADYO EKLENIYOR " + ixm++);
                            DataRowView rowView = RaporDesigner.TrfSablon.DW_LIST_RADYO_TARIFE.AddNew();
                            rowView["MECRA_KODU"] = reader["MEDYA"].ToString();
                            rowView.EndEdit();
                        }
                      //  RaporDesigner.TrfSablon.RADYO_TARIFESI.Tag = "KONTOL_EDILDI";
                        myConnectionTable.Close();
                        reader.Close();
                    }
                }
                if (RaporDesigner.TrfSablon.LBL_SINEMA_TARIFE_KODU.Text.Length > 0)
                {
                    using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                    { 
                        SqlCommand commands = new SqlCommand(string.Format(" SELECT  * FROM ( select MEDYA  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='SINEMA'  group by MEDYA  EXCEPT  SELECT MECRA_KODU FROM   dbo.TRF_SINEMA   where TARIFE_KODU = '{0}' and TARIFE_KODU = '{0}' and TARIFE_REF = '{1}' and SIRKET_KODU = '{2}' and KULLANICI_KODU = '{3}' ) x ORDER BY MEDYA ", RaporDesigner.TrfSablon.LBL_SINEMA_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_SINEMA_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);
                        commands.CommandTimeout = 0;
                        connection.Open();
                        SqlDataReader reader = commands.ExecuteReader();
                        SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        myConnectionTable.Open();
                        Int32 ixm = 0;
                        while (reader.Read())
                        {
                            splashScreenManagers.SetWaitFormDescription("SINEMA EKLENIYOR " + ixm++);
                            DataRowView rowView = RaporDesigner.TrfSablon.DW_LIST_SINEMA_TARIFE.AddNew();
                            rowView["MECRA_KODU"] = reader["MEDYA"].ToString();
                            rowView.EndEdit();
                        }
                     //   RaporDesigner.TrfSablon.SINEMA_TARIFESI.Tag = "KONTOL_EDILDI";
                        myConnectionTable.Close();
                        reader.Close();
                    }
                }
                if (RaporDesigner.TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Text.Length > 0 )
                {
                    using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                    {
                        SqlCommand commands = new SqlCommand(string.Format(" SELECT  * FROM ( select MEDYA,ILI,UNITE  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='OUTDOOR'  group by MEDYA,ILI,UNITE  EXCEPT  SELECT MECRA_KODU,ILI,UNITE  FROM   dbo.TRF_OUTDOOR    where TARIFE_KODU = '{0}' and TARIFE_KODU = '{0}' and TARIFE_REF = '{1}' and SIRKET_KODU = '{2}' and KULLANICI_KODU = '{3}' ) x ORDER BY MEDYA ", RaporDesigner.TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);
                        commands.CommandTimeout = 0;
                        connection.Open();
                        SqlDataReader reader = commands.ExecuteReader();
                        SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        myConnectionTable.Open();
                        Int32 ixm = 0;
                        while (reader.Read())
                        {
                            splashScreenManagers.SetWaitFormDescription("OUTDOOR EKLENIYOR " + ixm++);
                            DataRowView rowView = RaporDesigner.TrfSablon.DW_LIST_OUTDOOR_TARIFE.AddNew();
                            rowView["MECRA_KODU"] = reader["MEDYA"].ToString();
                            rowView["ILI"] = reader["ILI"].ToString();
                            rowView["UNITE"] = reader["UNITE"].ToString();
                            rowView.EndEdit();
                        }
                   //     RaporDesigner.TrfSablon.OUTDOOR_TARIFESI.Tag = "KONTOL_EDILDI";
                        myConnectionTable.Close();
                        reader.Close();
                    }
                }
                if (RaporDesigner.TrfSablon.LBL_INTERNET_TARIFE_KODU.Text.Length > 0 )
                {
                    using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                    {
                        SqlCommand commands = new SqlCommand(string.Format(" SELECT  * FROM ( select MEDYA  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='INTERNET'  group by MEDYA  EXCEPT  SELECT MECRA_KODU  FROM   dbo.TRF_INTERNET    where TARIFE_KODU = '{0}' and TARIFE_KODU = '{0}' and TARIFE_REF = '{1}' and SIRKET_KODU = '{2}' and KULLANICI_KODU = '{3}' ) x ORDER BY MEDYA ", RaporDesigner.TrfSablon.LBL_INTERNET_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_INTERNET_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);

                        commands.CommandTimeout = 0;
                        connection.Open();
                        SqlDataReader reader = commands.ExecuteReader();
                        SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        myConnectionTable.Open();
                        Int32 ixm = 0;
                        while (reader.Read())
                        {
                            splashScreenManagers.SetWaitFormDescription("INTERNET EKLENIYOR " + ixm++);
                            DataRowView rowView = RaporDesigner.TrfSablon.DW_LIST_INTERNET_TARIFE.AddNew();
                            rowView["MECRA_KODU"] = reader["MEDYA"].ToString();
                            rowView.EndEdit();
                        }
                     //   RaporDesigner.TrfSablon.INTERNET_TARIFESI.Tag = "KONTOL_EDILDI";
                        myConnectionTable.Close();
                        reader.Close();
                    }
                }
                if (RaporDesigner.TrfSablon.LBL_SEKTOR_TARIFE_KODU.Text.Length > 0 )
                {
                    using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                    { 
                        SqlCommand commands = new SqlCommand(string.Format(" SELECT  * FROM ( select MEDYA  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='SEKTOR'  group by MEDYA  EXCEPT  SELECT MECRA_KODU  FROM   dbo.TRF_SEKTOR    where TARIFE_KODU = '{0}' and TARIFE_KODU = '{0}' and TARIFE_REF = '{1}' and SIRKET_KODU = '{2}' and KULLANICI_KODU = '{3}' ) x ORDER BY MEDYA ", RaporDesigner.TrfSablon.LBL_SEKTOR_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_SEKTOR_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);
                        commands.CommandTimeout = 0;
                        connection.Open();
                        SqlDataReader reader = commands.ExecuteReader();
                        SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        myConnectionTable.Open();
                        Int32 ixm = 0;
                        while (reader.Read())
                        {
                            splashScreenManagers.SetWaitFormDescription("SEKTOR EKLENIYOR " + ixm++);
                            DataRowView rowView = RaporDesigner.TrfSablon.DW_LIST_SEKTOR_TARIFE.AddNew();
                            rowView["MECRA_KODU"] = reader["MEDYA"].ToString();
                            rowView.EndEdit();
                        }
                      //  RaporDesigner.TrfSablon.SEKTOR_TARIFESI.Tag = "KONTOL_EDILDI";
                        myConnectionTable.Close();
                        reader.Close();
                    }
                }
                if (RaporDesigner.TrfSablon.LBL_NONETV_TARIFE_KODU.Text.Length > 0 )
                {
                    using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                    { 
                        SqlCommand commands = new SqlCommand(string.Format(" SELECT  * FROM ( select MEDYA  from [dbo].[_ADEX_INDEX_DATA]  where YAYIN_SINIFI='TELEVIZYON'  group by MEDYA  EXCEPT  SELECT MECRA_KODU  FROM   dbo.TRF_TELEVIZYON_NONE_MEASURED    where TARIFE_KODU = '{0}' and TARIFE_KODU = '{0}' and TARIFE_REF = '{1}' and SIRKET_KODU = '{2}' and KULLANICI_KODU = '{3}' ) x ORDER BY MEDYA ", RaporDesigner.TrfSablon.LBL_NONETV_TARIFE_KODU.Text, RaporDesigner.TrfSablon.LBL_NONETV_TARIFE_KODU.Tag, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL), connection);                        
                        commands.CommandTimeout = 0;
                        connection.Open();
                        SqlDataReader reader = commands.ExecuteReader();
                        SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        myConnectionTable.Open();
                        Int32 ixm = 0;
                        while (reader.Read())
                        {
                            splashScreenManagers.SetWaitFormDescription("NONE GRP EKLENIYOR " + ixm++);
                            DataRowView rowView = RaporDesigner.TrfSablon.DW_LIST_NONE_MEASURED_TV.AddNew();
                            rowView["MECRA_KODU"] = reader["MEDYA"].ToString();
                            rowView.EndEdit();
                        }
                     //   RaporDesigner.TrfSablon.NONE_TV_TARIFESI.Tag = "KONTOL_EDILDI";
                        myConnectionTable.Close();
                        reader.Close();
                    }
                }

                if (RaporDesigner.TrfSablon.LBL_ORANLAR_TARIFE_KODU.Text.Length > 0 )
                {
                    using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                    {
                        string SQLSTR = "SELECT  dbo._ADEX_INDEX_DATA.YAYIN_SINIFI  FROM dbo._ADEX_INDEX_DATA " +
                                        " WHERE  YAYIN_SINIFI  not IN  (SELECT YAYIN_SINIFI FROM   dbo.TRF_ORANLAR  " +
                                        " where   dbo.TRF_ORANLAR.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_ORANLAR_TARIFE_KODU.Text + "'  group by YAYIN_SINIFI ) " +
                                        " group by dbo._ADEX_INDEX_DATA.YAYIN_SINIFI " +
                                        " Order by dbo._ADEX_INDEX_DATA.YAYIN_SINIFI";
                        SqlCommand commands = new SqlCommand(SQLSTR, connection);
                        commands.CommandTimeout = 0;
                        connection.Open();
                        SqlDataReader reader = commands.ExecuteReader();
                        SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        myConnectionTable.Open();
                        Int32 ixm = 0;
                        while (reader.Read())
                        {
                            splashScreenManagers.SetWaitFormDescription("NONE GRP EKLENIYOR " + ixm++);
                            DataRowView rowView = RaporDesigner.TrfSablon.DW_LIST_ORANLAR.AddNew();
                            rowView["YAYIN_SINIFI"] = reader["YAYIN_SINIFI"].ToString();
                            rowView.EndEdit();
                        }
                      //  RaporDesigner.TrfSablon.ORANLAR_TARIFESI.Tag = "KONTOL_EDILDI";
                        myConnectionTable.Close();
                        reader.Close();
                    }
                }
                if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
            }
        }

        private void BTN_RAPORU_BASLAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            { 
                DateTime RUN_DATE = DateTime.Now;  
                string SQL = " INSERT INTO [dbo].[ADM_LOG_TABLE] (ISLEM_TURU,RAPOR_KODU,ISLEM_DETAYI,ISLEM_TARIHI,ISLEM_SAATI,ISLEMI_YAPAN) VALUES ('RAPOR BASLAT','" + _GLOBAL_PARAMETRELER._RAPOR_KODU+"','RARPOR OLUŞTUR','"+ RUN_DATE.ToString("yyyy.MM.dd") +"','"+ RUN_DATE.ToString("HH:mm:ss") +"','"+_GLOBAL_PARAMETRELER._KULLANICI_MAIL +"')";
                SqlCommand command = new SqlCommand(SQL, conn);
                conn.Open();
                command.CommandTimeout = 0;
                command.ExecuteReader(CommandBehavior.CloseConnection);
                conn.Close();  
            } 

            RAPORU_BASLAT();
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

        private void BTN_RAPORU_PAYLAS_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (RaporDesigner != null)
            {
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    DateTime RUN_DATE = DateTime.Now;
                    string SQL = " INSERT INTO [dbo].[ADM_LOG_TABLE] (ISLEM_TURU,RAPOR_KODU,ISLEM_DETAYI,ISLEM_TARIHI,ISLEM_SAATI,ISLEMI_YAPAN) VALUES ('RAPOR PAYLAŞ','" + _GLOBAL_PARAMETRELER._RAPOR_KODU + "','RAPOR PAYLAŞ','" + RUN_DATE.ToString("yyyy.MM.dd") + "','" + RUN_DATE.ToString("HH:mm:ss") + "','" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL+ "')";
                    SqlCommand command = new SqlCommand(SQL, conn);
                    conn.Open();
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();
                }
                if (BR_RAPOR_KODU.Caption.Length > 0)
                {
                    _MENU_BACK frm = new _MENU_BACK("RAPOR_PAYLAS", BR_RAPOR_KODU.Caption);
                    frm.TXT_SHR_RAPOR_KODU.Text = "Copy " + BR_RAPOR_KODU.Caption;
                    frm._TXT_FILE_NAME = BR_RAPOR_KODU.Caption;
                    frm.ShowDialog();
                    if (frm._BTN_TYPE != "Close")
                    {
                        if (frm.TXT_SHR_RAPOR_KODU.Text.Length > 0)
                        {
                            splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
                            splashScreenManagers.ClosingDelay = 100;
                            splashScreenManagers.ShowWaitForm();
                            splashScreenManagers.SetWaitFormDescription("Rapor Paylaşılıyor.");
                            RaporDesigner._RAPOR_PAYLAS(frm.TXT_SHR_RAPOR_KODU.Text, frm.TXT_SHR_RAPOR_ACIKLAMA.Text, frm.CMB_FRK_FIRMA.Text, frm.CMB_FRK_KULLANICI.Text, BR_RAPOR_SAVE_PATH.Caption);
                            splashScreenManagers.SetWaitFormDescription("Rapor Paylaşıldı.");
                            if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                        }
                        else
                        { MessageBox.Show(" Lütfen Rapor Kodu giriniz."); }
                    }
                }
            }
            else
            { MessageBox.Show(" Lütfen Rapor Ekranı açınız.."); }
        }

        void selectChildren(TreeListNode parent, ArrayList selectedNodes)
        {
            IEnumerator en = parent.Nodes.GetEnumerator();
            TreeListNode child;
            while (en.MoveNext())
            {
                child = (TreeListNode)en.Current;
                if (child.Checked) selectedNodes.Add(child);
                if (child.HasChildren && child.Checked) selectChildren(child, selectedNodes);
            }
        }


        private void PARAMETRE_TEMIZLE()
        { 
            ///
            /// RAPOR PARAMETRELERINI TEMİZLE 
            OZEL_TANIMLAMA = OZEL_TANIMLAMA_CAST = OZEL_TANIMLAMA_FIELD = OZEL_TABLE_CREATE =
            BASLIK = BASLIK_FIELD =   OLCUM = OLCUM_CAST = OLCUM_FIELD = OLCUM_TABLE_CREATE = FILITRE = FILITRE_CAST = FILITRE_FIELD = FILITRE_TABLE_CREATE = string.Empty; 
            COLUMS_COUNT = 0; 
            OZEL_TANIMLAMA =     TABLE_CREATE_FIELD_NAME = TABLE_CREATE_INSERT_QUERY = string.Empty; 
            SABITLER_SELECT_NAME =   string.Empty; 
            MyTable = null;
            MyTable = new DataTable("RAPOR_EXPORT");
            QUERY = string.Empty; 
            _CHANNEL = string.Empty; 
            INSERT_HEADER.Clear();
            TEMP_TABLE_CREATE.Clear();
            SELECT_TELEVIZYON.Clear();
            SELECT_GAZETE.Clear();
            SELECT_DERGI.Clear();
            SELECT_SINEMA.Clear();
            SELECT_RADYO.Clear();
            SELECT_OUTDOOR.Clear();
            SELECT_INTERNET.Clear();
            SELECT_ONBES_DK.Clear();

        }
        private void RAPORU_BASLAT()
        {
            if (splashScreenManagers != null) { if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm(); }

            splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
            splashScreenManagers.ClosingDelay = 500;
            PARAMETRE_TEMIZLE();
            ////
            //// RAPORLANACAK ALANLARI SEÇ
            ////
            if (RaporDesigner != null)
            {
                if (!RaporDesigner.TOOGLE_TELEVIZYON.IsOn && !RaporDesigner.TOOGLE_GAZETE.IsOn && !RaporDesigner.TOOGLE_DERGI.IsOn && 
                    !RaporDesigner.TOOGLE_OUTDOOR.IsOn && !RaporDesigner.TOOGLE_SINEMA.IsOn && !RaporDesigner.TOOGLE_RADYO.IsOn && !RaporDesigner.TOOGLE_INTERNET.IsOn)
                {
                    MessageBox.Show("Mecra Türü Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return;
                }
                if (RaporDesigner.gridView_BASLIKLAR.RowCount < 1)
                {
                    MessageBox.Show("Rapor Başlıkları seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return;
                }
                if (RaporDesigner.gridView_OLCUMLEME.RowCount < 1)
                {
                    MessageBox.Show("Ölçümlerden bir alan seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return;
                }
                if (RaporDesigner.TOGGLE_TARIFE.IsOn)
                {
                    if (RaporDesigner.TOOGLE_TELEVIZYON.IsOn) RaporDesigner.ZORUNLU_ALANLARI_ISARETLE("TELEVIZYON");
                    if (RaporDesigner.TOOGLE_TELEVIZYON.IsOn) RaporDesigner.ZORUNLU_ALANLARI_ISARETLE("NONEGRP");
                    if (RaporDesigner.TOOGLE_RADYO.IsOn) RaporDesigner.ZORUNLU_ALANLARI_ISARETLE("RADYO");
                    if (RaporDesigner.TOOGLE_GAZETE.IsOn) RaporDesigner.ZORUNLU_ALANLARI_ISARETLE("GAZETE");
                    if (RaporDesigner.TOOGLE_DERGI.IsOn) RaporDesigner.ZORUNLU_ALANLARI_ISARETLE("DERGI");
                    if (RaporDesigner.TOOGLE_SINEMA.IsOn) RaporDesigner.ZORUNLU_ALANLARI_ISARETLE("SINEMA");
                    if (RaporDesigner.TOOGLE_OUTDOOR.IsOn) RaporDesigner.ZORUNLU_ALANLARI_ISARETLE("OUTDOOR");
                    if (RaporDesigner.TOOGLE_INTERNET.IsOn) RaporDesigner.ZORUNLU_ALANLARI_ISARETLE("INTERNET");
                }

                SqlConnection myCon = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                myCon.Open();
                ///
                /// Kırılımlı Rapor İçin Node Kontrol
                ///   
                if (RaporDesigner.treeList_KIRILIMLAR.Nodes.Count != 0)
                {
                    List<TreeListNode> nds = RaporDesigner.treeList_KIRILIMLAR.GetAllCheckedNodes();
                    foreach (TreeListNode node in nds)
                    {
                        if (node.Checked)
                        {
                            KIRILIM_NAME.Clear();
                            string PATH_ = GetFullPath(node, "\\");
                            PATH_ = PATH_.Substring(1, PATH_.Length - 1);
                            string[] wordm = PATH_.Split('\\');
                            if (COLUMS_COUNT < wordm.Length) COLUMS_COUNT = wordm.Length;
                        }
                    }
                    if (COLUMS_COUNT > 0)
                    {
                        for (int i = 1; i <= COLUMS_COUNT; i++)
                        {
                            KIRILIM_NAME.Add(string.Format("[KIRILIM_{0}],", i)); 
                            TEMP_TABLE_CREATE.Add(string.Format("[KIRILIM_{0}] [nvarchar] (70) NULL ,", i)); 
                        }
                    }
                }
                ///
                /// Seçilen Başlıkları Node Kontrol
                ///    
                for (int iX = 0; iX <= RaporDesigner.gridView_BASLIKLAR.RowCount; iX++)
                {
                    DataRow DR = RaporDesigner.gridView_BASLIKLAR.GetDataRow(iX);
                    if (DR != null)
                    {
                        if (DR["TEXT"].ToString() != "")
                        {                       
                            SABITLER_VE_OLCUMLER_TABLOSUNU_OKU(RaporDesigner.CHKBOX_OTUZSN_GRP.Checked, DR["TEXT"].ToString());
                        }
                    }
                }
                /// 
                /// Ölçümler Node Kontrol
                ///   
                for (int iX = 0; iX <= RaporDesigner.gridView_OLCUMLEME.RowCount; iX++)
                {
                    DataRow DR = RaporDesigner.gridView_OLCUMLEME.GetDataRow(iX);
                    if (DR != null)
                    {
                        if (DR["TEXT"].ToString() != "")
                        {
                            SABITLER_VE_OLCUMLER_TABLOSUNU_OKU(RaporDesigner.CHKBOX_OTUZSN_GRP.Checked, DR["TEXT"].ToString());
                        }
                    }
                }

                if (RaporDesigner.TOOGLE_TELEVIZYON.IsOn || RaporDesigner.TOOGLE_RADYO.IsOn) SABITLER_VE_OLCUMLER_TABLOSUNU_OKU(RaporDesigner.CHKBOX_OTUZSN_GRP.Checked, "TİME İNT");

                ///
                /// Tabloyu Oluştur
                ///   
                TABLE_CREATE();
                ///
                ///
                if (BR_RAPOR_TYPE.Caption == "SABIT")
                {
                    DateTime BAS_TARIHI = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BAS_TARIHI.EditValue);
                    DateTime BIT_TARIHI = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BIT_TARIHI.EditValue);
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        conn.Open();
                        string SQL = string.Format("select top 1 * from {0}.[dbo].[__TEMP_{1}] Where  (YIL >= " + BAS_TARIHI.ToString("yyyy") + "  AND YIL <= " + BAS_TARIHI.ToString("yyyy") + ") AND (AY >= " + BAS_TARIHI.ToString("MM") + " and  AY <= " + BAS_TARIHI.ToString("MM") + ")", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._SABIT_TABLO_ADI);
                        SqlCommand cmd = new SqlCommand(SQL, conn) { CommandTimeout = 0 };
                        SqlDataReader myReader = cmd.ExecuteReader();
                        while (myReader.Read())
                        {
                            DialogResult c = MessageBox.Show("Data Tekrarı Yapıyorsunuz İçerdeki Datayı Silmelisiniz!", "Uyarı", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                            if (c == DialogResult.Yes)
                            {
                                using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                                {
                                    myConnection.Open();
                                    string SQL_DELETE = string.Format("DELETE  from  {0}.[dbo].[__TEMP_{1}] Where  (YIL >= " + BAS_TARIHI.ToString("yyyy") + "  AND YIL <= " + BAS_TARIHI.ToString("yyyy") + ") AND (AY >= " + BAS_TARIHI.ToString("MM") + " and  AY <= " + BAS_TARIHI.ToString("MM") + ")", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._SABIT_TABLO_ADI);
                                    SqlCommand cmdCon = new SqlCommand(SQL_DELETE, myConnection) { CommandTimeout = 0 };
                                    cmdCon.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }

                splashScreenManagers.ShowWaitForm();
                if (BASLIK.Length > 0) BASLIK = BASLIK.Substring(0, BASLIK.Length - 1);
                ///
                /// Kırılımlı Rapor İçin Node Kontrol
                ///    
                string PATH_KIRILIMLAR = "";
                if (RaporDesigner.treeList_KIRILIMLAR.Nodes.Count != 0)
                {
                    List<TreeListNode> ndsc = RaporDesigner.treeList_KIRILIMLAR.GetAllCheckedNodes();
                    foreach (TreeListNode nodes in ndsc)
                    {
                        if (nodes.Checked)
                        {
                            KIRILIM_NAME.Clear();
                            KIRILIM_CAST.Clear();
                            PATH_KIRILIMLAR = GetFullPath(nodes, "\\");
                            PATH_KIRILIMLAR = PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);
                            string[] wordm = PATH_KIRILIMLAR.Split('\\');
                            if (COLUMS_COUNT < wordm.Length) COLUMS_COUNT = wordm.Length;
                            if (COLUMS_COUNT > 0)
                            {
                                int NodeSay = 0;
                                for (int i = 1; i <= COLUMS_COUNT; i++)
                                {
                                    KIRILIM_NAME.Add(string.Format("[KIRILIM_{0}],", i));
                                    KIRILIM_CAST.Add(string.Format(" CAST('{0}' AS nvarchar ) AS  [KIRILIM_{1}],", wordm[NodeSay], i));
                                    if (NodeSay < wordm.Length - 1) NodeSay++;
                                }
                            }
                            splashScreenManagers.SetWaitFormDescription(PATH_KIRILIMLAR);
                            if (RaporDesigner.TOGGLE_OZEL_FILITRE.IsOn && RaporDesigner.treeList_FILITRELER.Nodes.Count > 0)
                            {
                                OZEL_FILITRE(PATH_KIRILIMLAR, "CALISMA");
                            }
                            else
                            {
                                FILITRE_SQL_DATA_READ_INSERT("");
                            }
                        }
                    }
                }
                splashScreenManagers.SetWaitFormDescription("Lütfen Bekleyiniz.");
                if (RaporDesigner.TOGGLE_MASTER.IsOn) MASTER_ALL_BASLASIN();// MASTER_BASLASIN();
                if (RaporDesigner.TOGGLE_WORD.IsOn) KEYWORD_BASLASIN();
                if (RaporDesigner.TOGGLE_TARIFE.IsOn) HESAPLAMA_BASLASIN();

                RAPOR_OLUSTUR();

                if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
            }
            else
            { MessageBox.Show(" Lütfen Rapor seçiniz."); }
        }
        private void OZEL_FILITRE(string PATH_KIRILIMLAR, string KONTROL)
        {
            TreeListNode myNode = RaporDesigner.treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == "KIRILIM#" + PATH_KIRILIMLAR; });
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
                        //  break;
                        if (TMP_QUERY_TELEVIZYON.Length > 0 || TMP_QUERY_GAZETE.Length > 0 || TMP_QUERY_DERGI.Length > 0 || TMP_QUERY_SINEMA.Length > 0 || TMP_QUERY_RADYO.Length > 0 || TMP_QUERY_OUTDOOR.Length > 0 || TMP_QUERY_INTERNET.Length > 0)
                        {
                            if (RaporDesigner.TOGGLE_GENEL_FILITRE.IsOn && RaporDesigner.treeList_GENEL_FILITRELER.Nodes.Count > 0)
                            {
                                GENEL_FILITRE();
                            }
                            if (KONTROL == "CALISMA") FILITRE_SQL_DATA_READ_INSERT(myNode.GetDisplayText("TEXT").ToString());
                            if (KONTROL != "CALISMA") FILITRE_SQL_DATA_READ(myNode.GetDisplayText("TEXT").ToString(), KONTROL);
                            break;
                        }
                        else
                        { break; }
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

                        if (item.GetValue("NAME").ToString() != "")
                        {
                            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                SqlCommand myCommand = new SqlCommand(string.Format(" SELECT   * FROM   dbo.ADM_SECENEKLER where SECENEK='{0}'", item.GetValue("NAME").ToString()), myConnection);
                                myConnection.Open();
                                SqlDataReader myReader = myCommand.ExecuteReader();
                                while (myReader.Read())
                                {
                                    BASLIK_KONTROL = "DT." + myReader["FIELDS"].ToString();
                                    if (myReader["TELEVIZYON"].ToString().IndexOf("CAST") == -1) _TELEVIZYON = 1; else _TELEVIZYON = 0;
                                    if (myReader["RADYO"].ToString().IndexOf("CAST") == -1) _RADYO = 1; else _RADYO = 0;
                                    if (myReader["GAZETE"].ToString().IndexOf("CAST") == -1) _GAZETE = 1; else _GAZETE = 0;
                                    if (myReader["DERGI"].ToString().IndexOf("CAST") == -1) _DERGI = 1; else _DERGI = 0;
                                    if (myReader["SINEMA"].ToString().IndexOf("CAST") == -1) _SINEMA = 1; else _SINEMA = 0;
                                    if (myReader["INTERNET"].ToString().IndexOf("CAST") == -1) _INTERNET = 1; else _INTERNET = 0;
                                    if (myReader["OUTDOOR"].ToString().IndexOf("CAST") == -1) _OUTDOOR = 1; else _OUTDOOR = 0;
                                }
                            }
                        }
                    }
                    if (item.GetValue("TYPE").ToString() == "SATIR")
                    {
                        if (DAHIL_HARIC == "Dahil")
                        {
                            if (item.GetValue("TEXT").ToString().IndexOf("%") != -1)
                            {
                                if (_TELEVIZYON == 1) QUERY_TELEVIZYON += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_GAZETE == 1) QUERY_GAZETE += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_DERGI == 1) QUERY_DERGI += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_SINEMA == 1) QUERY_SINEMA += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_RADYO == 1) QUERY_RADYO += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_OUTDOOR == 1) QUERY_OUTDOOR += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_INTERNET == 1) QUERY_INTERNET += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                            }
                            else
                            {
                                if (_TELEVIZYON == 1) QUERY_TELEVIZYON += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_GAZETE == 1) QUERY_GAZETE += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_DERGI == 1) QUERY_DERGI += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_SINEMA == 1) QUERY_SINEMA += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_RADYO == 1) QUERY_RADYO += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_OUTDOOR == 1) QUERY_OUTDOOR += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_INTERNET == 1) QUERY_INTERNET += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT"));
                            }
                        }

                        if (DAHIL_HARIC == "Hariç")
                        {
                            if (item.GetValue("TEXT").ToString().IndexOf("%") != -1)
                            {
                                if (_TELEVIZYON == 1) QUERY_TELEVIZYON += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_GAZETE == 1) QUERY_GAZETE += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_DERGI == 1) QUERY_DERGI += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_SINEMA == 1) QUERY_SINEMA += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_RADYO == 1) QUERY_RADYO += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_OUTDOOR == 1) QUERY_OUTDOOR += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_INTERNET == 1) QUERY_INTERNET += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                            }
                            else
                            {
                                if (_TELEVIZYON == 1) QUERY_TELEVIZYON += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_GAZETE == 1) QUERY_GAZETE += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_DERGI == 1) QUERY_DERGI += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_SINEMA == 1) QUERY_SINEMA += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_RADYO == 1) QUERY_RADYO += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_OUTDOOR == 1) QUERY_OUTDOOR += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
                                if (_INTERNET == 1) QUERY_INTERNET += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT"));
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

                if (QUERY_TELEVIZYON.Length > 0 || QUERY_GAZETE.Length > 0 || QUERY_DERGI.Length > 0 || QUERY_SINEMA.Length > 0 || QUERY_RADYO.Length > 0 || QUERY_OUTDOOR.Length > 0 || QUERY_INTERNET.Length > 0)
                {
                    if (RaporDesigner.TOGGLE_GENEL_FILITRE.IsOn && RaporDesigner.treeList_GENEL_FILITRELER.Nodes.Count > 0)
                    {
                        GENEL_FILITRE();
                    }

                    if (KONTROL == "CALISMA") FILITRE_SQL_DATA_READ_INSERT(myNode.GetDisplayText("TEXT").ToString());
                    if (KONTROL != "CALISMA") FILITRE_SQL_DATA_READ(myNode.GetDisplayText("TEXT").ToString(), KONTROL);
                }
            }
        }
        private void GENEL_FILITRE()
        {
            if (RaporDesigner.TOGGLE_GENEL_FILITRE.IsOn)
            {
                if (RaporDesigner.treeList_GENEL_FILITRELER.Nodes.Count != 0)
                {
                    QUERY_TELEVIZYON = QUERY_GAZETE = QUERY_DERGI = QUERY_SINEMA = QUERY_RADYO = QUERY_OUTDOOR = QUERY_INTERNET = "";

                    List<TreeListNode> nds = RaporDesigner.treeList_GENEL_FILITRELER.GetNodeList();
                    foreach (TreeListNode ite in nds)
                    {
                        if (ite.GetValue("TEXT").ToString().IndexOf("(Dahil)") != -1) DAHIL_HARIC = "Dahil";
                        if (ite.GetValue("TEXT").ToString().IndexOf("(Hariç)") != -1) DAHIL_HARIC = "Hariç";

                        if (ite.GetValue("TYPE").ToString() == "BASLIK")
                        {
                            if (QUERY_TELEVIZYON != "") TMP_QUERY_TELEVIZYON += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_TELEVIZYON, 4, "").ToString());
                            if (QUERY_GAZETE != "") TMP_QUERY_GAZETE += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_GAZETE, 4, "").ToString());
                            if (QUERY_DERGI != "") TMP_QUERY_DERGI += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_DERGI, 4, "").ToString());
                            if (QUERY_SINEMA != "") TMP_QUERY_SINEMA += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_SINEMA, 4, "").ToString());
                            if (QUERY_RADYO != "") TMP_QUERY_RADYO += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_RADYO, 4, "").ToString());
                            if (QUERY_OUTDOOR != "") TMP_QUERY_OUTDOOR += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_OUTDOOR, 4, "").ToString());
                            if (QUERY_INTERNET != "") TMP_QUERY_INTERNET += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY_INTERNET, 4, "").ToString());

                            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                SqlCommand myCommand = new SqlCommand(string.Format(" SELECT   * FROM   dbo.ADM_SECENEKLER where SECENEK='{0}'", ite.GetValue("TEXT").ToString().Replace(" (Dahil)", "").Replace(" (Hariç)", "")), myConnection);
                                myConnection.Open();
                                SqlDataReader myReader = myCommand.ExecuteReader();
                                while (myReader.Read())
                                {
                                    //BASLIK_KONTROL = myReader["FIELDS"].ToString();
                                    BASLIK_KONTROL = "DT." + myReader["FIELDS"].ToString();
                                    if (myReader["TELEVIZYON"].ToString().IndexOf("CAST") == -1) _TELEVIZYON = 1; else _TELEVIZYON = 0;
                                    if (myReader["RADYO"].ToString().IndexOf("CAST") == -1) _RADYO = 1; else _RADYO = 0;
                                    if (myReader["GAZETE"].ToString().IndexOf("CAST") == -1) _GAZETE = 1; else _GAZETE = 0;
                                    if (myReader["DERGI"].ToString().IndexOf("CAST") == -1) _DERGI = 1; else _DERGI = 0;
                                    if (myReader["SINEMA"].ToString().IndexOf("CAST") == -1) _SINEMA = 1; else _SINEMA = 0;
                                    if (myReader["INTERNET"].ToString().IndexOf("CAST") == -1) _INTERNET = 1; else _INTERNET = 0;
                                    if (myReader["OUTDOOR"].ToString().IndexOf("CAST") == -1) _OUTDOOR = 1; else _OUTDOOR = 0;
                                }
                            }
                        }

                        if (ite.GetValue("TYPE").ToString() == "SATIR")
                        {
                            if (DAHIL_HARIC == "Dahil")
                            {
                                if (ite.GetValue("TEXT").ToString().IndexOf("%") != -1)
                                {
                                    if (_TELEVIZYON == 1) TMP_QUERY_TELEVIZYON += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_GAZETE == 1) TMP_QUERY_GAZETE += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_DERGI == 1) TMP_QUERY_DERGI += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_SINEMA == 1) TMP_QUERY_SINEMA += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_RADYO == 1) TMP_QUERY_RADYO += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_OUTDOOR == 1) TMP_QUERY_OUTDOOR += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_INTERNET == 1) TMP_QUERY_INTERNET += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                }
                                else
                                {
                                    if (_TELEVIZYON == 1) TMP_QUERY_TELEVIZYON += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_GAZETE == 1) TMP_QUERY_GAZETE += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_DERGI == 1) TMP_QUERY_DERGI += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_SINEMA == 1) TMP_QUERY_SINEMA += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_RADYO == 1) TMP_QUERY_RADYO += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_OUTDOOR == 1) TMP_QUERY_OUTDOOR += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_INTERNET == 1) TMP_QUERY_INTERNET += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                }
                            }

                            if (DAHIL_HARIC == "Hariç")
                            {
                                if (ite.GetValue("TEXT").ToString().IndexOf("%") != -1)
                                {
                                    if (_TELEVIZYON == 1) TMP_QUERY_TELEVIZYON += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_GAZETE == 1) TMP_QUERY_GAZETE += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_DERGI == 1) TMP_QUERY_DERGI += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_SINEMA == 1) TMP_QUERY_SINEMA += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_RADYO == 1) TMP_QUERY_RADYO += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_OUTDOOR == 1) TMP_QUERY_OUTDOOR += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_INTERNET == 1) TMP_QUERY_INTERNET += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                }
                                else
                                {
                                    if (_TELEVIZYON == 1) TMP_QUERY_TELEVIZYON += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_GAZETE == 1) TMP_QUERY_GAZETE += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_DERGI == 1) TMP_QUERY_DERGI += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_SINEMA == 1) TMP_QUERY_SINEMA += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_RADYO == 1) TMP_QUERY_RADYO += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_OUTDOOR == 1) TMP_QUERY_OUTDOOR += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                    if (_INTERNET == 1) TMP_QUERY_INTERNET += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, ite.GetValue("TEXT"));
                                }
                            }
                        }
                    }
                }
            }
        }

        private string TEMIZLE_ELEMAN(string ALAN, int Deger, string TIP)
        {
            if (TIP != "")
            {
                if (ALAN.Length > Deger) { if (ALAN.Substring(ALAN.Length - Deger, Deger) == TIP) { ALAN = ALAN.Substring(0, ALAN.Length - Deger); } }
            }
            else
            {
                if (ALAN.Length > Deger) { ALAN = ALAN.Substring(0, ALAN.Length - Deger); }
            }
            return ALAN;
        }         
        private void FILITRE_SQL_DATA_READ_INSERT(string NodeName)
        {
            string KIRILIM_CST=string.Empty;
            string KIRILIM_NAME = string.Empty;
            string BASLIK_NAME = string.Empty;

            string FIELD_TELEVIZYON = string.Empty, FIELD_TELEVIZYON_GROUP_BY = string.Empty, FIELD_TELEVIZYON_SUM = string.Empty,
                   FIELD_GAZETE = string.Empty, FIELD_GAZETE_GROUP_BY = string.Empty, FIELD_GAZETE_SUM = string.Empty,
                   FIELD_DERGI = string.Empty, FIELD_DERGI_GROUP_BY = string.Empty, FIELD_DERGI_SUM = string.Empty,
                   FIELD_SINEMA = string.Empty, FIELD_SINEMA_GROUP_BY = string.Empty, FIELD_SINEMA_SUM = string.Empty,
                   FIELD_RADYO = string.Empty, FIELD_RADYO_GROUP_BY = string.Empty, FIELD_RADYO_SUM = string.Empty,
                   FIELD_OUTDOOR = string.Empty, FIELD_OUTDOOR_GROUP_BY = string.Empty, FIELD_OUTDOOR_SUM = string.Empty,
                   FIELD_INTERNET = string.Empty, FIELD_INTERNET_GROUP_BY = string.Empty, FIELD_INTERNET_SUM = string.Empty,
                   FIELD_ONBES_DK = string.Empty;


            for (int iIndex = 0; iIndex < KIRILIM_CAST.Count; iIndex++)
            {
                KIRILIM_CST += KIRILIM_CAST[iIndex].ToString().ToUpper();
            }

            for (int iIndex = 0; iIndex < this.KIRILIM_NAME.Count; iIndex++)
            {
                KIRILIM_NAME += this.KIRILIM_NAME[iIndex].ToString().ToUpper();
            }
            for (int iIndex = 0; iIndex < INSERT_HEADER.Count; iIndex++)
            {
                BASLIK_NAME += INSERT_HEADER[iIndex].ToString().ToUpper();
            }
            for (int iIndex = 0; iIndex < SELECT_TELEVIZYON.Count; iIndex++)
            {
                FIELD_TELEVIZYON += SELECT_TELEVIZYON[iIndex].ToString().ToUpper();
            }
            for (int iIndex = 0; iIndex < SELECT_GAZETE.Count; iIndex++)
            {
                FIELD_GAZETE += SELECT_GAZETE[iIndex].ToString().ToUpper()  ;
            }
            for (int iIndex = 0; iIndex < SELECT_DERGI.Count; iIndex++)
            {
                FIELD_DERGI += SELECT_DERGI[iIndex].ToString().ToUpper();
            }
            for (int iIndex = 0; iIndex < SELECT_SINEMA.Count; iIndex++)
            {
                FIELD_SINEMA += SELECT_SINEMA[iIndex].ToString().ToUpper();
            }
            for (int iIndex = 0; iIndex < SELECT_OUTDOOR.Count; iIndex++)
            {
                FIELD_OUTDOOR += SELECT_OUTDOOR[iIndex].ToString().ToUpper();
            }
            for (int iIndex = 0; iIndex < SELECT_RADYO.Count; iIndex++)
            {
                FIELD_RADYO += SELECT_RADYO[iIndex].ToString().ToUpper();
            }
            for (int iIndex = 0; iIndex < SELECT_INTERNET.Count; iIndex++)
            {
                FIELD_INTERNET += SELECT_INTERNET[iIndex].ToString().ToUpper();
            }

            BASLIK_NAME = TEMIZLE_ELEMAN(BASLIK_NAME, 1, ",").ToString();            
            FIELD_TELEVIZYON = TEMIZLE_ELEMAN(FIELD_TELEVIZYON, 1, ",").ToString();
            FIELD_GAZETE = TEMIZLE_ELEMAN(FIELD_GAZETE, 1, ",").ToString();
            FIELD_DERGI = TEMIZLE_ELEMAN(FIELD_DERGI, 1, ",").ToString();
            FIELD_SINEMA = TEMIZLE_ELEMAN(FIELD_SINEMA, 1, ",").ToString();
            FIELD_OUTDOOR = TEMIZLE_ELEMAN(FIELD_OUTDOOR, 1, ",").ToString();
            FIELD_RADYO = TEMIZLE_ELEMAN(FIELD_RADYO, 1, ",").ToString();
            FIELD_INTERNET = TEMIZLE_ELEMAN(FIELD_INTERNET, 1, ",").ToString();
            
            DateTime BAS_TARIHI = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BAS_TARIHI.EditValue);
            DateTime BIT_TARIHI = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BIT_TARIHI.EditValue);
            if (RaporDesigner.TOOGLE_TELEVIZYON.IsOn)
            {
                splashScreenManagers.SetWaitFormCaption(NodeName.ToString());
                splashScreenManagers.SetWaitFormDescription(" TELEVİZYON DATASI OLUŞTURULUYOR");
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    //WITH (TABLOCK) 
                    //string SQLTELEVIZYON = " INSERT INTO  [dbo].[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "] (" + STATIC_NAME + rpr.TABLE_CREATE_INSERT_QUERY + " )" +
                    //  " SELECT " + CAST_FIELD + rpr.FIELD_TELEVIZYON + " " +
                    //  " FROM  dbo.DATA_TELEVIZYON_ADEX as DT " +
                    //  " Where  " + QUERY + "  (TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))";

                 


                    string SQLTELEVIZYON = "";
                    if (BAS_TARIHI.Year == BIT_TARIHI.Year)
                    {
                        string SQLJOIN = "", SQLWHERE="";
                        ArrayList list = GetArrayList_MASTER_ALL_EXCLUDE("TELEVIZYON");             
                        splashScreenManagers.SetWaitFormDescription(" Dahil Hariç Kontrolu Yapılıyor");
                        for (int i = 0; i < list.Count; i++)
                        {
                            SQLJOIN += list[i];
                            i++;
                            SQLWHERE += "AND "+ list[i];
                        }
                        SQLJOIN = TEMIZLE_ELEMAN(SQLJOIN, 4, "").ToString();
                        FIELD_TELEVIZYON = FIELD_TELEVIZYON.Replace("DT.CAST", "CAST").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");
                        SQLTELEVIZYON = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, KIRILIM_NAME, BASLIK_NAME) +
                                                      " SELECT " + KIRILIM_CST + FIELD_TELEVIZYON + " " +
                                                      " FROM  dbo.DATA_TELEVIZYON_ADEX_" + BAS_TARIHI.Year + " as DT " + SQLJOIN +
                                                      " Where  " + TMP_QUERY_TELEVIZYON + "  (DT.TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (DT.TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))" +
                                                      "  " + SQLWHERE;
                    }
                    else
                    {


                        string SQLJOIN = "", SQLWHERE = "";
                        ArrayList list = GetArrayList_MASTER_ALL_EXCLUDE("TELEVIZYON");
                        splashScreenManagers.SetWaitFormDescription(" Dahil Hariç Kontrolu Yapılıyor");
                        for (int i = 0; i < list.Count; i++)
                        {
                            SQLJOIN += list[i];
                            i++;
                            SQLWHERE += "AND " + list[i];
                        }
                        SQLJOIN = TEMIZLE_ELEMAN(SQLJOIN, 4, "").ToString();
                        FIELD_TELEVIZYON = FIELD_TELEVIZYON.Replace("DT.CAST", "CAST").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");  
                        SQLTELEVIZYON = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, KIRILIM_NAME, BASLIK_NAME);
                        for (int iX = 0; iX <= BIT_TARIHI.Year - BAS_TARIHI.Year; iX++)
                        {
                            int YILLAR = BAS_TARIHI.Year + iX;

                               SQLTELEVIZYON +=  " SELECT " + KIRILIM_CST + FIELD_TELEVIZYON + " " +
                                                 " FROM  dbo.DATA_TELEVIZYON_ADEX_" + YILLAR + " as DT " + SQLJOIN +
                                                 " Where  " + TMP_QUERY_TELEVIZYON + "  (DT.TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (DT.TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))" +
                                                 "  " + SQLWHERE + " UNION ALL "; 
                        }
                        if (SQLTELEVIZYON != "") { if (SQLTELEVIZYON.Length > 0) { SQLTELEVIZYON = SQLTELEVIZYON.Substring(0, SQLTELEVIZYON.Length - 11); } }


                            //SQLTELEVIZYON = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM_NAME, BASLIK_NAME);
                            //for (int iX = 0; iX <= BIT_TARIHI.Year - BAS_TARIHI.Year; iX++)
                            //{   int YILLAR = BAS_TARIHI.Year + iX;

                            //    string SQLJOIN = "";
                            //    SQLTELEVIZYON +=" SELECT " + KIRILIM_CST + FIELD_TELEVIZYON + " " +
                            //                    " FROM  dbo.DATA_TELEVIZYON_ADEX_" + YILLAR + " as DT " + SQLJOIN +
                            //                    " Where  " + TMP_QUERY_TELEVIZYON + "  (TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))" +                                          
                            //                    " UNION ALL ";
                            //}
                          //if (SQLTELEVIZYON != "") { if (SQLTELEVIZYON.Length > 0) { SQLTELEVIZYON = SQLTELEVIZYON.Substring(0, SQLTELEVIZYON.Length - 11); } }
                    }

                    SqlCommand command = new SqlCommand(SQLTELEVIZYON, conn);
                    conn.Open();
                    command.CommandTimeout = 0;
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();
                }
            }
            if (RaporDesigner.TOOGLE_GAZETE.IsOn)
            {
                splashScreenManagers.SetWaitFormCaption(NodeName.ToString());
                splashScreenManagers.SetWaitFormDescription(" GAZETE DATASI OLUŞTURULUYOR");
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQLJOIN = "", SQLWHERE = "";
                    ArrayList list = GetArrayList_MASTER_ALL_EXCLUDE("GAZETE");           
                    splashScreenManagers.SetWaitFormDescription(" GAZETE Dahil Hariç Kontrolu Yapılıyor");
                    for (int i = 0; i < list.Count; i++)
                    {
                        SQLJOIN += list[i];
                        i++;
                        SQLWHERE += "AND " + list[i];
                    }
                    SQLJOIN = TEMIZLE_ELEMAN(SQLJOIN, 4, "").ToString();
                    FIELD_GAZETE = FIELD_GAZETE.Replace("DT.CAST", "CAST").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");

                    string  SQLGAZETE = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, KIRILIM_NAME, BASLIK_NAME) +
                                                  " SELECT " + KIRILIM_CST + FIELD_GAZETE + " " +
                                                  " FROM  dbo.DATA_BASIN_ADEX as DT " + SQLJOIN +
                                                  " Where  DT.YAYIN_SINIFI='GAZETE'  AND " + TMP_QUERY_GAZETE + "  (DT.TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (DT.TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))" +
                                                  "  " + SQLWHERE; 
                    SqlCommand command = new SqlCommand(SQLGAZETE, conn);
                    conn.Open();
                    command.CommandTimeout = 0;
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();


             //       string SQLJOIN = "", SQLWHERE = "";//= MASTER_ALL_EXCLUDE();

             //       ArrayList list = GetArrayList_MASTER_ALL_EXCLUDE();


             //       for (int i = 0; i < list.Count; i++)
             //       {
             //           SQLJOIN += list[i];
             //           i++;
             //           SQLWHERE += "AND " + list[i];
             //       }

             //       SQLJOIN = TEMIZLE_ELEMAN(SQLJOIN, 4, "").ToString();

             //       FIELD_GAZETE = FIELD_GAZETE.Replace("DT.cast", "cast").Replace("DT.substring", "substring");
             ////       FIELD_GAZETE = FIELD_GAZETE.Replace("DT.cast", "cast").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE");
             //       string SQLGAZETE = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM_NAME, BASLIK_NAME);
             //       SQLGAZETE += String.Format(" SELECT " + KIRILIM_CST + FIELD_GAZETE + "  FROM dbo.DATA_BASIN_ADEX as DT " + SQLJOIN + " Where  {0}", TMP_QUERY_GAZETE);
             //       SQLGAZETE += string.Format(" DT.YAYIN_SINIFI='GAZETE'  AND  (DT.TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (DT.TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
             //       SQLGAZETE += "  " + SQLWHERE;
             //       SqlCommand command = new SqlCommand(SQLGAZETE, conn);
             //       conn.Open();
             //       command.CommandTimeout = 0;
             //       command.ExecuteReader(CommandBehavior.CloseConnection);
             //       conn.Close();
                }
            }
            if (RaporDesigner.TOOGLE_DERGI.IsOn)
            {
                splashScreenManagers.SetWaitFormCaption(NodeName.ToString());
                splashScreenManagers.SetWaitFormDescription(" DERGI DATASI OLUŞTURULUYOR");
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {

                    string SQLJOIN = "", SQLWHERE = "";
                    ArrayList list = GetArrayList_MASTER_ALL_EXCLUDE("DERGI");
                    splashScreenManagers.SetWaitFormDescription(" DERGI Dahil Hariç Kontrolu Yapılıyor");
                    for (int i = 0; i < list.Count; i++)
                    {
                        SQLJOIN += list[i];
                        i++;
                        SQLWHERE += "AND " + list[i];
                    }
                    SQLJOIN = TEMIZLE_ELEMAN(SQLJOIN, 4, "").ToString();
                    FIELD_DERGI = FIELD_DERGI.Replace("DT.CAST", "CAST").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");

                    string SQLGAZETE = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, KIRILIM_NAME, BASLIK_NAME) +
                                                  " SELECT " + KIRILIM_CST + FIELD_DERGI + " " +
                                                  " FROM  dbo.DATA_BASIN_ADEX as DT " + SQLJOIN +
                                                  " Where  DT.YAYIN_SINIFI='DERGI'  AND " + TMP_QUERY_DERGI + "  (DT.TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (DT.TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))" +
                                                  "  " + SQLWHERE;
                    SqlCommand command = new SqlCommand(SQLGAZETE, conn);
                    conn.Open();
                    command.CommandTimeout = 0;
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();



                    //FIELD_DERGI = FIELD_DERGI.Replace("DT.cast", "cast").Replace("DT.CAST", "CAST").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");
                    //string SQLDERGI = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]   ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM_NAME, BASLIK_NAME);
                    //SQLDERGI += String.Format(" SELECT " + KIRILIM_CST + FIELD_DERGI + "    FROM dbo.DATA_BASIN_ADEX as DT Where  {0}", TMP_QUERY_DERGI);
                    //SQLDERGI += string.Format(" YAYIN_SINIFI='DERGI'  AND  (TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                    //SqlCommand command = new SqlCommand(SQLDERGI, conn);
                    //conn.Open();
                    //command.CommandTimeout = 0;
                    //command.ExecuteReader(CommandBehavior.CloseConnection);
                    //conn.Close();
                }
            }

            if (RaporDesigner.TOOGLE_OUTDOOR.IsOn)
            {
                splashScreenManagers.SetWaitFormCaption(NodeName.ToString());
                splashScreenManagers.SetWaitFormDescription(" OUTDOOR DATASI OLUŞTURULUYOR");
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {

                    string SQLJOIN = "", SQLWHERE = "";
                    ArrayList list = GetArrayList_MASTER_ALL_EXCLUDE("OUTDOOR");
                    splashScreenManagers.SetWaitFormDescription(" OUTDOOR Dahil Hariç Kontrolu Yapılıyor");
                    for (int i = 0; i < list.Count; i++)
                    {
                        SQLJOIN += list[i];
                        i++;
                        SQLWHERE += "AND " + list[i];
                    }
                    SQLJOIN = TEMIZLE_ELEMAN(SQLJOIN, 4, "").ToString();
                    FIELD_OUTDOOR = FIELD_OUTDOOR.Replace("DT.CAST", "CAST").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");

                    string SQLOUTDOOR = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, KIRILIM_NAME, BASLIK_NAME) +
                                                  " SELECT " + KIRILIM_CST + FIELD_OUTDOOR + " " +
                                                  " FROM  dbo.DATA_OUTDOOR_ADEX as DT " + SQLJOIN +
                                                  " Where  " + TMP_QUERY_OUTDOOR + "  (DT.TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (DT.TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))" +
                                                  "  " + SQLWHERE;
                    SqlCommand command = new SqlCommand(SQLOUTDOOR, conn);
                    conn.Open();
                    command.CommandTimeout = 0;
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();

                    //FIELD_OUTDOOR = FIELD_OUTDOOR.Replace("DT.cast", "cast").Replace("DT.CAST", "CAST").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");
                    //string SQLOUTDOOR = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]   ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM_NAME, BASLIK_NAME);
                    //SQLOUTDOOR += String.Format(" SELECT " + KIRILIM_CST + FIELD_OUTDOOR + "   FROM dbo.DATA_OUTDOOR_ADEX as DT Where  {0}", TMP_QUERY_OUTDOOR);
                    //SQLOUTDOOR += string.Format("  (TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                    //SqlCommand command = new SqlCommand(SQLOUTDOOR, conn);
                    //conn.Open();
                    //command.CommandTimeout = 0;
                    //command.ExecuteReader(CommandBehavior.CloseConnection);
                    //conn.Close();
                }
            }

            if (RaporDesigner.TOOGLE_SINEMA.IsOn)
            {
                splashScreenManagers.SetWaitFormCaption(NodeName.ToString());
                splashScreenManagers.SetWaitFormDescription(" SINEMA DATASI OLUŞTURULUYOR");
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {

                    string SQLJOIN = "", SQLWHERE = "";
                    ArrayList list = GetArrayList_MASTER_ALL_EXCLUDE("SINEMA");
                    splashScreenManagers.SetWaitFormDescription(" SINEMA Dahil Hariç Kontrolu Yapılıyor");
                    for (int i = 0; i < list.Count; i++)
                    {
                        SQLJOIN += list[i];
                        i++;
                        SQLWHERE += "AND " + list[i];
                    }
                    SQLJOIN = TEMIZLE_ELEMAN(SQLJOIN, 4, "").ToString();
                    FIELD_SINEMA = FIELD_SINEMA.Replace("DT.CAST", "CAST").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");

                    string SQLGAZETE = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, KIRILIM_NAME, BASLIK_NAME) +
                                                  " SELECT " + KIRILIM_CST + FIELD_SINEMA + " " +
                                                  " FROM  dbo.DATA_SINEMA_ADEX as DT " + SQLJOIN +
                                                  " Where  " + TMP_QUERY_SINEMA + "  (DT.TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (DT.TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))" +
                                                  "  " + SQLWHERE;
                    SqlCommand command = new SqlCommand(SQLGAZETE, conn);
                    conn.Open();
                    command.CommandTimeout = 0;
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();
                    
                    //FIELD_SINEMA = FIELD_SINEMA.Replace("DT.cast", "cast").Replace("DT.CAST", "CAST").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");
                    //string SQLSINEMA = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]   ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM_NAME, BASLIK_NAME);
                    //SQLSINEMA += String.Format(" SELECT " + KIRILIM_CST + FIELD_SINEMA + "    FROM dbo.DATA_SINEMA_ADEX as DT Where  {0}", TMP_QUERY_SINEMA);
                    //SQLSINEMA += string.Format(" (TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                    //SqlCommand command = new SqlCommand(SQLSINEMA, conn);
                    //conn.Open();
                    //command.CommandTimeout = 0;
                    //command.ExecuteReader(CommandBehavior.CloseConnection);
                    //conn.Close();
                }
            }

            if (RaporDesigner.TOOGLE_RADYO.IsOn)
            {
                splashScreenManagers.SetWaitFormCaption(NodeName.ToString());
                splashScreenManagers.SetWaitFormDescription(" RADYO DATASI OLUŞTURULUYOR");
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                { 
                    string SQLJOIN = "", SQLWHERE = "";
                    ArrayList list = GetArrayList_MASTER_ALL_EXCLUDE("RADYO");
                    splashScreenManagers.SetWaitFormDescription(" RADYO Dahil Hariç Kontrolu Yapılıyor");
                    for (int i = 0; i < list.Count; i++)
                    {
                        SQLJOIN += list[i];
                        i++;
                        SQLWHERE += "AND " + list[i];
                    }
                    SQLJOIN = TEMIZLE_ELEMAN(SQLJOIN, 4, "").ToString();
                    FIELD_RADYO = FIELD_RADYO.Replace("DT.CAST", "CAST").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");

                    string SQLRADYO = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, KIRILIM_NAME, BASLIK_NAME) +
                                                  " SELECT " + KIRILIM_CST + FIELD_RADYO + " " +
                                                  " FROM  dbo.DATA_RADYO_ADEX as DT " + SQLJOIN +
                                                  " Where  " + TMP_QUERY_RADYO + "  (DT.TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (DT.TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))" +
                                                  "  " + SQLWHERE;
                    SqlCommand command = new SqlCommand(SQLRADYO, conn);
                    conn.Open();
                    command.CommandTimeout = 0;
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();
                    
                    //FIELD_RADYO = FIELD_RADYO.Replace("DT.cast", "cast").Replace("DT.CAST", "CAST").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");
                    //string SQLRADYO = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM_NAME, BASLIK_NAME);
                    //SQLRADYO += String.Format(" SELECT " + KIRILIM_CST + FIELD_RADYO + "  FROM dbo.DATA_RADYO_ADEX as DT Where  {0}", TMP_QUERY_RADYO);
                    //SQLRADYO += string.Format(" (TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                    //SqlCommand command = new SqlCommand(SQLRADYO, conn);
                    //conn.Open();
                    //command.CommandTimeout = 0;
                    //command.ExecuteReader(CommandBehavior.CloseConnection);
                    //conn.Close();
                }
            }
            if (RaporDesigner.TOOGLE_INTERNET.IsOn)
            { splashScreenManagers.SetWaitFormCaption(NodeName.ToString());
                splashScreenManagers.SetWaitFormDescription(" INTERNET DATASI OLUŞTURULUYOR");
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {

                    string SQLJOIN = "", SQLWHERE = "";
                    ArrayList list = GetArrayList_MASTER_ALL_EXCLUDE("INTERNET");
                    splashScreenManagers.SetWaitFormDescription(" INTERNET Dahil Hariç Kontrolu Yapılıyor");
                    for (int i = 0; i < list.Count; i++)
                    {
                        SQLJOIN += list[i];
                        i++;
                        SQLWHERE += "AND " + list[i];
                    }
                    SQLJOIN = TEMIZLE_ELEMAN(SQLJOIN, 4, "").ToString();
                    FIELD_INTERNET = FIELD_INTERNET.Replace("DT.CAST", "CAST").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");

                    string SQLINTERNET = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, KIRILIM_NAME, BASLIK_NAME) +
                                                  " SELECT " + KIRILIM_CST + FIELD_INTERNET + " " +
                                                  " FROM  dbo.DATA_INTERNET_ADEX as DT " + SQLJOIN +
                                                  " Where  " + TMP_QUERY_INTERNET + "  (DT.TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (DT.TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))" +
                                                  "  " + SQLWHERE;
                    SqlCommand command = new SqlCommand(SQLINTERNET, conn);
                    conn.Open();
                    command.CommandTimeout = 0;
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();



                    //FIELD_INTERNET = FIELD_INTERNET.Replace("DT.cast", "cast").Replace("DT.CAST", "CAST").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE").Replace("DT.SUBSTRING", "SUBSTRING");
                    //string SQLINTERNET = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]   ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM_NAME, BASLIK_NAME);
                    //SQLINTERNET = String.Format(" SELECT " + KIRILIM_CST + FIELD_INTERNET + "  FROM dbo.DATA_INTERNET_ADEX as DT Where  {0}", TMP_QUERY_INTERNET);
                    //SQLINTERNET += string.Format("    (TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                    //SqlCommand command = new SqlCommand(SQLINTERNET, conn);
                    //conn.Open();
                    //command.CommandTimeout = 0;
                    //command.ExecuteReader(CommandBehavior.CloseConnection);
                    //conn.Close();
                }
            }
            TMP_QUERY_TELEVIZYON = TMP_QUERY_GAZETE = TMP_QUERY_DERGI = TMP_QUERY_SINEMA = TMP_QUERY_RADYO = TMP_QUERY_OUTDOOR = TMP_QUERY_INTERNET = "";
                QUERY_TELEVIZYON =     QUERY_GAZETE =     QUERY_DERGI =     QUERY_SINEMA =     QUERY_RADYO =     QUERY_OUTDOOR =     QUERY_INTERNET = "";
         
   

            //SELECT_TELEVIZYON.Clear();
            //SELECT_GAZETE.Clear();
            //SELECT_DERGI.Clear();
            //SELECT_SINEMA.Clear();
            //SELECT_RADYO.Clear();
            //SELECT_OUTDOOR.Clear();
            //SELECT_INTERNET.Clear();
            //SELECT_ONBES_DK.Clear();

        }

        string SQL = ""; string SELECT_FIELDS = "", GROUP_BY_FIELDS = "";

        private void FILITRE_SQL_DATA_READ(string TMP_QUERY, string MecraType)
        {

            string[] Ones = MecraType.ToString().Split(',');
            if (Ones[0].ToString() != string.Empty)
            {

                for (int c = 0; c < Ones.Length; c++)
                {

                    if (Ones[c].ToString() != string.Empty)
                    {
                        if (Ones[c].ToString() =="TELEVIZYON" || Ones[c].ToString() == "FREE")
                        {
                            if (SQL != "") SQL += " UNION ALL ";
                            SQL += string.Format("select {0} from [dbo].[{1}] DT  WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_TELEVIZYON + "[YAYIN_SINIFI]='TELEVIZYON' ", GROUP_BY_FIELDS);
                        }


                        if (Ones[c].ToString() == "RADYO" || Ones[c].ToString() == "FREE")
                        {
                            if (SQL != "") SQL += " UNION ALL ";
                            SQL += string.Format("select {0} from [dbo].[{1}] DT  WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_RADYO + "[YAYIN_SINIFI]='RADYO' ", GROUP_BY_FIELDS);
                        }

                        if (Ones[c].ToString() == "DERGI" || Ones[c].ToString() == "FREE")
                        {
                            if (SQL != "") SQL += " UNION ALL ";
                            SQL += string.Format("select {0} from [dbo].[{1}] DT  WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_DERGI + "[YAYIN_SINIFI]='DERGI' ", GROUP_BY_FIELDS);
                        }

                        if (Ones[c].ToString() == "GAZETE" || Ones[c].ToString() == "FREE")
                        {
                            if (SQL != "") SQL += " UNION ALL ";
                            SQL += string.Format("select {0} from [dbo].[{1}] DT  WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_GAZETE + "[YAYIN_SINIFI]='GAZETE' ", GROUP_BY_FIELDS);
                        }

                        if (Ones[c].ToString() == "SINEMA" || Ones[c].ToString() == "FREE")
                        {
                            if (SQL != "") SQL += " UNION ALL ";
                            SQL += string.Format("select {0} from [dbo].[{1}] DT  WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_SINEMA + "[YAYIN_SINIFI]='SINEMA' ", GROUP_BY_FIELDS);
                        }

                        if (Ones[c].ToString() == "OUTDOOR" || Ones[c].ToString() == "FREE")
                        {
                            if (SQL != "") SQL += " UNION ALL ";
                            SQL += string.Format("select {0} from [dbo].[{1}] DT  WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_OUTDOOR + "[YAYIN_SINIFI]='OUTDOOR' ", GROUP_BY_FIELDS);
                        }
                        if (Ones[c].ToString() == "INTERNET" || Ones[c].ToString() == "FREE")
                        {
                            if (SQL != "") SQL += " UNION ALL ";
                            SQL += string.Format("select {0} from [dbo].[{1}] DT  WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_INTERNET + "[YAYIN_SINIFI]='INTERNET' ", GROUP_BY_FIELDS);
                        }
                         

                    }

                }


            }


            //    if (RaporDesigner.TOOGLE_TELEVIZYON.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] DT  WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_TELEVIZYON + "[YAYIN_SINIFI]='TELEVIZYON' ", GROUP_BY_FIELDS);                
            //}
            //if (RaporDesigner.TOOGLE_RADYO.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] DT WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_RADYO + "[YAYIN_SINIFI]='RADYO' ", GROUP_BY_FIELDS);
            //}
            //if (RaporDesigner.TOOGLE_DERGI.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] DT WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_DERGI + "[YAYIN_SINIFI]='DERGI' ", GROUP_BY_FIELDS);
            //}
            //if (RaporDesigner.TOOGLE_GAZETE.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] DT WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_GAZETE + "[YAYIN_SINIFI]='GAZETE' ", GROUP_BY_FIELDS);
            //}
            //if (RaporDesigner.TOOGLE_SINEMA.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] DT WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_SINEMA + "[YAYIN_SINIFI]='SINEMA' ", GROUP_BY_FIELDS);
            //}
            //if (RaporDesigner.TOOGLE_OUTDOOR.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] DT WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_OUTDOOR + "[YAYIN_SINIFI]='OUTDOOR' ", GROUP_BY_FIELDS);
            //}
            //if (RaporDesigner.TOOGLE_INTERNET.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] DT WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_INTERNET + "[YAYIN_SINIFI]='INTERNET' ", GROUP_BY_FIELDS);
            //}        
            
            
                
            TMP_QUERY_TELEVIZYON = TMP_QUERY_GAZETE = TMP_QUERY_DERGI = TMP_QUERY_SINEMA = TMP_QUERY_RADYO = TMP_QUERY_OUTDOOR = TMP_QUERY_INTERNET = "";
            QUERY_TELEVIZYON = QUERY_GAZETE = QUERY_DERGI = QUERY_SINEMA = QUERY_RADYO = QUERY_OUTDOOR = QUERY_INTERNET = "";
        }


        public void SABITLER_VE_OLCUMLER_TABLOSUNU_OKU(bool OTUZ_SNGRP, string BASLIKLAR_NODES)
        {
            SABITLER_SELECT_NAME = string.Format("SECENEK='{0}' OR ", BASLIKLAR_NODES);
            if (SABITLER_SELECT_NAME.Length == 0) { MessageBox.Show("Lütfen başlık seçiniz."); return; }

            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = " SELECT   * FROM   dbo.ADM_SECENEKLER where " + SABITLER_SELECT_NAME.Substring(0, SABITLER_SELECT_NAME.Length - 3);
                SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                if (myReader.HasRows)
                {
                    while (myReader.Read())
                    {
                        if (myReader["BASLIK"].ToString() != "HEDEFKİTLELER")
                        { 
                            if (_GLOBAL_PARAMETRELER._DIL == "EN")
                            { if (!INSERT_HEADER.Contains(string.Format("[{0}],", myReader["SECENEK_EN"]))) INSERT_HEADER.Add(string.Format("[{0}],", myReader["SECENEK_" + _GLOBAL_PARAMETRELER._DIL + ""])); }
                            else
                            { if (!INSERT_HEADER.Contains(string.Format("[{0}],", myReader["SECENEK"]))) INSERT_HEADER.Add(string.Format("[{0}],", myReader["SECENEK"])); }

                            switch (myReader["TYPE"].ToString())
                            {
                                case "nvarchar":
                                    string str = "";
                                    if (_GLOBAL_PARAMETRELER._DIL == "EN")
                                    { str = string.Format("[{0}] [nvarchar] ({1}) NULL ,", myReader["SECENEK_"+ _GLOBAL_PARAMETRELER._DIL + ""], myReader["SIZE"]); }
                                    else
                                    { str = string.Format("[{0}] [nvarchar] ({1}) NULL ,", myReader["SECENEK"], myReader["SIZE"]); } 

                                    if (!TEMP_TABLE_CREATE.Contains(str)) TEMP_TABLE_CREATE.Add(str);
                                    break;
                                case "int":
                                case "int32":
                                case "int64":
                                case "float":
                                case "single":
                                case "Int":
                                case "Int32":
                                case "Int64":

                                    string strg = "";
                                    if (_GLOBAL_PARAMETRELER._DIL=="EN")
                                    { strg = string.Format("[{0}] [{1}]  NULL ,", myReader["SECENEK_EN"], myReader["TYPE"]); }
                                    else
                                    { strg = string.Format("[{0}] [{1}]  NULL ,", myReader["SECENEK"], myReader["TYPE"]); }


                                    if (!TEMP_TABLE_CREATE.Contains(strg)) TEMP_TABLE_CREATE.Add(strg);
                                    break;
                                case "smalldatetime": 

                                    string strgs = "";
                                    if (_GLOBAL_PARAMETRELER._DIL == "EN")
                                    { strgs = string.Format("[{0}] [smalldatetime]  NULL ,", myReader["SECENEK_EN"]); }
                                    else
                                    { strgs = string.Format("[{0}] [smalldatetime]  NULL ,", myReader["SECENEK"]); } 
                                   
                                    if (!TEMP_TABLE_CREATE.Contains(strgs)) TEMP_TABLE_CREATE.Add(strgs);
                                    break;
                            }

                            switch (myReader["SECENEK"].ToString().ToUpper())
                            {
                                case "AY":
                                    if (!SELECT_TELEVIZYON.Contains("MONTH(DT.TARIH) as AY,")) SELECT_TELEVIZYON.Add("MONTH(DT.TARIH) as AY,");
                                    if (!SELECT_GAZETE.Contains("MONTH(DT.TARIH) as AY,")) SELECT_GAZETE.Add("MONTH(DT.TARIH) as AY,");
                                    if (!SELECT_DERGI.Contains("MONTH(DT.TARIH) as AY,")) SELECT_DERGI.Add("MONTH(DT.TARIH) as AY,");
                                    if (!SELECT_SINEMA.Contains("MONTH(DT.TARIH) as AY,")) SELECT_SINEMA.Add("MONTH(DT.TARIH) as AY,");
                                    if (!SELECT_RADYO.Contains("MONTH(DT.TARIH) as AY,")) SELECT_RADYO.Add("MONTH(DT.TARIH) as AY,");
                                    if (!SELECT_OUTDOOR.Contains("MONTH(DT.TARIH) as AY,")) SELECT_OUTDOOR.Add("MONTH(DT.TARIH) as AY,");
                                    if (!SELECT_INTERNET.Contains("MONTH(DT.TARIH) as AY,")) SELECT_INTERNET.Add("MONTH(DT.TARIH) as AY,");
                                    break;

                                case "YIL":
                                    if (!SELECT_TELEVIZYON.Contains("YEAR(DT.TARIH) as YIL,")) SELECT_TELEVIZYON.Add("YEAR(DT.TARIH) as YIL,");
                                    if (!SELECT_GAZETE.Contains("YEAR(DT.TARIH) as YIL,")) SELECT_GAZETE.Add("YEAR(DT.TARIH) as YIL,");
                                    if (!SELECT_DERGI.Contains("YEAR(DT.TARIH) as YIL,")) SELECT_DERGI.Add("YEAR(DT.TARIH) as YIL,");
                                    if (!SELECT_SINEMA.Contains("YEAR(DT.TARIH) as YIL,")) SELECT_SINEMA.Add("YEAR(DT.TARIH) as YIL,");
                                    if (!SELECT_RADYO.Contains("YEAR(DT.TARIH) as YIL,")) SELECT_RADYO.Add("YEAR(DT.TARIH) as YIL,");
                                    if (!SELECT_OUTDOOR.Contains("YEAR(DT.TARIH) as YIL,")) SELECT_OUTDOOR.Add("YEAR(DT.TARIH) as YIL,");
                                    if (!SELECT_INTERNET.Contains("YEAR(DT.TARIH) as YIL,")) SELECT_INTERNET.Add("YEAR(DT.TARIH) as YIL,");
                                    break;

                                case "HAFTA":
                                    if (!SELECT_TELEVIZYON.Contains("DATEPART(DT.TARIH) as HAFTA,")) SELECT_TELEVIZYON.Add("DATEPART(ww,DT.TARIH) as HAFTA,");
                                    if (!SELECT_GAZETE.Contains("DATEPART(ww,DT.TARIH) as HAFTA,")) SELECT_GAZETE.Add("DATEPART(ww,DT.TARIH) as HAFTA,");
                                    if (!SELECT_DERGI.Contains("DATEPART(ww,DT.TARIH) as HAFTA,")) SELECT_DERGI.Add("DATEPART(ww,DT.TARIH) as HAFTA,");
                                    if (!SELECT_SINEMA.Contains("DATEPART(ww,DT.TARIH) as HAFTA,")) SELECT_SINEMA.Add("DATEPART(ww,DT.TARIH) as HAFTA,");
                                    if (!SELECT_RADYO.Contains("DATEPART(ww,DT.TARIH) as HAFTA,")) SELECT_RADYO.Add("DATEPART(ww,DT.TARIH) as HAFTA,");
                                    if (!SELECT_OUTDOOR.Contains("DATEPART(ww,DT.TARIH) as HAFTA,")) SELECT_OUTDOOR.Add("DATEPART(ww,DT.TARIH) as HAFTA,");
                                    if (!SELECT_INTERNET.Contains("DATEPART(ww,DT.TARIH) as HAFTA,")) SELECT_INTERNET.Add("DATEPART(ww,DT.TARIH) as HAFTA,");
                                    break;


                                case "GÜN":
                                    if (!SELECT_TELEVIZYON.Contains("DATENAME(dw,DT.TARIH) as GÜN,")) SELECT_TELEVIZYON.Add("DATENAME(dw,DT.TARIH) as GÜN,");
                                    if (!SELECT_GAZETE.Contains("DATENAME(dw,DT.TARIH) as GÜN,")) SELECT_GAZETE.Add("DATENAME(dw,DT.TARIH) as GÜN,");
                                    if (!SELECT_DERGI.Contains("DATENAME(dw,DT.TARIH) as GÜN,")) SELECT_DERGI.Add("DATENAME(dw,DT.TARIH) as GÜN,");
                                    if (!SELECT_SINEMA.Contains("DATENAME(dw,DT.TARIH) as GÜN,")) SELECT_SINEMA.Add("DATENAME(dw,DT.TARIH) as GÜN,");
                                    if (!SELECT_RADYO.Contains("DATENAME(dw,DT.TARIH) as GÜN,")) SELECT_RADYO.Add("DATENAME(dw,DT.TARIH) as GÜN,");
                                    if (!SELECT_OUTDOOR.Contains("DATENAME(dw,DT.TARIH) as GÜN,")) SELECT_OUTDOOR.Add("DATENAME(dw,DT.TARIH) as GÜN,");
                                    if (!SELECT_INTERNET.Contains("DATENAME(dw,DT.TARIH) as GÜN,")) SELECT_INTERNET.Add("DATENAME(dw,DT.TARIH) as GÜN,");
                                    break;
                                default:
                                    if (!SELECT_TELEVIZYON.Contains(string.Format("DT.{0},", myReader["TELEVIZYON"]))) SELECT_TELEVIZYON.Add(string.Format("DT.{0},", myReader["TELEVIZYON"]));
                                    if (!SELECT_GAZETE.Contains(string.Format("DT.{0},", myReader["GAZETE"]))) SELECT_GAZETE.Add(string.Format("DT.{0},", myReader["GAZETE"]));
                                    if (!SELECT_DERGI.Contains(string.Format("DT.{0},", myReader["DERGI"]))) SELECT_DERGI.Add(string.Format("DT.{0},", myReader["DERGI"]));
                                    if (!SELECT_SINEMA.Contains(string.Format("DT.{0},", myReader["SINEMA"]))) SELECT_SINEMA.Add(string.Format("DT.{0},", myReader["SINEMA"]));
                                    if (!SELECT_RADYO.Contains(string.Format("DT.{0},", myReader["RADYO"]))) SELECT_RADYO.Add(string.Format("DT.{0},", myReader["RADYO"]));
                                    if (!SELECT_OUTDOOR.Contains(string.Format("DT.{0},", myReader["OUTDOOR"]))) SELECT_OUTDOOR.Add(string.Format("DT.{0},", myReader["OUTDOOR"]));
                                    if (!SELECT_INTERNET.Contains(string.Format("DT.{0},", myReader["INTERNET"]))) SELECT_INTERNET.Add(string.Format("DT.{0},", myReader["INTERNET"]));
                                    if (!SELECT_ONBES_DK.Contains(string.Format("DT.{0},", myReader["TELEVIZYON"]))) SELECT_ONBES_DK.Add(string.Format("DT.{0},", myReader["TELEVIZYON"]));
                                 break;
                            }
                        }
                        else
                        {
                            if (OTUZ_SNGRP)
                            {
                                if (!INSERT_HEADER.Contains(string.Format(" [{0}],[{1} 30sn] ,", myReader["SECENEK"], myReader["SECENEK"]))) INSERT_HEADER.Add(string.Format(" [{0}],[{1} 30sn] ,", myReader["SECENEK"], myReader["SECENEK"]));
                                if (!TEMP_TABLE_CREATE.Contains(string.Format("[{0}] [float]  NULL ,[{1} 30sn] [float]  NULL ,", myReader["SECENEK"], myReader["SECENEK"]))) TEMP_TABLE_CREATE.Add(string.Format("[{0}] [float]  NULL ,[{1} 30sn] [float]  NULL ,", myReader["SECENEK"], myReader["SECENEK"]));

                                if (!SELECT_TELEVIZYON.Contains(string.Format(" CAST( {0} AS float) as [{1}] , ((CAST( {2} AS float)  /30) * SURE ) as  [{3} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"], myReader["FIELDS"], myReader["SECENEK"]))) SELECT_TELEVIZYON.Add(string.Format(" CAST( {0} AS float) as [{1}] , ((CAST( {2} AS float)  /30) * SURE ) as  [{3} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"], myReader["FIELDS"], myReader["SECENEK"]));
                                if (!SELECT_GAZETE.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]))) SELECT_GAZETE.Add(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]));
                                if (!SELECT_DERGI.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]))) SELECT_DERGI.Add(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]));
                                if (!SELECT_SINEMA.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]))) SELECT_SINEMA.Add(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]));
                                if (!SELECT_RADYO.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]))) SELECT_RADYO.Add(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]));
                                if (!SELECT_OUTDOOR.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]))) SELECT_OUTDOOR.Add(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]));
                                if (!SELECT_INTERNET.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]))) SELECT_INTERNET.Add(string.Format(" CAST( '0' AS float) as [{0}] ,  CAST( '0' AS float) as [{1} 30sn] ,", myReader["FIELDS"], myReader["SECENEK"]));
                            }
                            else
                            {
                                if (!INSERT_HEADER.Contains(string.Format("[{0}],", myReader["SECENEK"]))) INSERT_HEADER.Add(string.Format("[{0}],", myReader["SECENEK"]));
                                if (!TEMP_TABLE_CREATE.Contains(string.Format("[{0}] [float]  NULL ,", myReader["SECENEK"]))) TEMP_TABLE_CREATE.Add(string.Format("[{0}] [float]  NULL ,", myReader["SECENEK"]));
                                if (!SELECT_TELEVIZYON.Contains(string.Format(" CAST( {0} AS float) as [{1}] ,", myReader["FIELDS"], myReader["SECENEK"]))) SELECT_TELEVIZYON.Add(string.Format(" CAST( {0} AS float) as [{1}] ,", myReader["FIELDS"], myReader["SECENEK"]));
                                if (!SELECT_GAZETE.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]))) SELECT_GAZETE.Add(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]));
                                if (!SELECT_DERGI.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]))) SELECT_DERGI.Add(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]));
                                if (!SELECT_SINEMA.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]))) SELECT_SINEMA.Add(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]));
                                if (!SELECT_RADYO.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]))) SELECT_RADYO.Add(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]));
                                if (!SELECT_OUTDOOR.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]))) SELECT_OUTDOOR.Add(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]));
                                if (!SELECT_INTERNET.Contains(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]))) SELECT_INTERNET.Add(string.Format(" CAST( '0' AS float) as [{0}] ,", myReader["SECENEK"]));
                            }
                        }
                    }
                }
                else
                {
                    if (!INSERT_HEADER.Contains(string.Format("[{0}],", BASLIKLAR_NODES))) INSERT_HEADER.Add(string.Format("[{0}],", BASLIKLAR_NODES));
                    if (!TEMP_TABLE_CREATE.Contains(string.Format("[{0}] [nvarchar] (170) NULL ,", BASLIKLAR_NODES))) TEMP_TABLE_CREATE.Add(string.Format("[{0}] [nvarchar] (170) NULL ,", BASLIKLAR_NODES));

                    if (!SELECT_TELEVIZYON.Contains(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES))) SELECT_TELEVIZYON.Add(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES));
                    if (!SELECT_GAZETE.Contains(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES))) SELECT_GAZETE.Add(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES));
                    if (!SELECT_DERGI.Contains(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES))) SELECT_DERGI.Add(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES));
                    if (!SELECT_SINEMA.Contains(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES))) SELECT_SINEMA.Add(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES));
                    if (!SELECT_RADYO.Contains(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES))) SELECT_RADYO.Add(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES));
                    if (!SELECT_OUTDOOR.Contains(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES))) SELECT_OUTDOOR.Add(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES));
                    if (!SELECT_INTERNET.Contains(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES))) SELECT_INTERNET.Add(string.Format(" CAST('' AS nvarchar ) as [{0}] ,", BASLIKLAR_NODES));
                }
            }
        } 


        private void HESAPLAMA_BASLASIN()
        {
            _EDT_CALCULATE cl = new _EDT_CALCULATE();
            using (SqlConnection myCons = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                myCons.Open();
                if (RaporDesigner.TOOGLE_TELEVIZYON.IsOn)
                { 
                    if(RaporDesigner.TrfSablon.gridView_TELEVIZYON.RowCount >0)
                    {
                        string StrField = "";
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            string SqL = string.Format("  SELECT SIRKET_KODU, KULLANICI_KODU,TARIFE_REF, TARIFE_KODU, TARGET FROM dbo.TRF_TELEVIZYON GROUP BY SIRKET_KODU, KULLANICI_KODU, TARIFE_REF,TARIFE_KODU, TARGET HAVING(TARIFE_KODU = N'{0}' and SIRKET_KODU='{1}' and KULLANICI_KODU='{2}' and TARIFE_REF='{3}' )  ", RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Text.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA,_GLOBAL_PARAMETRELER._KULLANICI_MAIL, RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString());
                            SqlCommand myCommand = new SqlCommand(SqL, myConnection)  ;
                            myConnection.Open();
                            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                            while (myReader.Read())
                            {
                                //StrField += " WHEN uptbl.[" + myReader["TARGET"] + "] IS NOT NULL THEN  uptbl.[SÜRE] * uptbl.[" + myReader["TARGET"] + "] * Trf.[BIRIM_FIYAT]  ";
                                // StrField += " WHEN uptbl.[" + myReader["TARGET"] + "] IS NOT NULL THEN  uptbl.[SÜRE] * uptbl.[" + myReader["TARGET"] + "] * Trf.[BIRIM_FIYAT]  ";

                                if (RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_TURU.Text != "MECRA_VE_SPOTTURU_DETYAY") { StrField += " WHEN uptbl.[" + myReader["TARGET"] + "] IS NOT NULL THEN  CASE WHEN  ((uptbl.TARİH between CONVERT(DATETIME, Trf.BASLANGIC_TARIHI, 102) AND  CONVERT(DATETIME, Trf.BITIS_TARIHI, 102))   and  (uptbl.AY  between MONTH(Trf.BASLANGIC_TARIHI) AND MONTH(Trf.BITIS_TARIHI)) and  (uptbl.YIL  between YEAR(Trf.BASLANGIC_TARIHI) AND YEAR(Trf.BITIS_TARIHI))) THEN  uptbl.[SÜRE] * uptbl.[" + myReader["TARGET"] + "] * Trf.[BIRIM_FIYAT]  END  "; }

                                if (RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_TURU.Text == "MECRA_VE_SPOTTURU_DETYAY") { StrField += " WHEN uptbl.[" + myReader["TARGET"] + "] IS NOT NULL THEN  CASE WHEN uptbl.[SPOT TİPİ D.] = Trf.SPOT_TIPI_DETAY and ((uptbl.TARİH between CONVERT(DATETIME, Trf.BASLANGIC_TARIHI, 102) AND  CONVERT(DATETIME, Trf.BITIS_TARIHI, 102))   and  (uptbl.AY  between MONTH(Trf.BASLANGIC_TARIHI) AND MONTH(Trf.BITIS_TARIHI)) and  (uptbl.YIL  between YEAR(Trf.BASLANGIC_TARIHI) AND YEAR(Trf.BITIS_TARIHI))) THEN  uptbl.[SÜRE] * uptbl.[" + myReader["TARGET"] + "] * Trf.[BIRIM_FIYAT]  END  "; }
                            }
                        }

                        if (RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_TURU.Text != "MECRA_VE_SPOTTURU_DETYAY")
                        { 

                            splashScreenManagers.SetWaitFormDescription("TELEVIZYON TARIFESI");
                            string SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                            "  SET     dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[Tarife]=Trf.[BIRIM_FIYAT] ,  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[OPT_PT]=Trf.[OPT_PT] ,  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  " +
                            "  CASE WHEN Trf.[HESAPLANMA_TURU]= 'Cpp' THEN   CASE   " + StrField + " END " +
                            "  WHEN Trf.[HESAPLANMA_TURU]= 'Süre' THEN   CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN  CASE WHEN ((uptbl.TARİH between CONVERT(DATETIME, Trf.BASLANGIC_TARIHI , 102) AND  CONVERT(DATETIME, Trf.BITIS_TARIHI , 102))  and  (uptbl.AY  between MONTH(Trf.BASLANGIC_TARIHI) AND MONTH(Trf.BITIS_TARIHI)) and  (uptbl.YIL  between YEAR(Trf.BASLANGIC_TARIHI) AND YEAR(Trf.BITIS_TARIHI)))   THEN    uptbl.[SÜRE] * Trf.[BIRIM_FIYAT]  END   END " +
                            "  WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN   CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN  CASE WHEN ((uptbl.TARİH between CONVERT(DATETIME, Trf.BASLANGIC_TARIHI , 102) AND  CONVERT(DATETIME, Trf.BITIS_TARIHI , 102)) and  (uptbl.AY  between MONTH(Trf.BASLANGIC_TARIHI) AND MONTH(Trf.BITIS_TARIHI)) and  (uptbl.YIL  between YEAR(Trf.BASLANGIC_TARIHI) AND YEAR(Trf.BITIS_TARIHI)))  THEN    uptbl.[SÜRE] * uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT]  END   END   " +
                            "  END  " +
                            " FROM dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl RIGHT OUTER JOIN   dbo.TRF_TELEVIZYON AS Trf ON   (uptbl.TARİH between CONVERT(DATETIME, Trf.BASLANGIC_TARIHI , 102) AND  CONVERT(DATETIME, Trf.BITIS_TARIHI , 102)) AND  uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI  AND uptbl.[ANA YAYIN] = Trf.MECRA_KODU " +
                            " where  (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and   (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Text.ToString() + "') and  (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString() + "') and uptbl.[YAYIN SINIFI]= 'TELEVIZYON'  ";
                            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                myConnection.Open();
                                SqlCommand mCmd = new SqlCommand() { CommandText = SQL, Connection = myConnection, CommandTimeout = 0 };
                                mCmd.ExecuteNonQuery();
                            }
                        } 
                        if (RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_TURU.Text == "MECRA_VE_SPOTTURU_DETYAY")
                        { 
                            splashScreenManagers.SetWaitFormDescription("TELEVIZYON TARIFESI");
                            string SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                            "  SET     dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[Tarife]=Trf.[BIRIM_FIYAT] ,  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[OPT_PT]=Trf.[OPT_PT] ,  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  " +
                            "  CASE WHEN Trf.[HESAPLANMA_TURU]= 'Cpp' THEN   CASE   " + StrField + " END " +
                            "  WHEN Trf.[HESAPLANMA_TURU]= 'Süre' THEN   CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN  CASE WHEN uptbl.[SPOT TİPİ D.] = Trf.SPOT_TIPI_DETAY  AND  ((uptbl.TARİH between CONVERT(DATETIME, Trf.BASLANGIC_TARIHI , 102) AND  CONVERT(DATETIME, Trf.BITIS_TARIHI , 102))  and  (uptbl.AY  between MONTH(Trf.BASLANGIC_TARIHI) AND MONTH(Trf.BITIS_TARIHI)) and  (uptbl.YIL  between YEAR(Trf.BASLANGIC_TARIHI) AND YEAR(Trf.BITIS_TARIHI)))   THEN    uptbl.[SÜRE] * Trf.[BIRIM_FIYAT]  END   END " +
                            "  WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN   CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN  CASE WHEN uptbl.[SPOT TİPİ D.] = Trf.SPOT_TIPI_DETAY  AND ((uptbl.TARİH between CONVERT(DATETIME, Trf.BASLANGIC_TARIHI , 102) AND  CONVERT(DATETIME, Trf.BITIS_TARIHI , 102)) and  (uptbl.AY  between MONTH(Trf.BASLANGIC_TARIHI) AND MONTH(Trf.BITIS_TARIHI)) and  (uptbl.YIL  between YEAR(Trf.BASLANGIC_TARIHI) AND YEAR(Trf.BITIS_TARIHI)))  THEN    uptbl.[SÜRE] * uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT]  END   END   " +
                            "  END  " +
                            " FROM dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl RIGHT OUTER JOIN   dbo.TRF_TELEVIZYON AS Trf ON   (uptbl.TARİH between CONVERT(DATETIME, Trf.BASLANGIC_TARIHI , 102) AND  CONVERT(DATETIME, Trf.BITIS_TARIHI , 102)) AND  uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI  AND uptbl.[ANA YAYIN] = Trf.MECRA_KODU " +
                            " where  (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and   (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Text.ToString() + "') and  (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString() + "') and uptbl.[YAYIN SINIFI]= 'TELEVIZYON'  ";
                            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                myConnection.Open();
                                SqlCommand mCmd = new SqlCommand() { CommandText = SQL, Connection = myConnection, CommandTimeout = 0 };
                                mCmd.ExecuteNonQuery();
                            }
                        }


                    }
                    if (RaporDesigner.TrfSablon.gridView_NONE_MEASURED_TELEVIZYON.RowCount > 0)
                    {
                        splashScreenManagers.SetWaitFormDescription("NONE TV GRP TARIFESI");

                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            string SqL = string.Format("  SELECT SIRKET_KODU, KULLANICI_KODU, TARIFE_KODU, TARGET FROM dbo.TRF_TELEVIZYON_NONE_MEASURED GROUP BY SIRKET_KODU, KULLANICI_KODU, TARIFE_KODU, TARGET HAVING(TARIFE_KODU = N'{0}')  ", RaporDesigner.TrfSablon.LBL_NONETV_TARIFE_KODU.Text.ToString());
                            SqlCommand myCommand = new SqlCommand(SqL, myConnection);
                            myConnection.Open();
                            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                            while (myReader.Read())
                            {
                                string SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                                "  SET dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[" + myReader["TARGET"] + "] =   Trf.[GRP]  , dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[" + myReader["TARGET"] + " 30sn]  = ((CAST(Trf.[GRP] AS float)  /30) * uptbl.SÜRE )  ," +
                                "  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[OPT_PT] = Trf.[OPT_PT] "+

                                //"  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_NAME + "].[OPT_PT] = Trf.[OPT_PT], dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_NAME + "].[NET TUTAR] =  " +
                                //"  CASE WHEN Trf.[HESAPLANMA_TURU]= 'Cpp' THEN   CASE   " + StrField + " END " +
                                //"  WHEN Trf.[HESAPLANMA_TURU]= 'Süre' THEN   CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[SÜRE] * Trf.[BIRIM_FIYAT] END " +
                                //"  WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN   CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[SÜRE] * uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT] END   " +
                                //"  END  " + 

                                    "  FROM dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl INNER JOIN    dbo.TRF_TELEVIZYON_NONE_MEASURED Trf ON uptbl.[ANA YAYIN] = Trf.MECRA_KODU AND  " +
                                    "  uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI AND uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND   uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI " +
                                    "  where     (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and   (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_NONETV_TARIFE_KODU.Text.ToString() + "') and  (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_NONETV_TARIFE_KODU.Tag.ToString() + "') and  uptbl.[YAYIN SINIFI]= 'TELEVIZYON'  ";
                                using (SqlConnection connec = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                                {
                                    connec.Open();
                                    SqlCommand mCmd = new SqlCommand() { CommandText = SQL, Connection = connec, CommandTimeout = 0 };
                                    mCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
                if (RaporDesigner.TOOGLE_GAZETE.IsOn)
                { 
                    if (RaporDesigner.TrfSablon.gridView_GAZETE.RowCount > 0)
                    {
                        splashScreenManagers.SetWaitFormDescription("GAZETE TARIFESI");
                        string StrField = "";
                        string SQL = "";
                        string HESAPLAMA_TURU = " uptbl.[MEDYA] = Trf.MECRA_KODU  ";

                        if (RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_TURU.Text == "MECRA_SAYFA") { StrField = " AND  uptbl.[SAYFA GRUBU] = Trf.SAYFA_GRUBU "; }; 
                        if (RaporDesigner.TrfSablon.LBL_HESAPLAMA_TURU_GAZETE.Text == "MEDYA") { HESAPLAMA_TURU = " uptbl.[MEDYA] = Trf.MECRA_KODU "; };
                        if (RaporDesigner.TrfSablon.LBL_HESAPLAMA_TURU_GAZETE.Text == "ANA_YAYIN") { HESAPLAMA_TURU = " uptbl.[ANA YAYIN] = Trf.MECRA_KODU "; };

                        // NET KUTU TABLOID STCM
                        if (RaporDesigner.TrfSablon.LBL_TARIFE_GUNLUK_STANDART.Text == "STANDART_TARIFE")
                        {
                            SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                           "  SET dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  " +
                           "  CASE WHEN Trf.[HESAPLANMA_TURU]= 'Stcm' THEN CASE WHEN uptbl.SANTIM=0 THEN uptbl.[NET KUTU TABLOID STCM]  * Trf.[BIRIM_FIYAT]  else uptbl.[SANTIM] * Trf.[BIRIM_FIYAT] END  " +
                           "  WHEN Trf.[HESAPLANMA_TURU]= 'Sayfa' THEN  uptbl.[SAYFA] * Trf.[BIRIM_FIYAT]  WHEN Trf.[HESAPLANMA_TURU]= 'Kutu' THEN   uptbl.[EBAT-KUTU] * Trf.[BIRIM_FIYAT]  WHEN Trf.[HESAPLANMA_TURU]= 'KutuStcm' THEN  uptbl.[NET KUTU TABLOID STCM] * Trf.[BIRIM_FIYAT]  WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT]  END  " +

                          // "  WHEN Trf.[HESAPLANMA_TURU]= 'Sayfa' THEN  uptbl.[SAYFA] * Trf.[BIRIM_FIYAT]  WHEN Trf.[HESAPLANMA_TURU]= 'Kutu' THEN   uptbl.[NET KUTU TABLOID STCM] * Trf.[BIRIM_FIYAT] WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT]  END  " +

                           "  FROM  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl INNER JOIN  dbo.TRF_GAZETE Trf ON " + HESAPLAMA_TURU + " AND  " +
                           "  uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI  " + StrField +
                           "  where   (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Text.ToString() + "') and (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag.ToString() + "') and  uptbl.[YAYIN SINIFI]= 'GAZETE'  ";                            
                        }
                        if (RaporDesigner.TrfSablon.LBL_TARIFE_GUNLUK_STANDART.Text == "GUNLUK_TARIFE")
                        {  
                          SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                                "  SET  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[Tarife]=Trf.[BIRIM_FIYAT] , dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  " +
                                "  CASE WHEN Trf.[HESAPLANMA_TURU]= 'Stcm' THEN CASE WHEN uptbl.SANTIM= 0 THEN uptbl.[SAYFA] * Trf.[BIRIM_FIYAT]  else uptbl.[SANTIM] * Trf.[BIRIM_FIYAT] END  " +
                                "  WHEN Trf.[HESAPLANMA_TURU]= 'Sayfa' THEN  uptbl.[SAYFA] * Trf.[BIRIM_FIYAT]  WHEN Trf.[HESAPLANMA_TURU]= 'Kutu' THEN   uptbl.[EBAT-KUTU] * Trf.[BIRIM_FIYAT]  WHEN Trf.[HESAPLANMA_TURU]= 'KutuStcm' THEN  uptbl.[NET KUTU TABLOID STCM] * Trf.[BIRIM_FIYAT]  WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT]  END  " +                                                
                                "  FROM  dbo.[__TEMP_"+_GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl INNER JOIN  dbo.TRF_GAZETE Trf ON "+ HESAPLAMA_TURU + " AND  " +
                                "  uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI " + StrField +
                                "  where   (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Text.ToString() + "') and (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag.ToString() + "') and  uptbl.[YAYIN SINIFI]= 'GAZETE'  and datename(dw,uptbl.[TARİH])<>'Saturday' and datename(dw,uptbl.[TARİH])<>'Sunday' ";
                            
                           SQL +=";  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                                "  SET  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[Tarife]=Trf.[BIRIM_FIYAT] ,dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  " +
                                "  CASE WHEN Trf.[HESAPLANMA_TURU]= 'Stcm' THEN CASE WHEN uptbl.SANTIM= 0 THEN uptbl.[SAYFA] * Trf.[CUMARTESI]  else uptbl.[SANTIM] * Trf.[CUMARTESI] END  " +
                                "  WHEN Trf.[HESAPLANMA_TURU]= 'Sayfa' THEN  uptbl.[SAYFA] * Trf.[CUMARTESI]  WHEN Trf.[HESAPLANMA_TURU]= 'Kutu' THEN   uptbl.[EBAT-KUTU] * Trf.[CUMARTESI] WHEN   Trf.[HESAPLANMA_TURU]= 'KutuStcm' THEN  uptbl.[NET KUTU TABLOID STCM] * Trf.[CUMARTESI] WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN uptbl.[TUTAR TL] * Trf.[CUMARTESI]  END  " +
                                "  FROM  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl INNER JOIN  dbo.TRF_GAZETE Trf ON " + HESAPLAMA_TURU + " AND  " +
                                "  uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI  " + StrField +
                                "  where   (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Text.ToString() + "') and (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag.ToString() + "') and  uptbl.[YAYIN SINIFI]= 'GAZETE' and datename(dw,uptbl.[TARİH])='Saturday' ";

                            SQL += ";  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                                "  SET dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[Tarife]=Trf.[BIRIM_FIYAT] , dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  " +
                                "  CASE WHEN Trf.[HESAPLANMA_TURU]= 'Stcm' THEN CASE WHEN uptbl.SANTIM= 0 THEN uptbl.[SAYFA] * Trf.[PAZAR]  else uptbl.[SANTIM] * Trf.[PAZAR] END  " +
                                "  WHEN Trf.[HESAPLANMA_TURU]= 'Sayfa' THEN  uptbl.[SAYFA] * Trf.[PAZAR]  WHEN Trf.[HESAPLANMA_TURU]= 'Kutu' THEN   uptbl.[EBAT-KUTU] * Trf.[PAZAR] WHEN   Trf.[HESAPLANMA_TURU]= 'KutuStcm' THEN  uptbl.[NET KUTU TABLOID STCM] * Trf.[PAZAR] WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN uptbl.[TUTAR TL] * Trf.[PAZAR]  END  " +
                                "  FROM  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl INNER JOIN  dbo.TRF_GAZETE Trf ON " + HESAPLAMA_TURU + " AND  " +
                                "  uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI   " + StrField +
                                "  where   (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Text.ToString() + "') and (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_GAZETE_TARIFE_KODU.Tag.ToString() + "') and  uptbl.[YAYIN SINIFI]= 'GAZETE'  and datename(dw,uptbl.[TARİH])='Sunday'  "; 
                        }
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            myConnection.Open();
                            SqlCommand mCmd = new SqlCommand() { CommandText = SQL, Connection = myConnection, CommandTimeout = 0 };
                            mCmd.ExecuteNonQuery();
                       }
                    }
                }
                if (RaporDesigner.TOOGLE_DERGI.IsOn)
                {
                    if (RaporDesigner.TrfSablon.gridView_DERGI.RowCount > 0)
                    {
                        string HESAPLAMA_TURU = " uptbl.[ANA YAYIN] = Trf.MECRA_KODU ";

                        if (RaporDesigner.TrfSablon.LBL_HESAPLAMA_TURU_DERGI.Text == "MEDYA") { HESAPLAMA_TURU = " uptbl.[MEDYA] = Trf.MECRA_KODU "; };
                        if (RaporDesigner.TrfSablon.LBL_HESAPLAMA_TURU_DERGI.Text == "ANA_YAYIN") { HESAPLAMA_TURU = " uptbl.[ANA YAYIN] = Trf.MECRA_KODU "; };

                        splashScreenManagers.SetWaitFormDescription("DERGI TARIFESI");
                        string SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                        "  SET  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[Tarife]=Trf.[BIRIM_FIYAT] ,dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  " +
                        "  CASE WHEN Trf.[HESAPLANMA_TURU]= 'Stcm' THEN  CASE WHEN uptbl.SANTIM= 0 THEN uptbl.[SAYFA] * Trf.[BIRIM_FIYAT]  else uptbl.[SANTIM] * Trf.[BIRIM_FIYAT] END  " +
                        "  WHEN Trf.[HESAPLANMA_TURU]= 'Sayfa' THEN   CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[SAYFA] * Trf.[BIRIM_FIYAT] END  WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN  CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT] END  END  " +
                        "  FROM  dbo.[__TEMP_"+_GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl INNER JOIN    dbo.TRF_DERGI Trf ON "+ HESAPLAMA_TURU + " AND  " +
                        "  uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI  " +
                        "  where  (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and    (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and    (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_DERGI_TARIFE_KODU.Text.ToString() + "')  and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_DERGI_TARIFE_KODU.Tag.ToString() + "') and  uptbl.[YAYIN SINIFI]= 'DERGI'  ";
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            myConnection.Open();
                            SqlCommand mCmd = new SqlCommand() { CommandText = SQL, Connection = myConnection, CommandTimeout = 0 };
                            mCmd.ExecuteNonQuery();
                        }
                    }  
                }
                if (RaporDesigner.TOOGLE_OUTDOOR.IsOn)
                {
                    if (RaporDesigner.TrfSablon.gridView_OUTDOOR.RowCount > 0)
                    {
                        //    uptbl.ADET = 0 THEN     uptbl.[TUTAR TL] = Trf.[BIRIM_FIYAT]  * uptbl.ADET= (uptbl.[FREKANS] / Trf.[GUN])  else uptbl.[SANTIM] * Trf.[BIRIM_FIYAT]
                        splashScreenManagers.SetWaitFormDescription("OUTDOOR TARIFESI");
                        string SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                      "  SET dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[Tarife]=Trf.[BIRIM_FIYAT] ,dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  " +
                      "  CASE  WHEN Trf.[HESAPLANMA_TURU]= 'Frekans' THEN    CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[FREKANS] * Trf.[BIRIM_FIYAT] END  " +
                      "        WHEN Trf.[HESAPLANMA_TURU]= 'Adet' THEN  CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[ADET] * Trf.[BIRIM_FIYAT] END  " +
                      "        WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN  CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT] END  END " +
                      "  FROM  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl INNER JOIN    dbo.TRF_OUTDOOR Trf ON uptbl.[MEDYA] = Trf.MECRA_KODU AND  " +
                      "  uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI AND uptbl.[İLİ] = Trf.ILI AND uptbl.[ÜNİTE] = Trf.UNITE  " +
                      "  where  (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and    (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and  (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Text.ToString() + "') and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_OUTDOOR_TARIFE_KODU.Tag.ToString() + "') and uptbl.[YAYIN SINIFI]= 'OUTDOOR'  ";
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            myConnection.Open();
                            SqlCommand mCmd = new SqlCommand() { CommandText = SQL, Connection = myConnection, CommandTimeout = 0 };
                            mCmd.ExecuteNonQuery();
                        }
                    } 
                }
                if (RaporDesigner.TOOGLE_SINEMA.IsOn)
                {
                    if (RaporDesigner.TrfSablon.gridView_SINEMA.RowCount > 0)
                    {
                        splashScreenManagers.SetWaitFormDescription("SINEMA TARIFESI");
                        string SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                          " SET dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[Tarife]=Trf.[BIRIM_FIYAT] , dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  " +
                          " CASE  WHEN Trf.[HESAPLANMA_TURU]= 'Süre' THEN    CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN  (uptbl.[SÜRE]/35) * Trf.[BIRIM_FIYAT] END  " +
                          "       WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN  CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT] END  END " +
                          " FROM  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl INNER JOIN    dbo.TRF_SINEMA Trf ON uptbl.[MEDYA] = Trf.MECRA_KODU AND  " +
                          " uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI  " +
                          " where  (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and    (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and  (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_SINEMA_TARIFE_KODU.Text.ToString() + "')  and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_SINEMA_TARIFE_KODU.Tag.ToString() + "') and uptbl.[YAYIN SINIFI]= 'SINEMA'  ";
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                myConnection.Open();
                                 SqlCommand mCmd = new SqlCommand() { CommandText = SQL, Connection = myConnection, CommandTimeout = 0 };
                                mCmd.ExecuteNonQuery();
                            }
                    } 
                }
                if (RaporDesigner.TOOGLE_RADYO.IsOn)
                {
                    if (RaporDesigner.TrfSablon.gridView_RADYO.RowCount > 0)
                    {
                        splashScreenManagers.SetWaitFormDescription("RADYO TARIFESI");
                        string SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                          "  SET dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[Tarife]=Trf.[BIRIM_FIYAT] , dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  " +
                          "  CASE  WHEN Trf.[HESAPLANMA_TURU]= 'Süre' THEN    CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[SÜRE] * Trf.[BIRIM_FIYAT] END  " +
                          "        WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN  CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT] END  END " + 
                          "  FROM  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl INNER JOIN    dbo.TRF_RADYO Trf ON uptbl.[ANA YAYIN] = Trf.MECRA_KODU AND  " +
                          "  uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI  " +
                          "  where  (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and    (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and  (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_RADYO_TARIFE_KODU.Text.ToString() + "') and    (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_RADYO_TARIFE_KODU.Tag.ToString() + "') and uptbl.[YAYIN SINIFI]= 'RADYO'  ";
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            myConnection.Open();
                            SqlCommand mCmd = new SqlCommand() { CommandText = SQL, Connection = myConnection, CommandTimeout = 0 };
                            mCmd.ExecuteNonQuery();
                        }
                    }
                }
                if (RaporDesigner.TOGGLE_TARIFE.IsOn)
                {
                    if (RaporDesigner.TrfSablon.gridView_TARIFE_LISTESI.RowCount > 0)
                    {
                        splashScreenManagers.SetWaitFormDescription("ORAN ATAMASI");
                        string SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                          "  SET dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] = (uptbl.[TUTAR TL] * Trf.[ORAN_ARTI_EKSI] /100) + uptbl.[TUTAR TL] " +
                          "  FROM  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl INNER JOIN    dbo.TRF_ORANLAR Trf ON uptbl.[YAYIN SINIFI] = Trf.YAYIN_SINIFI AND  " +
                          "  uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI  " +
                          "  where  (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and    (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and  (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_ORANLAR_TARIFE_KODU.Text.ToString() + "') and    (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_ORANLAR_TARIFE_KODU.Tag.ToString() + "')  ";
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            myConnection.Open();
                            SqlCommand mCmd = new SqlCommand() { CommandText = SQL, Connection = myConnection, CommandTimeout = 0 };
                            mCmd.ExecuteNonQuery();
                        }
                    }
                }

                if (RaporDesigner.TOOGLE_TELEVIZYON.IsOn)
                {
                    if (RaporDesigner.TrfSablon.gridView_TARIFE_PROGRAM.RowCount > 0)
                    { 
                        string StrField = "";
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            string SqL = string.Format("  SELECT SIRKET_KODU, KULLANICI_KODU,TARIFE_REF, TARIFE_KODU, TARGET FROM dbo.TRF_TELEVIZYON GROUP BY SIRKET_KODU, KULLANICI_KODU, TARIFE_REF,TARIFE_KODU, TARGET HAVING(TARIFE_KODU = N'{0}' and SIRKET_KODU='{1}' and KULLANICI_KODU='{2}' and TARIFE_REF='{3}' )  ", RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Text.ToString(), _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL, RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_KODU.Tag.ToString());
                            SqlCommand myCommand = new SqlCommand(SqL, myConnection);
                            myConnection.Open();
                            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                            while (myReader.Read())
                            {
                                StrField += " WHEN uptbl.[" + myReader["TARGET"] + "] IS NOT NULL THEN  uptbl.[SÜRE] * uptbl.[" + myReader["TARGET"] + "] * Trf.[BIRIM_FIYAT]  ";
                            }
                        }
                        splashScreenManagers.SetWaitFormDescription("PROGRAM TARIFESI");
                        string SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                        "  SET    dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[Tarife]=Trf.[BIRIM_FIYAT] ,  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[OPT_PT]=Trf.[OPT_PT] ,  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  " +
                        "  CASE WHEN Trf.[HESAPLANMA_TURU]= 'Cpp' THEN   CASE   " + StrField + " END " +
                        "  WHEN Trf.[HESAPLANMA_TURU]= 'Süre' THEN   CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[SÜRE] * Trf.[BIRIM_FIYAT] END " +
                        "  WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN   CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[SÜRE] * uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT] END   " +
                        "  END  " +
                        "  FROM dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] uptbl INNER JOIN    dbo.TRF_PROGRAM_TARIFESI Trf ON uptbl.[ANA YAYIN] = Trf.MECRA_KODU AND  uptbl.[PROGRAM] = Trf.PROGRAM AND uptbl.[PG.ÖZEL] = Trf.PG_OZEL AND uptbl.[TİPOLOJİ] = Trf.TIPOLOJI and " +
                        " ( uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI) AND (uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND   uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI) " +
                        "  where  (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and   (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_PROGRAM_TARIFE_KODU.Text.ToString() + "') and  (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_PROGRAM_TARIFE_KODU.Tag.ToString() + "') and uptbl.[YAYIN SINIFI]= 'TELEVIZYON'  ";
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            myConnection.Open();
                            SqlCommand mCmd = new SqlCommand() { CommandText = SQL, Connection = myConnection, CommandTimeout = 0 };
                            mCmd.ExecuteNonQuery();
                        } 

                        //splashScreenManagers.SetWaitFormDescription("PROGRAM TARIFESI");
                        //string SQL = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_NAME + "] " +
                        //  "  SET dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_NAME + "].[NET TUTAR] =  " +
                        //  "  CASE  WHEN Trf.[HESAPLANMA_TURU]= 'Süre' THEN    CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[SÜRE] * Trf.[BIRIM_FIYAT] END  " +
                        //  "        WHEN Trf.[HESAPLANMA_TURU]= 'Ratecard' THEN  CASE WHEN Trf.[BIRIM_FIYAT] IS NOT NULL THEN uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT] END  END " +
                        //  "  FROM  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_NAME + "] uptbl INNER JOIN  dbo.TRF_PROGRAM_TARIFESI Trf ON uptbl.[ANA YAYIN] = Trf.MECRA_KODU AND uptbl.[PROGRAM] = Trf.PROGRAM AND uptbl.[PG.ÖZEL] = Trf.PG_OZEL AND uptbl.[TİPOLOJİ] = Trf.TIPOLOJI and uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI   AND (uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND   uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI)   " +
                        //  "  where  (Trf.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') and    (Trf.KULLANICI_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_NAME + "') and  (Trf.TARIFE_KODU = N'" + RaporDesigner.TrfSablon.LBL_PROGRAM_TARIFE_KODU.Text.ToString() + "') and   (Trf.TARIFE_REF= N'" + RaporDesigner.TrfSablon.LBL_PROGRAM_TARIFE_KODU.Tag.ToString() + "') and  uptbl.[YAYIN SINIFI]= 'TELEVIZYON'  ";
                        //using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        //{
                        //    myConnection.Open();
                        //    SqlCommand mCmd = new SqlCommand() { CommandText = SQL, Connection = myConnection, CommandTimeout = 0 };
                        //    mCmd.ExecuteNonQuery();
                        //}
                    }
                }


                if (RaporDesigner.TOOGLE_INTERNET.IsOn)
                {
                    //for (int iX = 0; iX <= RaporDesigner.TrfSablon.gridView_INTERNET.RowCount - 1; iX++)
                    //{
                    //    if (myCons.State.ToString() == "Closed") myCons.Open();
                    //    splashScreenManagers.SetWaitFormDescription(string.Format("INTERNET {0}/{1}", RaporDesigner.TrfSablon.gridView_INTERNET.RowCount, iX));
                    //    DataRow dw = RaporDesigner.TrfSablon.gridView_INTERNET.GetDataRow(iX);
                    //    if (dw["BASLANGIC_TARIHI"] != DBNull.Value || dw["BASLANGIC_TARIHI"] != DBNull.Value) cl.INTERNET_HESAPLA(DateTime.Parse(dw["BASLANGIC_TARIHI"].ToString()), DateTime.Parse(dw["BITIS_TARIHI"].ToString()), dw, myCons);
                    //}
                } 
            } 
        } 

        static ArrayList GetArrayList_MASTER_ALL_EXCLUDE(string MECRA_TURU)
        {
            ArrayList SQLJOIN = new ArrayList();
            for (int i = 0; i < RaporDesigner.MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.Count; i++)
            {
                string StrField = null;
                System.Windows.Forms.StatusStrip rt = (System.Windows.Forms.StatusStrip)RaporDesigner.MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages[i].Controls[3];
                if (rt.Items[3].ToString().IndexOf(MECRA_TURU) != -1)
                {
                    var rtb = RaporDesigner.MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages[i].Controls[0];
                    DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
                    DataView RW = (DataView)grd_.DataSource;
                    if (RW.Count != 0)
                    {
                        DevExpress.XtraGrid.Views.Grid.GridView grdView_ = (DevExpress.XtraGrid.Views.Grid.GridView)grd_.Views[0];
                        string DAHIL_HARIC_DURUMU = "";
                        for (int ix = 0; ix <= grdView_.Columns.Count - 1; ix++)
                        {
                            //  if (RW.Table.Columns[ix].Caption == "DAHIL_HARIC")
                            if (grdView_.Columns[ix].Caption == "DAHIL_HARIC")
                            {
                                DAHIL_HARIC_DURUMU = "VAR";
                                break;
                            }
                        }
                        if (DAHIL_HARIC_DURUMU == "VAR")
                        {
                            for (int ix = 0; ix <= grdView_.Columns.Count - 1; ix++)
                            {
                                if (grdView_.Columns[ix].Caption != "ID" && grdView_.Columns[ix].Caption != "NEW" && grdView_.Columns[ix].Caption != "GUID")
                                {
                                    using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                                    {
                                        SqlCommand myCommand = new SqlCommand(string.Format(" SELECT * FROM   dbo.ADM_SECENEKLER where SECENEK ='{0}' ", grdView_.Columns[ix].Caption), myConnection);
                                        myConnection.Open();
                                        SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                                        if (myReader.HasRows)
                                        {
                                            while (myReader.Read())
                                            {
                                                if (myReader[MECRA_TURU].ToString().ToUpper().IndexOf("CAST") == -1)
                                                {
                                                    StrField = StrField + " DT.[" + RW.Table.Columns[ix].Caption + "] = dbo.[__MAS_EDT_" + rtb.Tag + "_" + rtb.Name + "].[" + RW.Table.Columns[ix].Caption + "] AND ";
                                                }
                                            }
                                        }
                                    }
                                }
                            }
                            if (StrField != null)
                            {
                                SQLJOIN.Add(string.Format(" INNER JOIN dbo.[__MAS_EDT_{0}_{1}] ON {2}", rtb.Tag, rtb.Name, StrField));
                                SQLJOIN.Add(" dbo.[__MAS_EDT_" + rtb.Tag + "_" + rtb.Name + "].[DAHIL_HARIC]='Dahil'");
                            }
                        }
                    }
                }
            } 
            return SQLJOIN;
            
        }  
         
        private void MASTER_ALL_BASLASIN()
        {
            using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                con.Open();
                for (int i = 0; i < RaporDesigner.MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.Count; i++)
                {
                    string StrField = "";
                    string StrFieldUpdate = "";
                    var rtb = RaporDesigner.MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages[i].Controls[0];
                    DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
                    DataView RW = (DataView)grd_.DataSource;
                    if (RW.Count != 0)
                    {
                        splashScreenManagers.SetWaitFormCaption(string.Format("{0}-{1} Master Management", RaporDesigner.MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.Count, i));
                        splashScreenManagers.SetWaitFormDescription(rtb.Name.ToString() + " Kodlaması Yapılıyor");

                        DevExpress.XtraGrid.Views.Grid.GridView grdView_ = (DevExpress.XtraGrid.Views.Grid.GridView)grd_.Views[0];
                        for (int ix = 0; ix <= grdView_.Columns.Count - 1; ix++)
                        {
                            if (grdView_.Columns[ix].Caption != "ID" && grdView_.Columns[ix].Caption != "DAHIL_HARIC" && grdView_.Columns[ix].Caption != "NEW" && grdView_.Columns[ix].Caption != "GUID" && grdView_.Columns[ix].Caption != "ALT")
                            {
                                using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                                {                                   
                                    SqlCommand myCommand = new SqlCommand(string.Format(" SELECT   * FROM   dbo.ADM_SECENEKLER where SECENEK ='{0}'", grdView_.Columns[ix].Caption), myConnection) ;
                                    myConnection.Open();
                                    SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                                    if (myReader.HasRows)
                                    {
                                        while (myReader.Read())
                                        {
                                            StrField = StrField + " dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[" + myReader["SECENEK"] + "] = dbo.[__MAS_EDT_" + rtb.Tag + "_" + rtb.Name + "].[" + RW.Table.Columns[ix].Caption + "] AND ";
                                        }
                                    }
                                    else
                                    {
                                        StrFieldUpdate = StrFieldUpdate + " dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[" + RW.Table.Columns[ix].Caption + "] = dbo.[__MAS_EDT_" + rtb.Tag + "_" + rtb.Name + "].[" + RW.Table.Columns[ix].Caption + "] ,";
                                    }
                                }
                            }
                        }
                        if (StrField.Length > 0) StrField = StrField.Substring(0, StrField.Length - 4);
                        if (StrFieldUpdate.Length > 0) StrFieldUpdate = StrFieldUpdate.Substring(0, StrFieldUpdate.Length - 1);

                        if (StrFieldUpdate.Length > 0)
                        {
                            if (con.State.ToString() == "Closed") con.Open();
                            SqlCommand mCmd = new SqlCommand();
                            mCmd.CommandText = string.Format(" UPDATE  dbo.[__TEMP_{0}]  SET {1} from  dbo.[__MAS_EDT_{2}_{3}] INNER JOIN dbo.[__TEMP_{4}] ON {5}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, StrFieldUpdate, rtb.Tag, rtb.Name, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, StrField);
                            // FROM         dbo.__MAS_EDT_1081_WSXXXXX INNER JOIN       dbo.[__TEMP_hasan.yogurtcu] ON dbo.[__TEMP_hasan.yogurtcu].[ANA SEKTÖR] <> dbo.__MAS_EDT_1081_WSXXXXX.ANA_SEKTOR                        
                            mCmd.Connection = con;
                            mCmd.CommandTimeout = 0;
                            mCmd.ExecuteNonQuery();
                        }
                    }
                }
            }
        }
        private void KEYWORD_BASLASIN()
        {
            using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                con.Open();
                for (int i = 0; i < RaporDesigner.WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages.Count; i++)
                {
                    string StrField = null;
                    //string StrFieldUpdate = null;
                    var rtb = RaporDesigner.WordSablon.xtraTabControl_WORD_MNG_DETAY.TabPages[i].Controls[0];
                    DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
                    DataView RW = (DataView)grd_.DataSource;
                    if (RW.Count != 0)
                    {
                        DevExpress.XtraGrid.Views.Grid.GridView grdView_ = (DevExpress.XtraGrid.Views.Grid.GridView)grd_.Views[0];
                        for (int ix = 0; ix <= RW.Count ; ix++)
                        {
                            splashScreenManagers.SetWaitFormDescription("WORD CONTROL " + RW.Count + "/" + ix);
                            DataRow row = RW[ix].Row;
                            string[] Ones = row["ALANLAR"].ToString().Split(',');
                            if (Ones[0].ToString() != string.Empty)
                            {
                                for (int c = 0; c < Ones.Length; c++)
                                {
                                    StrField = string.Empty;
                                    string[] Word = row["KEYWORDS"].ToString().Split(';');
                                    if (Word[0].ToString() != string.Empty)
                                    {
                                        for (int cx = 0; cx < Word.Length; cx++)
                                        {
                                            if (Word[cx].ToString() != string.Empty) StrField += string.Format("[{0}] LIKE '%{1}%' OR ", Ones[c].Trim(), Word[cx]);
                                        }
                                    }
                                    SqlCommand myCmd = new SqlCommand();
                                    //  myCmd.Parameters.AddWithValue("@" + row["ALAN"], row["DEGER"]);                       
                                    //foreach (SqlParameter parameter in myCmd.Parameters)
                                    //{
                                    //    if (parameter.Value == null)
                                    //    {
                                    //        parameter.Value = DBNull.Value;
                                    //    }
                                    //}
                                    if (StrField.Length > 0) StrField = StrField.Substring(0, StrField.Length - 4);
                                    myCmd.CommandText = string.Format("UPDATE  dbo.[__TEMP_{0}]  SET  [{1}]='{2}'  where {3}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, row["ALAN"], row["DEGER"], StrField);
                                    myCmd.Connection = con;
                                    myCmd.ExecuteNonQuery();
                                }
                            }
                        }
                    }
                }
            }
        }
        private void RAPOR_OLUSTUR()
        {
           BR_RAPOR_SAVE_PATH.Caption = "C:\\temp\\CoreRapor" + DateTime.Now.ToString().Replace(".", "_").Replace(":", "_") + ".xlsx";

            string SELECT_FIELDS = "", INSERT_FIELDS = "", OZET_FIELDS = "", GROUP_BY_FIELDS = "", SUM_FIELDS = "";   
            for (int iX = 0; iX <= RaporDesigner.gridView_BASLIKLAR.RowCount; iX++)
            {
                DataRow DR = RaporDesigner.gridView_BASLIKLAR.GetDataRow(iX);
                if (DR != null)
                {
                    if (DR["TEXT"].ToString() != "BAŞLANGIÇ" && DR["TEXT"].ToString() != "TARİH")
                    {
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            string SQL = " SELECT   * FROM   dbo.ADM_SECENEKLER where SECENEK='"+ DR["TEXT"]  + "'";
                            SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                            myConnection.Open();
                            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                            if (myReader.HasRows)
                            {
                                while (myReader.Read())
                                {
                                    if (_GLOBAL_PARAMETRELER._DIL == "EN")
                                    {
                                        INSERT_FIELDS += string.Format("[{0}] ,", myReader["SECENEK_EN"]);
                                        SELECT_FIELDS += string.Format("[{0}] ,", myReader["SECENEK_EN"]);
                                        GROUP_BY_FIELDS += string.Format("[{0}] ,", myReader["SECENEK_EN"]);
                                    }
                                    else
                                    {
                                        INSERT_FIELDS += string.Format("[{0}] ,", DR["TEXT"]);
                                        SELECT_FIELDS += string.Format("[{0}] ,", DR["TEXT"]);
                                        GROUP_BY_FIELDS += string.Format("[{0}] ,", DR["TEXT"]);
                                    }
                                }
                            }
                            else
                            {
                                INSERT_FIELDS += string.Format("[{0}] ,", DR["TEXT"]);
                                SELECT_FIELDS += string.Format("[{0}] ,", DR["TEXT"]);
                                GROUP_BY_FIELDS += string.Format("[{0}] ,", DR["TEXT"]);
                            }
                        } 
                    }
                }
            }
            ///
            /// Ölçümler Node Kontrol
            ///       
            for (int iX = 0; iX <= RaporDesigner.gridView_OLCUMLEME.RowCount-1; iX++)
            {
                DataRow DR = RaporDesigner.gridView_OLCUMLEME.GetDataRow(iX);
                if (DR != null)
                { 
                    if (DR["TYPE"].ToString() == "HEDEFKİTLELER")
                    { 
                        if (!RaporDesigner.CHKBOX_OTUZSN_GRP.Checked)
                        {
                            if (DR["ISLEM_TIPI"].ToString() == "Sum")   { OZET_FIELDS += string.Format("SUM([{0}]) AS [{1}],", DR["TEXT"], DR["TEXT"]); }
                            if (DR["ISLEM_TIPI"].ToString() == "Avg")   { OZET_FIELDS += string.Format("AVG([{0}]) AS [{1}],", DR["TEXT"], DR["TEXT"]); }
                            if (DR["ISLEM_TIPI"].ToString() == "Count") { OZET_FIELDS += string.Format("Count([{0}]) AS [{1}],", DR["TEXT"], DR["TEXT"]); }
                        }
                        else
                        {
                            if (DR["ISLEM_TIPI"].ToString() == "Sum")   { OZET_FIELDS += string.Format("SUM([{0}]) AS [{1}],", DR["TEXT"], DR["TEXT"])+ string.Format("SUM([{0}]) AS [{1}],", DR["TEXT"]+ " 30sn", DR["TEXT"] + "  30sn"); }
                            if (DR["ISLEM_TIPI"].ToString() == "Avg")   { OZET_FIELDS += string.Format("AVG([{0}]) AS [{1}],", DR["TEXT"], DR["TEXT"])+string.Format("SUM([{0}]) AS [{1}],", DR["TEXT"] + " 30sn", DR["TEXT"] + "  30sn"); }
                            if (DR["ISLEM_TIPI"].ToString() == "Count") { OZET_FIELDS += string.Format("Count([{0}]) AS [{1}],", DR["TEXT"], DR["TEXT"])+string.Format("SUM([{0}]) AS [{1}],", DR["TEXT"] + " 30sn", DR["TEXT"] + "  30sn"); }
                        }
                    }
                    else
                    {
                        using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            string SQL = " SELECT   * FROM   dbo.ADM_SECENEKLER where SECENEK='" + DR["TEXT"] + "'";
                            SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                            myConnection.Open();
                            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                            if (myReader.HasRows)
                            {
                                while (myReader.Read())
                                {
                                    if (_GLOBAL_PARAMETRELER._DIL == "EN")
                                    { 
                                        if (DR["ISLEM_TIPI"].ToString() == "Sum") { OZET_FIELDS += string.Format("SUM([{0}]) AS [{1}],", myReader["SECENEK_EN"], myReader["SECENEK_EN"]); }
                                        if (DR["ISLEM_TIPI"].ToString() == "Avg") { OZET_FIELDS += string.Format("AVG([{0}]) AS [{1}],", myReader["SECENEK_EN"], myReader["SECENEK_EN"]); }
                                        if (DR["ISLEM_TIPI"].ToString() == "Count") { OZET_FIELDS += string.Format("Count([{0}]) AS [{1}],", myReader["SECENEK_EN"], myReader["SECENEK_EN"]); }
                                    }
                                    else
                                    {
                                        if (DR["ISLEM_TIPI"].ToString() == "Sum") { OZET_FIELDS += string.Format("SUM([{0}]) AS [{1}],", DR["TEXT"], DR["TEXT"]); }
                                        if (DR["ISLEM_TIPI"].ToString() == "Avg") { OZET_FIELDS += string.Format("AVG([{0}]) AS [{1}],", DR["TEXT"], DR["TEXT"]); }
                                        if (DR["ISLEM_TIPI"].ToString() == "Count") { OZET_FIELDS += string.Format("Count([{0}]) AS [{1}],", DR["TEXT"], DR["TEXT"]); }
                                    }
                                }
                            }
                        } 
                    }
                }
            }

                OZET_FIELDS += string.Format("SUM([{0}]) AS [{1}],","NET TUTAR", "NET TUTAR");
            if (RaporDesigner.TOGGLE_OZET_FILITRE.IsOn)
            {    
                string KIRILIM_NAME = string.Empty; 
                for (int iIndex = 0; iIndex < this.KIRILIM_NAME.Count; iIndex++)
                {
                    KIRILIM_NAME += this.KIRILIM_NAME[iIndex];
                } 

                SELECT_FIELDS = KIRILIM_NAME + SELECT_FIELDS + OZET_FIELDS + string.Format(" [{0}] AS [{1}],", "OPT_PT", "OPT PT"); 
                SELECT_FIELDS = SELECT_FIELDS.Substring(0, SELECT_FIELDS.Length - 1);
                INSERT_FIELDS = KIRILIM_NAME + INSERT_FIELDS.Substring(0, INSERT_FIELDS.Length - 1);
                OZET_FIELDS =  OZET_FIELDS.Substring(0, OZET_FIELDS.Length - 1);       
                GROUP_BY_FIELDS = KIRILIM_NAME + GROUP_BY_FIELDS + string.Format(" [{0}],", "OPT_PT");
                GROUP_BY_FIELDS = GROUP_BY_FIELDS.Substring(0, GROUP_BY_FIELDS.Length - 1);

                splashScreenManagers.SetWaitFormDescription("BEKLEYİNİZ DATA AKTARILIYOR");
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    conn.Open();
                    if (BR_RAPOR_TYPE.Caption == "SABIT")
                    {
                        string SQLINSERT = string.Format("INSERT INTO  {0}.[dbo].[__TEMP_{1}] ({2})", _GLOBAL_PARAMETRELER._RAPOR_DB,_GLOBAL_PARAMETRELER._SABIT_TABLO_ADI, INSERT_FIELDS);
                            string SQL = string.Format("select {0} from  {1}.[dbo].[__TEMP_{2}] group by {3}",  SELECT_FIELDS, _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, GROUP_BY_FIELDS);
                        SqlCommand command = new SqlCommand(string.Format("{0} {1}", SQLINSERT, SQL), conn) { CommandTimeout = 0 };
                        SqlCommand cmd = new SqlCommand(string.Format("select {0} from  {1}.[dbo].[__TEMP_{2}] group by {3} ", SELECT_FIELDS, _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, GROUP_BY_FIELDS), conn) { CommandTimeout = 0 };
                        command.ExecuteNonQuery();
                        myReaderData = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
                    }
                    else
                    {
                        string SQL = string.Format("select {0} from  {1}.[dbo].[__TEMP_{2}] group by {3}",  SELECT_FIELDS,_GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, GROUP_BY_FIELDS);
                             SQL+= string.Format("; select {0} from  {1}.[dbo].[__TEMP_{2}] ", OZET_FIELDS, _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, GROUP_BY_FIELDS);
                        SqlCommand command = new SqlCommand(SQL, conn) { CommandTimeout = 0 };
                        myReaderData = command.ExecuteReader(CommandBehavior.SequentialAccess);//.CloseConnection);
                    }

                    RAPORLA.GENEL_RAPOR rp = new RAPORLA.GENEL_RAPOR(BR_RAPOR_SAVE_PATH.Caption, true); 
                    byte[] jsonSecrets = ResourceHelper.GetEmbeddedResourceAsBytes("RAPORLA/SABLONLAR/GenelTemplate.xlsx"); 
                    rp.spreadsheetControls.LoadDocument(jsonSecrets, DocumentFormat.Xlsx); 
                    DateTime BAS_TARIHI = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BAS_TARIHI.EditValue);
                    DateTime BIT_TARIHI = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BIT_TARIHI.EditValue); 
                    Worksheet sheet = rp.spreadsheetControls.Document.Worksheets[1];
                    sheet.Cells["D4"].Value = BR_RAPOR_KODU.Caption;
                    sheet.Cells["D5"].Value = DateTime.Now.ToShortDateString();
                    sheet.Cells["D6"].Value = DateTime.Now.ToShortTimeString();
                    sheet.Cells["D7"].Value = BAS_TARIHI.ToString("dd.MM.yyyy");
                    sheet.Cells["D8"].Value = BIT_TARIHI.ToString("dd.MM.yyyy");
                    sheet.Cells["D9"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                    sheet.Import(myReaderData, true, 10, 1);
                    // myReaderData = null;
                    // myReaderData.AsQueryable();
                    // for (int i=0;i< myReaderData.)
                    double SURE = 0,ADET=0, TUTAR_TL=0, SAYFA=0, NET_TUTAR=0, SANTIM=0;

                    myReaderData.NextResult(); 
                    while (myReaderData.Read())
                    {
                        //if (myReaderData["ADET"] != DBNull.Value) ADET += Convert.ToDouble(myReaderData["ADET"].ToString());
                        //if (myReaderData["SÜRE"] != DBNull.Value) SURE += Convert.ToDouble(myReaderData["SÜRE"].ToString());
                        //if (myReaderData["SANTIM"] != DBNull.Value) SANTIM += Convert.ToDouble(myReaderData["SANTIM"].ToString());
                        //if (myReaderData["SAYFA"] != DBNull.Value) SAYFA += Convert.ToDouble(myReaderData["SAYFA"].ToString());
                        //if (myReaderData["TUTAR TL"] != DBNull.Value) TUTAR_TL += Convert.ToDouble(myReaderData["TUTAR TL"].ToString());
                        //if(myReaderData["NET TUTAR"]!=DBNull.Value) NET_TUTAR += Convert.ToDouble(myReaderData["NET TUTAR"].ToString());
                    }
                    sheet.Cells["I4"].Value = ADET;
                    sheet.Cells["I5"].Value = SURE;
                    sheet.Cells["I6"].Value = SANTIM;
                    sheet.Cells["I7"].Value = SAYFA;
                    sheet.Cells["I8"].Value = TUTAR_TL;
                    sheet.Cells["I9"].Value = NET_TUTAR;
                    IWorkbook workbookm = rp.spreadsheetControls.Document;
                    Range header = sheet.Range["11:11"];
                    header.Style = workbookm.Styles["Accent5"];
                    rp.spreadsheetControls.EndUpdate();
                    if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                    rp.Show();
                }
            }
            else
            {
                if(SELECT_FIELDS.Length>0)  SELECT_FIELDS = SELECT_FIELDS.Substring(0, SELECT_FIELDS.Length - 1);
                if (INSERT_FIELDS.Length > 0) INSERT_FIELDS = INSERT_FIELDS.Substring(0, INSERT_FIELDS.Length - 1);
                if (SUM_FIELDS.Length > 0) SUM_FIELDS = SUM_FIELDS.Substring(0, SUM_FIELDS.Length - 1);
                splashScreenManagers.SetWaitFormDescription("BEKLEYİNİZ DATA AKTARILIYOR");
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    conn.Open();
                    if (BR_RAPOR_TYPE.Caption == "SABIT")
                    {
                        string SQLINSERT = string.Format("INSERT INTO {0}.[dbo].[__TEMP_{1}] ({2})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._SABIT_TABLO_ADI, INSERT_FIELDS);
                        string SQL = string.Format("select {0} from  {1}.[dbo].[__TEMP_{2}] group by {3}",  SELECT_FIELDS, _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, GROUP_BY_FIELDS);
                        SqlCommand command = new SqlCommand(string.Format("{0} {1}", SQLINSERT, SQL), conn) { CommandTimeout = 0 };
                        SqlCommand cmd = new SqlCommand(string.Format("select {0} from  {1}.[dbo].[__TEMP_{2}] group by {3} ",  SELECT_FIELDS, _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, GROUP_BY_FIELDS), conn) { CommandTimeout = 0 };
                        command.ExecuteNonQuery();
                        myReaderData = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
                    }
                    else
                    {
                        string SQL = string.Format("select {1} from  {0}.[dbo].[__TEMP_{2}] ", _GLOBAL_PARAMETRELER._RAPOR_DB, "*" , _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME);
                        SqlCommand command = new SqlCommand(SQL, conn) { CommandTimeout = 0 };
                        myReaderData = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    }

                    RAPORLA.GENEL_RAPOR rp = new RAPORLA.GENEL_RAPOR(BR_RAPOR_SAVE_PATH.Caption, true);
                    byte[] jsonSecrets = ResourceHelper.GetEmbeddedResourceAsBytes("RAPORLA/SABLONLAR/GenelTemplate.xlsx");
                    rp.spreadsheetControls.LoadDocument(jsonSecrets, DocumentFormat.Xlsx);
                    DateTime BAS_TARIHI = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BAS_TARIHI.EditValue);
                    DateTime BIT_TARIHI = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BIT_TARIHI.EditValue);
                    Worksheet sheet = rp.spreadsheetControls.Document.Worksheets[1];//;.Add("CoRe Rapor");
                    sheet.Cells["D4"].Value = BR_RAPOR_KODU.Caption;
                    sheet.Cells["D5"].Value = DateTime.Now.ToShortDateString();
                    sheet.Cells["D6"].Value = DateTime.Now.ToShortTimeString();
                    sheet.Cells["D7"].Value = BAS_TARIHI.ToString("dd.MM.yyyy");
                    sheet.Cells["D8"].Value = BIT_TARIHI.ToString("dd.MM.yyyy");
                    sheet.Cells["D9"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                    sheet.Import(myReaderData, true, 10, 1);

                    // myReaderData = null;
                    // myReaderData.AsQueryable();
                    // for (int i=0;i< myReaderData.)
                    double SURE = 0, ADET = 0, TUTAR_TL = 0, SAYFA = 0, NET_TUTAR = 0, SANTIM = 0;

                    myReaderData.NextResult();
                    while (myReaderData.Read())
                    {
                        //if (myReaderData["ADET"] != DBNull.Value) ADET += Convert.ToDouble(myReaderData["ADET"].ToString());
                        //if (myReaderData["SÜRE"] != DBNull.Value) SURE += Convert.ToDouble(myReaderData["SÜRE"].ToString());
                        //if (myReaderData["SANTIM"] != DBNull.Value) SANTIM += Convert.ToDouble(myReaderData["SANTIM"].ToString());
                        //if (myReaderData["SAYFA"] != DBNull.Value) SAYFA += Convert.ToDouble(myReaderData["SAYFA"].ToString());
                        //if (myReaderData["TUTAR TL"] != DBNull.Value) TUTAR_TL += Convert.ToDouble(myReaderData["TUTAR TL"].ToString());
                        //if(myReaderData["NET TUTAR"]!=DBNull.Value) NET_TUTAR += Convert.ToDouble(myReaderData["NET TUTAR"].ToString());
                    }
                    sheet.Cells["I4"].Value = ADET;
                    sheet.Cells["I5"].Value = SURE;
                    sheet.Cells["I6"].Value = SANTIM;
                    sheet.Cells["I7"].Value = SAYFA;
                    sheet.Cells["I8"].Value = TUTAR_TL;
                    sheet.Cells["I9"].Value = NET_TUTAR;
                    IWorkbook workbookm = rp.spreadsheetControls.Document;
                    Range header = sheet.Range["11:11"];
                    header.Style = workbookm.Styles["Accent5"];
                    rp.spreadsheetControls.EndUpdate();
                    if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                    rp.Show();
                
                } 
            } 
            if (RaporDesigner._FTP_DURUMU)
            { 
                try
                {
                    FileInfo dosyaBilgisi = new FileInfo(@""+ BR_RAPOR_SAVE_PATH.Caption);
                    dosyaBilgisi.CopyTo(@"C:\\temp\\_"+ dosyaBilgisi.Name);
                    FtpWebRequest ftpRequest = (FtpWebRequest)WebRequest.Create("ftp://"+ RaporDesigner._FTP_ADRESI + "/" + "_"+dosyaBilgisi.Name); 
                    ftpRequest.Credentials = new NetworkCredential(RaporDesigner._FTP_USERNAME.Normalize(), RaporDesigner._FTP_PASSWORD.Normalize());
                    ftpRequest.Method = WebRequestMethods.Ftp.UploadFile; 
                    byte[] fileContent;

                    using (StreamReader sr = new StreamReader(@"C:\\temp\\_"+ dosyaBilgisi.Name))
                    {
                        fileContent = Encoding.UTF8.GetBytes(sr.ReadToEnd());
                    }
                    using (Stream sw = ftpRequest.GetRequestStream())
                    {
                        sw.Write(fileContent, 0, fileContent.Length);
                    } 
                    ftpRequest.GetResponse(); 
                }
                catch (Exception ex) { Console.WriteLine(ex.ToString()); }
                return;  
            } 
        }
        private void RAPORU_YENIDEN_EXEL_AKTAR()
        {
            MyTable = new DataTable("RAPOR_EXPORT");
            TABLE_SHEMA_KONTROL();

            string SELECT_FIELDS = "", INSERT_FIELDS = "", OZET_FIELDS = "", GROUP_BY_FIELDS = "", SUM_FIELDS = "";
            for (int z = 0; z <= MyTable.Columns.Count - 1; z++)
            {
                var name = MyTable.Columns[z].ColumnName.ToString();
                var dataType = MyTable.Columns[z].DataType.ToString();
                if (name != "TİME İNT")
                {
                    string datatypes = dataType.ToString().Replace("System.", "");
                    if (datatypes == "String")
                    {
                        if (name != "BAŞLANGIÇ" && name != "TARİH")
                        {
                            INSERT_FIELDS += string.Format("[{0}] ,", name);
                            SELECT_FIELDS += string.Format("[{0}] ,", name);
                            GROUP_BY_FIELDS += string.Format("[{0}] ,", name);
                        }
                    }
                    else
                    {
                        if (datatypes != "DateTime")
                        {
                            if (name != "AY" && name != "YIL" && name != "Hafta")
                            {
                                INSERT_FIELDS += string.Format("[{0}] ,", name);
                                SELECT_FIELDS += string.Format("sum ([{0}])  as [{0}] ,", name);
                                SUM_FIELDS += string.Format("([{0}])  as [{0}] ,", name);
                            }
                            else
                            {
                                INSERT_FIELDS += string.Format("[{0}] ,", name);
                                SELECT_FIELDS += string.Format("[{0}] ,", name);
                                GROUP_BY_FIELDS += string.Format("[{0}] ,", name);
                            }
                        }
                    }
                }
            }



            for (int z = 0; z <= MyTable.Columns.Count - 1; z++)
            {
                var name = MyTable.Columns[z].ColumnName.ToString();
                var dataType = MyTable.Columns[z].DataType.ToString();
                string datatypes = dataType.ToString().Replace("System.", "");
                if (name == "ADET" || name == "SÜRE" || name == "SANTIM" || name == "SAYFA" || name == "TUTAR TL" || name == "NET TUTAR")
                {
                    OZET_FIELDS += string.Format("SUM([{0}]) AS [{1}],", name, name);
                }
            }


            if (RaporDesigner.TOGGLE_OZET_FILITRE.IsOn)
            {
                GROUP_BY_FIELDS = GROUP_BY_FIELDS.Substring(0, GROUP_BY_FIELDS.Length - 1);
                SELECT_FIELDS = SELECT_FIELDS.Substring(0, SELECT_FIELDS.Length - 1);
                INSERT_FIELDS = INSERT_FIELDS.Substring(0, INSERT_FIELDS.Length - 1);

                OZET_FIELDS = OZET_FIELDS.Substring(0, OZET_FIELDS.Length - 1);

                // splashScreenManagers.SetWaitFormDescription("BEKLEYİNİZ DATA AKTARILIYOR");
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    conn.Open();
             
                        string SQL = string.Format("select {0} from  {1}.[dbo].[__TEMP_{2}] group by {3}", SELECT_FIELDS, _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, GROUP_BY_FIELDS);
                        SQL += string.Format("; select {0} from  {1}.[dbo].[__TEMP_{2}] ", OZET_FIELDS, _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, GROUP_BY_FIELDS);
                        SqlCommand command = new SqlCommand(SQL, conn) { CommandTimeout = 0 };
                        myReaderData = command.ExecuteReader(CommandBehavior.SequentialAccess);//.CloseConnection);
                  

                    RAPORLA.GENEL_RAPOR rp = new RAPORLA.GENEL_RAPOR(BR_RAPOR_SAVE_PATH.Caption, true);
                    byte[] jsonSecrets = ResourceHelper.GetEmbeddedResourceAsBytes("RAPORLA/SABLONLAR/GenelTemplate.xlsx");
                    rp.spreadsheetControls.LoadDocument(jsonSecrets, DocumentFormat.Xlsx);
                    DateTime BAS_TARIHI = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BAS_TARIHI.EditValue);
                    DateTime BIT_TARIHI = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BIT_TARIHI.EditValue);

                    Worksheet sheet = rp.spreadsheetControls.Document.Worksheets[1];//;.Add("CoRe Rapor");

                    sheet.Cells["D4"].Value = BR_RAPOR_KODU.Caption;
                    sheet.Cells["D5"].Value = DateTime.Now.ToShortDateString();
                    sheet.Cells["D6"].Value = DateTime.Now.ToShortTimeString();
                    sheet.Cells["D7"].Value = BAS_TARIHI.ToString("dd.MM.yyyy");
                    sheet.Cells["D8"].Value = BIT_TARIHI.ToString("dd.MM.yyyy");
                    sheet.Cells["D9"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;

                    sheet.Import(myReaderData, true, 10, 1);

                    // myReaderData = null;
                    // myReaderData.AsQueryable();
                    // for (int i=0;i< myReaderData.)
                    double SURE = 0, ADET = 0, TUTAR_TL = 0, SAYFA = 0, NET_TUTAR = 0, SANTIM = 0;

                    myReaderData.NextResult();
                    while (myReaderData.Read())
                    {
                        //if (myReaderData["ADET"] != DBNull.Value) ADET += Convert.ToDouble(myReaderData["ADET"].ToString());
                        //if (myReaderData["SÜRE"] != DBNull.Value) SURE += Convert.ToDouble(myReaderData["SÜRE"].ToString());
                        //if (myReaderData["SANTIM"] != DBNull.Value) SANTIM += Convert.ToDouble(myReaderData["SANTIM"].ToString());
                        //if (myReaderData["SAYFA"] != DBNull.Value) SAYFA += Convert.ToDouble(myReaderData["SAYFA"].ToString());
                        //if (myReaderData["TUTAR TL"] != DBNull.Value) TUTAR_TL += Convert.ToDouble(myReaderData["TUTAR TL"].ToString());
                        //if(myReaderData["NET TUTAR"]!=DBNull.Value) NET_TUTAR += Convert.ToDouble(myReaderData["NET TUTAR"].ToString());
                    }


                    sheet.Cells["I4"].Value = ADET;
                    sheet.Cells["I5"].Value = SURE;
                    sheet.Cells["I6"].Value = SANTIM;
                    sheet.Cells["I7"].Value = SAYFA;
                    sheet.Cells["I8"].Value = TUTAR_TL;
                    sheet.Cells["I9"].Value = NET_TUTAR;

                    IWorkbook workbookm = rp.spreadsheetControls.Document;
                    Range header = sheet.Range["11:11"];
                    header.Style = workbookm.Styles["Accent5"];  
                    rp.spreadsheetControls.EndUpdate();
                    if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                    rp.Show(); 
                }

            }
            else
            {
                SELECT_FIELDS = SELECT_FIELDS.Substring(0, SELECT_FIELDS.Length - 1);
                INSERT_FIELDS = INSERT_FIELDS.Substring(0, INSERT_FIELDS.Length - 1);
                SUM_FIELDS = SUM_FIELDS.Substring(0, SUM_FIELDS.Length - 1);
                //  splashScreenManagers.SetWaitFormDescription("BEKLEYİNİZ DATA AKTARILIYOR");
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    conn.Open();
                    if (BR_RAPOR_TYPE.Caption == "SABIT")
                    {
                        string SQLINSERT = string.Format("INSERT INTO {0}.[dbo].[__TEMP_{1}] ({2})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._SABIT_TABLO_ADI, INSERT_FIELDS);
                        string SQL = string.Format("select {0} from  {1}.[dbo].[__TEMP_{2}] group by {3}", SELECT_FIELDS, _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, GROUP_BY_FIELDS);
                        SqlCommand command = new SqlCommand(SQLINSERT + " " + SQL, conn) { CommandTimeout = 0 };
                        SqlCommand cmd = new SqlCommand(string.Format("select {0} from  {1}.[dbo].[__TEMP_{2}] group by {3} ", SELECT_FIELDS, _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, GROUP_BY_FIELDS), conn) { CommandTimeout = 0 };
                        command.ExecuteNonQuery();
                        myReaderData = cmd.ExecuteReader(CommandBehavior.SequentialAccess);
                    }
                    else
                    {
                        string SQL = string.Format("select {1} from  {0}.[dbo].[__TEMP_{2}] ", _GLOBAL_PARAMETRELER._RAPOR_DB, INSERT_FIELDS, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME);
                        SqlCommand command = new SqlCommand(SQL, conn) { CommandTimeout = 0 };
                        myReaderData = command.ExecuteReader(CommandBehavior.SequentialAccess);
                    }

                    RAPORLA.GENEL_RAPOR rp = new RAPORLA.GENEL_RAPOR(BR_RAPOR_SAVE_PATH.Caption, (bool)BR_RAPOR_AUTO_SAVE.EditValue);
                    Worksheet sheet = rp.spreadsheetControls.Document.Worksheets.Add("CoRe Rapor");
                    sheet.Cells["A1"].Value = "Rapor kodu";
                    sheet.Cells["A2"].Value = "Tarih";
                    sheet.Cells["b1"].Value = "Core Rapor";
                    sheet.Cells["b2"].Value = DateTime.Now.ToShortDateString();
                    sheet.Import(myReaderData, true, 5, 0);
                    rp.spreadsheetControls.EndUpdate();
                    if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                    rp.Show();

                }
            }

        }
        private void VIEW_RAPOR_OLUSTUR(string FIELD_LIST)
        {
            FIELD_LIST = FIELD_LIST.Substring(0, FIELD_LIST.Length - 1);
            VIEW_TABLE_SHEMA_KONTROL(FIELD_LIST); 
 
                string SELECT_FIELDS = "", INSERT_FIELDS = "";// _FIELDS = "";
                string GROUP_BY_FIELDS = "";//, SUM_FIELDS = "";

                for (int z = 0; z <= MyTable.Columns.Count - 1; z++)
                {
                    var name = MyTable.Columns[z].ColumnName.ToString();
                    var dataType = MyTable.Columns[z].DataType.ToString();
                    if (name != "TİME İNT")
                    {
                        string datatypes = dataType.ToString().Replace("System.", "");
                        if (datatypes == "String")
                        {
                            if (name != "BAŞLANGIÇ" && name != "TARİH")
                            {
                                INSERT_FIELDS += string.Format("[{0}] ,", name);
                                SELECT_FIELDS += string.Format("[{0}] ,", name);
                                GROUP_BY_FIELDS += string.Format("[{0}] ,", name); 
                            }
                        }
                        else
                        {
                            if (datatypes != "DateTime")
                            {
                                if (name != "AY" && name != "YIL" && name != "Hafta")
                                {
                                    INSERT_FIELDS += string.Format("[{0}] ,", name);
                                    SELECT_FIELDS += string.Format("sum ([{0}])  as [{0}] ,", name);
                                }
                                else
                                {
                                    INSERT_FIELDS += string.Format("[{0}] ,", name);
                                    SELECT_FIELDS += string.Format("[{0}] ,", name);
                                    GROUP_BY_FIELDS += string.Format("[{0}] ,", name);
                                } 
                            }
                        }

                    }
                }

                GROUP_BY_FIELDS = GROUP_BY_FIELDS.Substring(0, GROUP_BY_FIELDS.Length - 1);
                SELECT_FIELDS = SELECT_FIELDS.Substring(0, SELECT_FIELDS.Length - 1);
                INSERT_FIELDS = INSERT_FIELDS.Substring(0, INSERT_FIELDS.Length - 1);

                 // QUERY = TEMIZLE_ELEMAN(QUERY, 4,"").ToString();
        


                DateTime BAS_TARIHI = Convert.ToDateTime(RaporViewer.dtBAS_TARIHI.EditValue);
                DateTime BIT_TARIHI = Convert.ToDateTime(RaporViewer.dtBIT_TARIHI.EditValue);
     
                splashScreenManagers.SetWaitFormDescription("BEKLEYİNİZ DATA AKTARILIYOR");
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {   conn.Open(); 
                    string SQL=""; 
                    if (RaporViewer.TOOGLE_TELEVIZYON.IsOn)
                       { 
                            if (SQL != "") SQL += "UNION ALL ";
                            SQL += string.Format("select {0} from " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".[dbo].[__TEMP_{1}_LNK_{2}] WHERE {3}  group by {4}", SELECT_FIELDS, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._RAPOR_KODU, QUERY 
                                + "[YAYIN SINIFI]='TELEVIZYON'  AND  (YIL >= " + BAS_TARIHI.ToString("yyyy") + "  AND YIL <= " + BIT_TARIHI.ToString("yyyy") + ") AND (AY >= " + BAS_TARIHI.ToString("MM") + " and  AY <= " + BIT_TARIHI.ToString("MM") + ")" , GROUP_BY_FIELDS);

                        
                        }

                    if (RaporViewer.TOOGLE_RADYO.IsOn)
                    {
                    
                        if (SQL != "") SQL += "UNION ALL ";
                        SQL += string.Format("select {0} from  " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".[dbo].[__TEMP_{1}_LNK_{2}] WHERE {3}  group by {4}", SELECT_FIELDS, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._RAPOR_KODU, QUERY
                                             + "[YAYIN SINIFI]='RADYO'  AND  (YIL >= " + BAS_TARIHI.ToString("yyyy") + "  AND YIL <= " + BIT_TARIHI.ToString("yyyy") + ") AND (AY >= " + BAS_TARIHI.ToString("MM") + " and  AY <= " + BIT_TARIHI.ToString("MM") + ")", GROUP_BY_FIELDS);
                        }

                    if (RaporViewer.TOOGLE_DERGI.IsOn)
                       {
                      
                        if (SQL != "") SQL += "UNION ALL ";
                        SQL += string.Format("select {0} from  " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".[dbo].[__TEMP_{1}_LNK_{2}] WHERE {3}  group by {4}", SELECT_FIELDS, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._RAPOR_KODU, QUERY 
                              + "[YAYIN SINIFI]='DERGI'  AND  (YIL >= " + BAS_TARIHI.ToString("yyyy") + "  AND YIL <= " + BIT_TARIHI.ToString("yyyy") + ") AND (AY >= " + BAS_TARIHI.ToString("MM") + " and  AY <= " + BIT_TARIHI.ToString("MM") + ")", GROUP_BY_FIELDS); 
                        }

                    if (RaporViewer.TOOGLE_GAZETE.IsOn)
                    {
                  
                        if (SQL != "") SQL += "UNION ALL ";
                        SQL += string.Format("select {0} from  " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".[dbo].[__TEMP_{1}_LNK_{2}] WHERE {3}  group by {4}", SELECT_FIELDS, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._RAPOR_KODU, QUERY
                               + "[YAYIN SINIFI]='GAZETE'  AND  (YIL >= " + BAS_TARIHI.ToString("yyyy") + "  AND YIL <= " + BIT_TARIHI.ToString("yyyy") + ") AND (AY >= " + BAS_TARIHI.ToString("MM") + " and  AY <= " + BIT_TARIHI.ToString("MM") + ")", GROUP_BY_FIELDS); 
                        }


                    if (RaporViewer.TOOGLE_SINEMA.IsOn)
                    {
                     
                        if (SQL != "") SQL += "UNION ALL ";
                        SQL += string.Format("select {0} from  " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".[dbo].[__TEMP_{1}_LNK_{2}] WHERE {3}  group by {4}", SELECT_FIELDS, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._RAPOR_KODU, QUERY
                        + "[YAYIN SINIFI]='SINEMA'  AND  (YIL >= " + BAS_TARIHI.ToString("yyyy") + "  AND YIL <= " + BIT_TARIHI.ToString("yyyy") + ") AND (AY >= " + BAS_TARIHI.ToString("MM") + " and  AY <= " + BIT_TARIHI.ToString("MM") + ")", GROUP_BY_FIELDS);

                    //SQL += string.Format(string.Format("select {{0}} from  {0}.[dbo].[__TEMP_{{1}}_LNK_{{2}}] WHERE {{3}}  group by {{4}}", _GLOBAL_PARAMETRELER._RAPOR_DB), SELECT_FIELDS, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._RAPOR_KODU, QUERY


                }

                   if (RaporViewer.TOOGLE_OUTDOOR.IsOn)
                   {    
                        if (SQL != "") SQL += "UNION ALL ";
                        SQL += string.Format("select {0} from  " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".[dbo].[__TEMP_{1}_LNK_{2}] WHERE {3}  group by {4}", SELECT_FIELDS, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._RAPOR_KODU, QUERY 
                                  + "[YAYIN SINIFI]='OUTDOOR'  AND  (YIL >= " + BAS_TARIHI.ToString("yyyy") + "  AND YIL <= " + BIT_TARIHI.ToString("yyyy") + ") AND (AY >= " + BAS_TARIHI.ToString("MM") + " and  AY <= " + BIT_TARIHI.ToString("MM") + ")", GROUP_BY_FIELDS); 
                   }
                   if (RaporViewer.TOOGLE_INTERNET.IsOn)
                   {
                        if (SQL != "") SQL += "UNION ALL ";
                        SQL += string.Format("select {0} from  " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".[dbo].[__TEMP_{1}_LNK_{2}] WHERE {3}  group by {4}", SELECT_FIELDS, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._RAPOR_KODU, QUERY
                                    + "[YAYIN SINIFI]='INTERNET'  AND  (YIL >= " + BAS_TARIHI.ToString("yyyy") + "  AND YIL <= " + BIT_TARIHI.ToString("yyyy") + ") AND (AY >= " + BAS_TARIHI.ToString("MM") + " and  AY <= " + BIT_TARIHI.ToString("MM") + ")", GROUP_BY_FIELDS);
                   }                      
               SqlCommand command = new SqlCommand(SQL, conn) { CommandTimeout = 0 };
               myReaderData = command.ExecuteReader();


                RAPORLA.GENEL_RAPOR rp = new RAPORLA.GENEL_RAPOR(BR_RAPOR_SAVE_PATH.Caption, true);
                byte[] jsonSecrets = ResourceHelper.GetEmbeddedResourceAsBytes("RAPORLA/SABLONLAR/GenelTemplate.xlsx");
                rp.spreadsheetControls.LoadDocument(jsonSecrets, DocumentFormat.Xlsx);
                DateTime BAS_TARIH = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BAS_TARIHI.EditValue);
                DateTime BIT_TARIH = Convert.ToDateTime(RaporDesigner.DT_GNL_RPR_BIT_TARIHI.EditValue);

                Worksheet sheet = rp.spreadsheetControls.Document.Worksheets[1];//;.Add("CoRe Rapor");

                sheet.Cells["D4"].Value = BR_RAPOR_KODU.Caption;
                sheet.Cells["D5"].Value = DateTime.Now.ToShortDateString();
                sheet.Cells["D6"].Value = DateTime.Now.ToShortTimeString();
                sheet.Cells["D7"].Value = BAS_TARIHI.ToString("dd.MM.yyyy");
                sheet.Cells["D8"].Value = BIT_TARIHI.ToString("dd.MM.yyyy");
                sheet.Cells["D9"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;

                sheet.Import(myReaderData, true, 10, 1);

                // myReaderData = null;
                // myReaderData.AsQueryable();
                // for (int i=0;i< myReaderData.)
                double SURE = 0, ADET = 0, TUTAR_TL = 0, SAYFA = 0, NET_TUTAR = 0, SANTIM = 0;

                myReaderData.NextResult();
                while (myReaderData.Read())
                {
                    //if (myReaderData["ADET"] != DBNull.Value) ADET += Convert.ToDouble(myReaderData["ADET"].ToString());
                    //if (myReaderData["SÜRE"] != DBNull.Value) SURE += Convert.ToDouble(myReaderData["SÜRE"].ToString());
                    //if (myReaderData["SANTIM"] != DBNull.Value) SANTIM += Convert.ToDouble(myReaderData["SANTIM"].ToString());
                    //if (myReaderData["SAYFA"] != DBNull.Value) SAYFA += Convert.ToDouble(myReaderData["SAYFA"].ToString());
                    //if (myReaderData["TUTAR TL"] != DBNull.Value) TUTAR_TL += Convert.ToDouble(myReaderData["TUTAR TL"].ToString());
                    //if(myReaderData["NET TUTAR"]!=DBNull.Value) NET_TUTAR += Convert.ToDouble(myReaderData["NET TUTAR"].ToString());
                }


                sheet.Cells["I4"].Value = ADET;
                sheet.Cells["I5"].Value = SURE;
                sheet.Cells["I6"].Value = SANTIM;
                sheet.Cells["I7"].Value = SAYFA;
                sheet.Cells["I8"].Value = TUTAR_TL;
                sheet.Cells["I9"].Value = NET_TUTAR;

                IWorkbook workbookm = rp.spreadsheetControls.Document;
                Range header = sheet.Range["11:11"];
                header.Style = workbookm.Styles["Accent5"];


                rp.spreadsheetControls.EndUpdate();
                if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                rp.Show();
            } 
        }
        private void TABLE_SHEMA_KONTROL()
        {
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(string.Format(" select top 100 *  from {0}.[dbo].[__TEMP_{1}] ", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME), conn) { CommandTimeout = 0 };
                using (SqlDataReader myReader = command.ExecuteReader())
                {
                    MyTable.Load(myReader);
                }
                conn.Close();
            } 
        }
        private void VIEW_TABLE_SHEMA_KONTROL(string FIELD_LIST)
        {
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                conn.Open();
                SqlCommand command = new SqlCommand(string.Format(" select top 100   " + FIELD_LIST + "    from "+_GLOBAL_PARAMETRELER._RAPOR_DB+".[dbo].[__TEMP_{0}_LNK_{1}] ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._RAPOR_KODU), conn) { CommandTimeout = 0 };
                using (SqlDataReader myReader = command.ExecuteReader())
                {
                    MyTable.Load(myReader);
                }
                conn.Close();
            }
        }
    
        private void TABLE_CREATE()
        {
            if (TEMP_TABLE_CREATE.Count == 0) { return; }

            string BASLIK_TABLE_CREATE = "";

            for (int iIndex = 0; iIndex < TEMP_TABLE_CREATE.Count; iIndex++)
            {
                BASLIK_TABLE_CREATE += TEMP_TABLE_CREATE[iIndex];
            }
            if (BASLIK_TABLE_CREATE.Length > 0) BASLIK_TABLE_CREATE = BASLIK_TABLE_CREATE.Substring(0, BASLIK_TABLE_CREATE.Length - 1);

            using (SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format("IF EXISTS (SELECT * FROM {0}.dbo.sysobjects where id = object_id('{0}.[dbo].[__TEMP_{1}]')) drop table {0}.[dbo].[__TEMP_{1}]", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME);
                SqlCommand myCommandTable = new SqlCommand(SQL) { Connection = myConnectionTable, CommandTimeout = 60 };
                myConnectionTable.Open();
                myCommandTable.ExecuteNonQuery();
                myCommandTable.Connection.Close();
                myConnectionTable.Close();
            }
            using (SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format("   CREATE TABLE {0}.[dbo].[__TEMP_{1}]  ( {2},[NET TUTAR] [float] NULL,[OPT_PT] [nvarchar](15) NULL ) ON [PRIMARY];", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME,  BASLIK_TABLE_CREATE);
                SqlCommand myCommandTable = new SqlCommand(SQL) { Connection = myConnectionTable };
                myConnectionTable.Open();
                myCommandTable.CommandTimeout = 0;
                myCommandTable.ExecuteNonQuery();
                myCommandTable.Connection.Close();
                myConnectionTable.Close();
            }
            if (BR_RAPOR_TYPE.Caption == "SABIT")
            {
                using (SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQL = string.Format(" IF NOT EXISTS (SELECT * FROM {0}.dbo.sysobjects where id = object_id('{0}.[dbo].[__TEMP_{1}]')) CREATE TABLE {0}.[dbo].[__TEMP_{1}]  ({2} , [NET TUTAR] [float] NULL,[OPT_PT] [nvarchar](15) NULL ) ON [PRIMARY];", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._SABIT_TABLO_ADI,  BASLIK_TABLE_CREATE);
                    SqlCommand myCommandTable = new SqlCommand(SQL) { Connection = myConnectionTable };
                    myConnectionTable.Open();
                    myCommandTable.CommandTimeout = 0;
                    myCommandTable.ExecuteNonQuery();
                    myCommandTable.Connection.Close();
                    myConnectionTable.Close();
                }
            }
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet ds = new DataSet();
                string query = string.Format("      SELECT  top 100 * FROM  {0}.[dbo].[__TEMP_{1}]   ", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME);
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                adapter.Fill(ds, "dbo_TEMP_RAPOR");
                MyTable = ds.Tables[0];
            }
            TABLO_DURUMU = "OLUSTURULDU";
        }

        private void BTN_YNT_RAPOR_AYARLARI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            ADMIN.RAPORLAR todo = new ADMIN.RAPORLAR();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        }
        private void BTN_MAS_FREE_MAST_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.MASTER_FREE todo = new TARIFELER.MASTER_FREE() { Text = string.Format("{0}-{1}", "Free Master", childCount), MdiParent = this };
            todo.Show();
        }
        private void BTN_MAS_WORD_MANANGE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            childCount++;
            TARIFELER.MASTER_KEYWORD todo = new TARIFELER.MASTER_KEYWORD();
            todo.Text = string.Format("{0}-{1}", todo.Name, childCount);
            todo.MdiParent = this;
            todo.Show();
        } 
        private void BTN_KAYDET_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { 
            if (RaporDesigner != null)
            {
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    DateTime RUN_DATE = DateTime.Now;
                    string SQL = " INSERT INTO [dbo].[ADM_LOG_TABLE] (ISLEM_TURU,RAPOR_KODU,ISLEM_DETAYI,ISLEM_TARIHI,ISLEM_SAATI,ISLEMI_YAPAN) VALUES ('RAPOR KAYDET','" + _GLOBAL_PARAMETRELER._RAPOR_KODU + "','RAPOR KAYDET','" + RUN_DATE.ToString("yyyy.MM.dd") + "','" + RUN_DATE.ToString("HH:mm:ss") + "','" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "')";
                    SqlCommand command = new SqlCommand(SQL, conn);
                    conn.Open();
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();
                }

                if (BR_RAPOR_KODU.Caption.Length == 0)
                {          
                    _MENU_BACK frm = new _MENU_BACK("KAYDET","");
                    frm.ShowDialog();
                    if (frm._BTN_TYPE != "Close")
                    {
                        if (frm._TXT_FILE_NAME.Length > 0)
                        {
                            splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
                            splashScreenManagers.ClosingDelay = 400;
                            splashScreenManagers.ShowWaitForm();
                            splashScreenManagers.SetWaitFormDescription("Kontrol ediliyor.");

                            var ID = RaporDesigner._SAVE_UPDATE("0", frm._TXT_FILE_NAME, frm._TXT_ACIKLAMA, frm.BTN_FILE_SAVE_ADRESS.Text, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                            if (Convert.ToInt32(ID) != 0)
                            {
                                RaporDesigner.Text = frm._TXT_FILE_NAME;
                                _GLOBAL_PARAMETRELER._RAPOR_KODU = frm._TXT_FILE_NAME;
                                BR_RAPOR_KODU.Caption = frm._TXT_FILE_NAME;
                                BR_RAPOR_ID.Caption = ID;
                                BR_RAPOR_TYPE.Caption = "TEMP";
                                BR_RAPOR_SAVE_PATH.Caption = frm.BTN_FILE_SAVE_ADRESS.Text;
                                BR_RAPOR_AUTO_SAVE.EditValue = true;
                                BR_RAPOR_ACIKLAMA.EditValue = frm._TXT_ACIKLAMA;
                                splashScreenManagers.SetWaitFormDescription("KAYDEDİLDİ");
                            }
                            else
                            {
                                if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();

                                MessageBox.Show(frm._TXT_FILE_NAME + " Bu Kod daha önce kullanılmış", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }

                            
                            if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                        }
                        else
                        { MessageBox.Show(" Lütfen Rapor Kodu giriniz."); }
                    }
                }
                else
                { 
                    //if (RAPOR_SAHIBI != "YABANCI")
                    //{
                        splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
                        splashScreenManagers.ClosingDelay = 400;
                        splashScreenManagers.ShowWaitForm();
                        splashScreenManagers.SetWaitFormDescription("KAYDEDİLİYOR");
                        _GLOBAL_PARAMETRELER._RAPOR_KODU = BR_RAPOR_KODU.Caption;
                        RaporDesigner._SAVE_UPDATE(BR_RAPOR_ID.Caption, BR_RAPOR_KODU.Caption, BR_RAPOR_ACIKLAMA.EditValue.ToString(), BR_RAPOR_SAVE_PATH.Caption,_GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                        splashScreenManagers.SetWaitFormDescription("KAYDEDİLDİ");
                        if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                    //}
                    //else
                    //{ MessageBox.Show(" Paylaştırılımış raporları raporu oluşturan kullanıcı edit edebilir. Farklı kaydet ile kendinize bir kopya alabilirsiniz."); }
                }
            }
            else
            { MessageBox.Show(" Lütfen Rapor Ekranı açınız.."); } 
        }
        private void BTN_MAS_TARIFE_KONTROL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { 
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DateTime RUN_DATE = DateTime.Now;
                string SQL = " INSERT INTO [dbo].[ADM_LOG_TABLE] (ISLEM_TURU,RAPOR_KODU,ISLEM_DETAYI,ISLEM_TARIHI,ISLEM_SAATI,ISLEMI_YAPAN) VALUES ('MASTER','" + _GLOBAL_PARAMETRELER._RAPOR_KODU + "','MASTER KONTROL','" + RUN_DATE.ToString("yyyy.MM.dd") + "','" + RUN_DATE.ToString("HH:mm:ss") + "','" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "')";
                SqlCommand command = new SqlCommand(SQL, conn);
                conn.Open(); 
                command.ExecuteReader(CommandBehavior.CloseConnection);
                conn.Close();
            } 
       
            TARIFELER.MASTER_KONTROL fr = new TARIFELER.MASTER_KONTROL();
            fr.ShowDialog(); 

            if (fr._DURUMU == "TAMAM")
            {
                splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
                splashScreenManagers.ClosingDelay = 500;
                splashScreenManagers.ShowWaitForm();

                PARAMETRE_TEMIZLE();

                for (int i = 0; i < RaporDesigner.MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages.Count; i++)
                {
                    string StrField = null; 
                    var rtb = RaporDesigner.MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages[i].Controls[0]; 
                    DevExpress.XtraGrid.GridControl grd_ = (DevExpress.XtraGrid.GridControl)rtb;
                    DataView RW = (DataView)grd_.DataSource;
                    DevExpress.XtraGrid.Views.Grid.GridView grdView_ = (DevExpress.XtraGrid.Views.Grid.GridView)grd_.Views[0]; 
                    SELECT_FIELDS = GROUP_BY_FIELDS = "";
                    for (int ix = 0; ix <= grdView_.Columns.Count - 1; ix++)
                    {
                        if (grdView_.Columns[ix].Caption != "ID")
                        {
                            using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                string SQLD = string.Format(" SELECT   * FROM   dbo.ADM_SECENEKLER where FIELDS='{0}'", grdView_.Columns[ix].FieldName);
                                SqlCommand myCommand = new SqlCommand(SQLD, con) { CommandText = SQLD.ToString() };
                                con.Open();
                                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                                if (myReader.HasRows)
                                {
                                    while (myReader.Read())
                                    {
                                        StrField += myReader["FIELDS"] + ",";
                                        SELECT_FIELDS += string.Format("[{0}],", myReader["FIELDS"]);
                                        GROUP_BY_FIELDS += string.Format("[{0}],", myReader["FIELDS"]);
                                    }
                                }
                                else
                                {
                                    SELECT_FIELDS += string.Format(" CAST('' AS nvarchar ) as [{0}],", grdView_.Columns[ix].FieldName);
                                }
                            }
                        }
                    }
                    GROUP_BY_FIELDS = TEMIZLE_ELEMAN(GROUP_BY_FIELDS, 1, ",").ToString();
                    SELECT_FIELDS = TEMIZLE_ELEMAN(SELECT_FIELDS, 1, ",").ToString();
                    SQL = "";

                    StatusStrip stst = new StatusStrip();
                    var rtms = (StatusStrip)RaporDesigner.MstrSablon.xtraTabControl_MASTER_MNG_DETAY.TabPages[i].Controls[3];

                  
                    stst= rtms;

                    string PATH_KIRILIMLAR = "";
                    if (RaporDesigner.treeList_KIRILIMLAR.Nodes.Count != 0)
                    {
                        List<TreeListNode> ndsc = RaporDesigner.treeList_KIRILIMLAR.GetAllCheckedNodes();
                        foreach (TreeListNode nodes in ndsc)
                        {
                            if (nodes.Checked)
                            {
                                KIRILIM_CAST.Clear();
                                PATH_KIRILIMLAR = GetFullPath(nodes, "\\");
                                PATH_KIRILIMLAR = PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);
                                string[] wordm = PATH_KIRILIMLAR.Split('\\');
                                if (COLUMS_COUNT < wordm.Length) COLUMS_COUNT = wordm.Length;
                                if (COLUMS_COUNT > 0)
                                {
                                    int NodeSay = 0;
                                    for (int xi = 1; xi <= COLUMS_COUNT; xi++)
                                    {
                                        KIRILIM_CAST.Add(string.Format(" CAST('{0}' AS nvarchar ) AS  [KIRILIM_{1}],", wordm[NodeSay], xi));
                                        if (NodeSay < wordm.Length - 1) NodeSay++;
                                    }
                                }
                                splashScreenManagers.SetWaitFormDescription(PATH_KIRILIMLAR);
                                if (RaporDesigner.TOGGLE_OZEL_FILITRE.IsOn && RaporDesigner.treeList_FILITRELER.Nodes.Count > 0)
                                {
                                    OZEL_FILITRE(PATH_KIRILIMLAR, stst.Items[3].Text);
                                }
                            }
                        }
                    }

                    using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                    { 
                        
                            SQL = " SELECT  * FROM (  " + SQL;
                            SQL += " EXCEPT ";
                            SQL += " SELECT " + SELECT_FIELDS + " FROM   dbo.[__MAS_EDT_" + rtb.Tag + "_" + rtb.Name + "] ) X ";  //; AS b WHERE " + StrParaMeters + "  order by   " + StrFieldUpdate + "";                        }
                      

                            SqlCommand commands = new SqlCommand(SQL, connection);
                            commands.CommandTimeout = 0;
                            connection.Open();
                            SqlDataReader reader = commands.ExecuteReader();
                            SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                            myConnectionTable.Open();
                            Int32 ixm = 0;
                            while (reader.Read())
                            {
                                splashScreenManagers.SetWaitFormDescription(rtb.Name + " EKLENIYOR " + ixm++);
                                DataRowView rowView = RW.AddNew();
                                string[] Ones = StrField.ToString().Split(',');
                                if (Ones[0].ToString() != string.Empty)
                                {
                                    for (int cx = 0; cx < Ones.Length; cx++)
                                    {
                                        if (Ones[cx].ToString() != string.Empty) rowView[Ones[cx]] = reader[Ones[cx]].ToString();
                                    }
                                }
                                rowView.EndEdit();
                            }
                            //    RaporDesigner.TrfSablon.LBL_TELEVIZYON_TARIFE_DURUMU.Text  = "KONTOL_EDILDI";
                            myConnectionTable.Close();
                            reader.Close();
                        }
                    }
                    if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
              }
         } 
        private void BTN_FARKLI_KAYDET_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { 
            if (RaporDesigner != null)
            {
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    DateTime RUN_DATE = DateTime.Now;
                    string SQL = " INSERT INTO [dbo].[ADM_LOG_TABLE] (ISLEM_TURU,RAPOR_KODU,ISLEM_DETAYI,ISLEM_TARIHI,ISLEM_SAATI,ISLEMI_YAPAN) VALUES ('RAPOR KAYDET','" + _GLOBAL_PARAMETRELER._RAPOR_KODU + "','RAPOR FARKLI KAYDET','" + RUN_DATE.ToString("yyyy.MM.dd") + "','" + RUN_DATE.ToString("HH:mm:ss") + "','" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL+ "')";
                    SqlCommand command = new SqlCommand(SQL, conn);
                    conn.Open();
                    command.ExecuteReader(CommandBehavior.CloseConnection);
                    conn.Close();
                } 
                if (BR_RAPOR_KODU.Caption.Length > 0)
                { 
                    _MENU_BACK frm = new _MENU_BACK("FARKLI_KAYDET", BR_RAPOR_KODU.Caption);
                    frm.TXT_FRK_RAPOR_KODU.Text= "Copy " + BR_RAPOR_KODU.Caption;
                    frm._TXT_FILE_NAME= BR_RAPOR_KODU.Caption;
                    frm.ShowDialog();
                    if (frm._BTN_TYPE != "Close")
                    {
                        if (frm._TXT_FILE_NAME.Length > 0)
                        {
                            splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
                            splashScreenManagers.ClosingDelay = 400;
                            splashScreenManagers.ShowWaitForm();
                            splashScreenManagers.SetWaitFormDescription("FARKLI KAYDEDİLİYOR");
                            RaporDesigner._SAVE_AS(frm._TXT_FILE_NAME, frm._TXT_ACIKLAMA, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL, BR_RAPOR_SAVE_PATH.Caption);  
                            splashScreenManagers.SetWaitFormDescription("FARKLI KAYDEDİLDİ");
                            if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                        }
                        else
                        { MessageBox.Show(" Lütfen Rapor Kodu giriniz."); }
                    }
                } 
            }
            else
            { MessageBox.Show(" Lütfen Rapor Ekranı açınız.."); } 
             
        }
        private void BTN_TARIFE_YENIDEN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
            splashScreenManagers.ClosingDelay = 500;
            MyTable = null;
            MyTable = new DataTable("RAPOR_EXPORT");
            splashScreenManagers.ShowWaitForm();
            if (RaporDesigner.TOGGLE_MASTER.IsOn) MASTER_ALL_BASLASIN(); // MASTER_BASLASIN();
            if (RaporDesigner.TOGGLE_WORD.IsOn) KEYWORD_BASLASIN();
            if (RaporDesigner.TOGGLE_TARIFE.IsOn) HESAPLAMA_BASLASIN();

           RAPOR_OLUSTUR();
        }

        private void BTN_VW_BASLAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { 

            splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
            splashScreenManagers.ClosingDelay = 500; 
            ///
            /// RAPOR PARAMETRELERINI TEMİZLE
            /// 

          //  KIRILIM = KIRILIM_CAST = KIRILIM_FIELD = string.Empty;
            OZEL_TANIMLAMA = OZEL_TANIMLAMA_CAST = OZEL_TANIMLAMA_FIELD = OZEL_TABLE_CREATE = string.Empty;
       
            BASLIK = BASLIK_FIELD =   string.Empty;
            OLCUM = OLCUM_CAST = OLCUM_FIELD = OLCUM_TABLE_CREATE = string.Empty;
            FILITRE = FILITRE_CAST = FILITRE_FIELD = FILITRE_TABLE_CREATE = string.Empty;

            COLUMS_COUNT = 0;

            OZEL_TANIMLAMA = string.Empty;
            TABLE_CREATE_FIELD_NAME = string.Empty;
            TABLE_CREATE_INSERT_QUERY = string.Empty;
            SABITLER_SELECT_NAME =    string.Empty;

            MyTable = null;
            MyTable = new DataTable("RAPOR_EXPORT");
            QUERY = string.Empty;
            string QUERY_EACH = string.Empty;
         
            TMP_QUERY_TELEVIZYON = TMP_QUERY_GAZETE = TMP_QUERY_DERGI = TMP_QUERY_SINEMA = TMP_QUERY_RADYO = TMP_QUERY_OUTDOOR = TMP_QUERY_INTERNET = string.Empty; 
            _CHANNEL = string.Empty;

            ////
            //// RAPORLANACAK ALANLARI SEÇ
            ////
            if (RaporViewer != null)
            {
                if (!RaporViewer.TOOGLE_TELEVIZYON.IsOn && !RaporViewer.TOOGLE_GAZETE.IsOn && !RaporViewer.TOOGLE_DERGI.IsOn && !RaporViewer.TOOGLE_OUTDOOR.IsOn && !RaporViewer.TOOGLE_SINEMA.IsOn && !RaporViewer.TOOGLE_RADYO.IsOn && !RaporViewer.TOOGLE_INTERNET.IsOn)
                {
                    MessageBox.Show("Mecra Türü Seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return;
                }
                if (RaporViewer.gridView_BASLIKLAR.RowCount < 1)
                {
                    MessageBox.Show("Rapor Başlıkları seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return;
                }
                if (RaporViewer.gridView_OLCUMLEME.RowCount < 1)
                {
                    MessageBox.Show("Ölçümlerden bir alan seçiniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Question); return;
                }
                if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
                splashScreenManagers.ShowWaitForm(); 

                SqlConnection myCon = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                myCon.Open();
                ///
                /// Kırılımlı Rapor İçin Node Kontrol
                ///   
                if (RaporViewer.treeList_KIRILIMLAR.Nodes.Count != 0)
                {
                    List<TreeListNode> nds = RaporViewer.treeList_KIRILIMLAR.GetNodeList();
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
                            KIRILIM_NAME.Add(string.Format("[KIRILIM_{0}],", i)); 
                        }
                    }
                }

                /////
                ///// Seçilen Başlıkları Node Kontrol
                /////   
                //for (int iX = 0; iX <= RaporViewer.gridView_BASLIKLAR.RowCount; iX++)
                //{
                //    DataRow DR = RaporViewer.gridView_BASLIKLAR.GetDataRow(iX);
                //    if (DR != null) BASLIKLAR += string.Format("[{0}],", DR["TEXT"]);
                //}
                /////
                ///// Ölçümler Node Kontrol
                /////   
                //for (int iX = 0; iX <= RaporViewer.gridView_OLCUMLEME.RowCount; iX++)
                //{
                //    DataRow DR = RaporViewer.gridView_OLCUMLEME.GetDataRow(iX);
                //    if (DR != null)  BASLIKLAR += string.Format("[{0}],", DR["TEXT"]); 
                //} 
                string PATH_KIRILIMLAR = "";

                if (RaporViewer.treeList_KIRILIMLAR.Nodes.Count != 0)
                {
                    List<TreeListNode> ndsc = RaporViewer.treeList_KIRILIMLAR.GetNodeList();
                    foreach (TreeListNode nodes in ndsc)
                    {
                        if (nodes.Checked)
                        {
                            KIRILIM_CAST.Clear();
                            PATH_KIRILIMLAR = GetFullPath(nodes, "\\");
                            PATH_KIRILIMLAR = PATH_KIRILIMLAR.Substring(1, PATH_KIRILIMLAR.Length - 1);
                            //string[] wordm = PATH_KIRILIMLAR.Split('\\');
                            //if (COLUMS_COUNT < wordm.Length) COLUMS_COUNT = wordm.Length;
                            //if (COLUMS_COUNT > 0)
                            //{
                            //    int NodeSay = 0;
                            //    for (int i = 1; i <= COLUMS_COUNT; i++)
                            //    {
                            //        KIRILIM_CAST += string.Format(" CAST('{0}' AS nvarchar ) AS  [KIRILIM_{1}],", wordm[NodeSay], i);
                            //        if (NodeSay < wordm.Length - 1) NodeSay++;
                            //    }
                            //} 
                            if (RaporViewer.treeList_FILITRELER.Nodes.Count != 0)
                            {
                                TreeListNode myNode = RaporViewer.treeList_FILITRELER.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
                                if (myNode != null)
                                {
                                    ArrayList selectedNodes = new ArrayList();
                                    selectChildren(myNode, selectedNodes);
                                    //RaporViewer.treeList_FILITRELER.OptionsSelection.MultiSelect = true;
                                    //RaporViewer.treeList_FILITRELER.Selection.Set(selectedNodes); 
                                    foreach (TreeListNode item in selectedNodes)
                                    {
                                            if (item.GetValue(1).ToString().IndexOf("KIRILIM#") != -1)
                                            {
                                                TreeListNode arrs = RaporViewer.treeList_KIRILIMLAR.FindNode((node) => { return node["PATH"].ToString() == string.Format("KIRILIM#{0}", PATH_KIRILIMLAR); });
                                                if (arrs != null)
                                                {   if (arrs.Checked)
                                                    { 
                                                        if (QUERY != "") { if (QUERY.Length > 0) { QUERY = string.Format("({0}) and ", QUERY.Substring(0, QUERY.Length - 4)); } } 
                                                    } 
                                                }
                                            }

                                           if (item.GetValue("TEXT").ToString().IndexOf("(Dahil)") != -1) DAHIL_HARIC = "Dahil";
                                           if (item.GetValue("TEXT").ToString().IndexOf("(Hariç)") != -1) DAHIL_HARIC = "Hariç";
                                           if (item.GetValue("TYPE").ToString() == "BASLIK") 
                                            {
                                                if (QUERY_EACH != "") { if (QUERY_EACH.Length > 0) { QUERY_EACH = string.Format("({0}) and ", QUERY_EACH.Substring(0, QUERY_EACH.Length - 4)); } }
                                                if (QUERY_EACH != "") QUERY += QUERY_EACH;
                                                    QUERY_EACH   = ""; 
                                                  BASLIK_KONTROL = string.Format("[{0}]", item.GetValue("TEXT").ToString().Replace("(Dahil)", "").Replace(" (Hariç)", ""));         
                                            }

                                            if (item.GetValue("TYPE").ToString() == "SATIR") 
                                            {
                                                if (DAHIL_HARIC == "Dahil")
                                                {
                                                    if (item.GetValue(1).ToString().IndexOf("%") != -1)
                                                    { QUERY_EACH += string.Format("{0} LIKE '{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT")); }
                                                    else
                                                    { QUERY_EACH += string.Format("{0}='{1}' OR ", BASLIK_KONTROL, item.GetValue("TEXT")); }
                                                } 
                                                if (DAHIL_HARIC == "Hariç")
                                                {
                                                    if (item.GetValue(1).ToString().IndexOf("%") != -1)
                                                    { QUERY_EACH += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT")); }                                                       
                                                    else
                                                    { QUERY_EACH += string.Format("{0}<>'{1}' AND ", BASLIK_KONTROL, item.GetValue("TEXT")); }
                                                }
                                            }
                                       
                                    }
                                    if (QUERY_EACH != "") { if (QUERY_EACH.Length > 0) { QUERY_EACH = string.Format("({0}) and ", QUERY_EACH.Substring(0, QUERY_EACH.Length - 4)); } }
                                    if (QUERY_EACH != "") QUERY += QUERY_EACH;
                                    QUERY_EACH = ""; 
                                }
                            }
                        }
                    }
                } 
                splashScreenManagers.SetWaitFormDescription("Lütfen Bekleyiniz.");
                //if (RaporViewer.TOGGLE_MASTER.IsOn) MASTER_ALL_BASLASIN();// MASTER_BASLASIN();
                //if (RaporViewer.TOGGLE_WORD.IsOn) KEYWORD_BASLASIN();
                //if (RaporViewer.TOGGLE_TARIFE.IsOn) HESAPLAMA_BASLASIN(); 


                //for (int iX = 0; iX <= KIRILIM.RowCount; iX++)
                //{
                //    DataRow DR = RaporViewer.gridView_BASLIKLAR.GetDataRow(iX);
                //    if (DR != null) BASLIKLAR += string.Format("[{0}],", DR["TEXT"]);
                //}
                /////
                ///// Ölçümler Node Kontrol
                /////   
                //for (int iX = 0; iX <= RaporViewer.gridView_OLCUMLEME.RowCount; iX++)
                //{
                //    DataRow DR = RaporViewer.gridView_OLCUMLEME.GetDataRow(iX);
                //    if (DR != null) BASLIKLAR += string.Format("[{0}],", DR["TEXT"]);
                //}


                //VIEW_RAPOR_OLUSTUR(KIRILIM + BASLIKLAR_LISTESI); 
                if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm(); 
            }
            else
            { MessageBox.Show(" Lütfen Rapor seçiniz."); }  
        }

        private void BTN_AYARLAR_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { 
            _MENU_BACK tr = new _MENU_BACK("AYARLAR","");
            tr.ShowDialog();
            if (tr._BTN_TYPE == "Tamam")
            {
                splashScreenManagers = new DevExpress.XtraSplashScreen.SplashScreenManager(this, typeof(global::CoreV2.waitforms), true, true);
                splashScreenManagers.ClosingDelay = 300;
                splashScreenManagers.ShowWaitForm();
                splashScreenManagers.SetWaitFormDescription("Rapor ayarları güncelleniyor.");
                if (tr.SELECT_RAPOR_DURUMU == "SABIT")
                {
                    BR_RAPOR_TYPE.Caption = "SABIT";
                    _GLOBAL_PARAMETRELER._SABIT_TABLO_ADI = string.Format("{0}_LNK_{1}", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, tr.SELECT_RAPOR_KODU);
                }
                RaporDesigner.Text = tr.SELECT_RAPOR_KODU;
                BR_RAPOR_KODU.Caption = tr.SELECT_RAPOR_KODU;
                BR_RAPOR_ID.Caption = tr.SELECT_ID;
                BR_RAPOR_TYPE.Caption = tr.SELECT_RAPOR_DURUMU;
                BR_RAPOR_SAVE_PATH.Caption = tr.SELECT_RAPOR_PATH;
                BR_RAPOR_AUTO_SAVE.EditValue = tr.SELECT_RAPOR_AUTO_SAVE;
                BR_RAPOR_ACIKLAMA.EditValue = tr.SELECT_RAPOR_ACIKLAMASI; 
                _GLOBAL_PARAMETRELER._RAPOR_KODU = tr.SELECT_RAPOR_KODU; 

                splashScreenManagers.SetWaitFormDescription("Rapor ekrana yüklendi.");
                if (splashScreenManagers.IsSplashFormVisible) splashScreenManagers.CloseWaitForm();
            }
        }

        private void MASTER_RIBBON_ApplicationButtonClick(object sender, EventArgs e)
        {
            RAPOR_LIST_LOAD();  // string a = e.ToString();
           
        }   
    }
}
