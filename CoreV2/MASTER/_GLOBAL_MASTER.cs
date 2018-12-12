using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Windows.Forms;

namespace CoreV2.MASTER
{
   public  class _GLOBAL_MASTER
    {

        public void KAYDET(string FILE_NAME,string MECRA_TURU)
        {
            DateTime myDTStart = Convert.ToDateTime(DateTime.Now);
            SqlConnection myConnectionKontrol = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()); 
            SqlCommand myCommandKontrol = new SqlCommand(" select * from dbo.TRF_TARIFELER_LISTESI Where  TARIFE_TURU='MASTER' AND  SIRKET_KODU='" + _GLOBAL_PARAMETRELER._KULLANICI_FIRMA + "' and MECRA_TURU='DERGI' and TARIFE_KODU= '" + FILE_NAME + "'");
            myCommandKontrol.Connection = myConnectionKontrol;
            myConnectionKontrol.Open();
            SqlDataReader rdKontrol = myCommandKontrol.ExecuteReader(CommandBehavior.CloseConnection);
            if (rdKontrol.HasRows == false)
            {
                using (SqlConnection conn = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString()))
                {
                    conn.Open();
                    SqlCommand myCmd = new SqlCommand();
                    myCmd.CommandText = "INSERT INTO dbo.TRF_TARIFELER_LISTESI(TARIFE_TURU,SIRKET_KODU, MECRA_TURU,TARIFE_KODU, TARIFE_OWNER,ISLEM_TARIHI,ISLEM_SAATI)  VALUES ( @TARIFE_TURU,@SIRKET_KODU, @MECRA_TURU,@TARIFE_KODU, @TARIFE_OWNER,@ISLEM_TARIHI,@ISLEM_SAATI ) SELECT @@IDENTITY AS ID ";
                    myCmd.Parameters.Add("@TARIFE_TURU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_TURU"].Value = "MASTER";
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = MECRA_TURU;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = FILE_NAME;
                    myCmd.Parameters.Add("@TARIFE_OWNER", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_OWNER"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                    myCmd.Parameters.Add("@ISLEM_TARIHI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_TARIHI"].Value = myDTStart.ToString("dd.MM.yyyy").ToString();
                    myCmd.Parameters.Add("@ISLEM_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_SAATI"].Value = myDTStart.ToString("hh:MM:ss").ToString();
                    myCmd.Connection = conn;
                    SqlDataReader rdr = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
                    while (rdr.Read())
                    {
                        // lbID.Caption = myReader["ID"].ToString();
                    }
                    rdr.Close();
                    myCmd.Connection.Close();
                }
            }
            else
            {
                  MessageBox.Show("Bu kod daha önce kullanılmış! Lütfen farklı bir kod kullanınız.", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        public void GUNCELLE(string FILE_NAME, string lbID,string MECRA_TURU)
        {
            DateTime myDTStart = Convert.ToDateTime(DateTime.Now);
            SqlConnection myConnectionTable = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnectionTable.Open();
            SqlCommand myCmd = new SqlCommand();
            myCmd.CommandText = " Update dbo.TRF_TARIFELER_LISTESI SET SIRKET_KODU=@SIRKET_KODU, MECRA_TURU=@MECRA_TURU,TARIFE_KODU=@TARIFE_KODU, TARIFE_OWNER=@TARIFE_OWNER,ISLEM_TARIHI=@ISLEM_TARIHI,ISLEM_SAATI=@ISLEM_SAATI   WHERE TARIFE_TURU='MASTER' AND (ID = '" + lbID + "')";
            myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
            myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = MECRA_TURU;
            myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = FILE_NAME;
            myCmd.Parameters.Add("@TARIFE_OWNER", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_OWNER"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
            myCmd.Parameters.Add("@ISLEM_TARIHI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_TARIHI"].Value = myDTStart.ToString("dd.MM.yyyy").ToString();
            myCmd.Parameters.Add("@ISLEM_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@ISLEM_SAATI"].Value = myDTStart.ToString("hh:MM:ss").ToString();
            myCmd.Connection = myConnectionTable;
            myCmd.ExecuteNonQuery();
            myCmd.Connection.Close();
        }

        public void TELEVIZYON_MASTER_ROW_DELETE(DataView dv)
       {
           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();
           for (int i = 0; i < dv.Count; i++)
           {
               SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_TELEVIZYON_MASTER  where ID=@ID " };
               myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = dv[i]["ID"];
               myCmd.Connection = myConnection;
               myCmd.ExecuteNonQuery();

           } myConnection.Close();
       }
        public void TELEVIZYON_MASTER_ROW_ADD(DataView dv, string TARIFE_KODU)
       {

           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();
           for (int i = 0; i < dv.Count; i++)
           {

               if (dv[i]["MECRA_KODU"].ToString() != "")
               {
                   DataRow DR = dv[i].Row;
                   SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_TELEVIZYON_MASTER(SIRKET_KODU, TARIFE_KODU,KULLANICI_KODU, MECRA_TURU, MNM, MECRA_KODU, MECRA_KODU_YENI, MECRA_GROUP, YAYIN_TURU, BASLANGIC_SAATI, BITIS_SAATI,OPT_PT) " + " VALUES ( @SIRKET_KODU, @TARIFE_KODU,@KULLANICI_KODU, @MECRA_TURU,@MNM,  @MECRA_KODU, @MECRA_KODU_YENI, @MECRA_GROUP, @YAYIN_TURU, @BASLANGIC_SAATI, @BITIS_SAATI,@OPT_PT)" };
                   myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                   myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                   myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                   myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "TELEVIZYON";
                   myCmd.Parameters.Add("@MNM", SqlDbType.NVarChar); myCmd.Parameters["@MNM"].Value = dv[i]["MNM"];
                   myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = dv[i]["MECRA_KODU"];
                   myCmd.Parameters.Add("@MECRA_KODU_YENI", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU_YENI"].Value = dv[i]["MECRA_KODU_YENI"];
                   myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = dv[i]["MECRA_GROUP"];
                   myCmd.Parameters.Add("@YAYIN_TURU", SqlDbType.NVarChar); myCmd.Parameters["@YAYIN_TURU"].Value = dv[i]["YAYIN_TURU"];
                   myCmd.Parameters.Add("@BASLANGIC_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BASLANGIC_SAATI"].Value = dv[i]["BASLANGIC_SAATI"];
                   myCmd.Parameters.Add("@BITIS_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BITIS_SAATI"].Value = dv[i]["BITIS_SAATI"];
                   myCmd.Parameters.Add("@OPT_PT", SqlDbType.NVarChar); myCmd.Parameters["@OPT_PT"].Value = dv[i]["OPT_PT"];


                   myCmd.Connection = myConnection;
                   myCmd.ExecuteNonQuery();


               }
           }
           myConnection.Close();
       }
        public void TELEVIZYON_MASTER_ROW_UPDATE(DataView dv)
       {
           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();
           for (int i = 0; i < dv.Count; i++)
           {
               DataRow DR = dv[i].Row;
               if (DR["MECRA_KODU"].ToString() != "")
               {
                   
                   SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_TELEVIZYON_MASTER SET     MNM=@MNM, MECRA_KODU=@MECRA_KODU, MECRA_KODU_YENI=@MECRA_KODU_YENI, MECRA_GROUP=@MECRA_GROUP, YAYIN_TURU@=YAYIN_TURU,BASLANGIC_SAATI=@BASLANGIC_SAATI, BITIS_SAATI=@BITIS_SAATI ,OPT_PT=@OPT_PT where ID=@ID " };
                   myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                   myCmd.Parameters.Add("@MNM", SqlDbType.NVarChar); myCmd.Parameters["@MNM"].Value = DR["MNM"];
                   myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                   myCmd.Parameters.Add("@MECRA_KODU_YENI", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU_YENI"].Value = DR["MECRA_KODU_YENI"];
                   myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = DR["MECRA_GROUP"];
                   myCmd.Parameters.Add("@YAYIN_TURU", SqlDbType.NVarChar); myCmd.Parameters["@YAYIN_TURU"].Value = DR["YAYIN_TURU"];
                   myCmd.Parameters.Add("@BASLANGIC_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BASLANGIC_SAATI"].Value = DR["BASLANGIC_SAATI"];
                   myCmd.Parameters.Add("@BITIS_SAATI", SqlDbType.NVarChar); myCmd.Parameters["@BITIS_SAATI"].Value = DR["BITIS_SAATI"];
                   myCmd.Parameters.Add("@OPT_PT", SqlDbType.NVarChar); myCmd.Parameters["@OPT_PT"].Value = DR["OPT_PT"];

                   myCmd.Connection = myConnection;
                   myCmd.ExecuteNonQuery();
               }

           } myConnection.Close();
       }
        public void TELEVIZYON_MASTER_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU)
       {
           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();
           SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_TELEVIZYON_MASTER  where TARIFE_KODU=@TARIFE_KODU " };
           myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
           myCmd.Connection = myConnection;
           myCmd.ExecuteNonQuery();
           myConnection.Close();
       }


        public void GAZETE_MASTER_ROW_DELETE(DataView dv)
       {
           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();
           for (int i = 0; i < dv.Count; i++)
           {
               DataRow DR = dv[i].Row;
               SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_GAZETE_MASTER  where ID=@ID " };
               myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
               myCmd.Connection = myConnection;
               myCmd.ExecuteNonQuery();

           } myConnection.Close();
       }
        public void GAZETE_MASTER_ROW_ADD(DataView dv, string TARIFE_KODU)
       {
           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();

           for (int i = 0; i < dv.Count; i++)
           {
               if (dv[i]["MECRA_KODU"].ToString() == "") break;
               SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_GAZETE_MASTER( SIRKET_KODU, TARIFE_KODU,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU, MECRA_KODU_YENI, MEDYA_DAHIL_HARIC,MECRA_GROUP)  VALUES ( @SIRKET_KODU, @TARIFE_KODU,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU, @MECRA_KODU_YENI, @MEDYA_DAHIL_HARIC,@MECRA_GROUP)" };
             
               myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
               myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
               myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
               myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = "GAZETE_MASTER";
               myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = dv[i]["MECRA_KODU"];
               myCmd.Parameters.Add("@MECRA_KODU_YENI", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU_YENI"].Value = dv[i]["MECRA_KODU_YENI"];
               myCmd.Parameters.Add("@MEDYA_DAHIL_HARIC", SqlDbType.NVarChar); myCmd.Parameters["@MEDYA_DAHIL_HARIC"].Value = dv[i]["MEDYA_DAHIL_HARIC"];
               myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = dv[i]["MECRA_GROUP"];
               myCmd.Connection = myConnection;
               myCmd.ExecuteNonQuery();

           }

       }
        public void GAZETE_MASTER_ROW_UPDATE(DataView dv)
       {
           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();
           for (int i = 0; i < dv.Count; i++)
           {
               DataRow DR = dv[i].Row;
               SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_GAZETE_MASTER SET    MECRA_KODU_YENI=@MECRA_KODU_YENI,MEDYA_DAHIL_HARIC=@MEDYA_DAHIL_HARIC, MECRA_GROUP=@MECRA_GROUP   where ID=@ID " };
               myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
               myCmd.Parameters.Add("@MECRA_KODU_YENI", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU_YENI"].Value = DR["MECRA_KODU_YENI"];
               myCmd.Parameters.Add("@MEDYA_DAHIL_HARIC", SqlDbType.NVarChar); myCmd.Parameters["@MEDYA_DAHIL_HARIC"].Value = DR["MEDYA_DAHIL_HARIC"];
               myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = DR["MECRA_GROUP"];
               myCmd.Connection = myConnection;
               myCmd.ExecuteNonQuery();
           }
           myConnection.Close();
       }
        public void GAZETE_TARIFE_MASTER_ROW_ALL_DELETE(string TARIFE_KODU)
       {
           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();
           SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_GAZETE_MASTER  where TARIFE_KODU=@TARIFE_KODU " };
           myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
           myCmd.Connection = myConnection;
           myCmd.ExecuteNonQuery();
           myConnection.Close();
       }

        public void DERGI_MASTER_ROW_DELETE(DataView dv)
       {
           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();
           for (int i = 0; i < dv.Count; i++)
           {
               DataRow DR = dv[i].Row;
               SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_DERGI_MASTER  where ID=@ID " };
               myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
               myCmd.Connection = myConnection;
               myCmd.ExecuteNonQuery();
           }
           myConnection.Close();
       }
        public void DERGI_MASTER_ROW_ADD(DataView dv, string TARIFE_KODU)
       {

           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();
           for (int i = 0; i < dv.Count; i++)
           {
               DataRow DR = dv[i].Row;
               if (DR["MECRA_KODU"].ToString() != "")
               {
                   SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_DERGI_MASTER( SIRKET_KODU, TARIFE_KODU,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU,MECRA_GROUP  )  VALUES ( @SIRKET_KODU, @TARIFE_KODU,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU,@MECRA_GROUP   )" };
                 
                   myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                   myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                   myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                   myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = DR["MECRA_TURU"];
                   myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                   myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = DR["MECRA_GROUP"];
                   myCmd.Connection = myConnection;
                   myCmd.ExecuteNonQuery();

               }
           } myConnection.Close();

       }
        public void DERGI_MASTER_ROW_UPDATE(DataView dv)
       {
           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();
           for (int i = 0; i < dv.Count; i++)
           {
               DataRow DR = dv[i].Row;
               SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_DERGI_MASTER SET     MECRA_GROUP=@MECRA_GROUP  where ID=@ID " };
               myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
               myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = DR["MECRA_GROUP"];
               myCmd.Connection = myConnection;
               myCmd.ExecuteNonQuery();
           }
           myConnection.Close();
       }
        public void DERGI_MASTER_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU)
       {
           SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
           myConnection.Open();
           SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_DERGI_MASTER  where TARIFE_KODU=@TARIFE_KODU " };
           myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
           myCmd.Connection = myConnection;
           myCmd.ExecuteNonQuery();
           myConnection.Close();
       }

       /// <summary>
       /// OUTDOOR
       /// </summary>
       /// <param name="dv"></param>

        public void OUTDOOR_MASTER_ROW_DELETE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_OUTDOOR_MASTER  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void OUTDOOR_MASTER_ROW_ADD(DataView dv, string TARIFE_KODU)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["MECRA_KODU"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_OUTDOOR_MASTER( SIRKET_KODU, TARIFE_KODU,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU,MECRA_GROUP  )  VALUES (  @SIRKET_KODU, @TARIFE_KODU,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU,@MECRA_GROUP   )" };
             
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = DR["MECRA_TURU"];
                    myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                    myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = DR["MECRA_GROUP"];
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            } myConnection.Close();

        }
        public void OUTDOOR_MASTER_ROW_UPDATE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_OUTDOOR_MASTER SET     MECRA_GROUP=@MECRA_GROUP  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = DR["MECRA_GROUP"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void OUTDOOR_MASTER_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU)
        {
           
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_OUTDOOR_MASTER  where TARIFE_KODU=@TARIFE_KODU " };
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }


       /// <summary>
       /// INTERNET
       /// </summary>
       /// <param name="dv"></param> 

        public void INTERNET_MASTER_ROW_DELETE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_INTERNET_MASTER  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void INTERNET_MASTER_ROW_ADD(DataView dv, string TARIFE_KODU)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["MECRA_KODU"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_INTERNET_MASTER(SIRKET_KODU, TARIFE_KODU,KULLANICI_KODU, MECRA_TURU,   MECRA_KODU,MECRA_GROUP  )  VALUES ( @SIRKET_KODU, @TARIFE_KODU,@KULLANICI_KODU, @MECRA_TURU,  @MECRA_KODU,@MECRA_GROUP   )" };
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                    myCmd.Parameters.Add("@MECRA_TURU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_TURU"].Value = DR["MECRA_TURU"];
                    myCmd.Parameters.Add("@MECRA_KODU", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_KODU"].Value = DR["MECRA_KODU"];
                    myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = DR["MECRA_GROUP"];
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            } myConnection.Close();

        }
        public void INTERNET_MASTER_ROW_UPDATE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_INTERNET_MASTER SET     MECRA_GROUP=@MECRA_GROUP  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = DR["MECRA_GROUP"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void INTERNET_MASTER_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_INTERNET_MASTER  where TARIFE_KODU=@TARIFE_KODU " };
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }

       /// <summary>
       /// MEDYA AJANSI
       /// </summary>
       /// <param name="dv"></param>

        public void MEDYA_AJANSI_MASTER_ROW_DELETE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_MEDYAAJANSI_MASTER  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void MEDYA_AJANSI_MASTER_ROW_ADD(DataView dv, string TARIFE_KODU)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["MEDYA_AJANSI"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_MEDYAAJANSI_MASTER(SIRKET_KODU, TARIFE_KODU,KULLANICI_KODU, MEDYA_AJANSI,   GROUPM,GROUPM_DIGER  )  VALUES ( @SIRKET_KODU, @TARIFE_KODU,@KULLANICI_KODU, @MEDYA_AJANSI,  @GROUPM,@GROUPM_DIGER   )" };
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                    myCmd.Parameters.Add("@MEDYA_AJANSI", SqlDbType.NVarChar); myCmd.Parameters["@MEDYA_AJANSI"].Value = DR["MEDYA_AJANSI"];
                    myCmd.Parameters.Add("@GROUPM", SqlDbType.NVarChar); myCmd.Parameters["@GROUPM"].Value = DR["GROUPM"];
                    myCmd.Parameters.Add("@GROUPM_DIGER", SqlDbType.NVarChar); myCmd.Parameters["@GROUPM_DIGER"].Value = DR["GROUPM_DIGER"];
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            } myConnection.Close();

        }
        public void MEDYA_AJANSI_MASTER_ROW_UPDATE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_MEDYAAJANSI_MASTER SET     MECRA_GROUP=@MECRA_GROUP  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = DR["MECRA_GROUP"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void MEDYA_AJANSI_MASTER_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_MEDYAAJANSI_MASTER  where TARIFE_KODU=@TARIFE_KODU " };
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }

       /// <summary>
       /// REKLAM TEXT
       /// </summary>
       /// <param name="dv"></param>
       /// 

        public void REKLAM_TEXT_MASTER_ROW_DELETE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_REKLAM_TEXT_MASTER  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void REKLAM_TEXT_MASTER_ROW_ADD(DataView dv, string TARIFE_KODU)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["VERSIYON_GUNCELLE"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_REKLAM_TEXT_MASTER(SIRKET_KODU, TARIFE_KODU,KULLANICI_KODU, REKLAMIN_FIRMASI,   VERSIYON,VERSIYON_GUNCELLE  )  VALUES (@SIRKET_KODU, @TARIFE_KODU,@KULLANICI_KODU, @REKLAMIN_FIRMASI,  @VERSIYON,@VERSIYON_GUNCELLE   )" };
                  
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                    myCmd.Parameters.Add("@REKLAMIN_FIRMASI", SqlDbType.NVarChar); myCmd.Parameters["@REKLAMIN_FIRMASI"].Value = DR["REKLAMIN_FIRMASI"];
                    myCmd.Parameters.Add("@VERSIYON", SqlDbType.NVarChar); myCmd.Parameters["@VERSIYON"].Value = DR["VERSIYON"];
                    myCmd.Parameters.Add("@VERSIYON_GUNCELLE", SqlDbType.NVarChar); myCmd.Parameters["@VERSIYON_GUNCELLE"].Value = DR["VERSIYON_GUNCELLE"];
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            } myConnection.Close();

        }
        public void REKLAM_TEXT_MASTER_ROW_UPDATE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_REKLAM_TEXT_MASTER SET     VERSIYON_GUNCELLE=@VERSIYON_GUNCELLE  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@VERSIYON_GUNCELLE", SqlDbType.NVarChar); myCmd.Parameters["@VERSIYON_GUNCELLE"].Value = DR["VERSIYON_GUNCELLE"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void REKLAM_TEXT_MASTER_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_REKLAM_TEXT_MASTER  where TARIFE_KODU=@TARIFE_KODU " };
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }


