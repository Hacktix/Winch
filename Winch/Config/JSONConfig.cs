using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Winch.Config
{
    public class JSONConfig
    {
        private Dictionary<string, object> _config;
        private string _configPath;

        public JSONConfig(string path, string defaultConfig) {
            _configPath = path;

            if (File.Exists(_configPath))
            {
                string confText = File.ReadAllText(_configPath);
                _config = JsonConvert.DeserializeObject<Dictionary<string, object>>(confText);
            }
            else
            {
                File.WriteAllText(_configPath, defaultConfig);
                _config = JsonConvert.DeserializeObject<Dictionary<string, object>>(defaultConfig);
            }
        }

        public T GetProperty<T>(string key, T defaultValue)
        {
            if (!_config.ContainsKey(key))
            {
                SetProperty(key, defaultValue);
                return defaultValue;
            }
            return (T)_config[key];
        }

        public void SetProperty<T>(string key, T value)
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
