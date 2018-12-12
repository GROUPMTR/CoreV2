using System;
using System.ComponentModel;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.DXErrorProvider;
using System.DirectoryServices;
using System.Data.SqlClient;

namespace CoreV2
{
    public partial class _LOGIN : DevExpress.XtraEditors.XtraForm
    {
        public _LOGIN()
        {
            InitializeComponent();
            ControlBox = false;

            _GLOBAL_PARAMETRELER._KULLANICI_MAIL = null;
            FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            StartPosition = FormStartPosition.CenterParent;
            TXT_USERNAME.Text = Environment.UserName.ToString().ToLower();
            InitFields();
            ValidateFields();
        }
        private void InitFields()
        {
            //TXT_PASSWORDS.Text = null;         
        }
        private void ValidateFields()
        {
            Validate_EmptyStringRule(TXT_PASSWORDS);
          
        }
        private void Validate_EmptyStringRule(BaseEdit control)
        {
            if (control.Text == null || control.Text.Trim().Length == 0) dxErrorProviders.SetError(control, "Bu alan boş bırakılamaz.", ErrorType.Critical);
            else dxErrorProviders.SetError(control, "");
        }
        private DirectoryEntry GetDirectoryObject()
        {
            DirectoryEntry oDE = new DirectoryEntry("LDAP://10.219.168.51", string.Format("{0}", TXT_USERNAME.Text), string.Format("{0}", TXT_PASSWORDS.Text), AuthenticationTypes.Secure);
       
            return oDE;
        }

        private void BTN_TAMAM_Click(object sender, EventArgs e)
        {
            _GLOBAL_PARAMETRELER._DIL = CMB_DIL.Text;
            LOGIN();
        }

        private void BTN_VAZGEC_Click(object sender, EventArgs e)
        {

            Application.Exit();
        }

        private void TXT_PASSWORDS_Validating(object sender, CancelEventArgs e)
        {
            Validate_EmptyStringRule(sender as BaseEdit);
        }

        private void TXT_PASSWORDS_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (Convert.ToInt32(e.KeyChar) == 13)
            {    _GLOBAL_PARAMETRELER._DIL = CMB_DIL.Text;
                LOGIN();        
            }
        } 
        void LOGIN()
        { 
            ValidateFields();
            if (dxErrorProviders.HasErrors != true)
            {
                string UserMail = "";
                if (TXT_USERNAME.Text != "" && TXT_PASSWORDS.Text != "")
                {
                    _GLOBAL_PARAMETRELER._KULLANICI_TABLO_NAME = TXT_USERNAME.Text;
                     SqlConnection myConnection = new SqlConnection(_GLOBAL_PARAMETRELER._CONNECTION_STRING.ToString());
                    string SQL = string.Format("  select *  from  ADM_KULLANICILAR   WHERE     (EMAIL LIKE N'{0}%')  ", TXT_USERNAME.Text);
                    SqlCommand myCommand = new SqlCommand(SQL, myConnection) { CommandText = SQL.ToString() };
                    myConnection.Open();
                    SqlDataReader myReader = myCommand.ExecuteReader(CommandBehavior.CloseConnection);
                    int Rowx = 0;
                    while (myReader.Read())
                    {
                        Rowx = 1;

                        _GLOBAL_PARAMETRELER._KULLANICI_FIRMA = myReader["FIRMA"].ToString();
                        _GLOBAL_PARAMETRELER._KULLANICI_MAIL = myReader["EMAIL"].ToString();
                        _GLOBAL_PARAMETRELER._KULLANICI_GRUBU = myReader["DEPARTMAN"].ToString();
                        Close();

                        DirectoryEntry de = GetDirectoryObject();
                        DirectorySearcher deSearch = new DirectorySearcher() { SearchRoot = de, Filter = string.Format("(&(objectClass=user)(SAMAccountName={0}))", TXT_USERNAME.Text) };
                        deSearch.PropertiesToLoad.Add("mail");
                        deSearch.PropertiesToLoad.Add("userPrincipalName");
                        SortOption Srt = new SortOption("mail", System.DirectoryServices.SortDirection.Ascending);
                        deSearch.Sort = Srt;
                        //Sonuçları bir değişkene atalım.
                        try
                        {
                            SearchResultCollection Results = deSearch.FindAll();
                            if (Results != null)
                            {
                                foreach (SearchResult Result in Results)
                                {
                                    ResultPropertyCollection Rpc = Result.Properties;
                                    //foreach (string Property in Rpc.PropertyNames)
                                    //{
                                    UserMail = Rpc["userPrincipalName"][0].ToString();
                                    //}
                                }
                            }
                            Close();
                        }
                        catch (Exception EX) { MessageBox.Show("Kullanıcı adı veya Password geçersiz."); }
                    }
                    if (Rowx == 0)
                    {
                        ADMIN.KULLANICI_TANIMLA todo = new ADMIN.KULLANICI_TANIMLA();
                        _GLOBAL_PARAMETRELER._KULLANICI_FIRMA = todo.CMB_FIRMA.Text;
                        _GLOBAL_PARAMETRELER._KULLANICI_MAIL = todo.txt_MAIL.Text;
                

                        todo.ShowDialog();
                    }
                }
            }
            else
            { MessageBox.Show(" Hatalı alanı kontrol ediniz."); }
        }
    }
}
