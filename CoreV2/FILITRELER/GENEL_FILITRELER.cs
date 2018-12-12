using System;
using System.Data;
using System.Data.SqlClient;

namespace CoreV2.FILITRELER
{
    public partial class GENEL_FILITRELER : DevExpress.XtraEditors.XtraForm
    {
        public string _SATIRLAR_NEREYE;
        public string _FILITRE_NEREYE;
        public DataView dvSELECT;
        DataView dvLIST;
        string _TABLE_NAME="";
        string _QUERY="";
        string _FIELD_NAME="";


        string __FIELD_NAME; string __GROUP_BY; string __QUERY; bool __TELEVIZYON; bool __GAZETE; bool __DERGI; bool __OUTDOOR; bool __SINEMA; bool __RADYO; bool __INTERNET;

        public GENEL_FILITRELER(string FIELD_NAME, string GROUP_BY, string QUERY, bool TELEVIZYON, bool GAZETE, bool DERGI, bool OUTDOOR, bool SINEMA, bool RADYO, bool INTERNET)
        {
            InitializeComponent();

            ControlBox = false;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;


            //_SATIRLAR_NEREYE = "DAHIL";
            //_FILITRE_NEREYE = "OZEL";
            //_QUERY = QUERY;
            //_FIELD_NAME = FIELD_NAME;
            //string YAYINTURU = string.Empty;
            //if (TELEVIZYON) { YAYINTURU += " YAYIN_SINIFI='TELEVIZYON' OR "; }
            //if (GAZETE) { YAYINTURU += " YAYIN_SINIFI='GAZETE' OR "; }
            //if (DERGI) { YAYINTURU += " YAYIN_SINIFI='DERGI' OR "; }
            //if (OUTDOOR) { YAYINTURU += " YAYIN_SINIFI='OUTDOOR' OR "; }
            //if (SINEMA) { YAYINTURU += " YAYIN_SINIFI='SINEMA' OR "; }
            //if (RADYO) { YAYINTURU += " YAYIN_SINIFI='RADYO' OR "; }
            //if (INTERNET) { YAYINTURU += " YAYIN_SINIFI='INTERNET' OR "; }
            //if (YAYINTURU != "") YAYINTURU = "(" + YAYINTURU.Substring(0, YAYINTURU.Length - 4) + ")";
            //if (QUERY != "") QUERY = " AND " + QUERY;
            //_TABLE_NAME = " cast(null as int) as ID , cast(null as nvarchar) as SIRKET_KODU , cast(null as nvarchar) as TARIFE_KODU, cast(null as uniqueidentifier) as GUI, cast(null as nvarchar) as KIRILIM_KODU, cast(" + _FIELD_NAME + " as nvarchar) as FIELD_NAME,  " + _FIELD_NAME + ", cast('False' as bit) as Auto FROM dbo._ADEX_INDEX_DATA  WHERE " + YAYINTURU + QUERY + " group by  " + GROUP_BY + " order by " + _FIELD_NAME + " ";
            //if (QUERY != "") _TABLE_NAME = String.Format(" cast(null as int) as ID , cast(null as nvarchar) as SIRKET_KODU , cast(null as nvarchar) as TARIFE_KODU, cast(null as uniqueidentifier) as GUI, cast(null as nvarchar) as KIRILIM_KODU, cast(" + _FIELD_NAME + " as nvarchar) as FIELD_NAME,  " + _FIELD_NAME + " , cast('False' as bit) as Auto FROM dbo._ADEX_INDEX_DATA   WHERE {0}  group by " + GROUP_BY, YAYINTURU + QUERY) + " order by " + _FIELD_NAME + "  ";



            __FIELD_NAME = FIELD_NAME;   __GROUP_BY =GROUP_BY; __QUERY = QUERY;   __TELEVIZYON =TELEVIZYON;   __GAZETE =GAZETE;  __DERGI = DERGI; __OUTDOOR = OUTDOOR;  __SINEMA = SINEMA;  __RADYO = RADYO; __INTERNET = INTERNET;
             
            Liste( FIELD_NAME,  GROUP_BY,  QUERY,  TELEVIZYON,  GAZETE,  DERGI,  OUTDOOR,  SINEMA,  RADYO,  INTERNET);

        }
        private void Liste(string FIELD_NAME, string GROUP_BY, string QUERY, bool TELEVIZYON, bool GAZETE, bool DERGI, bool OUTDOOR, bool SINEMA, bool RADYO, bool INTERNET)
        {
            try
            {

                _SATIRLAR_NEREYE = "DAHIL";
                _FILITRE_NEREYE = "OZEL";
                _QUERY = QUERY;
                _FIELD_NAME = FIELD_NAME;
                string YAYINTURU = string.Empty;
                if (TELEVIZYON) { YAYINTURU += " YAYIN_SINIFI='TELEVIZYON' OR "; }
                if (GAZETE) { YAYINTURU += " YAYIN_SINIFI='GAZETE' OR "; }
                if (DERGI) { YAYINTURU += " YAYIN_SINIFI='DERGI' OR "; }
                if (OUTDOOR) { YAYINTURU += " YAYIN_SINIFI='OUTDOOR' OR "; }
                if (SINEMA) { YAYINTURU += " YAYIN_SINIFI='SINEMA' OR "; }
                if (RADYO) { YAYINTURU += " YAYIN_SINIFI='RADYO' OR "; }
                if (INTERNET) { YAYINTURU += " YAYIN_SINIFI='INTERNET' OR "; }
                if (YAYINTURU != "") YAYINTURU = "(" + YAYINTURU.Substring(0, YAYINTURU.Length - 4) + ")";
                if (QUERY != "") QUERY = " AND " + QUERY;
                _TABLE_NAME = " cast(null as int) as ID , cast(null as nvarchar) as SIRKET_KODU , cast(null as nvarchar) as TARIFE_KODU, cast(null as uniqueidentifier) as GUI, cast(null as nvarchar) as KIRILIM_KODU, cast(" + _FIELD_NAME + " as nvarchar) as FIELD_NAME,  " + _FIELD_NAME + ", cast('False' as bit) as Auto FROM dbo._ADEX_INDEX_DATA  WHERE " + YAYINTURU + QUERY + " group by  " + GROUP_BY + " order by " + _FIELD_NAME + " ";
                if (QUERY != "") _TABLE_NAME = String.Format(" cast(null as int) as ID , cast(null as nvarchar) as SIRKET_KODU , cast(null as nvarchar) as TARIFE_KODU, cast(null as uniqueidentifier) as GUI, cast(null as nvarchar) as KIRILIM_KODU, cast(" + _FIELD_NAME + " as nvarchar) as FIELD_NAME,  " + _FIELD_NAME + " , cast('False' as bit) as Auto FROM dbo._ADEX_INDEX_DATA   WHERE {0}  group by " + GROUP_BY, YAYINTURU + QUERY) + " order by " + _FIELD_NAME + "  ";
                 

                if (_TABLE_NAME != null)
                {
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        DataSet ds = new DataSet();
                        string query = "   select " + _TABLE_NAME;
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = new SqlCommand(query, conn);
                        adapter.SelectCommand.CommandTimeout = 0;
                        adapter.Fill(ds, "dbo_" + _TABLE_NAME);
                        DataViewManager dvManager = new DataViewManager(ds);
                          dvLIST = dvManager.CreateDataView(ds.Tables[0]);
                        grdCntrl_LIST.DataSource = dvLIST;
                        gridView_LIST.Columns[0].Visible = false;
                        gridView_LIST.Columns[1].Visible = false;
                        gridView_LIST.Columns[2].Visible = false;
                        gridView_LIST.Columns[3].Visible = false;
                        gridView_LIST.Columns[4].Visible = false;
                        gridView_LIST.Columns[5].Visible = false;
                        gridView_LIST.Columns[6].Width = 300;
                        gridView_LIST.Columns[6].OptionsColumn.AllowEdit = false;
                        gridView_LIST.Columns[7].Width = 30;
                        gridView_LIST.Columns[7].OptionsColumn.AllowEdit = true;
                    }
                    gridView_LIST.ShowFindPanel();
                }

                if (_TABLE_NAME != null)
                {
                    dvSELECT = null;
                    using (SqlConnection myConnections = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        string SQL = "   select top 0 " + _TABLE_NAME;
                        SqlDataAdapter MySqlDataAdapter = new SqlDataAdapter(SQL, myConnections);
                        DataSet ds = new DataSet();
                        MySqlDataAdapter.Fill(ds, "dbo_" + _TABLE_NAME);
                        DataViewManager dvManager = new DataViewManager(ds);
                        dvSELECT = dvManager.CreateDataView(ds.Tables[0]);
                        grdCntrl_SELECT.DataSource = dvSELECT;
                        gridView_SELECT.Columns[0].Visible = false;
                        gridView_SELECT.Columns[1].Visible = false;
                        gridView_SELECT.Columns[2].Visible = false;
                        gridView_SELECT.Columns[3].Visible = false;
                        gridView_SELECT.Columns[4].Visible = false;
                        gridView_SELECT.Columns[5].Visible = false;
                        gridView_SELECT.Columns[6].Width = 300;
                        gridView_SELECT.Columns[6].OptionsColumn.AllowEdit = false;
                        gridView_SELECT.Columns[7].Width = 30;
                        gridView_SELECT.Columns[7].OptionsColumn.AllowEdit = true;
                    }
                }
            }
            catch { }
        }

