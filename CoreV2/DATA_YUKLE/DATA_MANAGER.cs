using System.Data.SqlClient;

namespace CoreV2.DATA_YUKLE
{
    public class DATA_MANAGER
    {

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


        public void YENI_DATA_INDEX_ROW_ADD()
        {
            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = @" INSERT INTO _ADEX_INDEX_DATA (MEDYA, YAYIN_TURU, YAYIN_SINIFI, ANA_YAYIN,  YAYIN_GRUBU, REKLAM_SLOGANI, VERSIYON, KREATIF_AJANS, MEDYA_AJANSI, URUN_HIZMET, ANA_SEKTOR, SEKTOR, REKLAMIN_FIRMASI, FIRMA_GRUBU, URUN_TURU, MARKA, IKINCI_URUN_HIZMET, IKINCI_URUN_TURU, IKINCI_MARKA, IKINCI_FIRMA, IKINCI_FIRMA_GRUBU, IKINCI_ANASEKTOR, IKINCI_SEKTOR, SPOT_TIPI, SPOT_TIPI_DETAY, RENK, ILAN_EBADI, TARIFE_GRUP, SAYFA_GRUBU, ILI, BOLGE, TIPI, KAMPANYA_KODU, GRUBU, UNITE, BILESEN, SITE, KAMPANYA_ACIKLAMASI, R_NETWORK )
                    SELECT     MEDYA, YAYIN_TURU, YAYIN_SINIFI, ANA_YAYIN,  YAYIN_GRUBU, REKLAM_SLOGANI, VERSIYON, KREATIF_AJANS, MEDYA_AJANSI, URUN_HIZMET, ANA_SEKTOR, SEKTOR, REKLAMIN_FIRMASI, FIRMA_GRUBU, URUN_TURU, MARKA, IKINCI_URUN_HIZMET, IKINCI_URUN_TURU, IKINCI_MARKA, IKINCI_FIRMA, IKINCI_FIRMA_GRUBU, IKINCI_ANASEKTOR, IKINCI_SEKTOR, SPOT_TIPI, SPOT_TIPI_DETAY, RENK, ILAN_EBADI, TARIFE_GRUP, SAYFA_GRUBU, ILI, BOLGE, TIPI, KAMPANYA_KODU, GRUBU, UNITE, BILESEN, SITE, KAMPANYA_ACIKLAMASI, R_NETWORK
                    FROM         dbo._TEMP_ADEX_DATA
                    GROUP BY  MEDYA, YAYIN_TURU, YAYIN_SINIFI, ANA_YAYIN,  YAYIN_GRUBU, REKLAM_SLOGANI, VERSIYON, KREATIF_AJANS, MEDYA_AJANSI, URUN_HIZMET, ANA_SEKTOR, SEKTOR, REKLAMIN_FIRMASI, FIRMA_GRUBU, URUN_TURU, MARKA, IKINCI_URUN_HIZMET, IKINCI_URUN_TURU, IKINCI_MARKA, IKINCI_FIRMA, IKINCI_FIRMA_GRUBU, IKINCI_ANASEKTOR, IKINCI_SEKTOR, SPOT_TIPI, SPOT_TIPI_DETAY, RENK, ILAN_EBADI, TARIFE_GRUP, SAYFA_GRUBU, ILI, BOLGE, TIPI, KAMPANYA_KODU, GRUBU, UNITE, BILESEN, SITE, KAMPANYA_ACIKLAMASI, R_NETWORK
                    ";
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }

