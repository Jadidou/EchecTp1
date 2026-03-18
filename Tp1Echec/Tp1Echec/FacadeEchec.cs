using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
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

            return _partie.JouerCoup(x, y);

        }

        // Démarre une partie
        public void DemarrerPartie()
        {

            if (_listeJoueur.Count < 2)
                return;

            Joueur blanc = _listeJoueur[0];
            Joueur noir = _listeJoueur[1];

            _partie = new Partie(blanc, noir);

            _partie.DemarrerPartie();

        }

        // Abandonner la partie
        public void AbandonnerPartie()
        {

            if (_partie != null)
            {
                _partie.AbandonnerPartie();
                _partie = null; 
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

        // Ajouter un joueur
        public void AjouterJoueur(Joueur joueur)
        {

            _listeJoueur.Add(joueur);

        }

        public void ChargerJoueursDepuisFichier(string chemin)
        {
            _listeJoueur.Clear();

            //MessageBox.Show("Chemin utilisé : " + chemin); // DEBUG

            if (!File.Exists(chemin))
            {
                MessageBox.Show("FICHIER INTROUVABLE ❌");
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
