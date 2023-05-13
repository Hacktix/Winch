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
        private const string defaultConfigFile = "Config.json";

        private ModConfig(string modName, string fileName) : base(GetConfigPath(modName, fileName), GetDefaultConfig(modName))
        {
        }

        private static string GetDefaultConfig(string modName)
        {
            if (!DefaultConfigs.ContainsKey(modName))
            {
                WinchCore.Log.Debug($"No default config found for {modName}");
                DefaultConfigs.Add(modName, "{}");
            }
            return DefaultConfigs[modName];
        }

        private static string GetConfigPath(string configLocation, string fileName)
        {
            string basePath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "Config");
            string output = Path.Combine(basePath, configLocation, fileName);
            if (Directory.Exists(Path.Combine(basePath, configLocation)))
                return output;
            Directory.CreateDirectory(Path.Combine("Config", configLocation));
            return output;
        }

        private static ModConfig GetConfig(string modName, string fileName, string subDirectory)
        {
            string _path = Path.Combine(modName, subDirectory);
            if (!Instances.ContainsKey(_path))
                Instances.Add(_path, new ModConfig(_path, fileName));
            return Instances[_path];
        }

        public static T? GetProperty<T>(string modName, string key, T? defaultValue, string fileName = defaultConfigFile, string subDirectory = "")
        {
            return GetConfig(modName, fileName, subDirectory).GetProperty(key, defaultValue);
        }

        public static Dictionary<string, object?> GetFullConfig(string modName, string fileName = defaultConfigFile, string subDirectory = "")
        {
            return GetConfig(modName, fileName, subDirectory).Config;
        }

        public static void RegisterDefaultConfig(string modName, string config)
        {
            DefaultConfigs.Add(modName, config);
        }
    }
}
