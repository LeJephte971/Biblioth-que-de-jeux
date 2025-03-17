using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace test_jeux
{
    public partial class Form3 : Form
    {
        // Initialisez la chaîne de connexion avec une valeur valide
        private MySqlConnection conn;
        private string connectionString;
        private DataGridView dataGridView2;
        private object dataGridView1;

        public Form3(MySqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
            // Initialiser le DataGridView avec des colonnes

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Gérez les clics sur les cellules ici si nécessaire
        }

        private void LoadData()
        {
            try
            {
                //MessageBox.Show("Connexion à la base de données établie.");
                string query = "SELECT titre, contenu, regle, condit, nombre_joueurs, nombre_cartes FROM jeux";
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
        private void Ajouter_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifiez si une ligne est sélectionnée
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner une ligne ou saisir les informations à ajouter.");
                    return;
                }

                // Récupérez la ligne sélectionnée
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Préparez la requête d'insertion
                string query = "INSERT INTO jeux (titre, contenu, règle, condit, nombre_joueurs, nombre_cartes) " +
                               "VALUES (@titre, @contenu, @règle, @condit, @nombre_joueurs, @nombres_cartes)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Récupérez les valeurs des cellules
                cmd.Parameters.AddWithValue("@titre", selectedRow.Cells["titre"].Value?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@contenu", selectedRow.Cells["contenu"].Value?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@règle", selectedRow.Cells["règle"].Value?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@condit", selectedRow.Cells["condit"].Value?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@nombre_joueurs", selectedRow.Cells["nombre_joueurs"].Value?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@nombre_cartes", selectedRow.Cells["nombre_cartes"].Value?.ToString() ?? string.Empty);

                // Exécutez la commande SQL
                cmd.ExecuteNonQuery();

                // Rechargez les données après l'ajout
                LoadData();
                MessageBox.Show("Jeu ajouté avec succès !");
            }
            catch (MySqlException sqlEx)
            {
                MessageBox.Show("Erreur SQL : " + sqlEx.Message);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur s'est produite : " + ex.Message);
            }
        }

        private void InitializeComponent()
        {
            this.dataGridView2 = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView2
            // 
            this.dataGridView2.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dataGridView2.Location = new System.Drawing.Point(217, 184);
            this.dataGridView2.Name = "dataGridView2";
            this.dataGridView2.RowHeadersWidth = 51;
            this.dataGridView2.RowTemplate.Height = 24;
            this.dataGridView2.Size = new System.Drawing.Size(240, 150);
            this.dataGridView2.TabIndex = 0;
            // 
            // Form3
            // 
            this.ClientSize = new System.Drawing.Size(1346, 702);
            this.Controls.Add(this.dataGridView2);
            this.Name = "Form3";
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView2)).EndInit();
            this.ResumeLayout(false);

        }
    }
}