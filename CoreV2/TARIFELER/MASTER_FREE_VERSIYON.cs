using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Data.SqlClient;
using System.Collections;

namespace CoreV2.TARIFELER
{
    public partial class MASTER_FREE_VERSIYON : DevExpress.XtraEditors.XtraForm
    {
        public ArrayList VERSIYON_LIST;
        DataView  DW_LIST = null  ;
        public MASTER_FREE_VERSIYON(string YAYIN_SINFI,string VER_QUERY)
        {

            InitializeComponent();

            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            string SQL = string.Empty;
            if (YAYIN_SINFI == "TELEVIZYON" || YAYIN_SINFI == "RADYO")
            {
                 SQL = string.Format("select VERSIYON from [dbo].[_ADEX_INDEX_DATA] WHERE {0} group by VERSIYON ", VER_QUERY);
            }
            else
            {
                 SQL = string.Format("select REKLAM_SLOGANI from [dbo].[_ADEX_INDEX_DATA] WHERE {0} group by REKLAM_SLOGANI ", VER_QUERY);
            }



            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            SqlDataAdapter da = new SqlDataAdapter(SQL, myConnection);
            DataSet ds = new DataSet();
            da.Fill(ds, "stock");
            DataViewManager dvManager = new DataViewManager(ds);
            DW_LIST = dvManager.CreateDataView(ds.Tables[0]);
            gridControl_FREE_TABLE.DataSource = DW_LIST;
        }

        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void MN_CNT_EKLE_Click(object sender, EventArgs e)
        {
            if (gridView_SABITLER.RowCount > 0)
            {
                  VERSIYON_LIST = new ArrayList();
                 

                int[] GETROW = gridView_SABITLER.GetSelectedRows();
                for (int i = 0; (i <= (GETROW.Length - 1)); i++)
                {
                    VERSIYON_LIST.Add(DW_LIST[GETROW[i]][0].ToString() );
                }
            }
            else { MessageBox.Show("Data Bulunamadı"); }
            gridView_SABITLER.RefreshData();

            Close();
        }

        private void MN_CNT_TUMUNU_SEC_Click(object sender, EventArgs e)
        {

        }
    }
}