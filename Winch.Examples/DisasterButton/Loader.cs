using UnityEngine;
using UnityEngine.Localization.Settings;
using Winch.Config;
using Winch.Core;
using Winch.Core.API;

namespace DisasterButton
{
    public class Loader
    {
        public static void Initialize()
        {
            DredgeEvent.ManagersLoaded += InitializeDisasterButton;
        }

        private static void InitializeDisasterButton(object sender, System.EventArgs e)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<DisasterButton>();
            GameObject.DontDestroyOnLoad(gameObject);

            LocalizationSettings.StringDatabase.GetTable(LanguageManager.STRING_TABLE).AddEntry("notification.disaster-button", "A sudden chill runs down your spine.");
        }
    }
}
