using CommandTerminal;
using Octokit;
using UnityEngine;
using Winch.Config;
using Winch.Core.API;
using Winch.Util;

namespace Winch.Core
{
    class Initializer
    {
        public static void Initialize()
        {
            WinchCore.Log.Debug("Initializer started.");

            InitializeAssetLoader();

            InitializeVersionLabel();

            CheckForUpdate();

            if(WinchConfig.GetProperty("EnableDeveloperConsole", false))
                InitializeDevConsole();

            DredgeEvent.TriggerManagersLoaded();
        }

        private static void InitializeAssetLoader()
        {
            GameObject assetLoader = new GameObject();
            assetLoader.AddComponent<AssetLoaderObject>();
            Object.DontDestroyOnLoad(assetLoader);
        }

        private static void InitializeVersionLabel()
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

        private static async void CheckForUpdate()
        {
            var client = new GitHubClient(new ProductHeaderValue("WinchFetchLatest"));
            var releases = await client.Repository.Release.GetAll("Hacktix", "Winch");
            var latest = releases[0];

            string updateAvailableString;
            if (VersionUtil.IsSameOrNewer(VersionUtil.GetVersion(), latest.TagName))
            {
                updateAvailableString = $"Latest version installed.";
            }
            else
            {
                updateAvailableString = $"Update {latest.TagName} available.";
            }

            GameManager.Instance.BuildInfo.BuildNumber += $"\n{updateAvailableString}";
        }
    }
}
