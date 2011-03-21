using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;

using AwkEverywhere.Config;
namespace AwkEverywhere
{
    public class AwkConfig : AbstractConfig<AwkScriptXml>
    {
        private const string STATIC_CONFIG_FILE_NAME = "AwkConfig.xml";

        protected override string CONFIG_FILE_NAME
        {
            get { return STATIC_CONFIG_FILE_NAME; }
        }


        /// <summary>Constructor</summary>
        public AwkConfig():base(ScriptType.Undefined,ScriptType.Awk)
        {
            
        }


        

        public static AwkConfig GetInstance()
        {
            AwkConfig instance = null;

            string sConfigFilePath = Path.Combine(StaticWorkingDirectory, STATIC_CONFIG_FILE_NAME);
            if (!File.Exists(sConfigFilePath))
            {
                instance = new AwkConfig();
                instance.ProgramPath = "gawk.exe";
                instance.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                instance.WriteConfig();
            }
            else
            {
                XmlSerializer oSerializer = new XmlSerializer(typeof(AwkConfig));
                using (XmlTextReader oReader = new XmlTextReader(sConfigFilePath))
                {
                    instance = (AwkConfig)oSerializer.Deserialize(oReader);
                }
            }
            return instance;
        }


        


        


        

        

       


        
    }
}
