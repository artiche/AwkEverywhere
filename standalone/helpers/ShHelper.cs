using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace AwkEverywhere.helpers
{
    public static class ShHelper
    {

        private static Regex gIncludePattern = new Regex("\".*\"");
        private static Regex gReferencePattern = new Regex("\".*\"");

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
                        list.Add(match.Value.Substring(1, match.Value.Length - 2));
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

    }
}
