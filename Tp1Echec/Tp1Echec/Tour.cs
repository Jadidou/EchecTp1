using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class Tour
    // @brief Représente la pièce Tour (Rook) aux échecs.
    //        Hérite de SansLimite (déplacement sans limite de distance).
    public class Tour : SansLimite
    {

        //constructeur

        // @brief Constructeur principal
        // @param pieceEstBlanche Indique si la pièce est blanche
        // @param pieceNaPasBouge Indique si la pièce n’a jamais bougé (utile pour le roque)
        public Tour(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        // @brief Constructeur de copie (clonage)
        // @param tour Tour à copier
        public Tour(Tour tour) : base(tour.PieceEstBlanche, tour.PieceNaPasBouge) { }

        //methode

        // @brief Valide la géométrie du mouvement de la tour
        //        La tour se déplace uniquement en ligne droite (horizontal ou vertical)
        // @param coup Coup à valider
        // @return true si le mouvement est orthogonal, sinon false
        public override bool ValiderGeometrie(Coup coup)
        {
            return coup.EstOrthogonal();
        }

        // @brief Indique si la tour peut initier un roque
        // @return false (c’est le roi qui initie le roque)
        public override bool PeutInitierRoque()
        {
            return false;
        }

        // @brief Indique si la tour peut participer à un roque
        // @return true (la tour suit le roi lors du roque)
        public override bool PeutSuivreRoque()
        {
            return true;
        }

        // @brief Indique si la pièce est vulnérable à une règle spéciale
        // @return false (non utilisé pour la tour)
        public override bool PieceEstVulnerable()
        {
            return false;
        }

        // @brief Indique si la pièce peut être promue
        // @return false (seuls les pions peuvent être promus)
        public override bool PeutEtrePromu()
        {
            return false;
        }

        // @brief Indique si la pièce peut charger (ex: pion double)
        // @return false (non applicable à la tour)
        public override bool PeutCharger()
        {
            return false;
        }

        // @brief Indique si la pièce peut effectuer une prise en passant
        // @return false (réservé aux pions)
        public override bool PeutPrendreEnPassant()
        {
            return false;
        }

        // @brief Indique si la collision se fait uniquement vers l’avant
        // @return false (la tour peut se déplacer dans toutes les directions orthogonales)
        public override bool CollisionParDevant()
        {
            return false;
        }

        // @brief Indique si la pièce capture en diagonale
        // @return false (la tour capture en ligne droite)
        public override bool PriseParDiagonal()
        {
            return false;
        }

        // @brief Sérialise la pièce en code texte
        //        Exemple : "WR" (White Rook) ou "BR" (Black Rook)
        // @return Code représentant la pièce
        public override string Serilization()
        {
            string color = _pieceEstBlanche ? "W" : "B";
            return color + "R";
        }

        // @brief Crée une copie de la tour (clonage profond)
        // @return Nouvelle instance identique
        public override Piece Copier()
        {
            return new Tour(this);
        }

        //destructeur
        // @brief Destructeur de la classe Tour
        ~Tour() { }

    }
}
