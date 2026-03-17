using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Fou : SansLimite
    {

        //constructeur
        public Fou(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        public Fou(Fou fou) : base(fou.PieceEstBlanche, fou.PieceNaPasBouge) { }

        //methode

        public override bool ValiderGeometrie(Coup coup)
        {
            return coup.EstDiagonal();
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
            return color + "B";
        }

        public override Piece Copier()
        {
            return new Fou(this);            
        }

        //destructeur
        ~Fou () { }

    }
}
