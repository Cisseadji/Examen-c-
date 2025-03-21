using GestionSchoolApp.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSchoolApp
{
    internal static class ClassGestion
    {
        public static FormLogin formLogin { get; set; }
        public static FormAcueil formAcueil { get; set; }
        public static void showFormlogin()
        {
            if(formLogin == null || formLogin.IsDisposed)
            {
                formLogin = new FormLogin();
            }
            formAcueil?.Hide();
            formLogin.Show();
        }
        public static void showFormAcueil()
        {
            if (formAcueil == null || formAcueil.IsDisposed)
            {
                //formAcueil = new FormAcueil();
            }
            formLogin?.Hide();
            formAcueil.Show();
            
        }
    }
}
