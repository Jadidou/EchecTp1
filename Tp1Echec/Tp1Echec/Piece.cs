using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public abstract class Piece
    {

        //atributs

        private bool _pieceEstBlanche;
        private bool _pieceNaPasBouge;

        //constructeur

        public Piece(bool pieceEstBlanche, bool pieceNaPasBouge) 
        {
            _pieceEstBlanche = pieceEstBlanche;
            _pieceNaPasBouge = pieceNaPasBouge;

        }


        //proprieter

        public bool PieceEstBlanche
        {
            get { return _pieceEstBlanche; }
        }

        public bool PieceNaPasBouge
        {
            get { return _pieceNaPasBouge; }
            set { _pieceNaPasBouge = value; }
        }

        //methode

        public abstract bool ValiderCoup(Coup coup);

        public abstract bool PeutCharger();

        public abstract bool PeutPrendreEnPassant();

        public abstract bool CauseCollision();

        public abstract bool PeutInitierRoque();

        public abstract bool PeutSuivreRoque();

        public abstract bool PeutEtrePromu();

        public abstract bool CollisionParDevant();

        public abstract bool PriseParDiagonal();

        public abstract bool PieceEstVulnerable();

        public void SetPieceABouge(bool aBouge)
        {


        }

        public bool PieceABouge()
        {
            return true;
        }

        public string Serilization()
        {

            return "";
        }

        //destructeur
        ~Piece() { }

    }
}
