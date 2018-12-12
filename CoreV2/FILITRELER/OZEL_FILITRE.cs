using System;
using System.Data;
using System.Windows.Forms;

namespace CoreV2.FILITRELER
{
    public partial class OZEL_FILITRE : DevExpress.XtraEditors.XtraForm
    {
        public string SATIRLAR_NEREYE;
        public DataView dvSELECT;
        public string _DURUMU = "";
        public string _TEXT = "";
        public string _DAHIL_HARIC = "";
        public string _OZEL_GENEL = ""; 
        public OZEL_FILITRE()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            _DAHIL_HARIC = " (Dahil)";
            _OZEL_GENEL = "OZEL"; 
        }

        private void BTN_TAMAM_Click(object sender, EventArgs e)
        {
            if (RD_SONUNDA.Checked) _TEXT = TXT_METIN.Text +"%";
            if (RD_ICINDE.Checked) _TEXT = "%" + TXT_METIN.Text + "%";
            if (RD_BASINDA.Checked) _TEXT = "%"+ TXT_METIN.Text;  
            _DURUMU = "TAMAM";
            Close();
        }

        private void BTN_VAZGEC_Click(object sender, EventArgs e)
        {
            Close();
        }
 

        private void TXT_METIN_KeyPress(object sender, KeyPressEventArgs e)
        {   e.KeyChar = Char.ToUpper(e.KeyChar); 
        }

        private void TXT_METIN_KeyDown(object sender, KeyEventArgs e)
        {
    
          
        }

        private void TOOGLE_DAHIL_HARIC_Toggled(object sender, EventArgs e)
        { 
            if (TOOGLE_DAHIL_HARIC.IsOn) _DAHIL_HARIC = " (Dahil)"; else _DAHIL_HARIC = " (Hariç)";

        }

        private void TOOGLE_OZEL_GENEL_FILITRE_Toggled(object sender, EventArgs e)
        {
            if (TOOGLE_OZEL_GENEL_FILITRE.IsOn) _OZEL_GENEL = "GENEL"; else _OZEL_GENEL = "OZEL";
        }
    }
}