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

        public event Action OnJoueurAjoute;

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

            string nom = txtBoxAjouterJoueur.Text.Trim();
  
            if (string.IsNullOrWhiteSpace(nom))
            {
                MessageBox.Show("Le nom ne peut pas être vide.");
                return;
            }

            if (nom.Contains(" "))
            {
                MessageBox.Show("Le nom ne doit pas contenir d'espace.");
                return;
            }

            if (!nom.All(char.IsLetter))
            {
                MessageBox.Show("Le nom doit contenir seulement des lettres.");
                return;
            }

            try
            {

                JeuEchec.AjouterJoueur(nom);

                ChargerListe();
                txtBoxAjouterJoueur.Clear();

                OnJoueurAjoute?.Invoke();

                MessageBox.Show("Joueur ajouté avec succès !");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
