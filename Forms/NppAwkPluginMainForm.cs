using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using AwkEverywhere;
using AwkEverywhere.Config;

namespace AwkEverywhere.Forms
{
    public partial class NppAwkPluginMainForm : Form
    {
        private AwkEverywhere.Frontend.IFrontEnd moFrontEnd;
        private IFrontEndConfig moConfig;
        private IScript moSelectedScript = null;
        private bool bHideOnClose = true;
        
        private HotKey moHotKey;
        
		public bool HideOnClose {
			get { return bHideOnClose; }
			set { bHideOnClose = value; }
		}

        public NppAwkPluginMainForm(
            AwkEverywhere.Frontend.IFrontEnd oFrontEnd,
            AwkEverywhere.Config.IFrontEndConfig oConfig
            )
        {
            moConfig = oConfig;
            moFrontEnd = oFrontEnd;
            
            moHotKey = new HotKey(this);
			moHotKey.RegisterHotKey(Keys.A, HotKey.HotKeyModifiers.Control | HotKey.HotKeyModifiers.Alt);

			moHotKey.HotKeyPress += new HotKey.HotKeyHandler(moHotKey_HotKeyPress);
            
            InitializeComponent();
            
            TB_Data.AllowDrop = true;
            TB_Data.DragDrop += new DragEventHandler(TB_ScriptAwkDragDrop);
            TB_Data.DragEnter += new DragEventHandler(TB_ScriptAwkDragEnter);
            
        }

        void moHotKey_HotKeyPress(object sender, HotKeyArgs args)
        {
        	this.Show();
        	this.Activate();
        }

        //needed to catch enter key :
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 273 && m.WParam.ToInt32() == 1)
            {
                if (TB_ScriptAwk.Focused) { PasteString(TB_ScriptAwk, Environment.NewLine); }
                else if (TB_Data.Focused) { PasteString(TB_Data,Environment.NewLine); }
                else if (TB_Result.Focused) { PasteString(TB_Result, Environment.NewLine); }
                else if (BtnCopyFromNpp.Focused) { BtnCopyFromNpp.PerformClick(); }
                else if (BtnCopyToNpp.Focused) { BtnCopyToNpp.PerformClick(); }
                else if (BtnDeleteScript.Focused) { BtnDeleteScript.PerformClick(); }
                else if (BtnNewScript.Focused) { BtnNewScript.PerformClick(); }
                else if (BtnSelectAwkPath.Focused) { BtnSelectAwkPath.PerformClick(); }
                else if (BtnExecScript.Focused) { BtnExecScript.PerformClick(); }
            }
            
