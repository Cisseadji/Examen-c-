namespace GestionSchoolApp
{
    partial class FormValidation
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
            this.btnvalider_otp = new System.Windows.Forms.Button();
            this.txtOTP = new System.Windows.Forms.TextBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this.btngenerer = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnvalider_otp
            // 
            this.btnvalider_otp.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnvalider_otp.Location = new System.Drawing.Point(305, 105);
            this.btnvalider_otp.Name = "btnvalider_otp";
            this.btnvalider_otp.Size = new System.Drawing.Size(153, 43);
            this.btnvalider_otp.TabIndex = 0;
            this.btnvalider_otp.Text = "Valider";
            this.btnvalider_otp.UseVisualStyleBackColor = true;
            this.btnvalider_otp.Click += new System.EventHandler(this.btnvalider_otp_Click);
            // 
            // txtOTP
            // 
            this.txtOTP.Location = new System.Drawing.Point(65, 105);
            this.txtOTP.Multiline = true;
            this.txtOTP.Name = "txtOTP";
            this.txtOTP.Size = new System.Drawing.Size(161, 43);
            this.txtOTP.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.HotTrack;
            this.panel1.Controls.Add(this.btngenerer);
            this.panel1.Controls.Add(this.btnvalider_otp);
            this.panel1.Controls.Add(this.txtOTP);
            this.panel1.Location = new System.Drawing.Point(164, 129);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(513, 215);
            this.panel1.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 16F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(222, 68);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(383, 37);
            this.label1.TabIndex = 3;
            this.label1.Text = "VALIDATION DU CODE";
            // 
            // btngenerer
            // 
            this.btngenerer.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btngenerer.Location = new System.Drawing.Point(123, 32);
            this.btngenerer.Name = "btngenerer";
            this.btngenerer.Size = new System.Drawing.Size(280, 43);
            this.btngenerer.TabIndex = 2;
            this.btngenerer.Text = "Générer le code";
            this.btngenerer.UseVisualStyleBackColor = true;
            this.btngenerer.Click += new System.EventHandler(this.btngenerer_Click);
            // 
            // FormValidation
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(9F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(874, 380);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.panel1);
            this.Name = "FormValidation";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "FormValidation";
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnvalider_otp;
        private System.Windows.Forms.TextBox txtOTP;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btngenerer;
    }
}