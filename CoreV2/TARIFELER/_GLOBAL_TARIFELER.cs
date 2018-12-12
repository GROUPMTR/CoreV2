using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient; 
using System.Windows.Forms;

namespace CoreV2.TARIFELER
{
    public class _GLOBAL_TARIFELER
   {        

        string ClipboardData
        {
            get
            {
                IDataObject iData = Clipboard.GetDataObject();
                if (iData == null) return "";

                if (iData.GetDataPresent(DataFormats.Text))
                    return (string)iData.GetData(DataFormats.Text);
                return "";
            }
            set { Clipboard.SetDataObject(value); }
        }
        DataTable tbl;
        private void CLIPBOARD_DATA_READ()
        {
            tbl = null;
            tbl = new DataTable();
            tbl.TableName = "ImportedTable";
            List<string> data = new List<string>(ClipboardData.Split('\n'));
            bool firstRow = true;

            if (data.Count > 0 && string.IsNullOrWhiteSpace(data[data.Count - 1]))
            {
                data.RemoveAt(data.Count - 1);
            }

            foreach (string iterationRow in data)
            {
                string row = iterationRow;
                if (row.EndsWith("\r"))
                {
                    row = row.Substring(0, row.Length - "\r".Length);
                }

                string[] rowData = row.Split(new char[] { '\r', '\x09' });
                DataRow newRow = tbl.NewRow();
                if (firstRow)
                {
                    int colNumber = 0;
                    foreach (string value in rowData)
                    {
                        if (string.IsNullOrWhiteSpace(value))
                        {
                            tbl.Columns.Add(string.Format("[BLANK{0}]", colNumber));
                        }
                        else if (!tbl.Columns.Contains(value))
                        {
                            tbl.Columns.Add(value);
                        }
                        else
                        {
                            tbl.Columns.Add(string.Format("Column {0}", colNumber));
                        }
                        colNumber++;
                    }
                    firstRow = false;
                }
                else
                {
                    for (int i = 0; i < rowData.Length; i++)
                    {
                        if (i >= tbl.Columns.Count) break;
                        newRow[i] = rowData[i];
                    }
                    tbl.Rows.Add(newRow);
                }
            }

        }
        public void _YAPISTIR(DataView DW_GENERIC)
        {
            CLIPBOARD_DATA_READ();
            for (int x = 0; x <= tbl.Rows.Count - 1; x++)
            {
                DataRow rowm = tbl.Rows[x];
                DataRow rows = DW_GENERIC.Table.NewRow();

                for (int XZ = 0; XZ <= tbl.Columns.Count - 1; XZ++)
                {
                    rows[XZ] = rowm[XZ];
                }
                DW_GENERIC.Table.Rows.Add(rows);
            }
        }
        

