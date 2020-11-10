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
    public partial class FrmGiderler : Form
    {
        public FrmGiderler()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_GIDERLER Order By ID ASC", bgl.baglanti()); //ORDER BY ID YE GÖRE SIRALADI.
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void temizle()
        {
            CmbAy.Text = "";
            CmbYil.Text = "";
            TxtElektrik.Text = "";
            TxtSu.Text = "";
            TxtDogalgaz.Text = "";
            Txtinternet.Text = "";
            TxtMaas.Text = "";
            TxtEkstra.Text = "";
            RchNotlar.Text = "";
            Txtid.Text = "";

        }
        private void FrmGiderler_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
            
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand komut = new SqlCommand("insert into TBL_GIDERLER (AY,YIL,ELEKTRIK,SU,DOGALGAZ,INTERNET,MAASLAR,EKSTRA,NOTLAR) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9)", bgl.baglanti());
            komut.Parameters.AddWithValue("@p1", CmbAy.Text);
            komut.Parameters.AddWithValue("@p2", CmbYil.Text);
            komut.Parameters.AddWithValue("@p3",decimal.Parse(TxtElektrik.Text));
            komut.Parameters.AddWithValue("@p4",decimal.Parse( TxtSu.Text));
            komut.Parameters.AddWithValue("@p5",decimal.Parse(TxtDogalgaz.Text));
            komut.Parameters.AddWithValue("@p6",decimal.Parse(Txtinternet.Text));
            komut.Parameters.AddWithValue("@p7",decimal.Parse( TxtMaas.Text));
            komut.Parameters.AddWithValue("@p8",decimal.Parse( TxtEkstra.Text));
            komut.Parameters.AddWithValue("@p9", RchNotlar.Text);

            komut.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Xərc Bilgisi Əlavə Edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);  //FrmGiderler da yan kısımdaki groupControlde verileri göstermeye yarar.
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                CmbAy.Text = dr["AY"].ToString();
                CmbYil.Text = dr["YIL"].ToString();
                TxtElektrik.Text = dr["ELEKTRIK"].ToString();
                TxtSu.Text = dr["SU"].ToString();
                TxtDogalgaz.Text = dr["DOGALGAZ"].ToString();
                Txtinternet.Text = dr["INTERNET"].ToString();
                TxtMaas.Text = dr["MAASLAR"].ToString();
                TxtEkstra.Text = dr["EKSTRA"].ToString();
                RchNotlar.Text= dr["NOTLAR"].ToString();
           
            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();

        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete TBL_GIDERLER where ID=@P1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", Txtid.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Xərc Bilgisi Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_GIDERLER set AY=@P1 ,YIL=@P2 ,ELEKTRIK=@P3 ,SU=@P4, DOGALGAZ=@P5,INTERNET=@P6,MAASLAR=@P7,EKSTRA=@P8,NOTLAR=@P9 where ID=@P10", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", CmbAy.Text);
            cmd.Parameters.AddWithValue("@p2", CmbYil.Text);
            cmd.Parameters.AddWithValue("@p3", decimal.Parse(TxtElektrik.Text));
            cmd.Parameters.AddWithValue("@p4", decimal.Parse(TxtSu.Text));
            cmd.Parameters.AddWithValue("@p5", decimal.Parse(TxtDogalgaz.Text));
            cmd.Parameters.AddWithValue("@p6", decimal.Parse(Txtinternet.Text));
            cmd.Parameters.AddWithValue("@p7", decimal.Parse(TxtMaas.Text));
            cmd.Parameters.AddWithValue("@p8", decimal.Parse(TxtEkstra.Text));
            cmd.Parameters.AddWithValue("@p9", RchNotlar.Text);
            cmd.Parameters.AddWithValue("@p10", Txtid.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Xərc Bilgisi Yeniləndi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();
        }

        private void CmbYil_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
