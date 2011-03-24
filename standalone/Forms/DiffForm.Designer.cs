namespace AwkEverywhere.Forms
{
    partial class DiffForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DiffForm));
            this.ButtonsPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.BtnPrevDiff = new System.Windows.Forms.Button();
            this.BtnNextDiff = new System.Windows.Forms.Button();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.TB_Input = new System.Windows.Forms.RichTextBox();
            this.TB_Output = new System.Windows.Forms.RichTextBox();
            this.ButtonsPanel.SuspendLayout();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SuspendLayout();
            // 
            // ButtonsPanel
            // 
            this.ButtonsPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ButtonsPanel.Controls.Add(this.BtnPrevDiff);
            this.ButtonsPanel.Controls.Add(this.BtnNextDiff);
            this.ButtonsPanel.Location = new System.Drawing.Point(12, 8);
            this.ButtonsPanel.Name = "ButtonsPanel";
            this.ButtonsPanel.Size = new System.Drawing.Size(832, 28);
            this.ButtonsPanel.TabIndex = 0;
            // 
            // BtnPrevDiff
            // 
            this.BtnPrevDiff.AutoSize = true;
            this.BtnPrevDiff.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnPrevDiff.Location = new System.Drawing.Point(3, 3);
            this.BtnPrevDiff.Name = "BtnPrevDiff";
            this.BtnPrevDiff.Size = new System.Drawing.Size(42, 23);
            this.BtnPrevDiff.TabIndex = 0;
            this.BtnPrevDiff.Text = "Prev.";
            this.BtnPrevDiff.UseVisualStyleBackColor = true;
            // 
            // BtnNextDiff
            // 
            this.BtnNextDiff.AutoSize = true;
            this.BtnNextDiff.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.BtnNextDiff.Location = new System.Drawing.Point(51, 3);
            this.BtnNextDiff.Name = "BtnNextDiff";
            this.BtnNextDiff.Size = new System.Drawing.Size(39, 23);
            this.BtnNextDiff.TabIndex = 1;
            this.BtnNextDiff.Text = "Next";
            this.BtnNextDiff.UseVisualStyleBackColor = true;
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.Location = new System.Drawing.Point(12, 40);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.TB_Input);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.TB_Output);
            this.splitContainer1.Size = new System.Drawing.Size(832, 334);
            this.splitContainer1.SplitterDistance = 414;
            this.splitContainer1.TabIndex = 1;
            // 
            // TB_Input
            // 
            this.TB_Input.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Input.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_Input.Location = new System.Drawing.Point(3, 3);
            this.TB_Input.Name = "TB_Input";
            this.TB_Input.ReadOnly = true;
            this.TB_Input.Size = new System.Drawing.Size(408, 328);
            this.TB_Input.TabIndex = 0;
            this.TB_Input.Text = "";
            this.TB_Input.WordWrap = false;
            // 
            // TB_Output
            // 
            this.TB_Output.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.TB_Output.Font = new System.Drawing.Font("Courier New", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TB_Output.Location = new System.Drawing.Point(3, 3);
            this.TB_Output.Name = "TB_Output";
            this.TB_Output.ReadOnly = true;
            this.TB_Output.Size = new System.Drawing.Size(408, 327);
            this.TB_Output.TabIndex = 1;
            this.TB_Output.Text = "";
            this.TB_Output.WordWrap = false;
            // 
            // DiffForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(856, 386);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.ButtonsPanel);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "DiffForm";
            this.Text = "DiffForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DiffForm_FormClosing);
            this.ButtonsPanel.ResumeLayout(false);
            this.ButtonsPanel.PerformLayout();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.FlowLayoutPanel ButtonsPanel;
        private System.Windows.Forms.Button BtnPrevDiff;
        private System.Windows.Forms.Button BtnNextDiff;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.RichTextBox TB_Input;
        private System.Windows.Forms.RichTextBox TB_Output;
    }
}