using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GestionSchoolApp
{
    public static class UserSession
    {
        public static string username { get; set; }
        public static string role { get; set; }

        public static void Clear()
        {
            username = null;
            role = null;
        }
    }
}
