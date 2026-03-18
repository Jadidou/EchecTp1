namespace Tp1Echec
{
    partial class VuePrincipale
    {

        /// <summary>
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtBienvenue = new System.Windows.Forms.Label();
            this.vuePlateau1 = new Tp1Echec.VuePlateau();
            this.vueJoueur1 = new Tp1Echec.VueJoueur();
            this.SuspendLayout();
            // 
            // txtBienvenue
            // 
            this.txtBienvenue.AutoSize = true;
            this.txtBienvenue.Location = new System.Drawing.Point(12, 22);
            this.txtBienvenue.Name = "txtBienvenue";
            this.txtBienvenue.Size = new System.Drawing.Size(152, 13);
            this.txtBienvenue.TabIndex = 1;
            this.txtBienvenue.Text = "Bienvenue a notre jeu d\'echec";
            // 
            // vuePlateau1
            // 
            this.vuePlateau1.Location = new System.Drawing.Point(215, 10);
            this.vuePlateau1.Margin = new System.Windows.Forms.Padding(1, 1, 1, 1);
            this.vuePlateau1.Name = "vuePlateau1";
            this.vuePlateau1.Size = new System.Drawing.Size(638, 553);
            this.vuePlateau1.TabIndex = 0;
            // 
            // vueJoueur1
            // 
            this.vueJoueur1.Location = new System.Drawing.Point(12, 56);
            this.vueJoueur1.Name = "vueJoueur1";
            this.vueJoueur1.Size = new System.Drawing.Size(199, 450);
            this.vueJoueur1.TabIndex = 2;
            // 
            // VuePrincipale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(865, 592);
            this.Controls.Add(this.vueJoueur1);
            this.Controls.Add(this.txtBienvenue);
            this.Controls.Add(this.vuePlateau1);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "VuePrincipale";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private void Form1_Load(object sender, System.EventArgs e)
        {

        }

        private VuePlateau vuePlateau1;
        private System.Windows.Forms.Label txtBienvenue;
        private VueJoueur vueJoueur1;
    }
}

