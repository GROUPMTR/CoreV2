using DevExpress.XtraTreeList;
using DevExpress.XtraTreeList.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace CoreV2.PLANLAMA.DESIGNER.CLASS
{
    public class OZEL_FILITRE
    {   string SQL = "";
        private string TEMIZLE_ELEMAN(string ALAN, int Deger, string TIP)
        {
            if (ALAN != null)
            {
                if (TIP != "")
                {
                    if (ALAN.Length > Deger)
                    {
                        if (ALAN.Substring(ALAN.Length - Deger, Deger) == TIP)
                        {
                            ALAN = ALAN.Substring(0, ALAN.Length - Deger);
                        }
                    }
                }
                else
                {
                    if (ALAN.Length > Deger)
                    {
                        ALAN = ALAN.Substring(0, ALAN.Length - Deger);
                    }
                }
            }
            return ALAN;
        }
        

        public void  _TREE_LIST_READ(TreeListNode node)
        {

            ///
            /// RAPOR PARAMETRELERINI TEMİZLE
            ///  

            //KIRILIM = KIRILIM_CAST = KIRILIM_FIELD = KIRILIM_TABLE_CREATE =
            //OZEL_TANIMLAMA = OZEL_TANIMLAMA_CAST = OZEL_TANIMLAMA_FIELD = OZEL_TABLE_CREATE =
            //BASLIK = BASLIK_CAST = BASLIK_FIELD = BASLIK_TABLE_CREATE =
            //OLCUM = OLCUM_CAST = OLCUM_FIELD = OLCUM_TABLE_CREATE =
            //FILITRE = FILITRE_CAST = FILITRE_FIELD = FILITRE_TABLE_CREATE = string.Empty;

            //COLUMS_COUNT = 0;

            //OZEL_TANIMLAMA = CAST_KIRILIM_TABLE_CREATE = BASLIKLAR = ANA_TEXT_TREE = TABLE_CREATE_FIELD_NAME = TABLE_CREATE_INSERT_QUERY = string.Empty;

            //FIELD_SELECT = FIELD_GROUP_BY = FIELD_SUM =
            //FIELD_TELEVIZYON = FIELD_TELEVIZYON_GROUP_BY = FIELD_TELEVIZYON_SUM =
            //FIELD_GAZETE = FIELD_GAZETE_GROUP_BY = FIELD_GAZETE_SUM =
            //FIELD_DERGI = FIELD_DERGI_GROUP_BY = FIELD_DERGI_SUM =
            //FIELD_SINEMA = FIELD_SINEMA_GROUP_BY = FIELD_SINEMA_SUM =
            //FIELD_RADYO = FIELD_RADYO_GROUP_BY = FIELD_RADYO_SUM =
            //FIELD_OUTDOOR = FIELD_OUTDOOR_GROUP_BY = FIELD_OUTDOOR_SUM =
            //FIELD_INTERNET = FIELD_INTERNET_GROUP_BY = FIELD_INTERNET_SUM = string.Empty;

            //SABITLER_SELECT_NAME = PARENT_NAME = CAST_KIRILIM = CAST_FIELD = STATIC_NAME = STATIC_KIRLIMLAR = string.Empty;
            //QUERY = string.Empty;
            //QUERY_TELEVIZYON = QUERY_GAZETE = QUERY_DERGI = QUERY_SINEMA = QUERY_RADYO = QUERY_OUTDOOR = QUERY_INTERNET = string.Empty;
            //TMP_QUERY_TELEVIZYON = TMP_QUERY_GAZETE = TMP_QUERY_DERGI = TMP_QUERY_SINEMA = TMP_QUERY_RADYO = TMP_QUERY_OUTDOOR = TMP_QUERY_INTERNET = string.Empty;
            //_CHANNEL = string.Empty;

        }

        public string GetFullPath(TreeListNode node, string pathSeparator)
        {
            if (node == null) return "";
            string result = "";
            while (node != null)
            {
                result = pathSeparator + node.GetDisplayText(0) + result;
                node = node.ParentNode;
            }
            return result;
        }

        void selectChildren(TreeListNode parent, ArrayList selectedNodes)
        {
            IEnumerator en = parent.Nodes.GetEnumerator();
            TreeListNode child;
            while (en.MoveNext())
            {
                child = (TreeListNode)en.Current;
                if (child.Checked) selectedNodes.Add(child);
                if (child.HasChildren && child.Checked) selectChildren(child, selectedNodes);
            }
        }

    

        private void OZEL_FILITRE_READ(string TMP_QUERY, TreeList TrList_ANAKIRILIM,TreeList OZEL_FILITRE)
        {
            int  COLUMS_COUNT = 0;   
            if (TrList_ANAKIRILIM.Nodes.Count != 0)
            {
                List<TreeListNode> nds = TrList_ANAKIRILIM.GetAllCheckedNodes();
                foreach (TreeListNode node in nds)
                {
                    if (node.Checked)
                    {
                        string PATH_ = GetFullPath(node, "/");
                        PATH_ = PATH_.Substring(1, PATH_.Length - 1);
                        string[] wordm = PATH_.Split('/');
                        if (COLUMS_COUNT < wordm.Length) COLUMS_COUNT = wordm.Length;
                    }
                }
                if (COLUMS_COUNT > 0)
                {
                    for (int i = 1; i <= COLUMS_COUNT; i++)
                    {
                      //  KIRILIM += string.Format("[KIRILIM_{0}],", i);
                    //    KIRILIM_TABLE_CREATE += string.Format("[KIRILIM_{0}] [nvarchar] (70) NULL ,", i);
                    }
                }
            }
            


            //string SELECT_FIELDS = "", GROUP_BY_FIELDS = "";
            //for (int i = 0; i < DW.Table.Columns.Count; i++)
            //{
            //    using (SqlConnection con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
            //    {
            //        string SQLD = string.Format(" SELECT * FROM   dbo.ADM_SECENEKLER where SECENEK='{0}'", DW.Table.Columns[i].Caption);
            //        SqlCommand myCommand = new SqlCommand(SQLD, con) { CommandText = SQLD.ToString() };
            //        con.Open();
            //        SqlDataReader myReader = myCommand.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
            //        if (myReader.HasRows)
            //        {
            //            while (myReader.Read())
            //            {
            //                SELECT_FIELDS += string.Format("[{0}],", myReader["FIELDS"]);
            //                GROUP_BY_FIELDS += string.Format("[{0}],", myReader["FIELDS"]);
            //            }
            //        }
            //        else
            //        {
            //            SELECT_FIELDS += string.Format(" CAST('' AS nvarchar ) as [{0}],", DW.Table.Columns[i].Caption);
            //        }
            //    }
            //}
            //GROUP_BY_FIELDS = TEMIZLE_ELEMAN(GROUP_BY_FIELDS, 1, ",").ToString();
            //SELECT_FIELDS = TEMIZLE_ELEMAN(SELECT_FIELDS, 1, ",").ToString();


            //if (TOOGLE_TELEVIZYON.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_TELEVIZYON + "[YAYIN_SINIFI]='TELEVIZYON' ", GROUP_BY_FIELDS);
            //}
            //if (TOOGLE_RADYO.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_RADYO + "[YAYIN_SINIFI]='RADYO' ", GROUP_BY_FIELDS);
            //}
            //if (TOOGLE_DERGI.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_DERGI + "[YAYIN_SINIFI]='DERGI' ", GROUP_BY_FIELDS);
            //}
            //if (TOOGLE_GAZETE.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_GAZETE + "[YAYIN_SINIFI]='GAZETE' ", GROUP_BY_FIELDS);
            //}
            //if (TOOGLE_SINEMA.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_SINEMA + "[YAYIN_SINIFI]='SINEMA' ", GROUP_BY_FIELDS);
            //}
            //if (TOOGLE_OUTDOOR.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_OUTDOOR + "[YAYIN_SINIFI]='OUTDOOR' ", GROUP_BY_FIELDS);
            //}
            //if (TOOGLE_INTERNET.IsOn)
            //{
            //    if (SQL != "") SQL += " UNION ALL ";
            //    SQL += string.Format("select {0} from [dbo].[{1}] WHERE {2}  group by {3}", SELECT_FIELDS, "_ADEX_INDEX_DATA", TMP_QUERY_INTERNET + "[YAYIN_SINIFI]='INTERNET' ", GROUP_BY_FIELDS);
            //}



            //TMP_QUERY_TELEVIZYON = TMP_QUERY_GAZETE = TMP_QUERY_DERGI = TMP_QUERY_SINEMA = TMP_QUERY_RADYO = TMP_QUERY_OUTDOOR = TMP_QUERY_INTERNET = "";
            //QUERY_TELEVIZYON = QUERY_GAZETE = QUERY_DERGI = QUERY_SINEMA = QUERY_RADYO = QUERY_OUTDOOR = QUERY_INTERNET = "";
        }



     

    }
}
