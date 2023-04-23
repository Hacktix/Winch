using System;
using Winch.Core.API.Events.Addressables;

namespace Winch.Core.API
{
    public static class DredgeEvent
    {
        public static AddressableEvents AddressableEvents = new AddressableEvents();

        public static event EventHandler ManagersLoaded;
        public static void TriggerManagersLoaded()
        {
            WinchCore.Log.Debug("Triggered ManagersLoaded event");
            ManagersLoaded?.Invoke(null, null);
        }

        public static event EventHandler ModAssetsLoaded;
        public static void TriggerModAssetsLoaded()
        {
            WinchCore.Log.Debug("Triggered ModAssetsLoaded event");
            ModAssetsLoaded?.Invoke(null, null);
        }
    }
}
