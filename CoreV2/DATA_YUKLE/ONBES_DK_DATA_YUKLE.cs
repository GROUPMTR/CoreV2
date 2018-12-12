using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace CoreV2.DATA_YUKLE
{
    public partial class ONBES_DK_DATA_YUKLE : DevExpress.XtraEditors.XtraForm
    {
        DataSet ws;
        public ONBES_DK_DATA_YUKLE()
        {
            InitializeComponent();
        }

        private void re_FILE_SELECT_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            OpenFileDialog file = new OpenFileDialog();
            if (file.ShowDialog() == DialogResult.OK)
            {
                BR_FILE_SELECT.EditValue = file.FileName;

                spreadsheetControls.LoadDocument(BR_FILE_SELECT.EditValue.ToString());
                
                //Infragistics.Documents.Excel.Workbook workbook = Infragistics.Documents.Excel.Workbook.Load(BR_FILE_SELECT.EditValue.ToString());//, Infragistics.Documents.Excel.WorkbookFormat.Excel2007);
                ws = new DataSet();


            
                for (int i = 0; i <= spreadsheetControls.Document.Worksheets.Count- 1; i++)
                {
                    ws.Tables.Add(spreadsheetControls.Document.Worksheets[i].Name.Replace("Periods(", "").Replace(")", ""));
                    int m_row = 5;
                    for (int rows = 0; rows < 10; rows++)
                    {
                        if (spreadsheetControls.Document.Worksheets[i].Rows[rows][0].Value != null)
                        {
                            if (spreadsheetControls.Document.Worksheets[i].Rows[rows][0].Value.ToString() == "Timebands") m_row = rows;
                        }
                    }
                    for (int col = 0; col < 7; col++)
                    {
                        if (spreadsheetControls.Document.Worksheets[i].Rows[m_row][col].Value != null)
                        {
                            string header = spreadsheetControls.Document.Worksheets[i].Rows[m_row][col].Value.ToString();
                            ws.Tables[spreadsheetControls.Document.Worksheets[i].Name.Replace("Periods(", "").Replace(")", "")].Columns.Add(col.ToString() + "|" + header, typeof(string));
                        }
                    }

                    DataRow dr;
                    for (int xi = 0; xi < 110; xi++)
                    {
                        m_row++;
                        dr = ws.Tables[spreadsheetControls.Document.Worksheets[i].Name.Replace("Periods(", "").Replace(")", "")].NewRow();

                        for (int col = 0; col < 7; col++)
                        {
                            if (spreadsheetControls.Document.Worksheets[i].Rows[m_row][col].Value != null)
                            {
                                dr[col] = spreadsheetControls.Document.Worksheets[i].Rows[m_row][col].Value.ToString();
                            }
                        }
                        ws.Tables[spreadsheetControls.Document.Worksheets[i].Name.Replace("Periods(", "").Replace(")", "")].Rows.Add(dr);
                    }

                    DevExpress.XtraGrid.GridControl grdCntrl_ = new DevExpress.XtraGrid.GridControl();
                    DevExpress.XtraGrid.Views.Grid.GridView gridView_ = new DevExpress.XtraGrid.Views.Grid.GridView();
                    //DevExpress.XtraTab.XtraTabPage xtraTabPage_ = new DevExpress.XtraTab.XtraTabPage();

                    grdCntrl_.DataSource = ws.Tables[i]; ;
                    xtraTabControls.TabPages.Add();
                    xtraTabControls.TabPages[xtraTabControls.TabPages.Count - 1].Controls.Add(grdCntrl_);
                    xtraTabControls.TabPages[xtraTabControls.TabPages.Count - 1].Name = spreadsheetControls.Document.Worksheets[i].Name.Replace("Periods(", "").Replace(")", "");
                    xtraTabControls.TabPages[xtraTabControls.TabPages.Count - 1].Text = spreadsheetControls.Document.Worksheets[i].Name.Replace("Periods(", "").Replace(")", "");

                    //xtraTabPage_LISTRAPOR.Controls.Add(grdCntrl_);
                    //xtraTabPage_LISTRAPOR.Name = "dddd";
                    //xtraTabPage_LISTRAPOR.Size = new System.Drawing.Size(699, 368);
                    //xtraTabPage_LISTRAPOR.Text = "List Rapor";
                    // 
                    // grdCntrl_List
                    // 
                    grdCntrl_.Dock = System.Windows.Forms.DockStyle.Fill;
                    grdCntrl_.Location = new System.Drawing.Point(0, 0);
                    grdCntrl_.MainView = gridView_;
                    //grdCntrl_.MenuManager = this.barManagers;
                    grdCntrl_.Name = "grdCntrl_List";
                    grdCntrl_.Size = new System.Drawing.Size(699, 368);
                    grdCntrl_.TabIndex = 1;
                    grdCntrl_.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] { gridView_ });
                    // 
                    // gridView_List
                    // 
                    gridView_.ColumnPanelRowHeight = 40;
                    gridView_.GridControl = grdCntrl_;
                    gridView_.Name = "gridView_";
                    gridView_.OptionsView.ColumnAutoWidth = false;
                    gridView_.OptionsView.ShowGroupPanel = false;
                }
            }
        }

        private void BR_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BR_YUKLE_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

            for (int i = 0; i <= xtraTabControls.TabPages.Count - 1; i++)
            {
                DevExpress.XtraGrid.GridControl grid = new DevExpress.XtraGrid.GridControl();
                DevExpress.XtraGrid.Views.Grid.GridView GridView = new DevExpress.XtraGrid.Views.Grid.GridView();
                //  DevExpress.XtraTab.XtraTabControl xtraTabControls = xtraTabControls;


                DevExpress.XtraTab.XtraTabPage pg = xtraTabControls.TabPages[i];

                // Get all the controls here
                Control.ControlCollection col = pg.Controls;

                // should have only one dgv
                foreach (Control myControl in col)
                {

                    grid = (DevExpress.XtraGrid.GridControl)myControl;
                    GridView = (DevExpress.XtraGrid.Views.Grid.GridView)grid.DefaultView;

                    if (GridView.RowCount > 0)
                    {
                        DateTime DT = Convert.ToDateTime(BR_DATE.EditValue.ToString());
                        SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                        myConnection.Open();
                        for (int iX = 0; iX <= GridView.RowCount - 1; iX++)
                        {
                            DataRow dw = GridView.GetDataRow(iX);

                            if (dw[0].ToString() != "")
                            {

                                if (dw[0].ToString() == "Total Day") break;
                                SqlCommand myCommand = new SqlCommand();
                                myCommand.CommandText = "INSERT INTO dbo.DATA_TELEVIZYON_ONBESDKGRP ( DATE_, CHANNEL, TIME_, TI, M20p, F20p, AAB, A20ABC1, A5_11,START_TIME,END_TIME) " +
                                " Values(@DATE_, @CHANNEL, @TIME_, @TI, @M20p, @F20p, @AAB, @A20ABC1,@A5_11,@START_TIME,@END_TIME)";
                                myCommand.Parameters.Add("@DATE_", SqlDbType.SmallDateTime); myCommand.Parameters["@DATE_"].Value = DT.ToString("yyyy-MM-dd");
                                myCommand.Parameters.Add("@CHANNEL", SqlDbType.NVarChar); myCommand.Parameters["@CHANNEL"].Value = pg.Name;
                                myCommand.Parameters.Add("@TIME_", SqlDbType.NVarChar); myCommand.Parameters["@TIME_"].Value = dw[0].ToString();
                                myCommand.Parameters.Add("@TI", SqlDbType.Float); myCommand.Parameters["@TI"].Value = dw[1].ToString();
                                myCommand.Parameters.Add("@M20p", SqlDbType.Float); myCommand.Parameters["@M20p"].Value = dw[2].ToString();
                                myCommand.Parameters.Add("@F20p", SqlDbType.Float); myCommand.Parameters["@F20p"].Value = dw[3].ToString();
                                myCommand.Parameters.Add("@AAB", SqlDbType.Float); myCommand.Parameters["@AAB"].Value = dw[4].ToString();
                                myCommand.Parameters.Add("@A20ABC1", SqlDbType.Float); myCommand.Parameters["@A20ABC1"].Value = dw[5].ToString();
                                myCommand.Parameters.Add("@A5_11", SqlDbType.Float); myCommand.Parameters["@A5_11"].Value = dw[6].ToString();

                                string[] Ones = dw[0].ToString().Split('-');

                                myCommand.Parameters.Add("@START_TIME", SqlDbType.Float); myCommand.Parameters["@START_TIME"].Value = Ones[0].ToString().Replace(":", "").Replace(" ", "");
                                myCommand.Parameters.Add("@END_TIME", SqlDbType.Float); myCommand.Parameters["@END_TIME"].Value = Ones[1].ToString().Replace(":", "").Replace(" ", "");


                                myCommand.Connection = myConnection;
                                myCommand.ExecuteNonQuery();
                            }
                        }
                        myConnection.Close();
                    }
                }
            }



            using (SqlConnection connection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING))
            {
                string queryString = "SELECT * FROM dbo.ADM_MECRA_KODU_KONTROL ";
                SqlCommand commands = new SqlCommand(queryString, connection);
                connection.Open();
                SqlDataReader reader = commands.ExecuteReader();

                SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                myConnectionTable.Open();
                while (reader.Read())
                {
                    SqlCommand myCmd = new SqlCommand();

                    myCmd.Parameters.AddWithValue("@MECRA_KODU", reader["MECRA_KODU"]);
                    myCmd.Parameters.AddWithValue("@MECRA_KODU_CONVERT", reader["MECRA_KODU_CONVERT"]);
                    myCmd.CommandText = " UPDATE   dbo.DATA_TELEVIZYON_ONBESDKGRP SET Channel= @MECRA_KODU_CONVERT   WHERE Channel=@MECRA_KODU  ";
                    myCmd.Connection = myConnectionTable;
                    myCmd.ExecuteNonQuery();
                }
                //myCmd.Connection.Close();
                // Call Close when done reading.
                myConnectionTable.Close();
                reader.Close();
            }
        }
    }
}