using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoreV2.TARIFELER
{
    public partial class OUTDOOR : DevExpress.XtraEditors.XtraForm
    {
        TARIFELER._GLOBAL_TARIFELER GLOBALTARIFELER = new TARIFELER._GLOBAL_TARIFELER();
        public DataView dv = null;
        public OUTDOOR()
        {
            InitializeComponent();
            SABIT_LISTE();

        }

        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }


        private void SABIT_LISTE()
        {
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet ds = new DataSet();
                string query = "      SELECT   MEDYA,ILI,UNITE,ADRES,GENISLIK,YUKSEKLIK,TOPLAM_ALAN,FREKANS,GUN_SAYISI FROM  dbo.DATA_OUTDOOR_ADEX GROUP BY YAYIN_SINIFI, MEDYA,ILI,UNITE,ADRES,GENISLIK,YUKSEKLIK,TOPLAM_ALAN,FREKANS,GUN_SAYISI  ORDER BY MEDYA ";
                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, conn);
                adapter.Fill(ds, "dbo_ANA_YAYIN");
                DataViewManager dvManager = new DataViewManager(ds);
                DataView dvLIST = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_SABITLER.DataSource = dvLIST;
            }
            gridView_SABITLER.ShowFindPanel();
            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = "   SELECT   top 0 * FROM  dbo.TRF_OUTDOOR ";
                SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter(SQL, myConnections);
                DataSet ds = new DataSet();
                MySqlDataAdapter.Fill(ds, "dbo_SELECTANA_YAYIN");
                DataViewManager dvManager = new DataViewManager(ds);
                dv = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_SECENEKLER.DataSource = dv;
            }
        }
        private void DATA_READ(string _TARIFE_KODU)
        {
            SqlConnection MySqlConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter("SELECT  * FROM  dbo.TRF_OUTDOOR  WHERE     TARIFE_KODU='" + _TARIFE_KODU + "' and SIRKET_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "'", MySqlConnection);
            DataSet MyDataSet = new DataSet();
            MySqlDataAdapter.Fill(MyDataSet, "TRF_OUTDOOR");
            DataViewManager dvManager = new DataViewManager(MyDataSet);
            dv = dvManager.CreateDataView(MyDataSet.Tables[0]);
            gridControl_SECENEKLER.DataSource = dv;
        }
        private void BTN_YENI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SABIT_LISTE();
        }

        private void BTN_LISTE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
      

            TARIFELER._TARIFE_LISTESI TF = new TARIFELER._TARIFE_LISTESI("TARIFE", "OUTDOOR");
            TF.ShowDialog();

            lbFILE_NAME.Caption = TF._TARIFE_KODU;
            lbID.Caption = TF._TARIFE_ID;

            dv = null;

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string query = string.Format("      SELECT  *  FROM         dbo.TRF_OUTDOOR where KULLANICI_KODU='{0}' and  SIRKET_KODU='{1}' and TARIFE_KODU='{2}'  ", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, lbFILE_NAME.Caption);
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                adapter.Fill(ds, "TRF_OUTDOOR");
                DataViewManager dvManager = new DataViewManager(ds);
                dv = dvManager.CreateDataView(ds.Tables[0]);

            }

            gridControl_SECENEKLER.DataSource = dv;

        }

        private void BTN_EXCEL_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void MN_TUMUNU_SEC_Click(object sender, EventArgs e)
        {
            gridView_SABITLER.SelectAll();
        }

        private void MN_EKLE_Click(object sender, EventArgs e)
        {
            gridView_SECENEKLER.ClearSorting();
            if (COMBO_HESAPLAMA_TURU.Text == "") { MessageBox.Show("Hesaplama Türü Boş geçilemez"); return; }
            if (TXT_BIRIM_FIYAT.Text == "") { MessageBox.Show("Birim fiyat Boş geçilemez"); return; }

            int[] GETROW = gridView_SABITLER.GetSelectedRows();
            for (int i = 0; i < GETROW.Length; i++)
            {
                DataRow dr = gridView_SABITLER.GetDataRow(Convert.ToInt32(GETROW[i]));
                DataRowView rowView = dv.AddNew(); 
                rowView["MECRA_KODU"] = dr["MEDYA"];
                rowView["ILI"] = dr["ILI"];
                rowView["UNITE"] = dr["UNITE"];
                rowView["HESAPLANMA_TURU"] = COMBO_HESAPLAMA_TURU.Text;
                rowView["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.Text;

              //  MEDYA,ILI,UNITE,ADRES,GENISLIK,YUKSEKLIK,TOPLAM_ALAN,FREKANS,GUN_SAYISI


                rowView["ADRES"] = dr["ADRES"];
                rowView["GENISLIK"] = dr["GENISLIK"];
                rowView["YUKSEKLIK"] = dr["YUKSEKLIK"];
           //     rowView["TOPLAM_ALAN"] = dr["TOPLAM_ALAN"];

                rowView.EndEdit();
            }
        }

        private void MN_SIL_Click(object sender, EventArgs e)
        {
            gridView_SECENEKLER.ClearSorting();
            int[] GETROW = gridView_SECENEKLER.GetSelectedRows();
            for (int i = GETROW.Length - 1; i >= 0; i--)
            {
                DataRow drs = gridView_SECENEKLER.GetDataRow(Convert.ToUInt16(GETROW[i]));
                if (drs != null)
                {
                    drs.Delete();
                }
            }
        }

        private void MN_TRG_TUMUNU_SEC_Click(object sender, EventArgs e)
        {
            gridView_SECENEKLER.SelectAll();
        }

        private void BTN_KAYDET_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (lbID.Caption != "")
            {
                GLOBALTARIFELER.GUNCELLE(lbFILE_NAME.Caption, lbID.Caption, "OUTDOOR","");
            }
            if (lbID.Caption == "")
            {
                TARIFELER._TARIFE_KAYDET frm = new TARIFELER._TARIFE_KAYDET();
                frm.ShowDialog();
                lbFILE_NAME.Caption = frm._TXT_FILE_NAME.ToString();
                if (lbFILE_NAME.Caption.ToString() != "") { lbID.Caption = GLOBALTARIFELER.KAYDET(lbFILE_NAME.Caption, "OUTDOOR", "", false,"").ToString(); } else { MessageBox.Show("Tarife kodu giriniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            if (dv != null)
            {
                // SATIR SİL 
                dv.RowStateFilter = DataViewRowState.Deleted;
                GLOBALTARIFELER.OUTDOOR_ROW_DELETE(dv, lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                // SATIR EKLE
                dv.RowStateFilter = DataViewRowState.Added;
                GLOBALTARIFELER.OUTDOOR_ROW_ADD(dv, lbFILE_NAME.Caption.ToString(), lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                // SATIR GUNCELLE
                dv.RowStateFilter = DataViewRowState.ModifiedOriginal;
                GLOBALTARIFELER.OUTDOOR_ROW_UPDATE(dv, lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                DATA_READ(lbFILE_NAME.Caption.ToString());
                //dv.RowStateFilter = DataViewRowState.CurrentRows ;
                //dv.Table.AcceptChanges();
            }
        }

        private void gridControl_SECENEKLER_Click(object sender, EventArgs e)
        {

        }
    }
}