            base.WndProc(ref m);
        }



        private void B_ExecScript_Click(object sender, EventArgs e)
        {
            try
            {
                moFrontEnd.Data = TB_Data.Text;
                moFrontEnd.Script = TB_ScriptAwk.Text;
                moFrontEnd.ExePath = moConfig.ProgramPath;
                moFrontEnd.TempDirectory = moConfig.PluginWorkingDirectory;
                

                moFrontEnd.ExecScript();

                string sResult = moFrontEnd.Result;
                string sError = moFrontEnd.Error;
                if (sError.Trim() != string.Empty)
                {
                    TB_Result.Text = sError;
                }
                else
                {
                    TB_Result.Text = moFrontEnd.Result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSelectAwkPath_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {
            TB_PathAwk.Text = openFileDialog1.FileName;
            moConfig.ProgramPath = openFileDialog1.FileName;
            moConfig.WriteConfig();
        }

        private void NppAwkPluginMainForm_Load(object sender, EventArgs e)
        {
            //load scripts;
            List<IScript> oListeScripts = moConfig.GetScripts();
            if (oListeScripts.Count == 0)
            {
                moConfig.CreateNewScript();
            }
            
            CBScriptTitle.Items.AddRange(oListeScripts.ToArray());

            foreach (IScript oElt in oListeScripts)
            {
                if (oElt.Id == moConfig.SelectedScriptId)
                {
                    CBScriptTitle.SelectedItem = oElt;
                    break; //exit foreach
                }
            }
            if (CBScriptTitle.SelectedItem == null)
            {
                CBScriptTitle.SelectedItem = oListeScripts[0];
            }

            TB_PathAwk.Text = moConfig.ProgramPath;
        }

        
        private void CBScriptTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            IScript oSelectedScript = CBScriptTitle.SelectedItem as IScript;
            if (oSelectedScript != null)
            {
                TB_ScriptAwk.Text = oSelectedScript.Script;
                moSelectedScript = oSelectedScript;
                if (moConfig.SelectedScriptId != moSelectedScript.Id)
                {
                    moConfig.SelectedScriptId = moSelectedScript.Id;
                    moConfig.WriteConfig();
                }
            }
        }

        private void BtnNewScript_Click(object sender, EventArgs e)
        {

            //create new script
            IScript oNewScript = moConfig.CreateNewScript();
            int iIndex = CBScriptTitle.Items.Add(oNewScript);
            CBScriptTitle.SelectedIndex = iIndex;
 
        }


        private void BtnDeleteScript_Click(object sender, EventArgs e)
        {
            IScript oSelectedScript = CBScriptTitle.SelectedItem as IScript;
            if (oSelectedScript != null)
            {
                moConfig.DeleteScript(oSelectedScript);
                int iIndex = CBScriptTitle.Items.IndexOf(oSelectedScript);
                
                CBScriptTitle.Items.Remove(oSelectedScript);
                moSelectedScript = null;

                if (CBScriptTitle.Items.Count == 0)
                {
                    IScript oNewScript = moConfig.CreateNewScript();
                    CBScriptTitle.Items.Add(oNewScript);
                }
                CBScriptTitle.SelectedIndex = Math.Max(iIndex-1, 0);
            }
        }

        private void TB_ScriptAwk_Validating(object sender, CancelEventArgs e)
        {
            //update current script
            IScript oSelectedScript = CBScriptTitle.SelectedItem as IScript;
            if (oSelectedScript != null)
            {
                oSelectedScript.Script = TB_ScriptAwk.Text;
            }
        }


        private void CBScriptTitle_Validating(object sender, CancelEventArgs e)
        {
            //update current script
            IScript oSelectedScriptInCombo = CBScriptTitle.SelectedItem as IScript;
            if (moSelectedScript != null && oSelectedScriptInCombo == null)
            {
                moSelectedScript.Title = CBScriptTitle.Text;
                int iIndex = CBScriptTitle.Items.IndexOf(moSelectedScript);
                CBScriptTitle.Items.RemoveAt(iIndex);
                CBScriptTitle.Items.Insert(iIndex, moSelectedScript);
            }
        }

        private void NppAwkPluginMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (IScript oScript in moConfig.GetScripts())
            {
                if (oScript.IsModified)
                {
                    moConfig.WriteScript(oScript);  
                }
            }

            if(HideOnClose){
	            this.Hide();
	            e.Cancel = true;
            }
            
        }


        private void BtnCopyFromNpp_Click(object sender, EventArgs e)
        {
            if (this.CopyFromNpp != null)
            {
                this.CopyFromNpp(sender, e);
            }
        }

        private void BtnCopyToNpp_Click(object sender, EventArgs e)
        {
            if (this.CopyToNpp != null)
            {
                this.CopyToNpp(sender, e);
            }
        }


        public event EventHandler CopyFromNpp;
        public event EventHandler CopyToNpp;

        public void SetDataString(string sData)
        {
            TB_Data.Text = sData;
        }

        public string GetResultString()
        {
            return TB_Result.Text;
        }

        private void PasteString(TextBox oTB, string sText)
        {
            StringBuilder oBuffer = new StringBuilder(sText);
            
            if (sText == Environment.NewLine)
            {
                int iSelStart = oTB.SelectionStart;
                int iLigne = oTB.GetLineFromCharIndex(iSelStart);

                if (oTB.Lines.Length > iLigne)
                {
                    string sLigne = oTB.Lines[iLigne];
                    foreach (char c in sLigne)
                    {
                        if (c == ' ' || c == '\t')
                        {
                            oBuffer.Append(c);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
            oTB.Paste(oBuffer.ToString());
        }

        private void PasteString(RichTextBox oTB, string sText)
        {
            StringBuilder oBuffer = new StringBuilder(sText);
            
            if (sText == Environment.NewLine)
            {
                int iSelStart = oTB.SelectionStart;
                int iLigne = oTB.GetLineFromCharIndex(iSelStart);

                if (oTB.Lines.Length > iLigne)
                {
                    string sLigne = oTB.Lines[iLigne];
                    foreach (char c in sLigne)
                    {
                        if (c == ' ' || c == '\t')
                        {
                            oBuffer.Append(c);
                        }
                        else
                        {
                            break;
                        }
                    }
                }
            }
           
            oTB.SelectedText = oBuffer.ToString();
        }

        private void TB_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.Space)
            {
                TextBox oTB = sender as TextBox;
                if (oTB != null)
                {
                    PasteString(oTB, "\t");
                }
                else
                {
                    RichTextBox oRTB = sender as RichTextBox;
                    PasteString(oRTB, "\t");
                }
                e.SuppressKeyPress = true;
            }
            
            if (e.KeyCode == Keys.Return) {
            	TextBox oTB = sender as TextBox;
                if (oTB != null)
                {
                	PasteString(oTB, Environment.NewLine);
                    e.SuppressKeyPress = true;
                }
            }
           
        }
        
        	


        
       
        
        void TB_ScriptAwkDragEnter(object sender, DragEventArgs e)
        {
        	if( e.Data.GetDataPresent(DataFormats.FileDrop, false) == true ){
        		e.Effect = DragDropEffects.All;
        	} else {
        		e.Effect = DragDropEffects.None;
        	}
        }
        
        void TB_ScriptAwkDragDrop(object sender, DragEventArgs e)
        {
        	string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
			
        	if(sender!=null){        	
				foreach( string file in files )
				{
					try {
						StreamReader oReader = new StreamReader(file);
						if(sender == TB_Data){
							TB_Data.Clear();
							TB_Data.AppendText(oReader.ReadToEnd());
						} else if (sender == TB_ScriptAwk){
							IScript oNewScript = moConfig.CreateNewScript();
							oNewScript.Title = Path.GetFileName(file);
							oNewScript.Script = oReader.ReadToEnd();
							int iIndex = CBScriptTitle.Items.Add(oNewScript);
							CBScriptTitle.SelectedIndex = iIndex;
						}
					} catch (Exception ex) {
						MessageBox.Show(ex.Message);
					}
				}
        	}
        }
        
    }
}