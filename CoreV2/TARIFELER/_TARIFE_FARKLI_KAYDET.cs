using System;
using System.Data;
using System.Data.SqlClient;

namespace CoreV2.TARIFELER
{
    public partial class _TARIFE_FARKLI_KAYDET : DevExpress.XtraEditors.XtraForm
    {
        public string _BTN_TYPE = "", _TXT_FILE_NAME = "", _TXT_ACIKLAMA = "";
        public _TARIFE_FARKLI_KAYDET()
        {
            InitializeComponent(); 
          
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent; 
 
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