namespace Winch.Core.API.Events.Addressables
{
    public class AddressableEvents
    {
        public AddressablesLoadedHook<AchievementData> AchievementsLoaded = new();
        public AddressablesLoadedHook<GridConfiguration> GridConfigsLoaded = new();
        public AddressablesLoadedHook<ItemData> ItemsLoaded = new();
        public AddressablesLoadedHook<MapMarkerData> MapMarkersLoaded = new();
        public AddressablesLoadedHook<QuestData> QuestsLoaded = new();
        public AddressablesLoadedHook<QuestGridConfig> QuestGridConfigsLoaded = new();
        public AddressablesLoadedHook<UpgradeData> UpgradesLoaded = new();
        public AddressablesLoadedHook<WeatherData> WeatherDataLoaded = new();
        public AddressablesLoadedHook<WorldEventData> WorldEventsLoaded = new();
    }
}
