using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class SansCollision
    // @brief Classe abstraite représentant les pièces qui ne sont jamais bloquées
    //        par d'autres pièces (ex : Cavalier).
    //        Hérite de Piece.
    public abstract class SansCollision : Piece
    {

        //contructeur

        // @brief Constructeur de la classe SansCollision
        // @param pieceEstBlanche Indique si la pièce est blanche
        // @param pieceNaPasBouge Indique si la pièce n’a jamais bougé
        public SansCollision(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche,pieceNaPasBouge) { }

        //methode

        //Les pièces ne sont jamais bloquées par d'autres pièces
        // @brief Indique si la pièce peut être bloquée par une autre pièce
        //        Les pièces sans collision (ex : Cavalier) ignorent les obstacles
        // @return false (aucune collision possible)
        public override bool CauseCollision()
        {
            return false;
        }

        //destructeur
        // @brief Destructeur de la classe SansCollision
        ~SansCollision() { }

    }
}
