using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class SansLimite
    // @brief Classe abstraite représentant les pièces pouvant se déplacer
    //        sur une distance illimitée (ex : Tour, Fou, Dame).
    //        Hérite de AvecCollision (gestion des obstacles sur le chemin).
    public abstract class SansLimite : AvecCollision
    {

        //constructeur

        // @brief Constructeur de la classe SansLimite
        // @param pieceEstBlanche Indique si la pièce est blanche
        // @param pieceNaPasBouge Indique si la pièce n’a jamais bougé
        public SansLimite(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        //methode

        // @brief Valide un coup pour une pièce sans limite de déplacement
        //        Ici, seule la géométrie est vérifiée (le chemin est géré ailleurs)
        // @param coup Coup à valider
        // @return true si la géométrie du coup est valide
        public override bool ValiderCoup(Coup coup)
        {
            return ValiderGeometrie(coup);
        }

        // @brief Valide la géométrie spécifique de la pièce
        //        (ex : diagonale pour fou, orthogonale pour tour)
        // @param coup Coup à valider
        // @return true si la géométrie est correcte
        public abstract bool ValiderGeometrie(Coup coup);

        //destructeur
        // @brief Destructeur de la classe SansLimite
        ~SansLimite() { }


    }
}
