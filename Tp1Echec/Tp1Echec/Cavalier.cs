using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class Cavalier
    // @brief Représente la pièce Cavalier (Knight) aux échecs.
    //        Hérite de SansCollision (le cavalier ignore les collisions sur son chemin).
    public class Cavalier : SansCollision
    {

        //constructeur

        // @brief Constructeur principal
        // @param pieceEstBlanche Indique si la pièce est blanche
        // @param pieceNaPasBouge Indique si la pièce n’a jamais bougé
        public Cavalier(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        // @brief Constructeur de copie (clonage)
        // @param cavalier Cavalier à copier
        public Cavalier(Cavalier cavalier) : base(cavalier.PieceEstBlanche, cavalier.PieceNaPasBouge) { }

        //methode

        // @brief Valide la géométrie du mouvement du cavalier
        //        Le cavalier se déplace en "L" (2 cases dans une direction + 1 case perpendiculaire)
        // @param coup Coup à valider
        // @return true si le mouvement est en L, sinon false
        public override bool ValiderCoup(Coup coup)
        {

            return coup.EstEnL();

        }

        // @brief Indique si la pièce est vulnérable à une règle spéciale
        // @return false (non applicable au cavalier)
        public override bool PieceEstVulnerable()
        {
            return false;
        }

        // @brief Indique si le cavalier peut initier un roque
        // @return false (seul le roi peut initier le roque)
        public override bool PeutInitierRoque()
        {
            return false;
        }

        // @brief Indique si le cavalier peut suivre un roque
        // @return false (le cavalier ne participe pas au roque)
        public override bool PeutSuivreRoque()
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
        // @return false (non applicable au cavalier)
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
        // @return false (le cavalier ignore les collisions)
        public override bool CollisionParDevant()
        {
            return false;
        }

        // @brief Indique si la pièce capture en diagonale
        // @return false (le cavalier capture sur sa case d'arrivée, pas de diagonale spéciale)
        public override bool PriseParDiagonal()
        {
            return false;
        }

        // @brief Sérialise la pièce en code texte
        //        Exemple : "WN" (White Knight) ou "BN" (Black Knight)
        // @return Code représentant la pièce
        public override string Serilization()
        {
            string color = _pieceEstBlanche ? "W" : "B";
            return color + "N";
        }

        // @brief Crée une copie du cavalier (clonage profond)
        // @return Nouvelle instance identique
        public override Piece Copier()
        {
            return new Cavalier(this);
        }

        //destructeur
        // @brief Destructeur de la classe Cavalier
        ~Cavalier() { }

    }
}
