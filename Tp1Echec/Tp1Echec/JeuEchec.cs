using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tp1Echec
{
    // @class JeuEchec
    // @brief Controleur.
    //        Sert de façade statique pour communiquer entre l’interface utilisateur et la logique du jeu.
    public static class JeuEchec
    {

        //attribut

        // @brief Instance de la façade qui gère la logique du jeu
        private static FacadeEchec _facade = new FacadeEchec();


        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        // @brief Lance l’application Windows Forms
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new VuePrincipale());
        }

        // @brief Retourne la liste des joueurs
        // @return Liste des joueurs sous forme de chaîne
        public static string ConsulterListeJoueur()
        {
            return _facade.ConsulterListeJoueur();
        }

        // @brief Joue un coup
        // @param x Position de départ
        // @param y Position d’arrivée
        // @return Code de résultat
        public static int JouerCoup(string x, string y)
        {

            return _facade.JouerCoup(x, y);
        }

        // @brief Promeut un pion
        // @param codePiece Type de pièce choisie (Q, R, N, B)
        // @return Code de résultat
        public static int PromouvoirPion(string codePiece)
        {
            return _facade.PromouvoirPion(codePiece);
        }

        // @brief Démarre une nouvelle partie
        // @param joueurBlanc Nom du joueur blanc
        // @param joueurNoir Nom du joueur noir
        public static void DemarrerPartie(string joueurBlanc, string joueurNoir)
        {

            _facade.DemarrerPartie(joueurBlanc, joueurNoir);

        }

        // @brief Abandonne la partie en cours
        public static void AbandonnerPartie()
        {

            _facade.AbandonnerPartie();

        }

        // @brief Quitte le programme
        public static void QuitterProgramme()
        {

            _facade.QuitterProgramme();

        }

        // @brief Vérifie l’état de la partie
        // @return Code représentant l’état de la partie
        public static int VerifierEtatPartie()
        {
            return _facade.VerifierEtatPartie();
        }

        // @brief Retourne une représentation du plateau
        // @return Plateau sous forme de chaîne
        public static string AfficherPlateau()
        {

            return _facade.AfficherPlateau();

        }

        // @brief Permet de demander une nulle
        public static void DemanderUneNulle()
        {

            _facade.DemanderUneNulle();

        }

        // @brief Ajoute un joueur
        // @param joueur Nom du joueur
        public static void AjouterJoueur(string joueur)
        {

            _facade.AjouterJoueur(joueur);
            string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Joueur.txt");
            //joueur.SauvegarderDansFichier(chemin);

        }

        // @brief Charge les joueurs depuis un fichier
        public static void ChargerJoueurs()
        {
            string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Joueur.txt");
            _facade.ChargerJoueursDepuisFichier(chemin);

        }
    }
}
