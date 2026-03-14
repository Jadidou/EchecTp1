using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Joueur
    {

        //attributs

        private string _nomJoueur;
        private int _pointage;

        //constructeur

        public Joueur(string nomJoueur, int pointage) 
        {
            _nomJoueur = nomJoueur;
            _pointage = pointage;

        }

        //proprieter

        public string nomJoueur
        {
            get { return _nomJoueur; }
            set { _nomJoueur = value; }

        }

        public int pointage
        { 
        
            get { return _pointage; }
            set { _pointage = value; }
        }

        //methode

        public void AjusterPoint(int points)
        {

        }

        //destructeur
        ~Joueur() { }

    }
}
