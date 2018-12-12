using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoreV2.TARIFELER
{
    public partial class DERGI : DevExpress.XtraEditors.XtraForm
    {
        TARIFELER._GLOBAL_TARIFELER GLOBALTARIFELER = new TARIFELER._GLOBAL_TARIFELER();
        public DataView dv = null;
        public DERGI()
        {
            InitializeComponent();
            SABIT_LISTE();
        }

        private void SABIT_LISTE()
        {
            if (rdBtn_MECRA_BAZINDA.Checked)
            {
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    DataSet ds = new DataSet();
                    string query = "      SELECT     ANA_YAYIN FROM         dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, ANA_YAYIN  HAVING      (YAYIN_SINIFI = N'DERGI') ORDER BY ANA_YAYIN ";
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(query, conn);
                    adapter.Fill(ds, "dbo_ANA_YAYIN");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DataView dvLIST = dvManager.CreateDataView(ds.Tables[0]);
                    gridControl_SABITLER.DataSource = dvLIST;
                }
            }

            if (rdBtn_MECRA_VE_TARIFE_MADDE.Checked)
            {
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    DataSet ds = new DataSet();
                    string query = "      SELECT     ANA_YAYIN,TARIFE_MADDE FROM         dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, ANA_YAYIN,TARIFE_MADDE  HAVING      (YAYIN_SINIFI = N'DERGI') ORDER BY ANA_YAYIN ";
                    SqlDataAdapter adapter = new SqlDataAdapter();
                    adapter.SelectCommand = new SqlCommand(query, conn);
                    adapter.Fill(ds, "dbo_ANA_YAYIN");
                    DataViewManager dvManager = new DataViewManager(ds);
                    DataView dvLIST = dvManager.CreateDataView(ds.Tables[0]);
                    gridControl_SABITLER.DataSource = dvLIST;
                }
            }


            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = "   SELECT   top 0  * FROM         dbo.TRF_DERGI ";
                SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter(SQL, myConnections);
                DataSet ds = new DataSet();
                MySqlDataAdapter.Fill(ds, "TRF_DERGI");
                DataViewManager dvManager = new DataViewManager(ds);
                dv = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_SECENEKLER.DataSource = dv;
            }
        }

        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BTN_YENI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SABIT_LISTE();
         
        }

        private void BTN_LISTE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
 


            TARIFELER._TARIFE_LISTESI TF = new TARIFELER._TARIFE_LISTESI("TARIFE", "DERGI");
            TF.ShowDialog();
 
            lbFILE_NAME.Caption = TF._TARIFE_KODU;
            lbID.Caption = TF._TARIFE_ID;

            dv = null;

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string query = string.Format("      SELECT  *  FROM         dbo.TRF_DERGI where KULLANICI_KODU='{0}' and  SIRKET_KODU='{1}' and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, lbFILE_NAME.Caption);
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                adapter.Fill(ds, "TRF_DERGI");
                DataViewManager dvManager = new DataViewManager(ds);
                dv = dvManager.CreateDataView(ds.Tables[0]);
            }

            gridControl_SECENEKLER.DataSource = dv;
      




            //TARIFE_KODU = TF._TARIFE_KODU;
            //TARIFE_ID = TF._TARIFE_ID;
            //TARIFE_CHANGE(TF._MECRA_TURU);
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
                rowView["MECRA_KODU"] = dr["ANA_YAYIN"];
                rowView["HESAPLANMA_TURU"] = COMBO_HESAPLAMA_TURU.Text;
                rowView["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.Text;   
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
            string SELECT_COLUMS = "MEDYA";

            if (RDBTN_MEDYA.Checked) SELECT_COLUMS = "MEDYA";
            if (RDBTN_ANAYAYIN.Checked) SELECT_COLUMS = "ANA_YAYIN";

            if (lbID.Caption != "")
            { 

                GLOBALTARIFELER.GUNCELLE(lbFILE_NAME.Caption, lbID.Caption, "DERGI", SELECT_COLUMS);
            }
            if (lbID.Caption == "")
            {
                TARIFELER._TARIFE_KAYDET frm = new TARIFELER._TARIFE_KAYDET();
                frm.ShowDialog();
                lbFILE_NAME.Caption = frm._TXT_FILE_NAME.ToString();
                if (lbFILE_NAME.Caption.ToString() != "")
                { lbID.Caption = GLOBALTARIFELER.KAYDET(lbFILE_NAME.Caption, "DERGI", "", false, SELECT_COLUMS).ToString(); }
                else { MessageBox.Show("Tarife kodu giriniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }

            if (dv != null)
            {
                // SATIR SİL
                dv.RowStateFilter = DataViewRowState.Deleted;
                GLOBALTARIFELER.DERGI_ROW_DELETE(dv, lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                // SATIR EKLE
                dv.RowStateFilter = DataViewRowState.Added;
                GLOBALTARIFELER.DERGI_ROW_ADD(dv, lbFILE_NAME.Caption.ToString(), lbID.Caption,_GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                // SATIR GUNCELLE
                dv.RowStateFilter = DataViewRowState.ModifiedOriginal;
                GLOBALTARIFELER.DERGI_ROW_UPDATE(dv, lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                DATA_READ(lbFILE_NAME.Caption.ToString());
            }
        }

        private void DATA_READ(string _TARIFE_KODU)
        {
            SqlConnection MySqlConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter("SELECT  * FROM  dbo.TRF_DERGI  WHERE   TARIFE_KODU='" + _TARIFE_KODU + "' and SIRKET_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "'", MySqlConnection);
            DataSet MyDataSet = new DataSet();
            MySqlDataAdapter.Fill(MyDataSet, "dbo_TarifeDergi");
            DataViewManager dvManager = new DataViewManager(MyDataSet);
            dv = dvManager.CreateDataView(MyDataSet.Tables[0]);
            gridControl_SECENEKLER.DataSource = dv;
            MySqlConnection.Close();
        }

        private void rdBtn_MECRA_BAZINDA_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
        }

        private void rdBtn_MECRA_VE_TARIFE_MADDE_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
        }

        private void RDBTN_ANAYAYIN_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
        }

        private void RDBTN_MEDYA_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
        }
    } 
}