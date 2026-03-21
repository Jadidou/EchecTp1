using System;
using System.Collections.Generic;
using System.IO;
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

        // Méthode pour ajuster le pointage du joueur
        public void AjusterPoint(int points)
        {

            // On ajoute les points au pointage actuel
            _pointage += points;

            // empêcher le pointage d'être négatif
            if (_pointage < 0)
                _pointage = 0;

        }

        //À REVÉRIFIER CETTE PARTIE!!!

        //Méthodes pour intéragir avec fichier txt
        public void SauvegarderDansFichier(string chemin)
        {
            File.AppendAllText(chemin, $"{_nomJoueur},{_pointage}\n");
        }

        public static Joueur ChargerDepuisFichier(string ligne)
        {
            var parties = ligne.Split(',');
            string nom = parties[0];
            int pointage = int.Parse(parties[1]);
            return new Joueur(nom, pointage);
        }

        public override string ToString()
        {
            return $"Joueur: {_nomJoueur}, Pointage: {_pointage}";
        }

        //destructeur
        ~Joueur() { }

    }
}
