using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SoalUAS1
{
    public partial class Form1 : Form
    {
        string connection = "Data Source=jerichosiahaya.database.windows.net;Initial Catalog=SampleDb;Persist Security Info=True;User ID=jerichosiahaya;Password=Kambingjantan25";
        public Form1()
        {
            InitializeComponent();
            this.Text = "Aplikasi Manajemen Mahasiswa";
        }

        // insert mahasiswa
        private void button1_Click(object sender, EventArgs e)
        {
            int nim = Convert.ToInt32(textBox1.Text);
            string nama = textBox2.Text;
            int angkatan = Convert.ToInt32(textBox3.Text);

            SqlConnection sql = new SqlConnection(connection);
            string queryInsert = "insert into Mahasiswa (nim, nama, angkatan, fakultas, prodi) values (@nim, @nama, @angkatan, @fakultas, @prodi)";
            SqlCommand command = new SqlCommand(queryInsert, sql);
            command.Parameters.AddWithValue("@nim", nim);
            command.Parameters.AddWithValue("@nama", nama);
            command.Parameters.AddWithValue("@angkatan", angkatan);
            command.Parameters.AddWithValue("@fakultas", "tes"); // change to combobox
            command.Parameters.AddWithValue("@prodi", "tess"); // change to combobox
            try
            {
                sql.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                MessageBox.Show("Data successfully addedd");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sql.Close();
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            comboBox1.Items.Add("FTI");
            comboBox1.Items.Add("DKV");

            comboBox2.Items.Add("Sistem Informasi");
            comboBox2.Items.Add("Teknik Informatika");


            // fill combobox matkul
            SqlConnection sql = new SqlConnection(connection);
            string queryInsert = "select kode_matkul from Matakuliah";
            SqlCommand command = new SqlCommand(queryInsert, sql);
            sql.Open();
            SqlDataReader dataReader = command.ExecuteReader();
            while (dataReader.Read())
            {
                for (int i = 0; i < dataReader.FieldCount; i++)
                {
                    comboBox3.Items.Add(dataReader.GetString(i));
                }
            }
            sql.Close();
        }

        // insert enrollment
        private void button3_Click(object sender, EventArgs e)
        {
            int nim = Convert.ToInt32(textBox4.Text);
            string kodeMatkul = comboBox3.SelectedItem.ToString();
            SqlConnection sql = new SqlConnection(connection);
            string queryInsert = "insert into Enrollment (nim, kode_matkul) values (@nim, @kode_matkul)";
            SqlCommand command = new SqlCommand(queryInsert, sql);
            command.Parameters.AddWithValue("@nim", nim);
            command.Parameters.AddWithValue("@kode_matkul", kodeMatkul);
            try
            {
                sql.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                MessageBox.Show("Enrollment success!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sql.Close();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int nim = Convert.ToInt32(textBox5.Text);
            SqlConnection sql = new SqlConnection(connection);
            string queryInsert = $"select a.kode_matkul, b.nama_matkul from Enrollment a, Matakuliah b where nim = {nim} and a.kode_matkul = b.kode_matkul";
            SqlCommand command = new SqlCommand(queryInsert, sql);
            try
            {
                sql.Open();
                SqlDataReader dataReader = command.ExecuteReader();
                DataTable data = new DataTable();
                data.Load(dataReader);
                dataGridView1.DataSource = data;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                sql.Close();
            }
        }

        private void generateCrystalReportToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Form2 form = new Form2();
            form.Show();
        }
    }
}
