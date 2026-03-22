using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class Pion
    // @brief Représente la pièce Pion aux échecs.
    //        Hérite de AvecLimite (déplacement limité en distance).
    public class Pion : AvecLimite
    {

        //constructeur

        // @brief Constructeur principal
        // @param pieceEstBlanche Indique si la pièce est blanche
        // @param pieceNaPasBouge Indique si la pièce n’a jamais bougé (utile pour le premier déplacement de 2 cases)
        public Pion(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        // @brief Constructeur de copie (clonage)
        // @param pion Pion à copier
        public Pion(Pion pion) : base(pion.PieceEstBlanche, pion.PieceNaPasBouge) { }

        //methode

        // @brief Valide la géométrie du mouvement du pion
        //        Le pion peut avancer tout droit (orthogonal) ou capturer en diagonale
        // @param coup Coup à valider
        // @return true si le mouvement est valide géométriquement, sinon false
        public override bool ValiderGeometrie(Coup coup)
        {
            return coup.EstOrthogonal() || coup.EstDiagonal();
        }

        // @brief Valide la limite du déplacement du pion
        //        Le pion peut avancer de 1 ou 2 cases maximum
        // @param coup Coup à valider
        // @return true si la longueur est valide, sinon false
        public override bool ValiderLimite(Coup coup)
        {
            return coup.Longueur() <= 2;
        }

        // @brief Indique si le pion peut être promu
        // @return true (le pion peut être promu en atteignant la dernière rangée)
        public override bool PeutEtrePromu()
        {
            return true;
        }

        // @brief Indique si le pion peut effectuer un déplacement de charge (2 cases)
        // @return true (autorisé au premier mouvement)
        public override bool PeutCharger()
        {
            return true;
        }

        // @brief Indique si le pion peut effectuer une prise en passant
        // @return true (règle spéciale du pion)
        public override bool PeutPrendreEnPassant()
        {
            return true;
        }

        // @brief Indique si la collision se fait uniquement vers l’avant
        // @return true (le pion avance uniquement vers l’avant)
        public override bool CollisionParDevant()
        {
            return true;
        }

        // @brief Indique si le pion capture en diagonale
        // @return true (le pion capture uniquement en diagonale)
        public override bool PriseParDiagonal()
        {
            return true;
        }

        // @brief Indique si la pièce est vulnérable à une règle spéciale
        // @return false (non utilisé directement ici)
        public override bool PieceEstVulnerable()
        {
            return false;
        }

        // @brief Indique si le pion peut initier un roque
        // @return false (seul le roi peut initier le roque)
        public override bool PeutInitierRoque()
        {
            return false;
        }

        // @brief Indique si le pion peut participer à un roque
        // @return false (le pion ne participe pas au roque)
        public override bool PeutSuivreRoque()
        {
            return false;
        }

        // @brief Sérialise la pièce en code texte
        //        Exemple : "WP" (White Pawn) ou "BP" (Black Pawn)
        // @return Code représentant la pièce
        public override string Serilization()
        {
            string color = _pieceEstBlanche ? "W" : "B";
            return color + "P";
        }

        // @brief Crée une copie du pion (clonage profond)
        // @return Nouvelle instance identique
        public override Piece Copier()
        {
            return new Pion(this);
        }

        //destructeur
        // @brief Destructeur de la classe Pion
        ~Pion() { }

    }
}
