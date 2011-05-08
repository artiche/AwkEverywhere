using System;
using System.Collections.Generic;
using System.Text;

namespace AwkEverywhere.Config
{
    public enum ScriptType
    {
        Undefined,Awk,Sh,Diff
    }

    public interface IScript
    {
        string Title { get; set;}
        string Script { get; set;}
        ScriptType Type { get; }
        int Id {get; set;}
        bool IsModified { get; set; }
        Nullable<DateTime> CreationDate {get; set;}
        Nullable<DateTime> ModificationDate {get; set;}
        string Author {get; set;}
        string Guid {get; set;}
        string Version {get; set;}
        string GenerateFinalScript(IFrontEndConfig config);
        IList<KeyValuePair<string, string>> GetDefines();
        IList<string> GetIncludes();
        IList<string> GetReferences();
    }
}
