using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Plateau
    {

        //attribut

        //private Array<Piece> _grillage;
        //bien ecrit en dessous
        private Piece[,] _grillage;

        //constructeur

        public Plateau() 
        {

            //_grillage = new Array<Piece>();
            _grillage = new Piece[8, 8];

        }

        //indexeur

        public Piece this[int x, int y]
        {
            get { return _grillage[x, y]; }
            set { _grillage[x, y] = value; }
        }

        //methode

        public string serilizationPlateau() 
        {

            return "";
        }

        public void ValiderCoup(Coup coup)
        {


        }

        public bool PositionDansPlateau(int x,int y)
        {

            return false;
        }

        public void JouerCoup(Coup coup)
        {


        }

        public bool VerificationEchec()
        { 
            return false; 
        }

        public bool VerificationEchecMat()
        {
            return false;
        }

        public bool VerificationEchecPat()
        {
            return false;
        }

        //destructeur
        ~Plateau() { }

    }
}
