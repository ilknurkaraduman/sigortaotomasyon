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

namespace veritabaniodev
{
    public partial class Sigortaekle : Form
    {
        public Sigortaekle()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SENAE;Initial Catalog=veritabani;User ID=sa;Password=123456");

        SqlConnection con;
        SqlDataAdapter da;
        DataSet ds;
        SqlCommandBuilder cmdb;


        private void Sigortaekle_Load(object sender, EventArgs e)
        {
            // TODO: Bu kod satırı 'veritabaniDataSet.sigorta' tablosuna veri yükler. Bunu gerektiği şekilde taşıyabilir, veya kaldırabilirsiniz.
            this.sigortaTableAdapter.Fill(this.veritabaniDataSet.sigorta);


            con = new SqlConnection(@"Data Source=.\SENAE;Initial Catalog=veritabani;User ID=sa;Password=123456");
            con.Open();
            da = new SqlDataAdapter("Select * from sigorta", con);
            cmdb = new SqlCommandBuilder(da);
            ds = new DataSet();
            da.Fill(ds, "sigorta");
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
            komut = new SqlCommand("Insert Into sigorta(sigorta_turu) values(@sigorta_turu)", baglanti);
            komut.Parameters.AddWithValue("@sigorta_turu", textBox1.Text);         
            komut.ExecuteNonQuery();
            verilerigoster("select *from sigorta");

            baglanti.Close();
            textBox1.Clear();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            
            baglanti.Open();
            SqlCommand komut = new SqlCommand("delete from sigorta where Id=@Id", baglanti);
            komut.Parameters.AddWithValue("@Id", textBox2.Text);
            komut.ExecuteNonQuery();
            verilerigoster("select *from sigorta");
            baglanti.Close();
            textBox2.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {


            da.Update(ds, "sigorta");
            MessageBox.Show("Kayıt güncellendi");
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
