using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSystemeEtudiant
{
    internal class Note
    {
        public int id { get; set; }
        public int idE { get; set; }
        public virtual Etudiant etudiant { get; set; }
        public int idM { get; set; }
        public virtual Matiere matiere { get; set; } // 🔥 Doit être virtual
        public float note { get; set; }

    }
}
