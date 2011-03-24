using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using AwkEverywhere.helpers;

namespace AwkEverywhere.Frontend
{
    public class DiffFrontEnd : IFrontEnd
    {
        private IList<string> msData = null;
        private StringBuilder moResult = null;
        private StringBuilder moError = null;
        private List<string> moArgs = null;

        public DiffFrontEnd()
        {
            this.msData = new List<string>();
            moArgs = new List<string>();

        }

        #region IFrontEnd Membres

        public void ExecScript()
        {
            if (this.Data.Count != 2)
            {
                throw new ArgumentException("Diff needs exactly 2 data block (found "+this.Data.Count+")");
            }

            #region write temp files
            string sDataPath = Path.Combine(TempDirectory, "Diff.Data{0}.txt");
            
            if (!Directory.Exists(TempDirectory))
            {
                Directory.CreateDirectory(TempDirectory);
            }


            for (int i = 0; i < this.Data.Count; i++)
            {
                string sData = this.Data[i];
                string sFichPath = string.Format(sDataPath, (i + 1).ToString());
                using (StreamWriter oStreamData = new StreamWriter(sFichPath, false, Encoding.Default))
                {
                    oStreamData.Write(sData);
                    oStreamData.Flush();
                }
            }
            #endregion

            #region execute Diff process
            moResult = new StringBuilder();
            moError = new StringBuilder();

            //format Awk arguments
            StringBuilder oArgs = new StringBuilder();
            foreach (string sElt in Args)
            {
                oArgs.Append(sElt);
                oArgs.Append(" ");
            }

            StringBuilder oData1Path = new StringBuilder(260);
            Win32Helper.GetShortPathName(string.Format(sDataPath, 1), oData1Path, oData1Path.Capacity);

            StringBuilder oData2Path = new StringBuilder(260);
            Win32Helper.GetShortPathName(string.Format(sDataPath, 2), oData2Path, oData2Path.Capacity);

            System.Diagnostics.ProcessStartInfo oInfo = new System.Diagnostics.ProcessStartInfo(ExePath, string.Format("{0} -d {1} {2}", oArgs.ToString(), oData1Path.ToString(), oData2Path.ToString()));
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

        public void ExecScript(Dictionary<Type, AwkEverywhere.Config.IFrontEndConfig> references)
        {
            ExecScript();
        }

        public IList<string> Data
        {
            get
            {
                return msData;
            }
        }

        public AwkEverywhere.Config.IScript Script
        {
            get;
            set;
        }

        public string Result
        {
            get { return moResult.ToString(); }
        }

        public string Error
        {
            get { return moError.ToString(); }
        }

        public string ExePath
        {
            get;
            set;
        }

        public string TempDirectory
        {
            get;
            set;
        }

        public List<string> Args
        {
            get { return moArgs; }
        }

        #endregion

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
    }
}
