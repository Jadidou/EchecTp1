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

        private string m_name;

        //constructeur

        public Partie(string Name) 
        {

            m_name = Name;

        }


        //proprieter

        public string Name {
            get { return m_name; }
            set { m_name = value; }
        }

        //methode

        public string JoueurBlanc(string Name)
        {
            return m_name;
        }

        public string JoueurNoir(string Name)
        {
            return m_name;
        }


        //destructeur
        ~Partie() { }

    }
}
