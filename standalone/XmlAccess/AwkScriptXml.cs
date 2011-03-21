using System;
using System.Collections.Generic;
using System.Text;
using System.Xml.Serialization;
using System.Xml;

using AwkEverywhere.Config;
using AwkEverywhere.helpers;
using System.IO;

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

        public ScriptType Type
        {
            get { return ScriptType.Awk; }
        }


        public override string ToString()
        {
            return Title;
        }



        private IList<string> mIncludes = null;
        public IList<string> GetIncludes()
        {
            if (this.mIncludes == null || this.IsModified)
            {
                mIncludes = ScriptHelper.ParseIncludes(this.Script);
            }
            return mIncludes;
        }

        private IList<string> mReferences = null;
        public IList<string> GetReferences()
        {
            if (this.mReferences == null || this.IsModified)
            {
                mReferences = ScriptHelper.ParseReferences(this.Script);
            }
            return mReferences;
        }

        /// <summary>
        /// generate the final script (body + includes)
        /// </summary>
        /// <param name="config"></param>
        /// <returns></returns>
        public string GenerateFinalScript(IFrontEndConfig config)
        {
            StringBuilder finalScript = new StringBuilder();
            GenerateFinalScript(config, new List<string>(), finalScript);
            return finalScript.ToString();
        }


        private void GenerateFinalScript(IFrontEndConfig config, List<string> alreadyIncluded, StringBuilder finalScript)
        {
            foreach (string include in this.GetIncludes())
            {
                if (alreadyIncluded.Contains(include.ToUpper()))
                {
                    continue;
                }
                else
                {
                    alreadyIncluded.Add(include.ToUpper());
                }

                string[] tab = include.Split('|');
                string includeType = "FILE";
                string includeValue = include;
                if (tab.Length == 2)
                {
                    includeType = tab[0].ToUpper();
                    includeValue = tab[1];
                }

                

                switch (includeType)
                {
                    case "FILE":
                        {
                            using (StreamReader reader = new StreamReader(includeValue))
                            {
                                finalScript.Append(reader.ReadToEnd());
                                finalScript.Append(Environment.NewLine);
                            }
                        }
                        break;
                    case "SCRIPT":
                        {
                            AwkScriptXml s = config.GetScript(includeValue) as AwkScriptXml;
                            if (s != null)
                            {
                                s.GenerateFinalScript(config, alreadyIncluded, finalScript);
                                finalScript.Append(Environment.NewLine);
                            }
                        }
                        break;
                    default:
                        throw new NotImplementedException("Unknown include type");
                }
            }

            finalScript.Append(this.Script);
        }

    }
}
