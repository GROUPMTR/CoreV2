using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

namespace CoreV2
{
    public class _QUERY
    {
        public string OZEL_FILITRE(TreeListNode myNode, TreeListNode GENEL_FILITRELER, bool TOGGLE_GENEL_FILITRE, string QUERY, string TMP_QUERY)
        {
            string DAHIL_HARIC = "Dahil", BASLIK_BUL="";
            if (myNode != null)
            {
                ArrayList selectedNodes = new ArrayList();
                selectChildren(myNode, selectedNodes);
                foreach (TreeListNode item in selectedNodes)
                {
                    if (item.GetValue("TYPE").ToString() == "KIRILIM")
                    {
                        if (QUERY != "") TMP_QUERY += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY, 4, "").ToString());
                        if (QUERY.Length > 0 )
                        {
                            break;
                        }
                        else
                        { break; }
                    }

                    if (item.GetValue("TEXT").ToString().IndexOf("(Dahil)") != -1) DAHIL_HARIC = "Dahil";
                    if (item.GetValue("TEXT").ToString().IndexOf("(Hariç)") != -1) DAHIL_HARIC = "Hariç";
                    if (item.GetValue("TYPE").ToString() == "BASLIK")
                    {
                        if (QUERY != "") TMP_QUERY += string.Format("({0}) AND ", TEMIZLE_ELEMAN(TMP_QUERY, 4, "").ToString());
                            QUERY =  "";

                        if (item.GetValue("NAME").ToString() != "")
                        {
                            using (SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                            {
                                SqlCommand myCommand = new SqlCommand(string.Format(" SELECT   * FROM   dbo.ADM_SECENEKLER where SECENEK='{0}'", item.GetValue("NAME").ToString()), myConnection);
                                myConnection.Open();
                                SqlDataReader myReader = myCommand.ExecuteReader();
                                while (myReader.Read())
                                {
                                    BASLIK_BUL = "DT." + myReader["FIELDS"].ToString(); 
                                }
                            }
                        }
                    }
                    if (item.GetValue("TYPE").ToString() == "SATIR")
                    {
                        if (DAHIL_HARIC == "Dahil")
                        {
                            if (item.GetValue("TEXT").ToString().IndexOf("%") != -1)
                            {
                                QUERY += string.Format("{0} LIKE '{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));                               
                            }
                            else
                            {
                                QUERY += string.Format("{0}='{1}' OR ", BASLIK_BUL, item.GetValue("TEXT"));                               
                            }
                        }

                        if (DAHIL_HARIC == "Hariç")
                        {
                            if (item.GetValue("TEXT").ToString().IndexOf("%") != -1)
                            {
                                QUERY += string.Format("{0} NOT LIKE '{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));                               
                            }
                            else
                            {
                                QUERY += string.Format("{0}<>'{1}' AND ", BASLIK_BUL, item.GetValue("TEXT"));                               
                            }
                        }
                    }
                }
                if (QUERY != "") TMP_QUERY += string.Format("({0}) AND ", TEMIZLE_ELEMAN(QUERY, 4, "").ToString());
            }
            return TMP_QUERY;            
        }



        public string FILITRE_SQL_DATA_READ_INSERT(string NodeName,DateTime DT_GNL_RPR_BAS_TARIHI, DateTime DT_GNL_RPR_BIT_TARIHI,
            bool TOOGLE_TELEVIZYON, bool TOOGLE_GAZETE, bool TOOGLE_DERGI, bool TOOGLE_OUTDOOR, bool TOOGLE_SINEMA, bool TOOGLE_RADYO, bool TOOGLE_INTERNET)
        {
            //for (int iIndex = 0; iIndex < BASLIKLAR_LISTESI.Count; iIndex++)
            //{
            //    BASLIK += BASLIKLAR_LISTESI[iIndex];
            //}
            //for (int iIndex = 0; iIndex < FI_TELEVIZYON.Count; iIndex++)
            //{
            //    FIELD_TELEVIZYON += FI_TELEVIZYON[iIndex];
            //}
            //for (int iIndex = 0; iIndex < FI_GAZETE.Count; iIndex++)
            //{
            //    FIELD_GAZETE += FI_GAZETE[iIndex];
            //}
            //for (int iIndex = 0; iIndex < FI_DERGI.Count; iIndex++)
            //{
            //    FIELD_DERGI += FI_DERGI[iIndex];
            //}
            //for (int iIndex = 0; iIndex < FI_SINEMA.Count; iIndex++)
            //{
            //    FIELD_SINEMA += FI_SINEMA[iIndex];
            //}
            //for (int iIndex = 0; iIndex < FI_OUTDOOR.Count; iIndex++)
            //{
            //    FIELD_OUTDOOR += FI_OUTDOOR[iIndex];
            //}
            //for (int iIndex = 0; iIndex < FI_RADYO.Count; iIndex++)
            //{
            //    FIELD_RADYO += FI_RADYO[iIndex];
            //}
            //for (int iIndex = 0; iIndex < FI_INTERNET.Count; iIndex++)
            //{
            //    FIELD_INTERNET += FI_INTERNET[iIndex];
            //}
            //BASLIK = TEMIZLE_ELEMAN(BASLIK, 1, ",").ToString();
            //FIELD_TELEVIZYON = TEMIZLE_ELEMAN(FIELD_TELEVIZYON, 1, ",").ToString();
            //FIELD_GAZETE = TEMIZLE_ELEMAN(FIELD_GAZETE, 1, ",").ToString();
            //FIELD_DERGI = TEMIZLE_ELEMAN(FIELD_DERGI, 1, ",").ToString();
            //FIELD_SINEMA = TEMIZLE_ELEMAN(FIELD_SINEMA, 1, ",").ToString();
            //FIELD_OUTDOOR = TEMIZLE_ELEMAN(FIELD_OUTDOOR, 1, ",").ToString();
            //FIELD_RADYO = TEMIZLE_ELEMAN(FIELD_RADYO, 1, ",").ToString();
            //FIELD_INTERNET = TEMIZLE_ELEMAN(FIELD_INTERNET, 1, ",").ToString();





            DateTime BAS_TARIHI = Convert.ToDateTime(DT_GNL_RPR_BAS_TARIHI);
            DateTime BIT_TARIHI = Convert.ToDateTime(DT_GNL_RPR_BIT_TARIHI);
            if (TOOGLE_TELEVIZYON)
            {
 
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {


                //    //WITH (TABLOCK) 
                //    //string SQLTELEVIZYON = " INSERT INTO  [dbo].[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "] (" + STATIC_NAME + rpr.TABLE_CREATE_INSERT_QUERY + " )" +
                //    //  " SELECT " + CAST_FIELD + rpr.FIELD_TELEVIZYON + " " +
                //    //  " FROM  dbo.DATA_TELEVIZYON_ADEX as DT " +
                //    //  " Where  " + QUERY + "  (TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))";

                //    string SQLTELEVIZYON = "";
                //    if (BAS_TARIHI.Year == BIT_TARIHI.Year)
                //    {
                //        string SQLJOIN = "";// MASTER_ALL_EXCLUDE();
                //        FIELD_TELEVIZYON = FIELD_TELEVIZYON.Replace("DT.cast", "cast").Replace("DT.substring", "substring");

                //        SQLTELEVIZYON = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM, BASLIK) +
                //                                      " SELECT " + KIRILIM_CAST + FIELD_TELEVIZYON + " " +
                //                                      " FROM  dbo.DATA_TELEVIZYON_ADEX_" + BAS_TARIHI.Year + " as DT " + SQLJOIN +
                //                                      " Where  " + TMP_QUERY_TELEVIZYON + "  (DT.TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (DT.TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))";
                //    }
                //    else
                //    {
                //        SQLTELEVIZYON = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM, BASLIK);
                //        for (int iX = 0; iX <= BIT_TARIHI.Year - BAS_TARIHI.Year; iX++)
                //        {
                //            int YILLAR = BAS_TARIHI.Year + iX;

                //            string SQLJOIN = "";// MASTER_ALL_EXCLUDE();


                //            SQLTELEVIZYON += " SELECT " + KIRILIM_CAST + FIELD_TELEVIZYON + " " +
                //                            " FROM  dbo.DATA_TELEVIZYON_ADEX_" + YILLAR + " as DT " + SQLJOIN +

                //                            " Where  " + TMP_QUERY_TELEVIZYON + "  (TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))" +

                //                            " UNION ALL ";
                //        }
                //        if (SQLTELEVIZYON != "") { if (SQLTELEVIZYON.Length > 0) { SQLTELEVIZYON = SQLTELEVIZYON.Substring(0, SQLTELEVIZYON.Length - 11); } }
                //    }

                //    SqlCommand command = new SqlCommand(SQLTELEVIZYON, conn);
                //    conn.Open();
                //    command.CommandTimeout = 0;
                //    command.ExecuteReader(CommandBehavior.CloseConnection);
                //    conn.Close();
                 }
            }
            if (TOOGLE_GAZETE)
            {
 
                //using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                //{

                //    FIELD_GAZETE = FIELD_GAZETE.Replace("DT.cast", "cast").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE");
                //    string SQLGAZETE = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM, BASLIK);
                //    SQLGAZETE += String.Format(" SELECT " + KIRILIM_CAST + FIELD_GAZETE + "  FROM dbo.DATA_BASIN_ADEX as DT  Where  {0}", TMP_QUERY_GAZETE);
                //    SQLGAZETE += string.Format(" YAYIN_SINIFI='GAZETE'  AND  (TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                //    SqlCommand command = new SqlCommand(SQLGAZETE, conn);
                //    conn.Open();
                //    command.CommandTimeout = 0;
                //    command.ExecuteReader(CommandBehavior.CloseConnection);
                //    conn.Close();
                //}
            }
            if (TOOGLE_DERGI)
            {
           
                //using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                //{
                //    FIELD_DERGI = FIELD_DERGI.Replace("DT.cast", "cast").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE");
                //    string SQLDERGI = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]   ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM, BASLIK);
                //    SQLDERGI += String.Format(" SELECT " + KIRILIM_CAST + FIELD_DERGI + "    FROM dbo.DATA_BASIN_ADEX as DT Where  {0}", TMP_QUERY_DERGI);
                //    SQLDERGI += string.Format(" YAYIN_SINIFI='DERGI'  AND  (TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                //    SqlCommand command = new SqlCommand(SQLDERGI, conn);
                //    conn.Open();
                //    command.CommandTimeout = 0;
                //    command.ExecuteReader(CommandBehavior.CloseConnection);
                //    conn.Close();
                //}
            }

            if (TOOGLE_OUTDOOR)
            {
     
                //using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                //{
                //    FIELD_OUTDOOR = FIELD_OUTDOOR.Replace("DT.cast", "cast").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE");

                //    string SQLOUTDOOR = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]   ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM, BASLIK);
                //    SQLOUTDOOR += String.Format(" SELECT " + KIRILIM_CAST + FIELD_OUTDOOR + "   FROM dbo.DATA_OUTDOOR_ADEX as DT Where  {0}", TMP_QUERY_OUTDOOR);
                //    SQLOUTDOOR += string.Format("  (TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                //    SqlCommand command = new SqlCommand(SQLOUTDOOR, conn);
                //    conn.Open();
                //    command.CommandTimeout = 0;
                //    command.ExecuteReader(CommandBehavior.CloseConnection);
                //    conn.Close();
                //}
            }

            if (TOOGLE_SINEMA)
            {
      
                //using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                //{
                //    FIELD_SINEMA = FIELD_SINEMA.Replace("DT.cast", "cast").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE");

                //    string SQLSINEMA = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]   ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM, BASLIK);
                //    SQLSINEMA += String.Format(" SELECT " + KIRILIM_CAST + FIELD_SINEMA + "    FROM dbo.DATA_SINEMA_ADEX as DT Where  {0}", TMP_QUERY_SINEMA);
                //    SQLSINEMA += string.Format(" (TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                //    SqlCommand command = new SqlCommand(SQLSINEMA, conn);
                //    conn.Open();
                //    command.CommandTimeout = 0;
                //    command.ExecuteReader(CommandBehavior.CloseConnection);
                //    conn.Close();
                //}
            }

            if (TOOGLE_RADYO)
            {
 
                //using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                //{

                //    FIELD_RADYO = FIELD_RADYO.Replace("DT.cast", "cast").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE");
                //    string SQLRADYO = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM, BASLIK);
                //    SQLRADYO += String.Format(" SELECT " + KIRILIM_CAST + FIELD_RADYO + "  FROM dbo.DATA_RADYO_ADEX as DT Where  {0}", TMP_QUERY_RADYO);
                //    SQLRADYO += string.Format(" (TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                //    SqlCommand command = new SqlCommand(SQLRADYO, conn);
                //    conn.Open();
                //    command.CommandTimeout = 0;
                //    command.ExecuteReader(CommandBehavior.CloseConnection);
                //    conn.Close();
                //}
            }
            if (TOOGLE_INTERNET)
            {
 
                //using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                //{
                //    FIELD_INTERNET = FIELD_INTERNET.Replace("DT.cast", "cast").Replace("DT.substring", "substring").Replace("DT.(CASE", "(CASE");
                //    string SQLINTERNET = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]   ({2}{3})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_NAME, KIRILIM, BASLIK);
                //    SQLINTERNET = String.Format(" SELECT " + KIRILIM_CAST + FIELD_INTERNET + "  FROM dbo.DATA_INTERNET_ADEX as DT Where  {0}", TMP_QUERY_INTERNET);
                //    SQLINTERNET += string.Format("    (TARIH >=  CONVERT(DATETIME, '{0:yyyy-MM-dd}', 102))  AND (TARIH <= CONVERT(DATETIME, '{1:yyyy-MM-dd}', 102))", BAS_TARIHI, BIT_TARIHI);
                //    SqlCommand command = new SqlCommand(SQLINTERNET, conn);
                //    conn.Open();
                //    command.CommandTimeout = 0;
                //    command.ExecuteReader(CommandBehavior.CloseConnection);
                //    conn.Close();
                //}
            }
            //TMP_QUERY_TELEVIZYON = TMP_QUERY_GAZETE = TMP_QUERY_DERGI = TMP_QUERY_SINEMA = TMP_QUERY_RADYO = TMP_QUERY_OUTDOOR = TMP_QUERY_INTERNET = "";
            //QUERY_TELEVIZYON = QUERY_GAZETE = QUERY_DERGI = QUERY_SINEMA = QUERY_RADYO = QUERY_OUTDOOR = QUERY_INTERNET = "";

            //BASLIKLAR_LISTESI.Clear();
            //BASLIKLAR_TABLE_CREATE.Clear();
            //FI_TELEVIZYON.Clear();
            //FI_GAZETE.Clear();
            //FI_DERGI.Clear();
            //FI_SINEMA.Clear();
            //FI_RADYO.Clear();
            //FI_OUTDOOR.Clear();
            //FI_INTERNET.Clear();
            //FI_ONBES_DK.Clear();


            return "Tamam";
        }




        public string FILITRE_SQL_DATA_READ_INSERT_TELEVIZYON(string NodeName, DateTime DT_GNL_RPR_BAS_TARIHI, DateTime DT_GNL_RPR_BIT_TARIHI, ArrayList SELECT_HEADER_ARRY,ArrayList SELECT_FIELD_ARRY, ArrayList INSERT_FIELD_ARRY,string STR_WHERE)
        {
            string SELECT_FIELD = "";
            string INSERT_FIELD = "";
            for (int iIndex = 0; iIndex < SELECT_FIELD_ARRY.Count; iIndex++)
            {
                SELECT_FIELD += SELECT_FIELD_ARRY[iIndex];
            }
            for (int iIndex = 0; iIndex < INSERT_FIELD_ARRY.Count; iIndex++)
            {
                INSERT_FIELD += INSERT_FIELD_ARRY[iIndex];
            }

            SELECT_FIELD = TEMIZLE_ELEMAN(SELECT_FIELD, 1, ",").ToString();
            INSERT_FIELD = TEMIZLE_ELEMAN(INSERT_FIELD, 1, ",").ToString();




            DateTime BAS_TARIHI = Convert.ToDateTime(DT_GNL_RPR_BAS_TARIHI);
            DateTime BIT_TARIHI = Convert.ToDateTime(DT_GNL_RPR_BIT_TARIHI);
         

                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {


                //WITH (TABLOCK) 
                //string SQLTELEVIZYON = " INSERT INTO  [dbo].[__TEMP_" + GLOBAL_PARAMETRELER._KULLANICI_NAME + "] (" + STATIC_NAME + rpr.TABLE_CREATE_INSERT_QUERY + " )" +
                //  " SELECT " + CAST_FIELD + rpr.FIELD_TELEVIZYON + " " +
                //  " FROM  dbo.DATA_TELEVIZYON_ADEX as DT " +
                //  " Where  " + QUERY + "  (TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))";

                string SQLTELEVIZYON = "";
                if (BAS_TARIHI.Year == BIT_TARIHI.Year)
                {
                    string SQLJOIN = "";// MASTER_ALL_EXCLUDE();
                   // FIELD_TELEVIZYON = FIELD_TELEVIZYON.Replace("DT.cast", "cast").Replace("DT.substring", "substring");

                    SQLTELEVIZYON = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}]  ({2})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, INSERT_FIELD) +
                                                  " SELECT " + SELECT_FIELD + " " +
                                                  " FROM  dbo.DATA_TELEVIZYON_ADEX_" + BAS_TARIHI.Year + " as DT " + SQLJOIN +
                                                  " Where  " + STR_WHERE + "  (DT.TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (DT.TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))";
                }
                else
                {
                    SQLTELEVIZYON = string.Format(" INSERT INTO  {0}.[dbo].[__TEMP_{1}] ({2})", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME, INSERT_FIELD);
                    for (int iX = 0; iX <= BIT_TARIHI.Year - BAS_TARIHI.Year; iX++)
                    {
                        int YILLAR = BAS_TARIHI.Year + iX;

                        string SQLJOIN = "";// MASTER_ALL_EXCLUDE();


                        SQLTELEVIZYON += " SELECT " + SELECT_FIELD  + " " +
                                        " FROM  dbo.DATA_TELEVIZYON_ADEX_" + YILLAR + " as DT " + SQLJOIN +

                                        " Where  " + STR_WHERE + "  (TARIH >=CONVERT(DATETIME, '" + BAS_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))  AND (TARIH <= CONVERT(DATETIME, '" + BIT_TARIHI.ToString("yyyy-MM-dd 00:00:00") + "', 102))" +

