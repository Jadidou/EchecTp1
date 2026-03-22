using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tp1Echec
{
    // @class FacadeEchec
    // @brief Classe façade qui protege la logique du jeu.
    //        Gère la liste des joueurs et la partie en cours.
    public class FacadeEchec
    {

        //attribut

        private List<Joueur> _listeJoueur;
        private Partie _partie;

        //constructeur

        // @brief Constructeur principal
        //        Initialise la liste de joueurs et aucun partie en cours
        public FacadeEchec() 
        {

            _listeJoueur = new List<Joueur>();
            _partie = null;

        }


        //proprieter

        // @brief Accesseur pour la partie en cours
        public Partie partie
        {

            get { return _partie; }
            set { _partie = value; }

        }

        //indexeur

        // @brief Accède à un joueur par son index dans la liste
        public Joueur this[int index]
        {
            get{return _listeJoueur[index];}
            set{_listeJoueur[index] = value;}
        }

        //methode

        // @brief Retourne la liste des joueurs sous forme de chaîne
        // @return Liste des joueurs (nom et pointage)
        public string ConsulterListeJoueur()
        {
            StringBuilder liste = new StringBuilder();

            foreach (Joueur j in _listeJoueur)
            {
                liste.AppendLine(j.ToString());
            }

            return liste.ToString();
        }

        // @brief Joue un coup sur la partie en cours
        // @param x Position de départ
        // @param y Position d’arrivée
        // @return Code de retour du coup (-1 si aucune partie)
        public int JouerCoup(string x, string y)
        {

            if (_partie == null)
                return -1;

            int codeRetour = _partie.JouerCoup(x, y);

            // Persister les scores après chaque coup valide (ils changent en fin de partie)
            if (codeRetour == 1)
                SauvegarderJoueurs();

            return codeRetour;
        }

        // Applique la promotion choisie par le joueur (après un retour de code 2 de JouerCoup).
        // @brief Applique la promotion choisie par le joueur
        // @param codePiece Type de pièce choisie (Q, R, N, B)
        // @return Code de retour
        public int PromouvoirPion(string codePiece)
        {
            if (_partie == null) return -1;

            int codeRetour = _partie.PromouvoirPion(codePiece);

            if (codeRetour == 1)
                SauvegarderJoueurs();

            return codeRetour;
        }

        // @brief Démarre une partie avec deux joueurs existants
        // @param joueurBlanc Nom du joueur blanc
        // @param joueurNoir Nom du joueur noir
        public void DemarrerPartie(string joueurBlanc, string joueurNoir)
        {
            if (_listeJoueur.Count < 2)
                throw new Exception("Pas assez de joueurs pour démarrer une partie.");

            joueurBlanc = joueurBlanc.Trim();
            joueurNoir = joueurNoir.Trim();

            string pattern = @"^Joueur: ([^,]+), Pointage: \d+$";

            // Extraire nom joueur blanc
            Match matchBlanc = Regex.Match(joueurBlanc, pattern);
            if (!matchBlanc.Success)
                throw new Exception("Format du joueur blanc invalide.");

            string nomBlanc = matchBlanc.Groups[1].Value;

            // Extraire nom joueur noir
            Match matchNoir = Regex.Match(joueurNoir, pattern);
            if (!matchNoir.Success)
                throw new Exception("Format du joueur noir invalide.");

            string nomNoir = matchNoir.Groups[1].Value;

            // Trouver les joueurs dans la liste
            Joueur blanc = _listeJoueur.FirstOrDefault(j => j.nomJoueur == nomBlanc);
            Joueur noir = _listeJoueur.FirstOrDefault(j => j.nomJoueur == nomNoir);

            if (blanc == null)
                throw new Exception("Joueur blanc introuvable.");

            if (noir == null)
                throw new Exception("Joueur noir introuvable.");

            if (blanc == noir)
                throw new Exception("Les deux joueurs doivent être différents.");

            _partie = new Partie(blanc, noir);
            _partie.DemarrerPartie();
        }

        // @brief Abandonne la partie en cours
        public void AbandonnerPartie()
        {

            if (_partie != null)
            {
                _partie.AbandonnerPartie();
                SauvegarderJoueurs();
                _partie = null; 
            }

        }

        // @brief Sauvegarde tous les joueurs dans un fichier texte
        public void SauvegarderJoueurs()
        {
            string chemin = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Joueur.txt");

            using (StreamWriter sw = new StreamWriter(chemin, false)) // overwrite
            {
                foreach (Joueur j in _listeJoueur)
                {
                    sw.WriteLine($"{j.nomJoueur},{j.pointage}");
                }
            }
        }

        // @brief Quitte le programme
        public void QuitterProgramme()
        {

            Environment.Exit(0);

        }

        // @brief Retourne le plateau actuel sous forme de chaîne
        // @return Plateau ou message si aucune partie en cours
        public string AfficherPlateau()
        {

            if (_partie == null)
                return "Aucune partie en cours.";

            return _partie.AfficherPlateau();

        }

        // @brief Demande une nulle pour la partie en cours
        public void DemanderUneNulle()
        {

            if (_partie != null)
            {
                _partie.DemanderUneNulle();
            }

        }

        // @brief Vérifie l’état de la partie
        // @return Code représentant l’état de la partie
        public int VerifierEtatPartie()
        {
            return (int)_partie.VerifierEtatPartie();
        }

        // @brief Ajoute un joueur à la liste
        // @param nom Nom du joueur à ajouter
        public void AjouterJoueur(string nom)
        {
            // Validation doublon
            if (_listeJoueur.Any(j => j.nomJoueur.Equals(nom, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Ce joueur existe déjà.");

            Joueur nouveau = new Joueur(nom, 0);

            _listeJoueur.Add(nouveau);

            SauvegarderJoueurs();
        }

        // @brief Charge les joueurs depuis un fichier texte
        // @param chemin Chemin du fichier contenant les joueurs
        public void ChargerJoueursDepuisFichier(string chemin)
        {
            _listeJoueur.Clear();

            //MessageBox.Show("Chemin utilisé : " + chemin); // DEBUG

            if (!File.Exists(chemin))
            {
                MessageBox.Show("FICHIER INTROUVABLE");
                return;
            }

            foreach (string ligne in File.ReadAllLines(chemin))
            {
                //MessageBox.Show("Ligne lue : " + ligne); // DEBUG

                if (!string.IsNullOrWhiteSpace(ligne))
                {
                    Joueur j = Joueur.ChargerDepuisFichier(ligne);
                    _listeJoueur.Add(j);
                }
            }

            //MessageBox.Show("Nombre de joueurs : " + _listeJoueur.Count); // DEBUG
        }

        //destructeur
        // @brief Destructeur de la classe FacadeEchec
        ~FacadeEchec() { }

    }
}
