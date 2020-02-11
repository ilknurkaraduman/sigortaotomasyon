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
using System.Data.OleDb;

namespace veritabaniodev
{
    public partial class Policeekle : Form
    {
        public Policeekle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SENAE;Initial Catalog=veritabani;User ID=sa;Password=123456");
        SqlConnection bag = new SqlConnection(@"Data Source=.\SENAE;Initial Catalog=veritabani;User ID=sa;Password=123456");

        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;

        private void Policeekle_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'veritabaniDataSet.police' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.policeTableAdapter.Fill(this.veritabaniDataSet.police);

            comboBox1.Items.Clear();
            comboBox2.Items.Clear();
            SqlDataReader oku;
            baglanti.Open();
            komut.Connection = baglanti;

            komut.CommandText = "select * from musteri";           
           oku = komut.ExecuteReader();
            while(oku.Read())
            {
                comboBox1.Items.Add(oku[1].ToString());
                comboBox2.Items.Add(oku[2].ToString());
                
            }
            
           
            comboBox3.Items.Clear();
            SqlDataReader okuu;
            bag.Open();
            kmt.Connection = bag;

            kmt.CommandText = "select * from sigorta";
            okuu = kmt.ExecuteReader();
            while (okuu.Read())
            {
               
                comboBox3.Items.Add(okuu[1].ToString());

            }
            bag.Close();
            baglanti.Close();


            con = new SqlConnection(@"Data Source=.\SENAE;Initial Catalog=veritabani;User ID=sa;Password=123456");
            con.Open();
            da = new SqlDataAdapter("Select * from police", con);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "police");
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();


        }

        SqlCommand komut=new SqlCommand();
        SqlCommand kmt = new SqlCommand();


        public void verilerigoster(string veriler)
        {
            SqlDataAdapter da = new SqlDataAdapter(veriler, baglanti);
            DataSet ds = new DataSet();
            da.Fill(ds);
            dataGridView1.DataSource = ds.Tables[0];
        }


        private void button1_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            komut = new SqlCommand("Insert Into police(m_ad,m_soyad,sigorta_tur,bitis_tarihi,fiyat) values(@m_ad,@m_soyad,@sigorta_tur,@bitis_tarihi,@fiyat)", baglanti);
            komut.Parameters.AddWithValue("@m_ad", comboBox1.Text);
            komut.Parameters.AddWithValue("@m_soyad", comboBox2.Text);
            komut.Parameters.AddWithValue("@sigorta_tur", comboBox3.Text);
            komut.Parameters.AddWithValue("@bitis_tarihi", textBox1.Text);
            komut.Parameters.AddWithValue("@fiyat", textBox2.Text);
            komut.ExecuteNonQuery();
            verilerigoster("select *from police");
            baglanti.Close();
            textBox1.Clear();
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from police where Id=@Id", baglanti);
            komut.Parameters.AddWithValue("@Id", textBox3.Text);
            komut.ExecuteNonQuery();
            verilerigoster("select *from police");
            baglanti.Close();
            textBox3.Clear();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            da.Update(ds, "police");
            MessageBox.Show("Kayıt güncellendi");
        }
    }
}
