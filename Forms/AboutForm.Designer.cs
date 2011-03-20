namespace AwkEverywhere.Forms
{
    partial class AboutForm
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
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutForm));
        	this.BtnCloseAbout = new System.Windows.Forms.Button();
        	this.LnkFamfamfam = new System.Windows.Forms.LinkLabel();
        	this.label1 = new System.Windows.Forms.Label();
        	this.label2 = new System.Windows.Forms.Label();
        	this.LnkUnxutils = new System.Windows.Forms.LinkLabel();
        	this.label3 = new System.Windows.Forms.Label();
        	this.label4 = new System.Windows.Forms.Label();
        	this.LnkArtiche = new System.Windows.Forms.LinkLabel();
        	this.SuspendLayout();
        	// 
        	// BtnCloseAbout
        	// 
        	this.BtnCloseAbout.Location = new System.Drawing.Point(12, 140);
        	this.BtnCloseAbout.Name = "BtnCloseAbout";
        	this.BtnCloseAbout.Size = new System.Drawing.Size(374, 23);
        	this.BtnCloseAbout.TabIndex = 0;
        	this.BtnCloseAbout.Text = "Ok";
        	this.BtnCloseAbout.UseVisualStyleBackColor = true;
        	this.BtnCloseAbout.Click += new System.EventHandler(this.BtnCloseAbout_Click);
        	// 
        	// LnkFamfamfam
        	// 
        	this.LnkFamfamfam.AutoSize = true;
        	this.LnkFamfamfam.Location = new System.Drawing.Point(192, 112);
        	this.LnkFamfamfam.Name = "LnkFamfamfam";
        	this.LnkFamfamfam.Size = new System.Drawing.Size(139, 13);
        	this.LnkFamfamfam.TabIndex = 1;
        	this.LnkFamfamfam.TabStop = true;
        	this.LnkFamfamfam.Text = "http://www.famfamfam.com";
        	this.LnkFamfamfam.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lnk_LinkClicked);
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(7, 112);
        	this.label1.Margin = new System.Windows.Forms.Padding(3, 0, 0, 0);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(179, 13);
        	this.label1.TabIndex = 2;
        	this.label1.Text = "Thanks to Famfamfam for the icons :";
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.Location = new System.Drawing.Point(7, 85);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(178, 13);
        	this.label2.TabIndex = 3;
        	this.label2.Text = "You can find gawk for Win32 here : ";
        	// 
        	// LnkUnxutils
        	// 
        	this.LnkUnxutils.AutoSize = true;
        	this.LnkUnxutils.Location = new System.Drawing.Point(192, 85);
        	this.LnkUnxutils.Name = "LnkUnxutils";
        	this.LnkUnxutils.Size = new System.Drawing.Size(194, 13);
        	this.LnkUnxutils.TabIndex = 4;
        	this.LnkUnxutils.TabStop = true;
        	this.LnkUnxutils.Text = "http://sourceforge.net/projects/unxutils";
        	this.LnkUnxutils.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lnk_LinkClicked);
        	// 
        	// label3
        	// 
        	this.label3.AutoSize = true;
        	this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
        	this.label3.Location = new System.Drawing.Point(6, 9);
        	this.label3.Name = "label3";
        	this.label3.Size = new System.Drawing.Size(202, 20);
        	this.label3.TabIndex = 5;
        	this.label3.Text = "AwkEverywhere v1.0.0.0";
        	// 
        	// label4
        	// 
        	this.label4.AutoSize = true;
        	this.label4.Location = new System.Drawing.Point(7, 40);
        	this.label4.Name = "label4";
        	this.label4.Size = new System.Drawing.Size(153, 13);
        	this.label4.TabIndex = 6;
        	this.label4.Text = "Programmed in C#.Net by MP :";
        	// 
        	// LnkArtiche
        	// 
        	this.LnkArtiche.AutoSize = true;
        	this.LnkArtiche.Location = new System.Drawing.Point(166, 40);
        	this.LnkArtiche.Name = "LnkArtiche";
        	this.LnkArtiche.Size = new System.Drawing.Size(90, 13);
        	this.LnkArtiche.TabIndex = 7;
        	this.LnkArtiche.TabStop = true;
        	this.LnkArtiche.Text = "http://artiche.info";
        	this.LnkArtiche.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.Lnk_LinkClicked);
        	// 
        	// AboutForm
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.BackColor = System.Drawing.SystemColors.Control;
        	this.ClientSize = new System.Drawing.Size(398, 175);
        	this.Controls.Add(this.LnkArtiche);
        	this.Controls.Add(this.label4);
        	this.Controls.Add(this.label3);
        	this.Controls.Add(this.LnkUnxutils);
        	this.Controls.Add(this.label2);
        	this.Controls.Add(this.label1);
        	this.Controls.Add(this.LnkFamfamfam);
        	this.Controls.Add(this.BtnCloseAbout);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.Name = "AboutForm";
        	this.Text = "About AwkEverywhere";
        	this.Load += new System.EventHandler(this.AboutForm_Load);
        	this.ResumeLayout(false);
        	this.PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Button BtnCloseAbout;
        private System.Windows.Forms.LinkLabel LnkFamfamfam;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.LinkLabel LnkUnxutils;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.LinkLabel LnkArtiche;

    }
}