using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tp1Echec
{
    public enum CodeEtatPartie : int
    {
        OK = 0,

        Echec = 1,
        EchecEtMat = 2,
        Pat = 3,
        Nulle = 4
    }
}
