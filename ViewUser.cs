using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
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

namespace test_jeux
{
    public partial class ViewUser : Form
    {
        private MySqlConnection conn;
        private string connectionString;
        public ViewUser(MySqlConnection conn)
        {
            InitializeComponent();
            this.conn = conn;
        }

        private void LoadData()
        {
            try
            {
                string query = "SELECT titre, contenu, règle, condit, nombre_joueurs, nombre_cartes FROM jeux";
                MySqlCommand cmd = new MySqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = query;
                MySqlDataAdapter adapter = new MySqlDataAdapter();
                adapter.SelectCommand = cmd;
                using (DataTable dt = new DataTable())
                {
                    adapter.Fill(dt);
                    dataGridView1.DataSource = dt;
                }
            }
            catch (MySqlException ex)
            {
                MessageBox.Show("Erreur de base de données 1 : " + ex.Message);
            }
        }

        private void ViewUser_Load(object sender, EventArgs e)
        {
            LoadData();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Connecte connecte = new Connecte(); 
            connecte.Show();              
            this.Hide();               
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void TxtFilter_TextChanged_1(object sender, EventArgs e)
        {
            string filterText = TxtFilter.Text.Trim();
            if (string.IsNullOrEmpty(filterText))
            {
                LoadData(); // Rechargez toutes les données si le filtre est vide
            }
            else
            {
                try
                {
                    string query = @"
                        SELECT titre, contenu, règle, condit, nombre_joueurs, nombre_cartes
                        FROM jeux
                        WHERE titre LIKE @filter
                        OR contenu LIKE @filter
                        OR règle LIKE @filter
                        OR condit LIKE @filter
                        OR nombre_joueurs LIKE @filter
                        OR nombre_cartes LIKE @filter";
                    MySqlCommand cmd = new MySqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@filter", "%" + filterText + "%");
                    MySqlDataAdapter adapter = new MySqlDataAdapter();
                    adapter.SelectCommand = cmd;
                    using (DataTable dt = new DataTable())
                    {
                        adapter.Fill(dt);
                        dataGridView1.DataSource = dt;
                    }
                }
                catch (MySqlException ex)
                {
                    MessageBox.Show("Erreur de base de données 2 : " + ex.Message);
                }
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form1= new Form1(); // Crée une instance de Form2
            form1.Show();              // Affiche Form2
            this.Hide();               // Cache Form1 (optionnel)
        }
    }
}
