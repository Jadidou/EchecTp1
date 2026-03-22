using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @class AvecLimite
    // @brief Classe abstraite pour les pièces ayant une limite de déplacement
    //        (ex: Pion qui ne peut avancer que d'une ou deux cases selon le contexte).
    //        Hérite de AvecCollision, donc les pièces avec limite gèrent aussi la collision.
    public abstract class AvecLimite : AvecCollision
    {

        //constructeur

        // @brief Constructeur principal
        // @param pieceEstBlanche Indique si la pièce est blanche
        // @param pieceNaPasBouge Indique si la pièce n’a jamais bougé
        public AvecLimite(bool pieceEstBlanche, bool pieceNaPasBouge) : base(pieceEstBlanche, pieceNaPasBouge) { }

        //methode

        // @brief Valide le coup en combinant la géométrie et la limite de déplacement
        // @param coup Coup à valider
        // @return true si le coup respecte la géométrie et la limite, sinon false
        public override bool ValiderCoup(Coup coup)
        {
            return ValiderGeometrie(coup) && ValiderLimite(coup);
        }

        // @brief Méthode abstraite pour valider la géométrie spécifique de la pièce
        // @param coup Coup à valider
        // @return true si le mouvement respecte la géométrie propre à la pièce
        public abstract bool ValiderGeometrie(Coup coup);

        // @brief Méthode abstraite pour valider la limite de déplacement
        // @param coup Coup à valider
        // @return true si le mouvement respecte la limite de cases autorisées
        public abstract bool ValiderLimite(Coup coup);

        //destructeur
        // @brief Destructeur de la classe AvecLimite
        ~AvecLimite() { }

    }
}
