using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public  class Tour : SansLimite
    {
   
        //constructeur

        public Tour(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        //methode

        public override bool ValiderGeometrie(Coup coup)
        {
            return true;
        }

        public override bool PeutInitierRoque()
        {
            return true;
        }

        public override bool PeutSuivreRoque()
        {
            return true;
        }

        public override bool PieceEstVulnerable()
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
        ~Tour() { }

    }
}
