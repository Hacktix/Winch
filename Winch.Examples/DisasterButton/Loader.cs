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
        }

        private static void InitializeDisasterButton(object sender, System.EventArgs e)
        {
            GameObject gameObject = new GameObject();
            gameObject.AddComponent<DisasterButton>();
            GameObject.DontDestroyOnLoad(gameObject);
        }
    }
}
