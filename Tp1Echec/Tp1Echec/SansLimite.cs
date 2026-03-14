using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public abstract class SansLimite : AvecCollision
    {


        //constructeur

        public SansLimite(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        //methode

        public override bool ValiderCoup(Coup coup)
        {
            return true;
        }

        public abstract bool ValiderGeometrie(Coup coup);

        //destructeur
        ~SansLimite() { }


    }
}
