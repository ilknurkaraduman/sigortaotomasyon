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
    public partial class Musteriekle : Form
    {
        public Musteriekle()
        {
            InitializeComponent();
        }
        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SENAE;Initial Catalog=veritabani;User ID=sa;Password=123456");

        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;



        private void Musteriekle_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'veritabaniDataSet.musteri' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.musteriTableAdapter.Fill(this.veritabaniDataSet.musteri);

            con = new SqlConnection(@"Data Source=.\SENAE;Initial Catalog=veritabani;User ID=sa;Password=123456");
            con.Open();
            da = new SqlDataAdapter("Select * from musteri", con);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "musteri");
            dataGridView1.DataSource = ds.Tables[0];
            con.Close();


        }
      
        SqlCommand komut;

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
            komut = new SqlCommand("Insert Into musteri(ad,soyad,tel,adres) values(@ad,@soyad,@tel,@adres)", baglanti);
            komut.Parameters.AddWithValue("@ad",textBox1.Text);
            komut.Parameters.AddWithValue("@soyad", textBox2.Text);
            komut.Parameters.AddWithValue("@tel", textBox3.Text);
            komut.Parameters.AddWithValue("@adres", textBox4.Text);
            komut.ExecuteNonQuery();
            verilerigoster("select *from musteri");        
            baglanti.Close();
            textBox1.Clear();
            textBox2.Clear();
            textBox3.Clear();
            textBox4.Clear();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from musteri where Id=@Id",baglanti);
            komut.Parameters.AddWithValue("@Id", textBox5.Text);
            komut.ExecuteNonQuery();
            verilerigoster("select *from musteri");
            baglanti.Close();
            textBox5.Clear();

        }


       

        private void button2_Click(object sender, EventArgs e)
        {
            
            da.Update(ds, "musteri");
            MessageBox.Show("Kayıt güncellendi");
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
