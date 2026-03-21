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
    public partial class VuePlateau : UserControl
    {
        PictureBox caseSelectionnee = null;
        public bool partieEnCours { get; private set; } = false;

        public VuePlateau()
        {
            InitializeComponent();
            MakeSquaresTransparent();
            AttachEvents();
        }

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

                pb.Parent = PlateauEchec;
                pb.Location = relativeLocation;
                pb.BackColor = Color.Transparent;
                pb.BringToFront();
            }
        }

        // Attacher UN SEUL event : Click
        private void AttachEvents()
        {
            foreach (var pb in PlateauEchec.Controls.OfType<PictureBox>())
            {
                pb.Click -= Case_Click;
                pb.Click += Case_Click;
            }
        }

        // LOGIQUE CLICK CLICK
        private void Case_Click(object sender, EventArgs e)
        {
            if (!partieEnCours) return;

            PictureBox pb = sender as PictureBox;
            if (pb == null) return;

            // 🔹 1er clic → sélectionner
            if (caseSelectionnee == null)
            {
                if (pb.Image == null) return; // pas de pièce

                caseSelectionnee = pb;
                pb.BorderStyle = BorderStyle.Fixed3D; // visuel sélection
                return;
            }

            //2e clic → destination
            string src = caseSelectionnee.Name;
            string dst = pb.Name;

            bool moved = false;

            try
            {
                int result = JeuEchec.JouerCoup(src, dst);

                if (result == 1)
                {
                    // déplacement visuel
                    pb.Image = caseSelectionnee.Image;
                    pb.Tag = caseSelectionnee.Tag;

                    caseSelectionnee.Image = null;
                    caseSelectionnee.Tag = null;

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

        public void DemarrerPartie()
        {
            //Program.DemarrerPartie();
            //MessageBox.Show("La partie a commencé.");
            partieEnCours = true;
            InitialiserPieces();
        }

        public void RefreshPlateau()
        {
            // Cette méthode peut être appelée par le contrôleur pour forcer une mise à jour visuelle du plateau
            // en cas de changements externes.
            //if (!partieEnCours) return;
            //InitialiserPieces();
        }

        public event Action OnScoreChanged;

        public void AbandonnerPartie(object sender, EventArgs e)
        {

            JeuEchec.AbandonnerPartie();
            MessageBox.Show("La partie a été abandonnée.");
            OnScoreChanged?.Invoke();
            partieEnCours = false;

        }

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

            MessageBox.Show("La partie est déclarée nulle !");
            partieEnCours = false;

        }

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
