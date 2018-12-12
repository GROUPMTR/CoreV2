using System;
using System.Windows.Forms;

namespace CoreV2.TARIFELER
{
    public partial class MASTER_KONTROL : DevExpress.XtraEditors.XtraForm
    {
        public string _DURUMU = "";
        public MASTER_KONTROL()
        {
            InitializeComponent();
            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.CenterParent;  

        }

        private void BTN_TAMAM_Click(object sender, EventArgs e)
        {
            _DURUMU = "TAMAM";
            Close();
        }

        private void BTN_VAZGEC_Click(object sender, EventArgs e)
        {
            _DURUMU = "VAZGEC";
            Close();
        }
    }
}