using System.Windows.Forms;

namespace CoreV2.TEXT
{
    public partial class DEGISTIR : DevExpress.XtraEditors.XtraForm
    {
        public string _DURUMU, _TEXT_OLD,_TEXT_NEW;
        public DEGISTIR()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            _DURUMU = "CANCEL";
        }

        private void BR_TAMAM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DialogResult c 
             = MessageBox.Show("Kodu değiştirmek istediğinize eminmisiniz?", "Uyarı",
                MessageBoxButtons.YesNo, MessageBoxIcon.Information);
            if (c == DialogResult.Yes)
            {

                //using (SqlConnection myConnections = new SqlConnection(GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                //{
                //    myConnections.Open();
                //    using (SqlCommand myCommands = new SqlCommand())
                //    {
                //        string myInsertQuery = "UPDATE dbo.ADM_KIRILIMLI_RAPOR SET  TEXT=@NEWKIRILIM_KODU WHERE (SIRKET_KODU=@FIRMA) AND (TEXT=@OLDKIRILIM_KODU) AND (RAPOR_KODU=@RAPOR_KODU);" +
                //                               "UPDATE dbo.ADM_KIRILIMLI_RAPOR_FILITRESI SET  KIRILIM_KODU=@NEWKIRILIM_KODU WHERE (SIRKET_KODU=@FIRMA) AND (KIRILIM_KODU=@OLDKIRILIM_KODU) AND (RAPOR_KODU=@RAPOR_KODU)";
                //        myCommands.Parameters.Add("@FIRMA", SqlDbType.NVarChar); myCommands.Parameters["@FIRMA"].Value = GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                //        myCommands.Parameters.Add("@OLDKIRILIM_KODU", SqlDbType.NVarChar); myCommands.Parameters["@OLDKIRILIM_KODU"].Value = txt_KIRILIM_ESKI_KOD.Text;
                //        myCommands.Parameters.Add("@NEWKIRILIM_KODU", SqlDbType.NVarChar); myCommands.Parameters["@NEWKIRILIM_KODU"].Value = txt_KIRILIM_YENI_KOD.Text;
                //        myCommands.Parameters.Add("@RAPOR_KODU", SqlDbType.NVarChar); myCommands.Parameters["@RAPOR_KODU"].Value = BR_RAPOR_KODU.Caption;
                //        myCommands.CommandText = myInsertQuery;
                //        myCommands.Connection = myConnections;
                //        myCommands.ExecuteNonQuery();
                //    }
                //}


                _DURUMU = "TAMAM";
                _TEXT_NEW = txt_KIRILIM_YENI_KOD.Text;
                Close();

                
            }
        }

        private void BR_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _DURUMU = "CANCEL";
            Close();
        }
    }
}