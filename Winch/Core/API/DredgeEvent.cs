using System;
using Winch.Core.API.Events;
using Winch.Core.API.Events.Addressables;
using Winch.Core.API.Events.POI;
using Winch.Core.API.Events.Scene;

namespace Winch.Core.API
{
    public static class DredgeEvent
    {
        public static AddressableEvents AddressableEvents = new();
        public static SceneEvents SceneEvents = new();

        public static event EventHandler? ManagersLoaded;
        public static void TriggerManagersLoaded()
        {
            WinchCore.Log.Debug("Triggered ManagersLoaded event");
            ManagersLoaded?.Invoke(null, null);
        }

        public static event EventHandler? ModAssetsLoaded;
        public static void TriggerModAssetsLoaded()
        {
            WinchCore.Log.Debug("Triggered ModAssetsLoaded event");
            ModAssetsLoaded?.Invoke(null, null);
        }
    }
}
