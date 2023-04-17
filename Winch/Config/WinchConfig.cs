using System.IO;
using System.Reflection;

namespace Winch.Config
{
    public class WinchConfig : JSONConfig
    {
        private static readonly string WinchConfigPath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "WinchConfig.json");

        private WinchConfig() : base(WinchConfigPath, Properties.Resources.DefaultConfig) { }

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

        public static new T GetProperty<T>(string key, T defaultValue)
        {
            return ((JSONConfig)Instance).GetProperty(key, defaultValue);
        }
    }
}
