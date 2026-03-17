using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Pion : AvecLimite
    {

        //constructeur

        public Pion(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        //methode

        public override bool ValiderGeometrie(Coup coup)
        {
            return coup.EstOrthogonal() || coup.EstDiagonal();
        }

        public override bool ValiderLimite(Coup coup)
        {
            return coup.Longueur() <= 2;
        }

        public override bool PeutEtrePromu()
        {
            return true;
        }

        public override bool PeutCharger()
        {
            return true;
        }

        public override bool PeutPrendreEnPassant()
        {
            return true;
        }

        public override bool CollisionParDevant()
        {
            return true;
        }

        public override bool PriseParDiagonal()
        {
            return true;
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

        public override string Serilization()
        {
            string color = _pieceEstBlanche ? "W" : "B";
            return color + "P";
        }

        //destructeur
        ~Pion() { }

    }
}
