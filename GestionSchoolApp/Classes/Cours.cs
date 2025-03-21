using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSystemeEtudiant
{
    internal class Cours
    {
        public int id {  get; set; }
        public string nomCours { get; set; }
        public string description { get; set; }
        public ICollection<Classe> classes { get; set; } = new List<Classe>();
        public ICollection<Matiere> matieres { get; set; } = new List<Matiere>();


    }
}
