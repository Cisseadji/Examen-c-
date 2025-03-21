using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using GestionSchoolApp.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GestionSchoolApp
{
    internal static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            ClassGestion.formLogin = new FormLogin();
            Application.Run(ClassGestion.formLogin);

            // Chemin du fichier Crystal Reports (.rpt)
            string reportPath = @"C:\Users\adjic\Desktop\LICENCE3_ISI\C#\GestionSchoolApp\GestionSchoolApp\ReportReleve.rpt";

            // Création de l'objet Crystal Report
            ReportDocument report = new ReportDocument();
            report.Load(reportPath);

            // Si tu as des paramètres, ajoute-les ici
            // report.SetParameterValue("NomParametre", valeur);

            // Exporter en PDF
            string pdfPath = @"C:\Users\adjic\Desktop\LICENCE3_ISI\C#\GestionSchoolApp\GestionSchoolApp\ReleveNotes.pdf";
            report.ExportToDisk(ExportFormatType.PortableDocFormat, pdfPath);
            Console.WriteLine(" Rapport exporté en PDF avec succès : " + pdfPath);

            // Exporter en Excel
            string excelPath = @"C:\Users\adjic\Desktop\LICENCE3_ISI\C#\GestionSchoolApp\GestionSchoolApp\ReleveNotes.xlsx";
            report.ExportToDisk(ExportFormatType.ExcelWorkbook, excelPath);
            Console.WriteLine(" Rapport exporté en Excel avec succès : " + excelPath);

            // Libérer les ressources
            report.Close();
            report.Dispose();
        }
    }
}
