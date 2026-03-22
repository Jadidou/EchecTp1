using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class Fou
    // @brief Représente la pièce Fou (Bishop) aux échecs.
    //        Hérite de SansLimite (déplacement sans limite de distance).
    public class Fou : SansLimite
    {

        //constructeur

        // @brief Constructeur principal
        // @param pieceEstBlanche Indique si la pièce est blanche
        // @param pieceNaPasBouge Indique si la pièce n’a jamais bougé
        public Fou(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        // @brief Constructeur de copie (clonage)
        // @param fou Fou à copier
        public Fou(Fou fou) : base(fou.PieceEstBlanche, fou.PieceNaPasBouge) { }

        //methode

        // @brief Valide la géométrie du mouvement du fou
        //        Le fou se déplace uniquement en diagonale
        // @param coup Coup à valider
        // @return true si le mouvement est diagonal, sinon false
        public override bool ValiderGeometrie(Coup coup)
        {
            return coup.EstDiagonal();
        }

        // @brief Indique si la pièce est vulnérable à une règle spéciale
        // @return false (non utilisé pour le fou)
        public override bool PieceEstVulnerable()
        {
            return false;
        }

        // @brief Indique si le fou peut initier un roque
        // @return false (seul le roi peut initier le roque)
        public override bool PeutInitierRoque()
        {
            return false;
        }

        // @brief Indique si le fou peut participer à un roque
        // @return false (le fou ne participe pas au roque)
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

        // @brief Indique si la pièce peut effectuer un mouvement de charge
        // @return false (non applicable au fou)
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
        // @return false (le fou se déplace en diagonale dans toutes les directions)
        public override bool CollisionParDevant()
        {
            return false;
        }

        // @brief Indique si la pièce capture en diagonale
        // @return false (la logique de capture est incluse dans le déplacement)
        public override bool PriseParDiagonal()
        {
            return false;
        }

        // @brief Sérialise la pièce en code texte
        //        Exemple : "WB" (White Bishop) ou "BB" (Black Bishop)
        // @return Code représentant la pièce
        public override string Serilization()
        {
            string color = _pieceEstBlanche ? "W" : "B";
            return color + "B";
        }

        // @brief Crée une copie du fou (clonage profond)
        // @return Nouvelle instance identique
        public override Piece Copier()
        {
            return new Fou(this);            
        }

        //destructeur
        // @brief Destructeur de la classe Fou
        ~Fou () { }

    }
}