        public int KAYDET(string FILE_NAME, string MECRA_TURU, string TYPE, bool CHK_GUNLUK,string HESAPLAMA_TURU)
        {
            Int32 DEGER = 0;
            DateTime myDTStart = Convert.ToDateTime(DateTime.Now); 
            SqlConnection myConnectionKontrol = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            string myInsertQueryKontrol = string.Format(" select * from dbo.TRF_TARIFELER_LISTESI Where TARIFE_TURU='TARIFE' AND  SIRKET_KODU='{0}' and MECRA_TURU='{1}' and TARIFE_KODU= '{2}' ", _GLOBAL_PARAMETRELER._KULLANICI_FIRMA, MECRA_TURU, FILE_NAME);
            SqlCommand myCommandKontrol = new SqlCommand(myInsertQueryKontrol) { Connection = myConnectionKontrol };
            myConnectionKontrol.Open();
            SqlDataReader myReaderKontrol = myCommandKontrol.ExecuteReader(CommandBehavior.CloseConnection);
            if (myReaderKontrol.HasRows == false)
            {
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    conn.Open();
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandText = "INSERT INTO dbo.TRF_TARIFELER_LISTESI(TARIFE_TURU,SIRKET_KODU, MECRA_TURU,TARIFE_KODU, TARIFE_OWNER,ISLEM_TARIHI,ISLEM_SAATI,SELECT_TYPE,GUNLUK_TARIFE,HESAPLAMA_TURU)  VALUES ( @TARIFE_TURU,@SIRKET_KODU, @MECRA_TURU,@TARIFE_KODU, @TARIFE_OWNER,@ISLEM_TARIHI,@ISLEM_SAATI,@SELECT_TYPE,@GUNLUK_TARIFE,@HESAPLAMA_TURU ) SELECT @@IDENTITY AS ID ";
                 
                    myCmd.Parameters.Add("@TARIFE_TURU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_TURU"].Value = "TARIFE";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = MECRA_TURU;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = FILE_NAME;
                    myCmd.Parameters.Add("@TARIFE_OWNER", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_OWNER"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                    myCmd.Parameters.Add("@ISLEM_TARIHI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_TARIHI"].Value = myDTStart.ToString("yyyy.MM.dd").ToString();
                    myCmd.Parameters.Add("@ISLEM_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_SAATI"].Value = myDTStart.ToString("hh:MM:ss").ToString();
                    myCmd.Parameters.Add("@GUNLUK_TARIFE", SqlDbType.Bit); myCmd.Parameters["@GUNLUK_TARIFE"].Value = CHK_GUNLUK;
                    myCmd.Parameters.Add("@SELECT_TYPE", SqlDbType.NVarChar); myCmd.Parameters["@SELECT_TYPE"].Value = TYPE;
                    myCmd.Parameters.Add("@HESAPLAMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLAMA_TURU"].Value = HESAPLAMA_TURU;
                    

                    myCmd.Connection = conn;
                    SqlDataReader myReader = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myReader.Read())
                    {
                        DEGER=Convert.ToInt32(myReader["ID"]);
                    }
                    myReader.Close();
                    myCmd.Connection.Close();
                }
            }
            else
            { 
                //  MessageBox.Show("Bu kod daha önce kullanılmış! Lütfen farklı bir kod kullanınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return DEGER;
        }
        public void GUNCELLE(string FILE_NAME, string TARIFE_REF, string MECRA_TURU, string HESAPLAMA_TURU)
        {
            DateTime myDTStart = Convert.ToDateTime(DateTime.Now);

            SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnectionTable.Open();
            SqlCommand myCmd = new SqlCommand();
            myCmd.CommandText = " Update dbo.TRF_TARIFELER_LISTESI SET SIRKET_KODU=@SIRKET_KODU, MECRA_TURU=@MECRA_TURU,TARIFE_KODU=@TARIFE_KODU, TARIFE_OWNER=@TARIFE_OWNER,ISLEM_TARIHI=@ISLEM_TARIHI,ISLEM_SAATI=@ISLEM_SAATI,HESAPLAMA_TURU=@HESAPLAMA_TURU  WHERE  TARIFE_TURU='TARIFE' AND SIRKET_KODU=@SIRKET_KODU AND  (ID=@TARIFE_REF)";
            myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
            myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = MECRA_TURU;
            myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = FILE_NAME;
            myCmd.Parameters.Add("@TARIFE_OWNER", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_OWNER"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
            myCmd.Parameters.Add("@ISLEM_TARIHI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_TARIHI"].Value = myDTStart.ToString("yyyy.MM.dd").ToString();
            myCmd.Parameters.Add("@ISLEM_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_SAATI"].Value = myDTStart.ToString("hh:MM:ss").ToString();
            myCmd.Parameters.Add("@HESAPLAMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLAMA_TURU"].Value = HESAPLAMA_TURU;
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Connection = myConnectionTable;
            myCmd.ExecuteNonQuery();
            myCmd.Connection.Close();
        }
        public int FARKLI_KAYDET(string FILE_NAME, string MECRA_TURU, string TYPE, bool CHK_GUNLUK, string HESAPLAMA_TURU,string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            Int32 DEGER = 0;
            DateTime myDTStart = Convert.ToDateTime(DateTime.Now); 
            SqlConnection myConnectionKontrol = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            string myInsertQueryKontrol = string.Format(" select * from dbo.TRF_TARIFELER_LISTESI Where TARIFE_TURU='TARIFE' AND  SIRKET_KODU='{0}' and TARIFE_OWNER='{1}' and MECRA_TURU='{2}' and TARIFE_KODU='{3}' ", _KULLANICI_FIRMA, _KULLANICI_NAME, MECRA_TURU, FILE_NAME);
            SqlCommand myCommandKontrol = new SqlCommand(myInsertQueryKontrol);
            myCommandKontrol.Connection = myConnectionKontrol;
            myConnectionKontrol.Open();
            SqlDataReader myReaderKontrol = myCommandKontrol.ExecuteReader(CommandBehavior.CloseConnection);
            if (myReaderKontrol.HasRows == false)
            {
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    conn.Open();
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandText = "INSERT INTO dbo.TRF_TARIFELER_LISTESI(TARIFE_TURU,SIRKET_KODU, MECRA_TURU,TARIFE_KODU, TARIFE_OWNER,ISLEM_TARIHI,ISLEM_SAATI,SELECT_TYPE,GUNLUK_TARIFE,HESAPLAMA_TURU)  VALUES ( @TARIFE_TURU,@SIRKET_KODU, @MECRA_TURU,@TARIFE_KODU, @TARIFE_OWNER,@ISLEM_TARIHI,@ISLEM_SAATI,@SELECT_TYPE,@GUNLUK_TARIFE,@HESAPLAMA_TURU ) SELECT @@IDENTITY AS ID ";
                    myCmd.Parameters.Add("@TARIFE_TURU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_TURU"].Value = "TARIFE";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = MECRA_TURU;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = FILE_NAME;
                    myCmd.Parameters.Add("@TARIFE_OWNER", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_OWNER"].Value = _KULLANICI_NAME;
                    myCmd.Parameters.Add("@ISLEM_TARIHI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_TARIHI"].Value = myDTStart.ToString("yyyy.MM.dd").ToString();
                    myCmd.Parameters.Add("@ISLEM_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_SAATI"].Value = myDTStart.ToString("hh:MM:ss").ToString();
                    myCmd.Parameters.Add("@GUNLUK_TARIFE", SqlDbType.Bit); myCmd.Parameters["@GUNLUK_TARIFE"].Value = CHK_GUNLUK;
                    myCmd.Parameters.Add("@SELECT_TYPE", SqlDbType.NVarChar); myCmd.Parameters["@SELECT_TYPE"].Value = TYPE;
                    myCmd.Parameters.Add("@HESAPLAMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLAMA_TURU"].Value = HESAPLAMA_TURU;
                    myCmd.Connection = conn;
                    SqlDataReader myReader = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myReader.Read())
                    {
                        DEGER = Convert.ToInt32(myReader["ID"]);
                    }
                    myReader.Close();
                    myCmd.Connection.Close();
                }
            }
            else
            {
                // MessageBox.Show("Bu kod daha önce kullanılmış! Lütfen farklı bir kod kullanınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return DEGER;
        }




        public int TARIFE_PAYLAS(string FILE_NAME, string MECRA_TURU, string TYPE, bool CHK_GUNLUK, string HESAPLAMA_TURU, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            Int32 DEGER = 0;
            DateTime myDTStart = Convert.ToDateTime(DateTime.Now);
            SqlConnection myConnectionKontrol = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            string myInsertQueryKontrol = string.Format(" select * from dbo.TRF_TARIFELER_LISTESI Where TARIFE_TURU='TARIFE' AND  SIRKET_KODU='{0}' and TARIFE_OWNER='{1}' and MECRA_TURU='{2}' and TARIFE_KODU='{3}' ", _KULLANICI_FIRMA, _KULLANICI_NAME, MECRA_TURU, FILE_NAME);
            SqlCommand myCommandKontrol = new SqlCommand(myInsertQueryKontrol);
            myCommandKontrol.Connection = myConnectionKontrol;
            myConnectionKontrol.Open();
            SqlDataReader myReaderKontrol = myCommandKontrol.ExecuteReader(CommandBehavior.CloseConnection);
            if (myReaderKontrol.HasRows == false)
            {
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    conn.Open();
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandText = "INSERT INTO dbo.TRF_TARIFELER_LISTESI(TARIFE_TURU,SIRKET_KODU, MECRA_TURU,TARIFE_KODU, TARIFE_OWNER,ISLEM_TARIHI,ISLEM_SAATI,SELECT_TYPE,GUNLUK_TARIFE,HESAPLAMA_TURU)  VALUES ( @TARIFE_TURU,@SIRKET_KODU, @MECRA_TURU,@TARIFE_KODU, @TARIFE_OWNER,@ISLEM_TARIHI,@ISLEM_SAATI,@SELECT_TYPE,@GUNLUK_TARIFE,@HESAPLAMA_TURU ) SELECT @@IDENTITY AS ID ";
                    myCmd.Parameters.Add("@TARIFE_TURU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_TURU"].Value = "TARIFE";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = MECRA_TURU;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = FILE_NAME;
                    myCmd.Parameters.Add("@TARIFE_OWNER", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_OWNER"].Value = _KULLANICI_NAME;
                    myCmd.Parameters.Add("@ISLEM_TARIHI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_TARIHI"].Value = myDTStart.ToString("yyyy.MM.dd").ToString();
                    myCmd.Parameters.Add("@ISLEM_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_SAATI"].Value = myDTStart.ToString("hh:MM:ss").ToString();
                    myCmd.Parameters.Add("@GUNLUK_TARIFE", SqlDbType.Bit); myCmd.Parameters["@GUNLUK_TARIFE"].Value = CHK_GUNLUK;
                    myCmd.Parameters.Add("@SELECT_TYPE", SqlDbType.NVarChar); myCmd.Parameters["@SELECT_TYPE"].Value = TYPE;
                    myCmd.Parameters.Add("@HESAPLAMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLAMA_TURU"].Value = HESAPLAMA_TURU;
                    myCmd.Connection = conn;
                    SqlDataReader myReader = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (myReader.Read())
                    {
                        DEGER = Convert.ToInt32(myReader["ID"]);
                    }
                    myReader.Close();
                    myCmd.Connection.Close();
                }
            }
            else
            {
                //  MessageBox.Show("Bu kod daha önce kullanılmış! Lütfen farklı bir kod kullanınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            return DEGER;
        }

        /// <summary>
        ///  TARIFELERIN SATIR BAZINDA KAYDEDILMESI
        /// </summary>
        /// <param name="dv"></param>

        public void GAZETE_ROW_DELETE(DataView dv,string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandText = "DELETE  dbo.TRF_GAZETE  where ID=@ID AND TARIFE_REF=@TARIFE_REF ";
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID", DataRowVersion.Original];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            } myConnection.Close();
        }
        public void GAZETE_ROW_ADD(DataView dv, string TARIFE_KODU, string GAZETE_TARIFE_SECENEK, string GUNLUK, string TARIFE_REF, string _KULLANICI_FIRMA,string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            if (GUNLUK == "GUNLUK_TARIFE")
            {
                if (GAZETE_TARIFE_SECENEK == "MECRA_SAYFA_YAYINTURU")
                { 
                    for (int i = 0; i < dv.Count; i++)
                    {
                        DataRow DR = dv[i].Row;
                        if (DR["MECRA_KODU"].ToString() == "") break;
                        SqlCommand myCmd = new SqlCommand();
                        myCmd.CommandTimeout = 0;
                        myCmd.CommandText = "INSERT INTO dbo.TRF_GAZETE(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU,SAYFA_GRUBU,YAYIN_TURU, HESAPLANMA_TURU,  BIRIM_FIYAT,  PAZARTESI,SALI,CARSAMBA,PERSEMBE,CUMA,CUMARTESI,PAZAR,BASLANGIC_TARIHI,BITIS_TARIHI) " +
                                           " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU,@SAYFA_GRUBU,@YAYIN_TURU, @HESAPLANMA_TURU,@BIRIM_FIYAT,   @PAZARTESI,@SALI,@CARSAMBA,@PERSEMBE,@CUMA,@CUMARTESI,@PAZAR,@BASLANGIC_TARIHI,@BITIS_TARIHI)";
                        myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                        myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                        myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                        myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "GAZETE";
                        myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                        myCmd.Parameters.Add("@SAYFA_GRUBU", SqlDbType.NVarChar); myCmd.Parameters["@SAYFA_GRUBU"].Value = DR["SAYFA_GRUBU"];
                        myCmd.Parameters.Add("@YAYIN_TURU", SqlDbType.NVarChar); myCmd.Parameters["@YAYIN_TURU"].Value = DR["YAYIN_TURU"];
                        myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                        myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"];
                        //myCmd.Parameters.Add("@PAZARTESI", SqlDbType.Float); myCmd.Parameters["@PAZARTESI"].Value = DR["PAZARTESI"];
                        //myCmd.Parameters.Add("@SALI", SqlDbType.Float); myCmd.Parameters["@SALI"].Value = DR["SALI"];
                        //myCmd.Parameters.Add("@CARSAMBA", SqlDbType.Float); myCmd.Parameters["@CARSAMBA"].Value = DR["CARSAMBA"];
                        //myCmd.Parameters.Add("@PERSEMBE", SqlDbType.Float); myCmd.Parameters["@PERSEMBE"].Value = DR["PERSEMBE"];
                        //myCmd.Parameters.Add("@CUMA", SqlDbType.Float); myCmd.Parameters["@CUMA"].Value = DR["CUMA"];
                        myCmd.Parameters.Add("@CUMARTESI", SqlDbType.Float); myCmd.Parameters["@CUMARTESI"].Value = DR["CUMARTESI"];
                        myCmd.Parameters.Add("@PAZAR", SqlDbType.Float); myCmd.Parameters["@PAZAR"].Value = DR["PAZAR"];
                        myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                        myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                        myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                        myCmd.Connection = myConnection;
                        myCmd.ExecuteNonQuery();
                    }
                }
                if (GAZETE_TARIFE_SECENEK == "MECRA_SAYFA")
                { 
                    for (int i = 0; i < dv.Count; i++)
                    {
                        DataRow DR = dv[i].Row;
                        if (DR["MECRA_KODU"].ToString() == "") break;

                        SqlCommand myCmd = new SqlCommand();
                        myCmd.CommandTimeout = 0;
                        myCmd.CommandText = "INSERT INTO dbo.TRF_GAZETE(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU,SAYFA_GRUBU, HESAPLANMA_TURU,  BIRIM_FIYAT,CUMARTESI,PAZAR,BASLANGIC_TARIHI,BITIS_TARIHI) " +
                                           " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU,@SAYFA_GRUBU, @HESAPLANMA_TURU,  @BIRIM_FIYAT,@CUMARTESI,@PAZAR,@BASLANGIC_TARIHI,@BITIS_TARIHI)";
                        myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                        myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                        myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                        myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "GAZETE";
                        myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                        myCmd.Parameters.Add("@SAYFA_GRUBU", SqlDbType.NVarChar); myCmd.Parameters["@SAYFA_GRUBU"].Value = DR["SAYFA_GRUBU"];
                        myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                        myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"];
                        //myCmd.Parameters.Add("@PAZARTESI", SqlDbType.Float); myCmd.Parameters["@PAZARTESI"].Value = DR["PAZARTESI"];
                        //myCmd.Parameters.Add("@SALI", SqlDbType.Float); myCmd.Parameters["@SALI"].Value = DR["SALI"];
                        //myCmd.Parameters.Add("@CARSAMBA", SqlDbType.Float); myCmd.Parameters["@CARSAMBA"].Value = DR["CARSAMBA"];
                        //myCmd.Parameters.Add("@PERSEMBE", SqlDbType.Float); myCmd.Parameters["@PERSEMBE"].Value = DR["PERSEMBE"];
                        //myCmd.Parameters.Add("@CUMA", SqlDbType.Float); myCmd.Parameters["@CUMA"].Value = DR["CUMA"];
                        myCmd.Parameters.Add("@CUMARTESI", SqlDbType.Float); myCmd.Parameters["@CUMARTESI"].Value = DR["CUMARTESI"];
                        myCmd.Parameters.Add("@PAZAR", SqlDbType.Float); myCmd.Parameters["@PAZAR"].Value = DR["PAZAR"];
                        myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                        myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                        myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                        myCmd.Connection = myConnection;
                        myCmd.ExecuteNonQuery(); 
                    } 
                } 
                if (GAZETE_TARIFE_SECENEK == "MECRA")
                { 
                    for (int i = 0; i < dv.Count; i++)
                    {
                        DataRow DR = dv[i].Row;
                        if (DR["MECRA_KODU"].ToString() == "") break;
                        SqlCommand myCmd = new SqlCommand();
                        myCmd.CommandTimeout = 0;
                        myCmd.CommandText = "INSERT INTO dbo.TRF_GAZETE(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU, HESAPLANMA_TURU, BIRIM_FIYAT,  CUMARTESI,PAZAR,BASLANGIC_TARIHI,BITIS_TARIHI) " +
                                           " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU, @HESAPLANMA_TURU, @BIRIM_FIYAT,  @CUMARTESI,@PAZAR,@BASLANGIC_TARIHI,@BITIS_TARIHI)";
                        myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                        myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                        myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                        myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "GAZETE";
                        myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                        myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                        myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"];
                        //myCmd.Parameters.Add("@PAZARTESI", SqlDbType.Float); myCmd.Parameters["@PAZARTESI"].Value = DR["PAZARTESI"];
                        //myCmd.Parameters.Add("@SALI", SqlDbType.Float); myCmd.Parameters["@SALI"].Value = DR["SALI"];
                        //myCmd.Parameters.Add("@CARSAMBA", SqlDbType.Float); myCmd.Parameters["@CARSAMBA"].Value = DR["CARSAMBA"];
                        //myCmd.Parameters.Add("@PERSEMBE", SqlDbType.Float); myCmd.Parameters["@PERSEMBE"].Value = DR["PERSEMBE"];
                        //myCmd.Parameters.Add("@CUMA", SqlDbType.Float); myCmd.Parameters["@CUMA"].Value = DR["CUMA"];
                        myCmd.Parameters.Add("@CUMARTESI", SqlDbType.Float); myCmd.Parameters["@CUMARTESI"].Value = DR["CUMARTESI"];
                        myCmd.Parameters.Add("@PAZAR", SqlDbType.Float); myCmd.Parameters["@PAZAR"].Value = DR["PAZAR"];
                        myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                        myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                        myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                        myCmd.Connection = myConnection;
                        myCmd.ExecuteNonQuery(); 
                    }
                }
            }
            else
            {
                if (GAZETE_TARIFE_SECENEK == "MECRA_SAYFA_YAYINTURU")
                { 
                    for (int i = 0; i < dv.Count; i++)
                    {
                        DataRow DR = dv[i].Row;
                        if (DR["MECRA_KODU"].ToString() == "") break;
                        SqlCommand myCmd = new SqlCommand();
                        myCmd.CommandTimeout = 0;
                        myCmd.CommandText = "INSERT INTO dbo.TRF_GAZETE(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU,SAYFA_GRUBU,YAYIN_TURU, HESAPLANMA_TURU, BIRIM_FIYAT,   BASLANGIC_TARIHI,BITIS_TARIHI) " +
                                           " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU,@SAYFA_GRUBU,@YAYIN_TURU, @HESAPLANMA_TURU, @BIRIM_FIYAT,@BASLANGIC_TARIHI,@BITIS_TARIHI)";
                        myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                        myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                        myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                        myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "GAZETE";
                        myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                        myCmd.Parameters.Add("@SAYFA_GRUBU", SqlDbType.NVarChar); myCmd.Parameters["@SAYFA_GRUBU"].Value = DR["SAYFA_GRUBU"];
                        myCmd.Parameters.Add("@YAYIN_TURU", SqlDbType.NVarChar); myCmd.Parameters["@YAYIN_TURU"].Value = DR["YAYIN_TURU"];
                        myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                        myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"];
                        myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                        myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                        myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                        myCmd.Connection = myConnection;
                        myCmd.ExecuteNonQuery(); 
                    } 
                }
                if (GAZETE_TARIFE_SECENEK == "MECRA_SAYFA")
                { 
                    for (int i = 0; i < dv.Count; i++)
                    {
                        DataRow DR = dv[i].Row;
                        if (DR["MECRA_KODU"].ToString() == "") break;
                        SqlCommand myCmd = new SqlCommand();
                        myCmd.CommandTimeout = 0;
                        myCmd.CommandText = "INSERT INTO dbo.TRF_GAZETE(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU,SAYFA_GRUBU, HESAPLANMA_TURU, BIRIM_FIYAT,   BASLANGIC_TARIHI,BITIS_TARIHI) " +
                                           " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU,@SAYFA_GRUBU, @HESAPLANMA_TURU, @BIRIM_FIYAT,@BASLANGIC_TARIHI,@BITIS_TARIHI)";
                        myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                        myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                        myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                        myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "GAZETE";
                        myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                        myCmd.Parameters.Add("@SAYFA_GRUBU", SqlDbType.NVarChar); myCmd.Parameters["@SAYFA_GRUBU"].Value = DR["SAYFA_GRUBU"];
                        myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                        myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"];
                        myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                        myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                        myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                        myCmd.Connection = myConnection;
                        myCmd.ExecuteNonQuery(); 
                    }
                }

                if (GAZETE_TARIFE_SECENEK == "MECRA")
                { 
                    for (int i = 0; i < dv.Count; i++)
                    {
                        DataRow DR = dv[i].Row;
                        if (DR["MECRA_KODU"].ToString() == "") break;
                        SqlCommand myCmd = new SqlCommand();
                        myCmd.CommandTimeout = 0;
                        myCmd.CommandText = "INSERT INTO dbo.TRF_GAZETE(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU, HESAPLANMA_TURU, BIRIM_FIYAT,   BASLANGIC_TARIHI,BITIS_TARIHI) " +
                                           " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU, @HESAPLANMA_TURU, @BIRIM_FIYAT,@BASLANGIC_TARIHI,@BITIS_TARIHI)";
                        myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                        myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                        myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                        myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "GAZETE";
                        myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                        myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                        myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"];
                        myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                        myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                        myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                        myCmd.Connection = myConnection;
                        myCmd.ExecuteNonQuery();
                    }
                }
            }
        }
        public void GAZETE_ROW_UPDATE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_GAZETE SET     MECRA_KODU=@MECRA_KODU,SAYFA_GRUBU=@SAYFA_GRUBU,YAYIN_TURU=@YAYIN_TURU, HESAPLANMA_TURU=@HESAPLANMA_TURU,BIRIM_FIYAT=@BIRIM_FIYAT,PAZARTESI=@PAZARTESI,SALI=@SALI,CARSAMBA=@CARSAMBA,PERSEMBE=@PERSEMBE,CUMA=@CUMA,CUMARTESI=@CUMARTESI,PAZAR=@PAZAR ,BASLANGIC_TARIHI=@BASLANGIC_TARIHI,BITIS_TARIHI=@BITIS_TARIHI  where ID=@ID AND TARIFE_REF=@TARIFE_REF  " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];   
                string[,] strFieldNameInt = new string[,] { {     "MECRA_KODU", "SAYFA_GRUBU", "YAYIN_TURU", "HESAPLANMA_TURU"      } };  
                foreach (string strsInt in strFieldNameInt)
                {
                    if (DR.Table.Columns[strsInt] != null)
                    {
                        myCmd.Parameters.AddWithValue("@" + strsInt.ToString(), DR[strsInt] == DBNull.Value ? 0 : DR[strsInt]); 
                    }
                    else
                    { myCmd.Parameters.AddWithValue("@" + strsInt.ToString(), ""); }
                }


                string[,] strNames = new string[,] 
                {   
                    { "BIRIM_FIYAT","PAZARTESI","SALI","CARSAMBA", "PERSEMBE", "CUMA","CUMARTESI" ,"PAZAR"  }                   
                };
                int end = strNames.Length;
                for (int XZ = 0; XZ <= end - 1; XZ++)
                {
                   // if (DR[strNames[0, XZ]] == DBNull.Value) DR[strNames[0, XZ]] = 0; ///&& DR[strNames[0, XZ]]==string.Empty   

                    if (DR.Table.Columns[strNames[0, XZ]] != null)
                    {
                        myCmd.Parameters.AddWithValue("@" + strNames[0, XZ], DR[strNames[0, XZ]] == DBNull.Value ? 0 : DR[strNames[0, XZ]]);
                    }
                    else
                    { myCmd.Parameters.AddWithValue("@" + strNames[0, XZ], 0); }/// 

                } 
                //for (int XZ = 0; XZ <= end - 1; XZ++)
                //{
                //    if (Convert.ToDecimal(DR[strNames[0, 0]]) == Convert.ToDecimal(DR[strNames[0, XZ]]))
                //    { DR["FARKLI"] = 0; }
                //        else 
                //    { DR["FARKLI"] = 1; break; } 
                //}
                //myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"].ToString().Replace(".", ",");

                //myCmd.Parameters.Add("@PAZARTESI", SqlDbType.Float); myCmd.Parameters["@PAZARTESI"].Value = DR["PAZARTESI"].ToString().Replace(".", ",");
                //myCmd.Parameters.Add("@SALI", SqlDbType.Float); myCmd.Parameters["@SALI"].Value = DR["SALI"].ToString().Replace(".", ",");
                //myCmd.Parameters.Add("@CARSAMBA", SqlDbType.Float); myCmd.Parameters["@CARSAMBA"].Value = DR["CARSAMBA"].ToString().Replace(".", ",");
                //myCmd.Parameters.Add("@PERSEMBE", SqlDbType.Float); myCmd.Parameters["@PERSEMBE"].Value = DR["PERSEMBE"].ToString().Replace(".", ",");
                //myCmd.Parameters.Add("@CUMA", SqlDbType.Float); myCmd.Parameters["@CUMA"].Value = DR["CUMA"].ToString().Replace(".", ",");
                //myCmd.Parameters.Add("@CUMARTESI", SqlDbType.Float); myCmd.Parameters["@CUMARTESI"].Value = DR["CUMARTESI"].ToString().Replace(".", ",");
                //myCmd.Parameters.Add("@PAZAR", SqlDbType.Float); myCmd.Parameters["@PAZAR"].Value = DR["PAZAR"].ToString().Replace(".", ",");

                myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void GAZETE_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        { 
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand();
            myCmd.CommandText = "DELETE  dbo.TRF_GAZETE  where TARIFE_KODU=@TARIFE_KODU AND TARIFE_REF=@TARIFE_REF and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
            myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close(); 
        } 

        public void DERGI_ROW_DELETE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandText = "DELETE  dbo.TRF_DERGI  where ID=@ID AND TARIFE_REF=@TARIFE_REF  and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
                myCmd.Parameters.AddWithValue("@TARIFE_REF",   TARIFE_REF);
                myCmd.Parameters.AddWithValue("@ID",   DR["ID", DataRowVersion.Original]);
                myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
                myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA); 
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();  
        }
        public void DERGI_ROW_ADD(DataView dv, string TARIFE_KODU, string TARIFE_REF,string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["MECRA_KODU"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandTimeout = 0;
                    myCmd.CommandText = "INSERT INTO dbo.TRF_DERGI(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU,SAYFA_GRUBU, HESAPLANMA_TURU,BIRIM_FIYAT ,BASLANGIC_TARIHI,BITIS_TARIHI) " +
                                                    " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU,@SAYFA_GRUBU, @HESAPLANMA_TURU,@BIRIM_FIYAT,@BASLANGIC_TARIHI,@BITIS_TARIHI )";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = DR["MECRA_TURU"];
                    myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                    myCmd.Parameters.Add("@SAYFA_GRUBU", SqlDbType.NVarChar); myCmd.Parameters["@SAYFA_GRUBU"].Value = DR["SAYFA_GRUBU"];
                    myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                    myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                    myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"]; 
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery(); 
                }
            } myConnection.Close();

        }
        public void DERGI_ROW_UPDATE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand()           
                { CommandText = "UPDATE  dbo.TRF_DERGI SET     MECRA_KODU=@MECRA_KODU,SAYFA_GRUBU=@SAYFA_GRUBU, HESAPLANMA_TURU=@HESAPLANMA_TURU,   BIRIM_FIYAT=@BIRIM_FIYAT,BASLANGIC_TARIHI=@BASLANGIC_TARIHI,BITIS_TARIHI=BITIS_TARIHI   where ID=@ID   AND TARIFE_REF=@TARIFE_REF" };
                myCmd.CommandTimeout = 0;
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                myCmd.Parameters.Add("@SAYFA_GRUBU", SqlDbType.NVarChar); myCmd.Parameters["@SAYFA_GRUBU"].Value = DR["SAYFA_GRUBU"];
                myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"];
                myCmd.Parameters.Add("@TARIFE_REF", SqlDbType.Int); myCmd.Parameters["@TARIFE_REF"].Value = TARIFE_REF;
                myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void DERGI_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        { 
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand();
            myCmd.CommandText = "DELETE  dbo.TRF_DERGI  where TARIFE_KODU=@TARIFE_KODU AND TARIFE_REF=@TARIFE_REF and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
            myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close(); 
        }

         
        public void TELEVIZYON_ROW_DELETE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandTimeout = 0;
                myCmd.CommandText = "DELETE  dbo.TRF_TELEVIZYON  where ID=@ID AND TARIFE_REF=@TARIFE_REF  and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Parameters.AddWithValue("@ID", DR["ID", DataRowVersion.Original]);
                myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
                myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();  
            } myConnection.Close();
        }
        public void TELEVIZYON_ROW_ADD(DataView dv, string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;

                if (DR["MECRA_KODU"].ToString() != "")
                {
                 
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandTimeout = 0;
                    myCmd.CommandText = "INSERT INTO dbo.TRF_TELEVIZYON(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,  MECRA_KODU,SPOT_TIPI_DETAY, HESAPLANMA_TURU, TARGET, BIRIM_FIYAT, BASLANGIC_SAATI, BITIS_SAATI,OPT_PT,BASLANGIC_TARIHI,BITIS_TARIHI) " +
                    " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU,@SPOT_TIPI_DETAY, @HESAPLANMA_TURU, @TARGET, @BIRIM_FIYAT, @BASLANGIC_SAATI, @BITIS_SAATI,@OPT_PT,@BASLANGIC_TARIHI,@BITIS_TARIHI)";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "TELEVIZYON";
             
                    myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                    myCmd.Parameters.Add("@SPOT_TIPI_DETAY", SqlDbType.NVarChar); myCmd.Parameters["@SPOT_TIPI_DETAY"].Value = DR["SPOT_TIPI_DETAY"];
                    myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                    myCmd.Parameters.Add("@TARGET", SqlDbType.NVarChar); myCmd.Parameters["@TARGET"].Value = DR["TARGET"];
                    myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"] ==DBNull.Value   ? 0 :Convert.ToDouble(DR["BIRIM_FIYAT"].ToString().Replace(".", ","));
                    //   myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"].ToString().Replace(".", ",");

                    if (DR["BASLANGIC_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"].ToString().Substring(0, 5).ToString());
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"]);
                    }

                    if (DR["BITIS_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"].ToString().Substring(0, 5));
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"]);
                    }


                    //myCmd.Parameters.Add("@BASLANGIC_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BASLANGIC_SAATI"].Value = DR["BASLANGIC_SAATI"];
                    //myCmd.Parameters.Add("@BITIS_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BITIS_SAATI"].Value = DR["BITIS_SAATI"];
                    myCmd.Parameters.Add("@OPT_PT", SqlDbType.NVarChar); myCmd.Parameters["@OPT_PT"].Value = DR["OPT_PT"];
                    myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                    myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                    //   myCmd.Parameters.Add("@ORAN_ARTI_EKSI", SqlDbType.NVarChar); myCmd.Parameters["@ORAN_ARTI_EKSI"].Value = DR["ORAN_ARTI_EKSI"];
                    myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();
                }
            }
            myConnection.Close();
        }
        public void TELEVIZYON_ROW_UPDATE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["MECRA_KODU"].ToString() != "")
                {
                   
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandTimeout = 0;
                    myCmd.CommandText = "UPDATE  dbo.TRF_TELEVIZYON SET     MECRA_KODU=@MECRA_KODU, HESAPLANMA_TURU=@HESAPLANMA_TURU, TARGET=@TARGET, BIRIM_FIYAT=@BIRIM_FIYAT, BASLANGIC_SAATI=@BASLANGIC_SAATI, BITIS_SAATI=@BITIS_SAATI ,OPT_PT=@OPT_PT,BASLANGIC_TARIHI=@BASLANGIC_TARIHI,BITIS_TARIHI=@BITIS_TARIHI where ID=@ID AND TARIFE_REF=@TARIFE_REF ";
                    myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                    //  myCmd.Parameters.Add("@MNM", SqlDbType.NVarChar); myCmd.Parameters["@MNM"].Value = DR["MNM"];
                    myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                    myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                    myCmd.Parameters.Add("@TARGET", SqlDbType.NVarChar); myCmd.Parameters["@TARGET"].Value = DR["TARGET"];
                    //myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"];
                    myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"] == DBNull.Value ? 0 : Convert.ToDouble(DR["BIRIM_FIYAT"].ToString().Replace(".", ","));



                    if (DR["BASLANGIC_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"].ToString().Substring(0, 5).ToString());
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"]);
                    }

                    if (DR["BITIS_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"].ToString().Substring(0, 5));
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"]);
                    }




                    //myCmd.Parameters.Add("@BASLANGIC_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BASLANGIC_SAATI"].Value = DR["BASLANGIC_SAATI"];
                    //myCmd.Parameters.Add("@BITIS_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BITIS_SAATI"].Value = DR["BITIS_SAATI"];
                    myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                    myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                    //    myCmd.Parameters.Add("@ORAN_ARTI_EKSI", SqlDbType.NVarChar); myCmd.Parameters["@ORAN_ARTI_EKSI"].Value = DR["ORAN_ARTI_EKSI"];
                    myCmd.Parameters.Add("@OPT_PT", SqlDbType.NVarChar); myCmd.Parameters["@OPT_PT"].Value = DR["OPT_PT"];
                    myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();
                }

            } myConnection.Close();
        }
        public void TELEVIZYON_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        { 
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand();
            myCmd.CommandText = "DELETE  dbo.TRF_TELEVIZYON  where TARIFE_KODU=@TARIFE_KODU AND TARIFE_REF=@TARIFE_REF and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
            myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();

        }
        
        public void TELEVIZYON_NONE_MEASURED_ROW_DELETE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand(); 
                myCmd.CommandText = "DELETE  dbo.TRF_TELEVIZYON_NONE_MEASURED  where ID=@ID AND TARIFE_REF=@TARIFE_REF  and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Parameters.AddWithValue("@ID", DR["ID", DataRowVersion.Original]);
                myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
                myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA); 
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery(); 
            } myConnection.Close();
        }
        public void TELEVIZYON_NONE_MEASURED_ROW_ADD(DataView dv, string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["MECRA_KODU"].ToString() != "")
                {                 
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandTimeout = 0;
                    myCmd.CommandText = "INSERT INTO dbo.TRF_TELEVIZYON_NONE_MEASURED(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,  MECRA_KODU, HESAPLANMA_TURU, TARGET, GRP, BASLANGIC_SAATI, BITIS_SAATI,OPT_PT,BASLANGIC_TARIHI,BITIS_TARIHI) " +
                    " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU, @HESAPLANMA_TURU, @TARGET, @GRP, @BASLANGIC_SAATI, @BITIS_SAATI,@OPT_PT,@BASLANGIC_TARIHI,@BITIS_TARIHI)";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "TELEVIZYON";
                    // myCmd.Parameters.Add("@MNM", SqlDbType.NVarChar); myCmd.Parameters["@MNM"].Value = DR["MNM"];
                    myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                    myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                    myCmd.Parameters.Add("@TARGET", SqlDbType.NVarChar); myCmd.Parameters["@TARGET"].Value = DR["TARGET"];
                    //myCmd.Parameters.Add("@GRP", SqlDbType.Float); myCmd.Parameters["@GRP"].Value = DR["GRP"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@GRP", SqlDbType.Float); myCmd.Parameters["@GRP"].Value = DR["GRP"] == DBNull.Value ? 0 : Convert.ToDouble(DR["GRP"].ToString().Replace(".", ","));


                    if (DR["BASLANGIC_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"].ToString().Substring(0, 5).ToString());
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"]);
                    }

                    if (DR["BITIS_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"].ToString().Substring(0, 5));
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"]);
                    }


                    //myCmd.Parameters.Add("@BASLANGIC_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BASLANGIC_SAATI"].Value = DR["BASLANGIC_SAATI"];
                    //myCmd.Parameters.Add("@BITIS_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BITIS_SAATI"].Value = DR["BITIS_SAATI"];
                    myCmd.Parameters.Add("@OPT_PT", SqlDbType.NVarChar); myCmd.Parameters["@OPT_PT"].Value = DR["OPT_PT"];
                    myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                    myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                    //   myCmd.Parameters.Add("@ORAN_ARTI_EKSI", SqlDbType.NVarChar); myCmd.Parameters["@ORAN_ARTI_EKSI"].Value = DR["ORAN_ARTI_EKSI"];
                    myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();
                }
            }
            myConnection.Close();
        }
        public void TELEVIZYON_NONE_MEASURED_ROW_UPDATE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["MECRA_KODU"].ToString() != "")
                {                 
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandTimeout = 0;
                    myCmd.CommandText = "UPDATE  dbo.TRF_TELEVIZYON_NONE_MEASURED SET     MECRA_KODU=@MECRA_KODU, HESAPLANMA_TURU=@HESAPLANMA_TURU, TARGET=@TARGET, GRP=@GRP, BASLANGIC_SAATI=@BASLANGIC_SAATI, BITIS_SAATI=@BITIS_SAATI ,OPT_PT=@OPT_PT,BASLANGIC_TARIHI=@BASLANGIC_TARIHI,BITIS_TARIHI=@BITIS_TARIHI where ID=@ID AND TARIFE_REF=@TARIFE_REF ";
                    myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                    //  myCmd.Parameters.Add("@MNM", SqlDbType.NVarChar); myCmd.Parameters["@MNM"].Value = DR["MNM"];
                    myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                    myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                    myCmd.Parameters.Add("@TARGET", SqlDbType.NVarChar); myCmd.Parameters["@TARGET"].Value = DR["TARGET"];
                   // myCmd.Parameters.Add("@GRP", SqlDbType.Float); myCmd.Parameters["@GRP"].Value = DR["GRP"];
                    myCmd.Parameters.Add("@GRP", SqlDbType.Float); myCmd.Parameters["@GRP"].Value = DR["GRP"] == DBNull.Value ? 0 : Convert.ToDouble(DR["GRP"].ToString().Replace(".", ","));

                    if (DR["BASLANGIC_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"].ToString().Substring(0, 5).ToString());
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"]);
                    }

                    if (DR["BITIS_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"].ToString().Substring(0, 5));
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"]);
                    }


                    //myCmd.Parameters.Add("@BASLANGIC_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BASLANGIC_SAATI"].Value = DR["BASLANGIC_SAATI"];
                    //myCmd.Parameters.Add("@BITIS_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BITIS_SAATI"].Value = DR["BITIS_SAATI"];
                    myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                    myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                    //    myCmd.Parameters.Add("@ORAN_ARTI_EKSI", SqlDbType.NVarChar); myCmd.Parameters["@ORAN_ARTI_EKSI"].Value = DR["ORAN_ARTI_EKSI"];
                    myCmd.Parameters.Add("@OPT_PT", SqlDbType.NVarChar); myCmd.Parameters["@OPT_PT"].Value = DR["OPT_PT"];
                    myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();
                }
            } myConnection.Close();
        }
        public void TELEVIZYON_NONE_MEASURED_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        { 
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand();
            myCmd.CommandText = "DELETE  dbo.TRF_TELEVIZYON_NONE_MEASURED  where TARIFE_KODU=@TARIFE_KODU AND TARIFE_REF=@TARIFE_REF and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
            myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF); 
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close(); 
        }
        
        public void RADYO_ROW_DELETE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandTimeout = 0; 
                myCmd.CommandText = "DELETE  dbo.TRF_RADYO  where ID=@ID AND TARIFE_REF=@TARIFE_REF  and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Parameters.AddWithValue("@ID", DR["ID", DataRowVersion.Original]);
                myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
                myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA); 
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();

            } myConnection.Close();
        }
        public void RADYO_ROW_ADD(DataView dv, string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["MECRA_KODU"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandTimeout = 0;
                    myCmd.CommandText = "INSERT INTO dbo.TRF_RADYO(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,  MECRA_KODU, HESAPLANMA_TURU,  BIRIM_FIYAT, BASLANGIC_SAATI, BITIS_SAATI,OPT_PT,BASLANGIC_TARIHI,BITIS_TARIHI,ORAN_ARTI_EKSI) " +
                                                    " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU, @MECRA_KODU, @HESAPLANMA_TURU,  @BIRIM_FIYAT, @BASLANGIC_SAATI, @BITIS_SAATI,@OPT_PT,@BASLANGIC_TARIHI,@BITIS_TARIHI,@ORAN_ARTI_EKSI)";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;

                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;                    
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = DR["MECRA_TURU"];
                    //myCmd.Parameters.Add("@MNM", SqlDbType.NVarChar); myCmd.Parameters["@MNM"].Value = dr["MNM"];
                    myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                    myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                    //myCmd.Parameters.Add("@TARGET", SqlDbType.NVarChar); myCmd.Parameters["@TARGET"].Value = dr["TARGET"];
                    //myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"].ToString().Replace(".", ",");

                    myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"] == DBNull.Value ? 0 : Convert.ToDouble(DR["BIRIM_FIYAT"].ToString().Replace(".", ","));  
                    //myCmd.Parameters.Add("@BASLANGIC_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BASLANGIC_SAATI"].Value = DR["BASLANGIC_SAATI"];
                    //myCmd.Parameters.Add("@BITIS_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BITIS_SAATI"].Value = DR["BITIS_SAATI"];

                    if (DR["BASLANGIC_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"].ToString().Substring(0, 5).ToString());
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"]);
                    }

                    if (DR["BITIS_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"].ToString().Substring(0, 5));
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"]);
                    }


                    myCmd.Parameters.Add("@OPT_PT", SqlDbType.NVarChar); myCmd.Parameters["@OPT_PT"].Value = DR["OPT_PT"];
                    myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                    myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                    myCmd.Parameters.Add("@ORAN_ARTI_EKSI", SqlDbType.NVarChar); myCmd.Parameters["@ORAN_ARTI_EKSI"].Value = DR["ORAN_ARTI_EKSI"];
                    myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            } myConnection.Close();
        }
        public void RADYO_ROW_UPDATE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandTimeout = 0;
                myCmd.CommandText = "UPDATE  dbo.TRF_RADYO SET   MECRA_KODU=@MECRA_KODU, HESAPLANMA_TURU=@HESAPLANMA_TURU,  BIRIM_FIYAT=@BIRIM_FIYAT, BASLANGIC_SAATI=@BASLANGIC_SAATI, BITIS_SAATI=@BITIS_SAATI,OPT_PT=@OPT_PT,BASLANGIC_TARIHI=@BASLANGIC_TARIHI,BITIS_TARIHI=@BITIS_TARIHI where ID=@ID AND TARIFE_REF=@TARIFE_REF ";
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
              //  myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"];
                myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"] == DBNull.Value ? 0 : Convert.ToDouble(DR["BIRIM_FIYAT"].ToString().Replace(".", ","));

                if (DR["BASLANGIC_SAATI"].ToString().Length > 5)
                {
                    myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"].ToString().Substring(0, 5).ToString());
                }
                else
                {
                    myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"]);
                }

                if (DR["BITIS_SAATI"].ToString().Length > 5)
                {
                    myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"].ToString().Substring(0, 5));
                }
                else
                {
                    myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"]);
                }


                //myCmd.Parameters.Add("@BASLANGIC_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BASLANGIC_SAATI"].Value = DR["BASLANGIC_SAATI"];
                //myCmd.Parameters.Add("@BITIS_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BITIS_SAATI"].Value = DR["BITIS_SAATI"];
                myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                myCmd.Parameters.Add("@OPT_PT", SqlDbType.NVarChar); myCmd.Parameters["@OPT_PT"].Value = DR["OPT_PT"];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();

            } myConnection.Close();
        }
        public void RADYO_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand();
            myCmd.CommandText = "DELETE  dbo.TRF_RADYO  where TARIFE_KODU=@TARIFE_KODU AND TARIFE_REF=@TARIFE_REF and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Parameters.AddWithValue("@KULLANICI_KODU",  _KULLANICI_NAME);
            myCmd.Parameters.AddWithValue("@SIRKET_KODU",  _KULLANICI_FIRMA);
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }
         

        public void SINEMA_ROW_DELETE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandTimeout = 0; 
                myCmd.CommandText = "DELETE  dbo.TRF_SINEMA  where ID=@ID AND TARIFE_REF=@TARIFE_REF  and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Parameters.AddWithValue("@ID", DR["ID", DataRowVersion.Original]);
                myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
                myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA); 
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void SINEMA_ROW_ADD(DataView dv, string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["MECRA_KODU"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandTimeout = 0;
                    myCmd.CommandText = "INSERT INTO dbo.TRF_SINEMA(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU, HESAPLANMA_TURU, ILI, BIRIM_FIYAT, BOLGE,BASLANGIC_TARIHI,BITIS_TARIHI) " +
                    " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,   @MECRA_KODU, @HESAPLANMA_TURU, @ILI, @BIRIM_FIYAT, @BOLGE,@BASLANGIC_TARIHI,@BITIS_TARIHI)";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;                    
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "SINEMA";
                    myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                    myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                    myCmd.Parameters.Add("@ILI", SqlDbType.NVarChar); myCmd.Parameters["@ILI"].Value = DR["ILI"];                    
                    myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"] == DBNull.Value ? 0 : Convert.ToDouble(DR["BIRIM_FIYAT"].ToString().Replace(".", ",")); 
                    myCmd.Parameters.Add("@BOLGE", SqlDbType.NVarChar); myCmd.Parameters["@BOLGE"].Value = DR["BOLGE"];
                    myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                    myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                    myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            }
            myConnection.Close();
        }
        public void SINEMA_ROW_UPDATE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandTimeout = 0;
                myCmd.CommandText = "UPDATE  dbo.TRF_SINEMA SET    MECRA_KODU=@MECRA_KODU, HESAPLANMA_TURU=@HESAPLANMA_TURU, ILI=@ILI,BOLGE=@BOLGE, BIRIM_FIYAT=@BIRIM_FIYAT,BASLANGIC_TARIHI=@BASLANGIC_TARIHI,BITIS_TARIHI=@BITIS_TARIHI where ID=@ID AND TARIFE_REF=@TARIFE_REF ";
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];                
                myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                myCmd.Parameters.Add("@ILI", SqlDbType.NVarChar); myCmd.Parameters["@ILI"].Value = DR["ILI"];
                myCmd.Parameters.Add("@BOLGE", SqlDbType.NVarChar); myCmd.Parameters["@BOLGE"].Value = DR["BOLGE"];
                myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"] == DBNull.Value ? 0 : Convert.ToDouble(DR["BIRIM_FIYAT"].ToString().Replace(".", ",")); 
                myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void SINEMA_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand();
            myCmd.CommandText = "DELETE  dbo.TRF_SINEMA  where TARIFE_KODU=@TARIFE_KODU AND TARIFE_REF=@TARIFE_REF ";
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        
        public void INTERNET_ROW_DELETE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand(); 
                myCmd.CommandText = "DELETE  dbo.TRF_INTERNET  where ID=@ID AND TARIFE_REF=@TARIFE_REF  and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Parameters.AddWithValue("@ID", DR["ID", DataRowVersion.Original]);
                myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
                myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA); 
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void INTERNET_ROW_ADD(DataView dv, string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["R_NETWORK"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandTimeout = 0;
                    myCmd.CommandText = "INSERT INTO dbo.TRF_INTERNET(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,   R_NETWORK, HESAPLANMA_TURU, SITE, BIRIM_FIYAT ) " +
                    " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,   @R_NETWORK, @HESAPLANMA_TURU, @SITE, @BIRIM_FIYAT )";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;                    
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = DR["MECRA_TURU"];
                    myCmd.Parameters.Add("@R_NETWORK", SqlDbType.NVarChar); myCmd.Parameters["@R_NETWORK"].Value = DR["R_NETWORK"];
                    myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                    myCmd.Parameters.Add("@SITE", SqlDbType.NVarChar); myCmd.Parameters["@SITE"].Value = DR["SITE"];
                    myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"].ToString().Replace(".", ",");
                    myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();
                }
            }
            myConnection.Close();
        }
        public void INTERNET_ROW_UPDATE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandText = "UPDATE  dbo.TRF_INTERNET SET    R_NETWORK=@R_NETWORK, HESAPLANMA_TURU=@HESAPLANMA_TURU, SITE=@SITE, BIRIM_FIYAT=@BIRIM_FIYAT,    where ID=@ID AND TARIFE_REF=@TARIFE_REF ";
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                myCmd.Parameters.Add("@TARGET", SqlDbType.NVarChar); myCmd.Parameters["@TARGET"].Value = DR["TARGET"];
                myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void INTERNET_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand();
            myCmd.CommandText = "DELETE  dbo.TRF_INTERNET  where TARIFE_KODU=@TARIFE_KODU AND TARIFE_REF=@TARIFE_REF  and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU  ";
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
            myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);  
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }
        

        public void OUTDOOR_ROW_DELETE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();  
                myCmd.CommandText = "DELETE  dbo.TRF_OUTDOOR  where ID=@ID AND TARIFE_REF=@TARIFE_REF  and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Parameters.AddWithValue("@ID", DR["ID", DataRowVersion.Original]);
                myCmd.Parameters.AddWithValue("@KULLANICI_KODU",  _KULLANICI_NAME);
                myCmd.Parameters.AddWithValue("@SIRKET_KODU",  _KULLANICI_FIRMA); 
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();

            } myConnection.Close();
        }
        public void OUTDOOR_ROW_ADD(DataView dv, string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["MECRA_KODU"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandTimeout = 0;
                    myCmd.CommandText = "INSERT INTO dbo.TRF_OUTDOOR(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU, HESAPLANMA_TURU, ILI,ADRES,GENISLIK,YUKSEKLIK, BIRIM_FIYAT, UNITE,BASLANGIC_TARIHI,BITIS_TARIHI) " +
                    " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,   @MECRA_KODU, @HESAPLANMA_TURU, @ILI,@ADRES,@GENISLIK,@YUKSEKLIK, @BIRIM_FIYAT, @UNITE,@BASLANGIC_TARIHI,@BITIS_TARIHI)";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "OUTDOOR";
                    myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                    myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                    myCmd.Parameters.Add("@ILI", SqlDbType.NVarChar); myCmd.Parameters["@ILI"].Value = DR["ILI"];
                    myCmd.Parameters.Add("@ADRES", SqlDbType.NVarChar); myCmd.Parameters["@ADRES"].Value = DR["ADRES"];
                    myCmd.Parameters.Add("@GENISLIK", SqlDbType.NVarChar); myCmd.Parameters["@GENISLIK"].Value = DR["GENISLIK"];
                    myCmd.Parameters.Add("@YUKSEKLIK", SqlDbType.NVarChar); myCmd.Parameters["@YUKSEKLIK"].Value = DR["YUKSEKLIK"];
                    //   myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"] == DBNull.Value ? 0 : Convert.ToDouble(DR["BIRIM_FIYAT"].ToString().Replace(".", ",")); 
                    myCmd.Parameters.Add("@UNITE", SqlDbType.NVarChar); myCmd.Parameters["@UNITE"].Value = DR["UNITE"];
                    myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                    myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                    myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();
                }
            } myConnection.Close();

        }
        public void OUTDOOR_ROW_UPDATE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandTimeout = 0;
                myCmd.CommandText = "UPDATE  dbo.TRF_OUTDOOR SET    MECRA_KODU=@MECRA_KODU, HESAPLANMA_TURU=@HESAPLANMA_TURU, ILI=@ILI,ADRES=@ADRES,GENISLIK=@GENISLIK,YUKSEKLIK=@YUKSEKLIK, BIRIM_FIYAT=@BIRIM_FIYAT, UNITE=@UNITE,BASLANGIC_TARIHI=@BASLANGIC_TARIHI,BITIS_TARIHI=@BITIS_TARIHI  where ID=@ID AND TARIFE_REF=@TARIFE_REF ";
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                myCmd.Parameters.Add("@ILI", SqlDbType.NVarChar); myCmd.Parameters["@ILI"].Value = DR["ILI"];
                myCmd.Parameters.Add("@ADRES", SqlDbType.NVarChar); myCmd.Parameters["@ADRES"].Value = DR["ADRES"];
                myCmd.Parameters.Add("@GENISLIK", SqlDbType.NVarChar); myCmd.Parameters["@GENISLIK"].Value = DR["GENISLIK"];
                myCmd.Parameters.Add("@YUKSEKLIK", SqlDbType.NVarChar); myCmd.Parameters["@YUKSEKLIK"].Value = DR["YUKSEKLIK"];
                //   myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"];
                myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"] == DBNull.Value ? 0 : Convert.ToDouble(DR["BIRIM_FIYAT"].ToString().Replace(".", ",")); 
                myCmd.Parameters.Add("@UNITE", SqlDbType.NVarChar); myCmd.Parameters["@UNITE"].Value = DR["UNITE"];
                myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void OUTDOOR_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand();
            myCmd.CommandText = "DELETE  dbo.TRF_OUTDOOR  where TARIFE_KODU=@TARIFE_KODU AND TARIFE_REF=@TARIFE_REF   and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU  ";
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
            myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA); 
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }


          
        public void SEKTOR_ROW_DELETE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();  
                myCmd.CommandText = "DELETE  dbo.TRF_SEKTOR  where ID=@ID AND TARIFE_REF=@TARIFE_REF  and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Parameters.AddWithValue("@ID", DR["ID", DataRowVersion.Original]);
                myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
                myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA); 
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void SEKTOR_ROW_ADD(DataView dv, string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["SEKTOR"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandTimeout = 0;
                    myCmd.CommandText = "INSERT INTO dbo.TRF_SEKTOR(SIRKET_KODU, KULLANICI_KODU, TARIFE_KODU,TARIFE_REF, SEKTOR, HESAPLANMA_TURU, OCAK, SUBAT, MART, NISAN, MAYIS, HAZIRAN, TEMMUZ, AGUSTOS, EYLUL, EKIM, KASIM, ARALIK) " +
                    " VALUES ( @SIRKET_KODU, @KULLANICI_KODU, @TARIFE_KODU,@TARIFE_REF, @SEKTOR, @HESAPLANMA_TURU, @OCAK, @SUBAT, @MART, @NISAN, @MAYIS, @HAZIRAN, @TEMMUZ, @AGUSTOS, @EYLUL, @EKIM, @KASIM, @ARALIK)";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@SEKTOR", SqlDbType.NVarChar); myCmd.Parameters["@SEKTOR"].Value = DR["SEKTOR"];
                    myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                    myCmd.Parameters.Add("@OCAK", SqlDbType.Float); myCmd.Parameters["@OCAK"].Value = DR["OCAK"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@SUBAT", SqlDbType.Float); myCmd.Parameters["@SUBAT"].Value = DR["SUBAT"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@MART", SqlDbType.Float); myCmd.Parameters["@MART"].Value = DR["MART"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@NISAN", SqlDbType.Float); myCmd.Parameters["@NISAN"].Value = DR["NISAN"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@MAYIS", SqlDbType.Float); myCmd.Parameters["@MAYIS"].Value = DR["MAYIS"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@HAZIRAN", SqlDbType.Float); myCmd.Parameters["@HAZIRAN"].Value = DR["HAZIRAN"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@TEMMUZ", SqlDbType.Float); myCmd.Parameters["@TEMMUZ"].Value = DR["TEMMUZ"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@AGUSTOS", SqlDbType.Float); myCmd.Parameters["@AGUSTOS"].Value = DR["AGUSTOS"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@EYLUL", SqlDbType.Float); myCmd.Parameters["@EYLUL"].Value = DR["EYLUL"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@EKIM", SqlDbType.Float); myCmd.Parameters["@EKIM"].Value = DR["EKIM"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@KASIM", SqlDbType.Float); myCmd.Parameters["@KASIM"].Value = DR["KASIM"].ToString().Replace(".", ",");
                    myCmd.Parameters.Add("@ARALIK", SqlDbType.Float); myCmd.Parameters["@ARALIK"].Value = DR["ARALIK"].ToString().Replace(".", ",");
                    myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();
                }
            }
            myConnection.Close();
        }
        public void SEKTOR_ROW_UPDATE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandTimeout = 0;
                myCmd.CommandText = "UPDATE  dbo.TRF_SEKTOR SET SEKTOR=@SEKTOR, HESAPLANMA_TURU=@HESAPLANMA_TURU, OCAK=@OCAK, SUBAT=@SUBAT, MART=@MART, NISAN=@NISAN, MAYIS=@MAYIS, HAZIRAN=@HAZIRAN, TEMMUZ=@TEMMUZ, AGUSTOS=@AGUSTOS, EYLUL=@EYLUL, EKIM=@EKIM, KASIM=@KASIM, ARALIK=@ARALIK  where ID=@ID AND TARIFE_REF=@TARIFE_REF ";
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@SEKTOR", SqlDbType.NVarChar); myCmd.Parameters["@SEKTOR"].Value = DR["SEKTOR"];
                myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                myCmd.Parameters.Add("@OCAK", SqlDbType.Float); myCmd.Parameters["@OCAK"].Value = DR["OCAK"].ToString().Replace(".", ",");
                myCmd.Parameters.Add("@SUBAT", SqlDbType.Float); myCmd.Parameters["@SUBAT"].Value = DR["SUBAT"].ToString().Replace(".", ",");
                myCmd.Parameters.Add("@MART", SqlDbType.Float); myCmd.Parameters["@MART"].Value = DR["MART"].ToString().Replace(".", ",");
                myCmd.Parameters.Add("@NISAN", SqlDbType.Float); myCmd.Parameters["@NISAN"].Value = DR["NISAN"].ToString().Replace(".", ",");
                myCmd.Parameters.Add("@MAYIS", SqlDbType.Float); myCmd.Parameters["@MAYIS"].Value = DR["MAYIS"].ToString().Replace(".", ",");
                myCmd.Parameters.Add("@HAZIRAN", SqlDbType.Float); myCmd.Parameters["@HAZIRAN"].Value = DR["HAZIRAN"].ToString().Replace(".", ",");
                myCmd.Parameters.Add("@TEMMUZ", SqlDbType.Float); myCmd.Parameters["@TEMMUZ"].Value = DR["TEMMUZ"].ToString().Replace(".", ",");
                myCmd.Parameters.Add("@AGUSTOS", SqlDbType.Float); myCmd.Parameters["@AGUSTOS"].Value = DR["AGUSTOS"].ToString().Replace(".", ",");
                myCmd.Parameters.Add("@EYLUL", SqlDbType.Float); myCmd.Parameters["@EYLUL"].Value = DR["EYLUL"].ToString().Replace(".", ",");
                myCmd.Parameters.Add("@EKIM", SqlDbType.Float); myCmd.Parameters["@EKIM"].Value = DR["EKIM"].ToString().Replace(".", ",");
                myCmd.Parameters.Add("@KASIM", SqlDbType.Float); myCmd.Parameters["@KASIM"].Value = DR["KASIM"].ToString().Replace(".", ",");
                myCmd.Parameters.Add("@ARALIK", SqlDbType.Float); myCmd.Parameters["@ARALIK"].Value = DR["ARALIK"].ToString().Replace(".", ",");
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void SEKTOR_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand();            
            myCmd.CommandText = "DELETE  dbo.TRF_SEKTOR  where ID=@ID AND TARIFE_REF=@TARIFE_REF  and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);            
            myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
            myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }
        

        public void PROGRAM_ROW_DELETE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();                
                myCmd.CommandText = "DELETE  dbo.TRF_PROGRAM_TARIFESI  where ID=@ID AND TARIFE_REF=@TARIFE_REF  and KULLANICI_KODU=@KULLANICI_KODU and SIRKET_KODU=@SIRKET_KODU ";
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Parameters.AddWithValue("@ID", DR["ID", DataRowVersion.Original]);
                myCmd.Parameters.AddWithValue("@KULLANICI_KODU", _KULLANICI_NAME);
                myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();

            } myConnection.Close();
        }
        public void PROGRAM_ROW_ADD(DataView dv, string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["PROGRAM"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_PROGRAM_TARIFESI(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU,MECRA_TURU, MECRA_KODU,PROGRAM,PG_OZEL,TIPOLOJI,HESAPLANMA_TURU,TARGET,BIRIM_FIYAT,BASLANGIC_SAATI,BITIS_SAATI,OPT_PT,BASLANGIC_TARIHI,BITIS_TARIHI)  VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @MECRA_TURU,@MECRA_KODU,@PROGRAM,@PG_OZEL,@TIPOLOJI,@HESAPLANMA_TURU,@TARGET,@BIRIM_FIYAT,@BASLANGIC_SAATI, @BITIS_SAATI,@OPT_PT,@BASLANGIC_TARIHI,@BITIS_TARIHI)" };
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@TARIFE_REF", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_REF"].Value = TARIFE_REF;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME; 
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "PROGRAM";
                    myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                    myCmd.Parameters.Add("@PROGRAM", SqlDbType.NVarChar); myCmd.Parameters["@PROGRAM"].Value = DR["PROGRAM"];
                    myCmd.Parameters.Add("@PG_OZEL", SqlDbType.NVarChar); myCmd.Parameters["@PG_OZEL"].Value = DR["PG_OZEL"];
                    myCmd.Parameters.Add("@TIPOLOJI", SqlDbType.NVarChar); myCmd.Parameters["@TIPOLOJI"].Value = DR["TIPOLOJI"]; 
                    myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                    myCmd.Parameters.Add("@TARGET", SqlDbType.NVarChar); myCmd.Parameters["@TARGET"].Value = DR["TARGET"];
                    myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"] == DBNull.Value ? 0 : Convert.ToDouble(DR["BIRIM_FIYAT"].ToString().Replace(".", ","));

                    if (DR["BASLANGIC_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"].ToString().Substring(0, 5).ToString());
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"]);
                    }

                    if (DR["BITIS_SAATI"].ToString().Length > 5)
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"].ToString().Substring(0, 5));
                    }
                    else
                    {
                        myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"]);
                    } 
 
                    myCmd.Parameters.Add("@OPT_PT", SqlDbType.NVarChar); myCmd.Parameters["@OPT_PT"].Value = DR["OPT_PT"];


                    myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                    myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"]; 
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            } myConnection.Close();
        }
        public void PROGRAM_ROW_UPDATE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandText = "UPDATE  dbo.TRF_PROGRAM_TARIFESI SET   PROGRAM=@PROGRAM,PG_OZEL=@PG_OZEL,  TIPOLOJI=@TIPOLOJI,  HESAPLANMA_TURU=@HESAPLANMA_TURU,TARGET=@TARGET,BIRIM_FIYAT=@BIRIM_FIYAT,BASLANGIC_SAATI=@BASLANGIC_SAATI, BITIS_SAATI=@BITIS_SAATI ,OPT_PT=@OPT_PT,BASLANGIC_TARIHI=@BASLANGIC_TARIHI,BITIS_TARIHI=@BITIS_TARIHI where ID=@ID AND TARIFE_REF=@TARIFE_REF ";
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@PROGRAM", SqlDbType.NVarChar); myCmd.Parameters["@PROGRAM"].Value = DR["PROGRAM"];
                myCmd.Parameters.Add("@PG_OZEL", SqlDbType.NVarChar); myCmd.Parameters["@PG_OZEL"].Value = DR["PG_OZEL"];
                myCmd.Parameters.Add("@TIPOLOJI", SqlDbType.NVarChar); myCmd.Parameters["@TIPOLOJI"].Value = DR["TIPOLOJI"];
                myCmd.Parameters.Add("@HESAPLANMA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@HESAPLANMA_TURU"].Value = DR["HESAPLANMA_TURU"];
                myCmd.Parameters.Add("@TARGET", SqlDbType.NVarChar); myCmd.Parameters["@TARGET"].Value = DR["TARGET"];
                myCmd.Parameters.Add("@BIRIM_FIYAT", SqlDbType.Float); myCmd.Parameters["@BIRIM_FIYAT"].Value = DR["BIRIM_FIYAT"] == DBNull.Value ? 0 : Convert.ToDouble(DR["BIRIM_FIYAT"].ToString().Replace(".", ","));

                if (DR["BASLANGIC_SAATI"].ToString().Length > 5)
                {
                    myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"].ToString().Substring(0, 5).ToString());
                }
                else
                {
                    myCmd.Parameters.AddWithValue("@BASLANGIC_SAATI", DR["BASLANGIC_SAATI"]);
                }

                if (DR["BITIS_SAATI"].ToString().Length > 5)
                {
                    myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"].ToString().Substring(0, 5));
                }
                else
                {
                    myCmd.Parameters.AddWithValue("@BITIS_SAATI", DR["BITIS_SAATI"]);
                }

                myCmd.Parameters.Add("@OPT_PT", SqlDbType.NVarChar); myCmd.Parameters["@OPT_PT"].Value = DR["OPT_PT"];


                myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();

            } myConnection.Close();
        }
        public void PROGRAM_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_PROGRAM_TARIFESI  where TARIFE_KODU=@TARIFE_KODU AND TARIFE_REF=@TARIFE_REF  " };
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }
                
        public void ORANLAR_ROW_DELETE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandText = "DELETE  dbo.TRF_ORANLAR  where ID=@ID AND TARIFE_REF=@TARIFE_REF ";
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID", DataRowVersion.Original];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();

            } myConnection.Close();
        }
        public void ORANLAR_ROW_ADD(DataView dv, string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["YAYIN_SINIFI"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_ORANLAR(SIRKET_KODU, TARIFE_KODU,TARIFE_REF,KULLANICI_KODU, YAYIN_SINIFI,BASLANGIC_TARIHI,BITIS_TARIHI,ORAN_ARTI_EKSI) " + " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF,@KULLANICI_KODU, @YAYIN_SINIFI, @BASLANGIC_TARIHI,@BITIS_TARIHI,@ORAN_ARTI_EKSI)" };
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _KULLANICI_NAME;
                    myCmd.Parameters.Add("@YAYIN_SINIFI", SqlDbType.NVarChar); myCmd.Parameters["@YAYIN_SINIFI"].Value = DR["YAYIN_SINIFI"];
                    myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                    myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                    myCmd.Parameters.Add("@ORAN_ARTI_EKSI", SqlDbType.NVarChar); myCmd.Parameters["@ORAN_ARTI_EKSI"].Value = DR["ORAN_ARTI_EKSI"];
                    myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            } myConnection.Close();
        }
        public void ORANLAR_ROW_UPDATE(DataView dv, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_ORANLAR SET   YAYIN_SINIFI=@YAYIN_SINIFI,  ORAN_ARTI_EKSI=@ORAN_ARTI_EKSI,  BASLANGIC_TARIHI=@BASLANGIC_TARIHI,BITIS_TARIHI=@BITIS_TARIHI where ID=@ID AND TARIFE_REF=@TARIFE_REF " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@YAYIN_SINIFI", SqlDbType.NVarChar); myCmd.Parameters["@YAYIN_SINIFI"].Value = DR["YAYIN_SINIFI"];
                myCmd.Parameters.Add("@ORAN_ARTI_EKSI", SqlDbType.Float); myCmd.Parameters["@ORAN_ARTI_EKSI"].Value = DR["ORAN_ARTI_EKSI"];
                myCmd.Parameters.Add("@BASLANGIC_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BASLANGIC_TARIHI"].Value = DR["BASLANGIC_TARIHI"];
                myCmd.Parameters.Add("@BITIS_TARIHI", SqlDbType.SmallDateTime); myCmd.Parameters["@BITIS_TARIHI"].Value = DR["BITIS_TARIHI"];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();

            } myConnection.Close();
        }
        public void ORANLAR_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU, string TARIFE_REF, string _KULLANICI_FIRMA, string _KULLANICI_NAME)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand();
            myCmd.CommandText = "DELETE  dbo.TRF_ORANLAR  where TARIFE_KODU=@TARIFE_KODU ";
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }
        

        public void MASTER_ROW_ADD(SqlConnection Con, DataRow row, string TARIFE_KODU, string TARIFE_REF)
        {
            string StrField = null;
            string StrParaMeters = null;
            SqlCommand myCmd = new SqlCommand();
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                if (row[i] != DBNull.Value)
                {
                    myCmd.Parameters.AddWithValue("@" + row.Table.Columns[i].ColumnName, row[i]); 
                    StrField = StrField + "[" + row.Table.Columns[i].ColumnName + "],";
                    StrParaMeters = StrParaMeters + "@" + row.Table.Columns[i].ColumnName + ",";
                }
            }
            foreach (SqlParameter parameter in myCmd.Parameters)
            {
                if (parameter.Value == null)
                {
                    parameter.Value = DBNull.Value;
                }
            }
            if (StrField.Length > 0) StrField = StrField.Substring(0, StrField.Length - 1);
            if (StrParaMeters.Length > 0) StrParaMeters = StrParaMeters.Substring(0, StrParaMeters.Length - 1); 
            myCmd.CommandText = " INSERT INTO  [dbo].[__MAS_EDT_" + TARIFE_REF + "_" + TARIFE_KODU + "] (" + StrField + ") VALUES (" + StrParaMeters + ") ";
            myCmd.Connection = Con;
            myCmd.ExecuteNonQuery();
        }
        public void MASTER_ROW_DELETE(SqlConnection Con, DataRow row,   string TARIFE_KODU, string TARIFE_REF)
        { 
            SqlCommand myCmd = new SqlCommand();  
            myCmd.Parameters.AddWithValue("@ID", row["ID",DataRowVersion.Original]);   
            myCmd.CommandText = "DELETE [dbo].[__MAS_EDT_" + TARIFE_REF + "_" + TARIFE_KODU + "]  where  ID=@ID and TARIFE_REF=@TARIFE_REF ";
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Connection = Con;
            myCmd.ExecuteNonQuery(); 
        }
        public void MASTER_ROW_UPDATE(SqlConnection Con, DataRow row,   string TARIFE_KODU, string TARIFE_REF)
        {
            string StrField = null;
            //string StrParaMeters = null;
            SqlCommand myCmd = new SqlCommand();
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                if (row[i] != DBNull.Value && row.Table.Columns[i].ColumnName!="ID" && row.Table.Columns[i].ColumnName != "NEW")
                {
                    myCmd.Parameters.AddWithValue("@" + row.Table.Columns[i].ColumnName, row[i]);
                    StrField = StrField + "[" + row.Table.Columns[i].ColumnName + "] = @" + row.Table.Columns[i].ColumnName + ",";  
                }
            } 

            myCmd.Parameters.AddWithValue("@ID", row["ID"]); 
            foreach (SqlParameter parameter in myCmd.Parameters)
            {
                if (parameter.Value == null)
                {
                    parameter.Value = DBNull.Value;
                }
            }
            if (StrField.Length > 0) StrField = StrField.Substring(0, StrField.Length - 1); 
            myCmd.CommandText = "UPDATE   [dbo].[__MAS_EDT_" + TARIFE_REF + "_" + TARIFE_KODU + "] SET " + StrField + "  where  ( ID=@ID )";  
            myCmd.Connection = Con;
            myCmd.ExecuteNonQuery();
        }

        

        public void VERSIYON_ROW_ADD(SqlConnection Con, DataRow row, string TARIFE_KODU, string TARIFE_REF)
        {
            string StrField = null;
            string StrParaMeters = null;
            SqlCommand myCmd = new SqlCommand();
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                if (row[i] != DBNull.Value)
                {
                    myCmd.Parameters.AddWithValue("@" + row.Table.Columns[i].ColumnName, row[i]);
                    StrField = StrField + "[" + row.Table.Columns[i].ColumnName + "],";
                    StrParaMeters = StrParaMeters + "@" + row.Table.Columns[i].ColumnName + ",";
                }
            }
            foreach (SqlParameter parameter in myCmd.Parameters)
            {
                if (parameter.Value == null)
                {
                    parameter.Value = DBNull.Value;
                }
            }
            if (StrField.Length > 0) StrField = StrField.Substring(0, StrField.Length - 1);
            if (StrParaMeters.Length > 0) StrParaMeters = StrParaMeters.Substring(0, StrParaMeters.Length - 1);
            myCmd.CommandText = " INSERT INTO  [dbo].[__MAS_EDT_GENEL_VERSION] (" + StrField + ") VALUES (" + StrParaMeters + ") ";
            myCmd.Connection = Con;
            myCmd.ExecuteNonQuery();
        }
        public void VERSIYON_ROW_DELETE(SqlConnection Con, DataRow row, string TARIFE_KODU, string TARIFE_REF)
        {
            SqlCommand myCmd = new SqlCommand();
            myCmd.Parameters.AddWithValue("@ID", row["ID", DataRowVersion.Original]);
            myCmd.CommandText = "DELETE [dbo].[__MAS_EDT_GENEL_VERSION]  where  ID=@ID and TARIFE_REF=@TARIFE_REF ";
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Connection = Con;
            myCmd.ExecuteNonQuery();
        }
        public void VERSIYON_ROW_UPDATE(SqlConnection Con, DataRow row, string TARIFE_KODU, string TARIFE_REF)
        {
            string StrField = null; 
            SqlCommand myCmd = new SqlCommand();
            for (int i = 0; i < row.Table.Columns.Count; i++)
            {
                if (row[i] != DBNull.Value && row.Table.Columns[i].ColumnName != "ID")
                {
                    myCmd.Parameters.AddWithValue("@" + row.Table.Columns[i].ColumnName, row[i]);
                    StrField = StrField + "[" + row.Table.Columns[i].ColumnName + "] = @" + row.Table.Columns[i].ColumnName + ",";
                }
            } 
            myCmd.Parameters.AddWithValue("@ID", row["ID"]);
            foreach (SqlParameter parameter in myCmd.Parameters)
            {
                if (parameter.Value == null)
                {
                    parameter.Value = DBNull.Value;
                }
            }
            if (StrField.Length > 0) StrField = StrField.Substring(0, StrField.Length - 1);
            myCmd.CommandText = "UPDATE   [dbo].[__MAS_EDT_GENEL_VERSION] SET " + StrField + "  where  ( ID=@ID )";
            myCmd.Connection = Con;
            myCmd.ExecuteNonQuery();
        }

       

        public void KEYWORD_ROW_ADD(SqlConnection Con, DataRow row,  string TARIFE_KODU, string TARIFE_REF)
        {  
            if (row["ALANLAR"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand(); 
                    myCmd.CommandText = "INSERT INTO dbo.TRF_KEYWORD(SIRKET_KODU, TARIFE_KODU, TARIFE_REF,  ALANLAR,KEYWORDS,ALAN,DEGER)  VALUES ( @SIRKET_KODU, @TARIFE_KODU,@TARIFE_REF, @ALANLAR, @KEYWORDS,@ALAN,@DEGER)";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU; 
                    myCmd.Parameters.Add("@ALANLAR", SqlDbType.NVarChar); myCmd.Parameters["@ALANLAR"].Value = row["ALANLAR"];
                    myCmd.Parameters.Add("@KEYWORDS", SqlDbType.NVarChar); myCmd.Parameters["@KEYWORDS"].Value = row["KEYWORDS"];
                    myCmd.Parameters.Add("@ALAN", SqlDbType.NVarChar); myCmd.Parameters["@ALAN"].Value = row["ALAN"];
                    myCmd.Parameters.Add("@DEGER", SqlDbType.NVarChar); myCmd.Parameters["@DEGER"].Value = row["DEGER"];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = Con;
                    myCmd.ExecuteNonQuery(); 
                } 
        }
        public void KEYWORD_ROW_DELETE(SqlConnection Con, DataRow row,  string TARIFE_KODU, string TARIFE_REF)
        {
            SqlCommand myCmd = new SqlCommand();
            myCmd.Parameters.AddWithValue("@ID", row["ID"]);
            myCmd.CommandText = "DELETE [dbo].[TRF_KEYWORD]  where  ID=@ID and TARIFE_REF=@TARIFE_REF ";
            myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
            myCmd.Connection = Con;
            myCmd.ExecuteNonQuery();
        }
        public void KEYWORD_ROW_UPDATE(SqlConnection Con, DataRow row,  string TARIFE_KODU, string TARIFE_REF)
        { 
            if (row["ALANLAR"].ToString() != "")
            {
                SqlCommand myCmd = new SqlCommand();
                myCmd.CommandText = "UPDATE   dbo.TRF_KEYWORD  SET    ALANLAR=@ALANLAR,KEYWORDS=@KEYWORDS,ALAN=@ALAN,DEGER=@DEGER WHERE    SIRKET_KODU=@SIRKET_KODU AND TARIFE_KODU=@TARIFE_KODU AND ID=@ID and TARIFE_REF=@TARIFE_REF ";
                myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                myCmd.Parameters.Add("@ALANLAR", SqlDbType.NVarChar); myCmd.Parameters["@ALANLAR"].Value = row["ALANLAR"];
                myCmd.Parameters.Add("@KEYWORDS", SqlDbType.NVarChar); myCmd.Parameters["@KEYWORDS"].Value = row["KEYWORDS"];
                myCmd.Parameters.Add("@ALAN", SqlDbType.NVarChar); myCmd.Parameters["@ALAN"].Value = row["ALAN"];
                myCmd.Parameters.Add("@DEGER", SqlDbType.NVarChar); myCmd.Parameters["@DEGER"].Value = row["DEGER"]; 
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = row["ID"];
                myCmd.Parameters.AddWithValue("@TARIFE_REF", TARIFE_REF);
                myCmd.Connection = Con;
                myCmd.ExecuteNonQuery(); 
            }  
        }  
    }
}
