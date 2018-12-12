using System.Windows.Forms;

namespace CoreV2.RAPORLA
{
    public partial class GENEL_RAPORLAMA : DevExpress.XtraEditors.XtraForm
    {
        public GENEL_RAPORLAMA()
        {
            InitializeComponent();
        }

        private void btn_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void btn_EXPORT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           

            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = ".xlsx (*.xlsx)|*.xlsx";
            sfd.FileName = "CORE_RAPOR.xlsx";
            DialogResult res = sfd.ShowDialog();
            if (res == DialogResult.OK)
            {
                gridViews.ExportToXlsx(sfd.FileName);

            }
        }
    }
}