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
using GestionSystemeEtudiant;

namespace GestionSchoolApp.Forms
{
    public partial class FormProfesseur : Form
    {
        private AppDBContext _context = new AppDBContext();
        private Professeur _professeur = new Professeur();
        private Professeur _professeurSelectionne;
        private ErrorProvider errorProviderProf = new ErrorProvider(); // Instance de l'ErrorProvider
        public FormProfesseur()
        {
            InitializeComponent();
        }

        private void FormProfesseur_Load(object sender, EventArgs e)
        {
            refresh();
            // Charger les matières disponibles
            ChargerMatieres();
            // Charger les classes disponibles
            ChargerClasses();

            // Si un prof existe déjà (pour la modification)
            if (_professeur != null)
            {
                foreach (var matiere in _professeur.matieres)
                {
                    // Sélectionner les matières associées au cours
                    cklistmatieres.SetItemChecked(cklistmatieres.Items.IndexOf(matiere.nomMatiere), true);
                }

                foreach (var classe in _professeur.classes)
                {
                    // Sélectionner les classes associées au cours
                    cklistclasses.SetItemChecked(cklistclasses.Items.IndexOf(classe.nomClasse), true);
                }
            }

        }
        public void refresh()
        {

            dataGridView1.DataSource = null;
            using (var db = new AppDBContext())
            {
                dataGridView1.DataSource = db.Professeurs.ToList();
            }
        }
        private void ChargerMatieres()
        {
            // Charger les matières depuis la base de données et les afficher dans la CheckedListBox
            var matieres = _context.Matieres.ToList();
            cklistmatieres.Items.Clear();
            foreach (var matiere in matieres)
            {
                cklistmatieres.Items.Add(matiere.nomMatiere);
            }
        }

        private void ChargerClasses()
        {
            // Charger les classes depuis la base de données et les afficher dans la CheckedListBox
            var classes = _context.Classes.ToList();
            cklistclasses.Items.Clear();
            foreach (var classe in classes)
            {
                cklistclasses.Items.Add(classe.nomClasse);
            }
        }



