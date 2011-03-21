namespace AwkEverywhere.Forms
{
    partial class AwkEverywhereMainForm
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AwkEverywhereMainForm));
            this.TB_PathAwk = new System.Windows.Forms.TextBox();
            this.TB_ScriptAwk = new System.Windows.Forms.TextBox();
            this.BtnExecScript = new System.Windows.Forms.Button();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.BtnSelectAwkPath = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.BtnDeleteScript = new System.Windows.Forms.Button();
            this.BtnNewScript = new System.Windows.Forms.Button();
            this.CBScriptTitle = new System.Windows.Forms.ComboBox();
            this.splitContainer2 = new System.Windows.Forms.SplitContainer();
            this.TB_Data = new System.Windows.Forms.RichTextBox();
            this.BtnCopyFromNpp = new System.Windows.Forms.Button();
            this.BtnCopyToNpp = new System.Windows.Forms.Button();
            this.TB_Result = new System.Windows.Forms.RichTextBox();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.splitContainer2.Panel1.SuspendLayout();
            this.splitContainer2.Panel2.SuspendLayout();
            this.splitContainer2.SuspendLayout();
            this.SuspendLayout();
            // 
            // TB_PathAwk
            // 
            this.TB_PathAwk.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_PathAwk.Location = new System.Drawing.Point(76, 12);
            this.TB_PathAwk.Name = "TB_PathAwk";
            this.TB_PathAwk.ReadOnly = true;
            this.TB_PathAwk.Size = new System.Drawing.Size(593, 20);
            this.TB_PathAwk.TabIndex = 0;
            this.TB_PathAwk.TabStop = false;
            // 
            // TB_ScriptAwk
            // 
            this.TB_ScriptAwk.AcceptsTab = true;
            this.TB_ScriptAwk.AllowDrop = true;
            this.TB_ScriptAwk.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_ScriptAwk.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_ScriptAwk.Location = new System.Drawing.Point(3, 30);
            this.TB_ScriptAwk.MaxLength = 327670;
            this.TB_ScriptAwk.Multiline = true;
            this.TB_ScriptAwk.Name = "TB_ScriptAwk";
            this.TB_ScriptAwk.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.TB_ScriptAwk.Size = new System.Drawing.Size(275, 246);
            this.TB_ScriptAwk.TabIndex = 3;
            this.TB_ScriptAwk.WordWrap = false;
            this.TB_ScriptAwk.DragDrop += new System.Windows.Forms.DragEventHandler(this.TB_ScriptAwkDragDrop);
            this.TB_ScriptAwk.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_KeyDown);
            this.TB_ScriptAwk.Validating += new System.ComponentModel.CancelEventHandler(this.TB_ScriptAwk_Validating);
            this.TB_ScriptAwk.DragEnter += new System.Windows.Forms.DragEventHandler(this.TB_ScriptAwkDragEnter);
            // 
            // BtnExecScript
            // 
            this.BtnExecScript.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnExecScript.Location = new System.Drawing.Point(0, 276);
            this.BtnExecScript.Margin = new System.Windows.Forms.Padding(1, 3, 3, 3);
            this.BtnExecScript.Name = "BtnExecScript";
            this.BtnExecScript.Size = new System.Drawing.Size(278, 23);
            this.BtnExecScript.TabIndex = 4;
            this.BtnExecScript.Text = "E&xec Script";
            this.BtnExecScript.UseVisualStyleBackColor = true;
            this.BtnExecScript.Click += new System.EventHandler(this.B_ExecScript_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "*.exe|*.exe";
            this.openFileDialog1.FileOk += new System.ComponentModel.CancelEventHandler(this.openFileDialog1_FileOk);
            // 
            // BtnSelectAwkPath
            // 
            this.BtnSelectAwkPath.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnSelectAwkPath.Location = new System.Drawing.Point(675, 10);
            this.BtnSelectAwkPath.Name = "BtnSelectAwkPath";
            this.BtnSelectAwkPath.Size = new System.Drawing.Size(25, 23);
            this.BtnSelectAwkPath.TabIndex = 6;
            this.BtnSelectAwkPath.TabStop = false;
            this.BtnSelectAwkPath.Text = "...";
            this.BtnSelectAwkPath.UseVisualStyleBackColor = true;
            this.BtnSelectAwkPath.Click += new System.EventHandler(this.BtnSelectAwkPath_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 13);
            this.label1.TabIndex = 7;
            this.label1.Text = "Awk path :";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 39);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.Controls.Add(this.BtnExecScript);
            this.splitContainer1.Panel1.Controls.Add(this.TB_ScriptAwk);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
            this.splitContainer1.Size = new System.Drawing.Size(688, 299);
            this.splitContainer1.SplitterDistance = 278;
            this.splitContainer1.TabIndex = 8;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.BtnDeleteScript);
            this.panel1.Controls.Add(this.BtnNewScript);
            this.panel1.Controls.Add(this.CBScriptTitle);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(278, 27);
            this.panel1.TabIndex = 5;
            // 
            // BtnDeleteScript
            // 
            this.BtnDeleteScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnDeleteScript.FlatAppearance.BorderSize = 0;
            this.BtnDeleteScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnDeleteScript.Image = global::AwkEverywhere.Properties.Resources.DeleteScriptIcon;
            this.BtnDeleteScript.Location = new System.Drawing.Point(259, 3);
            this.BtnDeleteScript.Margin = new System.Windows.Forms.Padding(1, 3, 2, 3);
            this.BtnDeleteScript.Name = "BtnDeleteScript";
            this.BtnDeleteScript.Size = new System.Drawing.Size(17, 21);
            this.BtnDeleteScript.TabIndex = 3;
            this.BtnDeleteScript.TabStop = false;
            this.toolTip1.SetToolTip(this.BtnDeleteScript, "Delete Script");
            this.BtnDeleteScript.UseVisualStyleBackColor = true;
            this.BtnDeleteScript.Click += new System.EventHandler(this.BtnDeleteScript_Click);
            // 
            // BtnNewScript
            // 
            this.BtnNewScript.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.BtnNewScript.FlatAppearance.BorderSize = 0;
            this.BtnNewScript.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BtnNewScript.Image = global::AwkEverywhere.Properties.Resources.NewScriptIcon;
            this.BtnNewScript.Location = new System.Drawing.Point(240, 3);
            this.BtnNewScript.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.BtnNewScript.Name = "BtnNewScript";
            this.BtnNewScript.Size = new System.Drawing.Size(17, 21);
            this.BtnNewScript.TabIndex = 1;
            this.BtnNewScript.TabStop = false;
            this.toolTip1.SetToolTip(this.BtnNewScript, "New Script");
            this.BtnNewScript.UseVisualStyleBackColor = true;
            this.BtnNewScript.Click += new System.EventHandler(this.BtnNewScript_Click);
            // 
            // CBScriptTitle
            // 
            this.CBScriptTitle.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.CBScriptTitle.FormattingEnabled = true;
            this.CBScriptTitle.Location = new System.Drawing.Point(3, 3);
            this.CBScriptTitle.Name = "CBScriptTitle";
            this.CBScriptTitle.Size = new System.Drawing.Size(233, 21);
            this.CBScriptTitle.TabIndex = 0;
            this.CBScriptTitle.Validating += new System.ComponentModel.CancelEventHandler(this.CBScriptTitle_Validating);
            this.CBScriptTitle.SelectedIndexChanged += new System.EventHandler(this.CBScriptTitle_SelectedIndexChanged);
            // 
            // splitContainer2
            // 
            this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer2.Location = new System.Drawing.Point(0, 0);
            this.splitContainer2.Name = "splitContainer2";
            this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer2.Panel1
            // 
            this.splitContainer2.Panel1.Controls.Add(this.TB_Data);
            this.splitContainer2.Panel1.Controls.Add(this.BtnCopyFromNpp);
            // 
            // splitContainer2.Panel2
            // 
            this.splitContainer2.Panel2.Controls.Add(this.BtnCopyToNpp);
            this.splitContainer2.Panel2.Controls.Add(this.TB_Result);
            this.splitContainer2.Size = new System.Drawing.Size(406, 299);
            this.splitContainer2.SplitterDistance = 150;
            this.splitContainer2.TabIndex = 0;
            // 
            // TB_Data
            // 
            this.TB_Data.AcceptsTab = true;
            this.TB_Data.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.TB_Data.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TB_Data.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_Data.Location = new System.Drawing.Point(0, 0);
            this.TB_Data.Name = "TB_Data";
            this.TB_Data.Size = new System.Drawing.Size(406, 127);
            this.TB_Data.TabIndex = 0;
            this.TB_Data.Text = "";
            this.TB_Data.WordWrap = false;
            this.TB_Data.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_KeyDown);
            // 
            // BtnCopyFromNpp
            // 
            this.BtnCopyFromNpp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(192)))), ((int)(((byte)(255)))));
            this.BtnCopyFromNpp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnCopyFromNpp.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BtnCopyFromNpp.Location = new System.Drawing.Point(0, 127);
            this.BtnCopyFromNpp.Margin = new System.Windows.Forms.Padding(3, 3, 1, 3);
            this.BtnCopyFromNpp.Name = "BtnCopyFromNpp";
            this.BtnCopyFromNpp.Size = new System.Drawing.Size(406, 23);
            this.BtnCopyFromNpp.TabIndex = 1;
            this.BtnCopyFromNpp.Text = "Copy &from clipboard";
            this.toolTip1.SetToolTip(this.BtnCopyFromNpp, "Copy Notepad++ sélection");
            this.BtnCopyFromNpp.UseVisualStyleBackColor = false;
            this.BtnCopyFromNpp.Click += new System.EventHandler(this.BtnCopyFromNpp_Click);
            // 
            // BtnCopyToNpp
            // 
            this.BtnCopyToNpp.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.BtnCopyToNpp.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.BtnCopyToNpp.Location = new System.Drawing.Point(0, 122);
            this.BtnCopyToNpp.Margin = new System.Windows.Forms.Padding(1, 3, 1, 3);
            this.BtnCopyToNpp.Name = "BtnCopyToNpp";
            this.BtnCopyToNpp.Size = new System.Drawing.Size(406, 23);
            this.BtnCopyToNpp.TabIndex = 5;
            this.BtnCopyToNpp.Text = "Copy to clipboard";
            this.BtnCopyToNpp.UseVisualStyleBackColor = false;
            this.BtnCopyToNpp.Click += new System.EventHandler(this.BtnCopyToNpp_Click);
            // 
            // TB_Result
            // 
            this.TB_Result.AcceptsTab = true;
            this.TB_Result.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Result.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.TB_Result.Font = new System.Drawing.Font("Courier New", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_Result.Location = new System.Drawing.Point(0, 2);
            this.TB_Result.Name = "TB_Result";
            this.TB_Result.Size = new System.Drawing.Size(406, 120);
            this.TB_Result.TabIndex = 0;
            this.TB_Result.Text = "";
            this.TB_Result.WordWrap = false;
            this.TB_Result.KeyDown += new System.Windows.Forms.KeyEventHandler(this.TB_KeyDown);
            // 
            // NppAwkPluginMainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(712, 344);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.BtnSelectAwkPath);
            this.Controls.Add(this.TB_PathAwk);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "NppAwkPluginMainForm";
            this.Text = "AwkEverywhere";
            this.Load += new System.EventHandler(this.NppAwkPluginMainForm_Load);
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.NppAwkPluginMainForm_FormClosing);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.splitContainer2.Panel1.ResumeLayout(false);
            this.splitContainer2.Panel2.ResumeLayout(false);
            this.splitContainer2.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox TB_PathAwk;
        private System.Windows.Forms.TextBox TB_ScriptAwk;
        private System.Windows.Forms.Button BtnExecScript;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.Button BtnSelectAwkPath;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button BtnNewScript;
        private System.Windows.Forms.ComboBox CBScriptTitle;
        private System.Windows.Forms.Button BtnDeleteScript;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.RichTextBox TB_Data;
        private System.Windows.Forms.RichTextBox TB_Result;
        private System.Windows.Forms.Button BtnCopyFromNpp;
        private System.Windows.Forms.Button BtnCopyToNpp;
    }
}

