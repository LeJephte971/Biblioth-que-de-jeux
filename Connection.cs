using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 

namespace test_jeux
{
    using System;
    using System.Data;
    using System.Windows.Forms;
    using MySql.Data.MySqlClient;
  

    namespace AlwaysDataConnection
    {
        public class Connection: Connecte
        {
            private MySqlConnection connection;
            private void button2_Click(object sender, EventArgs e)
            {
                string connectionString = "Server=mysql-crabes-projet.alwaysdata.net;Database=crabes-projet_971;User ID=391593_josephjef;Password=Jojomadoo971;";

                using (MySqlConnection conn = new MySqlConnection(connectionString))
                {
                    try
                    {
                        conn.Open();
                        MessageBox.Show("Connexion réussie !");
                    }
                    catch (MySqlException ex) when (ex.Number == 1042) // Erreur réseau ou serveur introuvable
                    {
                        MessageBox.Show("Erreur réseau : Impossible de se connecter au serveur.");
                    }
                    catch (MySqlException ex) when (ex.Number == 1045) // Erreur d'authentification
                    {
                        MessageBox.Show("Erreur d'authentification : Vérifiez vos identifiants.");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Erreur inattendue : " + ex.Message);
                    }
                }
            }

            private void InitializeComponent()
            {
            this.SuspendLayout();
            // 
            // Connection
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.ClientSize = new System.Drawing.Size(1902, 1033);
            this.Name = "Connection";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.Connection_Load);
            this.ResumeLayout(false);

            }
            private void Connection_Load(object sender, EventArgs e)
            {

            }
        }
    }
}
