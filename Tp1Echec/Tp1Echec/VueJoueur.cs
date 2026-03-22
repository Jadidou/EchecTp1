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
    // @class VueJoueur
    // @brief Représente la vue permettant de gérer les joueurs (ajout, affichage, pointage).
    public partial class VueJoueur : UserControl
    {
        // Événement déclenché lorsqu’un joueur est ajouté 
        public event Action OnJoueurAjoute;


        // @brief Constructeur de la vue joueur
        // Initialise les composants et charge la liste des joueurs
        public VueJoueur()
        {
            InitializeComponent();
            ChargerListe();
        }

        // @brief Charge et affiche la liste des joueurs dans la ListBox
        public void ChargerListe()
        {
            // Vide la liste actuelle pour éviter les doublons
            listBoxJoueurs.Items.Clear();

            // Demande à la logique de charger les joueurs (fichier, mémoire, etc.)
            JeuEchec.ChargerJoueurs();

            // Récupère la liste des joueurs sous forme de chaîne
            string liste = JeuEchec.ConsulterListeJoueur();

            // Sépare la liste ligne par ligne
            foreach (string ligne in liste.Split('\n'))
            {
                // Vérifie que la ligne n’est pas vide
                if (!string.IsNullOrWhiteSpace(ligne))
                {
                    // Ajoute le joueur à la ListBox
                    listBoxJoueurs.Items.Add(ligne);
                }
            }
        }

        // @brief Affiche le pointage de tous les joueurs dans une boîte de dialogue
        public void VoirPointageJoueurs()
        {

            string liste = JeuEchec.ConsulterListeJoueur();
            MessageBox.Show("Pointages :\n" + liste);

        }

        // @brief Ajoute un nouveau joueur à partir du champ texte
        // @param sender Objet déclencheur (bouton)
        // @param e Arguments de l’événement
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
