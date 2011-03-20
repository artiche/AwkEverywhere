using System;
using System.Collections.Generic;
using System.Text;
using AwkEverywhere.Config;

namespace AwkEverywhere.Frontend
{
    public interface IFrontEnd
    {
        void ExecScript();

        string Data { get; set;}

        IScript Script { get; set;}

        string Result { get;}

        string Error { get;}

        string ExePath { get; set;}

        string TempDirectory { get; set;}

        List<string> Args { get; }
    }
}
