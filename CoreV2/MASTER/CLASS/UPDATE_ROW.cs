using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;

namespace CoreV2.MASTER.CLASS
{
    public class UPDATE_ROW
    {
        public void _UPDATE_KIRILIMLI(SqlConnection Con, DataRow row,  string _PLAN_KODU)
        {
            if (row["ITEM_NAME"].ToString() != "")
            {
                string StrField = null;
                SqlCommand myCmd = new SqlCommand();
                string[,] strFieldNameInt = new string[,] { {  "GUI",   "TUTAR"} };
                foreach (string strsInt in strFieldNameInt)
                {
                    if (row[strsInt] != DBNull.Value)
                    {
                        if (row[strsInt].ToString() == "NaN") row[strsInt] = 0;

                        myCmd.Parameters.AddWithValue("@" + strsInt.ToString(), row[strsInt]);
                        StrField = StrField + "[" + row.Table.Columns[strsInt].ColumnName + "] = @" + strsInt.ToString() + ",";
                    }
                }
                myCmd.Parameters.AddWithValue("@ID", row["ID"]);
                //myCmd.Parameters.AddWithValue("@SIRKETREF", _GLOBAL_PARAMETERS._SIRKET_ID);
                //myCmd.Parameters.AddWithValue("@MUSTERIREF", _MUSTERIREF);
                //myCmd.Parameters.AddWithValue("@KAMPANYAREF", _KAMPANYAREF);
                //myCmd.Parameters.AddWithValue("@PLANREF", _PLANREF);
                // (SIRKETREF=@SIRKETREF and MUSTERIREF=@MUSTERIREF and KAMPANYAREF=@KAMPANYAREF and PLANREF=@PLANREF and ID=@ID )
                myCmd.Parameters.AddWithValue("@GUNCELLEME_YAPAN", System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
                foreach (SqlParameter parameter in myCmd.Parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                myCmd.CommandText = "UPDATE  dbo.MDY_PLAN_BUDGET_CLIENT SET " + StrField + " GUNCELLEME_YAPAN=@GUNCELLEME_YAPAN where  ( ID=@ID )";
                myCmd.Connection = Con;
                myCmd.ExecuteNonQuery();
            }

        }

        public void _UPDATE_MEASURED(SqlConnection Con, DataRow row, int _MUSTERIREF, int _KAMPANYAREF, int _PLANREF, string _PLAN_KODU)
        {

            if (row["MAIN_CODE"].ToString() != "")
            {
                string StrField = null;
                SqlCommand myCmd = new SqlCommand();
                string[,] strFieldNameInt = new string[,] { {  "GUI",   "ITEM_NAME", "OCAK","SUBAT","MART","NISAN","MAYIS","HAZIRAN","TEMMUZ","AGUSTOS","EYLUL","EKIM","KASIM","ARALIK"} };
                foreach (string strsInt in strFieldNameInt)
                {
                    if (row[strsInt] != DBNull.Value)
                    {
                        if (row[strsInt].ToString() == "NaN") row[strsInt] = 0;

                        myCmd.Parameters.AddWithValue("@" + strsInt.ToString(), row[strsInt]);
                        StrField = StrField + "[" + row.Table.Columns[strsInt].ColumnName + "] = @" + strsInt.ToString() + ",";
                    }
                }
                myCmd.Parameters.AddWithValue("@ID", row["ID"]);
                //myCmd.Parameters.AddWithValue("@SIRKETREF", _GLOBAL_PARAMETERS._SIRKET_ID);
                //myCmd.Parameters.AddWithValue("@MUSTERIREF", _MUSTERIREF);
                //myCmd.Parameters.AddWithValue("@KAMPANYAREF", _KAMPANYAREF);
                //myCmd.Parameters.AddWithValue("@PLANREF", _PLANREF);
                // (SIRKETREF=@SIRKETREF and MUSTERIREF=@MUSTERIREF and KAMPANYAREF=@KAMPANYAREF and PLANREF=@PLANREF and ID=@ID )
                myCmd.Parameters.AddWithValue("@GUNCELLEME_YAPAN", System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
                foreach (SqlParameter parameter in myCmd.Parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                myCmd.CommandText = "UPDATE  dbo.MDY_PLAN_BUDGET_CLIENT SET " + StrField + " GUNCELLEME_YAPAN=@GUNCELLEME_YAPAN where  ( ID=@ID )";
                myCmd.Connection = Con;
                myCmd.ExecuteNonQuery();
            }

        }


        public void _UPDATE_HEADER(SqlConnection Con, DataRow row, int _MUSTERIREF, int _KAMPANYAREF, int _PLANREF)
        {

            if (row["ITEM_NAME"].ToString()!= "")
            {
                string StrField = null;
                SqlCommand myCmd = new SqlCommand();
                string[,] strFieldNameInt = new string[,] { {  "GUI", "GROUP_INDEX","SIRA_INDEX", "ITEM_NAME",  "TARIH","DEGER"} };
                foreach (string strsInt in strFieldNameInt)
                {
                    if (row[strsInt] != DBNull.Value)
                    {
                        if (row[strsInt].ToString() == "NaN") row[strsInt] = 0;

                        myCmd.Parameters.AddWithValue("@" + strsInt.ToString(), row[strsInt]); 
                        StrField = StrField + "[" + row.Table.Columns[strsInt].ColumnName + "] = @" + strsInt.ToString() + ",";
                    }
                }
                myCmd.Parameters.AddWithValue("@ID", row["ID"]);
                //myCmd.Parameters.AddWithValue("@SIRKETREF", _GLOBAL_PARAMETERS._SIRKET_ID);
                //myCmd.Parameters.AddWithValue("@MUSTERIREF", _MUSTERIREF);
                //myCmd.Parameters.AddWithValue("@KAMPANYAREF", _KAMPANYAREF);
                //myCmd.Parameters.AddWithValue("@PLANREF", _PLANREF);
                // (SIRKETREF=@SIRKETREF and MUSTERIREF=@MUSTERIREF and KAMPANYAREF=@KAMPANYAREF and PLANREF=@PLANREF and ID=@ID )
                myCmd.Parameters.AddWithValue("@GUNCELLEME_YAPAN", System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
                foreach (SqlParameter parameter in myCmd.Parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                myCmd.CommandText = "UPDATE  dbo.MDY_PLAN_BUDGET_CLIENT SET " + StrField + " GUNCELLEME_YAPAN=@GUNCELLEME_YAPAN where  ( ID=@ID )";                                                                          
                myCmd.Connection = Con;
                myCmd.ExecuteNonQuery();
            }

        }


        public void _UPDATE_LINE(SqlConnection Con, DataRow row, int _MUSTERIREF, int _KAMPANYAREF, int _PLANREF)
        {
            if (row["ITEM_NAME"].ToString() != "")
            {
                string StrField = null;
                SqlCommand myCmd = new SqlCommand();
                string[,] strFieldNameInt = new string[,] { {  "GUI", "GROUP_INDEX","SIRA_INDEX", "ITEM_NAME",  "TARIH","DEGER"     } };
                foreach (string strsInt in strFieldNameInt)
                {
                    if (row[strsInt] != DBNull.Value)
                    {
                        if (row[strsInt].ToString() == "NaN") row[strsInt] = 0;

                        myCmd.Parameters.AddWithValue("@" + strsInt.ToString(), row[strsInt]);
                        StrField = StrField + "[" + row.Table.Columns[strsInt].ColumnName + "] = @" + strsInt.ToString() + ",";
                    }
                }
                myCmd.Parameters.AddWithValue("@ID", row["ID"]);
                //myCmd.Parameters.AddWithValue("@SIRKETREF", _GLOBAL_PARAMETERS._SIRKET_ID);
                //myCmd.Parameters.AddWithValue("@MUSTERIREF", _MUSTERIREF);
                //myCmd.Parameters.AddWithValue("@KAMPANYAREF", _KAMPANYAREF);
                //myCmd.Parameters.AddWithValue("@PLANREF", _PLANREF);
                // (SIRKETREF=@SIRKETREF and MUSTERIREF=@MUSTERIREF and KAMPANYAREF=@KAMPANYAREF and PLANREF=@PLANREF and ID=@ID )
                myCmd.Parameters.AddWithValue("@GUNCELLEME_YAPAN", System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
                foreach (SqlParameter parameter in myCmd.Parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                myCmd.CommandText = "UPDATE  dbo.MDY_PLAN_BUDGET_CLIENT SET " + StrField + " GUNCELLEME_YAPAN=@GUNCELLEME_YAPAN where  ( ID=@ID )";
                myCmd.Connection = Con;
                myCmd.ExecuteNonQuery();
            }

        }



        public void _UPDATE_CONVERSION(SqlConnection Con, DataRow row, int _MUSTERIREF, int _KAMPANYAREF, int _PLANREF)
        {

            if (row["ITEM_NAME"].ToString() != "")
            {
                string StrField = null;
                SqlCommand myCmd = new SqlCommand();
                string[,] strFieldNameInt = new string[,] { {  "HEDEFKITLE", "OPT_PT", "OCAK","SUBAT","MART","NISAN", "MAYIS", "HAZIRAN", "TEMMUZ"  ,"AGUSTOS","EYLUL", "EKIM", "KASIM", "ARALIK"   } };
                foreach (string strsInt in strFieldNameInt)
                {
                    if (row[strsInt] != DBNull.Value)
                    {
                        if (row[strsInt].ToString() == "NaN") row[strsInt] = 0;

                        myCmd.Parameters.AddWithValue("@" + strsInt.ToString(), row[strsInt]);
                        StrField = StrField + "[" + row.Table.Columns[strsInt].ColumnName + "] = @" + strsInt.ToString() + ",";
                    }
                }
                myCmd.Parameters.AddWithValue("@ID", row["ID"]);
                //myCmd.Parameters.AddWithValue("@SIRKETREF", _GLOBAL_PARAMETERS._SIRKET_ID);
                //myCmd.Parameters.AddWithValue("@MUSTERIREF", _MUSTERIREF);
                //myCmd.Parameters.AddWithValue("@KAMPANYAREF", _KAMPANYAREF);
                //myCmd.Parameters.AddWithValue("@PLANREF", _PLANREF);
                // (SIRKETREF=@SIRKETREF and MUSTERIREF=@MUSTERIREF and KAMPANYAREF=@KAMPANYAREF and PLANREF=@PLANREF and ID=@ID )
                myCmd.Parameters.AddWithValue("@GUNCELLEME_YAPAN", System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
                foreach (SqlParameter parameter in myCmd.Parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                myCmd.CommandText = "UPDATE  dbo.MDY_PLAN_BUDGET_CLIENT_CONVERSION SET " + StrField + " GUNCELLEME_YAPAN=@GUNCELLEME_YAPAN where  ( ID=@ID )";
                myCmd.Connection = Con;
                myCmd.ExecuteNonQuery();
            }

        }

        public void _UPDATE_CPP(SqlConnection Con, DataRow row, int _MUSTERIREF, int _KAMPANYAREF, int _PLANREF)
        {

            if (row["ITEM_NAME"].ToString() != "")
            {
                string StrField = null;
                SqlCommand myCmd = new SqlCommand();
                string[,] strFieldNameInt = new string[,] { {"SPOT_TIPI","ANA_KANAL","MECRA_KODU","OPT_PT","OCAK_GRP","SUBAT_GRP","MART_GRP","NISAN_GRP","MAYIS_GRP","HAZIRAN_GRP","TEMMUZ_GRP","AGUSTOS_GRP","EYLUL_GRP","EKIM_GRP","KASIM_GRP","ARALIK_GRP","OCAK_CPP","SUBAT_CPP","MART_CPP","NISAN_CPP","MAYIS_CPP","HAZIRAN_CPP","TEMMUZ_CPP","AGUSTOS_CPP","EYLUL_CPP","EKIM_CPP","KASIM_CPP","ARALIK_CPP"} };
                foreach (string strsInt in strFieldNameInt)
                {
                    if (row[strsInt] != DBNull.Value)
                    {
                        if (row[strsInt].ToString() == "NaN") row[strsInt] = 0;

                        myCmd.Parameters.AddWithValue("@" + strsInt.ToString(), row[strsInt]);
                        StrField = StrField + "[" + row.Table.Columns[strsInt].ColumnName + "] = @" + strsInt.ToString() + ",";
                    }
                }
                myCmd.Parameters.AddWithValue("@ID", row["ID"]);
                //myCmd.Parameters.AddWithValue("@SIRKETREF", _GLOBAL_PARAMETERS._SIRKET_ID);
                //myCmd.Parameters.AddWithValue("@MUSTERIREF", _MUSTERIREF);
                //myCmd.Parameters.AddWithValue("@KAMPANYAREF", _KAMPANYAREF);
                //myCmd.Parameters.AddWithValue("@PLANREF", _PLANREF);
                // (SIRKETREF=@SIRKETREF and MUSTERIREF=@MUSTERIREF and KAMPANYAREF=@KAMPANYAREF and PLANREF=@PLANREF and ID=@ID )
                myCmd.Parameters.AddWithValue("@GUNCELLEME_YAPAN", System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
                foreach (SqlParameter parameter in myCmd.Parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                myCmd.CommandText = "UPDATE  dbo.MDY_PLAN_BUDGET_CLIENT_CPP SET " + StrField + " GUNCELLEME_YAPAN=@GUNCELLEME_YAPAN where  ( ID=@ID )";
                myCmd.Connection = Con;
                myCmd.ExecuteNonQuery();
            }

        }

        public void _UPDATE_UP(SqlConnection Con, DataRow row, int _MUSTERIREF, int _KAMPANYAREF, int _PLANREF)
        {
            if (row["ITEM_NAME"].ToString() != "")
            {
                string StrField = null;
                SqlCommand myCmd = new SqlCommand();
                string[,] strFieldNameInt = new string[,] { { "SPOT_TIPI","ANA_KANAL","MECRA_KODU","OPT_PT","OCAK_GRP","SUBAT_GRP","MART_GRP","NISAN_GRP","MAYIS_GRP","HAZIRAN_GRP","TEMMUZ_GRP","AGUSTOS_GRP","EYLUL_GRP","EKIM_GRP","KASIM_GRP","ARALIK_GRP","OCAK_CPP","SUBAT_CPP","MART_CPP","NISAN_CPP","MAYIS_CPP","HAZIRAN_CPP","TEMMUZ_CPP","AGUSTOS_CPP","EYLUL_CPP","EKIM_CPP","KASIM_CPP","ARALIK_CPP"} };
                foreach (string strsInt in strFieldNameInt)
                {
                    if (row[strsInt] != DBNull.Value)
                    {
                        if (row[strsInt].ToString() == "NaN") row[strsInt] = 0;

                        myCmd.Parameters.AddWithValue("@" + strsInt.ToString(), row[strsInt]);
                        StrField = StrField + "[" + row.Table.Columns[strsInt].ColumnName + "] = @" + strsInt.ToString() + ",";
                    }
                }
                myCmd.Parameters.AddWithValue("@ID", row["ID"]);
                //myCmd.Parameters.AddWithValue("@SIRKETREF", _GLOBAL_PARAMETERS._SIRKET_ID);
                //myCmd.Parameters.AddWithValue("@MUSTERIREF", _MUSTERIREF);
                //myCmd.Parameters.AddWithValue("@KAMPANYAREF", _KAMPANYAREF);
                //myCmd.Parameters.AddWithValue("@PLANREF", _PLANREF);
                // (SIRKETREF=@SIRKETREF and MUSTERIREF=@MUSTERIREF and KAMPANYAREF=@KAMPANYAREF and PLANREF=@PLANREF and ID=@ID )
                myCmd.Parameters.AddWithValue("@GUNCELLEME_YAPAN", System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
                foreach (SqlParameter parameter in myCmd.Parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                myCmd.CommandText = "UPDATE  dbo.MDY_PLAN_BUDGET_CLIENT_CPP_UP SET " + StrField + " GUNCELLEME_YAPAN=@GUNCELLEME_YAPAN where  ( ID=@ID )";
                myCmd.Connection = Con;
                myCmd.ExecuteNonQuery();
            }

        }

        public void _UPDATE_DOWN(SqlConnection Con, DataRow row, int _MUSTERIREF, int _KAMPANYAREF, int _PLANREF)
        {

            if (row["ITEM_NAME"].ToString() != "")
            {
                string StrField = null;
                SqlCommand myCmd = new SqlCommand();
                string[,] strFieldNameInt = new string[,] { { "SPOT_TIPI","ANA_KANAL","MECRA_KODU","OPT_PT","OCAK_GRP","SUBAT_GRP","MART_GRP","NISAN_GRP","MAYIS_GRP","HAZIRAN_GRP","TEMMUZ_GRP","AGUSTOS_GRP","EYLUL_GRP","EKIM_GRP","KASIM_GRP","ARALIK_GRP","OCAK_CPP","SUBAT_CPP","MART_CPP","NISAN_CPP","MAYIS_CPP","HAZIRAN_CPP","TEMMUZ_CPP","AGUSTOS_CPP","EYLUL_CPP","EKIM_CPP","KASIM_CPP","ARALIK_CPP"} };
                foreach (string strsInt in strFieldNameInt)
                {
                    if (row[strsInt] != DBNull.Value)
                    {
                        if (row[strsInt].ToString() == "NaN") row[strsInt] = 0;

                        myCmd.Parameters.AddWithValue("@" + strsInt.ToString(), row[strsInt]);
                        StrField = StrField + "[" + row.Table.Columns[strsInt].ColumnName + "] = @" + strsInt.ToString() + ",";
                    }
                }
                myCmd.Parameters.AddWithValue("@ID", row["ID"]);
                //myCmd.Parameters.AddWithValue("@SIRKETREF", _GLOBAL_PARAMETERS._SIRKET_ID);
                //myCmd.Parameters.AddWithValue("@MUSTERIREF", _MUSTERIREF);
                //myCmd.Parameters.AddWithValue("@KAMPANYAREF", _KAMPANYAREF);
                //myCmd.Parameters.AddWithValue("@PLANREF", _PLANREF);
                // (SIRKETREF=@SIRKETREF and MUSTERIREF=@MUSTERIREF and KAMPANYAREF=@KAMPANYAREF and PLANREF=@PLANREF and ID=@ID )
                myCmd.Parameters.AddWithValue("@GUNCELLEME_YAPAN", System.Security.Principal.WindowsIdentity.GetCurrent().Name.ToString());
                foreach (SqlParameter parameter in myCmd.Parameters)
                {
                    if (parameter.Value == null)
                    {
                        parameter.Value = DBNull.Value;
                    }
                }
                myCmd.CommandText = "UPDATE  dbo.MDY_PLAN_BUDGET_CLIENT_CPP_DOWN SET " + StrField + " GUNCELLEME_YAPAN=@GUNCELLEME_YAPAN where  ( ID=@ID )";
                myCmd.Connection = Con;
                myCmd.ExecuteNonQuery();
            }

        }
    }
}
