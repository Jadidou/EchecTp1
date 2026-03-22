using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class Roi
    // @brief Représente la pièce Roi aux échecs.
    //        Hérite de AvecLimite (déplacement limité à une case).
    //        Le roi est une pièce spéciale (échec, mat, roque).
    public class Roi : AvecLimite
    {

        //constructeur

        // @brief Constructeur principal
        // @param pieceEstBlanche Indique si la pièce est blanche
        // @param pieceNaPasBouge Indique si le roi n’a jamais bougé (important pour le roque)
        public Roi(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        // @brief Constructeur de copie (clonage)
        // @param roi Roi à copier
        public Roi(Roi roi) : base(roi.PieceEstBlanche, roi.PieceNaPasBouge) { }

        //methode

        // @brief Valide la géométrie du déplacement
        //        Le roi peut se déplacer dans toutes les directions (orthogonale + diagonale)
        // @param coup Coup à valider
        // @return true si le mouvement est valide géométriquement
        public override bool ValiderGeometrie(Coup coup)
        {
            return coup.EstOrthogonal() || coup.EstDiagonal();
        }

        // @brief Vérifie la limite de déplacement
        //        Le roi ne peut se déplacer que d'une seule case
        // @param coup Coup à valider
        // @return true si la distance est de 1
        public override bool ValiderLimite(Coup coup)
        {
            return coup.Longueur() == 1;
        }

        // @brief Indique si la pièce est vulnérable (peut être en échec)
        // @return true (le roi est la pièce principale du jeu)
        public override bool PieceEstVulnerable()
        {
            return true;
        }

        // @brief Indique si le roi peut initier un roque
        // @return true (c’est toujours le roi qui déclenche le roque)
        public override bool PeutInitierRoque()
        {
            return true;
        }

        // @brief Indique si le roi peut suivre un roque
        // @return true (utilisé dans certaines logiques de validation)
        public override bool PeutSuivreRoque()
        {
            return true;
        }

        // @brief Indique si la pièce peut être promue
        // @return false (seuls les pions peuvent être promus)
        public override bool PeutEtrePromu()
        {
            return false;
        }

        // @brief Indique si la pièce peut effectuer un mouvement spécial de charge
        // @return false (non applicable au roi)
        public override bool PeutCharger()
        {
            return false;
        }

        // @brief Indique si la pièce peut faire une prise en passant
        // @return false (réservé aux pions)
        public override bool PeutPrendreEnPassant()
        {
            return false;
        }

        // @brief Indique si la collision se fait uniquement vers l’avant
        // @return false (le roi peut se déplacer dans toutes les directions)
        public override bool CollisionParDevant()
        {
            return false;
        }

        // @brief Indique si la pièce capture en diagonale uniquement
        // @return false (le roi capture dans toutes les directions)
        public override bool PriseParDiagonal()
        {
            return false;
        }

        // @brief Sérialise la pièce en code texte
        //        Exemple : "WK" (White King) ou "BK" (Black King)
        // @return Code représentant le roi
        public override string Serilization()
        {
            string color = _pieceEstBlanche ? "W" : "B";
            return color + "K";
        }

        // @brief Crée une copie du roi (clonage profond)
        // @return Nouvelle instance identique
        public override Piece Copier()
        {
            return new Roi(this);
        }

        //destructeur
        // @brief Destructeur de la classe Roi
        ~Roi() { }
    }
}
