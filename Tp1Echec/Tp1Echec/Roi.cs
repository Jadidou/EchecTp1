using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Roi : AvecLimite
    {


        //constructeur

        public Roi(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        //methode

        public override bool ValiderGeometrie(Coup coup)
        {
            return coup.EstOrthogonal() || coup.EstDiagonal();
        }

        public override bool ValiderLimite(Coup coup)
        {
            return coup.Longueur() == 1;
        }

        public override bool PieceEstVulnerable()
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
            return color + "K";
        }

        //destructeur
        ~Roi() { }
    }
}
