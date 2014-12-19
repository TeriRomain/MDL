namespace MaisonDesLigues
{
    partial class ComposantVacation
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
            this.grpBoxVacationComposant = new System.Windows.Forms.GroupBox();
            this.DtTimePickJour = new System.Windows.Forms.DateTimePicker();
            this.DtTimePickHeureDbt = new System.Windows.Forms.DateTimePicker();
            this.DtTimePickHeureFin = new System.Windows.Forms.DateTimePicker();
            // 
            // grpBoxVacationComposant
            // 
            this.grpBoxVacationComposant.Location = new System.Drawing.Point(0, 0);
            this.grpBoxVacationComposant.Name = "grpBoxVacationComposant";
            this.grpBoxVacationComposant.Size = new System.Drawing.Size(200, 100);
            this.grpBoxVacationComposant.TabIndex = 0;
            this.grpBoxVacationComposant.TabStop = false;
            this.grpBoxVacationComposant.Text = "groupBox1";
            // 
            // DtTimePickJour
            // 
            this.DtTimePickJour.Location = new System.Drawing.Point(0, 0);
            this.DtTimePickJour.Name = "DtTimePickJour";
            this.DtTimePickJour.Size = new System.Drawing.Size(200, 20);
            this.DtTimePickJour.TabIndex = 0;
            // 
            // DtTimePickHeureDbt
            // 
            this.DtTimePickHeureDbt.Location = new System.Drawing.Point(0, 0);
            this.DtTimePickHeureDbt.Name = "DtTimePickHeureDbt";
            this.DtTimePickHeureDbt.Size = new System.Drawing.Size(200, 20);
            this.DtTimePickHeureDbt.TabIndex = 0;
            // 
            // DtTimePickHeureFin
            // 
            this.DtTimePickHeureFin.Location = new System.Drawing.Point(0, 0);
            this.DtTimePickHeureFin.Name = "DtTimePickHeureFin";
            this.DtTimePickHeureFin.Size = new System.Drawing.Size(200, 20);
            this.DtTimePickHeureFin.TabIndex = 0;

        }

        #endregion

        private System.Windows.Forms.GroupBox grpBoxVacationComposant;
        private System.Windows.Forms.DateTimePicker DtTimePickJour;
        private System.Windows.Forms.DateTimePicker DtTimePickHeureDbt;
        private System.Windows.Forms.DateTimePicker DtTimePickHeureFin;



    }
}
