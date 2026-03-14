using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public abstract class SansCollision : Piece
    {

        //attributs


        //contructeur

        public SansCollision(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche,pieceNaPasBouge) { }

        //methode

        public override bool CauseCollision()
        {
            return false;
        }

        //destructeur
        ~SansCollision() { }

    }
}
