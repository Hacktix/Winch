using System;
using System.Collections.Generic;
using UnityEngine.ResourceManagement.AsyncOperations;

namespace Winch.Core.Events.Addressables
{
    public class AddressablesLoadedHook<T>
    {
        public event AddressablesLoadedEventHandler<T> Before;
        public event AddressablesLoadedEventHandler<T> On;

        public void Trigger(object sender, AsyncOperationHandle<IList<T>> handle, bool prefix)
        {
            WinchCore.Log.Debug($"Triggered {typeof(T)} type event: {handle.Result.Count} elements (Prefix: {prefix})");
            try
            {
                var args = new AddressablesLoadedEventArgs<T>(handle);
                if (prefix)
                    Before?.Invoke(sender, args);
                else
                    On?.Invoke(sender, args);
            }
            catch (Exception ex)
            {
                WinchCore.Log.Error($"Failed to trigger {typeof(T)} type event: {ex}");
            }

        }
    }
}
