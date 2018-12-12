using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreV2.TARIFELER
{
    public partial class _TARIFE_PAYLAS : Form
    {
        public string _BTN_TYPE = "", _TXT_FILE_NAME = "", _TXT_ACIKLAMA = "";
        public _TARIFE_PAYLAS()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

            
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlCommand myCommand = new SqlCommand("  select *  from  ADM_FIRMALAR    ", myConnection);
            myConnection.Open();
            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            while (myReader.Read())
            {
                CMB_FRK_FIRMA.Items.Add(myReader["FIRMA"].ToString());
            }
        }

        private void CMB_FRK_FIRMA_SelectedIndexChanged(object sender, EventArgs e)
        {
            CMB_FRK_KULLANICI.Items.Clear();
            CMB_FRK_KULLANICI.Text = "";
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            string SQL = string.Format("  select *  from  ADM_KULLANICILAR   where FIRMA='{0}'", CMB_FRK_FIRMA.Text);
            SqlCommand myCommand = new SqlCommand(SQL, myConnection);
            myConnection.Open();
            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            while (myReader.Read())
            {
                CMB_FRK_KULLANICI.Items.Add(myReader["EMAIL"].ToString());
            }
        }

        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _BTN_TYPE = "Close";
            Close();
        }



        private void BTN_TAMAM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (TXT_FILE_NAME.Text.Length > 0)
            {
                _BTN_TYPE = "Tamam";
                _TXT_FILE_NAME = TXT_FILE_NAME.Text;
                _TXT_ACIKLAMA = txt_RAPOR_ACIKLAMASI.Text;
                Close();

            }
        }


    }
}