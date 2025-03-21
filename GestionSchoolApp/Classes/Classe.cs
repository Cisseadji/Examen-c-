using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSystemeEtudiant
{
    internal class Classe
    {
        public int id { get; set; }
        public string nomClasse { get; set; }
        public ICollection<Cours> cours { get; set; }
        public ICollection<Etudiant> etudiants { get; set; }
        public ICollection<Professeur> professeurs { get; set; }


        
    }
}
