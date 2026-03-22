using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class Piece
    // @brief Représente une pièce abstraite du jeu d’échecs.
    //        Sert de classe de base pour toutes les pièces (Pion, Tour, Roi, etc.).
    public abstract class Piece
    {

        //atributs
        // @brief Indique si la pièce est blanche (sinon noire)
        protected bool _pieceEstBlanche;
        // @brief Indique si la pièce n’a jamais bougé (utile pour certaines règles spéciales)
        private bool _pieceNaPasBouge;

        //constructeur

        // @brief Constructeur principal
        // @param pieceEstBlanche Indique si la pièce est blanche
        // @param pieceNaPasBouge Indique si la pièce n’a jamais bougé
        public Piece(bool pieceEstBlanche, bool pieceNaPasBouge) 
        {
            _pieceEstBlanche = pieceEstBlanche;
            _pieceNaPasBouge = pieceNaPasBouge;

        }


        // Proprietés

        // @brief Retourne la couleur de la pièce
        public bool PieceEstBlanche
        {
            get { return _pieceEstBlanche; }
        }

        // @brief Indique si la pièce n’a jamais bougé
        public bool PieceNaPasBouge
        {
            get { return _pieceNaPasBouge; }
            set { _pieceNaPasBouge = value; }
        }

        // Méthodes

        // @brief Valide un coup selon les règles de la pièce
        // @param coup Coup à valider
        // @return true si le coup est valide, sinon false
        public abstract bool ValiderCoup(Coup coup);

        // @brief Indique si la pièce peut effectuer un mouvement de charge
        // @return true si applicable, sinon false
        public abstract bool PeutCharger();

        // @brief Indique si la pièce peut effectuer une prise en passant
        // @return true si applicable, sinon false
        public abstract bool PeutPrendreEnPassant();

        // @brief Indique si la pièce cause des collisions sur son trajet
        // @return true si elle peut entrer en collision avec d'autres pièces
        public abstract bool CauseCollision();

        // @brief Indique si la pièce peut initier un roque
        // @return true si applicable, sinon false
        public abstract bool PeutInitierRoque();

        // @brief Indique si la pièce peut participer à un roque
        // @return true si applicable, sinon false
        public abstract bool PeutSuivreRoque();

        // @brief Indique si la pièce peut être promue
        // @return true si applicable, sinon false
        public abstract bool PeutEtrePromu();

        // @brief Indique si la collision se fait uniquement vers l’avant
        // @return true si applicable, sinon false
        public abstract bool CollisionParDevant();

        // @brief Indique si la pièce capture en diagonale
        // @return true si applicable, sinon false
        public abstract bool PriseParDiagonal();

        // @brief Indique si la pièce est vulnérable à une règle spéciale
        // @return true si applicable, sinon false
        public abstract bool PieceEstVulnerable();

        // @brief Sérialise la pièce en code texte
        // @return Code représentant la pièce
        public abstract string Serilization();

        // @brief Crée une copie de la pièce (clonage profond)
        // @return Nouvelle instance identique
        public abstract Piece Copier();

        // @brief Indique que la pièce a bougé ou non
        // @param aBouge true si la pièce a bougé
        public void SetPieceABouge(bool aBouge)
        {

            _pieceNaPasBouge = !aBouge;

        }

        // @brief Retourne si la pièce a déjà bougé
        // @return true si la pièce a bougé, sinon false
        public bool PieceABouge()
        {
            return !_pieceNaPasBouge;
        }



        //destructeur
        // @brief Destructeur de la classe Piece
        ~Piece() { }

    }
}
