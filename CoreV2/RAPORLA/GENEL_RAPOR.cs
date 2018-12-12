using DevExpress.Spreadsheet;
using System;

namespace CoreV2.RAPORLA
{
    public partial class GENEL_RAPOR : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        string _EXPORT_FILE = "";
        bool _AUTO_SAVE = false;
        public GENEL_RAPOR(string EXPORT_FILE,bool AUTO_SAVE)
        {
            InitializeComponent();


            ControlBox = false;

            if(!AUTO_SAVE) WindowState = System.Windows.Forms.FormWindowState.Maximized;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;



            _EXPORT_FILE = EXPORT_FILE;
            _AUTO_SAVE = AUTO_SAVE;

            //  spreadsheetControls.SaveDocument();


            //      IWorkbook workbook = spreadsheetControls.Document;
            //    workbook.Options.Save.CurrentFileName = "C:\\Temp\\SavedDoffffcument.xlsx";
            //  spreadsheetControls.SaveDocumentAs();  // saves as PA-FE-01-KTL1.xlsx





            //       spreadsheetControls.SaveDocument("C:\\Temp\\SavedDoffffcument.xlsx", DocumentFormat.Xlsx);

            //Stream myStream;
            //SaveFileDialog saveFileDialog1 = new SaveFileDialog();

            //saveFileDialog1.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            //saveFileDialog1.FilterIndex = 2;
            //saveFileDialog1.RestoreDirectory = true;
            //saveFileDialog1.FileName = "test.1";

            //if (saveFileDialog1.ShowDialog() == DialogResult.OK)
            //{
            //    if ((myStream = saveFileDialog1.OpenFile()) != null)
            //    {
            //        // Code to write the stream goes here.
            //        myStream.Close();
            //    }
            //}




            //Workbook workbook = new Workbook();
            //IWorkbook workbooks = spreadsheetControls.Document;
            //// ... 

            //// Save the modified document to the stream. 
            //using (FileStream stream = new FileStream("C:\\Temp\\SavedDoffffcument.xlsx",
            //    FileMode.Create, FileAccess.ReadWrite))
            //{
            //    workbooks.SaveDocument(stream, DocumentFormat.OpenXml);
            //}



            //"C:\\Temp\\SavedDocument.xlsx"


            //IWorkbook workbook = spreadsheetControls.Document;
            //// ... 

            //// Save the modified document to a stream. 
            //using (FileStream stream = new FileStream("C:\\Temp\\SavedDocument.xlsx", FileMode.Create, FileAccess.ReadWrite))
            //{
            //    workbook.SaveDocument(stream, DocumentFormat.OpenXml);


            //}
        }

        private void BTN_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Close();
        }

        private void BTN_KAYDET_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {  
            spreadsheetControls.SaveDocumentAs();


            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = ".xlsx (*.xlsx)|*.xlsx";
            //sfd.FileName = "ListData.xlsx";
            //DialogResult res = sfd.ShowDialog();
            //if (res == DialogResult.OK)
            //{
              
            //}
            
            //SaveFileDialog sfd = new SaveFileDialog();
            //sfd.Filter = ".xlsx (*.xlsx)|*.xlsx";
            //sfd.FileName = "ListData.xlsx";
            //DialogResult res = sfd.ShowDialog();
            //if (res == DialogResult.OK)
            //{

            //    spreadsheetControls.SaveDocument ();
                
            //}
        }

        private void GENEL_RAPOR_Load(object sender, EventArgs e)
        {

            if (_AUTO_SAVE)
            {
                // Create an instance of a workbook.
                IWorkbook workbook = spreadsheetControls.Document;// = new DevExpress.Spreadsheet.Workbook();

                //// Access the first worksheet in the workbook.
                Worksheet worksheet = workbook.Worksheets[1];

                //// Access the "A1" cell in the worksheet.
                //Cell cell = worksheet.Cells["A1"];

                //// Specify the "A1" cell value.
                //cell.Value = 1;

                //// Fill cells with sequential numbers by using shared formulas.
                //worksheet.Range["A2:A10"].Formula = "=SUM(A1+1)";
                //worksheet.Range["B1:B10"].Formula = "=A1+2";

                //// Multiply values contained in the cell range A1 through A10 
                //// by the corresponding values contained in B1 through B10,
                //// and display the results in cells C1 through C10.
                //worksheet.Range["C1:C10"].ArrayFormula = "=A1:A10*B1:B10";

                // Save the document file under the specified name.
                //if (_EXPORT_FILE == "")
             //   _EXPORT_FILE = "C:\\temp\\CoreRapor"+DateTime.Now.ToString().Replace(".","_").Replace(":","_") +".xlsx";
                workbook.SaveDocument(_EXPORT_FILE, DocumentFormat.OpenXml);

                // Display gridlines in PDF.
                //  worksheet.PrintOptions.PrintGridlines = true;

                // Export the document to PDF.
                //workbook.ExportToPdf("TestDoc.pdf");

                // Open the PDF document using the default viewer..
                //System.Diagnostics.Process.Start("TestDoc.pdf");

                // Open the XLSX document using the default application.
                System.Diagnostics.Process.Start(_EXPORT_FILE);

                Close();
            }
        }
    }
}