        private void BR_KAPAT_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        { 
            if (TOOGLE_DAHIL_HARIC.IsOn) _SATIRLAR_NEREYE = "HARIC"; else _SATIRLAR_NEREYE = "DAHIL";
            if (TOOGLE_OZEL_GENEL_FILITRE.IsOn) _FILITRE_NEREYE = "GENEL"; else _FILITRE_NEREYE = "OZEL"; 
            Close();
        }

        private void MN_TUMUNU_SEC_Click(object sender, EventArgs e)
        {
            gridView_LIST.SelectAll();
        }

        private void MN_EKLE_Click(object sender, EventArgs e)
        {
            int[] GETROW = gridView_LIST.GetSelectedRows();
            for (int i = 0; i < GETROW.Length; i++)
            {

                DataRow dr = gridView_LIST.GetDataRow(Convert.ToInt32(GETROW[i]));
                DataRow drx = dvSELECT.Table.NewRow();
                for (int Coli = 0; Coli <= gridView_LIST.Columns.Count - 1; Coli++)
                {
                    drx[Coli] = dr[Coli];
                }
                dvSELECT.Table.Rows.Add(drx); 
            } 
            for (int i = GETROW.Length - 1; i >= 0; i--)
            {
                DataRow drs = gridView_LIST.GetDataRow(Convert.ToUInt16(GETROW[i]));
                if (drs != null)
                {
                    drs.Delete();
                }
            }
        }

