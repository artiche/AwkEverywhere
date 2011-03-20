namespace AwkEverywhere.Forms.WSForms
{
    partial class WSBrowser
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
        	System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WSBrowser));
        	this.groupBox1 = new System.Windows.Forms.GroupBox();
        	this.LabelLocalGuid = new System.Windows.Forms.Label();
        	this.LabelLocalVersion = new System.Windows.Forms.Label();
        	this.LabelLocalModifDate = new System.Windows.Forms.Label();
        	this.LabelLocalCreationDate = new System.Windows.Forms.Label();
        	this.LabelLocalAuthor = new System.Windows.Forms.Label();
        	this.label10 = new System.Windows.Forms.Label();
        	this.label5 = new System.Windows.Forms.Label();
        	this.label6 = new System.Windows.Forms.Label();
        	this.label7 = new System.Windows.Forms.Label();
        	this.label8 = new System.Windows.Forms.Label();
        	this.BtnUploadScript = new System.Windows.Forms.Button();
        	this.LB_LocalScripts = new System.Windows.Forms.ListBox();
        	this.groupBox2 = new System.Windows.Forms.GroupBox();
        	this.BtnViewScript = new System.Windows.Forms.Button();
        	this.label9 = new System.Windows.Forms.Label();
        	this.TB_SearchScript = new System.Windows.Forms.TextBox();
        	this.BtnSearchScript = new System.Windows.Forms.Button();
        	this.label4 = new System.Windows.Forms.Label();
        	this.label3 = new System.Windows.Forms.Label();
        	this.label2 = new System.Windows.Forms.Label();
        	this.label1 = new System.Windows.Forms.Label();
        	this.BtnDownloadScript = new System.Windows.Forms.Button();
        	this.LB_DistantScripts = new System.Windows.Forms.ListBox();
        	this.groupBox1.SuspendLayout();
        	this.groupBox2.SuspendLayout();
        	this.SuspendLayout();
        	// 
        	// groupBox1
        	// 
        	this.groupBox1.Controls.Add(this.LabelLocalGuid);
        	this.groupBox1.Controls.Add(this.LabelLocalVersion);
        	this.groupBox1.Controls.Add(this.LabelLocalModifDate);
        	this.groupBox1.Controls.Add(this.LabelLocalCreationDate);
        	this.groupBox1.Controls.Add(this.LabelLocalAuthor);
        	this.groupBox1.Controls.Add(this.label10);
        	this.groupBox1.Controls.Add(this.label5);
        	this.groupBox1.Controls.Add(this.label6);
        	this.groupBox1.Controls.Add(this.label7);
        	this.groupBox1.Controls.Add(this.label8);
        	this.groupBox1.Controls.Add(this.BtnUploadScript);
        	this.groupBox1.Controls.Add(this.LB_LocalScripts);
        	this.groupBox1.Location = new System.Drawing.Point(12, 12);
        	this.groupBox1.Name = "groupBox1";
        	this.groupBox1.Size = new System.Drawing.Size(368, 417);
        	this.groupBox1.TabIndex = 0;
        	this.groupBox1.TabStop = false;
        	this.groupBox1.Text = "Local scripts";
        	this.groupBox1.Enter += new System.EventHandler(this.GroupBox1Enter);
        	// 
        	// LabelLocalGuid
        	// 
        	this.LabelLocalGuid.Location = new System.Drawing.Point(109, 359);
        	this.LabelLocalGuid.Name = "LabelLocalGuid";
        	this.LabelLocalGuid.Size = new System.Drawing.Size(253, 13);
        	this.LabelLocalGuid.TabIndex = 15;
        	// 
        	// LabelLocalVersion
        	// 
        	this.LabelLocalVersion.Location = new System.Drawing.Point(109, 335);
        	this.LabelLocalVersion.Name = "LabelLocalVersion";
        	this.LabelLocalVersion.Size = new System.Drawing.Size(253, 13);
        	this.LabelLocalVersion.TabIndex = 14;
        	// 
        	// LabelLocalModifDate
        	// 
        	this.LabelLocalModifDate.Location = new System.Drawing.Point(109, 311);
        	this.LabelLocalModifDate.Name = "LabelLocalModifDate";
        	this.LabelLocalModifDate.Size = new System.Drawing.Size(253, 13);
        	this.LabelLocalModifDate.TabIndex = 13;
        	// 
        	// LabelLocalCreationDate
        	// 
        	this.LabelLocalCreationDate.Location = new System.Drawing.Point(109, 287);
        	this.LabelLocalCreationDate.Name = "LabelLocalCreationDate";
        	this.LabelLocalCreationDate.Size = new System.Drawing.Size(253, 13);
        	this.LabelLocalCreationDate.TabIndex = 12;
        	// 
        	// LabelLocalAuthor
        	// 
        	this.LabelLocalAuthor.Location = new System.Drawing.Point(109, 263);
        	this.LabelLocalAuthor.Name = "LabelLocalAuthor";
        	this.LabelLocalAuthor.Size = new System.Drawing.Size(253, 13);
        	this.LabelLocalAuthor.TabIndex = 11;
        	// 
        	// label10
        	// 
        	this.label10.AutoSize = true;
        	this.label10.Location = new System.Drawing.Point(9, 359);
        	this.label10.Name = "label10";
        	this.label10.Size = new System.Drawing.Size(35, 13);
        	this.label10.TabIndex = 10;
        	this.label10.Text = "Guid :";
        	// 
        	// label5
        	// 
        	this.label5.AutoSize = true;
        	this.label5.Location = new System.Drawing.Point(9, 263);
        	this.label5.Name = "label5";
        	this.label5.Size = new System.Drawing.Size(44, 13);
        	this.label5.TabIndex = 9;
        	this.label5.Text = "Author :";
        	// 
        	// label6
        	// 
        	this.label6.AutoSize = true;
        	this.label6.Location = new System.Drawing.Point(9, 335);
        	this.label6.Name = "label6";
        	this.label6.Size = new System.Drawing.Size(48, 13);
        	this.label6.TabIndex = 8;
        	this.label6.Text = "Version :";
        	// 
        	// label7
        	// 
        	this.label7.AutoSize = true;
        	this.label7.Location = new System.Drawing.Point(9, 311);
        	this.label7.Name = "label7";
        	this.label7.Size = new System.Drawing.Size(94, 13);
        	this.label7.TabIndex = 7;
        	this.label7.Text = "Modification date :";
        	// 
        	// label8
        	// 
        	this.label8.AutoSize = true;
        	this.label8.Location = new System.Drawing.Point(9, 287);
        	this.label8.Name = "label8";
        	this.label8.Size = new System.Drawing.Size(76, 13);
        	this.label8.TabIndex = 6;
        	this.label8.Text = "Creation date :";
        	// 
        	// BtnUploadScript
        	// 
        	this.BtnUploadScript.Location = new System.Drawing.Point(274, 388);
        	this.BtnUploadScript.Name = "BtnUploadScript";
        	this.BtnUploadScript.Size = new System.Drawing.Size(88, 23);
        	this.BtnUploadScript.TabIndex = 1;
        	this.BtnUploadScript.Text = "Upload >>";
        	this.BtnUploadScript.UseVisualStyleBackColor = true;
        	// 
        	// LB_LocalScripts
        	// 
        	this.LB_LocalScripts.FormattingEnabled = true;
        	this.LB_LocalScripts.Location = new System.Drawing.Point(12, 21);
        	this.LB_LocalScripts.Name = "LB_LocalScripts";
        	this.LB_LocalScripts.Size = new System.Drawing.Size(350, 238);
        	this.LB_LocalScripts.TabIndex = 0;
        	this.LB_LocalScripts.SelectedIndexChanged += new System.EventHandler(this.LB_LocalScriptsSelectedIndexChanged);
        	// 
        	// groupBox2
        	// 
        	this.groupBox2.Controls.Add(this.BtnViewScript);
        	this.groupBox2.Controls.Add(this.label9);
        	this.groupBox2.Controls.Add(this.TB_SearchScript);
        	this.groupBox2.Controls.Add(this.BtnSearchScript);
        	this.groupBox2.Controls.Add(this.label4);
        	this.groupBox2.Controls.Add(this.label3);
        	this.groupBox2.Controls.Add(this.label2);
        	this.groupBox2.Controls.Add(this.label1);
        	this.groupBox2.Controls.Add(this.BtnDownloadScript);
        	this.groupBox2.Controls.Add(this.LB_DistantScripts);
        	this.groupBox2.Location = new System.Drawing.Point(386, 12);
        	this.groupBox2.Name = "groupBox2";
        	this.groupBox2.Size = new System.Drawing.Size(385, 417);
        	this.groupBox2.TabIndex = 1;
        	this.groupBox2.TabStop = false;
        	this.groupBox2.Text = "Distant scripts";
        	// 
        	// BtnViewScript
        	// 
        	this.BtnViewScript.Location = new System.Drawing.Point(282, 388);
        	this.BtnViewScript.Name = "BtnViewScript";
        	this.BtnViewScript.Size = new System.Drawing.Size(97, 23);
        	this.BtnViewScript.TabIndex = 9;
        	this.BtnViewScript.Text = "View Script...";
        	this.BtnViewScript.UseVisualStyleBackColor = true;
        	// 
        	// label9
        	// 
        	this.label9.AutoSize = true;
        	this.label9.Location = new System.Drawing.Point(6, 359);
        	this.label9.Name = "label9";
        	this.label9.Size = new System.Drawing.Size(35, 13);
        	this.label9.TabIndex = 8;
        	this.label9.Text = "Guid :";
        	// 
        	// TB_SearchScript
        	// 
        	this.TB_SearchScript.Location = new System.Drawing.Point(6, 21);
        	this.TB_SearchScript.Name = "TB_SearchScript";
        	this.TB_SearchScript.Size = new System.Drawing.Size(308, 20);
        	this.TB_SearchScript.TabIndex = 7;
        	// 
        	// BtnSearchScript
        	// 
        	this.BtnSearchScript.Location = new System.Drawing.Point(320, 19);
        	this.BtnSearchScript.Name = "BtnSearchScript";
        	this.BtnSearchScript.Size = new System.Drawing.Size(59, 23);
        	this.BtnSearchScript.TabIndex = 6;
        	this.BtnSearchScript.Text = "Search";
        	this.BtnSearchScript.UseVisualStyleBackColor = true;
        	// 
        	// label4
        	// 
        	this.label4.AutoSize = true;
        	this.label4.Location = new System.Drawing.Point(6, 263);
        	this.label4.Name = "label4";
        	this.label4.Size = new System.Drawing.Size(44, 13);
        	this.label4.TabIndex = 5;
        	this.label4.Text = "Author :";
        	// 
        	// label3
        	// 
        	this.label3.AutoSize = true;
        	this.label3.Location = new System.Drawing.Point(6, 335);
        	this.label3.Name = "label3";
        	this.label3.Size = new System.Drawing.Size(48, 13);
        	this.label3.TabIndex = 4;
        	this.label3.Text = "Version :";
        	// 
        	// label2
        	// 
        	this.label2.AutoSize = true;
        	this.label2.Location = new System.Drawing.Point(6, 311);
        	this.label2.Name = "label2";
        	this.label2.Size = new System.Drawing.Size(94, 13);
        	this.label2.TabIndex = 3;
        	this.label2.Text = "Modification date :";
        	// 
        	// label1
        	// 
        	this.label1.AutoSize = true;
        	this.label1.Location = new System.Drawing.Point(6, 287);
        	this.label1.Name = "label1";
        	this.label1.Size = new System.Drawing.Size(76, 13);
        	this.label1.TabIndex = 2;
        	this.label1.Text = "Creation date :";
        	// 
        	// BtnDownloadScript
        	// 
        	this.BtnDownloadScript.Location = new System.Drawing.Point(6, 388);
        	this.BtnDownloadScript.Name = "BtnDownloadScript";
        	this.BtnDownloadScript.Size = new System.Drawing.Size(85, 23);
        	this.BtnDownloadScript.TabIndex = 1;
        	this.BtnDownloadScript.Text = "<< Download";
        	this.BtnDownloadScript.UseVisualStyleBackColor = true;
        	// 
        	// LB_DistantScripts
        	// 
        	this.LB_DistantScripts.FormattingEnabled = true;
        	this.LB_DistantScripts.Location = new System.Drawing.Point(6, 48);
        	this.LB_DistantScripts.Name = "LB_DistantScripts";
        	this.LB_DistantScripts.Size = new System.Drawing.Size(373, 212);
        	this.LB_DistantScripts.TabIndex = 0;
        	// 
        	// WSBrowser
        	// 
        	this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
        	this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
        	this.ClientSize = new System.Drawing.Size(955, 441);
        	this.Controls.Add(this.groupBox2);
        	this.Controls.Add(this.groupBox1);
        	this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
        	this.Name = "WSBrowser";
        	this.Text = "WSBrowser";
        	this.Load += new System.EventHandler(this.WSBrowserLoad);
        	this.groupBox1.ResumeLayout(false);
        	this.groupBox1.PerformLayout();
        	this.groupBox2.ResumeLayout(false);
        	this.groupBox2.PerformLayout();
        	this.ResumeLayout(false);
        }
        private System.Windows.Forms.Label LabelLocalAuthor;
        private System.Windows.Forms.Label LabelLocalCreationDate;
        private System.Windows.Forms.Label LabelLocalModifDate;
        private System.Windows.Forms.Label LabelLocalVersion;
        private System.Windows.Forms.Label LabelLocalGuid;
        private System.Windows.Forms.Button BtnViewScript;
        private System.Windows.Forms.TextBox TB_SearchScript;
        private System.Windows.Forms.Button BtnDownloadScript;
        private System.Windows.Forms.Button BtnSearchScript;
        private System.Windows.Forms.Button BtnUploadScript;
        private System.Windows.Forms.ListBox LB_DistantScripts;
        private System.Windows.Forms.ListBox LB_LocalScripts;

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
    }
}