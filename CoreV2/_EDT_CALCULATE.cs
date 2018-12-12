using System;
using System.Data;
using System.Data.SqlClient;

namespace CoreV2
{
    public class _EDT_CALCULATE
    {
        public void TELEVIZYON_HESAPLA_OLD(DateTime BASTAR, DateTime BITTAR, DataRow dw, SqlConnection myCon)
        {
            //string TABLE_UPDATE = string.Format(" [YAYIN SINIFI]='TELEVIZYON'  AND  [ANA YAYIN]='{0}' ", dw["MECRA_KODU"]);
            //if (dw["BASLANGIC_SAATI"].ToString() != "" && dw["BITIS_SAATI"].ToString() != "")
            //{
            //    TABLE_UPDATE += string.Format(" AND  [TİME İNT] >= '{0}00' AND  [TİME İNT] <= '{1}59' ", dw["BASLANGIC_SAATI"].ToString().Replace(":", ""), dw["BITIS_SAATI"].ToString().Replace(":", ""));
            //} 
            //if (dw["BASLANGIC_TARIHI"].ToString() != "" && dw["BITIS_TARIHI"].ToString() != "")
            //{
            //    DateTime BAS_TARIHI = DateTime.Parse(dw["BASLANGIC_TARIHI"].ToString());
            //    DateTime BIT_TARIHI = DateTime.Parse(dw["BITIS_TARIHI"].ToString());
            //    TABLE_UPDATE += string.Format(" AND  ([TARİH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARİH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
            //}

            string TABLE = "";
            if ("Cpp" == dw["HESAPLANMA_TURU"].ToString())
            {
                 TABLE = "   UPDATE dbo.[__TEMP_"+ _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] "+
                         "   SET  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[OPT_PT] = Trf.[OPT_PT],dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] = CASE WHEN uptbl.[" + dw["TARGET"] + "] IS NOT NULL THEN  uptbl.[SÜRE] * uptbl.[" + dw["TARGET"] + "] * Trf.[BIRIM_FIYAT] ELSE   uptbl.[SÜRE] * Trf.[BIRIM_FIYAT] END " +
                         "   FROM dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "]  uptbl INNER JOIN dbo.TRF_TELEVIZYON Trf ON uptbl.[ANA YAYIN] = Trf.MECRA_KODU AND " +
                         "   uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI AND uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND "+
                         "   uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI where  (Trf.TARIFE_KODU = N'JOKER') and uptbl.[YAYIN SINIFI]= 'TELEVIZYON'" ;

