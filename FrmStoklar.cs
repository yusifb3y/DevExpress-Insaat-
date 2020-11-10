﻿using System;
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
    public partial class FrmStoklar : Form
    {
        public FrmStoklar()
        {
            InitializeComponent();
        }
        sqlbaglantisi bgl = new sqlbaglantisi();

        private void FrmStoklar_Load(object sender, EventArgs e)
        {
            //DENEME
            //series=seri points dediği ise konumu,koordinatları.
            //chartControl1.Series["Series 1"].Points.AddPoint("İstanbul", 4);
            //chartControl1.Series["Series 1"].Points.AddPoint("İzmir", 8);
            //chartControl1.Series["Series 1"].Points.AddPoint("Ankara", 6);
            //chartControl1.Series["Series 1"].Points.AddPoint("Adana", 5);

            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter("Select URUNAD,SUM(ADET) AS 'Miktar'  From TBL_URUNLER Group BY URUNAD", bgl.baglanti());
            da.Fill(dt);
            gridControl1.DataSource = dt;

            //charta stok miktarı listeleme
            SqlCommand cmd = new SqlCommand("Select URUNAD,SUM(ADET) AS 'Miktar'  From TBL_URUNLER Group BY URUNAD", bgl.baglanti());
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                chartControl1.Series["Series 1"].Points.AddPoint(Convert.ToString(dr[0]),int.Parse(dr[1].ToString()));

            }
            bgl.baglanti().Close();

            
           

        }

        private void gridView1_DoubleClick(object sender, EventArgs e)
        {
            FrmStokDetay frm = new FrmStokDetay();
            DataRow dr = gridView1.GetDataRow(gridView1.FocusedRowHandle);
            if(dr != null)
            {
                frm.ad= dr["URUNAD"].ToString();
            }
            frm.Show();
        }
    }
}
