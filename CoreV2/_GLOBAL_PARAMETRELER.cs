using System;

namespace CoreV2
{
    public class _GLOBAL_PARAMETRELER
    {
        readonly System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
        public static System.Threading.Thread thread = System.Threading.Thread.CurrentThread;
        public static System.Globalization.CultureInfo Culture = thread.CurrentCulture;
        public static string _CULTURE = "tr-TR", _DIL = "tr-TR";

        // SERVER AYARLARI ID=sa;Password=nabuKad_07;
        //   public static string _CONNECTION_STRING = "Password=tr1net784;Persist Security Info=True;User ID=login;Initial Catalog=CoreV2;Data Source=10.219.168.94", _CONNECTION_STRING_GRM_DATA, _SERVERNAME, _IP, _DNS, _PLAN_SEC;

        public static string _CONNECTION_STRING_GRM_DATA, _SERVERNAME, _IP, _DNS, _PLAN_SEC;
        public static string _RAPOR_DB = "COMPETITIVE";
       //   public static string _CONNECTION_STRING= @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=COMPETITIVE;Data Source=.";
         public static string _CONNECTION_STRING = @"Password=tr1net784;Persist Security Info=True;User ID=login;Initial Catalog=COMPETITIVE;Data Source=10.219.168.94";
        // public static string _CONNECTION_"+_GLOBAL_PARAMETRELER._RAPOR_DB+" = @"Password=tr1net784;Persist Security Info=True;User ID=login;Initial Catalog="+_GLOBAL_PARAMETRELER._RAPOR_DB+";Data Source=10.219.168.94";

        // FIRMA VE KULLANICI KODU
        public static string _SIRKET_KODU, _KULLANICI_FIRMA, _KULLANICI_GRUBU, _KULLANICI_MAIL, _KULLANICI_TABLO_NAME, _MACHINELOGINNAME, _SABIT_TABLO_ADI, _RAPOR_KODU, _RAPOR_ID;

        //KULLANICI HAKLARI    
        public static string _PROGRAM_KISITLAMASI, _MENU_KISITLAMAISI, _MUSTERI_KISITLAMASI;

        public static DateTime _START_DATE, _END_DATE;


    }
}
