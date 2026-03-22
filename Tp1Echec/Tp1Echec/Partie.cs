using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tp1Echec
{
    // @class Partie
    // @brief Représente une partie d’échecs complète.
    //        Gère les joueurs, les coups, les états du plateau et les règles de fin de partie.
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
        // @brief Constructeur principal
        // @param joueurBlanc Joueur blanc
        // @param joueurNoir Joueur noir
        public Partie(Joueur joueurBlanc, Joueur joueurNoir) 
        {

            _joueurBlanc = joueurBlanc;
            _joueurNoir = joueurNoir;

            _pilePlateau = new Stack<Plateau>();
            _pileEtatPlateau = new List<string>();

            _tourBlanc = true; //blanc commence toujours

        }


        //proprieter

        // @brief Accesseur du joueur blanc
        public Joueur joueurBlanc
        {
            get { return _joueurBlanc; }
            set { _joueurBlanc = value; }
        }

        // @brief Accesseur du joueur noir
        public Joueur joueurNoir
        {
            get { return _joueurNoir; }
            set { _joueurNoir = value; }
        }

        //indexeur

        // @brief Accès à un plateau dans l’historique
        // @param index Position dans la pile
        public Plateau this[int index]
        {
            get
            {
                return _pilePlateau.ToArray()[index];
            }
        }

        // @brief Accès à un état de plateau (string)
        // @param index Index sous forme de string
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

        // @brief Vérifie une nulle par répétition de position (3 fois)
        // @return true si répétition détectée, sinon false
        public bool VerificationNulleParBoucle()
        {

            int countBoucle = 3;

            if (_pileEtatPlateau.Count < countBoucle)
                return false;

            string dernier = _pileEtatPlateau.Last();

            int count = _pileEtatPlateau.Count(x => x == dernier);

            return count >= countBoucle;

        }

        // @brief Vérifie l’état actuel de la partie (mat, pat, nulle, etc.)
        // @return Code représentant l’état de la partie
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

        // @brief Ajuste le pointage des joueurs
        // @param blancGagne true si les blancs gagnent, sinon noirs
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

        // @brief Joue un coup à partir de coordonnées texte
        // @param x Position de départ
        // @param y Position d’arrivée
        // @return Code de succès ou erreur
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
        // @brief Applique la promotion d’un pion
        // @param codePiece Type de pièce choisie (Q, R, N, B)
        // @return Code de succès
        public int PromouvoirPion(string codePiece)
        {
            if (_plateauEnAttentePromotion == null) return -1;

            _plateauEnAttentePromotion.PromouvoirPion(codePiece);

            Plateau p = _plateauEnAttentePromotion;
            _plateauEnAttentePromotion = null;

            return FinaliserCoup(p);
        }

        // Pousse le plateau sur la pile, enregistre l'état, change le tour et vérifie la fin de partie.
        // @brief Finalise un coup (sauvegarde, changement de tour, vérifications)
        // @param p Plateau après coup
        // @return Code de succès
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

        // @brief Initialise et démarre une nouvelle partie
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

        // @brief Abandonne la partie
        public void AbandonnerPartie()
        {

            if (_tourBlanc)
                AjusterPointage(false); // noir gagne
            else
                AjusterPointage(true);  // blanc gagne

            _pilePlateau.Clear();
            _pileEtatPlateau.Clear();

        }

        // @brief Demande une nulle
        public void DemanderUneNulle()
        {

            
                _joueurBlanc.AjusterPoint(0);
                _joueurNoir.AjusterPoint(0);

                _pilePlateau.Clear();
                _pileEtatPlateau.Clear();
            

        }

        // @brief Retourne une représentation texte du plateau actuel
        // @return Plateau sérialisé
        public string AfficherPlateau()
        {

            if (_pilePlateau.Count == 0)
                return "Aucune partie.";

            return _pilePlateau.Peek().serilizationPlateau();

        }

        // @brief Annule le dernier coup joué
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
        // @brief Destructeur de la classe Partie
        ~Partie() { }

    }
}
