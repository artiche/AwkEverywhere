using System;
using System.Collections.Generic;
using System.Text;

namespace AwkEverywhere.Config
{
    public interface IScript
    {
        string Title { get; set;}
        string Script { get; set;}
        int Id {get; set;}
        bool IsModified { get; set; }
        Nullable<DateTime> CreationDate {get; set;}
        Nullable<DateTime> ModificationDate {get; set;}
        string Author {get; set;}
        string Guid {get; set;}
        string Version {get; set;}
        string GenerateFinalScript(IFrontEndConfig config);
        IList<string> GetIncludes();
        IList<string> GetReferences();
    }
}
