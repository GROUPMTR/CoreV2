using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoreV2.INFO
{
    public partial class INFO_PAGE : Form
    {
        public INFO_PAGE()
        {
            InitializeComponent();
        }

        private void INFO_PAGE_Load(object sender, EventArgs e)
        {
            MEMOS_INFO.Text = @"KANAL		MEDYA
                                CPP		20+ABC1
                                Hesap Türü	Süre/Cpp/
                                Birim Fiyat		Tarife Fiyatı
                                Baslangıç Saati	18:00
                                Bitiş Saati		25:59	Bitiş Saati Başlangıç Saatinden küçük olamaz
                                Opt/Pt		+/-	İstenilen Veri Girilebilir
                                Başlangıç Tarihi	
                                Bitiş Tarihi		 ";
        }
    }
}
