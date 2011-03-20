using System;
using System.Collections.Generic;
using System.Text;

namespace AwkEverywhere.Config
{
    public interface IFrontEndConfig
    {
        string ProgramPath { get; set;}
        string PluginWorkingDirectory { get; }
        void WriteConfig();
        List<IScript> GetScripts();
        IScript GetScript(string title);
        void WriteScript(IScript oScript);
        void DeleteScript(IScript oScript);
        IScript CreateNewScript();
        int SelectedScriptId { get; set;}
    }
}