        public void TELEVIZYON_ROW_ADD()
        {   
            string SELECT_FIELDS = "";
            string INSERT_FIELDS = "";
            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_DATA_UPLOAD_MANAGER where MECRA_TURU='TELEVIZYON' ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                while (reader.Read())
                {
                    SELECT_FIELDS += reader["SELECT_FIELDS"].ToString() + ",";
                    INSERT_FIELDS += reader["INSERT_FIELDS"].ToString() + ",";                  
                }
                reader.Close();
            }  
            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format(" INSERT INTO dbo.DATA_TELEVIZYON_ADEX_2018({0}) SELECT {1}  FROM _TEMP_ADEX_DATA WHERE YAYIN_SINIFI='TELEVIZYON' ", TEMIZLE_ELEMAN(SELECT_FIELDS, 1 ,"").ToString(), TEMIZLE_ELEMAN(INSERT_FIELDS, 1, "").ToString() );
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.CommandTimeout = 0;
                myCmd.ExecuteNonQuery();
            } 
        } 

        public void DERGI_ROW_ADD()
        {
            string SELECT_FIELDS = "";
            string INSERT_FIELDS = "";
            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_DATA_UPLOAD_MANAGER where MECRA_TURU='DERGI' ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                while (reader.Read())
                {
                    SELECT_FIELDS += reader["SELECT_FIELDS"].ToString() + ",";
                    INSERT_FIELDS += reader["INSERT_FIELDS"].ToString() + ",";
                }
                reader.Close();
            }

            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format(" INSERT INTO dbo.DATA_BASIN_ADEX({0}) SELECT {1}  FROM _TEMP_ADEX_DATA WHERE YAYIN_SINIFI='DERGI' ", TEMIZLE_ELEMAN(SELECT_FIELDS, 1, "").ToString(), TEMIZLE_ELEMAN(INSERT_FIELDS, 1, "").ToString());
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        } 
        public void GAZETE_ROW_ADD()
        {
            string SELECT_FIELDS = "";
            string INSERT_FIELDS = "";
            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_DATA_UPLOAD_MANAGER where MECRA_TURU='GAZETE' ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                while (reader.Read())
                {
                    SELECT_FIELDS += reader["SELECT_FIELDS"].ToString() + ",";
                    INSERT_FIELDS += reader["INSERT_FIELDS"].ToString() + ",";
                }
                reader.Close();
            }

            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format(" INSERT INTO dbo.DATA_BASIN_ADEX({0}) SELECT {1}  FROM _TEMP_ADEX_DATA WHERE YAYIN_SINIFI='GAZETE' ", TEMIZLE_ELEMAN(SELECT_FIELDS, 1, "").ToString(), TEMIZLE_ELEMAN(INSERT_FIELDS, 1, "").ToString());
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }

        public void RADYO_ROW_ADD()
        {
            string SELECT_FIELDS = "";
            string INSERT_FIELDS = "";
            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_DATA_UPLOAD_MANAGER where MECRA_TURU='RADYO' ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                while (reader.Read())
                {
                    SELECT_FIELDS += reader["SELECT_FIELDS"].ToString() + ",";
                    INSERT_FIELDS += reader["INSERT_FIELDS"].ToString() + ",";
                }
                reader.Close();
            }

            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format(" INSERT INTO dbo.DATA_RADYO_ADEX({0}) SELECT {1}  FROM _TEMP_ADEX_DATA WHERE YAYIN_SINIFI='RADYO' ", TEMIZLE_ELEMAN(SELECT_FIELDS, 1, "").ToString(), TEMIZLE_ELEMAN(INSERT_FIELDS, 1, "").ToString());
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.CommandTimeout = 0;
                myCmd.ExecuteNonQuery();
            }
        }


        public void SINEMA_ROW_ADD()
        {
            string SELECT_FIELDS = "";
            string INSERT_FIELDS = "";
            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_DATA_UPLOAD_MANAGER where MECRA_TURU='SINEMA' ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                while (reader.Read())
                {
                    SELECT_FIELDS += reader["SELECT_FIELDS"].ToString() + ",";
                    INSERT_FIELDS += reader["INSERT_FIELDS"].ToString() + ",";
                }
                reader.Close();
            }

            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format(" INSERT INTO dbo.DATA_SINEMA_ADEX({0}) SELECT {1}  FROM _TEMP_ADEX_DATA WHERE YAYIN_SINIFI='SINEMA' ", TEMIZLE_ELEMAN(SELECT_FIELDS, 1, "").ToString(), TEMIZLE_ELEMAN(INSERT_FIELDS, 1, "").ToString());
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }

        public void OUTDOOR_ROW_ADD()
        {
            string SELECT_FIELDS = "";
            string INSERT_FIELDS = "";
            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_DATA_UPLOAD_MANAGER where MECRA_TURU='OUTDOOR' ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                while (reader.Read())
                {
                    SELECT_FIELDS += reader["SELECT_FIELDS"].ToString() + ",";
                    INSERT_FIELDS += reader["INSERT_FIELDS"].ToString() + ",";
                }
                reader.Close();
            }

            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format(" INSERT INTO dbo.DATA_OUTDOOR_ADEX({0}) SELECT {1}  FROM _TEMP_ADEX_DATA WHERE YAYIN_SINIFI='OUTDOOR' ", TEMIZLE_ELEMAN(SELECT_FIELDS, 1, "").ToString(), TEMIZLE_ELEMAN(INSERT_FIELDS, 1, "").ToString());
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }


        public void GRP_CONVERT_ROW_ADD()
        {
            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = @"  truncate table  dbo.DATA_TELEVIZYON_GRP  ";
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }

            string SELECT_FIELDS = "";
            string INSERT_FIELDS = "";
            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_DATA_UPLOAD_MANAGER where MECRA_TURU='GRP' ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                while (reader.Read())
                {
                    SELECT_FIELDS += reader["SELECT_FIELDS"].ToString() + ",";
                    INSERT_FIELDS += reader["INSERT_FIELDS"].ToString() + ",";
                }
                reader.Close();
            }

            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format("  INSERT INTO dbo.DATA_TELEVIZYON_GRP({0}) SELECT {1}  FROM _TEMP_GRP_DATA  ", TEMIZLE_ELEMAN(SELECT_FIELDS, 1, "").ToString(), TEMIZLE_ELEMAN(INSERT_FIELDS, 1, "").ToString());
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.CommandTimeout = 0;
                myCmd.ExecuteNonQuery();
            }
        }


        public void GRP_MACH_TELEVIZYON_UPDATE(string AY)
        { 
            string SELECT_FIELDS = ""; 
            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_DATA_UPLOAD_MANAGER where MECRA_TURU='GRP_UPDATE' ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                while (reader.Read())
                {
                    SELECT_FIELDS += reader["SELECT_FIELDS"].ToString() +"="+ reader["INSERT_FIELDS"].ToString() + ",";
                }
                reader.Close();
            }

            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format("  UPDATE DT  SET {0} FROM  dbo.DATA_TELEVIZYON_ADEX_2018 DT INNER JOIN  dbo.DATA_TELEVIZYON_GRP GRP ON DT.REF_TIME = GRP.REF_TIME   where    month (DT.TARIH)='"+ AY + "' ", TEMIZLE_ELEMAN(SELECT_FIELDS, 1, "").ToString());
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.CommandTimeout = 0;
                myCmd.ExecuteNonQuery();
            }
        }

        public void OUTDOOR_ALL_DELETE()
        {
            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = @"  delete dbo.DATA_OUTDOOR_ADEX WHERE     (TARIH >= CONVERT(DATETIME, '2016-01-01 00:00:00', 102)) ";
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }
        public void OUTDOOR_DELETE(string BAS_DATE,string BIT_DATE)
        {
            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = @"  delete dbo.DATA_OUTDOOR_ADEX WHERE     (TARIH >= CONVERT(DATETIME, '2016-01-01 00:00:00', 102))  AND (TARIH <= CONVERT(DATETIME, '2016-01-01 00:00:00', 102)) ";
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }        
        public void TELEVIZYON_DELETE(string BAS_TARIHI, string BIT_TARIHI)
        {
            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format(" delete dbo.DATA_TELEVIZYON_ADEX_2018 WHERE      ([TARIH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARIH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }
        public void DERGI_DELETE(string BAS_TARIHI, string BIT_TARIHI)
        {
            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format(" delete dbo.DATA_BASIN_ADEX WHERE      YAYIN_SINIFI='DERGI' AND   ([TARIH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARIH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }

        public void GAZETE_DELETE(string BAS_TARIHI, string BIT_TARIHI)
        {
            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format(" delete dbo.DATA_BASIN_ADEX WHERE    YAYIN_SINIFI='GAZETE' AND  ([TARIH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARIH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }

        public void RADYO_DELETE(string BAS_TARIHI, string BIT_TARIHI)
        {
            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();                
                string Str = string.Format(" delete dbo.DATA_RADYO_ADEX WHERE  ([TARIH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARIH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }


        public void SINEMA_DELETE(string BAS_TARIHI, string BIT_TARIHI)
        {
            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();                
                string Str = string.Format(" delete dbo.DATA_SINEMA_ADEX WHERE  ([TARIH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARIH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }

        public void GRP_CONVERT()
        { 
            string SELECT_FIELDS = "";
            string INSERT_FIELDS = "";
            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_DATA_UPLOAD_MANAGER where MECRA_TURU='GRP_UPDATE' ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();
                while (reader.Read())
                {
                    SELECT_FIELDS += reader["SELECT_FIELDS"].ToString() + ",";
                    INSERT_FIELDS += reader["INSERT_FIELDS"].ToString() + ",";
                }
                reader.Close();
            }

            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format(" INSERT INTO dbo.DATA_TELEVIZYON_GRP({0}) SELECT {1}  FROM _TEMP_GRP_DATA  ", TEMIZLE_ELEMAN(SELECT_FIELDS, 1, "").ToString(), TEMIZLE_ELEMAN(INSERT_FIELDS, 1, "").ToString());
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }




            //string HEDEFKITLE_LIST_SELECT = " Channel + '|' + CAST(CAST(PARSENAME(Date, 1) + '-' + PARSENAME(Date, 2) + '-' + PARSENAME(Date, 3) as DATE) AS nvarchar) + '|' + PARSENAME(replace(Time, ':', '.'), 3) + ':' + PARSENAME(replace(Time, ':', '.'), 2) AS REF_TIME , Channel,,CAST(CONVERT(date, CAST(PARSENAME(Date, 1) + '-' + PARSENAME(Date, 2) + '-' + PARSENAME(Date, 3) as DATE), 102) as date) as TARIH ,Time ";

            //string HEDEFKITLE_LIST_INSERT = "";
            //using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            //{
            //    SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_SECENEKLER BASLIK='HEDEFKİTLELER' ", connection);
            //    connection.Open();
            //    SqlDataReader reader = commands.ExecuteReader();
            //    while (reader.Read())
            //    {
            //        HEDEFKITLE_LIST_INSERT += " CAST(REPLACE("+ reader["FIELDS"].ToString() + ", ',', '.') AS float) "+ reader["FIELDS"].ToString() + ",";
            //    }
            //    reader.Close();
            //}
            //using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            //{
            //    cn.Open();
            //    SqlCommand myCmd = new SqlCommand();                
            //    string Str =  string.Format(" INSERT INTO dbo.DATA_TELEVIZYON_GRP(REF_TIME, CHANNEL, TARIHI, SAAT,{0} )  SELECT  {1}  FROM dbo._TEMP_GRP_DATA ", HEDEFKITLE_LIST_INSERT, HEDEFKITLE_LIST_SELECT);
            //    myCmd.CommandText = Str;
            //    myCmd.Connection = cn;
            //    myCmd.ExecuteNonQuery();
            //}
        } 
        public void HEDEFKITLE_ESLESTIR()
        {
            string HEDEFKITLE_LIST_INSERT = "";            
            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                SqlCommand commands = new SqlCommand("SELECT * FROM dbo.ADM_SECENEKLER BASLIK='HEDEFKİTLELER' ", connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();                
                while (reader.Read())
                {
                    HEDEFKITLE_LIST_INSERT += " DT." + reader["FIELDS"].ToString() +"=GRP."+  reader["FIELDS"].ToString() + ",";
                }                 
                reader.Close();
            }
            using (SqlConnection cn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                cn.Open();
                SqlCommand myCmd = new SqlCommand();
                string Str = string.Format(" UPDATE DT SET   {0}  FROM    dbo.DATA_TELEVIZYON_ADEX_2016 DT INNER JOIN dbo.DATA_TELEVIZYON_GRP GRP ON DT.REF_TIME = GRP.REF_TIME  where    month (DT.TARIH)=11  ", HEDEFKITLE_LIST_INSERT );
                myCmd.CommandText = Str;
                myCmd.Connection = cn;
                myCmd.ExecuteNonQuery();
            }
        }
    }
}
