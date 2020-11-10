using System.Data.SqlClient; //sql library.

namespace Ticari_Otomasyon
{
    class sqlbaglantisi
    {
        public SqlConnection baglanti()
        {
            SqlConnection baglan = new SqlConnection(@"Data Source=WINDOWS-C8FJ4V7; Initial Catalog=Dbo_InsaatZ;Integrated Security=True");
            baglan.Open();
            return baglan;

        }
    }
}
