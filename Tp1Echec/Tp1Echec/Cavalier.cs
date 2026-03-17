using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Cavalier : SansCollision
    {

        //constructeur
        public Cavalier(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        //methode

        public override bool ValiderCoup(Coup coup)
        {

            return coup.EstEnL();

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
        public override string Serilization()
        {
            string color = _pieceEstBlanche ? "W" : "B";
            return color + "N";
        }

        //destructeur
        ~Cavalier() { }

    }
}
