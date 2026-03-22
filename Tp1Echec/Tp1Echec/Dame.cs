using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class Dame
    // @brief Représente la pièce Dame (Queen) aux échecs.
    //        Hérite de SansLimite (déplacement sans limite de distance).
    public class Dame : SansLimite
    {

        //constructeur

        // @brief Constructeur principal
        // @param pieceEstBlanche Indique si la pièce est blanche
        // @param pieceNaPasBouge Indique si la pièce n’a jamais bougé
        public Dame(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        // @brief Constructeur de copie (clonage)
        // @param dame Dame à copier
        public Dame(Dame dame) : base(dame.PieceEstBlanche, dame.PieceNaPasBouge) { }

        //methode

        // @brief Valide la géométrie du mouvement de la dame
        //        La dame se déplace en ligne droite ou en diagonale
        // @param coup Coup à valider
        // @return true si le mouvement est valide, sinon false
        public override bool ValiderGeometrie(Coup coup)
        {
            return coup.EstDiagonal() || coup.EstOrthogonal();
        }

        // @brief Indique si la pièce est vulnérable à une règle spéciale
        // @return false (non applicable à la dame)
        public override bool PieceEstVulnerable()
        {
            return false;
        }

        // @brief Indique si la dame peut initier un roque
        // @return false (seul le roi peut initier le roque)
        public override bool PeutInitierRoque()
        {
            return false;
        }

        // @brief Indique si la dame peut suivre un roque
        // @return false (la dame ne participe pas au roque)
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
        // @return false (non applicable à la dame)
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
        // @return false (la dame peut se déplacer dans toutes les directions)
        public override bool CollisionParDevant()
        {
            return false;
        }

        // @brief Indique si la pièce capture en diagonale
        // @return false (la dame capture en ligne droite ou diagonale, mais cette méthode spécifique n’est pas utilisée)
        public override bool PriseParDiagonal()
        {
            return false;
        }

        // @brief Sérialise la pièce en code texte
        //        Exemple : "WQ" (White Queen) ou "BQ" (Black Queen)
        // @return Code représentant la pièce
        public override string Serilization()
        {
            string color = _pieceEstBlanche ? "W" : "B";
            return color + "Q";
        }

        // @brief Crée une copie de la dame (clonage profond)
        // @return Nouvelle instance identique
        public override Piece Copier()
        {
            return new Dame(this);
        }

        //destructeur
        // @brief Destructeur de la classe Dame
        ~Dame() { }

    }
}
