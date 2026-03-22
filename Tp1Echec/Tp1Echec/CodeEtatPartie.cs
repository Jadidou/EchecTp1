using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @enum CodeEtatPartie
    // @brief Représente les différents états possibles d'une partie d'échecs.
    public enum CodeEtatPartie : int
    {
        // @brief La partie est en cours et aucune condition particulière n'est remplie
        OK = 0,

        // @brief Le roi est en échec
        Echec = 1,

        // @brief Le roi est en échec et mat → partie terminée
        EchecEtMat = 2,

        // @brief Pat (égalité sans possibilité de mouvement légal)
        Pat = 3,

        // @brief Nulle (égalité, ex: répétition de position)
        Nulle = 4
    }
}
