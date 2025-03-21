using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionSystemeEtudiant;

namespace GestionSchoolApp.Forms
{
    public partial class FormMatiere : Form
    {

        private AppDBContext _context = new AppDBContext();
        private ErrorProvider errorProvider = new ErrorProvider();
        private Matiere _matiereSelectionnee = null;
        public FormMatiere()
        {
            InitializeComponent();
            refresh();
        }


        private bool ValiderChamps()
        {
            bool estValide = true;

            // Vérifier si le champ NomMatiere est vide
            if (string.IsNullOrWhiteSpace(txtnom.Text))
            {
                errorProvider.SetError(txtnom, "Le nom de la matière est requis !");
                estValide = false;
            }
            else
            {
                errorProvider.SetError(txtnom, ""); // Effacer l'erreur si valide
            }


            return estValide;
        }
        
        public void refresh()
        {

            dataGridView1.DataSource = null;
            using (var db = new AppDBContext())
            {
                dataGridView1.DataSource = db.Matieres.ToList();
            }
        }

        private void FormMatiere_Load(object sender, EventArgs e)
        {
            refresh();
            
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (!ValiderChamps()) return;
            try
            {
                // Créer une nouvelle instance de la matière
                var matiere = new Matiere
                {
                    nomMatiere = txtnom.Text
                };

                // Vérifier que le nom de la matière n'est pas vide
                if (string.IsNullOrWhiteSpace(matiere.nomMatiere))
                {
                    MessageBox.Show("Le nom de la matière ne peut pas être vide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                // Ajouter la matière à la base de données
                _context.Matieres.Add(matiere);
                _context.SaveChanges();
                refresh();

                // Message de succès
                MessageBox.Show("Matière ajoutée avec succès !");
            }
            catch (DbUpdateException dbEx)
            {
                // Exception spécifique liée à la base de données
                MessageBox.Show("Erreur lors de l'ajout de la matière dans la base de données. Détails : " + dbEx.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Exception générale
                MessageBox.Show("Une erreur s'est produite. Détails : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ChargerMatiere(int idMatiere)
        {
            // Récupérer la matière sélectionnée depuis la base de données
            var matiere = _context.Matieres.FirstOrDefault(m => m.id == idMatiere);

            if (matiere != null)
            {
                // Remplir les champs du formulaire avec les informations de la matière
                txtnom.Text = matiere.nomMatiere;
                // Vous pouvez ajouter d'autres champs ici si nécessaire pour afficher d'autres informations associées à la matière
            }
            else
            {
                MessageBox.Show("La matière sélectionnée n'a pas été trouvée.");
            }
        }


        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idMatiere = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value; // Assure-toi que c'est la colonne de l'ID de la matiere
                ChargerMatiere(idMatiere); // Charger les informations du matiere dans le formulaire
                                           // Assigner la matière sélectionnée à la variable _matiereSelectionnee
                _matiereSelectionnee = _context.Matieres.FirstOrDefault(m => m.id == idMatiere);
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
           /* if (_matiereSelectionnee == null)
            {
                MessageBox.Show("Veuillez sélectionner une matière à modifier.");
                return;
            }

            // Vérifier si la matière est associée à des cours ou des professeurs
           /* if (_matiereSelectionnee.cours.Any() || _matiereSelectionnee.professeurs.Any())
            {
                DialogResult result = MessageBox.Show(
                    "Cette matière est associée à des cours ou des professeurs. " +
                    "Êtes-vous sûr de vouloir la modifier ? Cela pourrait affecter les associations existantes.",
                    "Avertissement", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);

                if (result == DialogResult.No)
                {
                    return;  // Ne pas poursuivre si l'utilisateur annule
                }
            }*/

            // Mettre à jour le nom de la matière avec la nouvelle valeur du champ du formulaire
            _matiereSelectionnee.nomMatiere = txtnom.Text;

            // Vérifier que le nom de la matière n'est pas vide
            /*if (string.IsNullOrWhiteSpace(_matiereSelectionnee.nomMatiere))
            {
                MessageBox.Show("Le nom de la matière ne peut pas être vide.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            try
            {
                // Mettre à jour les cours associés à cette matière
                foreach (var cours in _matiereSelectionnee.cours)
                {
                    // Si la matière est associée au cours, mettre à jour le nom de la matière dans le cours
                    if (cours.matieres.Any(m => m.id == _matiereSelectionnee.id))
                    {
                        // Exemple de mise à jour du nom du cours en fonction de la matière
                        // Ici, on peut par exemple choisir de modifier le nom du cours pour refléter la mise à jour du nom de la matière.
                        // Si tu as une logique spécifique de nommage, applique-la ici.
                        cours.nomCours = "Cours de " + _matiereSelectionnee.nomMatiere;
                    }
                }

                // Sauvegarder les modifications dans la base de données
                _context.SaveChanges();  // Enregistrer les modifications dans la base de données
                MessageBox.Show("La matière a été mise à jour avec succès !");
                refresh();  // Rafraîchir les données affichées
            }
            catch (DbUpdateException dbEx)
            {
                // Gestion des erreurs de base de données
                MessageBox.Show("Erreur lors de la mise à jour de la matière dans la base de données. Détails : " + dbEx.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            catch (Exception ex)
            {
                // Gestion des erreurs générales
                MessageBox.Show("Une erreur s'est produite. Détails : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }*/
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
