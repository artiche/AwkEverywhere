using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using AwkEverywhere.Config;
using AwkEverywhere.helpers;

namespace AwkEverywhere.Frontend
{
    class AwkFrontEnd : IFrontEnd
    {
        private IList<string> msData = new List<string>();
        private IScript msScript = null;
        private StringBuilder moResult = null;
        private StringBuilder moError = null;
        private string msExePath = null;
        private string msTempDirectory = null;
        private List<string> moArgs = new List<string>();

        private IFrontEndConfig mConfig = null;

        public AwkFrontEnd(IFrontEndConfig config)
        {
            this.mConfig = config;  
        }

        #region IFrontEnd Membres

        /// <summary>
        /// Execute Awk 
        /// </summary>
        public void ExecScript(Dictionary<Type, IFrontEndConfig> referencesConfig)
        {


            #region write temp files
            string sNppAwkPluginDataPath = Path.Combine(TempDirectory, this.Script.Title+".Data{0}.txt");
            string sNppAwkPluginScriptPath = Path.Combine(TempDirectory, this.Script.Title+".awk");

            if (!Directory.Exists(TempDirectory))
            {
                Directory.CreateDirectory(TempDirectory);
            }

            
            for (int i=0 ; i<this.Data.Count ; i++)
            {
                string sData = this.Data[i];
                string sFichPath = string.Format(sNppAwkPluginDataPath, i == 0 ? string.Empty : (i + 1).ToString());
                using (StreamWriter oStreamData = new StreamWriter(sFichPath, false, Encoding.Default))
                {
                    oStreamData.Write(sData);
                    oStreamData.Flush();
                }
            }

            string finalScript = this.Script.GenerateFinalScript(mConfig);
            using (StreamWriter oStreamScript = new StreamWriter(sNppAwkPluginScriptPath, false, Encoding.Default))
            {
                oStreamScript.Write(finalScript);
                oStreamScript.Flush();
            }

            foreach (string reference in ScriptHelper.ParseReferences(finalScript))
            {
                //copy all references in the temp directory
                string[] tab = reference.Split('|');
                string referenceType = "FILE";
                string referenceValue = reference;
                if (tab.Length == 2)
                {
                    referenceType = tab[0].ToUpper();
                    referenceValue = tab[1];
                }

                switch (referenceType)
                {
                    case "FILE":
                        {
                            if (File.Exists(referenceValue))
                            {
                                File.Copy(referenceValue, Path.Combine(TempDirectory, Path.GetFileName(referenceValue)));
                            }
                        }
                        break;
                    case "SCRIPT":
                        {
                            IScript s = mConfig.GetScript(referenceValue);
                            if (s == null)
                            {
                                foreach (IFrontEndConfig c in referencesConfig.Values)
                                {
                                    s = c.GetScript(referenceValue);
                                    if (s != null)
                                    {
                                        break;
                                    }
                                }
                            }

                            if (s != null)
                            {
                                using (StreamWriter writer = new StreamWriter(Path.Combine(TempDirectory, s.Title + "." + s.Type.ToString().ToLower()),false))
                                {
                                    writer.Write(s.GenerateFinalScript(mConfig));
                                    writer.Flush();
                                }
                            }
                        }
                        break;
                    default :
                        throw new NotImplementedException("Unknown reference type");
                }
            }
            #endregion

            #region execute Awk process
            moResult = new StringBuilder();
            moError = new StringBuilder();

            //format Awk arguments
            StringBuilder oArgs = new StringBuilder();
            foreach (string sElt in Args)
            {
                oArgs.Append(sElt);
                oArgs.Append(" ");
            }

            StringBuilder oScriptPath = new StringBuilder(260);
            Win32Helper.GetShortPathName(sNppAwkPluginScriptPath, oScriptPath, oScriptPath.Capacity);

            StringBuilder oDataPaths = new StringBuilder();
            for (int i = 0; i < this.Data.Count; i++)
            {
                StringBuilder oDataPath = new StringBuilder(260);
                Win32Helper.GetShortPathName(string.Format(sNppAwkPluginDataPath, i == 0 ? string.Empty : (i + 1).ToString()), oDataPath, oDataPath.Capacity);
                oDataPaths.Append(oDataPath.ToString());
                if (i < this.Data.Count - 1)
                {
                    oDataPaths.Append(" ");
                }
            }

                
            System.Diagnostics.ProcessStartInfo oInfo = new System.Diagnostics.ProcessStartInfo(ExePath, string.Format("--posix {0} -f {1} {2}", oArgs.ToString(), oScriptPath.ToString(), oDataPaths.ToString()));
            oInfo.UseShellExecute = false;
            oInfo.RedirectStandardOutput = true;
            oInfo.RedirectStandardError = true;
            oInfo.CreateNoWindow = true;
            oInfo.WorkingDirectory = TempDirectory;

            System.Diagnostics.Process oProcess = new System.Diagnostics.Process();
            oProcess.StartInfo = oInfo;
            oProcess.OutputDataReceived += new System.Diagnostics.DataReceivedEventHandler(oProcess_OutputDataReceived);
            oProcess.ErrorDataReceived += new System.Diagnostics.DataReceivedEventHandler(oProcess_ErrorDataReceived);

            oProcess.Start();
            try
            {
                oProcess.BeginOutputReadLine();
                oProcess.BeginErrorReadLine();
                oProcess.WaitForExit();
            }
            finally
            {
                oProcess.Close();
            }
            #endregion
        }

        

        /// <summary>
        /// write standard error in moError
        /// </summary>
        /// <param name="sender">Awk process</param>
        /// <param name="e">output line</param>
        private void oProcess_ErrorDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            moError.Append(e.Data);
            moError.Append(Environment.NewLine);
        }

        /// <summary>
        /// write standard output in moResult
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void oProcess_OutputDataReceived(object sender, System.Diagnostics.DataReceivedEventArgs e)
        {
            moResult.Append(e.Data);
            moResult.Append(Environment.NewLine);
        }

        /// <summary>
        /// input data
        /// </summary>
        public IList<string> Data
        {
            get { return msData; }
        }

        /// <summary>
        /// input script
        /// </summary>
        public IScript Script
        {
            get { return msScript; }
            set { msScript = value; }
        }

        /// <summary>
        /// result of script execution
        /// </summary>
        public string Result
        {
            get { return moResult.ToString(); }
        }

        /// <summary>
        /// errors of script execution
        /// </summary>
        public string Error
        {
            get { return moError.ToString(); }
        }

        /// <summary>
        /// Path of Awk exe
        /// </summary>
        public string ExePath
        {
            get { return msExePath; }
            set { msExePath = value; }
        }

        /// <summary>
        /// temp directory
        /// </summary>
        public string TempDirectory
        {
            get { return msTempDirectory; }
            set { msTempDirectory = value; }
        }

        /// <summary>
        /// arguments of Awk process
        /// </summary>
        public List<string> Args
        {
            get { return moArgs; }
        }

        #endregion
    }
    
    
}
