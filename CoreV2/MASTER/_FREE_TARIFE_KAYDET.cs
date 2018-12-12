using System.Windows.Forms;

namespace CoreV2.MASTER
{
    public partial class _FREE_TARIFE_KAYDET : Form
    {
        public string _BTN_TYPE = "", _TXT_FILE_NAME = "";
        public _FREE_TARIFE_KAYDET()
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
            _BTN_TYPE = "Tamam";
            _TXT_FILE_NAME = TXT_FILE_NAME.Text;
            Close();
        }
    }
}
