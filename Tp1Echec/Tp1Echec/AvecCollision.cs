using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public abstract class AvecCollision : Piece
    {

        //constructeur

        public AvecCollision(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }


        //methode

        public override bool CauseCollision()
        {
            return true;
        }

        //destructeur
        ~AvecCollision() { }


    }
}
