using System;
using System.Data;
using System.Data.SqlClient;

namespace CoreV2
{
    public  class _EDT_MASTER_MANANGE
    { 
        public void MEDYA_AJANSI_GRUPLA(DataRow dw, SqlConnection myCon)
        {
            SqlCommand myCmd = new SqlCommand();
            myCmd.Parameters.AddWithValue("@MEDYA_AJANSI", dw["MEDYA_AJANSI"]);
            myCmd.Parameters.AddWithValue("@GROUPM_DIGER", dw["GROUPM_DIGER"]);
            myCmd.Parameters.AddWithValue("@GROUPM", dw["GROUPM"]);
            myCmd.CommandText = string.Format(" UPDATE [dbo].[__TEMP_{0}] SET [GroupM-Diger]= @GROUPM_DIGER, [GroupM]=@GROUPM  WHERE [MEDYA AJANSI]=@MEDYA_AJANSI  ", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME);
            myCmd.CommandTimeout = 0;
            myCmd.Connection = myCon;
            myCmd.ExecuteNonQuery();
        }
        public void REKLAM_VEREN_GUNCELLE(DataRow dw, SqlConnection myCon)
        {
            SqlCommand myCmd = new SqlCommand();
            myCmd.Parameters.AddWithValue("@REKLAMIN_FIRMASI", dw["REKLAMIN_FIRMASI"]);
            myCmd.Parameters.AddWithValue("@REKLAMIN_FIRMASI_YENI", dw["REKLAMIN_FIRMASI_YENI"]);
            myCmd.CommandText = string.Format(" UPDATE [dbo].[__TEMP_{0}] SET [REKLAMIN FİRMASI CODE]= @REKLAMIN_FIRMASI_YENI  WHERE [REKLAMIN FİRMASI]=@REKLAMIN_FIRMASI  ", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME);
            myCmd.CommandTimeout = 0;
            myCmd.Connection = myCon;
            myCmd.ExecuteNonQuery();
        }
        public void YTD_GUNCELLE(DataRow dw, SqlConnection myCon)
        {
            SqlCommand myCmd = new SqlCommand();
            myCmd.Parameters.AddWithValue("@YIL", dw["YIL"]);
            myCmd.Parameters.AddWithValue("@AY", dw["AY"]);
            myCmd.Parameters.AddWithValue("@YTD", dw["YTD"]);
            myCmd.CommandText = string.Format(" UPDATE [dbo].[__TEMP_{0}] SET [YTD]= @YTD  WHERE [YIL]=@YIL AND [AY]=@AY  ", _GLOBAL_PARAMETRELER._KULLANICI_MAIL);
            myCmd.CommandTimeout = 0;
            myCmd.Connection = myCon;
            myCmd.ExecuteNonQuery();
        }
        public void SPOT_TYPE_GUNCELLE(DataRow dw, SqlConnection myCon)
        {
            SqlCommand myCmd = new SqlCommand();
            myCmd.Parameters.AddWithValue("@SPOT_TIPI", dw["SPOT_TIPI"]);
            myCmd.Parameters.AddWithValue("@SPOT_TIPI_YENI", dw["SPOT_TIPI_YENI"]);
            myCmd.CommandText = string.Format(" UPDATE [dbo].[__TEMP_{0}] SET [Spot Tipleri]= @SPOT_TIPI_YENI  WHERE   [SPOT TİPİ]=@SPOT_TIPI  ", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME);
            myCmd.CommandTimeout = 0;
            myCmd.Connection = myCon;
            myCmd.ExecuteNonQuery();
        }

