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
    public partial class VueJoueur : UserControl
    {
        public VueJoueur()
        {
            InitializeComponent();
            ChargerListe();
        }

        public void ChargerListe()
        {
            listBoxJoueurs.Items.Clear();

            JeuEchec.ChargerJoueurs();

            string liste = JeuEchec.ConsulterListeJoueur();

            foreach (string ligne in liste.Split('\n'))
            {
                if (!string.IsNullOrWhiteSpace(ligne))
                {
                    listBoxJoueurs.Items.Add(ligne);
                }
            }
        }

        public void VoirPointageJoueurs()
        {

            string liste = JeuEchec.ConsulterListeJoueur();
            MessageBox.Show("Pointages :\n" + liste);

        }

        public void AjouterJoueur(object sender, EventArgs e)
        {



        }
    }
}
