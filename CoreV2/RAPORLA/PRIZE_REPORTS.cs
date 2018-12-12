using DevExpress.Spreadsheet;
using System.Collections;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace CoreV2.RAPORLA
{
    public partial class PRIZE_REPORTS : Form
    {
        ArrayList COLUMS = new ArrayList();
        string COLUMS_NAME = string.Empty;
        Worksheet worksheet;
        DataSet ds;
        int RowCount = 3;
        public PRIZE_REPORTS()
        {
            InitializeComponent();
        }

        private void btn_EXPORT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = ".xlsx (*.xlsx)|*.xlsx";
            sfd.FileName = "ListData.xlsx";
            DialogResult res = sfd.ShowDialog();
            if (res == DialogResult.OK)
            {
                spreadsheetControls.SaveDocument(sfd.FileName);

            }
        }

        private void btn_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void ColumsRead(string ALAN, int STARYEAR, int ENDYEAR, string TOPLAM_ALAN, int RaporTuru, string TABLO_KODU)
        {

            using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select *  from  OZL_RAPOR_LIST WHERE ID=" + RaporTuru, con))
                {
                    SqlDataReader myReader = cmd.ExecuteReader();
                    while (myReader.Read())
                    {

                        using (SqlConnection SqlConn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            SqlConn.Open();
                            using (SqlCommand cmdHdr = new SqlCommand())
                            {
                                cmdHdr.CommandText = "   select *  from  OZL_RAPOR_LIST_COLUMS  WHERE TABLO_KODU=@TABLO_KODU and   REFID=@REFID ";
                                cmdHdr.Parameters.AddWithValue("@REFID", myReader["ID"].ToString());
                                cmdHdr.Parameters.AddWithValue("@TABLO_KODU", TABLO_KODU);
                                cmdHdr.Connection = SqlConn;
                                SqlDataReader myRdrHdr = cmdHdr.ExecuteReader();
                                while (myRdrHdr.Read())
                                {
                                    COLUMS_NAME += "  ISNULL(SUM(CASE WHEN Ln." + ALAN + " = '" + myRdrHdr["MECRALAR"].ToString() + "' AND YEAR(Ln.[TARİH]) = '" + STARYEAR + "' THEN Ln.[" + TOPLAM_ALAN + "] END),  null) AS [" + STARYEAR + "], ";
                                    COLUMS_NAME += "  ISNULL(SUM(CASE WHEN Ln." + ALAN + " = '" + myRdrHdr["MECRALAR"].ToString() + "' AND YEAR(Ln.[TARİH]) = '" + ENDYEAR + "' THEN Ln.[" + TOPLAM_ALAN + "] END),  null) AS [" + ENDYEAR + "], ";
                                    //COLUMS_NAME += "  ISNULL(ISNULL(SUM(CASE WHEN Ln." + ALAN + " = '" + myRdrHdr["MECRALAR"].ToString() + "' AND YEAR(Ln.[TARİH]) = '" + STARYEAR + "' THEN Ln.[" + TOPLAM_ALAN + "] END),  null) / NULLIF (SUM(CASE WHEN Ln." + ALAN + "  = '" + myRdrHdr["MECRALAR"].ToString() + "' AND YEAR(Ln.[TARİH]) = '" + ENDYEAR + "' THEN Ln.[" + TOPLAM_ALAN + "] END), NULL),  null) AS [" + STARYEAR + "_" + ENDYEAR + "_" + myRdrHdr["MECRALAR"].ToString() + "],";
                                    COLUMS_NAME += "  CAST('0' as int ) AS [" + STARYEAR + "_" + ENDYEAR + "_" + myRdrHdr["MECRALAR"].ToString() + "],";
                                }
                                myRdrHdr.Close();
                            }
                        }
                    } myReader.Close();
                }
            }

        }


        private void Format(int StartRow, int EndRow, int RaporTuru, string TABLO_KODU)
        {

            using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                con.Open();
                using (SqlCommand cmd = new SqlCommand("select *  from  OZL_RAPOR_LIST WHERE ID=" + RaporTuru, con))
                {
                    SqlDataReader myReader = cmd.ExecuteReader();
                    while (myReader.Read())
                    {

                        using (SqlConnection SqlConn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                        {
                            SqlConn.Open();
                            using (SqlCommand cmdHdr = new SqlCommand())
                            {
                                cmdHdr.CommandText = "   select *  from  OZL_RAPOR_LIST_COLUMS  WHERE TABLO_KODU=@TABLO_KODU and   REFID=@REFID ";
                                cmdHdr.Parameters.AddWithValue("@REFID", myReader["ID"].ToString());
                                cmdHdr.Parameters.AddWithValue("@TABLO_KODU", TABLO_KODU);
                                cmdHdr.Connection = SqlConn;
                                SqlDataReader myRdrHdr = cmdHdr.ExecuteReader();
                                while (myRdrHdr.Read())
                                {
                                    worksheet.Cells["A1"].ColumnWidthInCharacters = 7;
                                    worksheet.Cells["B1"].ColumnWidthInCharacters = 2;
                                    worksheet.Cells["C1"].ColumnWidthInCharacters = 30;
                                    worksheet.Cells[myRdrHdr["TARGET"].ToString().Replace("$", StartRow.ToString())].Value = myRdrHdr["BASLIK"].ToString();
                                    //worksheet.Cells.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                                    //worksheet.Cells.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                                    worksheet.MergeCells(worksheet.Range[myRdrHdr["MERGES"].ToString().Replace("$", StartRow.ToString())]);
                                    Range range = worksheet.Range[myRdrHdr["MERGES"].ToString().Replace("$", StartRow.ToString())];
                                    Formatting rangeFormatting = range.BeginUpdateFormatting();
                                    if (myRdrHdr["COLOR"].ToString() == "Green")
                                    {
                                        rangeFormatting.Fill.BackgroundColor = Color.Green;
                                        rangeFormatting.Font.Color = Color.Black;
                                    }
                                    if (myRdrHdr["COLOR"].ToString() == "Yellow")
                                    {
                                        rangeFormatting.Fill.BackgroundColor = Color.Yellow;
                                        rangeFormatting.Font.Color = Color.Black;
                                    }

                                    rangeFormatting.Font.Size = 14;
                                    //rangeFormatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                                    //rangeFormatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                                    range.EndUpdateFormatting(rangeFormatting);


                                    int StartRowColor = StartRow + 1;
                                    Range rngBlackStart = worksheet.Range[myRdrHdr["MERGES"].ToString().Replace("$", StartRowColor.ToString())];
                                    Formatting rangeBlackStart = rngBlackStart.BeginUpdateFormatting();
                                    rangeBlackStart.Fill.BackgroundColor = Color.Black;
                                    rangeBlackStart.Font.Color = Color.White;
                                    rangeBlackStart.Font.Size = 14;
                                    //rangeBlack.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                                    //rangeBlack.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                                    rngBlackStart.EndUpdateFormatting(rangeBlackStart);
                                    //MECRALAR.Add(myRdrHdr["MECRALAR"].ToString());



                                    int EndRowColor = EndRow + 1;
                                    Range rngBlackEnd = worksheet.Range[myRdrHdr["MERGES"].ToString().Replace("$", EndRowColor.ToString())];
                                    Formatting rangeBlackEnd = rngBlackEnd.BeginUpdateFormatting();
                                    rangeBlackEnd.Fill.BackgroundColor = Color.Black;
                                    rangeBlackEnd.Font.Color = Color.White;
                                    rangeBlackEnd.Font.Size = 14;
                                    //rangeBlack.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                                    //rangeBlack.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                                    rngBlackEnd.EndUpdateFormatting(rangeBlackEnd);
                                    //MECRALAR.Add(myRdrHdr["MECRALAR"].ToString());




                                }
                                myRdrHdr.Close();
                            }
                        }


                        spreadsheetControls.Document.Worksheets.ActiveWorksheet.Name = myReader["RAPOR_ADI"].ToString();

                    } myReader.Close();
                }
            }

        }

        private void btn_RUN_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {


            DataSet ds;
            spreadsheetControls.Document.BeginUpdate();
            try
            {

                worksheet = spreadsheetControls.Document.Worksheets[0];
                worksheet.ActiveView.Zoom = 80;
                worksheet.DeleteCells(worksheet.Cells, DeleteMode.ShiftCellsLeft);
                worksheet.Cells["C1"].ColumnWidthInCharacters = 40;
                worksheet.Cells["C1"].Value = "Medya Ajanslarının Gazete ST/CM kullanımları ve Pay Geçişleri %";

                //Range range = worksheet.Range["A6:AH7"];
                //Formatting rangeFormatting = range.BeginUpdateFormatting();
                //rangeFormatting.Fill.BackgroundColor = Color.Black;
                //rangeFormatting.Font.Color = Color.White;
                //rangeFormatting.Font.Size = 14;
                //rangeFormatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
                //rangeFormatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
                //range.EndUpdateFormatting(rangeFormatting);

                ColumsRead("MEDYA", 2015, 2016, "SANTIM", 1, "T1");



                string SQL = "SELECT  Ln.GroupM AS AJANSLAR, " +
                        " ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2014' THEN Ln.[SANTIM] END), 0) AS [2014 TY], " +
                        " ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY], " +
                        " ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2014' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2014/2015 TY Geçiş %]," +
                        " ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2014 YTD], " +
                        " ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD]," +
                        " ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0)  / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2014/2015 YTD Geçiş %] ," + COLUMS_NAME.Substring(0, COLUMS_NAME.Length - 1) +
                        "  FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN  " +
                                             "    dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER  " +
                        " GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID,  dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID " +
                        " HAVING        (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1') AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 1) " +
                        " UNION ALL  " +
                        " SELECT    CAST('TOPLAM' as NVARCHAR), " +
                                       "      ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2014' THEN Ln.[SANTIM] END), 0) AS [2014 TY],  " +
                                       "      ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],  " +
                                       "      ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2014' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2014/2015 TY Geçiş %],  " +

                                       "      ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2014 YTD], " +
                                       "      ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD],  " +
                                       "      ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2014/2015 YTD Geçiş %], " + COLUMS_NAME.Substring(0, COLUMS_NAME.Length - 1) +
                        " FROM   dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN " +
                        " dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER " +
                        " GROUP BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID " +
                        " HAVING        (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1') AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 1)";

                //                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";


                using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                    ds = new DataSet();
                    da.Fill(ds, "TABLO");
                    worksheet.Import(ds.Tables[0], true, 3, 2);
                }
                RowCount = 3;
                Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 1, "T1");
                RowCount += ds.Tables[0].Rows.Count;
                RowCount = RowCount + 7;

                SQL = @"SELECT   Ln.[GroupM-Diger] AS AJANSLAR, ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2014' THEN Ln.[SANTIM] END), 0) AS [2014 TY], ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) 
                         = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY], ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2014' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) 
                         = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2014/2015 TY Geçiş %], ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2014 YTD], 
                         ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD], ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) 
                         / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2014/2015 YTD Geçiş %], ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) 
                         = '2015' THEN Ln.[SANTIM] END), 0) AS Expr1, ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2016 TY], 
                         ISNULL(ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) 
                         = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS Expr2, ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015], 
                         ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2016], ISNULL(ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) 
                         = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015/2016 TY Geçiş %]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.[GroupM-Diger] = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.[GroupM-Diger], dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING     (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T2')AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 1)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID";
                using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                    ds = new DataSet();
                    da.Fill(ds, "TABLO");
                    worksheet.Import(ds.Tables[0], true, RowCount, 2);
                }

                Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 1, "T1");
                RowCount += ds.Tables[0].Rows.Count;
                RowCount = RowCount + 4;





                SQL = @"SELECT  Ln.GroupM AS AJANSLAR, ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2014' THEN Ln.[SANTIM] END), 0) AS [2014 TY], ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) 
                         = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY], ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2014' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) 
                         = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2014/2015 TY Geçiş %], ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2014 YTD], 
                         ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD], ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) 
                         / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2014/2015 YTD Geçiş %], ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) 
                         = '2015' THEN Ln.[SANTIM] END), 0) AS Expr1, ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2016 TY], 
                         ISNULL(ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) 
                         = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS Expr2, ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015], 
                         ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2016], ISNULL(ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) 
                         = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015/2016 TY Geçiş %]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING    (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 1)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";


                using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                    ds = new DataSet();
                    da.Fill(ds, "TABLO");
                    //ds.Tables[0].Columns.RemoveAt(0);
                    worksheet.Import(ds.Tables[0], true, RowCount, 2);

                }

                Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 1, "T2");
                RowCount += ds.Tables[0].Rows.Count;
                RowCount = RowCount + 4;




                SQL = @"SELECT  Ln.GroupM AS AJANSLAR, 
                        ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2014' THEN Ln.[SANTIM] END), 0) AS [2014 TY], 
                        ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2014' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2014/2015 TY Geçiş %], 
                       
                         ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2014 YTD], 
                         ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD],
                         ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2014/2015 YTD Geçiş %], 
                         
                         ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS Expr1,

                         ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2016 TY], 
                         ISNULL(ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS Expr2, 

                         
                         ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015], 
                         ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2016], 
                         ISNULL(ISNULL(SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HÜRRİYET' AND YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015/2016 TY Geçiş %]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                      dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.[GroupM-Diger] = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING     (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T2') AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 1)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";


                using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                    ds = new DataSet();
                    da.Fill(ds, "TABLO");
                    //ds.Tables[0].Columns.RemoveAt(0);
                    worksheet.Import(ds.Tables[0], true, RowCount, 2);

                }


                Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 1, "T2");
                RowCount = ds.Tables[0].Rows.Count;

                Sheet_IKI();
                Sheet_UC();
                Sheet_DORT();
                Sheet_BES();

            }
            finally
            {
                spreadsheetControls.Document.EndUpdate();
            }



            //using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            //{

            //    SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
            //    ds = new DataSet();
            //    da.Fill(ds, "TABLO");
            //    ds.Tables[0].Columns.RemoveAt(0);
            //}



            //spreadsheetControls.Document.BeginUpdate();
            //try
            //{

            //    worksheet = spreadsheetControls.Document.Worksheets[0];
            //    worksheet.ActiveView.Zoom = 80;
            //    worksheet.DeleteCells(worksheet.Cells, DeleteMode.ShiftCellsLeft);
            //    worksheet.Cells["A1"].ColumnWidthInCharacters = 35;
            //    worksheet.Cells["A1"].Value = "Veri";
            //    worksheet.Cells["A2"].Value = "Dönem";
            //    worksheet.Cells["B1"].Value = ": Bütçe TL";
            //    worksheet.Cells["B2"].Value = ": Ocak - Ekim (YTD dönemi)";
            //    Range range = worksheet.Range["A6:AH7"];
            //    Formatting rangeFormatting = range.BeginUpdateFormatting();
            //    rangeFormatting.Fill.BackgroundColor = Color.Black;
            //    rangeFormatting.Font.Color = Color.White;
            //    rangeFormatting.Font.Size = 14;
            //    rangeFormatting.Alignment.Vertical = SpreadsheetVerticalAlignment.Center;
            //    rangeFormatting.Alignment.Horizontal = SpreadsheetHorizontalAlignment.Center;
            //    range.EndUpdateFormatting(rangeFormatting);

            //    worksheet.Import(ds.Tables[0], true, 7, 0);


            //    int Blues = ds.Tables[0].Rows.Count + 7;
            //    Range rangecls = worksheet.Range["D6:D" + Blues + ",g6:G" + Blues + ",J6:J" + Blues + ",M6:M" + Blues + ",P6:p" + Blues + ",S6:s" + Blues + ",V6:v" + Blues + ",Y6:y" + Blues + ",AB6:AB" + Blues + ",AE6:AE" + Blues + ",AH6:AH" + Blues + ""];
            //    rangecls.ClearFormats(); ;
            //    Formatting rangeFor = rangecls.BeginUpdateFormatting();
            //    rangeFor.Font.Color = Color.White;
            //    rangeFor.Fill.BackgroundColor = SystemColors.MenuHighlight;
            //    rangecls.EndUpdateFormatting(rangeFor);
            //    ConditionalFormattingCollection conditionalFormattings = worksheet.ConditionalFormattings;
            //    // Set the first threshold to the lowest value in the range of cells CoreV2 the MIN() formula.
            //    ConditionalFormattingIconSetValue minPoint = conditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Formula, "=MIN($D$7:$D$100)", ConditionalFormattingValueOperator.GreaterOrEqual);
            //    // Set the second threshold to 0.
            //    ConditionalFormattingIconSetValue midPoint = conditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0", ConditionalFormattingValueOperator.GreaterOrEqual);
            //    // Set the third threshold to 0.01.
            //    ConditionalFormattingIconSetValue maxPoint = conditionalFormattings.CreateIconSetValue(ConditionalFormattingValueType.Number, "0.01", ConditionalFormattingValueOperator.GreaterOrEqual);
            //    // Create the rule to apply a specific icon from the three arrow icon set to each cell in the range  E2:E15 based on its value.  
            //    IconSetConditionalFormatting cfRule = conditionalFormattings.AddIconSetConditionalFormatting(worksheet.Range["$D$7:$D$100"], IconSetType.Arrows3, new ConditionalFormattingIconSetValue[] { minPoint, midPoint, maxPoint });
            //    // Specify the custom icon to be displayed if the second condition is true. 
            //    // To do this, set the IconSetConditionalFormatting.IsCustom property to true, which is false by default.
            //    cfRule.IsCustom = true;
            //    // Initialize the ConditionalFormattingCustomIcon object.
            //    ConditionalFormattingCustomIcon cfCustomIcon = new ConditionalFormattingCustomIcon();
            //    // Specify the icon set where you wish to get the icon. 
            //    cfCustomIcon.IconSet = IconSetType.TrafficLights13;
            //    // Specify the index of the desired icon in the set.
            //    cfCustomIcon.IconIndex = 1;
            //    // Add the custom icon at the specified position in the initial icon set.
            //    cfRule.SetCustomIcon(1, cfCustomIcon);
            //    // Hide values of cells to which the rule is applied.
            //    cfRule.ShowValue = true;


            //    spreadsheetControls.Document.Worksheets.ActiveWorksheet.Name = "Tv Ölçülen Bütçe YTD-Ekim15";


            //}
            //finally
            //{
            //    spreadsheetControls.Document.EndUpdate();
            //}


        }

        private void Sheet_IKI()
        {

            ///
            ///  SHEET IKI
            ///


            ///
            /// BIRINCI GRUP TABLOLAR
            ///

            RowCount = 3;

            worksheet = spreadsheetControls.Document.Worksheets.Add();
            worksheet.ActiveView.Zoom = 80;
            worksheet.DeleteCells(worksheet.Cells, DeleteMode.ShiftCellsLeft);
            worksheet.Cells["C1"].Value = "2015 Tüm Yıl Medya Ajanslarının Gazete kullanımları - St/Cm";



            string SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 YTD],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD]
 
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING     (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1') AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 2)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";




            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, 3, 2);
            }
            RowCount = 3;
            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 2, "T1");
            RowCount += ds.Tables[0].Rows.Count;
            RowCount = RowCount + 7;


            SQL = @"SELECT   Ln.[GroupM-Diger] AS AJANSLAR,
                        ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],
                        ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 YTD], 
                        ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2016 YTD]                         
                        FROM    dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.[GroupM-Diger] = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.[GroupM-Diger], dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING       (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T2')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 2) 
                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID";



            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
            }

            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 2, "T1");
            RowCount += ds.Tables[0].Rows.Count;
            RowCount = RowCount + 4;




            ///
            /// ORANLAR TABLOSU 
            ///


            SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                     ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 YTD],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2016 YTD],  
                        ISNULL(ISNULL(SUM(CASE WHEN  YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HURRIYET GAZETESI' AND   YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY]                         
                        FROM     dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING   (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 2)
                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";




            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
            }

            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 2, "T1");
            RowCount += ds.Tables[0].Rows.Count;
            RowCount = RowCount + 4;



            SQL = @"SELECT   Ln.[GroupM-Diger] AS AJANSLAR,
                                            ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY],
                                            ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 YTD],
                                            ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2016 YTD],  
                                            ISNULL(ISNULL(SUM(CASE WHEN  YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HURRIYET GAZETESI' AND   YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.[GroupM-Diger] = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.[GroupM-Diger], dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING        (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T2')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 2)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID";
            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
            }

            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 2, "T1");
            RowCount += ds.Tables[0].Rows.Count;
            RowCount = RowCount + 4;


            ///
            /// IKINCI GRUP TABLOLAR
            ///

            SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 YTD],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD]  
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                        dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING     (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 2) 
                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";
            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
            }
            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 2, "T2");
            RowCount += ds.Tables[0].Rows.Count;
            RowCount = RowCount + 4;


            SQL = @"SELECT   Ln.[GroupM-Diger] AS AJANSLAR,
                        ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],
                        ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 YTD], 
                        ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2016 YTD]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.[GroupM-Diger] = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.[GroupM-Diger], dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING    (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T2')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 2)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID";

            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
            }
            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 2, "T2");
            RowCount += ds.Tables[0].Rows.Count;
            RowCount = RowCount + 4;




            ///
            /// ORANLAR TABLOSU
            ///


            SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                     ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 YTD],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2016 YTD],  
                        ISNULL(ISNULL(SUM(CASE WHEN  YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HURRIYET GAZETESI' AND   YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING        (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 2)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";

            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
            }
            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 2, "T2");
            RowCount += ds.Tables[0].Rows.Count;
            RowCount = RowCount + 4;

            SQL = @"SELECT   Ln.[GroupM-Diger] AS AJANSLAR,
                                             ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY],
                                            ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 YTD],
                                            ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2016 YTD],  
                                            ISNULL(ISNULL(SUM(CASE WHEN  YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HURRIYET GAZETESI' AND   YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.[GroupM-Diger] = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.[GroupM-Diger], dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING        (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T2')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 2)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID";
            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
            }
            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 2, "T2");
            RowCount += ds.Tables[0].Rows.Count;
            RowCount = RowCount + 4;


        }
        private void Sheet_UC()
        {

            ///
            ///  SHEET UC
            ///


            ///
            /// BIRINCI GRUP TABLOLAR
            ///

            RowCount = 3;

            worksheet = spreadsheetControls.Document.Worksheets.Add();
            worksheet.ActiveView.Zoom = 80;
            worksheet.DeleteCells(worksheet.Cells, DeleteMode.ShiftCellsLeft);
            worksheet.Cells["C1"].Value = "2015 Tüm Yıl Gazetelerin Ajans Dağılımları - St/Cm";
            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 3, "T1");

            string SQL = @"SELECT  Ln.GroupM AS GAZETELER,
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 YTD],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD]
 
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING        (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 3)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";


            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, 3, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }


            ///
            /// ORANLAR TABLOSU 
            ///

            RowCount = RowCount + 7;

            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 3, "T1");
            SQL = @"SELECT  Ln.GroupM AS GAZETELER,
                                     ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 YTD],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2016 YTD],  
                        ISNULL(ISNULL(SUM(CASE WHEN  YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HURRIYET GAZETESI' AND   YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING     (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 3)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";

            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }




            ///
            /// IKINCI GRUP TABLOLAR
            ///
            RowCount = RowCount + 7;

            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 3, "T2");

            SQL = @"SELECT  Ln.GroupM AS GAZETELER,
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 YTD],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD]
 
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING       (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 3)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";


            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }





            ///
            /// ORANLAR TABLOSU
            ///

            RowCount = RowCount + 7;

            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 3, "T2");
            SQL = @"SELECT  Ln.GroupM AS GAZETELER,
                                     ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 YTD],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2016 YTD],  
                        ISNULL(ISNULL(SUM(CASE WHEN  YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HURRIYET GAZETESI' AND   YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING     (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 3)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";

            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }


        }
        private void Sheet_DORT()
        {

            ///
            ///  SHEET UC
            ///


            ///
            /// BIRINCI GRUP TABLOLAR
            ///

            RowCount = 3;

            worksheet = spreadsheetControls.Document.Worksheets.Add();
            worksheet.ActiveView.Zoom = 80;
            worksheet.DeleteCells(worksheet.Cells, DeleteMode.ShiftCellsLeft);
            worksheet.Cells["C1"].Value = "2015 Tüm Yıl Gazetelerin Ajans Dağılımları - St/Cm";


            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 4, "T1");
            string SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 YTD],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD]
 
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING      (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')  AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 4)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";


            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, 3, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }


            ///
            /// ORANLAR TABLOSU 
            ///

            RowCount = RowCount + 7;

            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 4, "T1");
            SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                     ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 YTD],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2016 YTD],  
                        ISNULL(ISNULL(SUM(CASE WHEN  YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HURRIYET GAZETESI' AND   YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING      (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1') AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 4)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";

            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }




            ///
            /// IKINCI GRUP TABLOLAR
            ///
            RowCount = RowCount + 7;

            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 4, "T1");
            SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 YTD],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD]
 
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING         (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1') AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 4)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";


            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }

            RowCount = RowCount + 7;
            ///
            /// ORANLAR TABLOSU
            /// 

            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 4, "T1");
            SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                     ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 YTD],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2016 YTD],  
                        ISNULL(ISNULL(SUM(CASE WHEN  YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HURRIYET GAZETESI' AND   YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING      (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1') AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 4)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";

            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }


        }
        private void Sheet_BES()
        {

            ///
            ///  SHEET BES
            ///


            ///
            /// BIRINCI GRUP TABLOLAR
            ///

            RowCount = 3;

            worksheet = spreadsheetControls.Document.Worksheets.Add();
            worksheet.ActiveView.Zoom = 80;
            worksheet.DeleteCells(worksheet.Cells, DeleteMode.ShiftCellsLeft);
            worksheet.Cells["C1"].Value = "2015 Tüm Yıl Gazetelerin Ajans Dağılımları - St/Cm";


            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 5, "T1");

            string SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 YTD],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD]
 
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING    (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1') AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 5)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";


            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, 3, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }


            ///
            /// ORANLAR TABLOSU 
            ///

            RowCount = RowCount + 7;
            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 5, "T1");
            SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                     ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 YTD],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2016 YTD],  
                        ISNULL(ISNULL(SUM(CASE WHEN  YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HURRIYET GAZETESI' AND   YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING       (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 5)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";

            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }




            ///
            /// IKINCI GRUP TABLOLAR
            ///
            RowCount = RowCount + 7;
            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 5, "T1");

            SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 TY],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) AS [2015 YTD],
                                            ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) AS [2015 YTD]
 
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING        (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 5)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";


            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }


            ///
            /// ORANLAR TABLOSU
            /// 
            RowCount = RowCount + 7;
            Format(RowCount, ds.Tables[0].Rows.Count + RowCount, 5, "T1");
            SQL = @"SELECT  Ln.GroupM AS AJANSLAR,
                                     ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 YTD],
                        ISNULL(ISNULL(SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2016 YTD],  
                        ISNULL(ISNULL(SUM(CASE WHEN  YEAR(Ln.[TARİH]) = '2015' THEN Ln.[SANTIM] END), 0) / NULLIF (SUM(CASE WHEN Ln.MEDYA = 'HURRIYET GAZETESI' AND   YEAR(Ln.[TARİH]) = '2016' THEN Ln.[SANTIM] END), NULL), 0) AS [2015 TY]
                         
                        FROM            dbo.[__TEMP_hasan.yogurtcu] AS Ln INNER JOIN
                                                 dbo.OZL_RAPOR_LIST_ROWS_VALUE ON Ln.GroupM = dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER
                        GROUP BY Ln.GroupM, dbo.OZL_RAPOR_LIST_ROWS_VALUE.DEGER, dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU, dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID, dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID
                        HAVING    (dbo.OZL_RAPOR_LIST_ROWS_VALUE.GRUP_TABLE_KODU = N'T1')AND (dbo.OZL_RAPOR_LIST_ROWS_VALUE.REFID = 5)

                        ORDER BY dbo.OZL_RAPOR_LIST_ROWS_VALUE.ID ";

            using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            {
                SqlDataAdapter da = new SqlDataAdapter(SQL, myConnections);
                ds = new DataSet();
                da.Fill(ds, "TABLO");
                worksheet.Import(ds.Tables[0], true, RowCount, 2);
                RowCount += ds.Tables[0].Rows.Count;
            }


        }

    }
}
