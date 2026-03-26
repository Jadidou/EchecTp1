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
    // @class VuePlateau
    // @brief Représente la vue graphique du plateau d'échecs (UserControl).
    //        Gère l'affichage des pièces, les interactions utilisateur (clics)
    public partial class VuePlateau : UserControl
    {
        //attributs

        PictureBox caseSelectionnee = null; //< Case actuellement sélectionnée par l’utilisateur.
        VuePrincipale _principale; //< Référence vers la vue principale (menu, score, boutons).

        public VuePrincipale VuePrincipale { get { return _principale;  } set { _principale = value; } }


        //proprietés
        public bool partieEnCours { get; private set; } = false; //< Indique si une partie est en cours.

        //constructeur

        // @brief Initialise la vue du plateau.
        // @param principale Référence vers la vue principale.
        public VuePlateau(VuePrincipale principale) : this()
        {
            _principale = principale;
        }

        public VuePlateau()
        {
            InitializeComponent();
            MakeSquaresTransparent();
            AttachEvents();
            DesactiverBoutonsPlateau();
        }

        //methodes

        // @brief Rend les cases transparentes pour afficher le plateau en arrière-plan.
        private void MakeSquaresTransparent()
        {
            if (this.PlateauEchec == null) return;

            // Rendre transparent chaque PictureBox de case en la rattachant
            // comme enfant du PictureBox 'PlateauEchec' pour que la transparence
            // montre l'image du plateau.
            var squares = this.Controls.OfType<PictureBox>()
                                      .Where(pb => pb != PlateauEchec)
                                      .ToList();

            foreach (var pb in squares)
            {
                // Calculer la position relative au PlateauEchec avant de changer le Parent
                var relativeLocation = new Point(pb.Location.X - PlateauEchec.Location.X,
                                                 pb.Location.Y - PlateauEchec.Location.Y);
                
                // On rattache la case au plateau pour permettre la transparence
                pb.Parent = PlateauEchec;
                pb.Location = relativeLocation;
                pb.BackColor = Color.Transparent;
                pb.BringToFront();
            }
        }

        // @brief Attache l'événement Click à toutes les cases du plateau.
        private void AttachEvents()
        {
            foreach (var pb in PlateauEchec.Controls.OfType<PictureBox>())
            {
                pb.Click -= Case_Click;
                pb.Click += Case_Click;
            }
        }

        // @brief Gère les clics utilisateur sur les cases du plateau.
        //        Permet la sélection d'une pièce et son déplacement.
        private void Case_Click(object sender, EventArgs e)
        {
            if (!partieEnCours) return;

            PictureBox pb = sender as PictureBox;
            if (pb == null) return;

            // 1er clic → sélectionner
            if (caseSelectionnee == null)
            {
                if (pb.Image == null) return; // pas de pièce

                caseSelectionnee = pb;
                pb.BorderStyle = BorderStyle.Fixed3D; // visuel sélection
                return;
            }

            // 2e clic sur la même case → désélectionner
            if (pb == caseSelectionnee)
            {
                caseSelectionnee.BorderStyle = BorderStyle.None;
                caseSelectionnee = null;
                return;
            }

            //2e clic → destination
            string src = caseSelectionnee.Name;
            string dst = pb.Name;

            bool moved = false;

            try
            {
                int result = JeuEchec.JouerCoup(src, dst);

                if (result <= 0)
                {
                    string message = "";

                    switch (-result)
                    {
                        case 1:
                            message = "Tu dois déplacer la pièce.";
                            break;
                        case 3:
                            message = "Aucune pièce sélectionnée.";
                            break;
                        case 4:
                            message = "Ce n'est pas ton tour.";
                            break;
                        case 5:
                            message = "Impossible de manger ta propre pièce.";
                            break;
                        case 7:
                            message = "Une pièce bloque le chemin.";
                            break;
                        case 8:
                            message = "Ce coup te met en échec.";
                            break;
                        default:
                            message = "Coup invalide.";
                            break;
                    }

                    MessageBox.Show(message);

                    // RESET OBLIGATOIRE
                    caseSelectionnee.BorderStyle = BorderStyle.None;
                    caseSelectionnee = null;

                    return;
                }

                const int ETAT_OK = 0;
                const int ETAT_ECHEC = 1;
                const int ETAT_ECHEC_MAT = 2;
                const int ETAT_PAT = 3;
                const int ETAT_NULLE = 4;

                int etat = JeuEchec.VerifierEtatPartie();

                if (etat != ETAT_OK)
                {
                    switch (etat)
                    {
                        case ETAT_ECHEC:
                            MessageBox.Show("Échec !");
                            break;

                        case ETAT_ECHEC_MAT:
                            MessageBox.Show("Échec et mat !");
                            partieEnCours = false;
                            _principale.ActiverBoutonsPartie();
                            DesactiverBoutonsPlateau();
                            _principale.ActiverBoutonsPartie();
                            OnScoreChanged?.Invoke();
                            break;

                        case ETAT_PAT:
                            MessageBox.Show("Pat !");
                            partieEnCours = false;
                            _principale.ActiverBoutonsPartie();
                            DesactiverBoutonsPlateau();
                            _principale.ActiverBoutonsPartie();
                            OnScoreChanged?.Invoke();
                            break;

                        case ETAT_NULLE:
                            MessageBox.Show("Partie nulle (répétition) !");
                            partieEnCours = false;
                            _principale.ActiverBoutonsPartie();
                            DesactiverBoutonsPlateau();
                            _principale.ActiverBoutonsPartie();
                            OnScoreChanged?.Invoke();
                            break;
                    }
                }

                if (result == 2) // Promotion requise : demander la pièce au joueur
                {
                    string codePiece = ChoisirPiecePromotion();
                    int resultPromotion = JeuEchec.PromouvoirPion(codePiece);

                    if (resultPromotion == 1)
                    {
                        int etatPromotion = JeuEchec.VerifierEtatPartie();

                        if (etatPromotion != ETAT_OK)
                        {
                            switch (etatPromotion)
                            {
                                case ETAT_ECHEC:
                                    MessageBox.Show("Échec !");
                                    break;

                                case ETAT_ECHEC_MAT:
                                    MessageBox.Show("Échec et mat !");
                                    partieEnCours = false;
                                    OnScoreChanged?.Invoke();
                                    break;

                                case ETAT_PAT:
                                    MessageBox.Show("Pat !");
                                    partieEnCours = false;
                                    OnScoreChanged?.Invoke();
                                    break;

                                case ETAT_NULLE:
                                    MessageBox.Show("Partie nulle (répétition) !");
                                    partieEnCours = false;
                                    OnScoreChanged?.Invoke();
                                    break;
                            }
                        }
                        // Rafraîchir le plateau après promotion
                        RefreshFromState(JeuEchec.AfficherPlateau());
                        moved = true;
                    }
                }
                //coup joué avec succès, rafraîchir le plateau
                if (result == 1)
                {
                    RefreshFromState(JeuEchec.AfficherPlateau());
                    moved = true;
                }
            }
            catch
            {
                moved = false;
            }

            // reset visuel
            caseSelectionnee.BorderStyle = BorderStyle.None;
            caseSelectionnee = null;
        }

        // Affiche un popup de sélection de pièce pour la promotion et retourne le code choisi.
        // Codes : "Q" = Dame, "R" = Tour, "N" = Cavalier, "B" = Fou.
        // @brief Affiche une popup pour choisir la pièce de promotion.
        // @return Code de la pièce choisie (Q, R, N, B).
        private string ChoisirPiecePromotion()
        {
            string choix = "Q"; // défaut : Dame

            using (Form popup = new Form())
            {
                popup.Text = "Promotion du pion";
                popup.Size = new System.Drawing.Size(320, 100);
                popup.FormBorderStyle = FormBorderStyle.FixedDialog;
                popup.StartPosition = FormStartPosition.CenterParent;
                popup.MaximizeBox = false;
                popup.MinimizeBox = false;

                var boutons = new[]
                {
                    new Button { Text = "Dame",     Tag = "Q", Left = 10,  Top = 20, Width = 70 },
                    new Button { Text = "Tour",     Tag = "R", Left = 90,  Top = 20, Width = 70 },
                    new Button { Text = "Cavalier", Tag = "N", Left = 170, Top = 20, Width = 70 },
                    new Button { Text = "Fou",      Tag = "B", Left = 250, Top = 20, Width = 50 },
                };

                foreach (var b in boutons)
                {
                    b.Click += (s, _) =>
                    {
                        choix = (string)((Button)s).Tag;
                        popup.Close();
                    };
                    popup.Controls.Add(b);
                }

                popup.ShowDialog(this);
            }

            return choix;
        }

        // @brief Initialise les pièces sur le plateau au début de la partie.
        private void InitialiserPieces()
        {
            if (this.PlateauEchec == null) return;

            foreach (PictureBox pb in this.PlateauEchec.Controls.OfType<PictureBox>())
            {
                if (pb == null) continue;

                string caseName = pb.Name; // ex: "A1"
                string piece = "";

                //GROS SWITCH
                switch (caseName)
                {
                    // ===== PIÈCES NOIRES =====
                    case "A8": piece = "BR"; break;
                    case "B8": piece = "BN"; break;
                    case "C8": piece = "BB"; break;
                    case "D8": piece = "BQ"; break;
                    case "E8": piece = "BK"; break;
                    case "F8": piece = "BB"; break;
                    case "G8": piece = "BN"; break;
                    case "H8": piece = "BR"; break;

                    case "A7":
                    case "B7":
                    case "C7":
                    case "D7":
                    case "E7":
                    case "F7":
                    case "G7":
                    case "H7":
                        piece = "BP";
                        break;

                    // ===== PIÈCES BLANCHES =====
                    case "A1": piece = "WR"; break;
                    case "B1": piece = "WN"; break;
                    case "C1": piece = "WB"; break;
                    case "D1": piece = "WQ"; break;
                    case "E1": piece = "WK"; break;
                    case "F1": piece = "WB"; break;
                    case "G1": piece = "WN"; break;
                    case "H1": piece = "WR"; break;

                    case "A2":
                    case "B2":
                    case "C2":
                    case "D2":
                    case "E2":
                    case "F2":
                    case "G2":
                    case "H2":
                        piece = "WP";
                        break;

                    default:
                        piece = null;
                        break;
                }

                //Placement visuel
                if (piece != null)
                {
                    pb.Tag = piece;

                    switch (piece)
                    {
                        // Blanc
                        case "WP": pb.Image = Properties.Resources.pionBlanc; break;
                        case "WR": pb.Image = Properties.Resources.tourBlanc; break;
                        case "WN": pb.Image = Properties.Resources.cavalierBlanc; break;
                        case "WB": pb.Image = Properties.Resources.fouBlanc; break;
                        case "WQ": pb.Image = Properties.Resources.reineBlanc; break;
                        case "WK": pb.Image = Properties.Resources.roiBlanc; break;

                        // Noir
                        case "BP": pb.Image = Properties.Resources.pionNoir; break;
                        case "BR": pb.Image = Properties.Resources.tourNoir; break;
                        case "BN": pb.Image = Properties.Resources.cavalierNoir; break;
                        case "BB": pb.Image = Properties.Resources.fouNoir; break;
                        case "BQ": pb.Image = Properties.Resources.reineNoir; break;
                        case "BK": pb.Image = Properties.Resources.roiNoir; break;
                    }

                    pb.SizeMode = PictureBoxSizeMode.StretchImage;
                    pb.BringToFront();
                    pb.Refresh();
                }
                else
                {
                    // case vide
                    pb.Image = null;
                    pb.Tag = null;
                }

                pb.BorderStyle = BorderStyle.None;
               // MessageBox.Show(pb.Name);
            }

        }

        // @brief Démarre une nouvelle partie.
        public void DemarrerPartie()
        {
            //Program.DemarrerPartie();
            //MessageBox.Show("La partie a commencé.");
            partieEnCours = true;
            InitialiserPieces();
        }


        public event Action OnScoreChanged; //< Événement pour mise à jour du score

        // @brief Permet d’abandonner la partie.
        public void AbandonnerPartie(object sender, EventArgs e)
        {

            JeuEchec.AbandonnerPartie();
            MessageBox.Show("La partie a été abandonnée.");
            DesactiverBoutonsPlateau();
            _principale.ActiverBoutonsPartie();
            OnScoreChanged?.Invoke();
            partieEnCours = false;

        }

        // @brief Permet de demander une partie nulle.
        public void DemanderNulle(object sender, EventArgs e)
        {

            DialogResult blanc = MessageBox.Show(
                "Joueur blanc accepte la nulle ?",
                "Demande de nulle",
                MessageBoxButtons.YesNo
            );

            if (blanc == DialogResult.No)
            {
                MessageBox.Show("Nulle refusée par le joueur blanc.");
                return;
            }

            DialogResult noir = MessageBox.Show(
                "Joueur noir accepte la nulle ?",
                "Demande de nulle",
                MessageBoxButtons.YesNo
            );

            if (noir == DialogResult.No)
            {
                MessageBox.Show("Nulle refusée par le joueur noir.");
                return;
            }

            // Les deux acceptent → on applique
            JeuEchec.DemanderUneNulle();
            DesactiverBoutonsPlateau();
            _principale.ActiverBoutonsPartie();

            MessageBox.Show("La partie est déclarée nulle !");
            partieEnCours = false;

        }

        // Rafraîchit l'affichage complet du plateau à partir de la sérialisation retournée par AfficherPlateau().
        // Format : 8 lignes séparées par \n, chaque ligne = 8 codes séparés par virgule (ex: "WR,_,_,...").
        // Rangée 0 = rangée 1 (bas du plateau), colonne 0 = colonne A.
        // @brief Rafraîchit l'affichage du plateau à partir de l'état du modèle.
        private void RefreshFromState(string plateauSerialise)
        {
            if (plateauSerialise == null) return;

            string[] rangees = plateauSerialise.Split(new[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
            if (rangees.Length < 8) return;

            char[] colonnes = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H' };

            for (int rangee = 0; rangee < 8; rangee++)
            {
                string[] pieces = rangees[rangee].Split(',');
                if (pieces.Length < 8) continue;

                for (int col = 0; col < 8; col++)
                {
                    string nomCase = "" + colonnes[col] + (char)('1' + rangee);
                    string piece = pieces[col].Trim();

                    var pb = PlateauEchec.Controls.Find(nomCase, true).FirstOrDefault() as PictureBox;
                    if (pb == null) continue;

                    if (piece == "_")
                    {
                        pb.Image = null;
                        pb.Tag = null;
                    }
                    else
                    {
                        pb.Tag = piece;
                        pb.SizeMode = PictureBoxSizeMode.StretchImage;
                        switch (piece)
                        {
                            case "WP": pb.Image = Properties.Resources.pionBlanc; break;
                            case "WR": pb.Image = Properties.Resources.tourBlanc; break;
                            case "WN": pb.Image = Properties.Resources.cavalierBlanc; break;
                            case "WB": pb.Image = Properties.Resources.fouBlanc; break;
                            case "WQ": pb.Image = Properties.Resources.reineBlanc; break;
                            case "WK": pb.Image = Properties.Resources.roiBlanc; break;
                            case "BP": pb.Image = Properties.Resources.pionNoir; break;
                            case "BR": pb.Image = Properties.Resources.tourNoir; break;
                            case "BN": pb.Image = Properties.Resources.cavalierNoir; break;
                            case "BB": pb.Image = Properties.Resources.fouNoir; break;
                            case "BQ": pb.Image = Properties.Resources.reineNoir; break;
                            case "BK": pb.Image = Properties.Resources.roiNoir; break;
                        }
                    }
                }
            }
        }

        // @brief Active les boutons du plateau.
        public void ActiverBoutonsPlateau()
        {
            btnAbandonner.Enabled = true;
            btnDemanderNull.Enabled = true;
        }

        // @brief Désactive les boutons du plateau.
        public void DesactiverBoutonsPlateau()
        {
            btnAbandonner.Enabled = false;
            btnDemanderNull.Enabled = false;
        }

        // @brief Déplace une pièce visuellement (sans logique métier).
        public void MovePieceOnView(string from, string to)
        {
            var src = PlateauEchec.Controls.Find(from, true).FirstOrDefault() as PictureBox;
            var dst = PlateauEchec.Controls.Find(to, true).FirstOrDefault() as PictureBox;

            if (src == null || dst == null) return;

            dst.Image = src.Image;
            dst.Tag = src.Tag;

            src.Image = null;
            src.Tag = null;
        }

    }
}
