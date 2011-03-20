using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

using AwkEverywhere.Config;
namespace AwkEverywhere
{
    class NppAwkPluginConfig : IFrontEndConfig
    {
        private const string NPP_AWK_PLUGIN_CONFIG_FILE = "NppAwkPluginConfig.xml";

        private NppAwkPluginConfigXml moNppConfig = null;
        private string moPluginWorkingDirectory = null;
        private List<IScript> moListeScripts = null;


        /// <summary>Constructor</summary>
        public NppAwkPluginConfig()
        {
            string sConfigFilePath = Path.Combine(PluginWorkingDirectory, NPP_AWK_PLUGIN_CONFIG_FILE);
            if (!File.Exists(sConfigFilePath))
            {
                moNppConfig = new NppAwkPluginConfigXml();
                moNppConfig.AwkPath = "gawk.exe";
                moNppConfig.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

                WriteConfig();
            }
            else
            {
                XmlSerializer oSerializer = new XmlSerializer(typeof(NppAwkPluginConfigXml));
                XmlTextReader oReader = new XmlTextReader(sConfigFilePath);
                try
                {
                    moNppConfig = (NppAwkPluginConfigXml)oSerializer.Deserialize(oReader);
                }
                finally
                {
                    oReader.Close();
                }
            }
        }


        public string PluginWorkingDirectory
        {
            get
            {
                if (moPluginWorkingDirectory == null)
                {
                    Microsoft.Win32.RegistryKey okey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders");
                    string sUserAppDataPath = okey.GetValue("AppData") as string;
                    moPluginWorkingDirectory = Path.Combine(sUserAppDataPath, "AwkEverywhere");
                    if(!Directory.Exists(moPluginWorkingDirectory)){
                    	Directory.CreateDirectory(moPluginWorkingDirectory);
                    }
                }
                return moPluginWorkingDirectory;
            }
            
        }


        


        /// <summary>write plugin configuration</summary>
        /// <param name="oConfig">plugin config</param>
        public void WriteConfig()
        {
            if (moNppConfig != null)
            {
                XmlSerializer oSerializer = new XmlSerializer(typeof(NppAwkPluginConfigXml));
                XmlTextWriter oWriter = new XmlTextWriter(Path.Combine(PluginWorkingDirectory, NPP_AWK_PLUGIN_CONFIG_FILE),Encoding.UTF8);
                try
                {
                    oSerializer.Serialize(oWriter, moNppConfig);
                }
                catch (Exception ex)
                {
                	Exception oInnerEx = ex;
                	StringBuilder oMsg = new StringBuilder();
                	while(oInnerEx!=null)
                	{
                		oMsg.Append(oInnerEx.Message+"\n"+oInnerEx.StackTrace+"\n-----\n");
                		oInnerEx = oInnerEx.InnerException;
                	}
                	System.Windows.Forms.MessageBox.Show(oMsg.ToString());
                }
                finally
                {
                    oWriter.Close();
                }
            }
        }

        /// <summary>Read awk scripts</summary>
        /// <returns>list of scripts</returns>
        public List<IScript> GetScripts()
        {
            if (moListeScripts == null)
            {
                moListeScripts = new List<IScript>();
                XmlSerializer oSerializer = new XmlSerializer(typeof(AwkScriptXml));
                foreach (string sFile in Directory.GetFiles(PluginWorkingDirectory, "*.script.xml"))
                {
                    XmlTextReader oReader = new XmlTextReader(sFile);
                    try
                    {
                        AwkScriptXml oScript = oSerializer.Deserialize(oReader) as AwkScriptXml;
                        moListeScripts.Add(oScript);
                    }
                    finally
                    {
                        oReader.Close();
                    }
                }
                foreach (IScript oScript in moListeScripts)
                {
                    oScript.IsModified = false;
                }
            }
            return moListeScripts;
        }


        public void WriteScript(IScript oScript)
        {
            XmlSerializer oSerializer = new XmlSerializer(typeof(AwkScriptXml));
            XmlTextWriter oWriter = new XmlTextWriter(Path.Combine(PluginWorkingDirectory, string.Format("script{0:000}.script.xml", oScript.Id)),Encoding.UTF8);
            try
            {
                oSerializer.Serialize(oWriter, oScript);
                oScript.IsModified = false;
            }
            finally
            {
                oWriter.Close();
            }
        }

        public IScript CreateNewScript()
        {
            //create new script
            int iIdScript = 0;
            foreach (AwkScriptXml oItem in moListeScripts)
            {
                iIdScript = Math.Max(iIdScript, oItem.Id);
            }
            iIdScript++;
            AwkScriptXml oNewScript = new AwkScriptXml();
            oNewScript.Title = string.Format("Untitled {0}", iIdScript);
            oNewScript.Id = iIdScript;
            oNewScript.IsModified = false;

            moListeScripts.Add(oNewScript);
            
            return oNewScript;
        }

        public void DeleteScript(IScript oScript)
        {
            File.Delete(Path.Combine(PluginWorkingDirectory, string.Format("script{0:000}.script.xml", oScript.Id)));
            moListeScripts.Remove(oScript);
        }


        public string ProgramPath
        {
            get
            {
                return moNppConfig.AwkPath;
            }
            set
            {
                moNppConfig.AwkPath = value;
            }
        }

        public int SelectedScriptId {
            get { return moNppConfig.SelectedScriptId; }
            set { moNppConfig.SelectedScriptId = value; }
        }


        
    }
}
