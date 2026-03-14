using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public class FacadeModele
    {

        //attribut

        private List<Joueur> _listeJoueur;
        private Partie _partie;

        //constructeur

        public FacadeModele() 
        {

            _listeJoueur = new List<Joueur>();
            _partie = partie;

        }


        //proprieter

        public Partie partie
        {

            get { return _partie; }
            set { _partie = value; }

        }

        //indexeur
        public Joueur this[int index]
        {
            get
            {
                return _listeJoueur[index];
            }
            set
            {
                _listeJoueur[index] = value;
            }
        }

        //methode

        public string ConsulterListeJoueur()
        {

            return "";
        }

        public int JouerCoup(string x, string y)
        {

            return 1;

        }

        public void DemarrerPartie()
        {


        }

        public void AbandonnerPartie()
        {


        }

        public void QuitterProgramme()
        {


        }

        public string AfficherPlateau()
        {

            return "";

        }

        public void DemanderUneNulle()
        {


        }

        //destructeur
        ~FacadeModele() { }

    }
}
