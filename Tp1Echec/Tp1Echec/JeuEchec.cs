using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tp1Echec
{
    public static class Program
    {

        //attribut

        private static FacadeEchec _facade = new FacadeEchec();


        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VuePrincipale());
        }

        public static string ConsulterListeJoueur()
        {
            return _facade.ConsulterListeJoueur();
        }

        public static int JouerCoup(string x, string y)
        {

            return _facade.JouerCoup(x, y);
        }

        public static void DemarrerPartie(string joueurBlanc, string joueurNoir)
        {

            _facade.DemarrerPartie(joueurBlanc, joueurNoir);

        }

        public static void AbandonnerPartie()
        {

            _facade.AbandonnerPartie();

        }

        public static void QuitterProgramme()
        {

            _facade.QuitterProgramme();

        }

        public static string AfficherPlateau()
        {

            return _facade.AfficherPlateau();

        }

        public static void DemanderUneNulle()
        {

            _facade.DemanderUneNulle();

        }
        public static void AjouterJoueur(Joueur joueur)
        {

            _facade.AjouterJoueur(joueur);
            string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Joueur.txt");
            joueur.SauvegarderDansFichier(chemin);

        }
        public static void ChargerJoueurs()
        {
            string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Joueur.txt");
            _facade.ChargerJoueursDepuisFichier(chemin);

        }
    }
}
