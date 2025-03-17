using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace test_jeux
{
    public partial class Connecte : Form
    {
        public Connecte()
        {
            InitializeComponent();
        }

        // Méthode pour hacher un mot de passe
        private string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                // Convertir le mot de passe en tableau de bytes
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Convertir le tableau de bytes en une chaîne hexadécimale
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string user = txtId.Text;
            string mdp = txtMdp.Text;

            // Hacher le mot de passe saisi
            string hashedPassword = HashPassword(mdp);

            // Chaîne de connexion (assurez-vous que les identifiants sont corrects)
            string connectionString = "Server=mysql-crabes-projet.alwaysdata.net;Database=crabes-projet_971;" +
                                     "User ID=391593_josephjef;Password=Jojomadoo971;";
            try
            {
                MySqlConnection conn = new MySqlConnection(connectionString);
                // Tenter d'ouvrir la connexion
                conn.Open();

                // Requête pour vérifier l'utilisateur avec le mot de passe haché
                string query = "SELECT role_id FROM users WHERE username = @username AND mot_de_passe = @password";
                MySqlCommand cmd = new MySqlCommand(query, conn);
                cmd.Parameters.AddWithValue("@username", user);
                cmd.Parameters.AddWithValue("@password", hashedPassword);

                int role = (int)cmd.ExecuteScalar();
                MessageBox.Show("Bienvenue dans la Bibliothèque de jeux ! ", "Bienvenue", MessageBoxButtons.OK, MessageBoxIcon.Information);

                if (role == 1)
                {
                    FrmLstJeux frmLstJeux = new FrmLstJeux(conn); // Crée une instance de FrmLstJeux
                    frmLstJeux.Show();  // Affiche FrmLstJeux
                    this.Hide();
                }
                else
                {
                    ViewUser viewUser = new ViewUser(conn); // Crée une instance de ViewUser
                    viewUser.Show();  // Affiche ViewUser
                    this.Hide();
                }
            }
            catch (Exception ex)
            {
                // Afficher l'erreur en cas d'échec
                MessageBox.Show($"Erreur lors de la connexion : {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {
            // Gestion de l'événement du clic sur le label (si nécessaire)
        }

        private void label3_Click(object sender, EventArgs e)
        {
            // Gestion de l'événement du clic sur le label (si nécessaire)
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            // Gestion de l'événement de peinture du panel (si nécessaire)
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            // Gestion de l'événement de peinture du panel (si nécessaire)
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            Form1 form1 = new Form1(); // Crée une instance de Form1
            form1.Show();              // Affiche Form1
            this.Hide();               // Cache la fenêtre actuelle (optionnel)
        }
    }
}