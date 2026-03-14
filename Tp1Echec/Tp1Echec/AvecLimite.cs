using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public abstract class AvecLimite : AvecCollision
    {

        
        //constructeur

        public AvecLimite(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        //methode

        public override bool ValiderCoup(Coup coup)
        {
            return ValiderGeometrie(coup) && ValiderLimite(coup);
        }

        public abstract bool ValiderGeometrie(Coup coup);

        public abstract bool ValiderLimite(Coup coup);

        //destructeur

        ~AvecLimite() { }

    }
}