                                        " UNION ALL ";
                    }
                    if (SQLTELEVIZYON != "") { if (SQLTELEVIZYON.Length > 0) { SQLTELEVIZYON = SQLTELEVIZYON.Substring(0, SQLTELEVIZYON.Length - 11); } }
                }

                SqlCommand command = new SqlCommand(SQLTELEVIZYON, conn);
                conn.Open();
                command.CommandTimeout = 0;
                command.ExecuteReader(CommandBehavior.CloseConnection);
                conn.Close();
            }
           
            
            //TMP_QUERY_TELEVIZYON = TMP_QUERY_GAZETE = TMP_QUERY_DERGI = TMP_QUERY_SINEMA = TMP_QUERY_RADYO = TMP_QUERY_OUTDOOR = TMP_QUERY_INTERNET = "";
            //QUERY_TELEVIZYON = QUERY_GAZETE = QUERY_DERGI = QUERY_SINEMA = QUERY_RADYO = QUERY_OUTDOOR = QUERY_INTERNET = "";

            //BASLIKLAR_LISTESI.Clear();
            //BASLIKLAR_TABLE_CREATE.Clear();
            //FI_TELEVIZYON.Clear();
            //FI_GAZETE.Clear();
            //FI_DERGI.Clear();
            //FI_SINEMA.Clear();
            //FI_RADYO.Clear();
            //FI_OUTDOOR.Clear();
            //FI_INTERNET.Clear();
            //FI_ONBES_DK.Clear();


            return "Tamam";
        }




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
        public string GetFullPath(TreeListNode node, string pathSeparator)
        {
            if (node == null) return "";
            string result = "";
            while (node != null)
            {
                result = pathSeparator + node.GetDisplayText("TEXT") + result;
                node = node.ParentNode;
            }
            return result;
        }
        void selectChildren(TreeListNode parent, ArrayList selectedNodes)
        {
            IEnumerator en = parent.Nodes.GetEnumerator();
            TreeListNode child;
            while (en.MoveNext())
            {
                child = (TreeListNode)en.Current;
                if (child.Checked) selectedNodes.Add(child);
                if (child.HasChildren && child.Checked) selectChildren(child, selectedNodes);
            }
        }

        public DataTable TABLE_CREATE(ArrayList BASLIKLAR_TABLE_CREATE, string BR_RAPOR_TYPE)
        {
            DataTable myTable;
            //if (BASLIKLAR_TABLE_CREATE.Count == 0) { return; }

            string BASLIK_TABLE_CREATE = "";
            for (int iIndex = 0; iIndex < BASLIKLAR_TABLE_CREATE.Count; iIndex++)
            {
                BASLIK_TABLE_CREATE += BASLIKLAR_TABLE_CREATE[iIndex];
            } 
            if (BASLIK_TABLE_CREATE.Length > 0) BASLIK_TABLE_CREATE = BASLIK_TABLE_CREATE.Substring(0, BASLIK_TABLE_CREATE.Length - 1);

            using (SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format("IF EXISTS (SELECT * FROM {0}.dbo.sysobjects where id = object_id('{0}.[dbo].[__TEMP_{1}]')) drop table {0}.[dbo].[__TEMP_{1}]", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME);
                SqlCommand myCommandTable = new SqlCommand(SQL) { Connection = myConnectionTable, CommandTimeout = 0 };
                myConnectionTable.Open();
                myCommandTable.ExecuteNonQuery();
                myCommandTable.Connection.Close();
                myConnectionTable.Close();
            }
            using (SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                string SQL = string.Format("   CREATE TABLE {0}.[dbo].[__TEMP_{1}]  ( {2},[NET TUTAR] [float] NULL,[OPT_PT] [nvarchar](15) NULL ) ON [PRIMARY];", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME,   BASLIK_TABLE_CREATE);
                SqlCommand myCommandTable = new SqlCommand(SQL) { Connection = myConnectionTable };
                myConnectionTable.Open();
                myCommandTable.CommandTimeout = 0;
                myCommandTable.ExecuteNonQuery();
                myCommandTable.Connection.Close();
                myConnectionTable.Close();
            }
            if (BR_RAPOR_TYPE == "SABIT")
            {
                using (SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    string SQL = string.Format(" IF NOT EXISTS (SELECT * FROM {0}.dbo.sysobjects where id = object_id('{0}.[dbo].[__TEMP_{1}]')) CREATE TABLE {0}.[dbo].[__TEMP_{1}]  ({2} , [NET TUTAR] [float] NULL,[OPT_PT] [nvarchar](15) NULL ) ON [PRIMARY];", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._SABIT_TABLO_ADI,  BASLIK_TABLE_CREATE);
                    SqlCommand myCommandTable = new SqlCommand(SQL) { Connection = myConnectionTable };
                    myConnectionTable.Open();
                    myCommandTable.CommandTimeout = 0;
                    myCommandTable.ExecuteNonQuery();
                    myCommandTable.Connection.Close();
                    myConnectionTable.Close();
                }
            }
            using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                DataSet ds = new DataSet();
                string query = string.Format("      SELECT  top 100 * FROM  {0}.[dbo].[__TEMP_{1}]   ", _GLOBAL_PARAMETRELER._RAPOR_DB, _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME);
                SqlDataAdapter adapter = new SqlDataAdapter() { SelectCommand = new SqlCommand(query, conn) };
                adapter.Fill(ds, "dbo_TEMP_RAPOR");
                myTable = ds.Tables[0];
            }

            return myTable;
        }


    }



}
