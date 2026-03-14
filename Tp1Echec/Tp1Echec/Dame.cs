using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Dame : SansLimite
    {

        //constructeur

        public Dame(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        //methode

        public override bool ValiderGeometrie(Coup coup)
        {
            return coup.EstDiagonal() || coup.EstOrthogonal();
        }
        public override bool PieceEstVulnerable()
        {
            return false;
        }

        public override bool PeutInitierRoque()
        {
            return false;
        }

        public override bool PeutSuivreRoque()
        {
            return false;
        }

        public override bool PeutEtrePromu()
        {
            return false;
        }

        public override bool PeutCharger()
        {
            return false;
        }

        public override bool PeutPrendreEnPassant()
        {
            return false;
        }

        public override bool CollisionParDevant()
        {
            return false;
        }

        public override bool PriseParDiagonal()
        {
            return false;
        }

        //destructeur
        ~Dame() { }

    }
}
