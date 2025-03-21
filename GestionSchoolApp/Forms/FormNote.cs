using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.Entity;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionSystemeEtudiant;

namespace GestionSchoolApp.Forms
{
    public partial class FormNote : Form
    {
        private AppDBContext _context = new AppDBContext();
        public FormNote()
        {
            InitializeComponent();
            ChargerClasses();
            


        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void FormNote_Load(object sender, EventArgs e)
        {
             ChargerClasses();
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

        }

       

        private void cmbclasse_SelectedIndexChanged(object sender, EventArgs e)
        {
            Classe classeSelectionnee = cmbclasse.SelectedItem as Classe;
            if (classeSelectionnee != null)
            {
                int idClasse = classeSelectionnee.id; // Récupération de l'ID de la classe

                // Récupérer les matières liées à la classe via les cours
                var matieresAssociees = _context.Cours
                    .Where(c => c.classes.Any(cl => cl.id == idClasse)) // Filtrer les cours de la classe sélectionnée
                    .SelectMany(c => c.matieres) // Extraire les matières associées à ces cours
                    .Distinct() // Éviter les doublons
                    .ToList();

                cmbmatiere.DataSource = matieresAssociees;
                cmbmatiere.DisplayMember = "nomMatiere"; // Afficher les noms des matière


                // Lister les étudiants associés à cette classe
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

        private void btnsave_Click(object sender, EventArgs e)
        {
           try
            {
                // Vérifier si un étudiant est sélectionné
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner un étudiant.");
                    return;
                }

                // Vérifier si une matière est sélectionnée
                if (cmbmatiere.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez sélectionner une matière.");
                    return;
                }

                // Vérifier si la note est valide
                float noteValeur;
                if (!float.TryParse(txtnote.Text, out noteValeur) || noteValeur < 0 || noteValeur > 20)
                {
                    MessageBox.Show("Veuillez entrer une note valide entre 0 et 20.");
                    return;
                }

                // Récupérer l'étudiant sélectionné
                int idEtudiant = (int)dataGridView1.SelectedRows[0].Cells["id"].Value;

                // Récupérer la matière sélectionnée
                Matiere matiereSelectionnee = cmbmatiere.SelectedItem as Matiere;
                int idMatiere = matiereSelectionnee.id;

                // Vérifier combien de notes existent déjà pour cet étudiant et cette matière
                int nombreNotesExistantes = _context.Notes
                    .Count(n => n.idE == idEtudiant && n.idM == idMatiere);

                if (nombreNotesExistantes >= 3)
                {
                    MessageBox.Show("Un étudiant ne peut avoir que 3 notes par matière.");
                    return;
                }

                // Ajouter la nouvelle note
                Note nouvelleNote = new Note
                {
                    idE = idEtudiant,
                    idM = idMatiere,
                    note = noteValeur
                };

                _context.Notes.Add(nouvelleNote);
                _context.SaveChanges();

                MessageBox.Show("Note ajoutée avec succès !");
                ChargerNotes(idEtudiant); // Rafraîchir la liste des notes
                var testMatiere = _context.Matieres.FirstOrDefault();
                if (testMatiere != null)
                {
                    MessageBox.Show("Matière test récupérée : " + testMatiere.nomMatiere);
                }
                else
                {
                    MessageBox.Show("Aucune matière trouvée !");
                }
                foreach (var note in _context.Notes.Include(n => n.matiere).ToList())
                {
                    Console.WriteLine($"Note ID: {note.id}, Matière: {note.matiere?.nomMatiere ?? "NULL"}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue : " + ex.Message);
            }
        }

        



        // Fonction pour afficher les notes et la moyenne
        

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private int? GetEtudiantSelectionne()
        {
            if (dataGridView1.SelectedRows.Count > 0)
            {
                return (int)dataGridView1.SelectedRows[0].Cells["Id"].Value;
            }
            MessageBox.Show("Sélectionnez un étudiant !");
            return null;
        }
        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            int? etudiantId = GetEtudiantSelectionne();
            if (etudiantId == null) return;

            ChargerNotes(etudiantId.Value);
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            try
            {
                // Vérifier si un étudiant est sélectionné
                if (dataGridView1.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner un étudiant.");
                    return;
                }

                // Vérifier si une matière est sélectionnée
                if (cmbmatiere.SelectedItem == null)
                {
                    MessageBox.Show("Veuillez sélectionner une matière.");
                    return;
                }

                // Vérifier si une note est sélectionnée dans la liste des notes
                if (dataGridView2.SelectedRows.Count == 0) // Assurez-vous d’avoir un DataGridView pour afficher les notes
                {
                    MessageBox.Show("Veuillez sélectionner une note à modifier.");
                    return;
                }

                // Vérifier si la note est valide
                float nouvelleValeur;
                if (!float.TryParse(txtnote.Text, out nouvelleValeur) || nouvelleValeur < 0 || nouvelleValeur > 20)
                {
                    MessageBox.Show("Veuillez entrer une note valide entre 0 et 20.");
                    return;
                }

                // Récupérer l'étudiant sélectionné
                int idEtudiant = (int)dataGridView1.SelectedRows[0].Cells["id"].Value;

                // Récupérer la matière sélectionnée
                Matiere matiereSelectionnee = cmbmatiere.SelectedItem as Matiere;
                int idMatiere = matiereSelectionnee.id;

                // Récupérer l'ID de la note sélectionnée
                int idNote = (int)dataGridView2.SelectedRows[0].Cells["id"].Value;

                // Chercher la note correspondante dans la base de données
                var noteAModifier = _context.Notes.FirstOrDefault(n => n.id == idNote && n.idE == idEtudiant && n.idM == idMatiere);

                if (noteAModifier == null)
                {
                    MessageBox.Show("Impossible de trouver la note sélectionnée.");
                    return;
                }

                // Modifier la valeur de la note
                noteAModifier.note = nouvelleValeur;
                _context.SaveChanges();

                MessageBox.Show("Note modifiée avec succès !");
                ChargerNotes(idEtudiant); // Rafraîchir la liste des notes
            }
            catch (Exception ex)
            {
                MessageBox.Show("Une erreur est survenue : " + ex.Message);
            }
        }



        private void ChargerNotes(int idEtudiant)
        {
            try
            {
                // Charger les notes avec les informations de la matière
                var notesEtudiant = _context.Notes
                    .Include(n => n.matiere) //  Inclure la matière avant le filtrage
                    .Where(n => n.idE == idEtudiant)
                    .Select(n => new
                    {
                        n.id,
                        Matiere = n.matiere != null ? n.matiere.nomMatiere : "Inconnu", //  Vérifier si null
                        n.note
                    })
                    .ToList();

                if (notesEtudiant.Any())
                {
                    // Mettre à jour le DataGridView des notes
                    dataGridView2.DataSource = notesEtudiant;
                    float moyenne = notesEtudiant.Average(n => n.note);
                    labelMoyenne.Text = $"Moyenne: {moyenne:F2}";
                }
                else
                {
                    labelMoyenne.Text = "Moyenne: -";
                    MessageBox.Show("Aucune note trouvée pour cet étudiant.");
                }
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement des notes : " + ex.Message);
            }
        }


        private void ChargerNoteSelectionnee()
        {
            try
            {
                // Vérifier si une note est sélectionnée dans le DataGridView
                if (dataGridView2.SelectedRows.Count == 0)
                {
                    MessageBox.Show("Veuillez sélectionner une note à modifier.");
                    return;
                }

                // Récupérer l'ID de la note sélectionnée
                int idNote = Convert.ToInt32(dataGridView2.SelectedRows[0].Cells["id"].Value);

                // Charger la note depuis la base de données
                var noteSelectionnee = _context.Notes
                    .Include(n => n.matiere) // Charger la matière associée
                    .FirstOrDefault(n => n.id == idNote);

                if (noteSelectionnee != null)
                {
                    // Sélectionner la matière correspondante dans le ComboBox
                    cmbmatiere.SelectedItem = cmbmatiere.Items
                        .Cast<Matiere>()
                        .FirstOrDefault(m => m.id == noteSelectionnee.idM);

                    // Afficher la note dans le champ de texte
                    txtnote.Text = noteSelectionnee.note.ToString();
                }
                else
                {
                    MessageBox.Show("Impossible de charger la note sélectionnée.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors du chargement de la note : " + ex.Message);
            }
        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView2_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ChargerNoteSelectionnee();
        }
    }
}
