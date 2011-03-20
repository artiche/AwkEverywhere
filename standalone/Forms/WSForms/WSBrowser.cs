using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using AwkEverywhere.Config;

namespace AwkEverywhere.Forms.WSForms
{
    public partial class WSBrowser : Form
    {
    	private IFrontEndConfig moConfig;
    	
        public WSBrowser(IFrontEndConfig oConfig)
        {
        	moConfig = oConfig;
            InitializeComponent();
        }
        
        void WSBrowserLoad(object sender, EventArgs e)
        {
        	List<IScript> oListeScripts = moConfig.GetScripts();
        	
        	LB_LocalScripts.Items.AddRange(oListeScripts.ToArray());
        }
        
        void GroupBox1Enter(object sender, EventArgs e)
        {
        	
        }
        
        void LB_LocalScriptsSelectedIndexChanged(object sender, EventArgs e)
        {
        	IScript oSelectedScript = LB_LocalScripts.SelectedValue as IScript;
        	if(oSelectedScript!=null){
        	  LabelLocalAuthor.Text = oSelectedScript.Author;
        	  LabelLocalCreationDate.Text = oSelectedScript.CreationDate.HasValue?oSelectedScript.CreationDate.ToString():string.Empty;
        	  LabelLocalGuid.Text = oSelectedScript.Guid;
        	  LabelLocalModifDate.Text = oSelectedScript.ModificationDate.HasValue?oSelectedScript.ModificationDate.ToString():string.Empty;
        	  LabelLocalVersion.Text = oSelectedScript.Version;
        	}
        }
    }
}