       /// <summary>
       /// URUNLER 
       /// </summary>
       /// <param name="dv"></param>

        public void URUNLER_MASTER_ROW_DELETE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_URUN_HIZMET_MASTER  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void URUNLER_MASTER_ROW_ADD(DataView dv, string TARIFE_KODU)
        { 
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["URUN_HIZMET_GUNCELLE"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_URUN_HIZMET_MASTER(SIRKET_KODU, TARIFE_KODU,KULLANICI_KODU, REKLAMIN_FIRMASI,   URUN_HIZMET,URUN_HIZMET_GUNCELLE  )  VALUES ( @SIRKET_KODU, @TARIFE_KODU,@KULLANICI_KODU, @REKLAMIN_FIRMASI,  @URUN_HIZMET,@URUN_HIZMET_GUNCELLE   )" };
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                    myCmd.Parameters.Add("@REKLAMIN_FIRMASI", SqlDbType.NVarChar); myCmd.Parameters["@REKLAMIN_FIRMASI"].Value = DR["REKLAMIN_FIRMASI"];
                    myCmd.Parameters.Add("@URUN_HIZMET", SqlDbType.NVarChar); myCmd.Parameters["@URUN_HIZMET"].Value = DR["URUN_HIZMET"];
                    myCmd.Parameters.Add("@URUN_HIZMET_GUNCELLE", SqlDbType.NVarChar); myCmd.Parameters["@URUN_HIZMET_GUNCELLE"].Value = DR["URUN_HIZMET_GUNCELLE"];
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            } myConnection.Close();

        }
        public void URUNLER_MASTER_ROW_UPDATE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_URUN_HIZMET_MASTER SET     URUN_HIZMET_GUNCELLE=@URUN_HIZMET_GUNCELLE  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@URUN_HIZMET_GUNCELLE", SqlDbType.NVarChar); myCmd.Parameters["@URUN_HIZMET_GUNCELLE"].Value = DR["URUN_HIZMET_GUNCELLE"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void URUNLER_MASTER_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_URUN_HIZMET_MASTER  where TARIFE_KODU=@TARIFE_KODU " };
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }



        /// <summary>
        /// REKLAMVERENLER 
        /// </summary>
        /// <param name="dv"></param>

        public void REKLAMVEREN_MASTER_ROW_DELETE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_REKLAM_VEREN_MASTER  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void REKLAMVEREN_MASTER_ROW_ADD(DataView dv, string TARIFE_KODU)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["REKLAMIN_FIRMASI_YENI"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_REKLAM_VEREN_MASTER(SIRKET_KODU, TARIFE_KODU,KULLANICI_KODU, MEDYA_AJANSI,   REKLAMIN_FIRMASI,REKLAMIN_FIRMASI_YENI  )  VALUES ( @SIRKET_KODU, @TARIFE_KODU,@KULLANICI_KODU, @MEDYA_AJANSI,  @REKLAMIN_FIRMASI,@REKLAMIN_FIRMASI_YENI   )" };
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;
                    myCmd.Parameters.Add("@MEDYA_AJANSI", SqlDbType.NVarChar); myCmd.Parameters["@MEDYA_AJANSI"].Value = DR["MEDYA_AJANSI"];
                    myCmd.Parameters.Add("@REKLAMIN_FIRMASI", SqlDbType.NVarChar); myCmd.Parameters["@REKLAMIN_FIRMASI"].Value = DR["REKLAMIN_FIRMASI"];
                    myCmd.Parameters.Add("@REKLAMIN_FIRMASI_YENI", SqlDbType.NVarChar); myCmd.Parameters["@REKLAMIN_FIRMASI_YENI"].Value = DR["REKLAMIN_FIRMASI_YENI"];
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            } myConnection.Close();

        }
        public void REKLAMVEREN_MASTER_ROW_UPDATE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_REKLAM_VEREN_MASTER SET     MECRA_GROUP=@MECRA_GROUP  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@MECRA_GROUP", SqlDbType.NVarChar); myCmd.Parameters["@MECRA_GROUP"].Value = DR["MECRA_GROUP"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void REKLAMVEREN_MASTER_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_REKLAM_VEREN_MASTER  where TARIFE_KODU=@TARIFE_KODU " };
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }



        /// <summary>
        /// SPOT_TYPE  
        /// </summary>
        /// <param name="dv"></param>
        public void SPOT_TYPE_MASTER_ROW_DELETE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_SPOTTYPE_MASTER  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void SPOT_TYPE_MASTER_ROW_ADD(DataView dv, string TARIFE_KODU)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["SPOT_TIPI"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_SPOTTYPE_MASTER(SIRKET_KODU, TARIFE_KODU,KULLANICI_KODU, SPOT_TIPI,   SPOT_TIPI_YENI  )  VALUES ( @SIRKET_KODU, @TARIFE_KODU,@KULLANICI_KODU, @SPOT_TIPI,  @SPOT_TIPI_YENI   )" };
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_MAIL;

                    myCmd.Parameters.Add("@SPOT_TIPI", SqlDbType.NVarChar); myCmd.Parameters["@SPOT_TIPI"].Value = DR["SPOT_TIPI"];
                    myCmd.Parameters.Add("@SPOT_TIPI_YENI", SqlDbType.NVarChar); myCmd.Parameters["@SPOT_TIPI_YENI"].Value = DR["SPOT_TIPI_YENI"];
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            } myConnection.Close();

        }
        public void SPOT_TYPE_MASTER_ROW_UPDATE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_SPOTTYPE_MASTER SET     SPOT_TYPE_YENI=@SPOT_TYPE_YENI  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@SPOT_TIPI_YENI", SqlDbType.NVarChar); myCmd.Parameters["@SPOT_TIPI_YENI"].Value = DR["SPOT_TIPI_YENI"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();
            }
            myConnection.Close();
        }
        public void SPOT_TYPE_MASTER_ROW_ALL_DELETE(string TARIFE_KODU)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_SPOTTYPE_MASTER  where TARIFE_KODU=@TARIFE_KODU " };
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }

        public void YTD_ROW_DELETE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_YTD_MASTER  where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();

            } myConnection.Close();
        }
        public void YTD_ROW_ADD(DataView dv, string TARIFE_KODU)
        {

            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                if (DR["YIL"].ToString() != "")
                {
                    SqlCommand myCmd = new SqlCommand() { CommandText = "INSERT INTO dbo.TRF_YTD_MASTER(SIRKET_KODU, TARIFE_KODU,KULLANICI_KODU, YIL,AY,YTD)  VALUES ( @SIRKET_KODU, @TARIFE_KODU,@KULLANICI_KODU, @YIL,@AY,@YTD)" };
                    myCmd.Parameters.Add("@SIRKET_KODU", SqlDbType.NVarChar); myCmd.Parameters["@SIRKET_KODU"].Value = _GLOBAL_PARAMETRELER._KULLANICI_FIRMA;
                    myCmd.Parameters.Add("@TARIFE_KODU", SqlDbType.NVarChar); myCmd.Parameters["@TARIFE_KODU"].Value = TARIFE_KODU;
                    myCmd.Parameters.Add("@KULLANICI_KODU", SqlDbType.NVarChar); myCmd.Parameters["@KULLANICI_KODU"].Value = DR["KULLANICI_KODU"];

                    myCmd.Parameters.Add("@YIL", SqlDbType.NVarChar); myCmd.Parameters["@YIL"].Value = DR["YIL"];
                    myCmd.Parameters.Add("@AY", SqlDbType.NVarChar); myCmd.Parameters["@AY"].Value = DR["AY"];
                    //myCmd.Parameters.Add("@YILM", SqlDbType.NVarChar); myCmd.Parameters["@YILM"].Value = DR["YILM"];
                    //myCmd.Parameters.Add("@AYM", SqlDbType.NVarChar); myCmd.Parameters["@AYM"].Value = DR["AYM"];
                    myCmd.Parameters.Add("@YTD", SqlDbType.NVarChar); myCmd.Parameters["@YTD"].Value = DR["YTD"];
                    myCmd.Connection = myConnection;
                    myCmd.ExecuteNonQuery();

                }
            } myConnection.Close();
        }
        public void YTD_ROW_UPDATE(DataView dv)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            for (int i = 0; i < dv.Count; i++)
            {
                DataRow DR = dv[i].Row;
                SqlCommand myCmd = new SqlCommand() { CommandText = "UPDATE  dbo.TRF_YTD_MASTER SET   REKLAMIN_FIRMASI_YENI=@REKLAMIN_FIRMASI_YENI    where ID=@ID " };
                myCmd.Parameters.Add("@ID", SqlDbType.Int); myCmd.Parameters["@ID"].Value = DR["ID"];
                myCmd.Parameters.Add("@YTD", SqlDbType.NVarChar); myCmd.Parameters["@YTD"].Value = DR["YTD"];

                myCmd.Connection = myConnection;
                myCmd.ExecuteNonQuery();

            } myConnection.Close();
        }
        public void YTD_TARIFE_ROW_ALL_DELETE(string TARIFE_KODU)
        {
            SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
            myConnection.Open();
            SqlCommand myCmd = new SqlCommand() { CommandText = "DELETE  dbo.TRF_YTD_MASTER  where TARIFE_KODU=@TARIFE_KODU " };
            myCmd.Parameters.AddWithValue("@TARIFE_KODU", TARIFE_KODU);
            myCmd.Connection = myConnection;
            myCmd.ExecuteNonQuery();
            myConnection.Close();
        }



    }
}
