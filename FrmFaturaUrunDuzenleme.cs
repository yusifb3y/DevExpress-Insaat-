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
    public partial class FrmFaturaUrunDuzenleme : Form
    {
        public FrmFaturaUrunDuzenleme()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        public string urunid = "";

        private void FrmFaturaUrunDuzenleme_Load(object sender, EventArgs e)
        {
            TxtUrunID.Text = urunid;
            SqlCommand cmd = new SqlCommand("Select * From TBL_FATURADETAY where FATURAURUNID=@P1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", urunid);
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                TxtFiyat.Text = dr[3].ToString();
                TxtMiktar.Text = dr[2].ToString();
                TxtTutar.Text = dr[4].ToString();
                TxtUrunAd.Text = dr[1].ToString();
              
            }
            bgl.baglanti().Close();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
           SqlCommand cmd = new SqlCommand("Update TBL_FATURADETAY set URUNAD=@P1,MIKTAR=@P2,FIYAT=@P3,TUTAR=@P4 where FATURAURUNID=@P5", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtMiktar.Text);
            cmd.Parameters.AddWithValue("@p3",decimal.Parse( (TxtFiyat.Text).ToString()));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse((TxtTutar.Text).ToString()));
            cmd.Parameters.AddWithValue("@p5", TxtUrunID.Text);

            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Məhsul Yeniləndi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning); 



        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete TBL_FATURADETAY where FATURAURUNID=@P1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", TxtUrunID.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Faktura Detay Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);


        }
    }
}
