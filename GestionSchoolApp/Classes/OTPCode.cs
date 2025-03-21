using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSystemeEtudiant
{
    internal class OTPCode
    {
        public int id {  get; set; }
        public int idU { get; set; }
        public Utilisateur utilisateur { get; set; }
        public string code { get; set; }
        public DateTime dateExp { get; set; }

    }
}
