using  System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Data.SqlClient;

namespace CoreV2.ADMIN
{
    public partial class KULLANICI_TANIMLA : DevExpress.XtraEditors.XtraForm
    {
        public KULLANICI_TANIMLA()
        {
            InitializeComponent();

            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            txt_MAIL.Text = Environment.UserName.ToString().ToLower();
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            string SQL = "  select *  from  ADM_FIRMALAR    ";
            SqlCommand myCommand = new SqlCommand(SQL, myConnection);
            myCommand.CommandText = SQL.ToString();
            myConnection.Open();
            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            while (myReader.Read())
            {
                CMB_FIRMA.Items.Add(myReader["FIRMA"].ToString());
                _GLOBAL_PARAMETRELER._KULLANICI_MAIL = myReader["EMAIL"].ToString();
            }
        }

        private void BTN_TAMAM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CMB_FIRMA.Text != "" && txt_MAIL.Text != "")
            {
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                { 
                    SqlCommand myCommand = new SqlCommand("INSERT INTO dbo.ADM_KULLANICILAR  (FIRMA,EMAIL) VALUES (@FIRMA,@EMAIL)");
                    myCommand.Parameters.AddWithValue("@FIRMA", CMB_FIRMA.Text);
                    myCommand.Parameters.AddWithValue("@EMAIL", txt_MAIL.Text);
                    myCommand.Connection = conn;
                    conn.Open();
                    myCommand.ExecuteNonQuery();
                    myCommand.Connection.Close();
                }
                Close();
            }
        }

        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}