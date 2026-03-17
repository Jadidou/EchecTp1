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
    public partial class VuePlateau : Form
    {
        // état du drag
        private bool isDragging = false;
        private PictureBox dragSource = null;
        private Point dragOffset;
        private Point dragOriginalLocation;

        public VuePlateau()
        {
            InitializeComponent();
            MakeSquaresTransparent();
            AttachSquareEvents();
            PlaceWhiteKnight();
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

        // Attache les événements souris à chaque case (après MakeSquaresTransparent)
        private void AttachSquareEvents()
        {
            if (this.PlateauEchec == null) return;

            foreach (var pb in this.PlateauEchec.Controls.OfType<PictureBox>())
            {
                // ignore le plateau lui-même s'il était dans la liste
                if (pb == null) continue;
                // Attacher une fois seulement
                pb.MouseDown -= Square_MouseDown;
                pb.MouseMove -= Square_MouseMove;
                pb.MouseUp -= Square_MouseUp;

                pb.MouseDown += Square_MouseDown;
                pb.MouseMove += Square_MouseMove;
                pb.MouseUp += Square_MouseUp;
            }
        }

        private void Square_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            var pb = sender as PictureBox;
            if (pb == null) return;
            // ne démarrer que s'il y a une image (une pièce)
            if (pb.Image == null && (pb.Tag == null || string.IsNullOrEmpty(pb.Tag.ToString()))) return;

            isDragging = true;
            dragSource = pb;
            // Position du curseur relative à la PictureBox pour garder l'offset pendant le drag
            dragOffset = e.Location;
            dragOriginalLocation = pb.Location;
            pb.BringToFront();
            Cursor = Cursors.Hand;
        }

        private void Square_MouseMove(object sender, MouseEventArgs e)
        {
            if (!isDragging || dragSource == null) return;
            // Position du curseur en coords du plateau
            var plateau = this.PlateauEchec;
            var cursorPt = plateau.PointToClient(Control.MousePosition);

            // Calculer nouvelle position de la PictureBox en respectant l'offset initial
            var newLocation = new Point(cursorPt.X - dragOffset.X, cursorPt.Y - dragOffset.Y);

            // Optionnel : limiter afin que la PictureBox reste dans les limites du plateau
            newLocation.X = Math.Max(0, Math.Min(newLocation.X, plateau.Width - dragSource.Width));
            newLocation.Y = Math.Max(0, Math.Min(newLocation.Y, plateau.Height - dragSource.Height));

            dragSource.Location = newLocation;
        }

        private void Square_MouseUp(object sender, MouseEventArgs e)
        {
            if (!isDragging || dragSource == null) return;

            var plateau = this.PlateauEchec;
            var cursorPt = plateau.PointToClient(Control.MousePosition);

            // Trouver la case cible sous le curseur
            var target = plateau.GetChildAtPoint(cursorPt) as PictureBox;

            // Si la cible est la même que la source (la PictureBox déplacée recouvre la case),
            // masquer temporairement la source pour récupérer la case en dessous.
            if (target == dragSource)
            {
                dragSource.Visible = false;
                try
                {
                    target = plateau.GetChildAtPoint(cursorPt) as PictureBox;
                }
                finally
                {
                    dragSource.Visible = true;
                }
            }

            // Si GetChildAtPoint retourne null (par ex. sur les bords), fallback sur le centre du dragSource
            if (target == null)
            {
                var center = new Point(dragSource.Location.X + dragSource.Width / 2,
                                       dragSource.Location.Y + dragSource.Height / 2);
                // si centre retourne la source, masquer temporairement aussi
                var centerTarget = plateau.GetChildAtPoint(center) as PictureBox;
                if (centerTarget == dragSource)
                {
                    dragSource.Visible = false;
                    try
                    {
                        centerTarget = plateau.GetChildAtPoint(center) as PictureBox;
                    }
                    finally
                    {
                        dragSource.Visible = true;
                    }
                }
                target = centerTarget;
            }

            // Déterminer noms source / destination
            string srcName = dragSource?.Name;
            string dstName = target?.Name;

            bool moved = false;
            if (!string.IsNullOrEmpty(srcName) && !string.IsNullOrEmpty(dstName) && srcName != dstName)
            {
                // Appel au contrôleur pour valider et appliquer la logique métier
                try
                {
                    int result = Program.JouerCoup(srcName, dstName);
                    if (result == 1)
                    {
                        // Le contrôleur a validé : mise à jour visuelle :
                        target.Image = dragSource.Image;
                        target.Tag = dragSource.Tag;
                        dragSource.Image = null;
                        dragSource.Tag = null;
                        target.BringToFront();
                        moved = true;
                    }
                    else
                    {
                        // coup refusé : revenir en arrière
                        moved = false;
                    }
                }
                catch
                {
                    // Si exception controller, considérer comme refusé.
                    moved = false;
                }
            }

            if (!moved)
            {
                // Repositionner la case source à sa position d'origine
                dragSource.Location = dragOriginalLocation;
            }
            else
            {
                // Les PictureBox représentent des cases fixes : laisser la source à sa position d'origine.
                dragSource.Location = dragOriginalLocation;
            }

            // Reset état drag
            isDragging = false;
            dragSource = null;
            Cursor = Cursors.Default;
        }

        // place un cavalier blanc de test (image depuis resources) sur B1 et tague "WKnight"
        private void PlaceWhiteKnight()
        {
            if (this.PlateauEchec == null) return;
            var pb = this.PlateauEchec.Controls.Find("B1", true).FirstOrDefault() as PictureBox;
            if (pb == null) return;

            // Utilise l'image existante dans les ressources : Properties.Resources.cavalierBlanc
            pb.Image = Properties.Resources.cavalierBlanc;
            pb.Tag = "WKnight";
            pb.BringToFront();
        }

        public void AbandonnerPartie()
        {


        }

        public void DemanderNulle()
        {


        }

        public void QuitterProgramme()
        {


        }

        public void MovePieceOnView(string from, string to)
        {
            if (this.PlateauEchec == null) return;
            var src = this.PlateauEchec.Controls.Find(from, true).FirstOrDefault() as PictureBox;
            var dst = this.PlateauEchec.Controls.Find(to, true).FirstOrDefault() as PictureBox;
            if (src == null || dst == null) return;

            dst.Image = src.Image;
            dst.Tag = src.Tag;
            src.Image = null;
            src.Tag = null;
            dst.BringToFront();
        }

        public void SetPieceOnView(string square, Image image, string tag)
        {
            if (this.PlateauEchec == null) return;
            var pb = this.PlateauEchec.Controls.Find(square, true).FirstOrDefault() as PictureBox;
            if (pb == null) return;

            pb.Image = image;
            pb.Tag = tag;
            pb.BringToFront();
        }

    }
}
