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

        public VuePlateau _plateau;

        public VuePrincipale()
        {
            InitializeComponent();

            ChargerComboBoxJoueurs();

            _plateau = vuePlateau1;

            //DemarrerNouvellePartie();

        }

        public void DemarrerPartie(object sender, EventArgs e)
        {

            if (cbJoueurBlanc.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner le joueur blanc.");
                return;
            }

            if (cbJoueurNoir.SelectedItem == null)
            {
                MessageBox.Show("Veuillez sélectionner le joueur noir.");
                return;
            }

            string joueurBlanc = cbJoueurBlanc.SelectedItem.ToString();
            string joueurNoir = cbJoueurNoir.SelectedItem.ToString();

            if (joueurBlanc == joueurNoir)
            {
                MessageBox.Show("Les deux joueurs doivent être différents.");
                return;
            }

            try
            {
                JeuEchec.DemarrerPartie(joueurBlanc, joueurNoir);
                _plateau.DemarrerPartie();
                JeuEchec.AfficherPlateau();
                MessageBox.Show("La partie a commencé !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void ChargerComboBoxJoueurs()
        {
            cbJoueurBlanc.Items.Clear();
            cbJoueurNoir.Items.Clear();

            JeuEchec.ChargerJoueurs();

            string liste = JeuEchec.ConsulterListeJoueur();

            foreach (string ligne in liste.Split('\n'))
            {

                string propre = ligne.Trim();

                if (!string.IsNullOrWhiteSpace(ligne))
                {
                    cbJoueurBlanc.Items.Add(ligne);
                    cbJoueurNoir.Items.Add(ligne);
                }
            }
        }

        public void QuitterProgramme()
        {

            JeuEchec.QuitterProgramme();

        }

    }
}
