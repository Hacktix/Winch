using CommandTerminal;
using UnityEngine;
using Winch.Config;
using Winch.Core.API;
using Winch.Util;

namespace Winch.Core
{
    class Initializer
    {
        internal static void Initialize()
        {
            WinchCore.Log.Debug("Initializer started.");

            InitializeAssetLoader();

            if(WinchConfig.GetProperty("EnableDeveloperConsole", false))
                InitializeDevConsole();

            DredgeEvent.TriggerManagersLoaded();
        }

        internal static void InitializePostUnityLoad()
        {
            InitializeVersionLabel();
        }

        private static void InitializeAssetLoader()
        {
            GameObject assetLoader = new GameObject();
            assetLoader.AddComponent<AssetLoaderObject>();
            Object.DontDestroyOnLoad(assetLoader);
        }

        internal static void InitializeVersionLabel()
        {
            WinchCore.Log.Debug("Initializing Version Label...");

            string versionString = VersionUtil.GetVersion();
            GameManager.Instance.BuildInfo.BuildNumber += $"\nWinch {versionString}";

            int modsLoaded = ModAssemblyLoader.LoadedMods.Count;
            string modsLoadedString = $"{modsLoaded} Mod{(modsLoaded != 1 ? "s" : "")} loaded";
            GameManager.Instance.BuildInfo.BuildNumber += $"\n{modsLoadedString}";
        }

        private static void InitializeDevConsole()
        {
            WinchCore.Log.Debug("Initializing Developer Console...");
            GameObject term = new GameObject();
            term.AddComponent<Terminal>();
            UnityEngine.Object.DontDestroyOnLoad(term);
        }
    }
}
