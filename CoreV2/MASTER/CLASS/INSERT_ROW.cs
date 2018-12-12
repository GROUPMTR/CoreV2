using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoreV2.MASTER.CLASS
{
   
    public class INSERT_ROW
    {
         
        public void MASTER_ROW_ADD(SqlConnection Con, DataRow row, string ID, string TARIFE_KODU)
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

            myCmd.CommandText = " DELETE [dbo].[__MAS_EDT_" + ID + "_" + TARIFE_KODU + "] (" + StrField + ") VALUES (" + StrParaMeters + ") ";
            myCmd.Connection = Con;
            myCmd.ExecuteNonQuery();
        } 
        public void MASTER_ROW_DELETE(SqlConnection Con, DataRow row, string ID,string TARIFE_KODU)
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

                myCmd.CommandText = "INSERT INTO  [dbo].[__MAS_EDT_" + ID + "_" + TARIFE_KODU + "] (" + StrField + ") VALUES (" + StrParaMeters + ") ";
                myCmd.Connection = Con;
                myCmd.ExecuteNonQuery(); 
        }
         
        public void MASTER_ROW_UPDATE(SqlConnection Con, DataRow row, string ID, string TARIFE_KODU)
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

            myCmd.CommandText = "UPDATE  [dbo].[__MAS_EDT_" + ID + "_" + TARIFE_KODU + "] (" + StrField + ") VALUES (" + StrParaMeters + ") ";
            myCmd.Connection = Con;
            myCmd.ExecuteNonQuery();
        } 
    }
}
