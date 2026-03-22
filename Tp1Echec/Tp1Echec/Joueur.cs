using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class Joueur
    // @brief Représente un joueur d’échecs avec un nom et un pointage.
    public class Joueur
    {

        //attributs

        private string _nomJoueur;
        private int _pointage;

        //constructeur

        // @brief Constructeur principal
        // @param nomJoueur Nom du joueur
        // @param pointage Pointage initial du joueur
        public Joueur(string nomJoueur, int pointage) 
        {
            _nomJoueur = nomJoueur;
            _pointage = pointage;

        }

        //proprieter

        // @brief Accesseur du nom du joueur
        public string nomJoueur
        {
            get { return _nomJoueur; }
            set { _nomJoueur = value; }

        }

        // @brief Accesseur du pointage du joueur
        public int pointage
        { 
        
            get { return _pointage; }
            set { _pointage = value; }
        }

        //methode

        // @brief Ajuste le pointage du joueur
        // @param points Nombre de points à ajouter (ou retirer)
        public void AjusterPoint(int points)
        {

            // On ajoute les points au pointage actuel
            _pointage += points;

            // empêcher le pointage d'être négatif
            if (_pointage < 0)
                _pointage = 0;

        }

        // @brief Sauvegarde les informations du joueur dans un fichier texte
        // @param chemin Chemin du fichier
        public void SauvegarderDansFichier(string chemin)
        {
            File.AppendAllText(chemin, $"{_nomJoueur},{_pointage}\n");
        }

        // @brief Charge un joueur à partir d’une ligne de fichier texte
        // @param ligne Ligne contenant les données du joueur
        // @return Nouvelle instance de Joueur
        public static Joueur ChargerDepuisFichier(string ligne)
        {
            var parties = ligne.Split(',');
            string nom = parties[0];
            int pointage = int.Parse(parties[1]);
            return new Joueur(nom, pointage);
        }

        // @brief Retourne une représentation texte du joueur
        // @return Chaîne contenant le nom et le pointage
        public override string ToString()
        {
            return $"Joueur: {_nomJoueur}, Pointage: {_pointage}";
        }

        //destructeur
        // @brief Destructeur de la classe Joueur
        ~Joueur() { }

    }
}
