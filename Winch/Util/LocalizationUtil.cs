using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using Winch.Core;

namespace Winch.Util
{
    public static class LocalizationUtil
    {
        private static Dictionary<string, Dictionary<string, string>> StringDatabase = new Dictionary<string, Dictionary<string, string>>();

        public static void AddLocalizedString(string locale, string key, string value)
        {
            if(!StringDatabase.ContainsKey(locale))
                StringDatabase[locale] = new Dictionary<string, string>();
            StringDatabase[locale][key] = value;
        }

        public static string GetLocalizedString(string locale, string key)
        {
            if (!StringDatabase.ContainsKey(locale)) return null;
            if (!StringDatabase[locale].ContainsKey(key)) return null;
            return StringDatabase[locale][key];
        }

        internal static void LoadLocalizationFile(string path)
        {
            string locale = Path.GetFileNameWithoutExtension(path);
            string fileText = File.ReadAllText(path);
            Dictionary<string, string> dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(fileText);

            foreach(string key in dict.Keys)
                AddLocalizedString(locale, key, dict[key]);

            WinchCore.Log.Debug($"Loaded {dict.Keys.Count} localized string(s) from {path}");
        }
    }
}
