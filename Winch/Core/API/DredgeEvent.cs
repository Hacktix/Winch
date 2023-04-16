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



        public static event AddressablesLoadedEventHandler<MapMarkerData> BeforeMapMarkersLoaded;
        public static event AddressablesLoadedEventHandler<MapMarkerData> MapMarkersLoaded;
        internal static void TriggerMapMarkersLoaded(object sender, AsyncOperationHandle<IList<MapMarkerData>> loadHandle, bool prefixTrigger)
        {
            WinchCore.Log.Debug($"Triggered MapMarkersLoaded type event: {loadHandle.Result.Count} elements (Prefix: {prefixTrigger})");
            AddressablesLoadedEventArgs<MapMarkerData> args = new AddressablesLoadedEventArgs<MapMarkerData>(loadHandle);
            if (prefixTrigger)
                BeforeMapMarkersLoaded?.Invoke(sender, args);
            else
                MapMarkersLoaded?.Invoke(sender, args);
        }



        public static event AddressablesLoadedEventHandler<QuestData> BeforeQuestsLoaded;
        public static event AddressablesLoadedEventHandler<QuestData> QuestsLoaded;
        internal static void TriggerQuestsLoaded(object sender, AsyncOperationHandle<IList<QuestData>> loadHandle, bool prefixTrigger)
        {
            WinchCore.Log.Debug($"Triggered QuestsLoaded type event: {loadHandle.Result.Count} elements (Prefix: {prefixTrigger})");
            AddressablesLoadedEventArgs<QuestData> args = new AddressablesLoadedEventArgs<QuestData>(loadHandle);
            if (prefixTrigger)
                BeforeQuestsLoaded?.Invoke(sender, args);
            else
                QuestsLoaded?.Invoke(sender, args);
        }



        public static event AddressablesLoadedEventHandler<QuestGridConfig> BeforeQuestGridLoaded;
        public static event AddressablesLoadedEventHandler<QuestGridConfig> QuestGridLoaded;
        internal static void TriggerQuestGridLoaded(object sender, AsyncOperationHandle<IList<QuestGridConfig>> loadHandle, bool prefixTrigger)
        {
            WinchCore.Log.Debug($"Triggered QuestGridLoaded type event: {loadHandle.Result.Count} elements (Prefix: {prefixTrigger})");
            AddressablesLoadedEventArgs<QuestGridConfig> args = new AddressablesLoadedEventArgs<QuestGridConfig>(loadHandle);
            if (prefixTrigger)
                BeforeQuestGridLoaded?.Invoke(sender, args);
            else
                QuestGridLoaded?.Invoke(sender, args);
        }
    }

}
