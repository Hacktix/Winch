using CommandTerminal;
using UnityEngine;
using Winch.Config;
using Winch.Util;

namespace Winch.Core
{
    class Initializer
    {
        public static void Initialize()
        {
            InitializeVersionLabel();

            if(WinchConfig.GetProperty("EnableDeveloperConsole", false))
                InitializeDevConsole();
        }

        private static void InitializeVersionLabel()
        {
            string versionString = VersionUtil.GetVersion();
            GameManager.Instance.BuildInfo.BuildNumber += $"\nWinch {versionString}";
        }

        private static void InitializeDevConsole()
        {
            GameObject term = new GameObject();
            term.AddComponent<Terminal>();
            UnityEngine.Object.DontDestroyOnLoad(term);
        }
    }
}
