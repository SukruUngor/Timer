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

namespace sukru1
{
    public partial class Form1 : Form
    {
        int saniye = 0;
        int salise = 0;
        int dakika = 0;
        public Form1()
        {
            InitializeComponent();
        }
        static string constring = "Data Source=MSI\\SUKRU;Initial Catalog=sukru;Integrated Security=True;";
        SqlConnection connect = new SqlConnection(constring);
        private void Form1_Load(object sender, EventArgs e)
        {
             
        }

        private void baslat_Click(object sender, EventArgs e)
        {
            timer.Start();
        }

        private void durdur_Click(object sender, EventArgs e)
        {
            timer.Stop();
            try
            {
                if (connect.State == ConnectionState.Closed)
                { 
                    connect.Open(); 

                }
                string kayit = "insert into zamankayitlari(saniye,salise,dakika,tarih) values(@saniye,@salise,@dakika,@tarih)";
                SqlCommand komut = new SqlCommand(kayit, connect);
                komut.Parameters.AddWithValue("@saniye", label2.Text);
                komut.Parameters.AddWithValue("@salise", label3.Text);
                komut.Parameters.AddWithValue("@dakika", label1.Text);
                DateTime bugun = DateTime.Now;
                komut.Parameters.AddWithValue("@tarih", bugun.ToString());

                komut.ExecuteNonQuery();
                connect.Close();
                MessageBox.Show("Kayıt Eklendi");




            }
            catch(Exception hata)
            {
                MessageBox.Show("Kayıt Yüklenirken Hata Geldi" + hata.Message);
            }
        }

        private void timer_Tick(object sender, EventArgs e)
        {
            salise++;
            label3.Text = salise.ToString();
            if (salise == 60)
            {
                saniye++;
                salise = 0;
            }
            label2.Text = saniye.ToString();
            if (saniye == 60)
            {
                dakika++;
                saniye = 0;
            }
            label1.Text = dakika.ToString();
        }
    }
}
