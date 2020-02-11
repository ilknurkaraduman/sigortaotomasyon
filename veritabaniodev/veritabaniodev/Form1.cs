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
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        SqlConnection baglanti = new SqlConnection(@"Data Source=.\SENAE;Initial Catalog=veritabani;User ID=sa;Password=123456");


        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            try
            {
                baglanti.Open();
                SqlCommand komut = new SqlCommand("Select * from login where kullaniciad=@kullaniciad and sifre=@sifre", baglanti);
                komut.Parameters.Add(new SqlParameter("@kullaniciad", textBox1.Text));
                komut.Parameters.Add(new SqlParameter("@sifre", textBox2.Text));
                SqlDataReader dr = komut.ExecuteReader();


                if (dr.Read())
                {
                    Form2 fr = new Form2();
                    fr.Show();
                }

                else
                {
                    MessageBox.Show("Hatali Giris!");
                }

                baglanti.Close();

            }
            catch (Exception)
            {
                MessageBox.Show("Hatali Giris!");
            }
        }
    }
}
