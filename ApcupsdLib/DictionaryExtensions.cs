using System;
using System.Collections.Generic;
using System.Globalization;

namespace ApcupsdLib
{
    internal static class DictionaryExtensions
    {
        private static readonly CultureInfo USCulture = new CultureInfo("en-US");

        public static string GetStringOrEmpty(this Dictionary<string, string> dict, string key)
        {
            return dict.ContainsKey(key) ? dict[key] : string.Empty;
        }

        public static DateTime? GetNullableDateTime(this Dictionary<string, string> dict, string key)
        {
            return dict.ContainsKey(key) && dict[key] != "N/A" ? (DateTime?)DateTime.Parse(dict[key], DictionaryExtensions.USCulture) : null;
        }

        public static DateTime GetDateTime(this Dictionary<string, string> dict, string key)
        {
            return DateTime.Parse(dict[key]);
        }

        public static double? GetNullableDouble(this Dictionary<string, string> dict, string key)
        {
            // We need to use CultureInfo.InvariantCulture to force . to be used as the decimal separator.
            // In other regions like europe the parse would fail since the value from apcupsd uses . but in europe we use ,
            return dict.ContainsKey(key) ? (double?)Double.Parse(dict[key].Split(' ')[0],NumberStyles.Any, CultureInfo.InvariantCulture) : null;
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
