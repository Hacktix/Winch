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



        public static event AddressablesLoadedEventHandler<UpgradeData> BeforeUpgradesLoaded;
        public static event AddressablesLoadedEventHandler<UpgradeData> UpgradesLoaded;
        internal static void TriggerUpgradesLoaded(object sender, AsyncOperationHandle<IList<UpgradeData>> loadHandle, bool prefixTrigger)
        {
            WinchCore.Log.Debug($"Triggered UpgradesLoaded type event: {loadHandle.Result.Count} elements (Prefix: {prefixTrigger})");
            AddressablesLoadedEventArgs<UpgradeData> args = new AddressablesLoadedEventArgs<UpgradeData>(loadHandle);
            if (prefixTrigger)
                BeforeUpgradesLoaded?.Invoke(sender, args);
            else
                UpgradesLoaded?.Invoke(sender, args);
        }



        public static event AddressablesLoadedEventHandler<WeatherData> BeforeWeatherDataLoaded;
        public static event AddressablesLoadedEventHandler<WeatherData> WeatherDataLoaded;
        internal static void TriggerWeatherDataLoaded(object sender, AsyncOperationHandle<IList<WeatherData>> loadHandle, bool prefixTrigger)
        {
            WinchCore.Log.Debug($"Triggered WeatherDataLoaded type event: {loadHandle.Result.Count} elements (Prefix: {prefixTrigger})");
            AddressablesLoadedEventArgs<WeatherData> args = new AddressablesLoadedEventArgs<WeatherData>(loadHandle);
            if (prefixTrigger)
                BeforeWeatherDataLoaded?.Invoke(sender, args);
            else
                WeatherDataLoaded?.Invoke(sender, args);
        }



        public static event AddressablesLoadedEventHandler<WorldEventData> BeforeWorldEventsLoaded;
        public static event AddressablesLoadedEventHandler<WorldEventData> WorldEventsLoaded;
        internal static void TriggerWorldEventsLoaded(object sender, AsyncOperationHandle<IList<WorldEventData>> loadHandle, bool prefixTrigger)
        {
            WinchCore.Log.Debug($"Triggered WorldEventsLoaded type event: {loadHandle.Result.Count} elements (Prefix: {prefixTrigger})");
            AddressablesLoadedEventArgs<WorldEventData> args = new AddressablesLoadedEventArgs<WorldEventData>(loadHandle);
            if (prefixTrigger)
                BeforeWorldEventsLoaded?.Invoke(sender, args);
            else
                WorldEventsLoaded?.Invoke(sender, args);
        }
    }

}
