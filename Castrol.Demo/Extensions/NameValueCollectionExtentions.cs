using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;

namespace Castrol.Demo.Extensions
{
    public static class NameValueCollectionExtentions
    {
        public static Dictionary<string, string> ToDictionary(this NameValueCollection col)
        {
            var dict = new Dictionary<string, string>();

            foreach (string key in col.Keys)
            {
                if (!dict.ContainsKey(key))
                    dict.Add(key, col[key]);
            }

            return dict;
        }
    }
}