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
    // @class VuePrincipale
    // @brief Représente la fenêtre principale de l'application.
    //        Gère le démarrage des parties, la sélection des joueurs
    //        et la communication entre les différentes vues.
    public partial class VuePrincipale : Form
    {
        // Référence vers la vue du plateau d'échecs
        public VuePlateau _plateau;

        // @brief Constructeur de la vue principale
        // Initialise les composants et les liaisons entre les vues
        public VuePrincipale()
        {
            InitializeComponent();

            // Charge les joueurs dans les ComboBox au démarrage
            ChargerComboBoxJoueurs();

            // Associe la vue plateau existante (UserControl)
            _plateau = vuePlateau1;
            _plateau.VuePrincipale = this;

            // Met à jour la liste des joueurs et les ComboBox lorsqu’un score change
            _plateau.OnScoreChanged += vueJoueur1.ChargerListe;
            _plateau.OnScoreChanged += ChargerComboBoxJoueurs;

            // Met à jour les ComboBox lorsqu’un joueur est ajouté
            vueJoueur1.OnJoueurAjoute += ChargerComboBoxJoueurs;

        }

        // @brief Démarre une nouvelle partie entre deux joueurs sélectionnés
        // @param sender Objet déclencheur (bouton)
        // @param e Arguments de l’événement
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
                DesactiverBoutonsPartie();
                _plateau.ActiverBoutonsPlateau();
                MessageBox.Show("La partie a commencé !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        // @brief Désactive les boutons liés à la partie
        public void DesactiverBoutonsPartie()
        {
                btnDemarrerPartie.Enabled = false;
        }

        // @brief Active les boutons liés à la partie
        public void ActiverBoutonsPartie()
        {
                btnDemarrerPartie.Enabled = true;
        }

        // @brief Charge les joueurs dans les ComboBox (blanc et noir)
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

        // @brief Quitte le programme si aucune partie n’est en cours
        // @param sender Objet déclencheur
        // @param e Arguments de l’événement
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
