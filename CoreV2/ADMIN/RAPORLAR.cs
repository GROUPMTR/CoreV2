using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoreV2.ADMIN
{
    public partial class RAPORLAR : DevExpress.XtraEditors.XtraForm
    {
        public string SELECT_CODE = "", SELECT_TELEVIZYON = "", SELECT_RADYO = "",
            SELECT_GAZETE = "", SELECT_DERGI = "", SELECT_SINEMA = "", SELECT_OUTDOOR = "", SELECT_INTERNET = "",
            SELECT_SEKTOR = "", SELECT_MECRA_TURLERI = "", SELECT_SABIT_SECENEKLER = "", SELECT_RAPOR_SECENEKLER = "",
            SELECT_MECRALAR = "";
        public bool SELECT_OTUZ_SN_GRP = false, SELECT_ON_BES_DK_RATING = false;
        public RAPORLAR()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            RAPOR_LIST_READ();
        }

        private void RAPOR_LIST_READ()
        {
            //string SQL = " SELECT     dbo.ADM_GRUP_DAHIL_KULLANICILAR.FIRMA, dbo.ADM_GRUP_DAHIL_KULLANICILAR.EMAIL, dbo.ADM_GRUP_HAKLARI.RAPOR_KODU, dbo.ADM_RAPOR_DESIGNE.ACIKLAMA,dbo.ADM_RAPOR_DESIGNE.MECRA_TURLERI, dbo.ADM_RAPOR_DESIGNE.DURUMU " +
            //" FROM         dbo.ADM_GRUP_DAHIL_KULLANICILAR INNER JOIN   dbo.ADM_GRUP_HAKLARI ON dbo.ADM_GRUP_DAHIL_KULLANICILAR.GRUPLAR = dbo.ADM_GRUP_HAKLARI.GRUPLAR INNER JOIN " +
            //" dbo.ADM_RAPOR_DESIGNE ON dbo.ADM_GRUP_HAKLARI.RAPOR_KODU = dbo.ADM_RAPOR_DESIGNE.RAPOR_KODU " +

            //" GROUP BY dbo.ADM_GRUP_DAHIL_KULLANICILAR.FIRMA, dbo.ADM_GRUP_DAHIL_KULLANICILAR.EMAIL, dbo.ADM_GRUP_HAKLARI.RAPOR_KODU, dbo.ADM_GRUP_HAKLARI.FIRMA,  " +
            //" dbo.ADM_RAPOR_DESIGNE.ACIKLAMA, dbo.ADM_RAPOR_DESIGNE.SIRKET_KODU,dbo.ADM_GRUP_HAKLARI.PAYLASIM_TURU,dbo.ADM_RAPOR_DESIGNE.MECRA_TURLERI, dbo.ADM_RAPOR_DESIGNE.DURUMU" +

            //" HAVING      (dbo.ADM_GRUP_DAHIL_KULLANICILAR.EMAIL = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "') AND (dbo.ADM_GRUP_DAHIL_KULLANICILAR.FIRMA = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') AND (dbo.ADM_GRUP_HAKLARI.FIRMA = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "')  " +
            //" AND (dbo.ADM_RAPOR_DESIGNE.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') AND (dbo.ADM_GRUP_HAKLARI.PAYLASIM_TURU = N'RAPOR') " +
            //" UNION  " +
            //" SELECT dbo.ADM_KULLANICI_HAKLARI.FIRMA, dbo.ADM_KULLANICI_HAKLARI.EMAIL, dbo.ADM_RAPOR_DESIGNE.RAPOR_KODU, dbo.ADM_RAPOR_DESIGNE.ACIKLAMA,dbo.ADM_RAPOR_DESIGNE.DURUMU ,dbo.ADM_RAPOR_DESIGNE.MECRA_TURLERI " +
            //" FROM   dbo.ADM_RAPOR_DESIGNE INNER JOIN " +
            //" dbo.ADM_KULLANICI_HAKLARI ON dbo.ADM_RAPOR_DESIGNE.RAPOR_KODU = dbo.ADM_KULLANICI_HAKLARI.RAPOR_KODU " +

            //" GROUP BY dbo.ADM_RAPOR_DESIGNE.ACIKLAMA, dbo.ADM_RAPOR_DESIGNE.SIRKET_KODU, dbo.ADM_KULLANICI_HAKLARI.FIRMA, dbo.ADM_KULLANICI_HAKLARI.EMAIL, dbo.ADM_RAPOR_DESIGNE.RAPOR_KODU,dbo.ADM_KULLANICI_HAKLARI.PAYLASIM_TURU,dbo.ADM_RAPOR_DESIGNE.DURUMU,dbo.ADM_RAPOR_DESIGNE.MECRA_TURLERI " +
            //" HAVING  (dbo.ADM_RAPOR_DESIGNE.SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') AND (dbo.ADM_KULLANICI_HAKLARI.FIRMA = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') AND  (dbo.ADM_KULLANICI_HAKLARI.EMAIL = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "')  AND (dbo.ADM_KULLANICI_HAKLARI.PAYLASIM_TURU = N'RAPOR')       " +
            //" UNION  " +
            string SQL = " SELECT SIRKET_KODU, OWNER_MAIL, RAPOR_KODU, ACIKLAMA,MECRA_TURLERI,DURUMU   FROM   dbo.ADM_RAPOR_DESIGNE  WHERE  (SIRKET_KODU = N'" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "') AND (OWNER_MAIL = N'" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "')  order by RAPOR_KODU ";

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlDataAdapter da = new SqlDataAdapter(SQL, myConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "stock");
            DataViewManager dvManager = new DataViewManager(ds);
            DataView dv = dvManager.CreateDataView(ds.Tables[0]);
            gridControl_LISTE.DataSource = dv;
        }

        private void BR_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BR_TEMP_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult c = MessageBox.Show("Raporu Temp olarak değiştirmek önceki raporunuz silinecektir, eminmisiniz?", "Uyarı",
             MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (c == DialogResult.Yes)
            {
                using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQL = string.Format(" update   dbo.ADM_RAPOR_DESIGNE  set DURUMU='TEMP' WHERE  (RAPOR_KODU='{0}') AND   (SIRKET_KODU = N'{1}')   ", SELECT_CODE, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
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

                this.ParentForm.GetType().InvokeMember("_REFRESH", System.Reflection.BindingFlags.InvokeMethod, null, this.ParentForm, new object[] { "TEMP" });
            
                RAPOR_LIST_READ();
            }
        }

        private void BR_SABITLE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult c = MessageBox.Show("Raporu sabitlemek istediğinize eminmisiniz?", "Uyarı",
             MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (c == DialogResult.Yes)
            {
                using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQL = string.Format(" update   dbo.ADM_RAPOR_DESIGNE set  DURUMU='SABIT'  WHERE   (RAPOR_KODU='{0}') AND   (SIRKET_KODU = N'{1}')   ", SELECT_CODE, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA);
                    SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                    myConnection.Open();
                    myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    myConnection.Close();
                }
                this.ParentForm.GetType().InvokeMember("_REFRESH", System.Reflection.BindingFlags.InvokeMethod, null, this.ParentForm, new object[] { "SABIT" });
                RAPOR_LIST_READ();
            }
        }

        private void gridControl_LISTE_Click(object sender, EventArgs e)
        {
            DataRow DR = gridView_LISTE.GetDataRow(gridView_LISTE.FocusedRowHandle);
            if (DR != null)
            {
                SELECT_CODE = DR["RAPOR_KODU"].ToString();
                BR_SELECT_ROW.Caption = DR["RAPOR_KODU"].ToString();
            }
        }
    }
}