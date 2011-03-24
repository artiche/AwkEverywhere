using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwkEverywhere.Config;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Configuration;

namespace AwkEverywhere
{
    public class ShConfig : AbstractConfig<ShScriptXml>
    {
        private const string STATIC_CONFIG_FILE_NAME = "ShConfig.xml";
        

        protected override string CONFIG_FILE_NAME
        {
            get { return STATIC_CONFIG_FILE_NAME; }
        }


        /// <summary>Constructor</summary>
        public ShConfig():base(ScriptType.Sh)
        {
            
        }

        

        public static ShConfig GetInstance()
        {
            ShConfig instance = null;

            string sConfigFilePath = Path.Combine(StaticWorkingDirectory, STATIC_CONFIG_FILE_NAME);
            if (!File.Exists(sConfigFilePath))
            {
                instance = new ShConfig();
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[AppSettingsKey.SH_PATH_KEY]))
                {
                    instance.ProgramPath = ConfigurationManager.AppSettings[AppSettingsKey.SH_PATH_KEY];
                }
                else
                {
                    instance.ProgramPath = "sh.exe";
                }
                instance.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                instance.WriteConfig();
            }
            else
            {
                XmlSerializer oSerializer = new XmlSerializer(typeof(ShConfig));
                using (XmlTextReader oReader = new XmlTextReader(sConfigFilePath))
                {
                    instance = (ShConfig)oSerializer.Deserialize(oReader);
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[AppSettingsKey.SH_PATH_KEY]))
                    {
                        instance.ProgramPath = ConfigurationManager.AppSettings[AppSettingsKey.SH_PATH_KEY];
                    }
                }
            }
            return instance;
        }


    }
}
