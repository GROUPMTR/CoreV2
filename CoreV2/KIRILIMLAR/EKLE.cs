using System;
using System.Windows.Forms;

namespace CoreV2.KIRILIMLAR
{
    public partial class EKLE : DevExpress.XtraEditors.XtraForm
    {
        public string _DURUMU,_TEXT,_PATH; 
        Guid  g = Guid.NewGuid();  

        public EKLE()
        {
            InitializeComponent();

            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;

       
            _DURUMU = "CANCEL";
        }

        private void BR_TAMAM_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _DURUMU = "TAMAM";
            _TEXT = txt_KIRILIM.Text.ToUpper().Replace("Ö", "O").Replace("_", "").Replace("-", "").Replace("Ç", "C").Replace("Ü", "U").Replace("İ", "I").Replace("Ğ", "G").Replace("Ş", "S");
            _PATH = "KIRILIM#" + _TEXT; 
            Close();
        }

        private void BR_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            _DURUMU = "CANCEL";
            Close();
        }

        private void txt_KIRILIM_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {
                _TEXT = txt_KIRILIM.Text.ToUpper().Replace("Ö", "O").Replace("_", "").Replace("-", "").Replace("Ç", "C").Replace("Ü", "U").Replace("İ", "I").Replace("Ğ", "G").Replace("Ş", "S");

              
                _DURUMU = "TAMAM";
                _PATH = "KIRILIM#" + _TEXT;  
                Close();
            }
        }
    }
}