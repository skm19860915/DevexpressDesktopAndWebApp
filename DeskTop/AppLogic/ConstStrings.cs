using System;
using System.Collections.Generic;
using System.Reflection;

namespace Desktop.AppLogic
{
    public class ConstStrings
    {
        private static Dictionary<string, string> Map = new Dictionary<string, string>();
        public static string Get(string name)
        {
            return GetDefault(name);
        }

        public static void Add(string aKey, string aValue) => Map[aKey] = aValue;
        static string GetDefault(string aKey)
        {
            if (Map.ContainsKey(aKey))
                return Map[aKey];

            return null;
        }
    }

}
