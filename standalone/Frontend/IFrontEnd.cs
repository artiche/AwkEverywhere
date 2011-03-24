using System;
using System.Collections.Generic;
using System.Text;
using AwkEverywhere.Config;

namespace AwkEverywhere.Frontend
{
    public interface IFrontEnd
    {
        void ExecScript(Dictionary<Type, IFrontEndConfig> references);

        IList<string> Data { get;}

        IScript Script { get; set;}

        string Result { get;}

        string Error { get;}

        string ExePath { get; set;}

        string TempDirectory { get; set;}

        List<string> Args { get; }
    }
}
