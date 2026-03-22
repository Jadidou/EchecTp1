using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class AvecCollision
    // @brief Classe abstraite pour les pièces qui peuvent entrer en collision avec d'autres pièces sur le plateau.
    //        Hérite de Piece et sert de base pour les pièces comme Pion ou Tour qui doivent gérer les collisions.
    public abstract class AvecCollision : Piece
    {

        //constructeur

        // @brief Constructeur principal
        // @param pieceEstBlanche Indique si la pièce est blanche
        // @param pieceNaPasBouge Indique si la pièce n’a jamais bougé
        public AvecCollision(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }


        //methode

        // @brief Indique si la pièce provoque une collision lorsqu'elle se déplace
        // @return true (les pièces avec collision doivent tenir compte des autres pièces sur le plateau)
        public override bool CauseCollision()
        {
            return true;
        }

        //destructeur
        // @brief Destructeur de la classe AvecCollision
        ~AvecCollision() { }


    }
}