        private bool ValiderChamps()
        {
            bool estValide = true;

            // Vérifier si le champ Nom est vide
            if (string.IsNullOrWhiteSpace(txtnom.Text))
            {
                errorProviderProf.SetError(txtnom, "Le nom du professeur est requis !");
                estValide = false;
            }
            else
            {
                errorProviderProf.SetError(txtnom, ""); // Effacer l'erreur si valide
            }

            // Vérifier si le champ Prénom est vide
            if (string.IsNullOrWhiteSpace(txtprenom.Text))
            {
                errorProviderProf.SetError(txtprenom, "Le prénom du professeur est requis !");
                estValide = false;
            }
            else
            {
                errorProviderProf.SetError(txtprenom, ""); // Effacer l'erreur si valide
            }

            // Vérifier si le champ Email est vide ou incorrect
            if (string.IsNullOrWhiteSpace(txtemail.Text) || !Regex.IsMatch(txtemail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProviderProf.SetError(txtemail, "Entrer l'email et ça doit être valide !");
                estValide = false;
            }
            else
            {
                errorProviderProf.SetError(txtemail, ""); // Effacer l'erreur si valide
            }
            // Vérifier si le champ Téléphone est vide ou incorrect
            if (string.IsNullOrWhiteSpace(txttelephone.Text) || !Regex.IsMatch(txttelephone.Text, @"^(\+221\s?)?(77|78|70|76|75)\d{7}$"))
            {
                errorProviderProf.SetError(txttelephone, "Entrez un numéro de téléphone valide au format sénégalais !");
                estValide = false;
            }
            else
            {
                errorProviderProf.SetError(txttelephone, ""); // Effacer l'erreur si valide
            }


            // Vérifier si au moins une matière est sélectionnée dans la CheckedListBox
            if (cklistmatieres.CheckedItems.Count == 0)
            {
                errorProviderProf.SetError(cklistmatieres, "Veuillez sélectionner au moins une matière !");
                estValide = false;
            }
            else
            {
                errorProviderProf.SetError(cklistmatieres, ""); // Effacer l'erreur si valide
            }

            // Vérifier si au moins une classe est sélectionnée dans la CheckedListBox
            if (cklistclasses.CheckedItems.Count == 0)
            {
                errorProviderProf.SetError(cklistclasses, "Veuillez sélectionner au moins une matière !");
                estValide = false;
            }
            else
            {
                errorProviderProf.SetError(cklistclasses, ""); // Effacer l'erreur si valide
            }


            return estValide;
        }

        private void btnregister_Click(object sender, EventArgs e)
        {
            // Vérifier la validité des champs avant de procéder
            if (!ValiderChamps())
            {
                return; // Si la validation échoue, on arrête l'exécution
            }

            try
            {
                // Créer une nouvelle instance de Professeur
                var professeur = new Professeur
                {
                    nom = txtnom.Text,
                    prenom = txtprenom.Text,
                    email = txtemail.Text,
                    tel = txttelephone.Text
                };

                // Associer les matières sélectionnées
                professeur.matieres = new List<Matiere>();
                foreach (var item in cklistmatieres.CheckedItems)
                {
                    var matiere = _context.Matieres.FirstOrDefault(m => m.nomMatiere == item.ToString());
                    if (matiere != null)
                    {
                        professeur.matieres.Add(matiere);
                    }
                }
                // Associer les classes sélectionnées
                professeur.classes = new List<Classe>();
                foreach (var item in cklistclasses.CheckedItems)
                {
                    var classe = _context.Classes.FirstOrDefault(c => c.nomClasse == item.ToString());
                    if (classe != null)
                    {
                        _professeur.classes.Add(classe);
                    }
                }



                // Ajouter le professeur à la base de données
                _context.Professeurs.Add(professeur);
                _context.SaveChanges();
                refresh();

                // Afficher un message de succès
                MessageBox.Show("Professeur ajouté avec succès !");
               
            }
            catch (Exception ex)
            {
                // Capturer les exceptions et afficher un message d'erreur
                MessageBox.Show("Une erreur est survenue lors de l'ajout du professeur. Détails : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txttelephone_TextChanged(object sender, EventArgs e)
        {

        }

        private void txttelephone_KeyPress(object sender, KeyPressEventArgs e)
        {
            // Autoriser uniquement les chiffres, l'espace, le tiret, et les signes "+" pour l'indicatif international
            if (!char.IsDigit(e.KeyChar) && e.KeyChar != '+' && e.KeyChar != ' ' && e.KeyChar != '-')
            {
                e.Handled = true; // Bloquer la touche si elle n'est pas un chiffre, un espace, un tiret ou un plus
            }
        }

        // Méthode pour charger un professeur à modifier
        private void ChargerProfesseur(int id)
        {
            // Rechercher le professeur par son ID avec les matières et classes associées
            _professeurSelectionne = _context.Professeurs
                                    .Include("matieres") // Charger les matières associées
                                    .Include("classes") // Charger les classes associées
                                    .FirstOrDefault(p => p.id == id);

            if (_professeurSelectionne != null)
            {
                // Remplir les champs du formulaire avec ses informations
                txtnom.Text = _professeurSelectionne.nom;
                txtprenom.Text = _professeurSelectionne.prenom;
                txtemail.Text = _professeurSelectionne.email;
                txttelephone.Text = _professeurSelectionne.tel;

                // Cocher les matières associées dans la CheckedListBox
                for (int i = 0; i < cklistmatieres.Items.Count; i++)
                {
                    string matiereNom = cklistmatieres.Items[i].ToString();
                    cklistmatieres.SetItemChecked(i, _professeurSelectionne.matieres.Any(m => m.nomMatiere == matiereNom));
                }

                // Cocher les classes associées dans la CheckedListBox
                for (int i = 0; i < cklistclasses.Items.Count; i++)
                {
                    string classeNom = cklistclasses.Items[i].ToString();
                    cklistclasses.SetItemChecked(i, _professeurSelectionne.classes.Any(c => c.nomClasse == classeNom));
                }
            }
            else
            {
                MessageBox.Show("Professeur non trouvé.");
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {// Vérifier si les champs sont valides
            if (!ValiderChamps()) return;

            if (_professeurSelectionne == null)
            {
                MessageBox.Show("Professeur non trouvé.");
                return;
            }

            // Mettre à jour les informations du professeur
            _professeurSelectionne.nom = txtnom.Text;
            _professeurSelectionne.prenom = txtprenom.Text;
            _professeurSelectionne.email = txtemail.Text;
            _professeurSelectionne.tel = txttelephone.Text;

            // Mettre à jour les matières associées
            _professeurSelectionne.matieres.Clear(); // Effacer les anciennes associations

            foreach (var item in cklistmatieres.CheckedItems)
            {
                var matiere = _context.Matieres.FirstOrDefault(m => m.nomMatiere == item.ToString());
                if (matiere != null)
                {
                    _professeurSelectionne.matieres.Add(matiere);
                }
            }
            // Mettre à jour les classes associées
            _professeurSelectionne.classes.Clear(); // Effacer les anciennes associations

            foreach (var item in cklistclasses.CheckedItems)
            {
                var classe = _context.Classes.FirstOrDefault(c => c.nomClasse == item.ToString());
                if (classe != null)
                {
                    _professeurSelectionne.classes.Add(classe);
                }
            }

            // Sauvegarder les modifications avec gestion des erreurs
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Professeur modifié avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la modification du professeur : {ex.Message}");
            }
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                // Récupérer l'ID de l'étudiant depuis la ligne double-cliquée
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);

                // Charger les informations de l'étudiant dans le formulaire
                ChargerProfesseur(id);
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}