        public void TELEVIZYON_MASTER_GUNCELLE(DataRow dw, SqlConnection myCon)
        {
            string TABLE_UPDATE = string.Format(" [YAYIN SINIFI]='TELEVIZYON'  AND  [ANA YAYIN]='{0}' ", dw["MECRA_KODU"]);
            if (dw["BASLANGIC_SAATI"].ToString() != "" && dw["BITIS_SAATI"].ToString() != "")
            {
                TABLE_UPDATE += string.Format(" AND  [TİME İNT] >= '{0}00' AND  [TİME İNT] <= '{1}59' ", dw["BASLANGIC_SAATI"].ToString().Replace(":", ""), dw["BITIS_SAATI"].ToString().Replace(":", ""));
            }

            //if (dw["BASLANGIC_TARIHI"].ToString() != "" && dw["BITIS_TARIHI"].ToString() != "")
            //{
            //    DateTime BAS_TARIHI = DateTime.Parse(dw["BASLANGIC_TARIHI"].ToString());
            //    DateTime BIT_TARIHI = DateTime.Parse(dw["BITIS_TARIHI"].ToString());
            //    TABLE_UPDATE += " AND  ([TARİH] >=  CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd") + "', 102))  AND ([TARİH] <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd") + "', 102))";
            //}


            int _ROW = 0;
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format(" SELECT count(*) as ROWS FROM  dbo.[__TEMP_{0}]  WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
                SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReader.Read())
                {
                    _ROW = Convert.ToInt32(myReader["ROWS"]);
                }
            } 
            if (_ROW > 0)
            {
                SqlCommand myCmd = new SqlCommand() { CommandText = string.Format("set rowcount {0}  UPDATE dbo.[__TEMP_{1}]  SET [Opt/PT]=@OPT_PT ,MNM=@MNM ,[Mecra Adı]=@MECRA_KODU_YENI,[Medya Grupları]=@MECRA_GROUP  WHERE {2}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE) };
                myCmd.Parameters.AddWithValue("@OPT_PT", dw["OPT_PT"]);
                myCmd.Parameters.AddWithValue("@MNM", dw["MNM"]);
                myCmd.Parameters.AddWithValue("@MECRA_KODU_YENI", dw["MECRA_KODU_YENI"]);
                myCmd.Parameters.AddWithValue("@MECRA_GROUP", dw["MECRA_GROUP"]);
                myCmd.Parameters.AddWithValue("@YAYIN_TURU", dw["YAYIN_TURU"]); 
                myCmd.Connection = myCon;
                myCmd.CommandTimeout = 0;
                myCmd.ExecuteNonQuery();
            } 
        } 
        public void GAZETE_MASTER_GUNCELLE(DataRow dw, SqlConnection myCon)
        { 
            //  case WHEN cast((EBAT_KUTU)  as float) > 0 then cast((EBAT_KUTU*29.53)  as float)  else cast((SANTIM)  as float)   END as CAL_SANTIM
            string TABLE_UPDATE = string.Format(" [YAYIN SINIFI]='GAZETE'  AND  [MEDYA]='{0}' ", dw["MECRA_KODU"]);
            int _ROW = 0;
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format(" SELECT count(*) as ROWS FROM  dbo.[__TEMP_{0}]  WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
                SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReader.Read())
                {
                    _ROW = Convert.ToInt32(myReader["ROWS"]);
                }
            }

            if (_ROW > 0)
            {
                SqlCommand myCmd = new SqlCommand() { CommandText = string.Format("set rowcount {0}  UPDATE dbo.[__TEMP_{1}]  SET [Mecra Adı]=@MECRA_KODU_YENI ,[Medya Dahil Hariç]=@MEDYA_DAHIL_HARIC ,[Medya Grupları]=@MECRA_GROUP WHERE {2}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE) };
                myCmd.Parameters.AddWithValue("@MECRA_KODU_YENI", dw["MECRA_KODU_YENI"]);
                myCmd.Parameters.AddWithValue("@MEDYA_DAHIL_HARIC", dw["MEDYA_DAHIL_HARIC"]);
                myCmd.Parameters.AddWithValue("@MECRA_GROUP", dw["MECRA_GROUP"]); 
                myCmd.Connection = myCon;
                myCmd.CommandTimeout = 0;
                myCmd.ExecuteNonQuery();
            } 
        } 

        public void DERGI_MASTER_GUNCELLE(DataRow dw, SqlConnection myCon)
        {
            string TABLE_UPDATE = string.Format(" [YAYIN SINIFI]='DERGI'  AND  [MEDYA]='{0}' ", dw["MECRA_KODU"]); 
            int _ROW = 0;
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format(" SELECT count(*) as ROWS FROM  dbo.[__TEMP_{0}]  WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
                SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                myConnection.Open();
                SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                while (myReader.Read())
                {
                    _ROW = Convert.ToInt32(myReader["ROWS"]);
                }
            }

            if (_ROW > 0)
            {
                SqlCommand myCmd = new SqlCommand() { CommandText = string.Format("set rowcount {0}  UPDATE dbo.[__TEMP_{1}]  SET [Medya Grupları]=@MECRA_GROUP  WHERE {2}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE) };
                myCmd.Parameters.AddWithValue("@MECRA_GROUP", dw["MECRA_GROUP"]); 
                myCmd.Connection = myCon;
                myCmd.CommandTimeout = 0;
                myCmd.ExecuteNonQuery();
            }

        }
    }
}
