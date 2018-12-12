using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreV2.MASTER.CLASS
{ 
    public class DELETE_ROW
    {
        public void _DELETE_HEADER(SqlConnection Con, DataRow row, int MUSTERIREF, int KAMPANYAREF, int PLANREF)
        {
            if (row != null)
            {
                if (row["ID"] != DBNull.Value)
                {
                    string myInsertQuery = " DELETE  dbo.MDY_PLAN_BUDGET_CLIENT WHERE (SIRKET_KODU=@SIRKET_KODU) and (MUSTERI_KODU=@MUSTERI_KODU) and (PLAN_KODU=@PLAN_KODU) and (ID=@ID)";
                    SqlCommand myCmd = new SqlCommand(myInsertQuery);
                    myCmd.Parameters.AddWithValue("@SIRKET_KODU", _GLOBAL_PARAMETRELER._SIRKET_KODU);
                    myCmd.Parameters.AddWithValue("@MUSTERI_KODU", MUSTERIREF);
                    myCmd.Parameters.AddWithValue("@PLAN_KODU", PLANREF);
                    myCmd.Parameters.AddWithValue("@ID", row["ID"].ToString());
                    myCmd.Connection = Con; 
                    myCmd.ExecuteNonQuery(); 
                }
            }
        }

        public void _DELETE_LINE(SqlConnection Con, DataRow row, int MUSTERIREF, int KAMPANYAREF, int PLANREF)
        {
            if (row != null)
            {
                if (row["ID"] != DBNull.Value)
                {
                    string myInsertQuery = " DELETE  dbo.MDY_PLAN_BUDGET_CLIENT WHERE (SIRKET_KODU=@SIRKET_KODU) and (MUSTERIREF=@MUSTERIREF) and (PLANREF=@PLANREF) and (ID=@ID)";
                    SqlCommand myCmd = new SqlCommand(myInsertQuery);
                    myCmd.Parameters.AddWithValue("@SIRKET_KODU", _GLOBAL_PARAMETRELER._SIRKET_KODU); 
                    myCmd.Parameters.AddWithValue("@MUSTERIREF", MUSTERIREF);
                    myCmd.Parameters.AddWithValue("@PLANREF", PLANREF);
                    myCmd.Parameters.AddWithValue("@ID", row["ID"].ToString());
                    myCmd.Connection = Con;
                    myCmd.ExecuteNonQuery();
                }
            }
        }






        public void _DELETE_CONVERSION(SqlConnection Con, DataRow row, int MUSTERIREF, int KAMPANYAREF, int PLANREF)
        {
            if (row != null)
            {
                if (row["ID"] != DBNull.Value)
                {
                    string myInsertQuery = " DELETE  dbo.MDY_PLAN_BUDGET_CLIENT_CONVERSION WHERE (SIRKET_KODU=@SIRKET_KODU) and (MUSTERIREF=@MUSTERIREF) and (PLANREF=@PLANREF) and (ID=@ID)";
                    SqlCommand myCmd = new SqlCommand(myInsertQuery);
                    myCmd.Parameters.AddWithValue("@SIRKET_KODU", _GLOBAL_PARAMETRELER._SIRKET_KODU); 
                    myCmd.Parameters.AddWithValue("@MUSTERIREF", MUSTERIREF);
                    myCmd.Parameters.AddWithValue("@PLANREF", PLANREF);
                    myCmd.Parameters.AddWithValue("@ID", row["ID"].ToString());
                    myCmd.Connection = Con;
                    myCmd.ExecuteNonQuery();
                }
            }
        }

        public void _DELETE_CPP(SqlConnection Con, DataRow row, int MUSTERIREF, int KAMPANYAREF, int PLANREF)
        {
            if (row != null)
            {
                if (row["ID"] != DBNull.Value)
                {
                    string myInsertQuery = " DELETE  dbo.MDY_PLAN_BUDGET_CLIENT_CPP WHERE (SIRKET_KODU=@SIRKET_KODU) and (MUSTERIREF=@MUSTERIREF) and (PLANREF=@PLANREF) and (ID=@ID)";
                    SqlCommand myCmd = new SqlCommand(myInsertQuery);
                    myCmd.Parameters.AddWithValue("@SIRKET_KODU", _GLOBAL_PARAMETRELER._SIRKET_KODU); 
                    myCmd.Parameters.AddWithValue("@MUSTERIREF", MUSTERIREF);
                    myCmd.Parameters.AddWithValue("@PLANREF", PLANREF);
                    myCmd.Parameters.AddWithValue("@ID", row["ID"].ToString());
                    myCmd.Connection = Con;
                    myCmd.ExecuteNonQuery();
                }
            }
        }

        public void _DELETE_UP(SqlConnection Con, DataRow row, int MUSTERIREF, int KAMPANYAREF, int PLANREF)
        {
            if (row != null)
            {
                if (row["ID"] != DBNull.Value)
                {
                    string myInsertQuery = " DELETE  dbo.MDY_PLAN_BUDGET_CLIENT_CPP_UP WHERE (SIRKET_KODU=@SIRKET_KODU) and (MUSTERIREF=@MUSTERIREF) and (PLANREF=@PLANREF) and (ID=@ID)";
                    SqlCommand myCmd = new SqlCommand(myInsertQuery);
                    myCmd.Parameters.AddWithValue("@SIRKET_KODU", _GLOBAL_PARAMETRELER._SIRKET_KODU); 
                    myCmd.Parameters.AddWithValue("@MUSTERIREF", MUSTERIREF);
                    myCmd.Parameters.AddWithValue("@PLANREF", PLANREF);
                    myCmd.Parameters.AddWithValue("@ID", row["ID"].ToString());
                    myCmd.Connection = Con;
                    myCmd.ExecuteNonQuery();
                }
            }
        }

        public void _DELETE_DOWN(SqlConnection Con, DataRow row, int MUSTERIREF, int KAMPANYAREF, int PLANREF)
        {
            if (row != null)
            {
                if (row["ID"] != DBNull.Value)
                {
                    string myInsertQuery = " DELETE  dbo.MDY_PLAN_BUDGET_CLIENT_CPP_DOWN WHERE (SIRKET_KODU=@SIRKET_KODU) and (MUSTERIREF=@MUSTERIREF) and (PLANREF=@PLANREF) and (ID=@ID)";
                    SqlCommand myCmd = new SqlCommand(myInsertQuery);
                    myCmd.Parameters.AddWithValue("@SIRKET_KODU", _GLOBAL_PARAMETRELER._SIRKET_KODU); 
                    myCmd.Parameters.AddWithValue("@MUSTERIREF", MUSTERIREF);
                    myCmd.Parameters.AddWithValue("@PLANREF", PLANREF);
                    myCmd.Parameters.AddWithValue("@ID", row["ID"].ToString());
                    myCmd.Connection = Con;
                    myCmd.ExecuteNonQuery();
                }
            }
        }
    }
}
