using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneZiyaretTakip
{
    public partial class Form1 : Form
    {
        void HastaListele()
        {
            SqlDataAdapter da =
                new SqlDataAdapter(
                "SELECT * FROM Hasta",
                db.Baglanti());

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }

        public Form1()
        {
            InitializeComponent();
        }

        Database db = new Database();

        private void txtTC_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar)
               && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void btnKaydet_Click(object sender, EventArgs e)
        {
            if (txtTC.Text.Length != 11)
            {
                MessageBox.Show(
                    "TC Kimlik No 11 haneli olmalıdır.");

                return;
            }

            SqlCommand cmd = new SqlCommand(
                "INSERT INTO Hasta(Ad,Soyad,TCNo,OdaNo) VALUES(@ad,@soyad,@tc,@oda)",
                db.Baglanti());

            cmd.Parameters.AddWithValue("@ad", txtAd.Text);
            cmd.Parameters.AddWithValue("@soyad", txtSoyad.Text);
            cmd.Parameters.AddWithValue("@tc", txtTC.Text);
            cmd.Parameters.AddWithValue("@oda", txtOda.Text);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Hasta kaydedildi.");

            HastaListele();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            HastaListele();
        }
    }
}
