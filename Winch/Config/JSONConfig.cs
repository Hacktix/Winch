using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;
// ReSharper disable HeapView.PossibleBoxingAllocation

namespace Winch.Config
{
    public class JSONConfig
    {
        private readonly Dictionary<string, object?> _config;
        public Dictionary<string, object?> Config { get { return _config; } }
        private readonly string _configPath;

        public JSONConfig(string path, string defaultConfig) {
            _configPath = path;

            if (File.Exists(_configPath))
            {
                string confText = File.ReadAllText(_configPath);
                _config = JsonConvert.DeserializeObject<Dictionary<string, object?>>(confText) ?? throw new InvalidOperationException("Unable to parse config file.");
            }
            else
            {
                File.WriteAllText(_configPath, defaultConfig);
                _config = JsonConvert.DeserializeObject<Dictionary<string, object?>>(defaultConfig) ?? throw new InvalidOperationException("Unable to parse default config.");
            }
        }

        public T? GetProperty<T>(string key, T? defaultValue)
        {
            if (!_config.ContainsKey(key))
            {
                SetProperty(key, defaultValue);
                return defaultValue;
            }
            return (T?)_config[key];
        }

        public void SetProperty<T>(string key, T? value)
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
