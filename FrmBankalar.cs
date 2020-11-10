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
    public partial class FrmBankalar : Form
    {
        public FrmBankalar()
        {
            InitializeComponent();
        }

        sqlbaglantisi bgl = new sqlbaglantisi();

        void listele()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Execute BankaBilgileri ", bgl.baglanti());  //sql tabanında procedur kullanarak buraya çekti =join işlemi

            da.Fill(dt);
            gridControl1.DataSource = dt;
        }
        void sehirlistesi()
        {
            SqlCommand cmd = new SqlCommand("Select sehir From TBL_ILLER", bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Cmbil.Properties.Items.Add(dr[0]);   //combobox yapılışı.

            }
            bgl.baglanti().Close();
        }
        void firmalistesi()
        {
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select ID,AD From TBL_FIRMALAR", bgl.baglanti());
            da.Fill(dt);
            LueFirma.Properties.NullText = "Lütfen Bir Ad Seçiniz";
            LueFirma.Properties.ValueMember = "ID";  //arkaplanda saklı.
            LueFirma.Properties.DisplayMember = "AD"; //Bize gözükecek alan.
            LueFirma.Properties.DataSource = dt;

        }
        void temizle()
        {
            Txtid.Text = "";
            TxtBankaAd.Text = "";
            Cmbil.Text = "";
            Cmbilce.Text = "";
            TxtSube.Text = "";
            MskIban.Text = "";
            MskHesapno.Text = "";
            TxtYetkili.Text = "";
            MskTarih.Text = "";
            TxtHesaptur.Text = "";
            MskTel.Text = "";
            LueFirma.Text = "";

        }
        private void BtnSil_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Delete TBL_BANKALAR where ID=@P1", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", Txtid.Text);
            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Banka Bilgileri Silindi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            listele();
            temizle();
        }

        private void FrmBankalar_Load(object sender, EventArgs e)
        {
            listele();
            sehirlistesi();
            temizle();
            firmalistesi();
        }

        private void gridView1_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);  //FrmBankalar da yan kısımdaki groupControlde verileri göstermeye yarar.
            if (dr != null)
            {
                Txtid.Text = dr["ID"].ToString();
                TxtBankaAd.Text = dr["BANKAADI"].ToString();
                Cmbil.Text = dr["IL"].ToString();
                Cmbilce.Text = dr["ILCE"].ToString();
                TxtSube.Text = dr["SUBE"].ToString();
                MskIban.Text = dr["IBAN"].ToString();
                MskHesapno.Text = dr["HESAPNO"].ToString();
                TxtYetkili.Text = dr["YETKILI"].ToString();
                MskTarih.Text = dr["TARIH"].ToString();
                TxtHesaptur.Text = dr["HESAPTURU"].ToString();
                MskTel.Text = dr["TELEFON"].ToString();
             //   LueFirma.Text = dr["FIRMAID"].ToString();

            }
        }

        private void BtnKaydet_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("insert into TBL_BANKALAR (BANKAADI,IL,ILCE,SUBE,IBAN,HESAPNO,YETKILI,TARIH,HESAPTURU,TELEFON,FIRMAID) values (@p1,@p2,@p3,@p4,@p5,@p6,@p7,@p8,@p9,@p10,@p11)", bgl.baglanti());
            cmd.Parameters.AddWithValue("@p1", TxtBankaAd.Text);
            cmd.Parameters.AddWithValue("@p2", Cmbil.Text);
            cmd.Parameters.AddWithValue("@p3", Cmbilce.Text);
            cmd.Parameters.AddWithValue("@p4", TxtSube.Text);
            cmd.Parameters.AddWithValue("@p5", MskIban.Text);
            cmd.Parameters.AddWithValue("@p6", MskHesapno.Text);
            cmd.Parameters.AddWithValue("@p7", TxtYetkili.Text);
            cmd.Parameters.AddWithValue("@p8", MskTarih.Text);
            cmd.Parameters.AddWithValue("@p9", TxtHesaptur.Text);
            cmd.Parameters.AddWithValue("@p10", MskTel.Text);
            cmd.Parameters.AddWithValue("@p11", LueFirma.EditValue);

            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();

            MessageBox.Show("Banka Bilgileri Eklendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Information);
            listele();
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

        private void BtnTemizle_Click(object sender, EventArgs e)
        {
            temizle();
        }

        private void BtnGuncelle_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand("Update TBL_BANKALAR set BANKAADI=@P1 ,IL=@P2,ILCE=@P3,SUBE=@P4,IBAN=@P5,HESAPNO=@P6,YETKILI=@P7,TARIH=@P8,HESAPTURU=@P9,TELEFON=@P10,FIRMAID=@P12 where ID=@P11", bgl.baglanti());
            cmd.Parameters.AddWithValue("@P1", TxtBankaAd.Text);
            cmd.Parameters.AddWithValue("@P2", Cmbil.Text);
            cmd.Parameters.AddWithValue("@P3", Cmbilce.Text);
            cmd.Parameters.AddWithValue("@P4", TxtSube.Text);
            cmd.Parameters.AddWithValue("@P5", MskIban.Text);
            cmd.Parameters.AddWithValue("@P6", MskHesapno.Text);
            cmd.Parameters.AddWithValue("@P7", TxtYetkili.Text);
            cmd.Parameters.AddWithValue("@P8", MskTarih.Text);
            cmd.Parameters.AddWithValue("@P9", TxtHesaptur.Text);
            cmd.Parameters.AddWithValue("@P10", MskTel.Text);
            cmd.Parameters.AddWithValue("@P11", Txtid.Text);
            cmd.Parameters.AddWithValue("@P12", LueFirma.EditValue);

            cmd.ExecuteNonQuery();
            bgl.baglanti().Close();
            MessageBox.Show("Banka Bilgileri Güncellendi", "Bilgi", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            listele();
            temizle();


        }
    }
}
