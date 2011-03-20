using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;

namespace AwkEverywhere
{
    
    public class NppAwkPluginConfigXml
    {
        [XmlElement(typeof(string))]
        public string Version;

        [XmlElement(typeof(string))]
        public string AwkPath;

        [XmlElement(typeof(int))]
        public int SelectedScriptId;

    }
}
