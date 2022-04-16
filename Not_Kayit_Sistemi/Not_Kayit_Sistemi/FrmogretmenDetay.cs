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

namespace FRMogretmenDetay
{
    public partial class FrmogretmenDetay : Form
    {
        public FrmogretmenDetay()
        {
            InitializeComponent();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=DESKTOP-FHSN1O8;Initial Catalog=DbNotKayıt;Integrated Security=True");
        private void FrmogretmenDetay_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'dbNotKayıtDataSet.TBLDERS' table. You can move, or remove it, as needed.
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);

        }

        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("insert into TBLDERS (OGRNUMARA,OGRAD,OGRSOYAD) values (@p1,@p2,@p3) ",baglanti);
            komut.Parameters.AddWithValue("@p1", msk_numara.Text);
            komut.Parameters.AddWithValue("@p2", txt_ad.Text);
            komut.Parameters.AddWithValue("@p3", txt_soyad.Text);

            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
            komut.ExecuteNonQuery();
            MessageBox.Show("Öğrenci Sisteme Eklendi..");
            
            baglanti.Close();


        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int secilen = dataGridView1.SelectedCells[0].RowIndex;

            msk_numara.Text = dataGridView1.Rows[secilen].Cells[1].Value.ToString();
            txt_ad.Text = dataGridView1.Rows[secilen].Cells[2].Value.ToString();
            txt_soyad.Text = dataGridView1.Rows[secilen].Cells[3].Value.ToString();
            txt_sınav1.Text = dataGridView1.Rows[secilen].Cells[4].Value.ToString();
            txt_sınav2.Text = dataGridView1.Rows[secilen].Cells[5].Value.ToString();
            txt_sınav3.Text = dataGridView1.Rows[secilen].Cells[6].Value.ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            double ortalama, s1, s2, s3;
            string durum;

            s1 = Convert.ToDouble(txt_sınav1.Text);
            s2 = Convert.ToDouble(txt_sınav2.Text);
            s3 = Convert.ToDouble(txt_sınav3.Text);

            ortalama = (s1 + s2 + s3) / 3;
            lbl_ortalama.Text = ortalama.ToString();


            if(ortalama >= 50)
            {
                durum = "True";
            }
            else
            {
                durum = "False";
            }

            baglanti.Open();
            SqlCommand komut = new SqlCommand("update TBLDERS set OGRS1 = @p1,OGRS2 = @p2,OGRS3=@p3,ORTALAMA = @P4,DURUM = @P5 where OGRNUMARA = @P6",baglanti);

            komut.Parameters.AddWithValue("@P1", txt_sınav1.Text);
            komut.Parameters.AddWithValue("@P2", txt_sınav2.Text);
            komut.Parameters.AddWithValue("@P3", txt_sınav3.Text);
            komut.Parameters.AddWithValue("@P4", decimal.Parse(lbl_ortalama.Text));
            komut.Parameters.AddWithValue("@P5", durum);
            komut.Parameters.AddWithValue("@P6", msk_numara.Text);
            komut.ExecuteNonQuery();
            baglanti.Close();

            MessageBox.Show("Öğrenci Notları Güncellendi.. ");
            this.tBLDERSTableAdapter.Fill(this.dbNotKayıtDataSet.TBLDERS);
        }
    }
}
