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
    public partial class FrmLstJeux : Form
    {
        // Initialisez la chaîne de connexion avec une valeur valide
        private MySqlConnection conn;
        private string connectionString;
      
        public FrmLstJeux(MySqlConnection conn)
        {
            this.conn = conn;
            InitializeComponent();
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

        private void FrmLstJeux_Load(object sender, EventArgs e)
        {
            // Rechargez les données après l'ajout
            LoadData();
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
                                "VALUES (@titre, @contenu, @règle, @condit, @nombre_joueurs, @nombre_cartes)";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Récupérez les valeurs des cellules
                //cmd.Parameters.AddWithValue("@id", selectedRow.Cells["id"].Value?.ToString() ?? string.Empty);
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
                MessageBox.Show(" Ce Jeu à été ajouté avec succès !","Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifiez si une ligne est sélectionnée
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner une ligne à supprimer.");
                    return;
                }

                // Récupérez la ligne sélectionnée
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Vérifiez que la cellule "titre" contient une valeur valide
                object cellValue = selectedRow.Cells["titre"].Value;
                if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    MessageBox.Show("Impossible de supprimer : le titre est introuvable ou vide.");
                    return;
                }

                string titre = cellValue.ToString();

                // Préparez la requête de suppression
                string query = "DELETE FROM jeux WHERE titre = @titre";
                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Ajoutez le paramètre pour le titre
                cmd.Parameters.AddWithValue("@titre", titre);

                // Demandez confirmation avant de supprimer
                DialogResult confirmation = MessageBox.Show("Êtes-vous sûr de vouloir supprimer ce jeux ?",
                                                             "Confirmation",
                                                             MessageBoxButtons.YesNo,
                                                             MessageBoxIcon.Warning);

                if (confirmation == DialogResult.Yes)
                {
                    // Exécutez la commande SQL
                    cmd.ExecuteNonQuery();

                    // Rechargez les données après suppression
                    LoadData();
                    MessageBox.Show(" Le jeu à été supprimé avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
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
        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void txtFilter_TextChanged(object sender, EventArgs e)
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

        private void panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(); // Crée une instance de Form2
            form1.Show();              // Affiche Form2
            this.Hide();               // Cache Form1 (optionnel)
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifiez si une ligne est sélectionnée
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner une ligne à modifier.");
                    return;
                }

                // Récupérez la ligne sélectionnée
                DataGridViewRow selectedRow = dataGridView1.SelectedRows[0];

                // Vérifiez que la cellule "titre" contient une valeur valide
                object cellValue = selectedRow.Cells["titre"].Value;
                if (cellValue == null || string.IsNullOrWhiteSpace(cellValue.ToString()))
                {
                    MessageBox.Show("Impossible de modifier : le titre est introuvable ou vide.");
                    return;
                }

                string titre = cellValue.ToString();

                // Préparez la requête de mise à jour
                string query = "UPDATE jeux SET contenu = @contenu, règle = @règle, " +
                               "condit = @condit, nombre_joueurs = @nombre_joueurs, nombre_cartes = @nombre_cartes " +
                               "WHERE titre = @titre";

                MySqlCommand cmd = new MySqlCommand(query, conn);

                // Ajoutez les paramètres
                cmd.Parameters.AddWithValue("@titre", titre);
                cmd.Parameters.AddWithValue("@contenu", selectedRow.Cells["contenu"].Value?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@règle", selectedRow.Cells["règle"].Value?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@condit", selectedRow.Cells["condit"].Value?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@nombre_joueurs", selectedRow.Cells["nombre_joueurs"].Value?.ToString() ?? string.Empty);
                cmd.Parameters.AddWithValue("@nombre_cartes", selectedRow.Cells["nombre_cartes"].Value?.ToString() ?? string.Empty);

                // Exécutez la commande SQL
                int rowsAffected = cmd.ExecuteNonQuery();

                if (rowsAffected > 0)
                {
                    // Rechargez les données après la modification
                    LoadData();
                    MessageBox.Show("Le jeu a été modifié avec succès !", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Aucune modification effectuée. Vérifiez les valeurs saisies.", "Attention", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
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
    }
}
