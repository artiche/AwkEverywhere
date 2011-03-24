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
using AwkEverywhere.Frontend;
using System.Configuration;

namespace AwkEverywhere.Forms
{
    public partial class AwkEverywhereMainForm : Form
    {
        private Dictionary<Type, IFrontEnd> moFrontEnds;
        private Dictionary<Type, IFrontEndConfig> moConfigs;
        private DiffFrontEnd moDiffFrontEnd = null;
        private IScript moSelectedScript = null;
        private bool bHideOnClose = true;
        
        private HotKey moHotKey;
        
		public bool HideOnClose {
			get { return bHideOnClose; }
			set { bHideOnClose = value; }
		}

        public AwkEverywhereMainForm(
            Dictionary<Type,IFrontEndConfig> oConfigs,
            Dictionary<Type,IFrontEnd> oFrontEnds
            )
        {
            moConfigs = oConfigs;
            moFrontEnds = oFrontEnds;

            moDiffFrontEnd = new DiffFrontEnd();
            
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
                IFrontEnd oFrontEnd = moFrontEnds[moSelectedScript.GetType()];
                IFrontEndConfig oConfig = moConfigs[moSelectedScript.GetType()];
                oFrontEnd.Data.Clear();
                oFrontEnd.Data.Add(TB_Data.Text);

                oFrontEnd.Script = moSelectedScript;

                oFrontEnd.ExePath = oConfig.ProgramPath;
                oFrontEnd.TempDirectory = Path.Combine(oConfig.WorkingDirectory,"tmp");

                oFrontEnd.ExecScript(moConfigs);

                string sResult = oFrontEnd.Result;
                string sError = oFrontEnd.Error;
                if (sError.Trim() != string.Empty)
                {
                    TB_Result.Text = sError;
                }
                else
                {
                    TB_Result.Text = oFrontEnd.Result;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnSelectAwkPath_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                if (sender == BtnSelectAwkPath)
                {
                    TB_PathAwk.Text = openFileDialog1.FileName;
                    moConfigs[typeof(AwkScriptXml)].ProgramPath = openFileDialog1.FileName;
                    moConfigs[typeof(AwkScriptXml)].WriteConfig();
                }
                else if (sender == BtnSelectShPath)
                {
                    TB_PathSh.Text = openFileDialog1.FileName;
                    moConfigs[typeof(ShScriptXml)].ProgramPath = openFileDialog1.FileName;
                    moConfigs[typeof(ShScriptXml)].WriteConfig();
                }
            }
        }

        

        private void NppAwkPluginMainForm_Load(object sender, EventArgs e)
        {
            //load scripts;
            IFrontEndConfig oAwkConfig = moConfigs[typeof(AwkScriptXml)];
            IFrontEndConfig oShConfig = moConfigs[typeof(ShScriptXml)];

            List<IScript> oListeScripts = new List<IScript>();
            oListeScripts.AddRange(oAwkConfig.GetScripts());
            oListeScripts.AddRange(oShConfig.GetScripts());

            if (oListeScripts.Count == 0)
            {
                oListeScripts.Add(oAwkConfig.CreateNewScript());
            }
            
            CBScriptTitle.Items.AddRange(oListeScripts.ToArray());

            int iSelectedScriptId = Math.Max(oAwkConfig.SelectedScriptId, oShConfig.SelectedScriptId);

            foreach (IScript oElt in oListeScripts)
            {
                
                if (oElt.Id == iSelectedScriptId)
                {
                    CBScriptTitle.SelectedItem = oElt;
                    break; //exit foreach
                }
            }
            if (CBScriptTitle.SelectedItem == null)
            {
                CBScriptTitle.SelectedItem = oListeScripts[0];
            }

            
            TB_PathAwk.Text = oAwkConfig.ProgramPath;
            TB_PathSh.Text = oShConfig.ProgramPath;

            BtnSelectAwkPath.Visible = string.IsNullOrEmpty(ConfigurationManager.AppSettings[AppSettingsKey.AWK_PATH_KEY]);
            BtnSelectShPath.Visible = string.IsNullOrEmpty(ConfigurationManager.AppSettings[AppSettingsKey.SH_PATH_KEY]);
        }

        
        private void CBScriptTitle_SelectedIndexChanged(object sender, EventArgs e)
        {
            IScript oSelectedScript = CBScriptTitle.SelectedItem as IScript;
            if (oSelectedScript != null)
            {
                TB_ScriptAwk.Text = oSelectedScript.Script;
                moSelectedScript = oSelectedScript;
                IFrontEndConfig oConfig = moConfigs[oSelectedScript.GetType()];
                if (oConfig.SelectedScriptId != moSelectedScript.Id)
                {
                    oConfig.SelectedScriptId = moSelectedScript.Id;
                    foreach (IFrontEndConfig elt in moConfigs.Values)
                    {
                        if (elt != oConfig)
                        {
                            elt.SelectedScriptId = -1;
                            elt.WriteConfig();
                        }
                    }
                    oConfig.WriteConfig();
                }
                switch (oSelectedScript.Type)
                {
                    case ScriptType.Sh: 
                        RB_Sh.Checked = true;
                        TB_ScriptAwk.BackColor = Color.LightGray;
                        break;
                    case ScriptType.Awk :
                    default :
                        RB_Awk.Checked = true;
                        TB_ScriptAwk.BackColor = Color.White;
                        break;
                }
            }
        }

        private void BtnNewScript_Click(object sender, EventArgs e)
        {
            //create new script
            if (RB_Awk.Checked)
            {   
                IScript oNewScript = moConfigs[typeof(AwkScriptXml)].CreateNewScript();
                int iIndex = CBScriptTitle.Items.Add(oNewScript);
                CBScriptTitle.SelectedIndex = iIndex;
            }
            else if (RB_Sh.Checked)
            {
                IScript oNewScript = moConfigs[typeof(ShScriptXml)].CreateNewScript();
                int iIndex = CBScriptTitle.Items.Add(oNewScript);
                CBScriptTitle.SelectedIndex = iIndex;
            }
 
        }


