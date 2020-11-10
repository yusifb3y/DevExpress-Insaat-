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
    public partial class FrmAdmin : Form
    {
        public FrmAdmin()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void admingiris()
        {
            SqlCommand cmd = new SqlCommand("Select * From TBL_ADMİN where KullaniciAd=@p1 and Sifre=@p2", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", TxtKullaniciAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSifre.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            if(dr.Read())
            {
                FrmAnaModul fr = new FrmAnaModul();
                fr.kullanici = TxtKullaniciAd.Text;
                fr.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Hatalı Giris Yaptınız.","", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            bgl.baglanti().Close();

            
        }
        private void BtnGiris_MouseHover(object sender, EventArgs e)
        {
            BtnGiris.BackColor = Color.Yellow;
           


        }

        private void BtnGiris_MouseLeave(object sender, EventArgs e)
        {
            BtnGiris.BackColor = Color.LightCoral;

        }

        private void BtnGiris_Click(object sender, EventArgs e)
        {
            admingiris();
        }

        private void FrmAdmin_Load(object sender, EventArgs e)
        {

        }
    }
}
