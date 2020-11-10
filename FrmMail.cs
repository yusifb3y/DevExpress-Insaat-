using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Net;  // mail için gerekli library.
using System.Net.Mail; //mail için gerekli library.

namespace Ticari_Otomasyon
{
    public partial class FrmMail : Form
    {
        public FrmMail()
        {
            InitializeComponent();
        }
        public string mail;
        private void FrmMail_Load(object sender, EventArgs e)
        {
            TxtMail.Text = mail; //mail değişikliğinden gelecek değer.
        }

        private void BtnGonder_Click(object sender, EventArgs e)
        {
            MailMessage mesajim = new MailMessage(); //yeni mesaj için gerekli.
            SmtpClient istemci = new SmtpClient();  //istemci nesnesi üretiyoruz.kapıyı tıklatma gibi düşünebiliriz.
            istemci.Credentials = new System.Net.NetworkCredential("gokhanyzc9797@gmail.com", "gy67699297"); //istemcinin ve ağ kimliğini buluyor.
            istemci.Port = 587;  //türkiyede kullanılan port adresi 587
            istemci.Host = "smtp.gmail.com";   //istemicinin sunucusu
            istemci.EnableSsl = true; //mesajı şifrelesin mi evet.
            mesajim.To.Add(TxtMail.Text); //MESAJIn kime gönderileceği
            mesajim.From = new MailAddress("gokhanyzc9797@gmail.com"); //kimden gönderildiği
            mesajim.Subject = TxtKonu.Text; //mesajın konusu
            mesajim.Body = RchMesaj.Text; //mesajın içeriği
            istemci.Send(mesajim); // mesajı gönderme işlemi
            MessageBox.Show("Mesaj Gönderildi.");
        }
    }
}
