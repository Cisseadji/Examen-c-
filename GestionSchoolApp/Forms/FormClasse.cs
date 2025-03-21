using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GestionSystemeEtudiant;

namespace GestionSchoolApp.Forms
{
    public partial class FormClasse : Form
    {
        private AppDBContext _context = new AppDBContext();
        private ErrorProvider errorProviderClasse = new ErrorProvider();
        private Classe _classe = new Classe();
        private Classe classeSelectionnee = null;
        public FormClasse()
        {
            InitializeComponent();
            refresh();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }
        public void refresh()
        {

            dataGridView1.DataSource = null;
            using (var db = new AppDBContext())
            {
                dataGridView1.DataSource = db.Classes.ToList();
            }
        }

        private bool ValiderChamps()
        {
            bool estValide = true;

            // Vérifier si le champ NomClasse est vide
            if (string.IsNullOrWhiteSpace(txtnomclasse.Text))
            {
                errorProviderClasse.SetError(txtnomclasse, "Le nom de la classe est requis !");
                estValide = false;
            }
            else
            {
                errorProviderClasse.SetError(txtnomclasse, ""); // Effacer l'erreur si valide
            }
            // Vérifier si une autre classe avec le même nom existe
            var classeExistante = _context.Classes
                .FirstOrDefault(c => c.nomClasse.ToLower() == txtnomclasse.Text.Trim().ToLower());

            if (classeExistante != null && (classeSelectionnee == null || classeExistante.id != classeSelectionnee.id))
            {
                errorProviderClasse.SetError(txtnomclasse, "Une autre classe avec ce nom existe déjà !");
                estValide = false;
            }

            // Vérifier si au moins un cours est sélectionné dans la CheckedListBox
            if (cklistcours.CheckedItems.Count == 0)
            {
                errorProviderClasse.SetError(cklistcours, "Veuillez sélectionner au moins un cours !");
                estValide = false;
            }
            else
            {
                errorProviderClasse.SetError(cklistcours, ""); // Effacer l'erreur si valide
            }

            return estValide;
        }

        private void btnadd_Click(object sender, EventArgs e)
        {
            if (!ValiderChamps()) return;

            try
            {
                var classeExistante = _context.Classes
                    .Include("cours")
                    .FirstOrDefault(c => c.nomClasse.ToLower() == txtnomclasse.Text.Trim().ToLower());

                if (classeExistante != null)
                {
                    // Mise à jour des cours associés
                    classeExistante.cours.Clear();
                    foreach (var item in cklistcours.CheckedItems)
                    {
                        var cours = _context.Cours.FirstOrDefault(c => c.nomCours == item.ToString());
                        if (cours != null)
                        {
                            classeExistante.cours.Add(cours);
                        }
                    }

                    _context.SaveChanges();
                    refresh();
                    MessageBox.Show("Classe mise à jour avec succès !");
                }
                else
                {
                    MessageBox.Show("Classe non trouvée.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ChargerCours()
        {
            // Charger les cours depuis la base de données et les afficher dans la CheckedListBox
            var cours = _context.Cours.ToList();
            cklistcours.Items.Clear();
            foreach (var cour in cours)
            {
                cklistcours.Items.Add(cour.nomCours);
            }
        }
        private void FormClasse_Load(object sender, EventArgs e)
        {
            refresh();
            ChargerCours();

            if (_classe != null && _classe.cours != null && cklistcours != null)
            {
                foreach (var cours in _classe.cours)
                {
                    int index = cklistcours.Items.IndexOf(cours.nomCours);
                    if (index != -1) // Vérifier si le cours existe dans la liste
                    {
                        cklistcours.SetItemChecked(index, true);
                    }
                }
            }
        }

        private void ChargerClasse(int id)
        {
            var classeSelectionnee = _context.Classes.Include("cours").FirstOrDefault(c => c.id == id);
            if (classeSelectionnee != null)
            {
                txtnomclasse.Text = classeSelectionnee.nomClasse;

                // Cocher les cours associés à la classe
                for (int i = 0; i < cklistcours.Items.Count; i++)
                {
                    string coursNom = cklistcours.Items[i].ToString();
                    cklistcours.SetItemChecked(i, classeSelectionnee.cours.Any(c => c.nomCours == coursNom));
                }
            }
            else
            {
                MessageBox.Show("Classe non trouvée.");
            }
        }

        private void dataGridView1_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                int idClasse = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["id"].Value);

                // Récupérer la classe sélectionnée
                classeSelectionnee = _context.Classes
                    .Include("cours")
                    .FirstOrDefault(c => c.id == idClasse);

                if (classeSelectionnee != null)
                {
                    // Remplir le formulaire avec les données de la classe sélectionnée
                    txtnomclasse.Text = classeSelectionnee.nomClasse;

                    // Cocher les cours associés
                    for (int i = 0; i < cklistcours.Items.Count; i++)
                    {
                        string coursNom = cklistcours.Items[i].ToString();
                        cklistcours.SetItemChecked(i, classeSelectionnee.cours.Any(c => c.nomCours == coursNom));
                    }
                }
            }
        }

        private void btnupdate_Click(object sender, EventArgs e)
        {
            if (!ValiderChamps() || classeSelectionnee == null)
            {
                MessageBox.Show("Aucune classe sélectionnée pour modification.");
                return;
            }

            try
            {
                classeSelectionnee.nomClasse = txtnomclasse.Text.Trim();

                // Mise à jour des cours associés
                classeSelectionnee.cours.Clear();
                foreach (var item in cklistcours.CheckedItems)
                {
                    var cours = _context.Cours.FirstOrDefault(c => c.nomCours == item.ToString());
                    if (cours != null)
                    {
                        classeSelectionnee.cours.Add(cours);
                    }
                }

                _context.SaveChanges();
                refresh();
                MessageBox.Show("Classe mise à jour avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur lors de la modification : " + ex.Message, "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }




    }
}

