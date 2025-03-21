using GestionSystemeEtudiant;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionSchoolApp.Forms
{
    public partial class FormUtilisateur : Form
    {
        private AppDBContext _context = new AppDBContext();
        private Utilisateur _utilisateurSelectionne;
        private ErrorProvider errorProviderUser = new ErrorProvider();
        public FormUtilisateur()
        {
            InitializeComponent();
        }

        private void FormUtilisateur_Load(object sender, EventArgs e)
        {
            RafraichirListeUtilisateurs();
        }

        private void RafraichirListeUtilisateurs()
        {
            dataGridView1.DataSource = null;
            using (var db = new AppDBContext())
            {
                dataGridView1.DataSource = db.Utilisateurs.Select(u => new
                {
                    u.id,
                    u.nomUser,
                    u.role,
                    u.tel
                }).ToList();
            }
        }


        private void label1_Click(object sender, EventArgs e)
        {

        }


        private void btnadd_Click(object sender, EventArgs e)
        {
            if (!ValiderChamps()) return;

            try
            {
                var utilisateur = new Utilisateur
                {
                    nomUser = txtnom.Text,
                    mdp = BCrypt.Net.BCrypt.HashPassword(txtpassword.Text),
                    role = cmbrole.SelectedItem.ToString(),
                    tel = txttelephone.Text
                };

                _context.Utilisateurs.Add(utilisateur);
                _context.SaveChanges();
                RafraichirListeUtilisateurs();

                MessageBox.Show("Utilisateur ajouté avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de l'ajout de l'utilisateur : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerUtilisateur(int id)
        {
            _utilisateurSelectionne = _context.Utilisateurs.FirstOrDefault(u => u.id == id);

            if (_utilisateurSelectionne != null)
            {
                txtnom.Text = _utilisateurSelectionne.nomUser;
                cmbrole.SelectedItem = _utilisateurSelectionne.role;
                txttelephone.Text = _utilisateurSelectionne.tel;
                txtpassword.Text = _utilisateurSelectionne.mdp;  // ⚠ Attention : Affichage du mot de passe brut !
            }
            else
            {
                MessageBox.Show("Utilisateur non trouvé.");
            }
        }

        private bool ValiderChamps()
        {
            bool estValide = true;

            if (string.IsNullOrWhiteSpace(txtnom.Text))
            {
                errorProviderUser.SetError(txtnom, "Le nom d'utilisateur est requis !");
                estValide = false;
            }
            else
            {
                errorProviderUser.SetError(txtnom, "");
            }

            if (string.IsNullOrWhiteSpace(txtpassword.Text))
            {
                errorProviderUser.SetError(txtpassword, "Le mot de passe est requis !");
                estValide = false;
            }
            else
            {
                errorProviderUser.SetError(txtpassword, "");
            }

            if (cmbrole.SelectedIndex == -1)
            {
                errorProviderUser.SetError(cmbrole, "Veuillez sélectionner un rôle !");
                estValide = false;
            }
            else
            {
                errorProviderUser.SetError(cmbrole, "");
            }

            if (string.IsNullOrWhiteSpace(txttelephone.Text) || !Regex.IsMatch(txttelephone.Text, @"^(\+221\s?)?(77|78|70|76|75)\d{7}$"))
            {
                errorProviderUser.SetError(txttelephone, "Entrez un numéro de téléphone valide au format sénégalais !");
                estValide = false;
            }
            else
            {
                errorProviderUser.SetError(txttelephone, "");
            }

            return estValide;
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (!ValiderChamps()) return;

            if (_utilisateurSelectionne == null)
            {
                MessageBox.Show("Aucun utilisateur sélectionné.");
                return;
            }

            _utilisateurSelectionne.nomUser = txtnom.Text;
            _utilisateurSelectionne.role = cmbrole.SelectedItem.ToString();
            _utilisateurSelectionne.tel = txttelephone.Text;

            if (!string.IsNullOrWhiteSpace(txtpassword.Text))
            {
                _utilisateurSelectionne.mdp = BCrypt.Net.BCrypt.HashPassword(txtpassword.Text);
            }

            try
            {
                _context.SaveChanges();
                RafraichirListeUtilisateurs();
                MessageBox.Show("Utilisateur modifié avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message);
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);
                ChargerUtilisateur(id);
            }
        }

        private void ReinitialiserChamps()
        {
            txtnom.Clear();
            txttelephone.Clear();
            txtpassword.Clear();
            txttelephone.Clear();

            if (cmbrole.Items.Count > 0)
            {
                cmbrole.SelectedIndex = 0;
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (_utilisateurSelectionne == null)
            {
                MessageBox.Show("Aucun utilisateur sélectionné.");
                return;
            }

            // Vérifier si c'est un administrateur pour empêcher sa suppression
            if (_utilisateurSelectionne.role == "Administrateur")
            {
                MessageBox.Show("Impossible de supprimer un administrateur !");
                return;
            }

            // Confirmation avant suppression
            DialogResult result = MessageBox.Show("Voulez-vous vraiment supprimer cet utilisateur ?",
                                                  "Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _context.Utilisateurs.Remove(_utilisateurSelectionne);
                    _context.SaveChanges();
                    MessageBox.Show("Utilisateur supprimé avec succès !");

                    RafraichirListeUtilisateurs();
                    ReinitialiserChamps();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la suppression de l'utilisateur : {ex.Message}");
                }
            }
        }


        private void FormUtilisateur_Load_1(object sender, EventArgs e)
        {
            RafraichirListeUtilisateurs();
        }

       /* private void pictureBox1_Click(object sender, EventArgs e)
        {
            if (txtpassword.UseSystemPasswordChar)
            {
                txtpassword.UseSystemPasswordChar = false;
                pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject("eye_open"); // Affiche l'œil ouvert
            }
            else
            {
                txtpassword.UseSystemPasswordChar = true;
                // pictureBox1.Image = (Image)Properties.Resources.ResourceManager.GetObject("eye_closed"); // Affiche l'œil fermé
            }
        }*/
    }
}
