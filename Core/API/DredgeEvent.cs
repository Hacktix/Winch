using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Winch.Core.API
{
    public static class DredgeEvent
    {
        public class AddressablesLoadedEventArgs<T> : EventArgs
        {
            public AsyncOperationHandle<IList<T>> Handle;

            public AddressablesLoadedEventArgs(AsyncOperationHandle<IList<T>> handle)
            {
                Handle = handle;
            }
        }
        public delegate void AddressablesLoadedEventHandler<T>(object sender, AddressablesLoadedEventArgs<T> e);

        public static event AddressablesLoadedEventHandler<ItemData> BeforeItemsLoaded;
        public static event AddressablesLoadedEventHandler<ItemData> ItemsLoaded;
        internal static void TriggerItemsLoaded(object sender, AsyncOperationHandle<IList<ItemData>> loadHandle, bool prefixTrigger)
        {
            WinchCore.Log.Debug($"Triggered ItemsLoaded type event: {loadHandle.Result.Count} elements");
            AddressablesLoadedEventArgs<ItemData> args = new AddressablesLoadedEventArgs<ItemData>(loadHandle);
            if(prefixTrigger)
            {
                WinchCore.Log.Debug($"Invoking BeforeItemsLoaded Event...");
                BeforeItemsLoaded?.Invoke(sender, args);
            }
            else
            {
                WinchCore.Log.Debug($"Invoking ItemsLoaded Event...");
                ItemsLoaded?.Invoke(sender, args);
            }
        }
    }

}
