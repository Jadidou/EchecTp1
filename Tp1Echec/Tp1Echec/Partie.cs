using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

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

        // Plateau en attente lorsqu'une promotion est en cours (avant le choix du joueur).
        private Plateau _plateauEnAttentePromotion = null;

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

            int countBoucle = 3;

            if (_pileEtatPlateau.Count < countBoucle)
                return false;

            string dernier = _pileEtatPlateau.Last();

            int count = _pileEtatPlateau.Count(x => x == dernier);

            return count >= countBoucle;

        }

        public CodeEtatPartie VerifierEtatPartie()
        {
            if (_pilePlateau.Count == 0)
                return CodeEtatPartie.OK;

            Plateau plateauActuel = _pilePlateau.Peek();

            //priorité à la nulle par répétition
            if (VerificationNulleParBoucle())
            { 
                _joueurBlanc.AjusterPoint(0);
                _joueurNoir.AjusterPoint(0);
                return CodeEtatPartie.Nulle;
            }

            //sinon on vérifie l'état normal
            return plateauActuel.VerifierEtatPartie(_tourBlanc);
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
            /*if (!plateauActuel.ValiderCoup(coup))
                return -3;*/ // coup illégal

            var code = plateauActuel.ValiderCoup(coup);

            if (code != CodeErreurCoup.OK)
                return -(int)code;

            // clone du plateau avant de jouer (TRÈS IMPORTANT)
            Plateau nouveauPlateau = new Plateau(plateauActuel);

            // Jouer le coup
            nouveauPlateau.JouerCoup(coup);

            // Si une promotion est en attente, suspendre et laisser le joueur choisir la pièce
            if (nouveauPlateau.PromotionEnAttente)
            {
                _plateauEnAttentePromotion = nouveauPlateau;
                return 2; // PromotionRequise
            }

            return FinaliserCoup(nouveauPlateau);

        }

        // Applique le choix de promotion du joueur et finalise le coup.
        // codePiece : "Q" = Dame, "R" = Tour, "N" = Cavalier, "B" = Fou.
        public int PromouvoirPion(string codePiece)
        {
            if (_plateauEnAttentePromotion == null) return -1;

            _plateauEnAttentePromotion.PromouvoirPion(codePiece);

            Plateau p = _plateauEnAttentePromotion;
            _plateauEnAttentePromotion = null;

            return FinaliserCoup(p);
        }

        // Pousse le plateau sur la pile, enregistre l'état, change le tour et vérifie la fin de partie.
        private int FinaliserCoup(Plateau p)
        {
            // Sauvegarder
            _pilePlateau.Push(p);
            _pileEtatPlateau.Add(p.serilizationPlateau());

            // Changer de tour
            _tourBlanc = !_tourBlanc;

            // Vérifier état de la partie après le coup
            Plateau plateauCourant = _pilePlateau.Peek();

            if (plateauCourant.VerificationEchecMat(_tourBlanc))
            {
                // Le joueur actuel est mat → il perd
                AjusterPointage(!_tourBlanc);
            }
            else if (plateauCourant.VerificationEchecPat(_tourBlanc))
            {
                _joueurBlanc.AjusterPoint(0);
                _joueurNoir.AjusterPoint(0);
            }
            else if (VerificationNulleParBoucle())
            {
                _joueurBlanc.AjusterPoint(0);
                _joueurNoir.AjusterPoint(0);
            }

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

            
                _joueurBlanc.AjusterPoint(0);
                _joueurNoir.AjusterPoint(0);

                _pilePlateau.Clear();
                _pileEtatPlateau.Clear();
            

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
