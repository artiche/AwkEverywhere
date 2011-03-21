using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwkEverywhere.Config;
using System.Xml.Serialization;
using System.IO;
using System.Xml;

namespace AwkEverywhere
{
    public abstract class AbstractConfig<T> : IFrontEndConfig where T  : IScript
    {
        private List<ScriptType> mScriptTypes;
        public AbstractConfig(params ScriptType[] scriptTypes)
        {
            mScriptTypes = new List<ScriptType>(scriptTypes);
        }

        protected static string moWorkingDirectory = null;

        protected virtual string CONFIG_FILE_NAME { get { return string.Empty; } }

        private List<IScript> moListeScripts = null;

        [XmlElement(typeof(string))]
        public string Version;

        [XmlElement(typeof(string))]
        public string ProgramPath { get; set; }

        [XmlElement(typeof(int))]
        public int SelectedScriptId { get; set; }

        public static string StaticWorkingDirectory
        {
            get
            {
                if (moWorkingDirectory == null)
                {
                    Microsoft.Win32.RegistryKey okey = Microsoft.Win32.Registry.CurrentUser.OpenSubKey(@"Software\Microsoft\Windows\CurrentVersion\Explorer\Shell Folders");
                    string sUserAppDataPath = okey.GetValue("AppData") as string;
                    moWorkingDirectory = Path.Combine(sUserAppDataPath, "AwkEverywhere");
                    if (!Directory.Exists(moWorkingDirectory))
                    {
                        Directory.CreateDirectory(moWorkingDirectory);
                    }
                }
                return moWorkingDirectory;
            }
        }

        public string WorkingDirectory
        {
            get { return StaticWorkingDirectory; }
        }

        /// <summary>write plugin configuration</summary>
        /// <param name="oConfig">plugin config</param>
        public virtual void WriteConfig()
        {
            XmlSerializer oSerializer = new XmlSerializer(this.GetType());
            XmlTextWriter oWriter = new XmlTextWriter(Path.Combine(WorkingDirectory, CONFIG_FILE_NAME), Encoding.UTF8);
            try
            {
                oSerializer.Serialize(oWriter, this);
            }
            catch (Exception ex)
            {
                Exception oInnerEx = ex;
                StringBuilder oMsg = new StringBuilder();
                while (oInnerEx != null)
                {
                    oMsg.Append(oInnerEx.Message + "\n" + oInnerEx.StackTrace + "\n-----\n");
                    oInnerEx = oInnerEx.InnerException;
                }
                System.Windows.Forms.MessageBox.Show(oMsg.ToString());
            }
            finally
            {
                oWriter.Close();
            }
        }

        /// <summary>
        /// get a script by its id
        /// </summary>
        /// <param name="id">script id</param>
        /// <returns>script</returns>
        public IScript GetScript(string title, ScriptType type)
        {
            foreach (IScript s in moListeScripts)
            {
                if ((s.Type == type || type == ScriptType.Undefined)
                    && s.Title == title)
                {
                    return s;
                }
            }
            return null;
        }

        /// <summary>Read awk scripts</summary>
        /// <returns>list of scripts</returns>
        public List<IScript> GetScripts()
        {
            if (moListeScripts == null)
            {
                moListeScripts = new List<IScript>();
                XmlSerializer oSerializer = new XmlSerializer(typeof(T));
                #region load scripts
                foreach (ScriptType st in this.mScriptTypes)
                {
                    string pattern = "*." + st.ToString() + ".xml";
                    if (st == ScriptType.Undefined)
                    {
                        pattern = "*.script.xml";
                    }

                    foreach (string sFile in Directory.GetFiles(WorkingDirectory, pattern))
                    {
                        XmlTextReader oReader = new XmlTextReader(sFile);
                        try
                        {
                            IScript oScript = oSerializer.Deserialize(oReader) as IScript;
                            moListeScripts.Add(oScript);
                        }
                        finally
                        {
                            oReader.Close();
                        }
                    }
                }

                foreach (IScript elt in moListeScripts)
                {
                    giMaxId = Math.Max(giMaxId, elt.Id);
                }
                #endregion

                foreach (IScript oScript in moListeScripts)
                {
                    oScript.IsModified = false;
                }
            }
            return moListeScripts;
        }




        public void WriteScript(IScript oScript)
        {
            XmlSerializer oSerializer = new XmlSerializer(typeof(T));
            XmlTextWriter oWriter = new XmlTextWriter(Path.Combine(WorkingDirectory, string.Format("script{0:0000}." + oScript.Type.ToString() + ".xml", oScript.Id)), Encoding.UTF8);
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
        private static int giMaxId = 0;

        public IScript CreateNewScript()
        {
            //create new script
            int iIdScript = ++giMaxId;

            IScript oNewScript = typeof(T).GetConstructor(Type.EmptyTypes).Invoke(new object[0]) as IScript;

            oNewScript.Title = string.Format("Untitled {0}", iIdScript);
            oNewScript.Id = iIdScript;
            oNewScript.IsModified = false;

            moListeScripts.Add(oNewScript);

            return oNewScript;
        }

        public IScript CopyScript(IScript script)
        {
            IScript newScript = this.CreateNewScript();
            newScript.Author = script.Author;
            newScript.CreationDate = script.CreationDate;
            newScript.Guid = script.Guid;
            newScript.ModificationDate = script.ModificationDate;
            newScript.Script = script.Script;
            newScript.Title = script.Title;
            newScript.Version = script.Version;

            return newScript;
        }

        public void DeleteScript(IScript oScript)
        {
            File.Delete(Path.Combine(WorkingDirectory, string.Format("script{0:000}.script.xml", oScript.Id)));
            moListeScripts.Remove(oScript);
        }
    }
}
