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
    public partial class FrmFaturalar : Form
    {
        public FrmFaturalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_FATURABILGI", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

        }
        void temizle()
        {
            Txtid.Text = "";
            TxtSeri.Text = "";
            TxtSirano.Text = "";
            MskTarih.Text = "";
            MskSaat.Text = "";
            TxtVergiD.Text = "";
            TxtAlici.Text = "";
            TxtTeden.Text = "";
            TxtTalan.Text = "";


        }
        private void FrmFaturalar_Load(object sender, EventArgs e)
        {
            listele();
            temizle();
        }

        private void labelControl17_Click(object sender, EventArgs e)
        {

        }

        private void labelControl24_Click(object sender, EventArgs e)
        {

        }

        private void labelControl1_Click(object sender, EventArgs e)
        {

        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {

          
            if (TxtFaturaID.Text == "" ) //eğer faturaıd boş ise demek.
            {
                
                SqlCommand cmd = new SqlCommand("insert into TBL_FATURABILGI (SERI,SIRANO,TARIH,SAAT,VERGIDAIRE,ALICI,TESLIMEDEN,TESLIMALAN) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                cmd.Parameters.AddWithValue("@p1", TxtSeri.Text);
                cmd.Parameters.AddWithValue("@p2", TxtSirano.Text);
                cmd.Parameters.AddWithValue("@p3", MskTarih.Text);
                cmd.Parameters.AddWithValue("@p4", MskSaat.Text);
                cmd.Parameters.AddWithValue("@p5", TxtVergiD.Text);
                cmd.Parameters.AddWithValue("@p6", TxtAlici.Text);
                cmd.Parameters.AddWithValue("@p7", TxtTeden.Text);
                cmd.Parameters.AddWithValue("@p8", TxtTalan.Text);
                cmd.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();
                MessageBox.Show("Faktura Bilgisi Sistemə Əlavə Edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }

            //FİRMA CARİSİ
            if(TxtFaturaID.Text != "" && CmbTur.Text=="Firma")
            {
                double fiyat, miktar, tutar;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();
                SqlCommand cmd2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                cmd2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                cmd2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                cmd2.Parameters.AddWithValue("@p3",decimal.Parse(( TxtFiyat.Text).ToString()));
                cmd2.Parameters.AddWithValue("@p4",decimal.Parse((TxtTutar.Text).ToString()));
                cmd2.Parameters.AddWithValue("@p5", TxtFaturaID.Text);
                cmd2.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();
                //HAREKET TABLOSUNA VERİ GİRİŞİ
                SqlCommand cmd3 = new SqlCommand("insert into TBL_FIRMAHAREKETLER (URUNID,ADET,PERSONEL,FIRMA,FIYAT,TOPLAM,FATURAID,TARIH) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                cmd3.Parameters.AddWithValue("@p1", TxtUrunID.Text);
                cmd3.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                cmd3.Parameters.AddWithValue("@p3", TxtPersonel.Text);
                cmd3.Parameters.AddWithValue("@p4", TxtFirma.Text);
                cmd3.Parameters.AddWithValue("@p5", decimal.Parse((TxtFiyat.Text).ToString()));
                cmd3.Parameters.AddWithValue("@p6", decimal.Parse((TxtTutar.Text).ToString()));
                cmd3.Parameters.AddWithValue("@p7", TxtFaturaID.Text);
                cmd3.Parameters.AddWithValue("@p8", MskTarih.Text);
                cmd3.ExecuteNonQuery();
                bgl.baglanti().Close();

                //STOK SAYISINI AZALTMA
                SqlCommand cmd4 = new SqlCommand("update TBL_URUNLER set ADET=ADET-@P1 where ID=@P2", bgl.baglanti());
                cmd4.Parameters.AddWithValue("@P1", TxtMiktar.Text);
                cmd4.Parameters.AddWithValue("@P2", TxtUrunID.Text);
                cmd4.ExecuteNonQuery();
                bgl.baglanti().Close();


                MessageBox.Show("Faktura Detay Sistemə Əlavə edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
            

            

            //MÜŞTERİ CARİSİ
            if (TxtFaturaID.Text != "" && CmbTur.Text == "Müşteri")
            {
                double fiyat, miktar, tutar;
                fiyat = Convert.ToDouble(TxtFiyat.Text);
                miktar = Convert.ToDouble(TxtMiktar.Text);
                tutar = miktar * fiyat;
                TxtTutar.Text = tutar.ToString();
                SqlCommand cmd2 = new SqlCommand("insert into TBL_FATURADETAY (URUNAD,MIKTAR,FIYAT,TUTAR,FATURAID) values (@p1,@p2,@p3,@p4,@p5)", bgl.baglanti());
                cmd2.Parameters.AddWithValue("@p1", TxtUrunAd.Text);
                cmd2.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                cmd2.Parameters.AddWithValue("@p3", decimal.Parse((TxtFiyat.Text).ToString()));
                cmd2.Parameters.AddWithValue("@p4", decimal.Parse((TxtTutar.Text).ToString()));
                cmd2.Parameters.AddWithValue("@p5", TxtFaturaID.Text);
                cmd2.ExecuteNonQuery();
                bgl.baglanti().Close();
                listele();
                //HAREKET TABLOSUNA VERİ GİRİŞİ
                SqlCommand cmd3 = new SqlCommand("insert into TBL_MUSTERİHAREKETLER (URUNID,ADET,PERSONEL,MUSTERİ,FIYAT,TOPLAM,FATURAID,TARIH) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8)", bgl.baglanti());
                cmd3.Parameters.AddWithValue("@p1", TxtUrunID.Text);
                cmd3.Parameters.AddWithValue("@p2", TxtMiktar.Text);
                cmd3.Parameters.AddWithValue("@p3", TxtPersonel.Text);
                cmd3.Parameters.AddWithValue("@p4", TxtFirma.Text);
                cmd3.Parameters.AddWithValue("@p5", decimal.Parse((TxtFiyat.Text).ToString()));
                cmd3.Parameters.AddWithValue("@p6", decimal.Parse((TxtTutar.Text).ToString()));
                cmd3.Parameters.AddWithValue("@p7", TxtFaturaID.Text);
                cmd3.Parameters.AddWithValue("@p8", MskTarih.Text);
                cmd3.ExecuteNonQuery();
                bgl.baglanti().Close();

                //STOK SAYISINI AZALTMA
                SqlCommand cmd4 = new SqlCommand("update TBL_URUNLER set ADET=ADET-@P1 where ID=@P2", bgl.baglanti());
                cmd4.Parameters.AddWithValue("@P1", TxtMiktar.Text);
                cmd4.Parameters.AddWithValue("@P2", TxtUrunID.Text);
                cmd4.ExecuteNonQuery();
                bgl.baglanti().Close();


                MessageBox.Show("Faktura Detay Sistemə Əlavə edildi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
                temizle();
            }
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {

            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);  //FrmBankalar da yan kısımdaki groupControlde verileri göstermeye yarar.
            if (dr != null)
            {
                Txtid.Text = dr["FATURABILGIID"].ToString();
                TxtSeri.Text = dr["SERI"].ToString();
                TxtSirano.Text = dr["SIRANO"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                MskSaat.Text = dr["SAAT"].ToString();
                TxtVergiD.Text = dr["VERGIDAIRE"].ToString();
                TxtAlici.Text = dr["ALICI"].ToString();
                TxtTeden.Text = dr["TESLIMEDEN"].ToString();
                TxtTalan.Text = dr["TESLIMALAN"].ToString();

            }
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
           
        }

        private void simpleButton3_Click(object sender, EventArgs e)  //FrmFaturalar silme bölümü.
        {
            SqlCommand cmd = new SqlCommand("Delete TBL_FATURABILGI where FATURABILGIID=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", Txtid.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Faktura Bilgisi Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            temizle();



        }

        private void BtnTemizle_Click_1(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_FATURABILGI set SERI=@P1,SIRANO=@P2,TARIH=@P3,SAAT=@P4,VERGIDAIRE=@P5,ALICI=@P6,TESLIMEDEN=@P7,TESLIMALAN=@P8 where FATURABILGIID=@P9", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", TxtSeri.Text);
            cmd.Parameters.AddWithValue("@p2", TxtSirano.Text);
            cmd.Parameters.AddWithValue("@p3", MskTarih.Text);
            cmd.Parameters.AddWithValue("@p4", MskSaat.Text);
            cmd.Parameters.AddWithValue("@p5", TxtVergiD.Text);
            cmd.Parameters.AddWithValue("@p6", TxtAlici.Text);
            cmd.Parameters.AddWithValue("@p7", TxtTeden.Text);
            cmd.Parameters.AddWithValue("@p8", TxtTalan.Text);
            cmd.Parameters.AddWithValue("@p9", Txtid.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            listele();
            MessageBox.Show("Faktura Bilgisi Yeniləndi", "Güncel", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            temizle();
           
        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmFaturaUrunDetay frm = new FrmFaturaUrunDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if (dr != null)
            {
                frm.id= dr["FATURABILGIID"].ToString();    
            }
            frm.Show();
        }

        private void BtnBul_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Select URUNAD,SATISFIYAT From TBL_URUNLER where ID=@P1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@P1", TxtUrunID.Text);
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                TxtUrunAd.Text = dr[0].ToString();
                TxtFiyat.Text = dr[1].ToString();

            }
            bgl.baglanti().Close();
        }

        private void CmbTur_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
