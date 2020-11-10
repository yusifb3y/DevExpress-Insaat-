using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //required for sql library.
using System.Xml; //required for newspaper.

namespace Ticari_Otomasyon
{
    public partial class FrmAnaSayfa : Form
    {
        public FrmAnaSayfa()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        // AZALAN STOK İÇİN.
        void azalanstok()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select URUNAD,SUM(ADET) AS 'Adet' From TBL_URUNLER Group by URUNAD having SUM(ADET)<15 ORDER BY SUM(ADET)", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void ajanda()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select TARIH,SAAT,BASLIK From TBL_NOTLAR order by ID ASC ",bgl.baglanti());
            da.Fill(dt);
            gridControl2.DataSource = dt;
        }
        void firmahareket()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute FirmaHareketler",bgl.baglanti());
            da.Fill(dt);
            gridControl3.DataSource = dt;
        }
        void fihrist()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select AD,TELEFON1 From TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            gridControl4.DataSource = dt;
        }
       /* void haberler()
        {
            //xml deki verileri okumaya yarar.
            XmlTextReader xmloku = new XmlTextReader("http://www.hurriyet.com.tr/rss/anasayfa");
            while(xmloku.Read())
            {
                if(xmloku.Name=="title")
                {
                    listBox1.Items.Add(xmloku.ReadString());
                }
            }
        } */

        private void FrmAnaSayfa_Load(object sender, EventArgs e)
        {
            azalanstok();
            ajanda();
            firmahareket();
            fihrist();
         //  webBrowser1.Navigate("https://www.doviz.com/"); //Navigate yönlendirme.
            //haberler();
           // webBrowser2.Navigate("https://www.youtube.com/");
            
        }

      
    }
}
