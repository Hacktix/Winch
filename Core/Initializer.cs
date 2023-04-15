using CommandTerminal;
using System.Reflection;
using UnityEngine;
using Winch.Config;

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
            GameManager.Instance.BuildInfo.BuildNumber += "\nWinch alpha-0.0.1";
        }

        private static void InitializeDevConsole()
        {
            GameObject term = new GameObject();
            term.AddComponent<Terminal>();
            UnityEngine.Object.DontDestroyOnLoad(term);
        }
    }
}
