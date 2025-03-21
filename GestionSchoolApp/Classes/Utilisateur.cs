using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSystemeEtudiant
{
    internal class Utilisateur
    {
        public int id {  get; set; }
        public string nomUser { get; set; }
        public string mdp { get; set; }
        public string role { get; set; }
        public string tel { get; set; }
        
    }
}
