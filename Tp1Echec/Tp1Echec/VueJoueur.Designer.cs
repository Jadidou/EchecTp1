namespace Tp1Echec
{
    partial class VueJoueur
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtBoxAjouterJoueur = new System.Windows.Forms.TextBox();
            this.btnAjouterJoueur = new System.Windows.Forms.Button();
            this.listBoxJoueurs = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // txtBoxAjouterJoueur
            // 
            this.txtBoxAjouterJoueur.Location = new System.Drawing.Point(24, 258);
            this.txtBoxAjouterJoueur.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtBoxAjouterJoueur.Name = "txtBoxAjouterJoueur";
            this.txtBoxAjouterJoueur.Size = new System.Drawing.Size(268, 26);
            this.txtBoxAjouterJoueur.TabIndex = 0;
            // 
            // btnAjouterJoueur
            // 
            this.btnAjouterJoueur.Location = new System.Drawing.Point(24, 322);
            this.btnAjouterJoueur.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnAjouterJoueur.Name = "btnAjouterJoueur";
            this.btnAjouterJoueur.Size = new System.Drawing.Size(270, 63);
            this.btnAjouterJoueur.TabIndex = 1;
            this.btnAjouterJoueur.Text = "Ajouter un joueur";
            this.btnAjouterJoueur.UseVisualStyleBackColor = true;
            this.btnAjouterJoueur.Click += new System.EventHandler(this.AjouterJoueur);
            // 
            // listBoxJoueurs
            // 
            this.listBoxJoueurs.FormattingEnabled = true;
            this.listBoxJoueurs.ItemHeight = 20;
            this.listBoxJoueurs.Location = new System.Drawing.Point(24, 24);
            this.listBoxJoueurs.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.listBoxJoueurs.Name = "listBoxJoueurs";
            this.listBoxJoueurs.Size = new System.Drawing.Size(268, 204);
            this.listBoxJoueurs.TabIndex = 2;
            // 
            // VueJoueur
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.listBoxJoueurs);
            this.Controls.Add(this.btnAjouterJoueur);
            this.Controls.Add(this.txtBoxAjouterJoueur);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Name = "VueJoueur";
            this.Size = new System.Drawing.Size(320, 414);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtBoxAjouterJoueur;
        private System.Windows.Forms.Button btnAjouterJoueur;
        private System.Windows.Forms.ListBox listBoxJoueurs;
    }
}