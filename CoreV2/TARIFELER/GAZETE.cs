using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoreV2.TARIFELER
{
    public partial class GAZETE : DevExpress.XtraEditors.XtraForm
    {
        TARIFELER._GLOBAL_TARIFELER GLOBALTARIFELER = new TARIFELER._GLOBAL_TARIFELER();
        public DataView dv = null;

        public string  SELECT_SABIT_SECENEKLER = "", GAZETE_TARIFE_TURU = "STANDART_TARIFE", GAZETE_TARIFE_SECENEK = "MECRA";
        public GAZETE()
        {
            InitializeComponent();
            SABIT_LISTE();
        }

        private void BTN_KAYDET_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            string TARIFE_TURU = "MECRA", GUNLUK = "AYLIK";
            if (rdBtn_MECRA_BAZINDA.Checked) TARIFE_TURU = "MECRA";
            if (rdBtn_MECRA_VE_SAYFA_BAZINDA.Checked) TARIFE_TURU = "MECRA_SAYFA";
            if (rdBtn_MECRA_VE_SAYFA_YAYINTURU.Checked) TARIFE_TURU = "MECRA_SAYFA_YAYINTURU";
            if (CHK_GUNLUK_TARIFE.Checked) GUNLUK = "GUNLUK_TARIFE";

            string SELECT_COLUMS = "MEDYA";

            if (RDBTN_MEDYA.Checked) SELECT_COLUMS = "MEDYA";
            if (RDBTN_ANAYAYIN.Checked) SELECT_COLUMS = "ANA_YAYIN";

            if (lbID.Caption != "")
            {
               

                GLOBALTARIFELER.GUNCELLE(lbFILE_NAME.Caption, lbID.Caption,"GAZETE", SELECT_COLUMS);
            }
            if (lbID.Caption == "")
            {
                TARIFELER._TARIFE_KAYDET frm = new TARIFELER._TARIFE_KAYDET();
                frm.ShowDialog();
                lbFILE_NAME.Caption = frm._TXT_FILE_NAME.ToString();
                if (lbFILE_NAME.Caption.ToString() != "")
                { lbID.Caption = GLOBALTARIFELER.KAYDET(lbFILE_NAME.Caption, "GAZETE", TARIFE_TURU, CHK_GUNLUK_TARIFE.Checked, SELECT_COLUMS).ToString(); }
                else { MessageBox.Show("Tarife kodu giriniz.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information); }
            }

            if (dv != null)
            {
                // SATIR SİL
                dv.RowStateFilter = DataViewRowState.Deleted;
                GLOBALTARIFELER.GAZETE_ROW_DELETE(dv, lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                // SATIR EKLE
                dv.RowStateFilter = DataViewRowState.Added;
                // DataView dv, string TARIFE_KODU,string GAZETE_TARIFE_TURU,string GAZETE_TARIFE_SECENEK
                GLOBALTARIFELER.GAZETE_ROW_ADD(dv, lbFILE_NAME.Caption.ToString(), TARIFE_TURU, GUNLUK, lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
                // SATIR GUNCELLE
                dv.RowStateFilter = DataViewRowState.ModifiedOriginal;
                GLOBALTARIFELER.GAZETE_ROW_UPDATE(dv, lbID.Caption, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, _GLOBAL_PARAMETRELER._KULLANICI_MAIL);

                DATA_READ(lbFILE_NAME.Caption.ToString());
            }


           
        }
        private void DATA_READ(string _TARIFE_KODU)
        {
            SqlConnection MySqlConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter(string.Format("SELECT  * FROM  dbo.TRF_GAZETE  WHERE    TARIFE_KODU='{0}' and SIRKET_KODU='{1}'", _TARIFE_KODU, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA), MySqlConnection);
            DataSet MyDataSet = new DataSet();
            MySqlDataAdapter.Fill(MyDataSet, "TRF_GAZETE");
            DataViewManager dvManager = new DataViewManager(MyDataSet);
            dv = dvManager.CreateDataView(MyDataSet.Tables[0]);
            gridControl_SECENEKLER.DataSource = dv;
        }
        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BTN_YENI_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SABIT_LISTE();
        }



        private void SABIT_LISTE()
        {
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet ds = new DataSet();

                string query = "";

                if (RDBTN_ANAYAYIN.Checked)
                {
                    query = "    SELECT     ANA_YAYIN AS MEDYA FROM   dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, ANA_YAYIN  HAVING  (YAYIN_SINIFI = N'GAZETE') ORDER BY ANA_YAYIN";

                    gridView_SABITLER.Columns["MEDYA"].Visible = true;
                    gridView_SABITLER.Columns["SAYFA_GRUBU"].Visible = false;
                    gridView_SABITLER.Columns["YAYIN_TURU"].Visible = false;
                    gridView_SECENEKLER.Columns["MECRA_KODU"].Visible = true;
                    gridView_SECENEKLER.Columns["SAYFA_GRUBU"].Visible = false;
                    gridView_SECENEKLER.Columns["YAYIN_TURU"].Visible = false;
                }


                if (RDBTN_ANAYAYIN.Checked && rdBtn_MECRA_VE_SAYFA_BAZINDA.Checked)
                {
                    query = "      SELECT     ANA_YAYIN AS MEDYA,SAYFA_GRUBU FROM  dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, ANA_YAYIN ,SAYFA_GRUBU HAVING (YAYIN_SINIFI = N'GAZETE') ORDER BY ANA_YAYIN ";

                    gridView_SABITLER.Columns["MEDYA"].Visible = true;
                    gridView_SABITLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SABITLER.Columns["YAYIN_TURU"].Visible = false;
                    gridView_SECENEKLER.Columns["MECRA_KODU"].Visible = true;
                    gridView_SECENEKLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SECENEKLER.Columns["YAYIN_TURU"].Visible = false;

                }



                if (RDBTN_ANAYAYIN.Checked && rdBtn_MECRA_VE_SAYFA_YAYINTURU.Checked)
                {
                    query = "      SELECT     ANA_YAYIN AS MEDYA ,SAYFA_GRUBU,YAYIN_TURU FROM  dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, ANA_YAYIN ,SAYFA_GRUBU,YAYIN_TURU HAVING (YAYIN_SINIFI = N'GAZETE') ORDER BY ANA_YAYIN ";

                    gridView_SABITLER.Columns["MEDYA"].Visible = true;
                    gridView_SABITLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SABITLER.Columns["YAYIN_TURU"].Visible = true;
                    gridView_SECENEKLER.Columns["MECRA_KODU"].Visible = true;
                    gridView_SECENEKLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SECENEKLER.Columns["YAYIN_TURU"].Visible = true;

                }


                if (RDBTN_ANAYAYIN.Checked && rdBtn_MECRA_VE_SAYFA_YAYINTURU_VE_TARIFE_MADDE.Checked)
                {
                    query = "      SELECT     ANA_YAYIN AS MEDYA ,SAYFA_GRUBU,YAYIN_TURU,TARIFE_MADDE FROM  dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, ANA_YAYIN ,SAYFA_GRUBU,YAYIN_TURU,TARIFE_MADDE  HAVING  (YAYIN_SINIFI = N'GAZETE') ORDER BY ANA_YAYIN ";

                    gridView_SABITLER.Columns["MEDYA"].Visible = true;
                    gridView_SABITLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SABITLER.Columns["YAYIN_TURU"].Visible = true;
                    gridView_SECENEKLER.Columns["MECRA_KODU"].Visible = true;
                    gridView_SECENEKLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SECENEKLER.Columns["YAYIN_TURU"].Visible = true;

                }



                if (RDBTN_MEDYA.Checked)
                {
                    query = "    SELECT     MEDYA FROM   dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, MEDYA  HAVING  (YAYIN_SINIFI = N'GAZETE') ORDER BY MEDYA";

                    gridView_SABITLER.Columns["MEDYA"].Visible = true;
                    gridView_SABITLER.Columns["SAYFA_GRUBU"].Visible = false;
                    gridView_SABITLER.Columns["YAYIN_TURU"].Visible = false;
                    gridView_SECENEKLER.Columns["MECRA_KODU"].Visible = true;
                    gridView_SECENEKLER.Columns["SAYFA_GRUBU"].Visible = false;
                    gridView_SECENEKLER.Columns["YAYIN_TURU"].Visible = false;
                }
                 


                if (RDBTN_MEDYA.Checked && rdBtn_MECRA_VE_SAYFA_BAZINDA.Checked)
                {
                    query = "      SELECT     MEDYA,SAYFA_GRUBU FROM  dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, MEDYA ,SAYFA_GRUBU HAVING (YAYIN_SINIFI = N'GAZETE') ORDER BY MEDYA ";

                    gridView_SABITLER.Columns["MEDYA"].Visible = true;
                    gridView_SABITLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SABITLER.Columns["YAYIN_TURU"].Visible = false;
                    gridView_SECENEKLER.Columns["MECRA_KODU"].Visible = true;
                    gridView_SECENEKLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SECENEKLER.Columns["YAYIN_TURU"].Visible = false;

                }


                if (RDBTN_MEDYA.Checked &&  rdBtn_MECRA_VE_SAYFA_YAYINTURU.Checked)
                {
                    query = "      SELECT     MEDYA,SAYFA_GRUBU,YAYIN_TURU FROM  dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, MEDYA ,SAYFA_GRUBU,YAYIN_TURU HAVING (YAYIN_SINIFI = N'GAZETE') ORDER BY MEDYA ";

                    gridView_SABITLER.Columns["MEDYA"].Visible = true;
                    gridView_SABITLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SABITLER.Columns["YAYIN_TURU"].Visible = true;
                    gridView_SECENEKLER.Columns["MECRA_KODU"].Visible = true;
                    gridView_SECENEKLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SECENEKLER.Columns["YAYIN_TURU"].Visible = true;

                }


                if (RDBTN_MEDYA.Checked &&  rdBtn_MECRA_VE_SAYFA_YAYINTURU_VE_TARIFE_MADDE.Checked)
                {
                    query = "      SELECT     MEDYA,SAYFA_GRUBU,YAYIN_TURU,TARIFE_MADDE FROM  dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, MEDYA ,SAYFA_GRUBU,YAYIN_TURU,TARIFE_MADDE  HAVING  (YAYIN_SINIFI = N'GAZETE') ORDER BY MEDYA ";

                    gridView_SABITLER.Columns["MEDYA"].Visible = true;
                    gridView_SABITLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SABITLER.Columns["YAYIN_TURU"].Visible = true;
                    gridView_SECENEKLER.Columns["MECRA_KODU"].Visible = true;
                    gridView_SECENEKLER.Columns["SAYFA_GRUBU"].Visible = true;
                    gridView_SECENEKLER.Columns["YAYIN_TURU"].Visible = true;

                }






                //if (rdBtn_MECRA_BAZINDA.Checked)
                //{
                //    query = "    SELECT     MEDYA FROM   dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, MEDYA  HAVING  (YAYIN_SINIFI = N'GAZETE') ORDER BY MEDYA";

                //    gridView_SABITLER.Columns["MEDYA"].Visible = true;
                //    gridView_SABITLER.Columns["SAYFA_GRUBU"].Visible = false;
                //    gridView_SABITLER.Columns["YAYIN_TURU"].Visible = false; 
                //    gridView_SECENEKLER.Columns["MECRA_KODU"].Visible = true;
                //    gridView_SECENEKLER.Columns["SAYFA_GRUBU"].Visible = false;
                //    gridView_SECENEKLER.Columns["YAYIN_TURU"].Visible = false;
                //}

                //if (rdBtn_MECRA_VE_SAYFA_BAZINDA.Checked)
                //{
                //    query = "      SELECT     MEDYA,SAYFA_GRUBU FROM  dbo._ADEX_INDEX_DATA GROUP BY YAYIN_SINIFI, MEDYA ,SAYFA_GRUBU HAVING (YAYIN_SINIFI = N'GAZETE') ORDER BY MEDYA ";

                //    gridView_SABITLER.Columns["MEDYA"].Visible = true;
                //    gridView_SABITLER.Columns["SAYFA_GRUBU"].Visible = true;
                //    gridView_SABITLER.Columns["YAYIN_TURU"].Visible = false; 
                //    gridView_SECENEKLER.Columns["MECRA_KODU"].Visible = true;
                //    gridView_SECENEKLER.Columns["SAYFA_GRUBU"].Visible = true;
                //    gridView_SECENEKLER.Columns["YAYIN_TURU"].Visible = false;

                //}



                SqlDataAdapter adapter = new SqlDataAdapter();
                adapter.SelectCommand = new SqlCommand(query, conn);
                adapter.Fill(ds, "dbo_ANA_YAYIN");
                DataViewManager dvManager = new DataViewManager(ds);
                DataView dvLIST = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_MECRA_LISTESI.DataSource = dvLIST;
            }
            gridView_SABITLER.ShowFindPanel();

            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = "   SELECT   top 0  * FROM         dbo.TRF_GAZETE   ";
                SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter(SQL, myConnections);
                DataSet ds = new DataSet();
                MySqlDataAdapter.Fill(ds, "dbo_SELECTANA_YAYIN");
                DataViewManager dvManager = new DataViewManager(ds);
                dv = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_SECENEKLER.DataSource = dv;
            }
            gridView_SECENEKLER.ShowFindPanel();
        }
         

        private void BTN_LISTE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            TARIFELER._TARIFE_LISTESI TF = new TARIFELER._TARIFE_LISTESI("TARIFE","GAZETE");
            TF.ShowDialog(); 
            lbFILE_NAME.Caption = TF._TARIFE_KODU;
            lbID.Caption = TF._TARIFE_ID; 

            string STR_SECENEKLER_BIR = "", STR_SECENEKLER_IKI = "";
            dv = null;
            for (int i = 0; i <= gridView_SECENEKLER.Columns.Count - 1; i++)
            {
                gridView_SECENEKLER.Columns[i].Visible = false;
            }
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format(" SELECT  * FROM         dbo.TRF_TARIFELER_LISTESI WHERE   TARIFE_OWNER='{0}' and  (SIRKET_KODU = N'{1}') AND (MECRA_TURU = N'GAZETE') and TARIFE_KODU='{2}'", _GLOBAL_PARAMETRELER._KULLANICI_MAIL, _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, lbFILE_NAME.Caption);
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
                        STR_SECENEKLER_IKI = "BIRIM_FIYAT,HI,";//SALI,CARSAMBA,PERSEMBE,CUMA,CUMARTESI,PAZAR, ";
                        GAZETE_TARIFE_TURU = "GUNLUK_TARIFE";
                    }
                    LBL_GAZETE_TARIFE_TURU.Text = myReader["SELECT_TYPE"].ToString();
                    if (myReader["SELECT_TYPE"].ToString() == "MECRA") { STR_SECENEKLER_BIR = "MECRA_KODU,HESAPLANMA_TURU,"; GAZETE_TARIFE_SECENEK = "MECRA"; }
                    if (myReader["SELECT_TYPE"].ToString() == "MECRA_SAYFA") { STR_SECENEKLER_BIR = "MECRA_KODU,SAYFA_GRUBU,HESAPLANMA_TURU,"; GAZETE_TARIFE_SECENEK = "MECRA_SAYFA"; }
                    if (myReader["SELECT_TYPE"].ToString() == "MECRA_SAYFA_YAYINTURU") { STR_SECENEKLER_BIR = "MECRA_KODU,SAYFA_GRUBU,YAYIN_TURU,HESAPLANMA_TURU,"; GAZETE_TARIFE_SECENEK = "MECRA_SAYFA_YAYINTURU"; }
                }

                string qury = String.Format("{0}{1}BASLANGIC_TARIHI,BITIS_TARIHI", STR_SECENEKLER_BIR, STR_SECENEKLER_IKI);
                DataSet ds = new DataSet();
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string querys = String.Format(" SELECT ID, {0} {1}  BASLANGIC_TARIHI , BITIS_TARIHI   FROM     dbo.TRF_GAZETE where  KULLANICI_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_MAIL + "' and SIRKET_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "' and TARIFE_KODU='" + lbFILE_NAME.Caption + "'", STR_SECENEKLER_BIR, STR_SECENEKLER_IKI);
                    SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(querys, conn) };
                    adapter.Fill(ds, "TRF_GAZETE");
                    DataViewManager dvManager = new DataViewManager(ds);
                    dv = dvManager.CreateDataView(ds.Tables[0]);
                }
                gridControl_SECENEKLER.DataSource = dv;
                gridView_SECENEKLER.RefreshData();
                //string[] Onesz = qury.Split(',');
                //for (int i = 0; i < Onesz.Length; i++)
                //{
                //    gridView_SECENEKLER.Columns[Onesz[i].Trim()].VisibleIndex = i;
                //    gridView_SECENEKLER.Columns[Onesz[i].Trim()].Visible = true;
                //}
            } 
        }

        private void rdBtn_MECRA_BAZINDA_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
        }

        private void rdBtn_MECRA_VE_SAYFA_BAZINDA_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
        }

        private void rdBtn_MECRA_VE_SAYFA_YAYINTURU_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
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
         

                if (rdBtn_MECRA_BAZINDA.Checked)
                {
                    rowView["MECRA_KODU"] = dr["MEDYA"];
                    rowView["HESAPLANMA_TURU"] = COMBO_HESAPLAMA_TURU.Text;                    
                }
                if (rdBtn_MECRA_VE_SAYFA_BAZINDA.Checked)
                { 
                    rowView["MECRA_KODU"] = dr["MEDYA"];
                    rowView["SAYFA_GRUBU"] = dr["SAYFA_GRUBU"];
                    rowView["HESAPLANMA_TURU"] = COMBO_HESAPLAMA_TURU.Text; 
                }
                if (rdBtn_MECRA_VE_SAYFA_YAYINTURU.Checked)
                { 
                    rowView["MECRA_KODU"] = dr["MEDYA"];
                    rowView["SAYFA_GRUBU"] = dr["SAYFA_GRUBU"];
                    rowView["YAYIN_TURU"] = dr["YAYIN_TURU"];
                    rowView["HESAPLANMA_TURU"] = COMBO_HESAPLAMA_TURU.Text;  
                } 

                if (CHK_GUNLUK_TARIFE.Checked)
                {
                    rowView["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.Text;
                    //rowView["PAZARTESI"] = TXT_BIRIM_FIYAT.Text;
                    //rowView["SALI"] = TXT_BIRIM_FIYAT.Text;
                    //rowView["CARSAMBA"] = TXT_BIRIM_FIYAT.Text;
                    //rowView["PERSEMBE"] = TXT_BIRIM_FIYAT.Text;
                    rowView["HI"] = TXT_BIRIM_FIYAT.Text;
                    rowView["CUMARTESI"] = TXT_BIRIM_FIYAT.Text;
                    rowView["PAZAR"] = TXT_BIRIM_FIYAT.Text;
                }
                else
                {
                   rowView["BIRIM_FIYAT"] = TXT_BIRIM_FIYAT.Text;
                }
                rowView.EndEdit();
            }
        }

        private void rdBtn_MECRA_VE_SAYFA_YAYINTURU_VE_TARIFE_MADDE_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
        }

        private void CHK_GUNLUK_TARIFE_CheckedChanged(object sender, EventArgs e)
        {
            if (CHK_GUNLUK_TARIFE.Checked)
            {
                gridView_SECENEKLER.Columns["BIRIM_FIYAT"].Visible = true; 
                gridView_SECENEKLER.Columns["CUMARTESI"].Visible = true;
                gridView_SECENEKLER.Columns["PAZAR"].Visible = true;
            }
            else
            {
                gridView_SECENEKLER.Columns["BIRIM_FIYAT"].Visible = true; 
                gridView_SECENEKLER.Columns["CUMARTESI"].Visible = false;
                gridView_SECENEKLER.Columns["PAZAR"].Visible = false;
            } 
        }

        private void RDBTN_ANAYAYIN_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
        }

        private void RDBTN_MEDYA_CheckedChanged(object sender, EventArgs e)
        {
            SABIT_LISTE();
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
    }
}