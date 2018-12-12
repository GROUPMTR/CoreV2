using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Globalization;
using System.Data.SqlClient;

namespace CoreV2
{
    public partial class ONBESDKGRP : XtraForm
    {
        string Date_One, Date_Two;
        DataView dvAcik;
        DataSet MyDataSet = null;
        public ONBESDKGRP()
        {
            InitializeComponent();

            recomBox_OptPt.Items.Clear();
            recomBox_Mecrlar.Items.Clear();

            DateTime dtOne = Convert.ToDateTime(DateTime.Now.AddDays(-5));//DateTime.Now;
            dtTmPckrBasTar.EditValue = dtOne.ToString("yyyy-MM-dd 00:00:00", DateTimeFormatInfo.InvariantInfo);
            DateTime dtTwo = Convert.ToDateTime(DateTime.Now);//DateTime.Now;
            dtTmPckrBitTar.EditValue = dtTwo.ToString("yyyy-MM-dd 00:00:00", DateTimeFormatInfo.InvariantInfo);
            Mecralar();
        }

        void Mecralar()
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            string myInsertQuery = "SELECT CHANNEL FROM dbo.DATA_TELEVIZYON_ONBESDKGRP GROUP BY CHANNEL ";
            SqlCommand myCommand = new SqlCommand(myInsertQuery, myConnection) { CommandText = myInsertQuery.ToString() };
            myConnection.Open();
            SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
            recomBox_Mecrlar.Items.Add("Tümü");
            while (myReader.Read())
            {
                //recomBox_Mecrlar.Items.Add(myReader["MECRA"].ToString());
                reChk_MecraKodu.Items.Add(myReader["CHANNEL"].ToString());
            }
            recomBox_OptPt.Items.Add("Tümü");
            recomBox_OptPt.Items.Add("OPT");
            recomBox_OptPt.Items.Add("PT");
        }
        void DATAFILL(string Mecra, string OptPt)
        {

            DateTime dtOne = Convert.ToDateTime(dtTmPckrBasTar.EditValue);//DateTime.Now;
            Date_One = dtOne.ToString("yyyy-MM-dd 00:00:00", DateTimeFormatInfo.InvariantInfo);
            DateTime dtTwo = Convert.ToDateTime(dtTmPckrBitTar.EditValue);//DateTime.Now;
            Date_Two = dtTwo.ToString("yyyy-MM-dd 00:00:00", DateTimeFormatInfo.InvariantInfo);
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            string SQL = null;
            if (Mecra.ToString() != "Tümü")
            {
                if (comBox_OptPt.EditValue.ToString() == "Tümü")
                {
                    //SQL = " SELECT  DATEPART(mm, DATE_) as MM, DATE_,CHANNEL,TIME_, (AMR_TOTAL_INDIVIDUALS*100) as AMR_TOTAL_INDIVIDUALS , (AMR_MALES_20_PLUS*100) as AMR_MALES_20_PLUS, (AMR_FEMALES_20_PLUS*100) as AMR_FEMALES_20_PLUS, (AMR_AB*100) as AMR_AB, (AMR_20_PLUS_ABC1*100) as AMR_20_PLUS_ABC1   ,(AMR_5_11*100) as AMR_5_11 , (SHR_TOTAL_INDIVIDUALS*100) as SHR_TOTAL_INDIVIDUALS, (SHR_MALES_20_PLUS*100) as SHR_MALES_20_PLUS, (SHR_FEMALES_20_PLUS*100) as SHR_FEMALES_20_PLUS, (SHR_AB*100) as SHR_AB, (SHR_20_PLUS_ABC1*100) as SHR_20_PLUS_ABC1, OPT_PT, DAYPART FROM  dbo.MINUTE_DATA  Where  (DATE_ >= CONVERT(DATETIME, '" + Date_One + "', 102)) AND (DATE_ <= CONVERT(DATETIME, '" + Date_Two + "', 102)) ";

                    SQL = " SELECT  DATEPART(mm, DATE_) as MM, DATE_,CHANNEL,TIME_, (TI) as TOTAL_INDIVIDUALS , (M20p) as M_20_PLUS, (F20p) as F_20_PLUS, (AAB) as AB, (A20ABC1) as [20_PLUS_ABC1]   ,(A5_11) as [5_11] , OPT_PT, DAYPART FROM  dbo.DATA_TELEVIZYON_ONBESDKGRP  Where  (DATE_ >= CONVERT(DATETIME, '" + Date_One + "', 102)) AND (DATE_ <= CONVERT(DATETIME, '" + Date_Two + "', 102)) ";

                }
                else
                {
                    //SQL = " SELECT DATEPART(mm, DATE_) as MM, DATE_,CHANNEL,TIME_,  (AMR_TOTAL_INDIVIDUALS*100) as AMR_TOTAL_INDIVIDUALS , (AMR_MALES_20_PLUS*100) as AMR_MALES_20_PLUS, (AMR_FEMALES_20_PLUS*100) as AMR_FEMALES_20_PLUS, (AMR_AB*100) as AMR_AB, (AMR_20_PLUS_ABC1*100) as AMR_20_PLUS_ABC1   ,(AMR_5_11*100) as AMR_5_11 ,  (SHR_TOTAL_INDIVIDUALS*100) as SHR_TOTAL_INDIVIDUALS, (SHR_MALES_20_PLUS*100) as SHR_MALES_20_PLUS, (SHR_FEMALES_20_PLUS*100) as SHR_FEMALES_20_PLUS, (SHR_AB*100) as SHR_AB, (SHR_20_PLUS_ABC1*100) as SHR_20_PLUS_ABC1, OPT_PT, DAYPART FROM  dbo.MINUTE_DATA  Where  OPT_PT = '" + comBox_OptPt.EditValue.ToString() + "' and (DATE_ >= CONVERT(DATETIME, '" + Date_One + "', 102)) AND (DATE_ <= CONVERT(DATETIME, '" + Date_Two + "', 102))  ";
                    SQL = " SELECT DATEPART(mm, DATE_) as MM, DATE_,CHANNEL,TIME_,  (TI) as TOTAL_INDIVIDUALS , (M20p) as M_20_PLUS, (F20p) as F_20_PLUS, (AAB) as AB, (A20ABC1) as [20_PLUS_ABC1]   ,(A5_11) as [5_11] ,    OPT_PT, DAYPART FROM  dbo.DATA_TELEVIZYON_ONBESDKGRP  Where  OPT_PT = '" + comBox_OptPt.EditValue.ToString() + "' and (DATE_ >= CONVERT(DATETIME, '" + Date_One + "', 102)) AND (DATE_ <= CONVERT(DATETIME, '" + Date_Two + "', 102))  ";
                }
            }
            else
            {
                if (comBox_OptPt.EditValue.ToString() == "Tümü")
                {
                    //SQL = " SELECT DATEPART(mm, DATE_) as MM, DATE_,CHANNEL,TIME_, (AMR_TOTAL_INDIVIDUALS*100) as AMR_TOTAL_INDIVIDUALS , (AMR_MALES_20_PLUS*100) as AMR_MALES_20_PLUS, (AMR_FEMALES_20_PLUS*100) as AMR_FEMALES_20_PLUS, (AMR_AB*100) as AMR_AB, (AMR_20_PLUS_ABC1*100) as AMR_20_PLUS_ABC1 ,(AMR_5_11*100) as AMR_5_11 ,  (SHR_TOTAL_INDIVIDUALS*100) as SHR_TOTAL_INDIVIDUALS, (SHR_MALES_20_PLUS*100) as SHR_MALES_20_PLUS, (SHR_FEMALES_20_PLUS*100) as SHR_FEMALES_20_PLUS, (SHR_AB*100) as SHR_AB, (SHR_20_PLUS_ABC1*100) as SHR_20_PLUS_ABC1, OPT_PT, DAYPART FROM  dbo.MINUTE_DATA  Where  (DATE_ >= CONVERT(DATETIME, '" + Date_One + "', 102)) AND (DATE_ <= CONVERT(DATETIME, '" + Date_Two + "', 102))  ";
                    SQL = " SELECT DATEPART(mm, DATE_) as MM, DATE_,CHANNEL,TIME_, (TI) as TOTAL_INDIVIDUALS , (M20p) as M_20_PLUS, (F20p) as F_20_PLUS, (AAB) as AB, (A20ABC1) as [20_PLUS_ABC1] ,(A5_11) as [5_11] ,    OPT_PT, DAYPART FROM  dbo.DATA_TELEVIZYON_ONBESDKGRP  Where  (DATE_ >= CONVERT(DATETIME, '" + Date_One + "', 102)) AND (DATE_ <= CONVERT(DATETIME, '" + Date_Two + "', 102))  ";
                }
                else
                {
                    //SQL = " SELECT DATEPART(mm, DATE_) as MM, DATE_,CHANNEL,TIME_, (AMR_TOTAL_INDIVIDUALS*100) as AMR_TOTAL_INDIVIDUALS , (AMR_MALES_20_PLUS*100) as AMR_MALES_20_PLUS, (AMR_FEMALES_20_PLUS*100) as AMR_FEMALES_20_PLUS, (AMR_AB*100) as AMR_AB, (AMR_20_PLUS_ABC1*100) as AMR_20_PLUS_ABC1,(AMR_5_11*100) as AMR_5_11 , (SHR_TOTAL_INDIVIDUALS*100) as SHR_TOTAL_INDIVIDUALS, (SHR_MALES_20_PLUS*100) as SHR_MALES_20_PLUS, (SHR_FEMALES_20_PLUS*100) as SHR_FEMALES_20_PLUS, (SHR_AB*100) as SHR_AB, (SHR_20_PLUS_ABC1*100) as SHR_20_PLUS_ABC1, OPT_PT, DAYPART FROM  dbo.MINUTE_DATA  Where  OPT_PT = '" + comBox_OptPt.EditValue.ToString() + "' and (DATE_ >= CONVERT(DATETIME, '" + Date_One + "', 102)) AND (DATE_ <= CONVERT(DATETIME, '" + Date_Two + "', 102))  ";
                    SQL = " SELECT DATEPART(mm, DATE_) as MM, DATE_,CHANNEL,TIME_, (TI) as TOTAL_INDIVIDUALS , (M20p) as M_20_PLUS, (F20p) as F_20_PLUS, (AAB) as AB, (A20ABC1) as [20_PLUS_ABC1],(A5_11) as [5_11] ,   OPT_PT, DAYPART FROM  dbo.DATA_TELEVIZYON_ONBESDKGRP  Where  OPT_PT = '" + comBox_OptPt.EditValue.ToString() + "' and (DATE_ >= CONVERT(DATETIME, '" + Date_One + "', 102)) AND (DATE_ <= CONVERT(DATETIME, '" + Date_Two + "', 102))  ";
                }
            }

            string MecraKodu = "";
            for (int i = 0; i <= reChk_MecraKodu.Items.Count - 1; i++)
                if (reChk_MecraKodu.Items[i].CheckState.ToString() == "Checked") MecraKodu += " Channel = N'" + reChk_MecraKodu.Items[i].Value.ToString() + "'  OR ";



            if (MecraKodu != "")
            {
                MecraKodu = "  and  (" + MecraKodu.Substring(0, MecraKodu.Length - 4).ToString() + ")  Order By DATE_";

                SQL += MecraKodu;
            }


            SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter(SQL, myConnection);
            MyDataSet = new DataSet();
            MySqlDataAdapter.Fill(MyDataSet, "dbo_Talepler");
            DataViewManager dvManager = new DataViewManager(MyDataSet);
            dvAcik = dvManager.CreateDataView(MyDataSet.Tables[0]);
            grd_List.DataSource = dvAcik;
            // pivotGridControls.DataSource = dvAcik;
            //chartControl1.DataSource = MyDataSet.Tables[0];
        }
        private string ShowSaveFileDialog(string title, string filter)
        {
            using (SaveFileDialog dlg = new SaveFileDialog())
            {
                string name = Application.ProductName;
                int n = name.LastIndexOf(".") + 1;
                if (n > 0)
                    name = name.Substring(n, name.Length - n);
                dlg.Title = String.Format("Export To {0}", title);
                dlg.FileName = name;
                dlg.Filter = filter;
                if (dlg.ShowDialog() == DialogResult.OK)
                    return dlg.FileName;
            }
            return "";
        }
        private void br_Excel_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            string fileName = ShowSaveFileDialog("Microsoft Excel Document", "Microsoft Excel|*.xlsx");
            if (fileName != "")
            {
                grdView_List.ExportToXlsx(fileName);
                OpenFile(fileName);
            }
        }
        private void OpenFile(string fileName)
        {
            if (XtraMessageBox.Show("Do you want to open this file?", "Export To...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    using (System.Diagnostics.Process process = new System.Diagnostics.Process())
                    {
                        process.StartInfo.FileName = fileName;
                        process.StartInfo.Verb = "Open";
                        process.StartInfo.WindowStyle = System.Diagnostics.ProcessWindowStyle.Normal;
                        process.Start();
                    }
                }
                catch
                {
                    DevExpress.XtraEditors.XtraMessageBox.Show(this, "Cannot find an application on your system suitable for openning the file with exported data.", Application.ProductName, MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void br_Listele_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DATAFILL(comBox_Mecrlar.EditValue.ToString(), comBox_OptPt.EditValue.ToString());
        }

        private void br_Kapat_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }
    }
}