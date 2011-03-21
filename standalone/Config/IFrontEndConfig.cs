using System;
using System.Collections.Generic;
using System.Text;

namespace AwkEverywhere.Config
{
    public interface IFrontEndConfig
    {
        string ProgramPath { get; set; }
        string WorkingDirectory { get; }
        void WriteConfig();
        List<IScript> GetScripts();
        IScript GetScript(string title);
        void WriteScript(IScript oScript);
        void DeleteScript(IScript oScript);
        IScript CreateNewScript();
        IScript CopyScript(IScript script);
        int SelectedScriptId { get; set;}

    }
}
