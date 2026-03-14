using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class Partie
    {

        //attributs

        private Joueur _joueurBlanc;
        private Joueur _joueurNoir;
        private Stack<Plateau> _pilePlateau;
        private List<string> _pileEtatPlateau;

        //constructeur

        public Partie(Joueur joueurBlanc, Joueur joueurNoir) 
        {

            _joueurBlanc = joueurBlanc;
            _joueurBlanc = joueurNoir;
            _pilePlateau = new Stack<Plateau>();
            _pileEtatPlateau = new List<string>();

        }


        //proprieter

        public Joueur joueurBlanc
        {
            get { return _joueurBlanc; }
            set { _joueurBlanc = value; }
        }

        public Joueur joueurNoir
        {
            get { return _joueurNoir; }
            set { _joueurNoir = value; }
        }

        //indexeur

        public Plateau this[int index]
        {
            get
            {
                return _pilePlateau.ToArray()[index];
            }
        }

        public string this[string index]
        {
            get
            {
                int i = int.Parse(index);
                return _pileEtatPlateau[i];
            }
            set
            {
                int i = int.Parse(index);
                _pileEtatPlateau[i] = value;
            }
        }

        //methode

        public bool VerificationNulleParBoucle()
        {

            return true;

        }

        public void AjusterPointage()
        {

        }

        public int JouerCoup(string x, string y)
        {

            return 1;
        }

        public void DemarRerPartie()
        {


        }

        public void AbandonnerPartie()
        {


        }

        public void DemanderUneNulle()
        {


        }

        public string AfficherPlateau()
        {

            return "";
        }


        //destructeur
        ~Partie() { }

    }
}
