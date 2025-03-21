using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace GestionSystemeEtudiant
{
    internal class AppDBContext : DbContext
    {
        public AppDBContext() : base("gestionEtudiant")
        {

        }
        public DbSet<Etudiant> Etudiants { get; set; }
        public DbSet<Classe> Classes { get; set; }
        public DbSet<Cours> Cours { get; set; }
        public DbSet<Professeur> Professeurs { get; set; }
        public DbSet<Matiere> Matieres { get; set; }
        public DbSet<Utilisateur> Utilisateurs { get; set; }
        public DbSet<Note> Notes { get; set; }
        public DbSet<OTPCode> OTPCodes { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relation many-to-many entre Classe et Cours
            modelBuilder.Entity<Classe>()
                .HasMany(c => c.cours)
                .WithMany(c => c.classes)
                .Map(m =>
                {
                    m.ToTable("ClasseCours"); // Nom de la table de liaison
                    m.MapLeftKey("ClasseId"); // Clé étrangère pour Classe
                    m.MapRightKey("CoursId"); // Clé étrangère pour Cours
                });


            // Relation Cours-Matière (Many-to-Many)
            modelBuilder.Entity<Cours>()
                .HasMany(c => c.matieres)
                .WithMany(m => m.cours)
                .Map(m =>
                {
                    m.ToTable("CoursMatieres");  // Table de liaison
                    m.MapLeftKey("CoursId");     // Clé étrangère pour Cours
                    m.MapRightKey("MatiereId");  // Clé étrangère pour Matière
                });



            // Professeur-Classe (Many-to-Many)
            modelBuilder.Entity<Professeur>()
                .HasMany(p => p.classes)
                .WithMany(c => c.professeurs)
                .Map(m =>
                {
                    m.ToTable("ProfesseursClasses");
                    m.MapLeftKey("ProfesseurId");
                    m.MapRightKey("ClasseId");
                });

            // Professeur-Matière (Many-to-Many)
            modelBuilder.Entity<Professeur>()
                .HasMany(p => p.matieres)
                .WithMany(m => m.professeurs)
                .Map(m =>
                {
                    m.ToTable("ProfesseursMatieres");
                    m.MapLeftKey("ProfesseurId");
                    m.MapRightKey("MatiereId");
                });

        }


    }

}
