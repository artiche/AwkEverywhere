using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AwkEverywhere.Config;

namespace AwkEverywhere
{
    /// <summary>
    /// Dummy class : diff doesn't need a script.
    /// </summary>
    public class DiffScript : IScript
    {
        #region IScript Membres

        public string Title
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Script
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public ScriptType Type
        {
            get { throw new NotImplementedException(); }
        }

        public int Id
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsModified
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime? CreationDate
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public DateTime? ModificationDate
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Author
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Guid
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string Version
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public string GenerateFinalScript(IFrontEndConfig config)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetIncludes()
        {
            throw new NotImplementedException();
        }

        public IList<string> GetReferences()
        {
            throw new NotImplementedException();
        }

        public IList<KeyValuePair<string, string>> GetDefines()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
