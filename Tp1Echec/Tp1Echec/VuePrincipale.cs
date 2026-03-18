using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tp1Echec
{
    public partial class VuePrincipale : Form
    {


        public VuePrincipale()
        {
            InitializeComponent();

            
            DemarrerNouvellePartie();

        }

        public void DemarrerNouvellePartie()
        {

            Program.DemarrerPartie();

            //string plateau = Program.AfficherPlateau();
            //MessageBox.Show(plateau);

        }

        public void QuitterProgramme()
        {

            Program.QuitterProgramme();

        }

    }
}
