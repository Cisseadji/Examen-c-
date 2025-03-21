using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSystemeEtudiant
{
    internal class Etudiant
    {
        public int id { get; set; }
        public string matricule { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public DateTime datenaiss { get; set; }
        public string sexe { get; set; }
        public string adresse { get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        public int idC { get; set; }
        public Classe classe { get; set; }
        public ICollection<Note> Notes { get; set; } = new List<Note>();
    }

    internal class EtudiantView
    {
        public int id { get; set; }
        public string matricule { get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public DateTime datenaiss { get; set; }
        public string sexe { get; set; }
        public string adresse { get; set; }
        public string tel { get; set; }
        public string email { get; set; }
        public string classe { get; set; }
    }
}
