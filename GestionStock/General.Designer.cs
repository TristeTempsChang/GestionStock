namespace GestionStock
{
    partial class General
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

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMarque = new System.Windows.Forms.TextBox();
            this.Marque = new System.Windows.Forms.Label();
            this.Nom = new System.Windows.Forms.Label();
            this.txtNom = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtMarque
            // 
            this.txtMarque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMarque.BackColor = System.Drawing.SystemColors.Window;
            this.txtMarque.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMarque.Location = new System.Drawing.Point(383, 58);
            this.txtMarque.Name = "txtMarque";
            this.txtMarque.Size = new System.Drawing.Size(305, 26);
            this.txtMarque.TabIndex = 24;
            // 
            // Marque
            // 
            this.Marque.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.Marque.AutoSize = true;
            this.Marque.Font = new System.Drawing.Font("Montserrat SemiBold", 14F, System.Drawing.FontStyle.Bold);
            this.Marque.Location = new System.Drawing.Point(378, 27);
            this.Marque.Name = "Marque";
            this.Marque.Size = new System.Drawing.Size(98, 26);
            this.Marque.TabIndex = 23;
            this.Marque.Text = "Marque :";
            // 
            // Nom
            // 
            this.Nom.AutoSize = true;
            this.Nom.Font = new System.Drawing.Font("Montserrat SemiBold", 14F, System.Drawing.FontStyle.Bold);
            this.Nom.Location = new System.Drawing.Point(19, 27);
            this.Nom.Name = "Nom";
            this.Nom.Size = new System.Drawing.Size(177, 26);
            this.Nom.TabIndex = 22;
            this.Nom.Text = "Nom du produit :";
            // 
            // txtNom
            // 
            this.txtNom.BackColor = System.Drawing.SystemColors.Window;
            this.txtNom.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNom.Location = new System.Drawing.Point(24, 58);
            this.txtNom.Name = "txtNom";
            this.txtNom.Size = new System.Drawing.Size(305, 26);
            this.txtNom.TabIndex = 21;
            // 
            // General
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.White;
            this.Controls.Add(this.txtMarque);
            this.Controls.Add(this.Marque);
            this.Controls.Add(this.Nom);
            this.Controls.Add(this.txtNom);
            this.Margin = new System.Windows.Forms.Padding(0);
            this.Name = "General";
            this.Size = new System.Drawing.Size(709, 365);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label Marque;
        private System.Windows.Forms.Label Nom;
        public System.Windows.Forms.TextBox txtMarque;
        public System.Windows.Forms.TextBox txtNom;
    }
}
