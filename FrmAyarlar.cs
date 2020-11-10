using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Ticari_Otomasyon
{
    public partial class FrmAyarlar : Form
    {
        public FrmAyarlar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();
        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_ADMİN",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void temizle()
        {
            TxtKullaniciAd.Text = "";
            TxtSifre.Text = "";
        }
        private void FrmAyarlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void BtnIslem_Click(object sender, EventArgs e)
        {
            if (BtnIslem.Text == "Kaydet")
            {
                SqlCommand cmd = new SqlCommand("insert into TBL_ADMİN (KullaniciAd,Sifre) Values (@p1,@p2)", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
                cmd.Parameters.AddWithValue("@p2", TxtSifre.Text);

                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Yeni Admin Sisteme Kaydedildi", "", MessageBoxButtons.OK, MessageBoxIcon.Information);
                listele();
            }
            if(BtnIslem.Text == "Güncelle")
            {
                SqlCommand cmd2 = new SqlCommand("Update TBL_ADMİN set  Sifre=@p2 where KullaniciAd=@p1",bgl.baglanti());
                cmd2.Parameters.AddWithValue("@p1",TxtKullaniciAd.Text);
                cmd2.Parameters.AddWithValue("@p2",TxtSifre.Text);
                cmd2.ExecuteNonQuery();
                bgl.baglanti().Close();

                MessageBox.Show("Admin Bilgisi Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                listele();

            }

        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                TxtKullaniciAd.Text = dr[0].ToString();
                TxtSifre.Text = dr[1].ToString();

            }
            bgl.baglanti().Close();

        }

        private void TxtKullaniciAd_TextChanged(object sender, EventArgs e)
        {
            if(TxtKullaniciAd.Text != "") //null
            {
                BtnIslem.Text = "Güncelle";
                BtnIslem.BackColor = Color.GreenYellow;

            }
            else
            {
                BtnIslem.Text = "Kaydet";
                BtnIslem.BackColor = Color.MediumTurquoise;

            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
