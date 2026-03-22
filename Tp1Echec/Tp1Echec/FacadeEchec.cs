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
    public class FacadeEchec
    {

        //attribut

        private List<Joueur> _listeJoueur;
        private Partie _partie;

        //constructeur

        public FacadeEchec() 
        {

            _listeJoueur = new List<Joueur>();
            _partie = null;

        }


        //proprieter

        public Partie partie
        {

            get { return _partie; }
            set { _partie = value; }

        }

        //indexeur
        public Joueur this[int index]
        {
            get{return _listeJoueur[index];}
            set{_listeJoueur[index] = value;}
        }

        //methode

        // Retourne la liste des joueurs sous forme de string
        public string ConsulterListeJoueur()
        {
            StringBuilder liste = new StringBuilder();

            foreach (Joueur j in _listeJoueur)
            {
                liste.AppendLine(j.ToString());
            }

            return liste.ToString();
        }

        // Joue un coup
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

        // Démarre une partie
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

        // Abandonner la partie
        public void AbandonnerPartie()
        {

            if (_partie != null)
            {
                _partie.AbandonnerPartie();
                SauvegarderJoueurs();
                _partie = null; 
            }

        }
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

        // Quitter le programme
        public void QuitterProgramme()
        {

            Environment.Exit(0);

        }

        // Afficher le plateau
        public string AfficherPlateau()
        {

            if (_partie == null)
                return "Aucune partie en cours.";

            return _partie.AfficherPlateau();

        }

        // Demander une nulle
        public void DemanderUneNulle()
        {

            if (_partie != null)
            {
                _partie.DemanderUneNulle();
            }

        }

        public int VerifierEtatPartie()
        {
            return (int)_partie.VerifierEtatPartie();
        }

        // Ajouter un joueur
        public void AjouterJoueur(string nom)
        {
            // Validation doublon
            if (_listeJoueur.Any(j => j.nomJoueur.Equals(nom, StringComparison.OrdinalIgnoreCase)))
                throw new Exception("Ce joueur existe déjà.");

            Joueur nouveau = new Joueur(nom, 0);

            _listeJoueur.Add(nouveau);

            SauvegarderJoueurs();
        }

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
        ~FacadeEchec() { }

    }
}