                  //TABLE = "  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET uptbl.[NET TUTAR] =  uptbl.[SÜRE] * uptbl.[" + dw["TARGET"] + "] * Trf.[BIRIM_FIYAT]   " +
                  // " FROM dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  uptbl INNER JOIN    dbo.TRF_TELEVIZYON Trf ON uptbl.[ANA YAYIN] = Trf.MECRA_KODU AND  " +
                  // " uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND    uptbl.TARİH <= Trf.BITIS_TARIHI AND   uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND  uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI    where  (Trf.TARIFE_KODU = N'JOKER') and uptbl.[YAYIN SINIFI]= 'TELEVIZYON'";                   
            }
            if ("Süre" == dw["HESAPLANMA_TURU"].ToString())
            {
                TABLE = "   UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                        "   SET  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[OPT_PT] = Trf.[OPT_PT], dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  uptbl.[SÜRE] *   Trf.[BIRIM_FIYAT]  " +
                        "   FROM dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "]  uptbl INNER JOIN dbo.TRF_TELEVIZYON Trf ON uptbl.[ANA YAYIN] = Trf.MECRA_KODU AND " +
                        "   uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI AND uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND " +
                        "   uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI where  (Trf.TARIFE_KODU = N'JOKER') and uptbl.[YAYIN SINIFI]= 'TELEVIZYON'"; 
            }
            if ("Ratecard" == dw["HESAPLANMA_TURU"].ToString())
            {
                TABLE = "   UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                 "   SET  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[OPT_PT] = Trf.[OPT_PT], dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  = uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT]  " +
                 "   FROM dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "]  uptbl INNER JOIN dbo.TRF_TELEVIZYON Trf ON uptbl.[ANA YAYIN] = Trf.MECRA_KODU AND " +
                 "   uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI AND uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND " +
                 "   uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI where  (Trf.TARIFE_KODU = N'JOKER') and uptbl.[YAYIN SINIFI]= 'TELEVIZYON'";
            }

            SqlCommand myCmd = new SqlCommand();
            //if ("Cpp" == dw["HESAPLANMA_TURU"].ToString())
            //{
            //    myCmd.CommandText = string.Format("   UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}]  SET OPT_PT=@OPT_PT ,[NET TUTAR]=[SÜRE] * [{1}] * @BIRIM_FIYAT  WHERE {2}", _GLOBAL_PARAMETRELER._KULLANICI_NAME, dw["TARGET"], TABLE_UPDATE);
            //}
            //if ("Süre" == dw["HESAPLANMA_TURU"].ToString())
            //{
            //    myCmd.CommandText = string.Format("  UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}]  SET OPT_PT=@OPT_PT ,[NET TUTAR]=[SÜRE] * @BIRIM_FIYAT WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_NAME, TABLE_UPDATE);
            //}
            //if ("Ratecard" == dw["HESAPLANMA_TURU"].ToString())
            //{
            //    myCmd.CommandText = string.Format("  UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}]  SET OPT_PT=@OPT_PT ,[NET TUTAR]=[TUTAR TL] * @BIRIM_FIYAT WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_NAME, TABLE_UPDATE);
            //}

            //myCmd.Parameters.AddWithValue("@OPT_PT", dw["OPT_PT"]);
            //myCmd.Parameters.AddWithValue("@BIRIM_FIYAT", dw["BIRIM_FIYAT"]);

               myCmd.CommandText = TABLE;
               myCmd.Connection = myCon;
               myCmd.CommandTimeout = 0;
               myCmd.ExecuteNonQuery();
            //}
        }

        public void TELEVIZYON_HESAPLA(string TARIFE_KODU, DataRow dw, SqlConnection myCon)
        {
 
            string TABLE = "";

            if ("Cpp" == dw["HESAPLANMA_TURU"].ToString())
            { 
                TABLE = "   UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                        "   SET  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[OPT_PT] = Trf.[OPT_PT],dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] = CASE WHEN uptbl.[" + dw["TARGET"] + "] IS NOT NULL THEN  uptbl.[SÜRE] * uptbl.[" + dw["TARGET"] + "] * Trf.[BIRIM_FIYAT] ELSE   uptbl.[SÜRE] * Trf.[BIRIM_FIYAT] END " +
                        "   FROM dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "]  uptbl INNER JOIN dbo.TRF_TELEVIZYON Trf ON uptbl.[ANA YAYIN] = Trf.MECRA_KODU AND " +
                        "   uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI AND uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND " +
                        "   uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI where  (Trf.TARIFE_KODU = N'"+TARIFE_KODU+"') and uptbl.[YAYIN SINIFI]= 'TELEVIZYON'"; 
 
            }

            if ("Süre" == dw["HESAPLANMA_TURU"].ToString())
            { 
                TABLE = "   UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                        "   SET  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[OPT_PT] = Trf.[OPT_PT], dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =  uptbl.[SÜRE] *   Trf.[BIRIM_FIYAT]  " +
                        "   FROM dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "]  uptbl INNER JOIN dbo.TRF_TELEVIZYON Trf ON uptbl.[ANA YAYIN] = Trf.MECRA_KODU AND " +
                        "   uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI AND uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND " +
                        "   uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI where  (Trf.TARIFE_KODU = N'JOKER') and uptbl.[YAYIN SINIFI]= 'TELEVIZYON'"; 
            } 
            if ("Ratecard" == dw["HESAPLANMA_TURU"].ToString())
            { 
                TABLE = "   UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "] " +
                 "   SET  dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[OPT_PT] = Trf.[OPT_PT], dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "].[NET TUTAR] =   uptbl.[TUTAR TL] * Trf.[BIRIM_FIYAT]  " +
                 "   FROM dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME + "]  uptbl INNER JOIN dbo.TRF_TELEVIZYON Trf ON uptbl.[ANA YAYIN] = Trf.MECRA_KODU AND " +
                 "   uptbl.TARİH >= Trf.BASLANGIC_TARIHI AND  uptbl.TARİH <= Trf.BITIS_TARIHI AND uptbl.BAŞLANGIÇ >= Trf.BASLANGIC_SAATI AND " +
                 "   uptbl.BAŞLANGIÇ <= Trf.BITIS_SAATI where  (Trf.TARIFE_KODU = N'JOKER') and uptbl.[YAYIN SINIFI]= 'TELEVIZYON'"; 
            } 
            SqlCommand myCmd = new SqlCommand(); 
            myCmd.CommandText = TABLE;
            myCmd.Connection = myCon;
            myCmd.CommandTimeout = 0;
            myCmd.ExecuteNonQuery();
    

        }
        public void TELEVIZYON_NONE_MEASURED_HESAPLA(DateTime BASTAR, DateTime BITTAR, DataRow dw, SqlConnection myCon)
        {
            string TABLE_UPDATE = string.Format(" [YAYIN SINIFI]='TELEVIZYON'  AND  [ANA YAYIN]='{0}' ", dw["MECRA_KODU"]);
            if (dw["BASLANGIC_SAATI"].ToString() != "" && dw["BITIS_SAATI"].ToString() != "")
            {
                TABLE_UPDATE += string.Format(" AND  [TİME İNT] >= '{0}00' AND  [TİME İNT] <= '{1}59' ", dw["BASLANGIC_SAATI"].ToString().Replace(":", ""), dw["BITIS_SAATI"].ToString().Replace(":", ""));
            }
            if (dw["BASLANGIC_TARIHI"].ToString() != "" && dw["BITIS_TARIHI"].ToString() != "")
            {
                DateTime BAS_TARIHI = DateTime.Parse(dw["BASLANGIC_TARIHI"].ToString());
                DateTime BIT_TARIHI = DateTime.Parse(dw["BITIS_TARIHI"].ToString());
                TABLE_UPDATE += string.Format(" AND  ([TARİH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARİH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
            }
            int _ROW = 0;
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format(" SELECT count(*) as ROWS FROM  " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}]  WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
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
                SqlCommand myCmd = new SqlCommand() { CommandText = string.Format("set rowcount {0}  UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{1}]  SET OPT_PT=@OPT_PT , [{2}]=@GRP  WHERE {3}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, dw["TARGET"], TABLE_UPDATE) };
                myCmd.Parameters.AddWithValue("@OPT_PT", dw["OPT_PT"]);
                myCmd.Parameters.AddWithValue("@GRP", dw["GRP"]);
                myCmd.Connection = myCon;
                myCmd.CommandTimeout = 0;
                myCmd.ExecuteNonQuery();
            }

        }
        public void RADYO_HESAPLA(DateTime BASTAR, DateTime BITTAR, DataRow dw, SqlConnection myCon)
        {


            string TABLE_UPDATE = string.Format(" [YAYIN SINIFI]='RADYO'  AND  [ANA YAYIN]='{0}' ", dw["MECRA_KODU"]);

            if (dw["BASLANGIC_SAATI"].ToString() != "" && dw["BITIS_SAATI"].ToString() != "")
            {
                //                       TABLE_UPDATE += " AND  REPLACE(BASLANGIC_SAATI,':','') >= '" + dw["BASLANGIC_SAATI"].ToString().Replace(":", "") + "00' AND  REPLACE(BASLANGIC_SAATI,':','') <= '" + dw["BITIS_SAATI"].ToString().Replace(":", "") + "00' ";
                TABLE_UPDATE += string.Format(" AND  [TİME İNT] >= '{0}00' AND  [TİME İNT] <= '{1}59' ", dw["BASLANGIC_SAATI"].ToString().Replace(":", ""), dw["BITIS_SAATI"].ToString().Replace(":", ""));
            }

            if (dw["BASLANGIC_TARIHI"].ToString() != "" && dw["BITIS_TARIHI"].ToString() != "")
            {
                DateTime BAS_TARIHI = DateTime.Parse(dw["BASLANGIC_TARIHI"].ToString());
                DateTime BIT_TARIHI = DateTime.Parse(dw["BITIS_TARIHI"].ToString());


                TABLE_UPDATE += string.Format(" AND  ([TARİH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARİH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
            }
            int _ROW = 0;
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format(" SELECT count(*) as ROWS FROM   " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}] WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
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
                SqlCommand myCmd = new SqlCommand();

                if ("Süre" == dw["HESAPLANMA_TURU"].ToString())
                {
                    myCmd.CommandText = string.Format("set rowcount {0}  UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{1}]  SET OPT_PT=@OPT_PT ,[NET TUTAR]=[SÜRE] * @BIRIM_FIYAT WHERE {2}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
                }
                if ("Ratecard" == dw["HESAPLANMA_TURU"].ToString())
                {
                    myCmd.CommandText = string.Format("set rowcount {0}  UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{1}]  SET OPT_PT=@OPT_PT ,[TUTAR TL] * @BIRIM_FIYAT WHERE {2}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
                }

                myCmd.Parameters.AddWithValue("@OPT_PT", dw["OPT_PT"]);
                myCmd.Parameters.AddWithValue("@BIRIM_FIYAT", dw["BIRIM_FIYAT"]);
                myCmd.CommandTimeout = 0;
                myCmd.Connection = myCon;
                myCmd.ExecuteNonQuery();
            }
        }
        public void SINEMA_HESAPLA(DateTime BASTAR, DateTime BITTAR, DataRow dw, SqlConnection myCon)
        {

            string TABLE_UPDATE = string.Format("  [YAYIN SINIFI]='SINEMA' and [MEDYA]='{0}'", dw["MECRA_KODU"]);
            if (dw["ILI"] != DBNull.Value && dw["ILI"] != string.Empty) TABLE_UPDATE += string.Format("  AND  [İLİ]='{0}' ", dw["ILI"]);
            if (dw["BOLGE"] != DBNull.Value && dw["BOLGE"] != string.Empty) TABLE_UPDATE += string.Format("  AND  [BÖLGE]='{0}'", dw["BOLGE"]);

            if (dw["BASLANGIC_TARIHI"].ToString() != "" && dw["BITIS_TARIHI"].ToString() != "")
            {
                DateTime BAS_TARIHI = DateTime.Parse(dw["BASLANGIC_TARIHI"].ToString());
                DateTime BIT_TARIHI = DateTime.Parse(dw["BITIS_TARIHI"].ToString());
                TABLE_UPDATE += string.Format(" AND  ([TARİH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARİH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
            }

            int _ROW = 0;
            string SQL = string.Format(" SELECT count(*) as ROWS FROM      " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}] WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
            SqlCommand cmds = new SqlCommand(SQL, myCon) { CommandText = SQL.ToString() };
            SqlDataReader rds = cmds.ExecuteReader();
            while (rds.Read())
            {
                _ROW = Convert.ToInt32(rds["ROWS"]);
            }
            rds.Close();

            if (_ROW > 0)
            {

                SqlCommand myCmd = new SqlCommand();
                if ("Süre" == dw["HESAPLANMA_TURU"].ToString())
                {
                    myCmd.CommandText = string.Format(" set rowcount {0} UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{1}]  SET [NET TUTAR]= ([SÜRE] / 35)  * @BIRIM_FIYAT  WHERE {2}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
                }
                if ("Ratecard" == dw["HESAPLANMA_TURU"].ToString())
                {
                    myCmd.CommandText = string.Format(" set rowcount {0} UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{1}]  SET ( [NET TUTAR]=[TUTAR TL]/ 35) * @BIRIM_FIYAT  WHERE {2}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
                }
                myCmd.Parameters.AddWithValue("@BIRIM_FIYAT", dw["BIRIM_FIYAT"]);
                myCmd.Connection = myCon;
                myCmd.CommandTimeout = 0;
                myCmd.ExecuteNonQuery();
            }

        }
        public void GAZETE_HESAPLA(DateTime BASTAR, DateTime BITTAR, DataRow dw, SqlConnection myCon)
        {
            SqlCommand myCmd = new SqlCommand();
            string TABLE_UPDATE = "  [YAYIN SINIFI]='GAZETE' ";

            if (dw.Table.Columns["MECRA_KODU"] != null)
            {
                if (dw["MECRA_KODU"] != DBNull.Value && dw["MECRA_KODU"] != string.Empty) TABLE_UPDATE += "  AND  [MEDYA]=@MEDYA ";
                myCmd.Parameters.AddWithValue("@MEDYA", dw["MECRA_KODU"].ToString().Replace("&", "'+char(38)+'"));
            }
            if (dw.Table.Columns["SAYFA_GRUBU"] != null)
            {
                if (dw["SAYFA_GRUBU"] != DBNull.Value && dw["SAYFA_GRUBU"] != string.Empty) TABLE_UPDATE += "  AND  [SAYFA GRUBU]=@SAYFA_GRUBU ";
                myCmd.Parameters.AddWithValue("@SAYFA_GRUBU", dw["SAYFA_GRUBU"]);
            }
            //if(dw.Table.Columns["YAYIN_TURU"] != null) 
            //{
            //   if (dw["YAYIN_TURU"] != DBNull.Value && dw["YAYIN_TURU"] != string.Empty) TABLE_UPDATE += "  AND  [YAYIN TÜRÜ]=@SAYFA_GRUBU ";
            //    myCmd.Parameters.AddWithValue("@YAYIN_TURU", dw["YAYIN_TURU"]);
            //}
            if (dw["BASLANGIC_TARIHI"].ToString() != "" && dw["BITIS_TARIHI"].ToString() != "")
            {
                DateTime BAS_TARIHI = DateTime.Parse(dw["BASLANGIC_TARIHI"].ToString());
                DateTime BIT_TARIHI = DateTime.Parse(dw["BITIS_TARIHI"].ToString());
                TABLE_UPDATE += string.Format(" AND  ([TARİH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARİH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
            }

            //{ // myCmd.CommandText = " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET " + Calculator_PAZARTESI + " WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Monday';";


            //if (FARKLI.ToString() == "False")
            //{ set rowcount " + _ROW + " 
            switch (dw["HESAPLANMA_TURU"].ToString())
            {
                case "Stcm":
                case "Santim":
                    myCmd.CommandText = string.Format(" UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}]  SET [NET TUTAR]= CASE WHEN [SANTIM]=0  THEN  [SAYFA]*@BIRIM_FIYAT  else [SANTIM] * @BIRIM_FIYAT  END where {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);

                    //+ ";  UPDATE dbo.[__TEMP_" + _GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SANTIM] * @BIRIM_FIYAT  WHERE  " + TABLE_UPDATE;
                    myCmd.Parameters.AddWithValue("@BIRIM_FIYAT", dw["BIRIM_FIYAT"]);

                    break;

                case "Sayfa":
                    myCmd.CommandText = string.Format(" UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}]  SET [NET TUTAR]=[SAYFA] * @BIRIM_FIYAT  WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);

                    myCmd.Parameters.AddWithValue("@BIRIM_FIYAT", dw["BIRIM_FIYAT"]);
                    break;

                case "Kutu":
                    myCmd.CommandText = string.Format(" UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}]  SET [NET TUTAR]=[EBAT-KUTU] * @BIRIM_FIYAT  WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);

                    myCmd.Parameters.AddWithValue("@BIRIM_FIYAT", dw["BIRIM_FIYAT"]);

                    break;
                case "Ratecard":
                    myCmd.CommandText = string.Format(" UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}]  SET [NET TUTAR]=[TUTAR TL] * @BIRIM_FIYAT  WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);

                    myCmd.Parameters.AddWithValue("@BIRIM_FIYAT", dw["BIRIM_FIYAT"]);
                    break;
            }


            foreach (SqlParameter parameter in myCmd.Parameters)
            {
                if (parameter.Value == null)
                {
                    parameter.Value = DBNull.Value;
                }
            }

            myCmd.CommandTimeout = 0;
            myCmd.Connection = myCon;
            myCmd.ExecuteNonQuery();


            //}
            //else
            //{ 

            //    switch (dw["HESAPLANMA_TURU"].ToString())
            //    {
            //        case "Sayfa":
            //            myCmd.CommandText = "  UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SAYFA] * @PAZARTESI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Monday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SAYFA] * @SALI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Tuesday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SAYFA] * @CARSAMBA  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Wednesday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SAYFA] * @PERSEMBE  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Thursday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SAYFA] * @CUMA  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Friday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SAYFA] * @CUMARTESI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Saturday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SAYFA] * @PAZAR  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Sunday';";



            //            break;
            //        case "Stcm":
            //        case "Santim":

            //            myCmd.CommandText = " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SANTIM] * @PAZARTESI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Monday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SANTIM] * @SALI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Tuesday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SANTIM] * @CARSAMBA  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Wednesday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SANTIM] * @PERSEMBE  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Thursday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SANTIM] * @CUMA  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Friday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SANTIM] * @CUMARTESI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Saturday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[SANTIM] * @PAZAR  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Sunday';";


            //            break;
            //        case "Kutu":

            //            myCmd.CommandText = " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[EBAT-KUTU] * @PAZARTESI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Monday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[EBAT-KUTU] * @SALI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Tuesday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[EBAT-KUTU] * @CARSAMBA  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Wednesday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[EBAT-KUTU] * @PERSEMBE  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Thursday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[EBAT-KUTU] * @CUMA  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Friday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[EBAT-KUTU] * @CUMARTESI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Saturday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[EBAT-KUTU] * @PAZAR  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Sunday';";



            //            break;
            //        case "Ratecard":


            //            myCmd.CommandText = " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[TUTAR TL] * @PAZARTESI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Monday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[TUTAR TL] * @SALI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Tuesday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[TUTAR TL] * @CARSAMBA  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Wednesday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[TUTAR TL] * @PERSEMBE  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Thursday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[TUTAR TL] * @CUMA  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Friday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[TUTAR TL] * @CUMARTESI  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Saturday';";
            //            myCmd.CommandText += " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET [NET TUTAR]=[TUTAR TL] * @PAZAR  WHERE " + TABLE_UPDATE + " and datename(dw,[TARİH])='Sunday';";


            //            break;
            //    }

            //myCmd.Parameters.AddWithValue("@PAZARTESI", dw["PAZARTESI"]);
            //myCmd.Parameters.AddWithValue("@SALI", dw["SALI"]);
            //myCmd.Parameters.AddWithValue("@CARSAMBA", dw["CARSAMBA"]);
            //myCmd.Parameters.AddWithValue("@PERSEMBE", dw["PERSEMBE"]);
            //myCmd.Parameters.AddWithValue("@CUMA", dw["CUMA"]);
            //myCmd.Parameters.AddWithValue("@CUMARTESI", dw["CUMARTESI"]);
            //myCmd.Parameters.AddWithValue("@PAZAR", dw["PAZAR"]); 


            //} 




            //}

        }
        public void DERGI_HESAPLA(DateTime BASTAR, DateTime BITTAR, DataRow dw, SqlConnection myCon)
        {

            string
                    TABLE_UPDATE = "  [YAYIN SINIFI]='DERGI' ";
            if (dw.Table.Columns["MECRA_KODU"] != null) { if (dw["MECRA_KODU"] != DBNull.Value && dw["MECRA_KODU"] != string.Empty) TABLE_UPDATE += string.Format("  AND  [ANA YAYIN]='{0}' ", dw["MECRA_KODU"]); }
            if (dw.Table.Columns["SAYFA_GRUBU"] != null) { if (dw["SAYFA_GRUBU"] != DBNull.Value && dw["SAYFA_GRUBU"] != string.Empty) TABLE_UPDATE += string.Format("  AND  [SAYFA GRUBU]='{0}'", dw["SAYFA_GRUBU"]); }

            if (dw["BASLANGIC_TARIHI"].ToString() != "" && dw["BITIS_TARIHI"].ToString() != "")
            {
                DateTime BAS_TARIHI = DateTime.Parse(dw["BASLANGIC_TARIHI"].ToString());
                DateTime BIT_TARIHI = DateTime.Parse(dw["BITIS_TARIHI"].ToString());
                TABLE_UPDATE += string.Format(" AND  ([TARİH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARİH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
            }

            int _ROW = 0;
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format(" SELECT count(*) as ROWS FROM    " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}] WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
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
                SqlCommand myCmd = new SqlCommand();
                if ("Sayfa" == dw["HESAPLANMA_TURU"].ToString())
                {
                    myCmd.CommandText = string.Format(" set rowcount {0} UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{1}]  SET [NET TUTAR]=[SAYFA] * @BIRIM_FIYAT  WHERE {2}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
                }
                if ("Ratecard" == dw["HESAPLANMA_TURU"].ToString())
                {
                    myCmd.CommandText = string.Format("set rowcount {0} UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{1}]  SET [NET TUTAR]=[TUTAR TL] * @BIRIM_FIYAT  WHERE {2}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
                }
                myCmd.Parameters.AddWithValue("@BIRIM_FIYAT", dw["BIRIM_FIYAT"]);
                myCmd.CommandTimeout = 0;
                myCmd.Connection = myCon;
                myCmd.ExecuteNonQuery();
            }

        }
        public void OUTDOOR_HESAPLA(DateTime BASTAR, DateTime BITTAR, DataRow dw, SqlConnection myCon)
        {

            string TABLE_UPDATE = "  [YAYIN SINIFI]='OUTDOOR' ";
            if (dw.Table.Columns["MECRA_KODU"] != null) { if (dw["MECRA_KODU"] != DBNull.Value && dw["MECRA_KODU"] != string.Empty) TABLE_UPDATE += string.Format("  AND  [MEDYA]='{0}' ", dw["MECRA_KODU"]); }
            if (dw.Table.Columns["ILI"] != null) { if (dw["ILI"] != DBNull.Value && dw["ILI"] != string.Empty) TABLE_UPDATE += string.Format("  AND  [İLİ]='{0}'", dw["ILI"]); }
            if (dw.Table.Columns["UNITE"] != null) { if (dw["UNITE"] != DBNull.Value && dw["UNITE"] != string.Empty) TABLE_UPDATE += string.Format("  AND  [ÜNİTE]='{0}'", dw["UNITE"]); }

            if (dw["BASLANGIC_TARIHI"].ToString() != "" && dw["BITIS_TARIHI"].ToString() != "")
            {
                DateTime BAS_TARIHI = DateTime.Parse(dw["BASLANGIC_TARIHI"].ToString());
                DateTime BIT_TARIHI = DateTime.Parse(dw["BITIS_TARIHI"].ToString());
                TABLE_UPDATE += string.Format(" AND  ([TARİH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARİH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
            }
            int _ROW = 0;
            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format(" SELECT count(*) as ROWS FROM    " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{0}] WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
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
                SqlCommand myCmd = new SqlCommand();
                if ("Frekans" == dw["HESAPLANMA_TURU"].ToString())
                {
                    myCmd.CommandText = string.Format(" set rowcount {0} UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{1}]  SET [NET TUTAR]=[FREKANS] * @BIRIM_FIYAT  WHERE {2}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
                }
                if ("Ratecard" == dw["HESAPLANMA_TURU"].ToString())
                {
                    myCmd.CommandText = string.Format(" set rowcount {0} UPDATE " + _GLOBAL_PARAMETRELER._RAPOR_DB + ".dbo.[__TEMP_{1}]  SET [NET TUTAR]=[TUTAR TL] * @BIRIM_FIYAT  WHERE {2}", _ROW, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
                }

                myCmd.Parameters.AddWithValue("@BIRIM_FIYAT", dw["BIRIM_FIYAT"]);
                myCmd.CommandTimeout = 0;
                myCmd.Connection = myCon;
                myCmd.ExecuteNonQuery();
            }

        }
        public void INTERNET_HESAPLA(DateTime BASTAR, DateTime BITTAR, DataRow dw, SqlConnection myCon)
        {

            string
                    TABLE_UPDATE = "  [YAYIN SINIFI]='INTERNET' ";
            if (dw["R_NETWORK"] != DBNull.Value && dw["R_NETWORK"] != string.Empty) TABLE_UPDATE += string.Format("  AND  [R.NETWORK]='{0}' ", dw["R_NETWORK"]);
            if (dw["SITE"] != DBNull.Value && dw["SITE"] != string.Empty) TABLE_UPDATE += string.Format("  AND  [SİTE]='{0}'", dw["SITE"]);
            if (dw["BASLANGIC_TARIHI"].ToString() != "" && dw["BITIS_TARIHI"].ToString() != "")
            {
                DateTime BAS_TARIHI = DateTime.Parse(dw["BASLANGIC_TARIHI"].ToString());
                DateTime BIT_TARIHI = DateTime.Parse(dw["BITIS_TARIHI"].ToString());


                TABLE_UPDATE += string.Format(" AND  ([TARIH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARIH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);

            }

            SqlCommand myCmd = new SqlCommand();
            if ("Adet" == dw["HESAPLANMA_TURU"].ToString())
            {
                myCmd.CommandText = string.Format("INSERT INTO [_TEMP_{0}] (REF_ID,YAYIN_SINIFI,[NET TUTAR]) SELECT ID,[YAYIN_SINIFI],[ADET] * @BIRIM_FIYAT  FROM   dbo.DATA_INTERNET_ADEX WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
            }
            if ("Ratecard" == dw["HESAPLANMA_TURU"].ToString())
            {
                myCmd.CommandText = string.Format("INSERT INTO [_TEMP_{0}] (REF_ID,YAYIN_SINIFI,[NET TUTAR]) SELECT ID,[YAYIN_SINIFI], [TUTAR_TL] * @BIRIM_FIYAT  FROM   dbo.DATA_INTERNET_ADEX WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
            }

            myCmd.Parameters.AddWithValue("@BIRIM_FIYAT", dw["BIRIM_FIYAT"]);
            myCmd.CommandTimeout = 0;
            myCmd.Connection = myCon;
            myCmd.ExecuteNonQuery();

        }
        public void SEKTOR_HESAPLA(DateTime BASTAR, DateTime BITTAR, DataRow dw, SqlConnection myCon)
        {
            string TABLE_UPDATE = "";

            if (dw["SEKTOR"] != DBNull.Value && dw["SEKTOR"] != string.Empty) TABLE_UPDATE += string.Format("  AND  SEKTOR='{0}' ", dw["SEKTOR"]);


            SqlCommand myCmd = new SqlCommand();

            string Calculator_PAZARTESI = "";
            string Calculator_SALI = "";
            string Calculator_CARSAMBA = "";
            string Calculator_PERSEMBE = "";
            string Calculator_CUMA = "";
            string Calculator_CUMARTESI = "";
            string Calculator_PAZAR = "";

            if ("Santim" == dw["HESAPLANMA_TURU"].ToString())
            {
                // Calculator = "[NET TUTAR] = SANTIM *" + dw["BIRIM_FIYAT"].ToString().Replace(",", "."); ;

                var OCAK = dw["OCAK"];
                var SUBAT = dw["SUBAT"];
                var MART = dw["MART"];
                var NISAN = dw["NISAN"];
                var MAYIS = dw["MAYIS"];
                var HAZIRAN = dw["HAZIRAN"];
                var TEMMUZ = dw["TEMMUZ"];
                var AGUSTOS = dw["AGUSTOS"];
                var EYLUL = dw["EYLUL"];
                var EKIM = dw["EKIM"];
                var KASIM = dw["KASIM"];
                var ARALIK = dw["ARALIK"];


                if (Convert.ToDouble(OCAK.ToString()) > 0) Calculator_PAZARTESI = " [NET TUTAR] = SANTIM *" + dw["PAZARTESI"].ToString().Replace(",", ".");
                if (Convert.ToDouble(SUBAT.ToString()) > 0) Calculator_SALI = " [NET TUTAR] = SANTIM *" + dw["SALI"].ToString().Replace(",", ".");
                if (Convert.ToDouble(MART.ToString()) > 0) Calculator_CARSAMBA = " [NET TUTAR] = SANTIM *" + dw["CARSAMBA"].ToString().Replace(",", ".");
                if (Convert.ToDouble(NISAN.ToString()) > 0) Calculator_PERSEMBE = " [NET TUTAR] = SANTIM *" + dw["PERSEMBE"].ToString().Replace(",", ".");
                if (Convert.ToDouble(MAYIS.ToString()) > 0) Calculator_CUMA = " [NET TUTAR] = SANTIM *" + dw["CUMA"].ToString().Replace(",", ".");
                if (Convert.ToDouble(HAZIRAN.ToString()) > 0) Calculator_CUMARTESI = " [NET TUTAR] = SANTIM *" + dw["CUMARTESI"].ToString().Replace(",", ".");
                if (Convert.ToDouble(TEMMUZ.ToString()) > 0) Calculator_PAZAR = " [NET TUTAR] = SANTIM *" + dw["PAZAR"].ToString().Replace(",", ".");

            }

            //myCmd.CommandText = " UPDATE dbo.[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "]  SET " + Calculator + " WHERE " + TABLE_UPDATE;

            myCmd.CommandText = string.Format(" UPDATE dbo.[__TEMP_{0}]  SET {1} WHERE {2} and datename(dw,TARIH)='Monday';", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, Calculator_PAZARTESI, TABLE_UPDATE);
            myCmd.CommandText += string.Format(" UPDATE dbo.[__TEMP_{0}]  SET {1} WHERE {2} and datename(dw,TARIH)='Tuesday';", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, Calculator_SALI, TABLE_UPDATE);
            myCmd.CommandText += string.Format(" UPDATE dbo.[__TEMP_{0}]  SET {1} WHERE {2} and datename(dw,TARIH)='Wednesday';", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, Calculator_CARSAMBA, TABLE_UPDATE);
            myCmd.CommandText += string.Format(" UPDATE dbo.[__TEMP_{0}]  SET {1} WHERE {2} and datename(dw,TARIH)='Thursday';", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, Calculator_PERSEMBE, TABLE_UPDATE);
            myCmd.CommandText += string.Format(" UPDATE dbo.[__TEMP_{0}]  SET {1} WHERE {2} and datename(dw,TARIH)='Friday';", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, Calculator_CUMA, TABLE_UPDATE);
            myCmd.CommandText += string.Format(" UPDATE dbo.[__TEMP_{0}]  SET {1} WHERE {2} and datename(dw,TARIH)='Saturday';", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, Calculator_CUMARTESI, TABLE_UPDATE);
            myCmd.CommandText += string.Format(" UPDATE dbo.[__TEMP_{0}]  SET {1} WHERE {2} and datename(dw,TARIH)='Sunday';", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, Calculator_PAZAR, TABLE_UPDATE);

            myCmd.Connection = myCon;
            myCmd.ExecuteNonQuery();




        }
        public void ORAN_HESAPLA(DateTime BASTAR, DateTime BITTAR, DataRow dw, SqlConnection myCon)
        {

            string TABLE_UPDATE = "";
            if (dw["YAYIN SINIFI"] != DBNull.Value && dw["YAYIN SINIFI"] != string.Empty) TABLE_UPDATE += string.Format("  [YAYIN SINIFI]='{0}' ", dw["R_NETWORK"]);

            if (dw["BASLANGIC_TARIHI"].ToString() != "" && dw["BITIS_TARIHI"].ToString() != "")
            {
                DateTime BAS_TARIHI = DateTime.Parse(dw["BASLANGIC_TARIHI"].ToString());
                DateTime BIT_TARIHI = DateTime.Parse(dw["BITIS_TARIHI"].ToString());
                TABLE_UPDATE += string.Format(" AND  ([TARIH] >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND ([TARIH] <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);

            }

            SqlCommand myCmd = new SqlCommand();
            if ("Adet" == dw["HESAPLANMA_TURU"].ToString())
            {
                myCmd.CommandText = string.Format("INSERT INTO [_TEMP_{0}] (REF_ID,YAYIN_SINIFI,[NET TUTAR]) SELECT ID,[YAYIN_SINIFI],[ADET] * @BIRIM_FIYAT  FROM   dbo.DATA_INTERNET_ADEX WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
            }
            if ("Ratecard" == dw["HESAPLANMA_TURU"].ToString())
            {
                myCmd.CommandText = string.Format("INSERT INTO [_TEMP_{0}] (REF_ID,YAYIN_SINIFI,[NET TUTAR]) SELECT ID,[YAYIN_SINIFI], [TUTAR_TL] * @BIRIM_FIYAT  FROM   dbo.DATA_INTERNET_ADEX WHERE {1}", _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, TABLE_UPDATE);
            }

            myCmd.Parameters.AddWithValue("@BIRIM_FIYAT", dw["BIRIM_FIYAT"]);
            myCmd.CommandTimeout = 0;
            myCmd.Connection = myCon;
            myCmd.ExecuteNonQuery();

        }

    }
}
