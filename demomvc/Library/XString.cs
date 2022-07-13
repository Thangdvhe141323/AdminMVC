using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace demomvc.Library
{
    public class XString
    {
        public static string str_Slug(string s)
        {
            String[][] symbols =
            {
                new String[]{"[áàạảắằẳặ]","a[]"},
                new String[] {"[d]","[d]"},
                new String[] {"[iiiiiii]","[i]"}
            };
            s = s.ToLower();
            foreach(var ss in symbols)
            {
                s = Regex.Replace(s, ss[0], ss[1]);
            }
            return s;
        }
    }
}