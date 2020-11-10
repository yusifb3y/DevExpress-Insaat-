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
    public partial class FrmFirmalar : Form
    {
        public FrmFirmalar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();
        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select * From TBL_FIRMALAR",bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;
        }

        void sehirlistesi()
        {
            SqlCommand cmd = new SqlCommand("Select * From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[1]);
            }
            bgl.baglanti().Close();
        }

        void temizle()
        {
            TxtAd.Text = "";
            Txtid.Text = "";
            TxtKod1.Text = "";
            TxtKod2.Text = "";
            TxtKod3.Text = "";
            TxtMail.Text = "";
            TxtSektor.Text = "";
            TxtVergiD.Text = "";
            TxtYetkili.Text = "";
            TxtYGorev.Text = "";
            MskFax.Text = "";
            MskTC.Text = "";
            MskTel1.Text = "";
            MskTel2.Text = "";
            MskTel3.Text = "";
            RchAdres.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";

          
            TxtAd.Focus();  //imleci ad kısmına odaklıyor.

        }
       /* void carikodaciklamalar()
        { 
            //özel kod için açıklama yeri için yazılan sql kodu

            SqlCommand cmd = new SqlCommand("Select FIRMAKOD1 From TBL_KODLAR", bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                RchKod1.Text = dr[0].ToString();
            }
            bgl.baglanti().Close();

        }
       */
        private void labelControl20_Click(object sender, EventArgs e)
        {

        }

        private void labelControl21_Click(object sender, EventArgs e)
        {

        }

        private void FrmFirmalar_Load(object sender, EventArgs e)
        {
            firmalistesi();
            temizle();
            sehirlistesi();
           // carikodaciklamalar();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);  //FrmFirmalar da yan kısımdaki groupControlde verileri göstermeye yarar.
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtAd.Text = dr["AD"].ToString();
                TxtYGorev.Text = dr["YETKILISTATU"].ToString();
                TxtYetkili.Text = dr["YETKILIADSOYAD"].ToString();
                MskTC.Text = dr["YETKILITC"].ToString();
                TxtSektor.Text = dr["SEKTOR"].ToString();
                MskTel1.Text = dr["TELEFON1"].ToString();
                MskTel2.Text = dr["TELEFON2"].ToString();
                MskTel3.Text = dr["TELEFON3"].ToString();
                TxtMail.Text = dr["MAIL"].ToString();
                MskFax.Text = dr["FAX"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                TxtVergiD.Text = dr["VERGIDAIRE"].ToString();
                RchAdres.Text = dr["ADRES"].ToString();
                TxtKod1.Text = dr["OZELKOD1"].ToString();
                TxtKod2.Text = dr["OZELKOD2"].ToString();
                TxtKod3.Text = dr["OZELKOD3"].ToString();
            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into TBL_FIRMALAR (AD,YETKILISTATU,YETKILIADSOYAD,YETKILITC,SEKTOR,TELEFON1,TELEFON2,TELEFON3,MAIL,FAX,IL,ILCE,VERGIDAIRE,ADRES,OZELKOD1,OZELKOD2,OZELKOD3) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11,@p12,@p13,@p14,@p15,@p16,@p17)", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtYGorev.Text);
            cmd.Parameters.AddWithValue("@p3", TxtYetkili.Text);
            cmd.Parameters.AddWithValue("@p4", MskTC.Text);
            cmd.Parameters.AddWithValue("@p5", TxtSektor.Text);
            cmd.Parameters.AddWithValue("@p6", MskTel1.Text);
            cmd.Parameters.AddWithValue("@p7", MskTel2.Text);
            cmd.Parameters.AddWithValue("@p8", MskTel3.Text);
            cmd.Parameters.AddWithValue("@p9", TxtMail.Text);
            cmd.Parameters.AddWithValue("@p10", MskFax.Text);
            cmd.Parameters.AddWithValue("@p11", Cmbil.Text);
            cmd.Parameters.AddWithValue("@p12", Cmbilce.Text);
            cmd.Parameters.AddWithValue("@p13", TxtVergiD.Text);
            cmd.Parameters.AddWithValue("@p14", RchAdres.Text);
            cmd.Parameters.AddWithValue("@p15", TxtKod1.Text);
            cmd.Parameters.AddWithValue("@p16", TxtKod2.Text);
            cmd.Parameters.AddWithValue("@p17", TxtKod3.Text);

            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Siyahıya Əlavə edildi .", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            firmalistesi();
            temizle();
            
        }

        private void Cmbil_SelectedIndexChanged(object sender, EventArgs e)
        {
            Cmbilce.Properties.Items.Clear(); // herhangi bir il seçildiğinde ilçeler comboboxta daha önce seçilmiş ilçeleri temizlesin.
            SqlCommand cmd = new SqlCommand("Select ilce From TBL_ILCELER where sehir=@p1", bgl.baglanti());   //her ilin id'nosuna göre ilçeleri seçmeye yarar.
            cmd.Parameters.AddWithValue("@p1", Cmbil.SelectedIndex + 1);  //+1 yaptık çünkü sqlde id 1 den başlıyor ama comboboxda 0 dan başlıyor o yüzden 1 den başlattık.
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cmbilce.Properties.Items.Add(dr[0]);
            }
            bgl.baglanti().Close();
        }

        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete From TBL_FIRMALAR where ID=@p1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", Txtid.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgiləri Silindi", "Uyarı", MessageBoxButtons.OK, MessageBoxIcon.Error);
            
            firmalistesi();
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("update TBL_FIRMALAR set AD=@P1,YETKILISTATU=@P2,YETKILIADSOYAD=@P3,TELEFON1=@P4,TELEFON2=@P5,TELEFON3=@P6,MAIL=@P7,FAX=@P8,IL=@P9,ILCE=@P10,VERGIDAIRE=@P11,ADRES=@P12,YETKILITC=@P13,OZELKOD1=@P14,OZELKOD2=@P15,OZELKOD3=@P16,SEKTOR=@P17 where ID=@P18",bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", TxtAd.Text);
            cmd.Parameters.AddWithValue("@p2", TxtYGorev.Text);
            cmd.Parameters.AddWithValue("@p3", TxtYetkili.Text);
            cmd.Parameters.AddWithValue("@p4", MskTel1.Text);
            cmd.Parameters.AddWithValue("@p5", MskTel2.Text);
            cmd.Parameters.AddWithValue("@p6", MskTel3.Text);
            cmd.Parameters.AddWithValue("@p7", TxtMail.Text);
            cmd.Parameters.AddWithValue("@p8", MskFax.Text);
            cmd.Parameters.AddWithValue("@p9", Cmbil.Text);
            cmd.Parameters.AddWithValue("@p10", Cmbilce.Text);
            cmd.Parameters.AddWithValue("@p11", TxtVergiD.Text);
            cmd.Parameters.AddWithValue("@p12", RchAdres.Text);
            cmd.Parameters.AddWithValue("@p13", MskTC.Text);
            cmd.Parameters.AddWithValue("@p14", TxtKod1.Text);
            cmd.Parameters.AddWithValue("@p15", TxtKod2.Text);
            cmd.Parameters.AddWithValue("@p16", TxtKod3.Text);
            cmd.Parameters.AddWithValue("@p17", TxtSektor.Text);
            cmd.Parameters.AddWithValue("@p18", Txtid.Text);


            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Firma Bilgileri Yeniləndi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            firmalistesi();
            temizle();
        }

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();

        }
    }
     }
    
    

