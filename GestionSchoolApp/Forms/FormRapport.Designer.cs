﻿namespace GestionSchoolApp.Forms
{
    partial class FormRapport
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
            this.listeEtudiantParClasse1 = new GestionSchoolApp.ListeEtudiantParClasse();
            this.SuspendLayout();
            // 
            // FormRapport
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1051, 450);
            this.Name = "FormRapport";
            this.Text = "FormRapport";
            this.ResumeLayout(false);

        }

        #endregion

        private ListeEtudiantParClasse listeEtudiantParClasse1;
    }
}