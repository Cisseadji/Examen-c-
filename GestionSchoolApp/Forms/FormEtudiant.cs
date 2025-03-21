using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionSystemeEtudiant;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace GestionSchoolApp.Forms
{
    public partial class FormEtudiant : Form
    {
        private AppDBContext _context = new AppDBContext();
        private ErrorProvider errorProviderEtudiant = new ErrorProvider();
        public FormEtudiant()
        {
            InitializeComponent();
            ChargerClasses(); // Charger les classes dans le ComboBox
        }

        private void FormEtudiant_Load(object sender, EventArgs e)
        {
            txtid.Visible = false;
            ReinitialiserChamps();
            ChargerClasses(); // Charger les classes dans le ComboBox
            refresh();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void cmbclasse_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        public void refresh()
        {
            dataGridView1.DataSource = null;
            using (var db = new AppDBContext())
            {
                dataGridView1.DataSource = db.Etudiants.Select(e => new EtudiantView { id = e.id,matricule= e.matricule, prenom = e.prenom, nom = e.nom, classe = e.classe.nomClasse, sexe = e.sexe, adresse = e.adresse, datenaiss = e.datenaiss, email = e.email , tel = e.tel }).ToList();

            }
            
        }

        // Méthode de validation des champs
        private bool ValiderChamps()
        {
            bool estValide = true;

     

            // Vérifier si le champ Nom est vide
            if (string.IsNullOrWhiteSpace(txtnom.Text))
            {
                errorProviderEtudiant.SetError(txtnom, "Le nom est requis !");
                estValide = false;
            }
            else
            {
                errorProviderEtudiant.SetError(txtnom, ""); // Effacer l'erreur si valide
            }

            // Vérifier si le champ Prénom est vide
            if (string.IsNullOrWhiteSpace(txtprenom.Text))
            {
                errorProviderEtudiant.SetError(txtprenom, "Le prénom est requis !");
                estValide = false;
            }
            else
            {
                errorProviderEtudiant.SetError(txtprenom, ""); // Effacer l'erreur si valide
            }

            // Vérifier si le champ Adresse est vide
            if (string.IsNullOrWhiteSpace(txtadresse.Text))
            {
                errorProviderEtudiant.SetError(txtadresse, "Veuiller saisir une adresse !");
                estValide = false;
            }
            else
            {
                errorProviderEtudiant.SetError(txtadresse, ""); // Effacer l'erreur si valide
            }

            // Vérifier si le champ Email est valide
            if (string.IsNullOrWhiteSpace(txtemail.Text) || !Regex.IsMatch(txtemail.Text, @"^[\w-]+(\.[\w-]+)*@([\w-]+\.)+[a-zA-Z]{2,7}$"))
            {
                errorProviderEtudiant.SetError(txtemail, "L'email est requis et doit être valide !");
                estValide = false;
            }
            else
            {
                errorProviderEtudiant.SetError(txtemail, ""); // Effacer l'erreur si valide
            }

            // Vérifier si le champ Téléphone est vide ou incorrect
            if (string.IsNullOrWhiteSpace(txttelephone.Text) || !Regex.IsMatch(txttelephone.Text, @"^(\+221\s?)?(77|78|70|76|75)\d{7}$"))
            {
                errorProviderEtudiant.SetError(txttelephone, "Entrez un numéro de téléphone valide au format sénégalais !");
                estValide = false;
            }
            else
            {
                errorProviderEtudiant.SetError(txttelephone, ""); // Effacer l'erreur si valide
            }

            // Vérifier si un sexe est sélectionné
            if (!rdhomme.Checked && !rdfemme.Checked)
            {
                errorProviderEtudiant.SetError(panelsexe, "Veuillez sélectionner une option !");
                estValide = false;
            }
            else
            {
                errorProviderEtudiant.SetError(panelsexe, ""); // Effacer l'erreur si valide
            }

            // Vérifier si la classe a été sélectionnée
            if (cmbclasse.SelectedIndex == 0)
            {
                errorProviderEtudiant.SetError(cmbclasse, "La classe est requise !");
                estValide = false;
            }
            else
            {
                errorProviderEtudiant.SetError(cmbclasse, ""); // Effacer l'erreur si valide
            }

            return estValide;
        }

        // Réinitialiser les champs après ajout
      
        private void ReinitialiserChamps()
        {
            txtnom.Clear();
            txtprenom.Clear();
            dtpdatenaiss.Value = DateTime.Now;
            rdhomme.Checked = false;
            rdfemme.Checked = false;
            txtadresse.Clear();
            txttelephone.Clear();
            txtemail.Clear();

            if (cmbclasse.Items.Count > 0)
            {
                cmbclasse.SelectedIndex = 0;
            }
        }



        private void ChargerClasses()
        {
            // Supposons que _context.Classes est la source des classes
            var classes = _context.Classes.ToList();

            // Ajouter une option par défaut au début de la liste
            classes.Insert(0, new Classe { id = 0, nomClasse = "Choisir une classe" });

            // Lier les classes au ComboBox pour les classes
            cmbclasse.DataSource = classes;
            cmbclasse.DisplayMember = "nomClasse";
            cmbclasse.ValueMember = "id";

            // Lier les classes au ComboBox pour filtrer 
            cmbfiltre.DataSource = classes;
            cmbfiltre.DisplayMember = "nomClasse";
            cmbfiltre.ValueMember = "id";


            // Sélectionner l'option par défaut
            cmbclasse.SelectedIndex = 0;
        }

        private void dtnadd_Click(object sender, EventArgs e)
        {
            // Validation des champs
        if (!ValiderChamps())
                return;

            // Créer un nouvel étudiant
            var etudiant = new Etudiant
            {
                nom = txtnom.Text,
                prenom = txtprenom.Text,
                datenaiss = dtpdatenaiss.Value,
                sexe = rdhomme.Checked ? "Homme" : "Femme", // Assurez-vous que l'un des boutons radio est sélectionné
                adresse = txtadresse.Text,
                tel = txttelephone.Text,
                email = txtemail.Text,
                idC = (int)cmbclasse.SelectedValue,
                classe = (Classe)cmbclasse.SelectedItem
            };

            // Récupérer la classe sélectionnée
            var classe = _context.Classes.FirstOrDefault(c => c.id == (int)cmbclasse.SelectedValue);

            if (classe.id != 0)
            {
                etudiant.classe = classe;
            }
            else
            {
                MessageBox.Show("Classe non trouvée !");
                return;
            }

            // Ajouter l'étudiant à la base de données
            try
            {
                _context.Etudiants.Add(etudiant);
                _context.SaveChanges();
                string matriculeGenere = $"{etudiant.id}-{etudiant.nom}-{etudiant.datenaiss:yyyyMMdd}";

                etudiant.matricule= matriculeGenere;
                _context.SaveChanges();
                refresh();
                MessageBox.Show($"Étudiant ajouté avec succès !\nMatricule : {matriculeGenere}", "Succès", MessageBoxButtons.OK, MessageBoxIcon.Information);
                // Réinitialiser les champs après ajout
                ReinitialiserChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'ajout de l'étudiant : {ex.Message}");
            }
        }

        private void label11_Click(object sender, EventArgs e)
        {

        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        


        private void ChargerEtudiantFormulaire(int idEtu)
        {
            var _etudiantSelectionne = _context.Etudiants
                                   .Include("classe")
                                   .FirstOrDefault(e => e.id == idEtu);

            if (_etudiantSelectionne != null)
            {
                txtid.Text = _etudiantSelectionne.id.ToString(); // Stocker l'ID
                txtnom.Text = _etudiantSelectionne.nom;
                txtprenom.Text = _etudiantSelectionne.prenom;
                dtpdatenaiss.Value = _etudiantSelectionne.datenaiss;
                rdhomme.Checked = _etudiantSelectionne.sexe == "Homme";
                rdfemme.Checked = _etudiantSelectionne.sexe == "Femme";
                txtadresse.Text = _etudiantSelectionne.adresse;
                txttelephone.Text = _etudiantSelectionne.tel;
                txtemail.Text = _etudiantSelectionne.email;
                cmbclasse.SelectedValue = _etudiantSelectionne.idC; // Associer la classe
            }
            else
            {
                MessageBox.Show("Étudiant non trouvé !");
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idEtu = (int)dataGridView1.Rows[e.RowIndex].Cells[0].Value; // Assure-toi que c'est la colonne de l'ID du cours
                ChargerEtudiantFormulaire(idEtu); // Charger les informations du cours dans le formulaire
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            // Validation des champs
            if (!ValiderChamps()) return;

            // Vérifier que l'ID est valide
            if (!int.TryParse(txtid.Text, out int etudiantId))
            {
                MessageBox.Show("ID invalide.");
                return;
            }

            // Rechercher l'étudiant à modifier
            var _etudiantSelectionne = _context.Etudiants.FirstOrDefault(et => et.id == etudiantId);
            if (_etudiantSelectionne == null)
            {
                MessageBox.Show("Veuillez selectionner  un étudiant à modifier.");
                return;
            }

            // Mettre à jour les informations
            _etudiantSelectionne.nom = txtnom.Text;
            _etudiantSelectionne.prenom = txtprenom.Text;
            _etudiantSelectionne.datenaiss = dtpdatenaiss.Value;
            _etudiantSelectionne.sexe = rdhomme.Checked ? "Homme" : "Femme";
            _etudiantSelectionne.adresse = txtadresse.Text;
            _etudiantSelectionne.tel = txttelephone.Text;
            _etudiantSelectionne.email = txtemail.Text;
            _etudiantSelectionne.idC = (int)cmbclasse.SelectedValue;
            
            // Validation des champs
            if (!ValiderChamps()) return;

            // Mettre à jour la classe associée
            var classe = _context.Classes.FirstOrDefault(c => c.id == (int)cmbclasse.SelectedValue);
            if (classe != null)
            {
                _etudiantSelectionne.classe = classe;
            }
            else
            {
                MessageBox.Show("Classe non trouvée !");
                return;
            }

            // Sauvegarder les modifications
            try
            {
                _context.SaveChanges();
                MessageBox.Show("Étudiant modifié avec succès !");
                refresh();
                ReinitialiserChamps();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de la modification de l'étudiant : {ex.Message}");
            }
        }

        private void btndelete_Click(object sender, EventArgs e)
        {
            if (!int.TryParse(txtid.Text, out int etudiantId))
            {
                MessageBox.Show("Veuillez selectionner  un étudiant à supprimer.");
                return;
            }

            var etudiant = _context.Etudiants.FirstOrDefault(et => et.id == etudiantId);

            if (etudiant == null)
            {
                MessageBox.Show("Veuillez selectionner  un étudiant à supprimer.");
                return;                                                                                                                                                                                                                                                                                                                             
            }

            // Demander confirmation avant la suppression
            DialogResult result = MessageBox.Show("Voulez-vous vraiment supprimer cet étudiant ?",
                                                  "Confirmation",
                                                  MessageBoxButtons.YesNo,
                                                  MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                try
                {
                    _context.Etudiants.Remove(etudiant);
                    _context.SaveChanges();
                    MessageBox.Show("Étudiant supprimé avec succès !");
                    refresh();
                    ReinitialiserChamps();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Erreur lors de la suppression de l'étudiant : {ex.Message}");
                }
            }
        }

        private void cmbfiltre_SelectedIndexChanged(object sender, EventArgs e)
        {
            
            Classe classeSelectionnee = cmbfiltre.SelectedItem as Classe;
            if (classeSelectionnee != null)
            {
                int idClasse = classeSelectionnee.id; // Récupération de l'ID
                dataGridView1.DataSource = _context.Etudiants.Include("classe")
                    .Where(et => et.idC == idClasse).ToList();
            }
            else
            {
                MessageBox.Show("Veuillez sélectionner une classe valide.");
            }
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
        private void txtrecherche_TextChanged(object sender, EventArgs e)
        {
            string recherche = txtrecherche.Text.ToLower();
            var resultats = _context.Etudiants.Include("classe")
                .Where(et => et.nom.ToLower().Contains(recherche) ||
                            et.matricule.ToLower().Contains(recherche) ||
                            et.classe.nomClasse.ToLower().Contains(recherche))
                .ToList();
            if (resultats.Count > 0) {
                dataGridView1.DataSource = resultats;
            }
            else 
            {
                MessageBox.Show("Aucun étudiant ne correspond à votre recherche");
            }

            
        }
    }
}
