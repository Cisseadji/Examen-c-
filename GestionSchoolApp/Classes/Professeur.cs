using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSystemeEtudiant
{
    internal class Professeur
    {
        public int id {  get; set; }
        public string nom { get; set; }
        public string prenom { get; set; }
        public string email { get; set; }
        public string tel { get; set; }
        public ICollection<Classe> classes { get; set; } = new List<Classe>();
        public ICollection<Matiere> matieres { get; set; } = new List<Matiere>();
    }
}