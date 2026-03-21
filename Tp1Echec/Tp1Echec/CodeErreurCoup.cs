using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public enum CodeErreurCoup : int
    {

        // Enum pour les codes d'erreur de validation de coup, à utiliser dans une version améliorée de ValiderCoup qui retourne des messages d'erreur spécifiques au lieu d'un booléen générique.
        
        OK = 0,
        MemeCase = 1,
        HorsPlateau = 2,
        AucunePiece = 3,
        MauvaisTour = 4,
        DestinationAlliee = 5,
        MouvementInvalide = 6,
        Collision = 7,
        MetEnEchec = 8,
        CoupPionInvalide = 9,
        RoqueInvalide = 10
    

    }
}
