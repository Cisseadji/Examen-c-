using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionSchoolApp.Forms;

namespace GestionSchoolApp
{
    public partial class FormAcueil : Form
    {
        private string userRole;

        public FormAcueil(string role)
        {
            InitializeComponent();
            userRole = role;
            GérerAccèsUtilisateurs();
        }


        private void GérerAccèsUtilisateurs()
        {
            // Désactiver tous les liens au départ
            linkutilisateur.Enabled = false;
            linkclasse.Enabled = false;
            linkcours.Enabled = false;
            linkmatiere.Enabled = false;
            linkprof.Enabled = false;
            linketudiant.Enabled = false;
            linknote.Enabled = false;

            // Gestion des accès selon le rôle
            if (userRole == "Administrateur")
            {
                linkutilisateur.Enabled = true;
                linkclasse.Enabled = true;
                linkcours.Enabled = true;
                linkmatiere.Enabled = true;
                linkprof.Enabled = true;
                linketudiant.Enabled = true;
                linknote.Enabled = true;
            }
            else if (userRole == "DE")
            {
                linkclasse.Enabled = true;
                linkcours.Enabled = true;
                linkmatiere.Enabled = true;
                linkprof.Enabled = true;
                
               
            }
            else if (userRole == "Agent")
            {
                linketudiant.Enabled = true;
                linknote.Enabled = true;
            }
            else
            {
                MessageBox.Show("Rôle inconnu, accès restreint !");
            }
        }



        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void linkLabel4_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormProfesseur formprof = new FormProfesseur();
            AfficherFormDansPanel(formprof);
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void AfficherFormDansPanel(Form form)
        {
            // Vider le Panel avant d'afficher un nouveau formulaire
            panel7.Controls.Clear();

            // Configurer le formulaire pour qu'il s'affiche dans le Panel
            form.TopLevel = false;
            form.FormBorderStyle = FormBorderStyle.None;
            form.Dock = DockStyle.Fill;

            // Ajouter le formulaire au Panel et l'afficher
            panel7.Controls.Add(form);
            form.Show();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormEtudiant formEtudiant = new FormEtudiant();
            AfficherFormDansPanel(formEtudiant);
        }

        private void linkcours_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormCours formcours = new FormCours();
            AfficherFormDansPanel(formcours);

        }

        private void linknote_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormNote formNote = new FormNote();
            AfficherFormDansPanel(formNote);
        }

        private void linkclasse_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormClasse formclasse = new FormClasse();
            AfficherFormDansPanel(formclasse);
        }

        private void linkmatiere_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormMatiere formmatiere = new FormMatiere();
            AfficherFormDansPanel(formmatiere);
        }

        private void linkutilisateur_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            FormUtilisateur formUtilisateur = new FormUtilisateur();
            AfficherFormDansPanel(formUtilisateur);
        }

        private void panel11_Paint(object sender, PaintEventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox4_Click(object sender, EventArgs e)
        {

        }

        private void txtdeconnection_Click(object sender, EventArgs e)
        {
            // Afficher une boîte de dialogue de confirmation
            DialogResult result = MessageBox.Show("Êtes-vous sûr de vouloir vous déconnecter ?", "Déconnexion", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                // Fermer la session de l'utilisateur
                // Par exemple, vous pouvez réinitialiser les informations de l'utilisateur
                UserSession.Clear(); // Supposons que vous avez une classe UserSession pour gérer la session

                // Fermer la fenêtre actuelle
                this.Close();

                // Ouvrir la fenêtre de connexion
                FormLogin loginForm = new FormLogin();
                loginForm.Show();
            }
        }
    }
}
