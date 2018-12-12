using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoreV2.TARIFELER
{
    public partial class NONE_GRP : DevExpress.XtraEditors.XtraForm
    {
        TARIFELER._GLOBAL_TARIFELER GLOBALTARIFELER = new TARIFELER._GLOBAL_TARIFELER();
        public DataView dv = null;
        public NONE_GRP()
        {
            InitializeComponent();
            SABIT_LISTE();
            HEDEFKITLE_LISTESI();

        } 
        private void BTN_YENI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SABIT_LISTE();
            HEDEFKITLE_LISTESI();
        }


        private void SABIT_LISTE()
        {
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet ds = new DataSet();

                string query = "";
                if (rdBtn_MIX.Checked)
                {
                    query = "  SELECT     MNM, ANA_YAYIN, CAST('02:00' AS nvarchar) AS BASLANGIC_SAATI, CAST(N'18:29' AS nvarchar) AS BITIS_SAATI, CAST('-' AS nvarchar) AS OPT_PT " +
                 "  FROM         dbo._ADEX_INDEX_DATA  " +
                 "  WHERE     (YAYIN_SINIFI = N'TELEVIZYON') " +
                 "  GROUP BY MNM, ANA_YAYIN " +
                 "  UNION " +
                 "  SELECT      MNM, ANA_YAYIN, CAST('18:30' AS nvarchar) AS BASLANGIC_SAATI, CAST(N'25:59' AS nvarchar) AS BITIS_SAATI, CAST('+' AS nvarchar) AS OPT_PT " +
                 "  FROM         dbo._ADEX_INDEX_DATA " +
                 "  WHERE     (YAYIN_SINIFI = N'TELEVIZYON') " +
                 "  GROUP BY MNM, ANA_YAYIN " +
                 "  ORDER BY ANA_YAYIN ";

                }

                if (rdBtn_OPT.Checked)
                {

                    query = "  SELECT     MNM, ANA_YAYIN, CAST('02:00' AS nvarchar) AS BASLANGIC_SAATI, CAST(N'18:29' AS nvarchar) AS BITIS_SAATI, CAST('-' AS nvarchar) AS OPT_PT " +
              "  FROM         dbo._ADEX_INDEX_DATA  " +
              "  WHERE     (YAYIN_SINIFI = N'TELEVIZYON') " +
              "  GROUP BY MNM, ANA_YAYIN " +
              "  ORDER BY ANA_YAYIN ";


                }

                if (rdBtn_PT.Checked)
                {

                    query =
                  "  SELECT      MNM, ANA_YAYIN, CAST('18:30' AS nvarchar) AS BASLANGIC_SAATI, CAST(N'25:59' AS nvarchar) AS BITIS_SAATI, CAST('+' AS nvarchar) AS OPT_PT " +
                  "  FROM         dbo._ADEX_INDEX_DATA " +
                  "  WHERE     (YAYIN_SINIFI = N'TELEVIZYON') " +
                  "  GROUP BY MNM, ANA_YAYIN " +
                  "  ORDER BY ANA_YAYIN ";


                }

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
                string SQL = "   SELECT   top 0  * FROM         dbo.TRF_TELEVIZYON_NONE_MEASURED   ";
                SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter(SQL, myConnections);
                DataSet ds = new DataSet();
                MySqlDataAdapter.Fill(ds, "dbo_SELECTANA_YAYIN");
                DataViewManager dvManager = new DataViewManager(ds);
                dv = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_SECENEKLER.DataSource = dv;
            }
        }


        private void DATA_READ(string TARIFE_KODU)
        {
            SqlConnection MySqlConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter("SELECT  * FROM  dbo.TRF_TELEVIZYON_NONE_MEASURED  WHERE   TARIFE_KODU='" + TARIFE_KODU + "' and SIRKET_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "'", MySqlConnection);
            DataSet MyDataSet = new DataSet();
            MySqlDataAdapter.Fill(MyDataSet, "TRF_TELEVIZYON");
            DataViewManager dvManager = new DataViewManager(MyDataSet);
            dv = dvManager.CreateDataView(MyDataSet.Tables[0]);
            gridControl_SECENEKLER.DataSource = dv;
        }

        private void HEDEFKITLE_LISTESI()
        {
         //   COMBO_TARGET_UPDATE.Properties.Items.Clear();
            COMBO_TARGET.Properties.Items.Clear();
            re_TARGET.Items.Clear();
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = " SELECT     FIELDS, SECENEK FROM         dbo.ADM_SECENEKLER WHERE     (BASLIK = N'HEDEFKİTLELER')";
                SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReader.Read())
                {

                    re_TARGET.Items.Add(myReader["SECENEK"].ToString());
                    COMBO_TARGET.Properties.Items.Add(myReader["SECENEK"].ToString());
                 //   COMBO_TARGET_UPDATE.Properties.Items.Add(myReader["SECENEK"].ToString());
                }
            }
        }


        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BTN_LISTE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           

            TARIFELER._TARIFE_LISTESI TF = new TARIFELER._TARIFE_LISTESI("TARIFE", "NONEGRP");
            TF.ShowDialog();

            lbFILE_NAME.Caption = TF._TARIFE_KODU;
            lbID.Caption = TF._TARIFE_ID;

            dv = null;

            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string query = string.Format("      SELECT  *  FROM         dbo.TRF_TELEVIZYON_NONE_MEASURED where KULLANICI_KODU='{0}' and  SIRKET_KODU='{1}' and TARIFE_KODU='{2}'  ", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, lbFILE_NAME.Caption);
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                adapter.Fill(ds, "TRF_TELEVIZYON_NONE_MEASURED");
                DataViewManager dvManager = new DataViewManager(ds);
                dv = dvManager.CreateDataView(ds.Tables[0]);

            }

            gridControl_SECENEKLER.DataSource = dv;


        }

        private void MN_TUMUNU_SEC_Click(object sender, EventArgs e)
        {
            gridView_SABITLER.SelectAll();
        }

        private void MN_EKLE_Click(object sender, EventArgs e)
        {
            gridView_SECENEKLER.ClearSorting();
            if (COMBO_TARGET.Text == "") { MessageBox.Show("HedefKitle Boş geçilemez"); return; }

            int[] GETROW = gridView_SABITLER.GetSelectedRows();
            for (int i = 0; i < GETROW.Length; i++)
            {
                DataRow dr = gridView_SABITLER.GetDataRow(Convert.ToInt32(GETROW[i]));
                DataRowView rowView = dv.AddNew();


                rowView["MECRA_TURU"] = "NONEGRP";
                rowView["MNM"] = dr["MNM"];
                rowView["MECRA_KODU"] = dr["ANA_YAYIN"];

                rowView["TARGET"] = COMBO_TARGET.Text;
                rowView["GRP"] = TXT_GRP.Text;
                rowView["BASLANGIC_SAATI"] = dr["BASLANGIC_SAATI"];
                rowView["BITIS_SAATI"] = dr["BITIS_SAATI"];
                rowView["OPT_PT"] = dr["OPT_PT"];


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
                GLOBALTARIFELER.GUNCELLE(lbFILE_NAME.Caption, lbID.Caption, "NONEGRP","");
            }
            if (lbID.Caption == "")
            {
                TARIFELER._TARIFE_KAYDET frm = new TARIFELER._TARIFE_KAYDET();
                frm.ShowDialog();
                lbFILE_NAME.Caption = frm._TXT_FILE_NAME.ToString();
                if (lbFILE_NAME.Caption.ToString() != "") 
                { lbID.Caption = GLOBALTARIFELER.KAYDET(lbFILE_NAME.Caption, "NONEGRP", "", false,"").ToString(); } else { MessageBox.Show("Tarife kodu giriniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            if (dv != null)
            {
                // SATIR SİL 
                dv.RowStateFilter = DataViewRowState.Deleted;
                GLOBALTARIFELER.TELEVIZYON_NONE_MEASURED_ROW_DELETE(dv, lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                // SATIR EKLE
                dv.RowStateFilter = DataViewRowState.Added;
                GLOBALTARIFELER.TELEVIZYON_NONE_MEASURED_ROW_ADD(dv, lbFILE_NAME.Caption.ToString(), lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                // SATIR GUNCELLE
                dv.RowStateFilter = DataViewRowState.ModifiedOriginal;
                GLOBALTARIFELER.TELEVIZYON_NONE_MEASURED_ROW_UPDATE(dv, lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                DATA_READ(lbFILE_NAME.Caption.ToString());
                //dv.RowStateFilter = DataViewRowState.CurrentRows ;
                //dv.Table.AcceptChanges();
            }
        }

      
    }
}