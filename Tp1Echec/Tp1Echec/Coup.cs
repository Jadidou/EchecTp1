using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Tp1Echec
{
    // @class Coup
    // @brief Classe qui encapsule le comportement lié au coup, c-à-d, la géométrie du coup et les déplacements.
    public class Coup
    {

        // Membres de la classe
        // Remarque: Les coordonées sont encodées des tuples, donc le tuple est (colonne, ligne)
        private (int, int) _posDebut; //< Tuple de la position initiale.
        private (int, int) _posFin; //< Tuple de la position finale.
        private bool _estTourBlanc; //< Booleen qui détermine le tour du coup.

        // @brief Contructeur de base du coup.
        // @param posDebut Contient les coordonées de début du coup.
        // @param posFin Contient les coordonées de fin du coup.
        // @param estTourBlanc permet de savoir si c'est le tour des joueurs blancs.
        // @return Le coup.
        public Coup((int, int) posDebut, (int, int) posFin, bool estTourBlanc)
        { 
            _posDebut = posDebut;
            _posFin = posFin;
            _estTourBlanc = estTourBlanc;
        
        }

        // @brief Contructeur du coup à partir de coordonées d'échiquier.
        // @param posDebut Les coordonnées d'échiquier de début du coup.
        // @param posFin Les coordonnées d'échiquier de début du coup.
        // @param estTourBlanc permet de savoir si c'est le tour des joueurs blancs.
        // @return Le coup.
        // @throws ArgumentError Si les entrées ne sont pas conforme à l'expression régulière `^[a-hA-H][1-8]$`.
        public Coup(string posDebut, string posFin, bool estTourBlanc)
        { 
            // Basée sur les coordonnées de base d'un échiquier: a1 à h8.
            const string COUP_PATTERN_REGEX = "^[a-h][1-8]$";

            posDebut = posDebut.ToLower();
            posFin = posFin.ToLower();

            Regex reg = new Regex(COUP_PATTERN_REGEX);
            if (!reg.Match(posDebut).Success)
            {
                throw new ArgumentException("La position initiale est mal formattée `^[a-hA-H][1-8]$`.");
            }
            else if (!reg.Match(posFin).Success)
            {
                throw new ArgumentException("La position finale est mal formattée `^[a-hA-H][1-8]$`.");
            }
            else
            {
                int posIniCol => posDebut[0] switch
                {
                    "a" => 1, "b" => 2, "c" => 3, "d" => 4,
                    "e" => 5, "f" => 6, "g" => 7, "h" => 8
                }
                int posFinCol => posFin[0] switch
                {
                    "a" => 1, "b" => 2, "c" => 3, "d" => 4,
                    "e" => 5, "f" => 6, "g" => 7, "h" => 8
                }
                int posIniLign => posDebut[0] switch
                {
                    "1" => 1, "2" => 2, "3" => 3, "4" => 4,
                    "5" => 5, "6" => 6, "7" => 7, "8" => 8
                }
                int posFinLign => posFin[0] switch
                {
                    "1" => 1, "2" => 2, "3" => 3, "4" => 4,
                    "5" => 5, "6" => 6, "7" => 7, "8" => 8
                }
                this = new Coup((posIniCol, posIniLign), (posFinCol, posFinLign), estTourBlanc);
            }
        }


        // Propriétés
        public (int, int) posDebut
        {
            get { return _posDebut; }
            set { _posDebut = value; }
        }

        public (int, int) posFin
        {
            get { return _posFin; }
            set { _posFin = value; }
        }

        public bool estTourBlanc
        {
            get { return _estTourBlanc; }
            set { _estTourBlanc = value; }
        }


        // Méthodes

        // @brief Vérifie si le coup est diagonal (dx = dy != 0).
        // @return SiDiagonal.
        public bool EstDiagonal()
        {
            int deltaCol = Math.Abs(_posFin.Item1 - _posDebut.Item1);
            int deltaRow = Math.Abs(_posFin.Item2 - _posDebut.Item2);
            return deltaCol == deltaRow && deltaCol != 0;
        }

        // @brief Vérifie si le coup est orthogonal (dx = 0 ^^ dy = 0).
        // @return SiOrthogonal.
        public bool EstOrthogonal()
        {
            int deltaCol = Math.Abs(_posFin.Item1 - _posDebut.Item1);
            int deltaRow = Math.Abs(_posFin.Item2 - _posDebut.Item2);
            return (deltaCol == 0) != (deltaRow == 0);
        }

        // @brief Vérifie si le coup est en L (dx = 1 et dy = 2 OU dx = 2 et dy = 1).
        // @return SiEstEnL.
        public bool EstEnL()
        {
            int deltaCol = Math.Abs(_posFin.Item1 - _posDebut.Item1);
            int deltaRow = Math.Abs(_posFin.Item2 - _posDebut.Item2);
            return (deltaCol == 2 && deltaRow == 1) || (deltaCol == 1 && deltaRow == 2);
        }

        // @brief Permet d'avoir le nombre de cases traversées par le coup.
        // @return Nombre de cases traversées.
        // Remarque: La longueur d'un coup désigne le nombre de cases de déplacement,
        //           ce qui veut dire qu'un coup en L retourne 3 (1 + 2).
        public int Longueur()
        {
            int deltaCol = Math.Abs(_posFin.Item1 - _posDebut.Item1);
            int deltaRow = Math.Abs(_posFin.Item2 - _posDebut.Item2);
            if (EstEnL()) return 3;
            return Math.Max(deltaCol, deltaRow);
        }

        //destructeur
        ~Coup() { }
    }
}
