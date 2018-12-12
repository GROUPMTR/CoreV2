using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace CoreV2.PLANLAMA.DESIGNER.CLASS
{
    public class DESIGNE_CLASS
    { 
        public   int  _RAPOR_INSERT(  string _RAPOR_KODU, DateTime BAS_TARIHI ,  DateTime BIT_TARIHI ,
                 string TELEVIZYON, string TELEVIZYON_REF, string NONETV, string NONETV_REF, string RADYO, string RADYO_REF, string GAZETE, string GAZETE_REF, string DERGI, string DERGI_REF, string SINEMA, string SINEMA_REF, string OUTDOOR, string OUTDOOR_REF, string INTERNET, string INTERNET_REF, string SEKTOR, string SEKTOR_REF, string PROGRAM, string PROGRAM_REF, string ORANLAR, string ORANLAR_REF,
                 string SABIT_SECENEKLER, string RAPOR_SECENEKLER, string SELECT_MECRATURU, bool CHKBOX_OTUZSN_GRP, string RAPOR_ACIKLAMASI, string MASTER_SELECT, string KEYWORD_SELECT,
                 int KIRILIM_SAY, int BASLIK_SAY, int OLCUM_SAY, int FILITRE_SAY, string ExportFileAdresi,
                 string _KULLANICI_FIRMA, string _KULLANICI_ADI,bool FTP_DURUMU, string FTP_ADRESI, string FTP_USERNAME, string FTP_PASSWORD)
        {
            SqlConnection Con = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            Con.Open();
            int ID = 0;
            using (SqlCommand cmd = new SqlCommand())
            { 
                  cmd.CommandText =   string.Format(" SELECT  * FROM   dbo.ADM_RAPOR_DESIGNE where  SIRKET_KODU='{0}' and  RAPOR_KODU='{1}' and OWNER_MAIL='{2}'", _KULLANICI_FIRMA, _RAPOR_KODU, _KULLANICI_ADI);
                  cmd.Connection = Con; 
                  SqlDataReader myRder = cmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                  if (!myRder.HasRows)
                  {
                      using (SqlConnection cons = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                      {
                          cons.Open();
                          SqlCommand myCmd = new SqlCommand()
                          {
                              CommandText = "INSERT INTO dbo.ADM_RAPOR_DESIGNE(SIRKET_KODU, RAPOR_KODU,BAS_TARIHI,BIT_TARIHI,TELEVIZYON,TELEVIZYON_REF,NONETV,NONETV_REF,RADYO,RADYO_REF,GAZETE,GAZETE_REF,DERGI,DERGI_REF,SINEMA,SINEMA_REF,OUTDOOR,OUTDOOR_REF,INTERNET,INTERNET_REF,SEKTOR,SEKTOR_REF,MECRA_TURLERI ,SABIT_SECENEKLER,RAPOR_SECENEKLER,OTUZ_SN_GRP ,OWNER_MAIL,DURUMU,ACIKLAMA,MASTER_SELECT,KEYWORD_SELECT,KIRILIM_SAY,BASLIK_SAY,OLCUM_SAY,FILITRE_SAY,EXPORT_FILE_ADRES,ERISIM_TARIHI,ERISIM_SAATI,FTP_DURUMU,FTP_ADRESI,FTP_USERNAME,FTP_PASSWORD) " +
                                                            "  VALUES ( @SIRKET_KODU, @RAPOR_KODU ,@BAS_TARIHI,@BIT_TARIHI,@TELEVIZYON,@TELEVIZYON_REF,@NONETV,@NONETV_REF,@RADYO,@RADYO_REF,@GAZETE,@GAZETE_REF,@DERGI,@DERGI_REF,@SINEMA,@SINEMA_REF,@OUTDOOR,@OUTDOOR_REF,@INTERNET,@INTERNET_REF,@SEKTOR,@SEKTOR_REF ,@MECRA_TURLERI,@SABIT_SECENEKLER,@RAPOR_SECENEKLER,@OTUZ_SN_GRP,@OWNER_MAIL,@DURUMU,@ACIKLAMA,@MASTER_SELECT,@KEYWORD_SELECT,@KIRILIM_SAY,@BASLIK_SAY,@OLCUM_SAY,@FILITRE_SAY,@EXPORT_FILE_ADRES ,@ERISIM_TARIHI,@ERISIM_SAATI,@FTP_DURUMU,@FTP_ADRESI,@FTP_USERNAME,@FTP_PASSWORD)  SELECT * FROM  ADM_RAPOR_DESIGNE  WHERE ID=@@IDENTITY "
                          };
                          myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);
                          myCmd.Parameters.AddWithValue("@RAPOR_KODU", _RAPOR_KODU.ToString());
                          myCmd.Parameters.AddWithValue("@BAS_TARIHI",   BAS_TARIHI.ToString("yyyy-MM-dd"));
                          myCmd.Parameters.AddWithValue("@BIT_TARIHI",   BIT_TARIHI.ToString("yyyy-MM-dd"));
                          myCmd.Parameters.AddWithValue("@TELEVIZYON",  TELEVIZYON);
                          myCmd.Parameters.AddWithValue("@TELEVIZYON_REF", TELEVIZYON_REF);
                          myCmd.Parameters.AddWithValue("@NONETV", NONETV);
                          myCmd.Parameters.AddWithValue("@NONETV_REF", NONETV_REF);
                          myCmd.Parameters.AddWithValue("@RADYO",  RADYO);
                          myCmd.Parameters.AddWithValue("@RADYO_REF", RADYO_REF);
                          myCmd.Parameters.AddWithValue("@GAZETE",  GAZETE);
                          myCmd.Parameters.AddWithValue("@GAZETE_REF", GAZETE_REF);
                          myCmd.Parameters.AddWithValue("@DERGI",  DERGI);
                          myCmd.Parameters.AddWithValue("@DERGI_REF", DERGI_REF);
                          myCmd.Parameters.AddWithValue("@SINEMA",   SINEMA);
                          myCmd.Parameters.AddWithValue("@SINEMA_REF", SINEMA_REF);
                          myCmd.Parameters.AddWithValue("@OUTDOOR", OUTDOOR);
                          myCmd.Parameters.AddWithValue("@OUTDOOR_REF", OUTDOOR_REF);
                          myCmd.Parameters.AddWithValue("@INTERNET", INTERNET);
                          myCmd.Parameters.AddWithValue("@INTERNET_REF", INTERNET_REF);
                          myCmd.Parameters.AddWithValue("@SEKTOR",  SEKTOR);
                          myCmd.Parameters.AddWithValue("@SEKTOR_REF", SEKTOR_REF);
                          myCmd.Parameters.AddWithValue("@PROGRAM", PROGRAM);
                          myCmd.Parameters.AddWithValue("@PROGRAM_REF", PROGRAM_REF);
                          myCmd.Parameters.AddWithValue("@ORANLAR", ORANLAR);
                          myCmd.Parameters.AddWithValue("@ORANLAR_REF", ORANLAR_REF);
                          myCmd.Parameters.AddWithValue("@SABIT_SECENEKLER",  SABIT_SECENEKLER);
                          myCmd.Parameters.AddWithValue("@RAPOR_SECENEKLER", RAPOR_SECENEKLER);
                          myCmd.Parameters.AddWithValue("@MECRA_TURLERI", SELECT_MECRATURU);
                          myCmd.Parameters.AddWithValue("@OTUZ_SN_GRP",  CHKBOX_OTUZSN_GRP);
                          myCmd.Parameters.AddWithValue("@ACIKLAMA",   RAPOR_ACIKLAMASI);
                          myCmd.Parameters.AddWithValue("@MASTER_SELECT",  MASTER_SELECT);
                          myCmd.Parameters.AddWithValue("@KEYWORD_SELECT",  KEYWORD_SELECT);
                          myCmd.Parameters.AddWithValue("@OWNER_MAIL",  _KULLANICI_ADI);
                          myCmd.Parameters.AddWithValue("@DURUMU",  "TEMP");
                          myCmd.Parameters.AddWithValue("@KIRILIM_SAY", KIRILIM_SAY);
                          myCmd.Parameters.AddWithValue("@BASLIK_SAY", BASLIK_SAY);
                          myCmd.Parameters.AddWithValue("@OLCUM_SAY", OLCUM_SAY);
                          myCmd.Parameters.AddWithValue("@FILITRE_SAY", FILITRE_SAY);
                          myCmd.Parameters.AddWithValue("@EXPORT_FILE_ADRES", ExportFileAdresi);
                          DateTime ERISIM_TARIHI = DateTime.Now;
                          myCmd.Parameters.AddWithValue("@ERISIM_TARIHI", ERISIM_TARIHI.ToString("yyyy-MM-dd"));
                          myCmd.Parameters.AddWithValue("@ERISIM_SAATI", ERISIM_TARIHI.ToString("hh:mm:ss"));    
                          myCmd.Parameters.AddWithValue("@FTP_DURUMU", FTP_DURUMU);
                          myCmd.Parameters.AddWithValue("@FTP_ADRESI", FTP_ADRESI);
                          myCmd.Parameters.AddWithValue("@FTP_USERNAME", FTP_USERNAME);
                          myCmd.Parameters.AddWithValue("@FTP_PASSWORD", FTP_PASSWORD);
                          myCmd.Connection = cons;
                          SqlDataReader myreader = myCmd.ExecuteReader(System.Data.CommandBehavior.CloseConnection);
                          while (myreader.Read())
                          {
                              ID = Convert.ToInt32(myreader["ID"].ToString());
                          }
                          myreader.Close();
                      }
                  }
              }
               return ID;
        }
        public void _RAPOR_UPDATE(string _RAPOR_KODU, DateTime BAS_TARIHI, DateTime BIT_TARIHI, string TELEVIZYON, string TELEVIZYON_REF, string NONETV, string NONETV_REF, string RADYO, string RADYO_REF, string GAZETE, string GAZETE_REF, string DERGI, string DERGI_REF, string SINEMA, string SINEMA_REF, string OUTDOOR, string OUTDOOR_REF, string INTERNET, string INTERNET_REF, string SEKTOR, string SEKTOR_REF, string PROGRAM, string PROGRAM_REF, string ORANLAR, string ORANLAR_REF,
                                  string SABIT_SECENEKLER, string RAPOR_SECENEKLER, string SELECT_MECRATURU, bool CHKBOX_OTUZSN_GRP, int ID, string MASTER_SELECT, string KEYWORD_SELECT,
                                  int KIRILIM_SAY, int BASLIK_SAY, int OLCUM_SAY, int FILITRE_SAY, string _KULLANICI_FIRMA, string _KULLANICI_ADI, string ExportFileAdresi, bool FTP_DURUMU, string FTP_ADRESI, string FTP_USERNAME, string FTP_PASSWORD)
        {  
                    using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                    {
                        conn.Open();
                        SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.ADM_RAPOR_DESIGNE  set KIRILIM_SAY=@KIRILIM_SAY,BASLIK_SAY=@BASLIK_SAY,OLCUM_SAY=@OLCUM_SAY ,FILITRE_SAY=@FILITRE_SAY, BAS_TARIHI=@BAS_TARIHI,BIT_TARIHI=@BIT_TARIHI, TELEVIZYON=@TELEVIZYON, TELEVIZYON_REF= @TELEVIZYON_REF, NONETV=@NONETV, NONETV_REF=@NONETV_REF,  RADYO=@RADYO,   RADYO_REF=@RADYO_REF,  GAZETE =@GAZETE, GAZETE_REF=@GAZETE_REF, DERGI=@DERGI,   DERGI_REF=@DERGI_REF, SINEMA=@SINEMA, SINEMA_REF=@SINEMA_REF,  OUTDOOR=@OUTDOOR,    OUTDOOR_REF=@OUTDOOR_REF,INTERNET=@INTERNET,INTERNET_REF=@INTERNET_REF,SEKTOR=@SEKTOR,SEKTOR_REF=@SEKTOR_REF,PROGRAM=@PROGRAM,PROGRAM_REF=@PROGRAM_REF,ORANLAR=@ORANLAR,    ORANLAR_REF=@ORANLAR_REF,  SABIT_SECENEKLER=@SABIT_SECENEKLER,RAPOR_SECENEKLER=@RAPOR_SECENEKLER ,MECRA_TURLERI=@MECRA_TURLERI ,OTUZ_SN_GRP=@OTUZ_SN_GRP,MASTER_SELECT=@MASTER_SELECT,KEYWORD_SELECT=@KEYWORD_SELECT,EXPORT_FILE_ADRES=@EXPORT_FILE_ADRES,ERISIM_TARIHI=@ERISIM_TARIHI,ERISIM_SAATI=@ERISIM_SAATI,FTP_DURUMU=@FTP_DURUMU,FTP_ADRESI=@FTP_ADRESI,FTP_USERNAME=@FTP_USERNAME,FTP_PASSWORD=@FTP_PASSWORD   WHERE SIRKET_KODU=@SIRKET_KODU AND  RAPOR_KODU=@RAPOR_KODU and ID=@ID " };
                        myCmd.Parameters.AddWithValue("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = ID;
                        myCmd.Parameters.AddWithValue("@SIRKET_KODU", _KULLANICI_FIRMA);
                                myCmd.Parameters.AddWithValue("@RAPOR_KODU",  _RAPOR_KODU.ToString());
                                myCmd.Parameters.AddWithValue("@BAS_TARIHI",  BAS_TARIHI.ToString("yyyy-MM-dd"));
                                myCmd.Parameters.AddWithValue("@BIT_TARIHI",  BIT_TARIHI.ToString("yyyy-MM-dd"));
                                myCmd.Parameters.AddWithValue("@TELEVIZYON", TELEVIZYON);
                                myCmd.Parameters.AddWithValue("@TELEVIZYON_REF", TELEVIZYON_REF);
                                myCmd.Parameters.AddWithValue("@NONETV", NONETV);
                                myCmd.Parameters.AddWithValue("@NONETV_REF", NONETV_REF);
                                myCmd.Parameters.AddWithValue("@RADYO", RADYO);
                                myCmd.Parameters.AddWithValue("@RADYO_REF", RADYO_REF);
                                myCmd.Parameters.AddWithValue("@GAZETE", GAZETE);
                                myCmd.Parameters.AddWithValue("@GAZETE_REF", GAZETE_REF);
                                myCmd.Parameters.AddWithValue("@DERGI", DERGI);
                                myCmd.Parameters.AddWithValue("@DERGI_REF", DERGI_REF);
                                myCmd.Parameters.AddWithValue("@SINEMA", SINEMA);
                                myCmd.Parameters.AddWithValue("@SINEMA_REF", SINEMA_REF);
                                myCmd.Parameters.AddWithValue("@OUTDOOR", OUTDOOR);
                                myCmd.Parameters.AddWithValue("@OUTDOOR_REF", OUTDOOR_REF);
                                myCmd.Parameters.AddWithValue("@INTERNET", INTERNET);
                                myCmd.Parameters.AddWithValue("@INTERNET_REF", INTERNET_REF);
                                myCmd.Parameters.AddWithValue("@SEKTOR", SEKTOR);
                                myCmd.Parameters.AddWithValue("@SEKTOR_REF", SEKTOR_REF);
                                myCmd.Parameters.AddWithValue("@PROGRAM", PROGRAM);
                                myCmd.Parameters.AddWithValue("@PROGRAM_REF", PROGRAM_REF);
                                myCmd.Parameters.AddWithValue("@ORANLAR", ORANLAR);
                                myCmd.Parameters.AddWithValue("@ORANLAR_REF", ORANLAR_REF);
                                myCmd.Parameters.AddWithValue("@SABIT_SECENEKLER",   SABIT_SECENEKLER);
                                myCmd.Parameters.AddWithValue("@RAPOR_SECENEKLER",   RAPOR_SECENEKLER);
                                myCmd.Parameters.AddWithValue("@MASTER_SELECT",   MASTER_SELECT);
                                myCmd.Parameters.AddWithValue("@MECRA_TURLERI",   SELECT_MECRATURU);
                                myCmd.Parameters.AddWithValue("@OTUZ_SN_GRP",   CHKBOX_OTUZSN_GRP);
                                myCmd.Parameters.AddWithValue("@KEYWORD_SELECT",   KEYWORD_SELECT);
                                myCmd.Parameters.AddWithValue("@KIRILIM_SAY", KIRILIM_SAY); 
                                myCmd.Parameters.AddWithValue("@BASLIK_SAY", BASLIK_SAY);
                                myCmd.Parameters.AddWithValue("@OLCUM_SAY", OLCUM_SAY);
                                myCmd.Parameters.AddWithValue("@FILITRE_SAY", FILITRE_SAY);
                                myCmd.Parameters.AddWithValue("@EXPORT_FILE_ADRES", ExportFileAdresi);
                                DateTime ERISIM_TARIHI = DateTime.Now; 
                                myCmd.Parameters.AddWithValue("@ERISIM_TARIHI", ERISIM_TARIHI.ToString("yyyy-MM-dd"));
                                myCmd.Parameters.AddWithValue("@ERISIM_SAATI", ERISIM_TARIHI.ToString("hh:mm:ss")); 
                                myCmd.Parameters.AddWithValue("@FTP_DURUMU", FTP_DURUMU);
                                myCmd.Parameters.AddWithValue("@FTP_ADRESI", FTP_ADRESI);
                                myCmd.Parameters.AddWithValue("@FTP_USERNAME", FTP_USERNAME);
                                myCmd.Parameters.AddWithValue("@FTP_PASSWORD", FTP_PASSWORD);

                myCmd.Connection = conn;
                        myCmd.ExecuteNonQuery();
                    } 
        } 
         string ClipboardData
         {
             get
             {
                 IDataObject iData = Clipboard.GetDataObject();
                 if (iData == null) return "";

                 if (iData.GetDataPresent(DataFormats.Text))
                     return (string)iData.GetData(DataFormats.Text);
                 return "";
             }
             set { Clipboard.SetDataObject(value); }
         }
         DataTable tbl;
         private void CLIPBOARD_DATA_READ()
         {
             tbl = null;
             tbl = new DataTable();
             tbl.TableName = "ImportedTable";
             List<string> data = new List<string>(ClipboardData.Split('\n'));
             bool firstRow = true;

             if (data.Count > 0 && string.IsNullOrWhiteSpace(data[data.Count - 1]))
             {
                 data.RemoveAt(data.Count - 1);
             }

             foreach (string iterationRow in data)
             {
                 string row = iterationRow;
                 if (row.EndsWith("\r"))
                 {
                     row = row.Substring(0, row.Length - "\r".Length);
                 }

                 string[] rowData = row.Split(new char[] { '\r', '\x09' });
                 DataRow newRow = tbl.NewRow();
                 if (firstRow)
                 {
                     int colNumber = 0;
                     foreach (string value in rowData)
                     {
                         if (string.IsNullOrWhiteSpace(value))
                         {
                             tbl.Columns.Add(string.Format("[BLANK{0}]", colNumber));
                         }
                         else if (!tbl.Columns.Contains(value))
                         {
                             tbl.Columns.Add(value);
                         }
                         else
                         {
                             tbl.Columns.Add(string.Format("Column {0}", colNumber));
                         }
                         colNumber++;
                     }
                     firstRow = false;
                 }
                 else
                 {
                     for (int i = 0; i < rowData.Length; i++)
                     {
                         if (i >= tbl.Columns.Count) break;
                         newRow[i] = rowData[i];
                     }
                     tbl.Rows.Add(newRow);
                 }
             }

         }

         public void TARIFEYI_YAPISTIR(string TARIFE_TURU,DataView DW_GENERIC,DateTime dtBAS_TARIHI,DateTime dtBIT_TARIHI )
         {

             if (TARIFE_TURU == "Televizyon")
             {
                
                     CLIPBOARD_DATA_READ();
                     for (int x = 0; x <= tbl.Rows.Count - 1; x++)
                     {
                         DataRow rowm = tbl.Rows[x];
                         DataRow rows = DW_GENERIC.Table.NewRow();

                         for (int XZ = 0; XZ <= tbl.Columns.Count - 1; XZ++)
                         {
                             switch (tbl.Columns[XZ].ColumnName)
                             {
                                 case "KANAL":
                                     if (rowm["KANAL"].ToString() != string.Empty) rows["MECRA_KODU"] = rowm["KANAL"];
                                     break;
                                case "Spot Tipi":
                                    if (rowm["Spot Tipi"].ToString() != string.Empty) rows["SPOT_TIPI_DETAY"] = rowm["Spot Tipi"];
                                    break;
                                case "Hesap Türü":
                                        if (rowm["Hesap Türü"].ToString() != string.Empty)  rows["HESAPLANMA_TURU"] = rowm["Hesap Türü"].ToString().Replace("Sure","Süre").Replace("SURE","Süre") ;                                        
                                     break;
                                 case "Birim Fiyat / Faktör":
                                     if (rowm["Birim Fiyat / Faktör"].ToString() != string.Empty) rows["BIRIM_FIYAT"] = rowm["Birim Fiyat / Faktör"];
                                     break;
                                 case "Cpp":
                                     if (rowm["Cpp"].ToString() != string.Empty) rows["TARGET"] = rowm["Cpp"];
                                     break; 
                                case "Başlangıç Saati":
                                        if (rowm["Başlangıç Saati"].ToString() != string.Empty)
                                        {
                                            if (rowm["Başlangıç Saati"].ToString().Length > 5)
                                                rows["BASLANGIC_SAATI"] = rowm["Başlangıç Saati"].ToString().Substring(0,5);
                                            else
                                                rows["BASLANGIC_SAATI"] = rowm["Başlangıç Saati"];
                                        }
                                break;
                                 case "Bitiş Saati":
                                     if (rowm["Bitiş Saati"].ToString() != string.Empty)
                                    {
                                        if (rowm["Bitiş Saati"].ToString().Length > 5)
                                            rows["BITIS_SAATI"] = rowm["Bitiş Saati"].ToString().Substring(0, 5);
                                        else
                                            rows["BITIS_SAATI"] = rowm["Bitiş Saati"];
                                    }
                                break;
                                 case "Opt/Pt":
                                     if (rowm["Opt/Pt"].ToString() != string.Empty) rows["OPT_PT"] = rowm["Opt/Pt"];
                                     break;
                                 case "Başlangıç Tarihi":
                                     if (rowm["Başlangıç Tarihi"].ToString() != string.Empty) rows["BASLANGIC_TARIHI"] = rowm["Başlangıç Tarihi"]; else rows["BASLANGIC_TARIHI"] = dtBAS_TARIHI;
                                     break;
                                 case "Bitiş Tarihi":
                                     if (rowm["Bitiş Tarihi"].ToString() != string.Empty) rows["BITIS_TARIHI"] = rowm["Bitiş Tarihi"]; else rows["BITIS_TARIHI"] = dtBIT_TARIHI;
                                     break;
                             }
                         }
                         DW_GENERIC.Table.Rows.Add(rows);
                     }  
             } 
             if (TARIFE_TURU == "None Tv Grp")
             { 
                     CLIPBOARD_DATA_READ();
                     for (int x = 0; x <= tbl.Rows.Count - 1; x++)
                     {
                         DataRow rowm = tbl.Rows[x];
                         DataRow rows = DW_GENERIC.Table.NewRow();

                         for (int XZ = 0; XZ <= tbl.Columns.Count - 1; XZ++)
                         {
                             switch (tbl.Columns[XZ].ColumnName)
                             {
                                 case "KANAL":
                                     if (rowm["KANAL"].ToString() != string.Empty) rows["MECRA_KODU"] = rowm["KANAL"];
                                     break;
                                 case "Hesap Türü":
                                     if (rowm["Hesap Türü"].ToString() != string.Empty) rows["HESAPLANMA_TURU"] = rowm["Hesap Türü"];
                                     break;
                                 case "Birim Fiyat / Faktör":
                                     if (rowm["Birim Fiyat / Faktör"].ToString() != string.Empty) rows["BIRIM_FIYAT"] = rowm["Birim Fiyat / Faktör"];
                                     break;
                                 case "Cpp":
                                     if (rowm["Cpp"].ToString() != string.Empty) rows["TARGET"] = rowm["Cpp"];
                                     break;
                                 case "GRP":
                                     if (rowm["GRP"].ToString() != string.Empty) rows["GRP"] = rowm["GRP"];
                                     break;
                                 case "Başlangıç Saati":
                                //  if (rowm["Başlangıç Saati"].ToString() != string.Empty) rows["BASLANGIC_SAATI"] = rowm["Başlangıç Saati"];
                                        if (rowm["Başlangıç Saati"].ToString() != string.Empty)
                                        {
                                            if (rowm["Başlangıç Saati"].ToString().Length > 5)
                                                rows["BASLANGIC_SAATI"] = rowm["Başlangıç Saati"].ToString().Substring(0, 5);
                                            else
                                                rows["BASLANGIC_SAATI"] = rowm["Başlangıç Saati"];
                                        }
                                break;
                                 case "Bitiş Saati":
                                     //if (rowm["Bitiş Saati"].ToString() != string.Empty) rows["BITIS_SAATI"] = rowm["Bitiş Saati"];

                                        if (rowm["Bitiş Saati"].ToString() != string.Empty)
                                        {
                                            if (rowm["Bitiş Saati"].ToString().Length > 5)
                                                rows["BITIS_SAATI"] = rowm["Bitiş Saati"].ToString().Substring(0, 5);
                                            else
                                                rows["BITIS_SAATI"] = rowm["Bitiş Saati"];
                                        }

                                break;
                                 case "Opt/Pt":
                                     if (rowm["Opt/Pt"].ToString() != string.Empty) rows["OPT_PT"] = rowm["Opt/Pt"];
                                     break;
                                 case "Başlangıç Tarihi":
                                     if (rowm["Başlangıç Tarihi"].ToString() != string.Empty) rows["BASLANGIC_TARIHI"] = rowm["Başlangıç Tarihi"]; else rows["BASLANGIC_TARIHI"] = dtBAS_TARIHI;
                                     break;
                                 case "Bitiş Tarihi":
                                     if (rowm["Bitiş Tarihi"].ToString() != string.Empty) rows["BITIS_TARIHI"] = rowm["Bitiş Tarihi"]; else rows["BITIS_TARIHI"] = dtBIT_TARIHI;
                                     break;
                             }
                         }
                         DW_GENERIC.Table.Rows.Add(rows);
                     } 
             } 
             if (TARIFE_TURU == "Radyo")
             { 
                     CLIPBOARD_DATA_READ();
                     for (int x = 0; x <= tbl.Rows.Count - 1; x++)
                     {
                         DataRow rowm = tbl.Rows[x];
                         DataRow rows = DW_GENERIC.Table.NewRow();

                         for (int XZ = 0; XZ <= tbl.Columns.Count - 1; XZ++)
                         {
                             switch (tbl.Columns[XZ].ColumnName)
                             {
                                 case "KANAL":
                                     if (rowm["KANAL"].ToString() != string.Empty) rows["MECRA_KODU"] = rowm["KANAL"];
                                     break;
                                 case "Hesap Türü":
                                     if (rowm["Hesap Türü"].ToString() != string.Empty) rows["HESAPLANMA_TURU"] = rowm["Hesap Türü"].ToString().Replace("Sure", "Süre").Replace("SURE", "Süre");  
                                     break;
                                 case "Birim Fiyat / Faktör":
                                     if (rowm["Birim Fiyat / Faktör"].ToString() != string.Empty) rows["BIRIM_FIYAT"] = rowm["Birim Fiyat / Faktör"];
                                     break;
                                 case "Başlangıç Saati":                                 
                                    if (rowm["Başlangıç Saati"].ToString() != string.Empty)
                                    {
                                        if (rowm["Başlangıç Saati"].ToString().Length > 5)
                                        {
                                            string[] Onesz = rowm["Başlangıç Saati"].ToString().Split(':');
                                            if(Onesz[0].ToString().Length==1)
                                                rows["BASLANGIC_SAATI"] = "0"+Onesz[0].ToString() +":"+ Onesz[1].ToString();
                                                else
                                                rows["BASLANGIC_SAATI"] =   Onesz[0].ToString() +":"+ Onesz[1].ToString(); 
                                        }
                                        else
                                            rows["BASLANGIC_SAATI"] = rowm["Başlangıç Saati"];
                                    }
                                break;
                                 case "Bitiş Saati":                                
                                    if (rowm["Bitiş Saati"].ToString() != string.Empty)
                                    { 
                                        if (rowm["Bitiş Saati"].ToString().Length > 5)
                                        {
                                            string[] Onesz = rowm["Bitiş Saati"].ToString().Split(':');
                                            if (Onesz[0].ToString().Length == 1)
                                                rows["BITIS_SAATI"] = "0" + Onesz[0].ToString() + ":" + Onesz[1].ToString();
                                            else
                                                rows["BITIS_SAATI"] = Onesz[0].ToString() + ":" + Onesz[1].ToString();
                                        } 
                                        else
                                            rows["BITIS_SAATI"] = rowm["Bitiş Saati"];
                                    }
                                break;
                                 case "Opt/Pt":
                                     if (rowm["Opt/Pt"].ToString() != string.Empty) rows["OPT_PT"] = rowm["Opt/Pt"];
                                     break;
                                 case "Başlangıç Tarihi":
                                     if (rowm["Başlangıç Tarihi"].ToString() != string.Empty) rows["BASLANGIC_TARIHI"] = rowm["Başlangıç Tarihi"]; else rows["BASLANGIC_TARIHI"] = dtBAS_TARIHI;
                                     break;
                                 case "Bitiş Tarihi":
                                     if (rowm["Bitiş Tarihi"].ToString() != string.Empty) rows["BITIS_TARIHI"] = rowm["Bitiş Tarihi"]; else rows["BITIS_TARIHI"] = dtBIT_TARIHI;
                                     break;
                             }
                         }
                         DW_GENERIC.Table.Rows.Add(rows);
                     }
          
             }
             if (TARIFE_TURU == "Gazete")
             { 
                     CLIPBOARD_DATA_READ(); 
                     for (int x = 0; x <= tbl.Rows.Count - 1; x++)
                     {
                         DataRow rowm = tbl.Rows[x];
                         DataRow rows = DW_GENERIC.Table.NewRow();

                         for (int XZ = 0; XZ <= tbl.Columns.Count - 1; XZ++)
                         {
                             switch (tbl.Columns[XZ].ColumnName)
                             {
                                 case "Mecra Kodu":
                                     if (rowm["Mecra Kodu"].ToString() != string.Empty) rows["MECRA_KODU"] = rowm["Mecra Kodu"];
                                     break;
                                 case "Sayfa Grubu":
                                     if (rowm["Sayfa Grubu"].ToString() != string.Empty) rows["SAYFA_GRUBU"] = rowm["Sayfa Grubu"];
                                     break;
                                 case "Yayın Türü":
                                     if (rowm["Yayın Türü"].ToString() != string.Empty) rows["YAYIN_TURU"] = rowm["Yayın Türü"];
                                     break;
                                 case "Hesap Türü":
                                     if (rowm["Hesap Türü"].ToString() != string.Empty) rows["HESAPLANMA_TURU"] = rowm["Hesap Türü"];
                                     break;
                                 case "Birim Fiyat":
                                     if (rowm["Birim Fiyat"].ToString() != string.Empty) rows["BIRIM_FIYAT"] = rowm["Birim Fiyat"];
                                     break;
                                 //case "Pzt":
                                 //    if (rowm["Pzt"].ToString() != string.Empty) rows["PAZARTESI"] = rowm["Pzt"];
                                 //    break;
                                 //case "Sal":
                                 //    if (rowm["Sal"].ToString() != string.Empty) rows["SALI"] = rowm["Sal"];
                                 //    break;
                                 //case "Car":
                                 //    if (rowm["Car"].ToString() != string.Empty) rows["CARSAMBA"] = rowm["Car"];
                                 //    break;
                                 //case "Per":
                                 //    if (rowm["Per"].ToString() != string.Empty) rows["PERSEMBE"] = rowm["Per"];
                                 //    break;
                                 //case "Cum":
                                 //    if (rowm["Cum"].ToString() != string.Empty) rows["CUMA"] = rowm["Cum"];
                                 //    break;
                                 case "Cmt":
                                     if (rowm["Cmt"].ToString() != string.Empty) rows["CUMARTESI"] = rowm["Cmt"];
                                     break;
                                 case "Paz":
                                     if (rowm["Paz"].ToString() != string.Empty) rows["PAZAR"] = rowm["Paz"];
                                     break;
                                 case "Başlangıç Tarihi":
                                     if (rowm["Başlangıç Tarihi"].ToString() != string.Empty) rows["BASLANGIC_TARIHI"] = rowm["Başlangıç Tarihi"]; else rows["BASLANGIC_TARIHI"] = dtBAS_TARIHI;
                                     break;
                                 case "Bitiş Tarihi":
                                     if (rowm["Bitiş Tarihi"].ToString() != string.Empty) rows["BITIS_TARIHI"] = rowm["Bitiş Tarihi"]; else rows["BITIS_TARIHI"] = dtBIT_TARIHI;
                                     break;
                             }
                         }
                         DW_GENERIC.Table.Rows.Add(rows);
                     }
              
             }

             if (TARIFE_TURU == "Dergi")
             { 
                     CLIPBOARD_DATA_READ();
                     for (int x = 0; x <= tbl.Rows.Count - 1; x++)
                     {
                         DataRow rowm = tbl.Rows[x];
                         DataRow rows = DW_GENERIC.Table.NewRow();

                         for (int XZ = 0; XZ <= tbl.Columns.Count - 1; XZ++)
                         {
                             switch (tbl.Columns[XZ].ColumnName.Trim())
                             {
                                 case "MECRA":
                                     if (rowm["MECRA"].ToString() != string.Empty) rows["MECRA_KODU"] = rowm["MECRA"];
                                     break;
                                 case "Sayfa Grubu":
                                     if (rowm["Sayfa Grubu"].ToString() != string.Empty) rows["SAYFA_GRUBU"] = rowm["Sayfa Grubu"];
                                     break;
                                 case "Hesap Türü":
                                     if (rowm["Hesap Türü"].ToString() != string.Empty) rows["HESAPLANMA_TURU"] = rowm["Hesap Türü"];
                                     break;
                                 case "Birim Fiyat / Faktör":
                                     if (rowm[tbl.Columns[XZ].ColumnName].ToString() != string.Empty) rows["BIRIM_FIYAT"] = rowm[tbl.Columns[XZ].ColumnName];
                                     break;
                                 case "Başlangıç Tarihi":
                                     if (rowm["Başlangıç Tarihi"].ToString() != string.Empty) rows["BASLANGIC_TARIHI"] = rowm["Başlangıç Tarihi"]; else rows["BASLANGIC_TARIHI"] = dtBAS_TARIHI;
                                     break;
                                 case "Bitiş Tarihi":
                                     if (rowm["Bitiş Tarihi"].ToString() != string.Empty) rows["BITIS_TARIHI"] = rowm["Bitiş Tarihi"]; else rows["BITIS_TARIHI"] = dtBIT_TARIHI;
                                     break;
                             }
                         }
                         DW_GENERIC.Table.Rows.Add(rows);
                     } 
             }

             if (TARIFE_TURU == "Outdoor")
             { 
                     CLIPBOARD_DATA_READ();
                     for (int x = 0; x <= tbl.Rows.Count - 1; x++)
                     {
                         DataRow rowm = tbl.Rows[x];
                         DataRow rows = DW_GENERIC.Table.NewRow();

                         for (int XZ = 0; XZ <= tbl.Columns.Count - 1; XZ++)
                         {
                             switch (tbl.Columns[XZ].ColumnName)
                             {
                                 case "MECRA":
                                     if (rowm["MECRA"].ToString() != string.Empty) rows["MECRA_KODU"] = rowm["MECRA"];
                                     break;
                                 case "İLİ":
                                     if (rowm["İLİ"].ToString() != string.Empty) rows["ILI"] = rowm["İLİ"];
                                     break;
                                 case "ÜNİTE":
                                     if (rowm["ÜNİTE"].ToString() != string.Empty) rows["UNITE"] = rowm["ÜNİTE"];
                                     break;
                                 case "Hesap Türü":
                                     if (rowm["Hesap Türü"].ToString() != string.Empty) rows["HESAPLANMA_TURU"] = rowm["Hesap Türü"];
                                     break;
                                 case "Birim Fiyat / Faktör":
                                     if (rowm["Birim Fiyat / Faktör"].ToString() != string.Empty) rows["BIRIM_FIYAT"] = rowm["Birim Fiyat / Faktör"];
                                     break;
                                 case "Başlangıç Tarihi":
                                     if (rowm["Başlangıç Tarihi"].ToString() != string.Empty) rows["BASLANGIC_TARIHI"] = rowm["Başlangıç Tarihi"]; else rows["BASLANGIC_TARIHI"] = dtBAS_TARIHI;
                                     break;
                                 case "Bitiş Tarihi":
                                     if (rowm["Bitiş Tarihi"].ToString() != string.Empty) rows["BITIS_TARIHI"] = rowm["Bitiş Tarihi"]; else rows["BITIS_TARIHI"] = dtBIT_TARIHI;
                                     break;
                             }
                         }
                         DW_GENERIC.Table.Rows.Add(rows);
                     }
            
             }
             if (TARIFE_TURU == "Sinema")
             { 
                     CLIPBOARD_DATA_READ();
                     for (int x = 0; x <= tbl.Rows.Count - 1; x++)
                     {
                         DataRow rowm = tbl.Rows[x];
                         DataRow rows = DW_GENERIC.Table.NewRow();

                         for (int XZ = 0; XZ <= tbl.Columns.Count - 1; XZ++)
                         {
                             switch (tbl.Columns[XZ].ColumnName)
                             {
                                 case "MECRA":
                                     if (rowm["MECRA"].ToString() != string.Empty) rows["MECRA_KODU"] = rowm["MECRA"];
                                     break;
                                 case "İLİ":
                                     if (rowm["İLİ"].ToString() != string.Empty) rows["ILI"] = rowm["İLİ"];
                                     break;
                                 case "BÖLGE":
                                     if (rowm["BÖLGE"].ToString() != string.Empty) rows["BOLGE"] = rowm["BÖLGE"];
                                     break;
                                 case "Hesap Türü":
                                     if (rowm["Hesap Türü"].ToString() != string.Empty) rows["HESAPLANMA_TURU"] = rowm["Hesap Türü"];
                                     break;
                                 case "Birim Fiyat / Faktör":
                                     if (rowm["Birim Fiyat / Faktör"].ToString() != string.Empty) rows["BIRIM_FIYAT"] = rowm["Birim Fiyat / Faktör"];
                                     break;
                                 case "Başlangıç Tarihi":
                                     if (rowm["Başlangıç Tarihi"].ToString() != string.Empty) rows["BASLANGIC_TARIHI"] = rowm["Başlangıç Tarihi"]; else rows["BASLANGIC_TARIHI"] = dtBAS_TARIHI;
                                     break;
                                 case "Bitiş Tarihi":
                                     if (rowm["Bitiş Tarihi"].ToString() != string.Empty) rows["BITIS_TARIHI"] = rowm["Bitiş Tarihi"]; else rows["BITIS_TARIHI"] = dtBIT_TARIHI;
                                     break;
                             }
                         }
                         DW_GENERIC.Table.Rows.Add(rows);
                     }
               
             }
             if (TARIFE_TURU == "Internet")
             { 
                     CLIPBOARD_DATA_READ();
                     for (int x = 0; x <= tbl.Rows.Count - 1; x++)
                     {
                         DataRow rowm = tbl.Rows[x];
                         DataRow rows = DW_GENERIC.Table.NewRow();

                         for (int XZ = 0; XZ <= tbl.Columns.Count - 1; XZ++)
                         {
                             switch (tbl.Columns[XZ].ColumnName)
                             {
                                 case "Network":
                                     if (rowm["Network"].ToString() != string.Empty) rows["R_NETWORK"] = rowm["Network"];
                                     break;
                                 case "Site":
                                     if (rowm["Site"].ToString() != string.Empty) rows["SITE"] = rowm["Site"];
                                     break;
                                 case "Hesap Türü":
                                     if (rowm["Hesap Türü"].ToString() != string.Empty) rows["HESAPLANMA_TURU"] = rowm["Hesap Türü"];
                                     break;
                                 case "Birim Fiyat / Faktör":
                                     if (rowm["Birim Fiyat / Faktör"].ToString() != string.Empty) rows["BIRIM_FIYAT"] = rowm["Birim Fiyat / Faktör"];
                                     break;
                                 case "Başlangıç Tarihi":
                                     if (rowm["Başlangıç Tarihi"].ToString() != string.Empty) rows["BASLANGIC_TARIHI"] = rowm["Başlangıç Tarihi"]; else rows["BASLANGIC_TARIHI"] = dtBAS_TARIHI;
                                     break;
                                 case "Bitiş Tarihi":
                                     if (rowm["Bitiş Tarihi"].ToString() != string.Empty) rows["BITIS_TARIHI"] = rowm["Bitiş Tarihi"]; else rows["BITIS_TARIHI"] = dtBIT_TARIHI;
                                     break;
                             }
                         }
                         DW_GENERIC.Table.Rows.Add(rows);
                     } 
             }


            if (TARIFE_TURU == "Program")
            { 
                CLIPBOARD_DATA_READ();
                for (int x = 0; x <= tbl.Rows.Count - 1; x++)
                {
                    DataRow rowm = tbl.Rows[x];
                    DataRow rows = DW_GENERIC.Table.NewRow();

                    for (int XZ = 0; XZ <= tbl.Columns.Count - 1; XZ++)
                    {
                        switch (tbl.Columns[XZ].ColumnName)
                        {
                            case "KANAL":
                                if (rowm["KANAL"].ToString() != string.Empty) rows["MECRA_KODU"] = rowm["KANAL"];
                                break;

                            case "PROGRAM":
                                if (rowm["PROGRAM"].ToString() != string.Empty) rows["PROGRAM"] = rowm["PROGRAM"];
                                break;

                            case "PG.ÖZEL":
                                if (rowm["PG_OZEL"].ToString() != string.Empty) rows["PG_OZEL"] = rowm["PG.ÖZEL"];
                                break;

                            case "PG_OZEL":
                                if (rowm["PG_OZEL"].ToString() != string.Empty) rows["PG_OZEL"] = rowm["PG_OZEL"];
                                break;

                            case "TIPOLOJI":
                                if (rowm["TIPOLOJI"].ToString() != string.Empty) rows["TIPOLOJI"] = rowm["TIPOLOJI"];
                                break; 
                            case "Hesap Türü":
                                if (rowm["Hesap Türü"].ToString() != string.Empty) rows["HESAPLANMA_TURU"] = rowm["Hesap Türü"].ToString().Replace("Sure", "Süre").Replace("SURE", "Süre");
                                break;
                            case "Birim Fiyat / Faktör":
                                if (rowm["Birim Fiyat / Faktör"].ToString() != string.Empty) rows["BIRIM_FIYAT"] = rowm["Birim Fiyat / Faktör"];
                                break;
                            case "Cpp":
                                if (rowm["Cpp"].ToString() != string.Empty) rows["TARGET"] = rowm["Cpp"];
                                break;
                            case "Başlangıç Saati":
                                if (rowm["Başlangıç Saati"].ToString() != string.Empty)
                                {
                                    if (rowm["Başlangıç Saati"].ToString().Length > 5)
                                        rows["BASLANGIC_SAATI"] = rowm["Başlangıç Saati"].ToString().Substring(0, 5);
                                    else
                                        rows["BASLANGIC_SAATI"] = rowm["Başlangıç Saati"];
                                }
                                break;
                            case "Bitiş Saati":
                                if (rowm["Bitiş Saati"].ToString() != string.Empty)
                                {
                                    if (rowm["Bitiş Saati"].ToString().Length > 5)
                                        rows["BITIS_SAATI"] = rowm["Bitiş Saati"].ToString().Substring(0, 5);
                                    else
                                        rows["BITIS_SAATI"] = rowm["Bitiş Saati"];
                                }
                                break;
                            case "Opt/Pt":
                                if (rowm["Opt/Pt"].ToString() != string.Empty) rows["OPT_PT"] = rowm["Opt/Pt"];
                                break;
                            case "Başlangıç Tarihi":
                                if (rowm["Başlangıç Tarihi"].ToString() != string.Empty) rows["BASLANGIC_TARIHI"] = rowm["Başlangıç Tarihi"]; else rows["BASLANGIC_TARIHI"] = dtBAS_TARIHI;
                                break;
                            case "Bitiş Tarihi":
                                if (rowm["Bitiş Tarihi"].ToString() != string.Empty) rows["BITIS_TARIHI"] = rowm["Bitiş Tarihi"]; else rows["BITIS_TARIHI"] = dtBIT_TARIHI;
                                break;
                        }
                    }
                    DW_GENERIC.Table.Rows.Add(rows);
                }  
            }
        } 

         public void MASTER_YAPISTIR(DataView DW_GENERIC, DevExpress.XtraGrid.Views.Grid.GridView GW)
         {
             CLIPBOARD_DATA_READ();
             for (int x = 0; x <= tbl.Rows.Count - 1; x++)
             {
                 DataRow rowm = tbl.Rows[x];
                 DataRow rows = DW_GENERIC.Table.NewRow();
           
                 for (int XZ = 0; XZ <= tbl.Columns.Count+1 ; XZ++)
                 {
                    if (  XZ <= GW.Columns.Count)
                    {
                        if (GW.Columns[XZ].FieldName != "NEW" && GW.Columns[XZ].FieldName != "ID" && GW.Columns[XZ].FieldName != "GUID")
                        {
                            rows[GW.Columns[XZ].FieldName.ToString()] = rowm[GW.Columns[XZ].Caption.ToString()];
                        }
                    }
                }
                 DW_GENERIC.Table.Rows.Add(rows);
             } 
         }
        

    }
}
