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
    public partial class FrmNotlar : Form
    {
        public FrmNotlar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();


        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_NOTLAR", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void temizle()
        {
            Txtid.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
            TxtBaslik.Text = "";
            TxtHitap.Text = "";
            TxtOlusturan.Text = "";
            RchDetay.Text = "";

        }

        private void FrmNotlar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();

        }

       

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);  //FrmNotlar da yan kısımdaki groupControlde verileri göstermeye yarar.
            if (dr != null)  //boş bir satır değilse.
            {
                Txtid.Text = dr["ID"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtBaslik.Text = dr["BASLIK"].ToString();
                RchDetay.Text = dr["DETAY"].ToString();
                TxtOlusturan.Text = dr["OLUSTURAN"].ToString();
                TxtHitap.Text = dr["HITAP"].ToString();
                
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into TBL_NOTLAR (TARIH,SAAT,BASLIK,DETAY,OLUSTURAN,HITAP) values (@p1,@p2,@p3,@p4,@p5,@p6)", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", MskTarih.Text);
            cmd.Parameters.AddWithValue("@p2", MskSaat.Text);
            cmd.Parameters.AddWithValue("@p3", TxtBaslik.Text);
            cmd.Parameters.AddWithValue("@p4", RchDetay.Text);
            cmd.Parameters.AddWithValue("@p5", TxtOlusturan.Text);
            cmd.Parameters.AddWithValue("@p6", TxtHitap.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Not Bilgisi Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete TBL_NOTLAR where ID=@P1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@P1", Txtid.Text);

            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Not Bilgisi Sistemden Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            temizle();

        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_NOTLAR set TARIH=@P1,SAAT=@P2,BASLIK=@P3,DETAY=@P4,OLUSTURAN=@P5,HITAP=@P6 where ID=@P7", bgl.baglanti());
            cmd.Parameters.AddWithValue("@P1", MskTarih.Text);
            cmd.Parameters.AddWithValue("@P2", MskSaat.Text);
            cmd.Parameters.AddWithValue("@P3", TxtBaslik.Text);
            cmd.Parameters.AddWithValue("@P4", RchDetay.Text);
            cmd.Parameters.AddWithValue("@P5", TxtOlusturan.Text);
            cmd.Parameters.AddWithValue("@P6", TxtHitap.Text);
            cmd.Parameters.AddWithValue("@P7", Txtid.Text);

            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Not Bilgisi Güncellendi", "Güncel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            temizle();


        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmNotDetay fr = new FrmNotDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);

            if(dr !=null)
            {
                fr.not = dr["DETAY"].ToString();

            }
            fr.Show();

        }
    }
}
