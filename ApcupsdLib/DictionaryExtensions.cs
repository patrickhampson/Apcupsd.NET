using System;
using System.Collections.Generic;

namespace ApcupsdLib
{
    internal static class DictionaryExtensions
    {
        public static string GetStringOrEmpty(this Dictionary<string, string> dict, string key)
        {
            return dict.ContainsKey(key) ? dict[key] : string.Empty;
        }

        public static DateTime? GetNullableDateTime(this Dictionary<string, string> dict, string key)
        {
            return dict.ContainsKey(key) && dict[key] != "N/A" ? (DateTime?)DateTime.Parse(dict[key]) : null;
        }

        public static DateTime GetDateTime(this Dictionary<string, string> dict, string key)
        {
            return DateTime.Parse(dict[key]);
        }

        public static double? GetNullableDouble(this Dictionary<string, string> dict, string key)
        {
            return dict.ContainsKey(key) ? (double?)Double.Parse(dict[key].Split(' ')[0]) : null;
        }

        public static int? GetNullableInt(this Dictionary<string, string> dict, string key)
        {
            return dict.ContainsKey(key) ? (int?)int.Parse(dict[key].Split(' ')[0]) : null;
        }

        public static bool? GetNullableBool(this Dictionary<string, string> dict, string key)
        {
            return dict.ContainsKey(key) ? (bool?)(dict[key] == "YES") : null;
        }

    }
}
