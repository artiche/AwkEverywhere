using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwkEverywhere.Config;
using AwkEverywhere.helpers;
using System.IO;

namespace AwkEverywhere.Frontend
{
    class ShFrontEnd : IFrontEnd
    {
        public ShFrontEnd(IFrontEndConfig config)
        {
            mConfig = config;
            this.Data = new List<string>();
        }

        #region IFrontEnd Membres

        public void ExecScript(Dictionary<Type, IFrontEndConfig> referencesConfig)
        {

       
            #region write temp files
            string sNppAwkPluginDataPath = Path.Combine(TempDirectory, this.Script.Title + ".Data{0}.txt");
            string sNppAwkPluginScriptPath = Path.Combine(TempDirectory, this.Script.Title + ".sh");

            if (!Directory.Exists(TempDirectory))
            {
                Directory.CreateDirectory(TempDirectory);
            }


            for (int i = 0; i < this.Data.Count; i++)
            {
                string sData = this.Data[i];
                string sFichPath = string.Format(sNppAwkPluginDataPath, i == 0 ? string.Empty : (i + 1).ToString());
                using (StreamWriter oStreamData = new StreamWriter(sFichPath, false, Encoding.Default))
                {
                    oStreamData.Write(Data);
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
                                using (StreamWriter writer = new StreamWriter(Path.Combine(TempDirectory, s.Title + "." + s.Type.ToString().ToLower()), false))
                                {
                                    writer.Write(s.GenerateFinalScript(mConfig));
                                    writer.Flush();
                                }
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException("Unknown reference type");
                }
            }
            #endregion

            #region execute Awk process
            moResult = new StringBuilder();
            moError = new StringBuilder();

            StringBuilder oScriptPath = new StringBuilder(260);
            Win32Helper.GetShortPathName(sNppAwkPluginScriptPath, oScriptPath, oScriptPath.Capacity);

            System.Diagnostics.ProcessStartInfo oInfo = new System.Diagnostics.ProcessStartInfo(ExePath, oScriptPath.ToString());
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

        private IFrontEndConfig mConfig;
        private StringBuilder moResult = null;
        private StringBuilder moError = null;
        private List<string> moArgs = new List<string>();

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

        public IList<string> Data { get; set; }

        public AwkEverywhere.Config.IScript Script { get; set; }

        public string Result
        {
            get { return moResult.ToString(); }
        }

        public string Error
        {
            get { return moError.ToString(); }
        }

        public string ExePath { get; set; }

        public string TempDirectory { get; set; }

        public List<string> Args
        {
            get { return moArgs; }
        }

        #endregion
    }
}
