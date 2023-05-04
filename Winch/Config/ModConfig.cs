using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Winch.Core;

namespace Winch.Config
{
    public class ModConfig : JSONConfig
    {
        private static Dictionary<string, string> DefaultConfigs = new Dictionary<string, string>();
        private static Dictionary<string, ModConfig> Instances = new Dictionary<string, ModConfig>();

        private ModConfig(string modName) : base(GetConfigPath(modName), GetDefaultConfig(modName))
        {
        }

        private static string GetDefaultConfig(string modName)
        {
            if(!DefaultConfigs.ContainsKey(modName))
            {
                WinchCore.Log.Error($"No 'DefaultConfig' attribute found in mod_meta.json for {modName}!");
                throw new KeyNotFoundException($"No 'DefaultConfig' attribute found in mod_meta.json for {modName}!");
            }
            return DefaultConfigs[modName];
        }

        private static string GetConfigPath(string modName)
        {
            return Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Config", modName, "Config.json");
        }

        private static ModConfig GetConfig(string modName)
        {
            if (!Instances.ContainsKey(modName))
                Instances.Add(modName, new ModConfig(modName));
            return Instances[modName];
        }

        public static T? GetProperty<T>(string modName, string key, T? defaultValue)
        {
            return GetConfig(modName).GetProperty(key, defaultValue);
        }

        public static void RegisterDefaultConfig(string modName, string config)
        {
            DefaultConfigs.Add(modName, config);
        }
    }
}
