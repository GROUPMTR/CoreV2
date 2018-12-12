using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoreV2.TARIFELER
{
    public partial class PROGRAM : DevExpress.XtraEditors.XtraForm
    {
        TARIFELER._GLOBAL_TARIFELER GLOBALTARIFELER = new TARIFELER._GLOBAL_TARIFELER();
        public DataView dv = null;
        public DataView dvLIST = null;
        public PROGRAM()
        {
            InitializeComponent();

            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet ds = new DataSet();
                SqlDataAdapter adapter = new SqlDataAdapter(" SELECT   top 0  * FROM         dbo.TRF_PROGRAM_TARIFESI ", conn);
                adapter.Fill(ds, "dbo_ANA_YAYIN");
                DataViewManager dvManager = new DataViewManager(ds);
                dvLIST = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_SABITLER.DataSource = dvLIST;
            }
            gridView_SABITLER.ShowFindPanel();

            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {

                SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter(" SELECT   top 0  * FROM         dbo.TRF_PROGRAM_TARIFESI ", myConnections);
                DataSet ds = new DataSet();
                MySqlDataAdapter.Fill(ds, "dbo_SELECTANA_YAYIN");
                DataViewManager dvManager = new DataViewManager(ds);
                dv = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_SECENEKLER.DataSource = dv;
            }

            COMBO_HESAPLAMA_TURU.Properties.Items.Clear();
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                myConnection.Open();
                string SQL_TELEVIZYON = " SELECT     MECRA_TURU, HESAPLAMA_TURU FROM         dbo.ADM_HESAPLAMA_TURLERI WHERE     (MECRA_TURU = N'TELEVIZYON')";
                using (SqlCommand myCommand = new SqlCommand(SQL_TELEVIZYON, myConnection))
                {
                    SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (myReader.Read())
                    {
                        COMBO_HESAPLAMA_TURU.Properties.Items.Add(myReader["HESAPLAMA_TURU"].ToString());
                    }
                }
            }

            CMB_ANA_YAYIN.Properties.Items.Clear();
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                myConnection.Open();
                string SQL_ANA_YAYIN = " SELECT     ANA_YAYIN  FROM         dbo._ADEX_INDEX_DATA WHERE     (YAYIN_SINIFI = N'TELEVIZYON') group by ANA_YAYIN  ORDER BY ANA_YAYIN ";
                using (SqlCommand myCommand = new SqlCommand(SQL_ANA_YAYIN, myConnection))
                {
                    SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                    while (myReader.Read())
                    {
                        CMB_ANA_YAYIN.Properties.Items.Add(myReader["ANA_YAYIN"].ToString());
                    }
                }
            }
        }

        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BTN_LISTE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { 
            TARIFELER._TARIFE_LISTESI TF = new TARIFELER._TARIFE_LISTESI("TARIFE", "PROGRAM");
            TF.ShowDialog(); 
            lbFILE_NAME.Caption = TF._TARIFE_KODU;
            lbID.Caption = TF._TARIFE_ID; 
            dv = null; 
            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string query = string.Format("      SELECT  *  FROM         dbo.TRF_PROGRAM_TARIFESI where KULLANICI_KODU='{0}' and  SIRKET_KODU='{1}' and TARIFE_KODU='{2}'  ", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, lbFILE_NAME.Caption);
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                adapter.Fill(ds, "TRF_PROGRAM_TARIFESI");
                DataViewManager dvManager = new DataViewManager(ds);
                dv = dvManager.CreateDataView(ds.Tables[0]); 
            } 
            gridControl_SECENEKLER.DataSource = dv; 
        }

 
        private void SABIT_LISTE()
        {  
            using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                 con.Open(); 
                 string query = " SELECT  ANA_YAYIN, PROGRAM,TIPOLOJI , PG_OZEL,CAST('02:00' AS nvarchar) AS BASLANGIC_SAATI, CAST(N'18:29' AS nvarchar) AS BITIS_SAATI, CAST('-' AS nvarchar) AS OPT_PT  FROM  dbo.DATA_TELEVIZYON_ADEX_"+ CMB_YIL.Text + "   WITH (INDEX(IDX_ANA_YAYIN_PROGRAM_TIPOLOJI_PG_OZEL" + CMB_YIL.Text + " )) " +
                                " where  (MONTH(TARIH) = "+ CMB_AY.Text + ")  and  ANA_YAYIN='" + CMB_ANA_YAYIN.Text + "' and  ( PROGRAM LIKE '%" + TXT_METIN.Text + "%' OR TIPOLOJI LIKE '%" + TXT_METIN.Text + "%' )  GROUP BY  ANA_YAYIN, PROGRAM,TIPOLOJI,PG_OZEL "; 
                        using (SqlCommand myCommand = new SqlCommand(query, con))
                        {
                          SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                          while (myReader.Read())
                          {
                            DataRowView rowView = dvLIST.AddNew();
                            rowView["MECRA_TURU"] = "PROGRAM";
                            rowView["MECRA_KODU"] = myReader["ANA_YAYIN"].ToString();
                            rowView["PROGRAM"] = myReader["PROGRAM"].ToString();
                            rowView["PG_OZEL"] = myReader["PG_OZEL"].ToString();
                            rowView["TIPOLOJI"] = myReader["TIPOLOJI"].ToString();
                            rowView["BASLANGIC_SAATI"] = "02:00";
                            rowView["BITIS_SAATI"] = "18:29";
                            rowView["OPT_PT"] = "-";
                            rowView.EndEdit();

                            DataRowView row = dvLIST.AddNew();
                            row["MECRA_TURU"] = "PROGRAM";
                            row["MECRA_KODU"] = myReader["ANA_YAYIN"].ToString();
                            row["PROGRAM"] = myReader["PROGRAM"].ToString();
                            row["PG_OZEL"] = myReader["PG_OZEL"].ToString();
                            row["TIPOLOJI"] = myReader["TIPOLOJI"].ToString();
                            row["BASLANGIC_SAATI"] = "18:30";
                            row["BITIS_SAATI"] = "25:59";
                            row["OPT_PT"] = "+";
                            row.EndEdit();
                        }
                      }
             }
        }
        private void BTN_YENI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SABIT_LISTE();
        }

        private void MN_TUMUNU_SEC_Click(object sender, EventArgs e)
        {
            gridView_SABITLER.SelectAll();
        }

        private void MN_EKLE_Click(object sender, EventArgs e)
        {
            gridView_SABITLER.ClearSorting();
            if (COMBO_HESAPLAMA_TURU.Text == "") { MessageBox.Show("Hesaplama Türü Boş geçilemez"); return; }
            if (TXT_BIRIM_FIYAT.Text == "") { MessageBox.Show("Birim fiyat Boş geçilemez"); return; }


            int[] GETROW = gridView_SABITLER.GetSelectedRows();
            for (int i = 0; i < GETROW.Length; i++)
            {
                DataRow dr = gridView_SABITLER.GetDataRow(Convert.ToInt32(GETROW[i]));
                DataRowView rowView = dv.AddNew();  
                rowView["MECRA_TURU"] = "PROGRAM"; 
                rowView["MECRA_KODU"] = dr["MECRA_KODU"];
                rowView["PROGRAM"] = dr["PROGRAM"];
                rowView["PG_OZEL"] = dr["PG_OZEL"]; 
                rowView["TIPOLOJI"] = dr["TIPOLOJI"];
                rowView["BASLANGIC_SAATI"] = dr["BASLANGIC_SAATI"];
                rowView["BITIS_SAATI"] = dr["BITIS_SAATI"];
                rowView["OPT_PT"] = dr["OPT_PT"]; 
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
            if (lbID.Caption != "")
            {
                GLOBALTARIFELER.GUNCELLE(lbFILE_NAME.Caption, lbID.Caption, "PROGRAM","");
            }
            if (lbID.Caption == "")
            {
                TARIFELER._TARIFE_KAYDET frm = new TARIFELER._TARIFE_KAYDET();
                frm.ShowDialog();
                lbFILE_NAME.Caption = frm._TXT_FILE_NAME.ToString();
                if (lbFILE_NAME.Caption.ToString() != "") { lbID.Caption = GLOBALTARIFELER.KAYDET(lbFILE_NAME.Caption, "PROGRAM", "", false,"").ToString (); } else { MessageBox.Show("Tarife kodu giriniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }
            if (dv != null)
            {
                // SATIR SİL 
                dv.RowStateFilter = DataViewRowState.Deleted;
                GLOBALTARIFELER.PROGRAM_ROW_DELETE(dv, lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                // SATIR EKLE
                dv.RowStateFilter = DataViewRowState.Added;
                GLOBALTARIFELER.PROGRAM_ROW_ADD(dv, lbFILE_NAME.Caption.ToString(),lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                // SATIR GUNCELLE
                dv.RowStateFilter = DataViewRowState.ModifiedOriginal;
                GLOBALTARIFELER.PROGRAM_ROW_UPDATE(dv, lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                DATA_READ(lbFILE_NAME.Caption.ToString());
                //dv.RowStateFilter = DataViewRowState.CurrentRows ;
                //dv.Table.AcceptChanges();
            }
        }


        private void DATA_READ(string TARIFE_KODU)
        {
            SqlConnection MySqlConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter("SELECT  * FROM  dbo.TRF_PROGRAM_TARIFESI  WHERE   TARIFE_KODU='" + TARIFE_KODU + "' and SIRKET_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "'", MySqlConnection);
            DataSet MyDataSet = new DataSet();
            MySqlDataAdapter.Fill(MyDataSet, "TRF_PROGRAM_TARIFESI");
            DataViewManager dvManager = new DataViewManager(MyDataSet);
            dv = dvManager.CreateDataView(MyDataSet.Tables[0]);
            gridControl_SECENEKLER.DataSource = dv;
        }

        private void rdBtn_PROGRAM_TIPOLOJI_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
        }

        private void rdBtn_PROGRAM_TIPOLOJI_VE_PG_OZEL_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
        }

        private void BTN_TAMAM_Click(object sender, EventArgs e)
        {
            SABIT_LISTE();
        }
    }
}