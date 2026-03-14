using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tp1Echec
{
    public static class Program
    {
        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }

        public static string ConsulterListeJoueur()
        {
            return "";
        }

        public static int JouerCoup(string x, string y)
        {

            return 0;
        }

        public static void DemarrerPartie()
        {


        }

        public static void AbandonnerPartie()
        {


        }

        public static void QuitterProgramme()
        {


        }

        public static string AfficherPlateau()
        {

            return "";
        }

        public static void DemanderUneNulle()
        {


        }
    }
}
