using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Partie
    {

        //attributs

        private Joueur _joueurBlanc;
        private Joueur _joueurNoir;
        private Stack<Plateau> _pilePlateau;
        private List<string> _pileEtatPlateau;
        private bool _tourBlanc;

        //constructeur

        public Partie(Joueur joueurBlanc, Joueur joueurNoir) 
        {

            _joueurBlanc = joueurBlanc;
            _joueurNoir = joueurNoir;

            _pilePlateau = new Stack<Plateau>();
            _pileEtatPlateau = new List<string>();

            _tourBlanc = true; //blanc commence toujours

        }


        //proprieter

        public Joueur joueurBlanc
        {
            get { return _joueurBlanc; }
            set { _joueurBlanc = value; }
        }

        public Joueur joueurNoir
        {
            get { return _joueurNoir; }
            set { _joueurNoir = value; }
        }

        //indexeur

        public Plateau this[int index]
        {
            get
            {
                return _pilePlateau.ToArray()[index];
            }
        }

        public string this[string index]
        {
            get
            {
                int i = int.Parse(index);
                return _pileEtatPlateau[i];
            }
            set
            {
                int i = int.Parse(index);
                _pileEtatPlateau[i] = value;
            }
        }

        //methode

        // Vérifie répétition de position (nulle) (3 répétitions)
        public bool VerificationNulleParBoucle()
        {

            if (_pileEtatPlateau.Count < 3)
                return false;

            string dernier = _pileEtatPlateau.Last();

            int count = _pileEtatPlateau.Count(x => x == dernier);

            return count >= 3;

        }

        // Ajuste le pointage (ex: abandon, mat, nulle)
        public void AjusterPointage(bool blancGagne)
        {

            if (blancGagne)
            {
                _joueurBlanc.AjusterPoint(1);
                _joueurNoir.AjusterPoint(0);
            }
            else
            {
                _joueurNoir.AjusterPoint(1);
                _joueurBlanc.AjusterPoint(0);
            }

        }

        public int JouerCoup(string x, string y)
        {

            if (_pilePlateau.Count == 0)
                return -1;

            Plateau plateauActuel = _pilePlateau.Peek();

            // Créer le coup avec le bon tour
            Coup coup;
            try
            {
                coup = new Coup(x, y, _tourBlanc);
            }
            catch
            {
                return -2; // format invalide
            }

            // Vérifier le coup
            if (!plateauActuel.ValiderCoup(coup))
                return -3; // coup illégal

            // clone du plateau avant de jouer (TRÈS IMPORTANT)
            Plateau nouveauPlateau = new Plateau(plateauActuel);

            // Jouer le coup
            nouveauPlateau.JouerCoup(coup);

            // Sauvegarder
            _pilePlateau.Push(nouveauPlateau);
            _pileEtatPlateau.Add(nouveauPlateau.serilizationPlateau());

            // Changer de tour
            _tourBlanc = !_tourBlanc;

            return 1;

        }

        //démarre la partie
        public void DemarrerPartie()
        {

            Plateau plateau = new Plateau();
            plateau.InitialiserPlateau();

            _pilePlateau.Clear();
            _pileEtatPlateau.Clear();

            _pilePlateau.Push(plateau);
            _pileEtatPlateau.Add(plateau.serilizationPlateau());

            _tourBlanc = true;

        }

        //abandonne la partie
        public void AbandonnerPartie()
        {

            if (_tourBlanc)
                AjusterPointage(false); // noir gagne
            else
                AjusterPointage(true);  // blanc gagne

            _pilePlateau.Clear();
            _pileEtatPlateau.Clear();

        }

        //demande une nulle
        public void DemanderUneNulle()
        {

            if (VerificationNulleParBoucle())
            {
                _joueurBlanc.AjusterPoint(0);
                _joueurNoir.AjusterPoint(0);

                _pilePlateau.Clear();
                _pileEtatPlateau.Clear();
            }

        }

        //affiche le plateau
        public string AfficherPlateau()
        {

            if (_pilePlateau.Count == 0)
                return "Aucune partie.";

            return _pilePlateau.Peek().serilizationPlateau();

        }

        //la methode undo si jamais!
        public void AnnulerDernierCoup()
        {
            if (_pilePlateau.Count > 1)
            {
                _pilePlateau.Pop();
                _pileEtatPlateau.RemoveAt(_pileEtatPlateau.Count - 1);

                _tourBlanc = !_tourBlanc;
            }
        }


        //destructeur
        ~Partie() { }

    }
}
