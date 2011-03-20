using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

using AwkEverywhere.Config;

namespace AwkEverywhere
{
    public class AwkScriptXml : IScript
    {
        public AwkScriptXml() {
            Title = string.Empty;
            Script = string.Empty;
            Id = 0;
        }

        private string msTitle;
        [XmlElement(typeof(string))]
        public string Title
        {
            get { return msTitle; }
            set {
                IsModified |= (msTitle != value);
                msTitle = value; 
            }
        }


        private string msScript;
        [XmlElement(typeof(string))]
        public string Script
        {
            get { return msScript; }
            set {
                IsModified |= (msScript != value);
                msScript = value; 
            }
        }

        private int miId;
        [XmlElement(typeof(int))]
        public int Id
        {
            get { return miId; }
            set {
                IsModified |= (miId != value);
                miId = value; 
            }
        }

        private bool mbIsModified = false;
        public bool IsModified
        {
            get { return mbIsModified; }
            set { mbIsModified = value; }
        }

        
        private Nullable<DateTime> mdCreationDate = DateTime.Now;
    	public Nullable<DateTime> CreationDate {
			get {return mdCreationDate;}
			set {mdCreationDate = value;}
		}
    	
        private Nullable<DateTime> mdModificationDate;
		public Nullable<DateTime> ModificationDate {
			get {return mdModificationDate;}
			set {mdModificationDate = value;}
		}
    	
        private string msAuthor;
		public string Author {
			get {return msAuthor;}
			set {msAuthor = value;}
		}
    	
        private string msGuid;
		public string Guid {
			get {return msGuid;}
			set {msGuid = value;}
		}
    	
        private string msVersion;
		public string Version {
			get {return msVersion;}
			set {msVersion = value;}
		}
        
        public override string ToString()
        {
            return Title;
        }
    	
		
    }
}