        private void MN_TRG_TUMUNU_SEC_Click(object sender, EventArgs e)
        {
            gridView_SELECT.SelectAll();
        }

        private void MN_SIL_Click(object sender, EventArgs e)
        {
            int[] GETROW = gridView_SELECT.GetSelectedRows();
            for (int i = 0; i < GETROW.Length; i++)
            {
                DataRow dr = gridView_SELECT.GetDataRow(Convert.ToInt32(GETROW[i])); 
                DataRow drx = dvLIST.Table.NewRow();
                for (int Coli = 0; Coli <= gridView_SELECT.Columns.Count - 1; Coli++)
                {
                    drx[Coli] = dr[Coli];
                }
                dvLIST.Table.Rows.Add(drx); 
            }
            for (int i = GETROW.Length - 1; i >= 0; i--)
            {
                DataRow drs = gridView_SELECT.GetDataRow(Convert.ToUInt16(GETROW[i]));
                if (drs != null)
                {
                    drs.Delete();
                }
            }
        }

        private void grdCntrl_LIST_DoubleClick(object sender, EventArgs e)
        {
            int[] GETROW = gridView_LIST.GetSelectedRows();
            for (int i = 0; i < GETROW.Length; i++)
            {
                DataRow dr = gridView_LIST.GetDataRow(Convert.ToInt32(GETROW[i])); 
                DataRow drx = dvSELECT.Table.NewRow();  
                for (int Coli = 0; Coli <= gridView_LIST.Columns.Count - 1; Coli++)
                {
                    drx[Coli] = dr[Coli];
                }
                dvSELECT.Table.Rows.Add(drx); 
 
            }
            for (int i = GETROW.Length - 1; i >= 0; i--)
            {
                DataRow drs = gridView_LIST.GetDataRow(Convert.ToInt32(GETROW[i]));
                if (drs != null)
                {
                    drs.Delete();
                }
            }
        }

        private void grdCntrl_SELECT_DoubleClick(object sender, EventArgs e)
        {

            gridView_LIST.FindFilterText = "";
            int[] GETROW = gridView_SELECT.GetSelectedRows();
            for (int i = 0; i < GETROW.Length; i++)
            { 
                DataRow dr = gridView_SELECT.GetDataRow(Convert.ToInt32(GETROW[i]));
                DataRow drx = dvLIST.Table.NewRow();
                for (int Coli = 0; Coli <= gridView_SELECT.Columns.Count - 1; Coli++)
                {
                    drx[Coli] = dr[Coli];
                }
                dvLIST.Table.Rows.Add(drx);
            }
            for (int i = GETROW.Length - 1; i >= 0; i--)
            {
                DataRow drs = gridView_SELECT.GetDataRow(Convert.ToUInt16(GETROW[i]));
                if (drs != null)
                {
                    drs.Delete();
                }
            }
        } 

        private void TXT_DAHIL_ICEREN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        { 

            e.KeyChar = Char.ToUpper(e.KeyChar);

           // e.KeyChar = Char.ToUpper(new CultureInfo("tr-TR", false));

        }

        private void TXT_HARIC_ICEREN_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
        {
          //  string s = e.KeyChar.ToString().Replace("İ", "I").Replace ("Ö","O").Replace ("Ü","U").Replace("Ş","S").Replace ("Ğ","G").Replace("Ç","C").Replace("Ş","S") ;

            e.KeyChar = Char.ToUpper(e.KeyChar); 

        }

        private void TOOGLE_GAZETE_Toggled(object sender, EventArgs e)
        {

        }

        private void TOGGLE_BASLIK_FILITRE_Toggled(object sender, EventArgs e)
        {
            if (TOGGLE_BASLIK_FILITRE.IsOn)                
                Liste(__FIELD_NAME, __GROUP_BY, __QUERY, __TELEVIZYON, __GAZETE, __DERGI, __OUTDOOR, __SINEMA, __RADYO, __INTERNET);
            else
                Liste(__FIELD_NAME, __GROUP_BY, "", __TELEVIZYON, __GAZETE, __DERGI, __OUTDOOR, __SINEMA, __RADYO, __INTERNET);            
        }
    }
}