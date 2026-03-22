using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    // @enum CodeErreurCoup
    // @brief Représente les différents codes d'erreur pouvant survenir lors de la validation d'un coup aux échecs.
    //        À utiliser pour fournir des messages d'erreur spécifiques plutôt qu'un booléen générique.
    public enum CodeErreurCoup : int
    {
        // Enum pour les codes d'erreur de validation de coup, à utiliser dans une version améliorée de ValiderCoup qui retourne des messages d'erreur spécifiques au lieu d'un booléen générique.

        // @brief Le coup est valide
        OK = 0,

        // @brief Le joueur tente de déplacer une pièce sur sa propre case actuelle
        MemeCase = 1,

        // @brief Le coup tente de sortir du plateau
        HorsPlateau = 2,

        // @brief Il n'y a pas de pièce sur la case de départ
        AucunePiece = 3,

        // @brief La pièce sélectionnée n'appartient pas au joueur dont c'est le tour
        MauvaisTour = 4,

        // @brief La case de destination contient une pièce alliée
        DestinationAlliee = 5,

        // @brief Le mouvement de la pièce est géométriquement invalide
        MouvementInvalide = 6,

        // @brief Il y a une collision avec une autre pièce sur le chemin
        Collision = 7,

        // @brief Le coup met son propre roi en échec
        MetEnEchec = 8,

        // @brief Le pion effectue un mouvement invalide (ex: double avancée impossible)
        CoupPionInvalide = 9,

        // @brief Le roque demandé n'est pas valide
        RoqueInvalide = 10
    

    }
}
