using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwkEverywhere.Config;
using System.IO;
using System.Configuration;
using System.Xml;
using System.Xml.Serialization;

namespace AwkEverywhere
{
    public class DiffConfig : AbstractConfig<DiffScript>
    {
        private const string STATIC_CONFIG_FILE_NAME = "DiffConfig.xml";


        protected override string CONFIG_FILE_NAME
        {
            get { return STATIC_CONFIG_FILE_NAME; }
        }


        public DiffConfig()
            : base(ScriptType.Diff)
        {
        }

        public static DiffConfig GetInstance()
        {
            DiffConfig instance = null;

            string sConfigFilePath = Path.Combine(StaticWorkingDirectory, STATIC_CONFIG_FILE_NAME);
            if (!File.Exists(sConfigFilePath))
            {
                instance = new DiffConfig();
                if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[AppSettingsKey.DIFF_PATH_KEY]))
                {
                    instance.ProgramPath = ConfigurationManager.AppSettings[AppSettingsKey.DIFF_PATH_KEY];
                }
                else
                {
                    instance.ProgramPath = "diff.exe";
                }
                instance.Version = System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();
                instance.WriteConfig();
            }
            else
            {
                XmlSerializer oSerializer = new XmlSerializer(typeof(DiffConfig));
                using (XmlTextReader oReader = new XmlTextReader(sConfigFilePath))
                {
                    instance = (DiffConfig)oSerializer.Deserialize(oReader);
                    if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings[AppSettingsKey.DIFF_PATH_KEY]))
                    {
                        instance.ProgramPath = ConfigurationManager.AppSettings[AppSettingsKey.DIFF_PATH_KEY];
                    }
                }
            }
            return instance;
        }

    }
}
