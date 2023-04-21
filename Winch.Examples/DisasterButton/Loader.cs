using UnityEngine;
using Winch.Core.API;
using Winch.Util;

namespace DisasterButton
{
    public class Loader
    {
        public static void Initialize()
        {
            DredgeEvent.ManagersLoaded += InitializeDisasterButton;

            LocalizationUtil.AddLocalizedString("de", "notification.disaster-button", "Ein kalter Schauer läuft deinen Rücken hinunter.");
            LocalizationUtil.AddLocalizedString("en", "notification.disaster-button", "A sudden chill runs down your spine.");
        }

        private static void InitializeDisasterButton(object sender, System.EventArgs e)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<DisasterButton>();
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}
