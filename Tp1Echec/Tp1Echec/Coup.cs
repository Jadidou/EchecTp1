using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Coup
    {

        //attributs

        private (int, int) _posDebut;
        private (int, int) _posFin;
        private bool _estTourBlanc;

        //constructeur

        public Coup((int, int) posDebut, (int, int) posFin, bool estTourBlanc) //je suis pas sur ici!
        { 
        
            _posDebut = posDebut;
            _posFin = posFin;
            _estTourBlanc = estTourBlanc;
        
        }


        //proprieter

        public (int, int) PosDebut
        {
            get { return _posDebut; }
            set { _posDebut = value; }
        }

        public (int, int) PosFin
        {
            get { return _posFin; }
            set { _posFin = value; }
        }

        public bool EstTourBlanc
        {
            get { return _estTourBlanc; }
            set { _estTourBlanc = value; }
        }

        //methode

        public bool EstDiagonal()
        {
            return true;
        }

        public bool EstOrthogonal()
        { 
        
            return true;
        }

        public bool EstEnL()
        {

            return true;
        }

        public int Longueur() //est ce que c'est un int qui va ici?
        {

            return 0;
        }

        //destructeur
        ~Coup() { }

    }
}
