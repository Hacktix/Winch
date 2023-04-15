using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Winch.Config
{
    public class WinchConfig
    {
        private Dictionary<string, object> _config;
        private static readonly string _configPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WinchConfig.json");

        private WinchConfig() {
            if(File.Exists(_configPath))
            {
                string confText = File.ReadAllText(_configPath);
                _config = JsonConvert.DeserializeObject<Dictionary<string, object>>(confText);
            }
            else
                _config = GenerateDefaultConfig();
        }

        private static WinchConfig _instance;
        public static WinchConfig Instance
        {
            get
            {
                if(_instance == null)
                    _instance = new WinchConfig();
                return _instance;
            }
        }



        private static Dictionary<string, object> GenerateDefaultConfig()
        {
            string confText = Properties.Resources.DefaultConfig;
            File.WriteAllText(_configPath, confText);
            return JsonConvert.DeserializeObject<Dictionary<string, object>>(confText);
        }

        public static T GetProperty<T>(string key, T defaultValue)
        {
            return Instance._getProperty<T>(key, defaultValue);
        }



        private T _getProperty<T>(string key, T defaultValue)
        {
            if (!_config.ContainsKey(key))
            {
                _setProperty(key, defaultValue);
                return defaultValue;
            }
            return (T)_config[key];
        }

        private void _setProperty<T>(string key, T value)
        {
            _config[key] = value;
            SaveSettings();
        }

        private void SaveSettings()
        {
            string confText = JsonConvert.SerializeObject(_config, Formatting.Indented);
            File.WriteAllText(_configPath, confText);
        }
    }
}
