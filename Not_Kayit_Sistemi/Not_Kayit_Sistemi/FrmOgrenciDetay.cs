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

namespace ogrenciDetay
{
    public partial class FrmOgrenciDetay : Form
    {
        public FrmOgrenciDetay()
        {
            InitializeComponent();
        }
        public string numara;



        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FHSN1O8;Initial Catalog=DbNotKayıt;Integrated Security=True");

        private void FrmOgrenciDetay_Load(object sender, EventArgs e)
        {
            lblNumara.Text = numara;

            baglanti.Open();
            SqlCommand komut = new SqlCommand("Select * from TBLDERS where OGRNUMARA = @p1", baglanti);
            komut.Parameters.AddWithValue("@p1", numara) ;
            SqlDataReader dr = komut.ExecuteReader();
            while (dr.Read())
            {
                lbl_adSoyad.Text = dr[2].ToString() + " " + dr[3].ToString();
                lbl_sinav1.Text = dr[4].ToString();
                lbl_sinav2.Text = dr[5].ToString();
                lbl_sinav3.Text = dr[6].ToString();
                lbl_ortalama.Text = dr[7].ToString();
                lbl_durum.Text = dr[8].ToString();
                
            }

            baglanti.Close();

        }
    }
}
