using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace Marvel.MarvelApi
{
    public static class MarvelApiHelper
    {
        public static string UppercaseFirst(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
            return char.ToUpper(s[0]) + s.Substring(1);
        }

        public static string DisplayDate(string s)
        {
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }
                return s.Substring(0,10);
        }

        public static int extractId(string resourceUri)
        {
            if (string.IsNullOrEmpty(resourceUri))
            {
                return 0;
            }

            string[] parts = resourceUri.Split('/');
            return Int32.Parse(parts.Last());
        }
    }
}