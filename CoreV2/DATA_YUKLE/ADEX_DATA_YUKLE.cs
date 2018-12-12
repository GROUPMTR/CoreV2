using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Diagnostics;
using System.IO;

namespace CoreV2.DATA_YUKLE
{
    public partial class ADEX_DATA_YUKLE : DevExpress.XtraEditors.XtraForm
    {
        public ADEX_DATA_YUKLE()
        {
            InitializeComponent();
            RAPOR_LIST_READ();
        }

        private void BTN_FILE_SELECT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                BTN_FILE_SELECT.EditValue = file.FileName;
             //   ReadArrayFromFile(file.FileName);
            }
        }



        static string[] ReadArrayFromFile(string fileName)
         {
            DataTable csvData = new DataTable();
            string[] result = null;
            
            using (StreamReader reader = new StreamReader(fileName))
            {

                result = reader.ReadLine().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                int col = 0;
                foreach (string column in result)
                {
                    col++;
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn + col.ToString());
                }



                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    csvData.Rows.Add(result);

                }
            }


            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(cn))
                {
                    copy.ColumnMappings.Clear();
                    for (int i = 0; i <= csvData.Columns.Count - 1; i++)
                    {
                        copy.ColumnMappings.Add(i, i);
                    }
                    copy.DestinationTableName = "_TEMP_ADEX_DATA";
                    copy.BulkCopyTimeout = 0;
                    //for (int i = 0; i <= csvData.Columns.Count - 1; i++)
                    //{
                    //    if (dvAcik.Table.Columns[i].DataType.ToString() == "System.DateTime")
                    //    {
                    //        for (int ix = 0; ix <= TRADEDATESET.Tables[0].Rows.Count - 1; ix++)
                    //        {
                    //            if (TRADEDATESET.Tables[0].Rows[ix][i] != DBNull.Value)
                    //            {
                    //                TRADEDATESET.Tables[0].Rows[ix][i] = TRADEDATESET.Tables[0].Rows[ix][i].ToString().Replace("30.12.1899", "").Replace("1.1.1900", "").Replace("01.01.1900", "").Replace("1900-01-01", "");
                    //            }
                    //        }
                    //    }
                    //}
                    copy.WriteToServer(csvData);
                }
            }


            using (var reader = new StreamReader(fileName)) 
             { 
               //  var size = Convert.ToInt32(reader.ReadLine()); 
                 result = reader.ReadLine() 
                         .Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                int col = 0;
                foreach (string column in result)
                {
                    col++;
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn+col.ToString());
                }


                //Making empty value as null
                for (int i = 0; i < result.Length; i++)
                {
                    if (result[i] == "")
                    {
                        result[i] = null;
                    }
                }
                csvData.Rows.Add(result);


            }
            return result; 
         }

        private void BTN_YUKLE_Click(object sender, EventArgs e)
        {
            if (CHK_TRUNCATE_TABLE.Checked)
            {
                using (SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    myConnectionTable.Open();
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandText = " truncate table   dbo._TEMP_ADEX_DATA ";
                    myCmd.Connection = myConnectionTable;
                    myCmd.ExecuteNonQuery();
                    myConnectionTable.Close();
                }
                TXT_BX_LOG.Text += " ADEX Temp table Temizlendi " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
             
            }

      

            Stopwatch sw = new Stopwatch();
            sw.Start();
            //SqlConnection conn;
            //SqlCommand command;
            //conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING);
            //command = new SqlCommand();
            //conn.Open();
            //command.Connection = conn;
            //command.CommandText = " BULK INSERT _TEMP_ADEX_DATA FROM '" + BTN_FILE_SELECT.EditValue + "' WITH (DATAFILETYPE ='WIDECHAR',FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\r\n', CODEPAGE = '1254')";
            //command.CommandTimeout = 0;
            //command.ExecuteNonQuery();
            
            DataTable csvData = new DataTable();
            string[] result = null;
            if (BTN_FILE_SELECT.EditValue == null) return;
            using (StreamReader reader = new StreamReader(BTN_FILE_SELECT.EditValue.ToString ()))
            {
                result = reader.ReadLine().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                int col = 0;
                foreach (string column in result)
                {
                    col++;
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn + col.ToString());
                }
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    csvData.Rows.Add(result);
                }
                reader.Close();
            }
            TXT_BX_LOG.Text += " ADEX table yapısı okunuyor " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(cn))
                {
                    copy.ColumnMappings.Clear();
                    for (int i = 0; i <= csvData.Columns.Count - 1; i++)
                    {
                        copy.ColumnMappings.Add(i, i);
                    }
                    copy.BulkCopyTimeout = 0;
                    copy.DestinationTableName = "_TEMP_ADEX_DATA";                  
                    copy.WriteToServer(csvData);
                }
            }

            TXT_BX_LOG.Text += " ADEX Temp table data yüklendi " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {   
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_MECRA_KODU_KONTROL ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                myConnectionTable.Open();
                while (reader.Read())
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.Parameters.AddWithValue("@MECRA_KODU", reader["MECRA_KODU"]);
                    myCmd.Parameters.AddWithValue("@MECRA_KODU_CONVERT", reader["MECRA_KODU_CONVERT"]);
                    myCmd.CommandText = " UPDATE   dbo._TEMP_ADEX_DATA SET MEDYA= @MECRA_KODU_CONVERT   WHERE MEDYA=@MECRA_KODU  ";
                    myCmd.Connection = myConnectionTable;
                    myCmd.ExecuteNonQuery();
                }
                myConnectionTable.Close();
                reader.Close();
            }


            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand("SELECT   top 1000 * FROM  dbo._TEMP_ADEX_DATA", conn) };
                adapter.Fill(ds, "_TEMP_ADEX_DATA");
                DataViewManager dvManager = new DataViewManager(ds);
                DataView _TEMP_ADEX_DATA = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_DATA.DataSource = _TEMP_ADEX_DATA;
            }

            TXT_BX_LOG.Text += " ADEX Kanal İsimleri düzeltildi " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
             

            sw.Stop();

            MessageBox.Show((sw.ElapsedMilliseconds / 1000.00).ToString());
        }

        private void RAPOR_LIST_READ()
        {
            string SQL = " SELECT * FROM dbo.ADM_MECRA_KODU_KONTROL  ";
 

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlDataAdapter da = new SqlDataAdapter(SQL, myConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "stock");
            DataViewManager dvManager = new DataViewManager(ds);
            DataView dv = dvManager.CreateDataView(ds.Tables[0]);
            gridControl_LISTE.DataSource = dv;
        }

        private void BTN_CHANNEL_KONTROL_Click(object sender, EventArgs e)
        {
            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                string queryString = "SELECT * FROM dbo.ADM_MECRA_KODU_KONTROL ";
                SqlCommand commands = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                myConnectionTable.Open();
                while (reader.Read())
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.Parameters.AddWithValue("@MECRA_KODU", reader["MECRA_KODU"]);
                    myCmd.Parameters.AddWithValue("@MECRA_KODU_CONVERT", reader["MECRA_KODU_CONVERT"]);
                    myCmd.CommandText = " UPDATE   dbo._TEMP_ADEX_DATA SET ANA_YAYIN=@MECRA_KODU_CONVERT ,MEDYA= @MECRA_KODU_CONVERT   WHERE MEDYA=@MECRA_KODU  ";
                    myCmd.Connection = myConnectionTable;
                    myCmd.ExecuteNonQuery();
                }
                myConnectionTable.Close();
                reader.Close();
            }
        }

        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void TOGGLE_OZET_FILITRE_Toggled(object sender, EventArgs e)
        {

        }

        private void BTN_SQL_YUKLE_Click(object sender, EventArgs e)
        {
            DATA_MANAGER DT = new DATA_YUKLE.DATA_MANAGER();



            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                string queryString = "SELECT * FROM dbo.ADM_MECRA_KODU_KONTROL ";
                SqlCommand commands = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                myConnectionTable.Open();
                while (reader.Read())
                {
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.Parameters.AddWithValue("@MECRA_KODU", reader["MECRA_KODU"]);
                    myCmd.Parameters.AddWithValue("@MECRA_KODU_CONVERT", reader["MECRA_KODU_CONVERT"]);
                    myCmd.CommandText = " UPDATE   dbo._TEMP_ADEX_DATA SET ANA_YAYIN=@MECRA_KODU_CONVERT ,MEDYA= @MECRA_KODU_CONVERT   WHERE MEDYA=@MECRA_KODU  ";
                    myCmd.Connection = myConnectionTable;
                    myCmd.ExecuteNonQuery();
                }
                myConnectionTable.Close();
                reader.Close();
            }

            TXT_BX_LOG.Text += " ADEX Kanal İsimleri düzeltildi " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

            if (CHK_VERIKONTROLU.Checked)
            {
                TXT_BX_LOG.Text += " Tarhi Kontrolu Yapılıyor   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

                using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
                {
                    SqlCommand commands = new SqlCommand("SELECT MIN(CONVERT(datetime, TARIH, 104)) AS MIN, MAX(CONVERT(datetime, TARIH, 104)) AS MAX, YAYIN_SINIFI FROM dbo._TEMP_ADEX_DATA GROUP BY YAYIN_SINIFI", connection);
                    connection.Open();
                    SqlDataReader reader = commands.ExecuteReader();
                    SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                    myConnectionTable.Open();
                    while (reader.Read())
                    {
                        TXT_BX_LOG.Text += reader["MIN"].ToString() + "-" + reader["MIN"].ToString() + " " + reader["YAYIN_SINIFI"].ToString() + " Tarhi Kontrolu Yapılıyor   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

                        string TABLES = "";
                        SqlCommand myCmd = new SqlCommand();
                        myCmd.Parameters.AddWithValue("@MINTARIH", reader["MIN"]);
                        myCmd.Parameters.AddWithValue("@MAXTARIH", reader["MAX"]);
                        myCmd.Parameters.AddWithValue("@YAYIN_SINIFI", reader["YAYIN_SINIFI"]);
                        if (reader["YAYIN_SINIFI"].ToString() == "DERGI") TABLES = "dbo.[DATA_BASIN_ADEX]";
                        if (reader["YAYIN_SINIFI"].ToString() == "GAZETE") TABLES = "dbo.[DATA_BASIN_ADEX]";
                        if (reader["YAYIN_SINIFI"].ToString() == "OUTDOOR") TABLES = "dbo.[DATA_OUTDOOR_ADEX]";
                        if (reader["YAYIN_SINIFI"].ToString() == "RADYO") TABLES = "dbo.[DATA_RADYO_ADEX]";
                        if (reader["YAYIN_SINIFI"].ToString() == "SINEMA") TABLES = "dbo.[DATA_SINEMA_ADEX]";
                        if (reader["YAYIN_SINIFI"].ToString() == "TELEVIZYON") TABLES = "dbo.[DATA_TELEVIZYON_ADEX_2018]";
                        if (reader["YAYIN_SINIFI"].ToString() == "YEREL TV") TABLES = "dbo.[DATA_TELEVIZYON_ADEX_2018]";
                        myCmd.CommandText = " DELETE " + TABLES + " WHERE TARIH>=@MINTARIH and TARIH<=@MAXTARIH  and YAYIN_SINIFI=@YAYIN_SINIFI ";
                        myCmd.Connection = myConnectionTable;
                        myCmd.ExecuteNonQuery();
                    }
                    myConnectionTable.Close();
                    reader.Close();
                }
                TXT_BX_LOG.Text += " Tarhi Kontrolu Tamamlandı   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            }
            TXT_BX_LOG.Text += " Telvizyon datası yükleniyor   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            if (CHK_TELEVIZYON.Checked)
            {
                DT.TELEVIZYON_ROW_ADD();
                TXT_BX_LOG.Text += " Telvizyon datası yükleniyor   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                TXT_BX_LOG.Text += " Dergi datası yükleniyor   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            }
            if (CHK_DERGI.Checked)
            {
                DT.DERGI_ROW_ADD();
                TXT_BX_LOG.Text += " Dergi datası Yüklendi   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                TXT_BX_LOG.Text += " Gazete datası yükleniyor   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            }
            if (CHK_GAZETE.Checked)
            {
                DT.GAZETE_ROW_ADD();
                TXT_BX_LOG.Text += " Gazete datası Yüklendi   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                TXT_BX_LOG.Text += " Radyo datası yükleniyor   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            }
            if (CHK_RADYO.Checked)
            {
                DT.RADYO_ROW_ADD();
                TXT_BX_LOG.Text += " Radyo datası Yüklendi   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                TXT_BX_LOG.Text += " Sinema datası yükleniyor   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            }
            if (CHK_SINEMA.Checked)
            {
                DT.SINEMA_ROW_ADD();
                TXT_BX_LOG.Text += " Sinema Datası Yüklendi    " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                TXT_BX_LOG.Text += " Outdoor Datası  yükleniyor   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            }
            if (CHK_OUTDOOR.Checked)
            {
                DT.OUTDOOR_ROW_ADD();
                TXT_BX_LOG.Text += " Outdoor Datası Yüklendi   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
                TXT_BX_LOG.Text += " GRP Datası ADEX Convert ediliyor   " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            }

            if (CHK_GRP.Checked)
            {
                DT.GRP_MACH_TELEVIZYON_UPDATE(CMB_AY.Text);
                TXT_BX_LOG.Text += " GRP Datası Adex Convert edildi  " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            }
             
            //if (TOGGLE_OUTDOOR_ALL_DELETE.IsOn) DT.OUTDOOR_ALL_DELETE(); 
            // DT.YENI_DATA_INDEX_ROW_ADD(); 
        }

        private void BTN_DATA_SIL_Click(object sender, EventArgs e)
        {
            //DATA_MANAGER DT = new DATA_YUKLE.DATA_MANAGER(); 
            //if (TOOGLE_TELEVIZYON.IsOn) DT.TELEVIZYON_DELETE(DT_GNL_RPR_BAS_TARIHI.EditValue.ToString() , DT_GNL_RPR_BIT_TARIHI.EditValue.ToString()); 
            //if (TOOGLE_DERGI.IsOn) DT.DERGI_DELETE(DT_GNL_RPR_BAS_TARIHI.EditValue.ToString(), DT_GNL_RPR_BIT_TARIHI.EditValue.ToString());
            //if (TOOGLE_GAZETE.IsOn) DT.GAZETE_DELETE(DT_GNL_RPR_BAS_TARIHI.EditValue.ToString(), DT_GNL_RPR_BIT_TARIHI.EditValue.ToString());
            //if (TOOGLE_RADYO.IsOn) DT.RADYO_DELETE(DT_GNL_RPR_BAS_TARIHI.EditValue.ToString(), DT_GNL_RPR_BIT_TARIHI.EditValue.ToString());
            //if (TOOGLE_SINEMA.IsOn) DT.SINEMA_DELETE(DT_GNL_RPR_BAS_TARIHI.EditValue.ToString(), DT_GNL_RPR_BIT_TARIHI.EditValue.ToString());
            ////if (TOOGLE_INTERNET.IsOn) DT.ın();
            //if (TOOGLE_OUTDOOR.IsOn) DT.OUTDOOR_DELETE(DT_GNL_RPR_BAS_TARIHI.EditValue.ToString(), DT_GNL_RPR_BIT_TARIHI.EditValue.ToString()); 
        }

        private void BTN_GRP_DATA_YUKLE_Click(object sender, EventArgs e)
        {
            if (CHK_TRUNCATE_TABLE.Checked)
            {
                using (SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    myConnectionTable.Open();
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandText = " truncate table   dbo._TEMP_GRP_DATA ; truncate table  DATA_TELEVIZYON_GRP";
                    myCmd.Connection = myConnectionTable;
                    myCmd.ExecuteNonQuery();
                    myConnectionTable.Close();
                }
                TXT_BX_LOG.Text += " GRP Temp table Temizlendi " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            }


            //using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            //{
            //    SqlCommand command = new SqlCommand();
            //    conn.Open();
            //    command.Connection = conn;
            //    //command.CommandText = " BULK INSERT dbo._TEMP_GRP_DATA FROM '" + BTN_FILE_SELECT.EditValue + "' WITH (DATAFILETYPE = 'widechar',FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\r\n',CODEPAGE = 'RAW')";
            //    command.CommandText = " BULK INSERT _TEMP_GRP_DATA FROM 'F:\\_MIGRA_DATA\\1-15 Aug.csv' WITH(CODEPAGE = 1254, FIRSTROW = 2) "; //" BULK INSERT _DATA_TEMP FROM '" + BTN_FILE_SELECT.EditValue + "' WITH (DATAFILETYPE ='WIDECHAR',FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\r\n', CODEPAGE = '1254')";
            //    command.CommandTimeout = 0;
            //    //    "BULK INSERT Northwind.dbo.[Order Details]" +, DATAFILETYPE ='WIDECHAR',
            //    //@"FROM 'f:\orders\lineitem.tbl'" +
            //    //"WITH" +
            //    //"(" +
            //    //"FIELDTERMINATOR = '|'," +
            //    //"ROWTERMINATOR = ':\n'," +
            //    //"FIRE_TRIGGERS" +
            //    //")";
            //    command.ExecuteNonQuery();
            //}



            Stopwatch sw = new Stopwatch();
            sw.Start();
            //SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING);
            //SqlCommand command = new SqlCommand();
            //conn.Open();
            //command.Connection = conn;
            ////command.CommandText = " BULK INSERT dbo._TEMP_GRP_DATA FROM '" + BTN_FILE_SELECT.EditValue + "' WITH (DATAFILETYPE = 'widechar',FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\r\n',CODEPAGE = 'RAW')";
            //command.CommandText = " BULK INSERT _TEMP_GRP_DATA FROM '" + BTN_FILE_SELECT.EditValue + "' WITH (DATAFILETYPE ='WIDECHAR',FIRSTROW = 2, FIELDTERMINATOR = ';', ROWTERMINATOR = '\r\n', CODEPAGE = '1254')";
            //command.CommandTimeout = 0;
            ////    "BULK INSERT Northwind.dbo.[Order Details]" +, DATAFILETYPE ='WIDECHAR',
            ////@"FROM 'f:\orders\lineitem.tbl'" +
            ////"WITH" +
            ////"(" +
            ////"FIELDTERMINATOR = '|'," +
            ////"ROWTERMINATOR = ':\n'," +
            ////"FIRE_TRIGGERS" +
            ////")";
            //command.ExecuteNonQuery();


            DataTable csvData = new DataTable();
            string[] result = null;
            if (BTN_GRP_FILE_SELECT.EditValue == null) return;
            using (StreamReader reader = new StreamReader(BTN_GRP_FILE_SELECT.EditValue.ToString()))
            {
                result = reader.ReadLine().Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                int col = 0;
                foreach (string column in result)
                {
                    col++;
                    DataColumn datecolumn = new DataColumn(column);
                    datecolumn.AllowDBNull = true;
                    csvData.Columns.Add(datecolumn + col.ToString());
                }
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    result = line.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    csvData.Rows.Add(result);
                }
                reader.Close();
            }

            TXT_BX_LOG.Text += " GRP table yapısı okunuyor " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                using (SqlBulkCopy copy = new SqlBulkCopy(cn))
                {
                    copy.ColumnMappings.Clear();
                    for (int i = 0; i <= csvData.Columns.Count - 1; i++)
                    {
                        copy.ColumnMappings.Add(i, i);
                    }
                    copy.BulkCopyTimeout = 0;
                    copy.DestinationTableName = "_TEMP_GRP_DATA";

                    copy.WriteToServer(csvData);
                }
            }

            //TXT_BX_LOG.Text += " GRP Temp table data yüklendi " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_MECRA_KODU_KONTROL ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();

                SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                myConnectionTable.Open();
                while (reader.Read())
                {
                    SqlCommand myCmd = new SqlCommand();

                    myCmd.Parameters.AddWithValue("@MECRA_KODU", reader["MECRA_KODU"]);
                    myCmd.Parameters.AddWithValue("@MECRA_KODU_CONVERT", reader["MECRA_KODU_CONVERT"]);
                    //myCmd.CommandText = " UPDATE  dbo.DATA_TELEVIZYON_GRP SET Channel= @MECRA_KODU_CONVERT   WHERE Channel=@MECRA_KODU  ";
                    myCmd.CommandText = " UPDATE  dbo._TEMP_GRP_DATA  SET Channel= @MECRA_KODU_CONVERT   WHERE Channel=@MECRA_KODU  ";
                    myCmd.Connection = myConnectionTable;
                    myCmd.ExecuteNonQuery();
                }
                //myCmd.Connection.Close();
                // Call Close when done reading.
                myConnectionTable.Close();
                reader.Close();
            }


            DataSet ds = new DataSet();
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand("SELECT   top 1000 * FROM  dbo._TEMP_GRP_DATA", conn) };
                adapter.Fill(ds, "_TEMP_GRP_DATA");
                DataViewManager dvManager = new DataViewManager(ds);
                DataView _TEMP_GRP_DATA = dvManager.CreateDataView(ds.Tables[0]);
                gridControl_DATA.DataSource = _TEMP_GRP_DATA;
            }

            TXT_BX_LOG.Text += " Kanal İsimleri düzeltildi " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
            TXT_BX_LOG.Text += " GRP dataları Convert ediliyor " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

            DATA_MANAGER DT = new DATA_YUKLE.DATA_MANAGER();
            DT.GRP_CONVERT_ROW_ADD();

            TXT_BX_LOG.Text += " GRP dataları Convert işlemi tamamlandı " + DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

            sw.Stop();
            MessageBox.Show((sw.ElapsedMilliseconds / 1000.00).ToString());
        }

        private void BTN_GRP_FILE_SELECT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                BTN_GRP_FILE_SELECT.EditValue = file.FileName;
            }
        }
    }
}