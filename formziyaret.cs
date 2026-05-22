using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HastaneZiyaretTakip
{
    public partial class formziyaret : Form
    {
        void HastaGetir()
        {
            SqlDataAdapter da =
                new SqlDataAdapter(
                "SELECT HastaID, Ad + ' ' + Soyad AS AdSoyad FROM Hasta",
                db.Baglanti());

            DataTable dt = new DataTable();

            da.Fill(dt);

            cmbHasta.DisplayMember = "AdSoyad";
            cmbHasta.ValueMember = "HastaID";

            cmbHasta.DataSource = dt;
        }

        void ZiyaretciGetir()
        {
            SqlDataAdapter da =
                new SqlDataAdapter(
                "SELECT ZiyaretciID, Ad + ' ' + Soyad AS AdSoyad FROM Ziyaretci",
                db.Baglanti());

            DataTable dt = new DataTable();

            da.Fill(dt);

            cmbZiyaretci.DisplayMember = "AdSoyad";
            cmbZiyaretci.ValueMember = "ZiyaretciID";

            cmbZiyaretci.DataSource = dt;
        }

        void AktifZiyaretleriListele()
        {
            SqlDataAdapter da =
                new SqlDataAdapter(
                @"SELECT
        
                z.ZiyaretID,
                v.Ad + ' ' + v.Soyad AS Ziyaretci,
                h.Ad + ' ' + h.Soyad AS Hasta,
                z.GirisSaati
        

                FROM Ziyaret z
        

                INNER JOIN Ziyaretci v
        
                ON z.ZiyaretciID = v.ZiyaretciID
        

                INNER JOIN Hasta h
        
                ON z.HastaID = h.HastaID
        

                WHERE z.CikisSaati IS NULL\",
        
                db.Baglanti());

            DataTable dt = new DataTable();

            da.Fill(dt);

            dataGridView1.DataSource = dt;
        }



        Database db = new Database();
        public formziyaret()
        {
            InitializeComponent();
        }

        private void btnGiris_Click(object sender, EventArgs e)
        {
            SqlCommand cmd = new SqlCommand(
        "INSERT INTO Ziyaret(HastaID,ZiyaretciID,GirisSaati) VALUES(@hasta,@ziyaretci,@giris)",
        db.Baglanti());

            cmd.Parameters.AddWithValue("@hasta", cmbHasta.SelectedValue);
            cmd.Parameters.AddWithValue("@ziyaretci", cmbZiyaretci.SelectedValue);

            cmd.Parameters.AddWithValue("@giris", DateTime.Now);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Ziyaret girişi yapıldı.");

            AktifZiyaretleriListele();
        }
        
        private void btnCikis_Click(object sender, EventArgs e)
        {
            int id =
        Convert.ToInt32(
        dataGridView1.CurrentRow.Cells[0].Value);

            SqlCommand cmd = new SqlCommand(
                "UPDATE Ziyaret SET CikisSaati=@cikis WHERE ZiyaretID=@id",
                db.Baglanti());

            cmd.Parameters.AddWithValue("@cikis", DateTime.Now);

            cmd.Parameters.AddWithValue("@id", id);

            cmd.ExecuteNonQuery();

            MessageBox.Show("Çıkış işlemi yapıldı.");

            AktifZiyaretleriListele();
        }

        private void formziyaret_Load(object sender, EventArgs e)
        {

        }
    }
}
