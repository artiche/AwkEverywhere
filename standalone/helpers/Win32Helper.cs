using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace AwkEverywhere.helpers
{
    public static class Win32Helper
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        public static extern int GetShortPathName(string LongPath, StringBuilder ShortPath, int BufferSize);

    }
}
