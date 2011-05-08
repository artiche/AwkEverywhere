using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AwkEverywhere.helpers
{
    public static class ScriptHelper
    {

        private static Regex gIncludePattern = new Regex("\".*\"");
        private static Regex gReferencePattern = new Regex("\".*\"");

        public static IList<KeyValuePair<string, string>> ParseDefines(string script)
        {
            IList<KeyValuePair<string, string>> list = new List<KeyValuePair<string, string>>();
            foreach (string sLine in script.Split('\n'))
            {
                if (sLine.ToLower().Trim().StartsWith("#@define ") && sLine.IndexOf('=')>0)
                {
                    string pair = sLine.Substring(9);
                    string[] pairTab = pair.Split('=');
                    string key = pairTab[0].Trim();
                    string value = pairTab[1].Trim();
                    list.Add(new KeyValuePair<string,string>(key,value));
                }
            }
            return list;
        }

        public static IList<string> ParseIncludes(string script)
        {
            List<string> list = new List<string>();
            foreach (string sLine in script.Split('\n'))
            {
                if (sLine.ToLower().Trim().StartsWith("#@include "))
                {
                    Match match = gIncludePattern.Match(sLine);
                    if (match.Success)
                    {
                        list.Add(match.Value.Substring(1,match.Value.Length-2));
                    }
                }
            }
            return list;
        }

        public static IList<string> ParseReferences(string script)
        {
            List<string> list = new List<string>();
            List<string> alreadyParsed = new List<string>();

            foreach (string sLine in script.Split('\n'))
            {
                if (sLine.ToLower().Trim().StartsWith("#@reference "))
                {
                    Match match = gReferencePattern.Match(sLine);
                    if (match.Success && !alreadyParsed.Contains(match.Value.ToLower()))
                    {
                        list.Add(match.Value.Substring(1, match.Value.Length - 2));
                        alreadyParsed.Add(match.Value.ToLower());
                    }
                }
            }
            return list;
        }

        public static int MaxScriptId { get; set; }
    }
}
