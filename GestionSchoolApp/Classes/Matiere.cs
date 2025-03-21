using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSystemeEtudiant
{
    internal class Matiere
    {
        public int id {get;set;}
        public string nomMatiere {get;set;}
        public ICollection<Professeur> professeurs { get; set; }
        public ICollection<Cours> cours { get; set; }



    }
}
