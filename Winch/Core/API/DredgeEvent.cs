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



        public static event AddressablesLoadedEventHandler<AchievementData> BeforeAchievementsLoaded;
        public static event AddressablesLoadedEventHandler<AchievementData> AchievementsLoaded;
        internal static void TriggerAchievementsLoaded(object sender, AsyncOperationHandle<IList<AchievementData>> loadHandle, bool prefixTrigger)
        {
            WinchCore.Log.Debug($"Triggered AchievementsLoaded type event: {loadHandle.Result.Count} elements (Prefix: {prefixTrigger})");
            AddressablesLoadedEventArgs<AchievementData> args = new AddressablesLoadedEventArgs<AchievementData>(loadHandle);
            if (prefixTrigger)
                BeforeAchievementsLoaded?.Invoke(sender, args);
            else
                AchievementsLoaded?.Invoke(sender, args);
        }



        public static event AddressablesLoadedEventHandler<ItemData> BeforeItemsLoaded;
        public static event AddressablesLoadedEventHandler<ItemData> ItemsLoaded;
        internal static void TriggerItemsLoaded(object sender, AsyncOperationHandle<IList<ItemData>> loadHandle, bool prefixTrigger)
        {
            WinchCore.Log.Debug($"Triggered ItemsLoaded type event: {loadHandle.Result.Count} elements (Prefix: {prefixTrigger})");
            AddressablesLoadedEventArgs<ItemData> args = new AddressablesLoadedEventArgs<ItemData>(loadHandle);
            if (prefixTrigger)
                BeforeItemsLoaded?.Invoke(sender, args);
            else
                ItemsLoaded?.Invoke(sender, args);
        }



        public static event AddressablesLoadedEventHandler<GridConfiguration> BeforeGridConfigsLoaded;
        public static event AddressablesLoadedEventHandler<GridConfiguration> GridConfigsLoaded;
        internal static void TriggerGridConfigsLoaded(object sender, AsyncOperationHandle<IList<GridConfiguration>> loadHandle, bool prefixTrigger)
        {
            WinchCore.Log.Debug($"Triggered GridConfigsLoaded type event: {loadHandle.Result.Count} elements (Prefix: {prefixTrigger})");
            AddressablesLoadedEventArgs<GridConfiguration> args = new AddressablesLoadedEventArgs<GridConfiguration>(loadHandle);
            if (prefixTrigger)
                BeforeGridConfigsLoaded?.Invoke(sender, args);
            else
                GridConfigsLoaded?.Invoke(sender, args);
        }
    }

}
