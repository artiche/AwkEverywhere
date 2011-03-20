using System;
using System.IO;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;

namespace AwkEverywhere.Frontend
{
    class AwkFrontEnd : IFrontEnd
    {
        private string msData = null;
        private string msScript = null;
        private StringBuilder moResult = null;
        private StringBuilder moError = null;
        private string msExePath = null;
        private string msTempDirectory = null;
        private List<string> moArgs = new List<string>();

        
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        private static extern int GetShortPathName(string LongPath, StringBuilder ShortPath, int BufferSize);

        #region IFrontEnd Membres

        /// <summary>
        /// Execute Awk 
        /// </summary>
        public void ExecScript()
        {
            

            #region write temp files
            string sNppAwkPluginDataPath = Path.Combine(TempDirectory, "NppAwkPluginData.txt");
            string sNppAwkPluginScriptPath = Path.Combine(TempDirectory, "NppAwkPluginScript.txt");

            if (!Directory.Exists(TempDirectory))
            {
                Directory.CreateDirectory(TempDirectory);
            }
            
            StreamWriter oStreamData = new StreamWriter(sNppAwkPluginDataPath, false, Encoding.Default);
            try
            {
                oStreamData.Write(Data);
                oStreamData.Flush();
            }
            finally
            {
                oStreamData.Close();
            }

            StreamWriter oStreamScript = new StreamWriter(sNppAwkPluginScriptPath, false, Encoding.Default);
            try
            {
                oStreamScript.Write(Script);
                oStreamScript.Flush();
            }
            finally
            {
                oStreamScript.Close();
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
            GetShortPathName(sNppAwkPluginScriptPath, oScriptPath, oScriptPath.Capacity);

            StringBuilder oDataPath = new StringBuilder(260);
            GetShortPathName(sNppAwkPluginDataPath, oDataPath, oDataPath.Capacity);

            System.Diagnostics.ProcessStartInfo oInfo = new System.Diagnostics.ProcessStartInfo(ExePath, string.Format("{0} -f {1} {2}", oArgs.ToString(), oScriptPath.ToString(), oDataPath.ToString()));
            oInfo.UseShellExecute = false;
            oInfo.RedirectStandardOutput = true;
            oInfo.RedirectStandardError = true;
            oInfo.CreateNoWindow = true;

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
        public string Data
        {
            get { return msData; }
            set { msData = value; }
        }

        /// <summary>
        /// input script
        /// </summary>
        public string Script
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