        private void BtnDeleteScript_Click(object sender, EventArgs e)
        {
            IScript oSelectedScript = CBScriptTitle.SelectedItem as IScript;
            if (oSelectedScript != null)
            {
                moConfigs[oSelectedScript.GetType()].DeleteScript(oSelectedScript);
                int iIndex = CBScriptTitle.Items.IndexOf(oSelectedScript);
                
                CBScriptTitle.Items.Remove(oSelectedScript);
                moSelectedScript = null;

                if (CBScriptTitle.Items.Count == 0)
                {
                    IScript oNewScript = moConfigs[oSelectedScript.GetType()].CreateNewScript();
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
            if (moSelectedScript != null)
            {
                moSelectedScript.Title = CBScriptTitle.Text;
                if (oSelectedScriptInCombo == null)
                {
                    int iIndex = CBScriptTitle.Items.IndexOf(moSelectedScript);
                    CBScriptTitle.Items.RemoveAt(iIndex);
                    CBScriptTitle.Items.Insert(iIndex, moSelectedScript);
                }
            }
        }

        private void NppAwkPluginMainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (IFrontEndConfig oConfig in moConfigs.Values)
            {
                foreach (IScript oScript in oConfig.GetScripts())
                {
                    if (oScript.IsModified)
                    {
                        oConfig.WriteScript(oScript);
                    }
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
                        using (StreamReader oReader = new StreamReader(file))
                        {
                            if (sender == TB_Data)
                            {
                                TB_Data.Clear();
                                TB_Data.AppendText(oReader.ReadToEnd());
                            }
                            else if (sender == TB_ScriptAwk)
                            {
                                IScript oNewScript = null;
                                switch (Path.GetExtension(file).ToLower())
                                {
                                    case ".sh":
                                        oNewScript = moConfigs[typeof(ShScriptXml)].CreateNewScript();
                                        break;
                                    case ".awk":
                                        oNewScript = moConfigs[typeof(AwkScriptXml)].CreateNewScript();
                                        break;
                                    default:
                                        oNewScript = moConfigs[typeof(AwkScriptXml)].CreateNewScript();
                                        break;
                                }
                                oNewScript.Title = Path.GetFileName(file);
                                oNewScript.Script = oReader.ReadToEnd();
                                int iIndex = CBScriptTitle.Items.Add(oNewScript);
                                CBScriptTitle.SelectedIndex = iIndex;
                            }
                        }
					} catch (Exception ex) {
						MessageBox.Show(ex.Message);
					}
				}
        	}
        }

        private void RB_Sh_CheckedChanged(object sender, EventArgs e)
        {
            IScript selectedScript = moSelectedScript;
            if (RB_Sh.Checked && selectedScript != null && selectedScript.Type != ScriptType.Sh)
            {
                IFrontEndConfig oShConfig = moConfigs[typeof(ShScriptXml)];
                IFrontEndConfig oAwkConfig = moConfigs[typeof(AwkScriptXml)];
                oAwkConfig.DeleteScript(selectedScript);
                IScript convertScript = oShConfig.CopyScript(selectedScript);
                oShConfig.WriteScript(convertScript);
                int i = CBScriptTitle.SelectedIndex;
                CBScriptTitle.Items.RemoveAt(i);
                CBScriptTitle.Items.Insert(i, convertScript);
                CBScriptTitle.SelectedItem = convertScript;
            }
        }

        private void RB_Awk_CheckedChanged(object sender, EventArgs e)
        {
            IScript selectedScript = moSelectedScript;
            if (RB_Awk.Checked && selectedScript != null && selectedScript.Type != ScriptType.Awk && selectedScript.Type != ScriptType.Undefined)
            {
                IFrontEndConfig oShConfig = moConfigs[typeof(ShScriptXml)];
                IFrontEndConfig oAwkConfig = moConfigs[typeof(AwkScriptXml)];
                oShConfig.DeleteScript(selectedScript);
                IScript convertScript = oAwkConfig.CopyScript(selectedScript);
                oAwkConfig.WriteScript(convertScript);
                int i = CBScriptTitle.SelectedIndex;
                CBScriptTitle.Items.RemoveAt(i);
                CBScriptTitle.Items.Insert(i, convertScript);
                CBScriptTitle.SelectedItem = convertScript;
            }
        }

        private DiffForm moDiffForm = null;
        private void BtnDiff_Click(object sender, EventArgs e)
        {
            try
            {
                IFrontEnd oFrontEnd = moFrontEnds[typeof(DiffScript)];
                IFrontEndConfig oConfig = moConfigs[typeof(DiffScript)];
                oFrontEnd.Data.Clear();
                oFrontEnd.Data.Add(TB_Data.Text);
                oFrontEnd.Data.Add(TB_Result.Text);

                oFrontEnd.ExePath = oConfig.ProgramPath;
                oFrontEnd.TempDirectory = Path.Combine(oConfig.WorkingDirectory, "tmp");

                oFrontEnd.ExecScript(moConfigs);

                string sResult = oFrontEnd.Result;
                string sError = oFrontEnd.Error;
                if (sError.Trim() != string.Empty)
                {
                    MessageBox.Show(sError);
                }
                else
                {
                    if (moDiffForm == null)
                    {
                        moDiffForm = new DiffForm();
                    }
                    moDiffForm.ShowDiff(TB_Data.Text,TB_Result.Text,oFrontEnd.Result);
                    moDiffForm.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
        
    }
}