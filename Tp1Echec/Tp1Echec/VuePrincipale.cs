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
            _plateau.OnScoreChanged += vueJoueur1.ChargerListe;
            _plateau.OnScoreChanged += ChargerComboBoxJoueurs;
            _plateau.OnScoreChanged += DesactiverBoutonsPartie;
            _plateau.OnScoreChanged += _plateau.DesactiverBoutonsPlateau;

            vueJoueur1.OnJoueurAjoute += ChargerComboBoxJoueurs;

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
                btnDemarrerPartie.Enabled = false;
                _plateau.ActiverBoutonsPlateau();
                MessageBox.Show("La partie a commencé !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        public void DesactiverBoutonsPartie()
        {
                btnDemarrerPartie.Enabled = true;
        }

        public void ChargerComboBoxJoueurs()
        {
            cbJoueurBlanc.Items.Clear();
            cbJoueurNoir.Items.Clear();
            cbJoueurBlanc.SelectedIndex = -1;
            cbJoueurNoir.SelectedIndex = -1;
            cbJoueurBlanc.Text = String.Empty;
            cbJoueurNoir.Text = String.Empty;

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

        public void QuitterProgramme(object sender, EventArgs e)
        {
            // Vérifier si une partie est en cours
            if (_plateau.partieEnCours)
            {
                MessageBox.Show("Impossible de quitter : une partie est en cours !");
                return;
            }

            // Sinon, quitter normalement
            JeuEchec.QuitterProgramme();

        }

    }
}
