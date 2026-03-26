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
            this.btnDemarrerPartie = new System.Windows.Forms.Button();
            this.cbJoueurNoir = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.cbJoueurBlanc = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.vueJoueur1 = new Tp1Echec.VueJoueur();
            this.vuePlateau1 = new Tp1Echec.VuePlateau();
            this.btnQuitter = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtBienvenue
            // 
            this.txtBienvenue.AutoSize = true;
            this.txtBienvenue.Location = new System.Drawing.Point(63, 32);
            this.txtBienvenue.Name = "txtBienvenue";
            this.txtBienvenue.Size = new System.Drawing.Size(152, 13);
            this.txtBienvenue.TabIndex = 1;
            this.txtBienvenue.Text = "Bienvenue a notre jeu d\'echec";
            // 
            // btnDemarrerPartie
            // 
            this.btnDemarrerPartie.Location = new System.Drawing.Point(52, 185);
            this.btnDemarrerPartie.Margin = new System.Windows.Forms.Padding(2);
            this.btnDemarrerPartie.Name = "btnDemarrerPartie";
            this.btnDemarrerPartie.Size = new System.Drawing.Size(177, 38);
            this.btnDemarrerPartie.TabIndex = 3;
            this.btnDemarrerPartie.Text = "Démarrer la partie";
            this.btnDemarrerPartie.UseVisualStyleBackColor = true;
            this.btnDemarrerPartie.Click += new System.EventHandler(this.DemarrerPartie);
            // 
            // cbJoueurNoir
            // 
            this.cbJoueurNoir.FormattingEnabled = true;
            this.cbJoueurNoir.Location = new System.Drawing.Point(52, 163);
            this.cbJoueurNoir.Margin = new System.Windows.Forms.Padding(2);
            this.cbJoueurNoir.Name = "cbJoueurNoir";
            this.cbJoueurNoir.Size = new System.Drawing.Size(179, 21);
            this.cbJoueurNoir.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(50, 140);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(59, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "Joueur noir";
            // 
            // cbJoueurBlanc
            // 
            this.cbJoueurBlanc.FormattingEnabled = true;
            this.cbJoueurBlanc.Location = new System.Drawing.Point(52, 112);
            this.cbJoueurBlanc.Margin = new System.Windows.Forms.Padding(2);
            this.cbJoueurBlanc.Name = "cbJoueurBlanc";
            this.cbJoueurBlanc.Size = new System.Drawing.Size(179, 21);
            this.cbJoueurBlanc.TabIndex = 6;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(50, 91);
            this.label2.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(68, 13);
            this.label2.TabIndex = 7;
            this.label2.Text = "Joueur blanc";
            // 
            // vueJoueur1
            // 
            this.vueJoueur1.Location = new System.Drawing.Point(32, 256);
            this.vueJoueur1.Name = "vueJoueur1";
            this.vueJoueur1.Size = new System.Drawing.Size(213, 269);
            this.vueJoueur1.TabIndex = 2;
            // 
            // vuePlateau1
            // 
            this.vuePlateau1.Location = new System.Drawing.Point(215, 10);
            this.vuePlateau1.Margin = new System.Windows.Forms.Padding(1);
            this.vuePlateau1.Name = "vuePlateau1";
            this.vuePlateau1.Size = new System.Drawing.Size(795, 553);
            this.vuePlateau1.TabIndex = 0;
            // 
            // btnQuitter
            // 
            this.btnQuitter.Location = new System.Drawing.Point(54, 227);
            this.btnQuitter.Margin = new System.Windows.Forms.Padding(2);
            this.btnQuitter.Name = "btnQuitter";
            this.btnQuitter.Size = new System.Drawing.Size(177, 38);
            this.btnQuitter.TabIndex = 8;
            this.btnQuitter.Text = "Fermer le programe";
            this.btnQuitter.UseVisualStyleBackColor = true;
            this.btnQuitter.Click += new System.EventHandler(this.QuitterProgramme);
            // 
            // VuePrincipale
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1020, 592);
            this.Controls.Add(this.btnQuitter);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cbJoueurBlanc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.cbJoueurNoir);
            this.Controls.Add(this.btnDemarrerPartie);
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
        private System.Windows.Forms.Button btnDemarrerPartie;
        private System.Windows.Forms.ComboBox cbJoueurNoir;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox cbJoueurBlanc;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnQuitter;
    }
}

