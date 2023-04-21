namespace Winch.Core.Events.Addressables
{
    public class AddressableEvents
    {
        public AddressablesLoadedHook<AchievementData> AchievementsLoaded = new AddressablesLoadedHook<AchievementData>();

        public AddressablesLoadedHook<GridConfiguration> GridConfigsLoaded = new AddressablesLoadedHook<GridConfiguration>();

        public AddressablesLoadedHook<ItemData> ItemsLoaded = new AddressablesLoadedHook<ItemData>();

        public AddressablesLoadedHook<MapMarkerData> MapMarkersLoaded = new AddressablesLoadedHook<MapMarkerData>();

        public AddressablesLoadedHook<QuestData> QuestsLoaded = new AddressablesLoadedHook<QuestData>();

        public AddressablesLoadedHook<QuestGridConfig> QuestGridConfigsLoaded = new AddressablesLoadedHook<QuestGridConfig>();

        public AddressablesLoadedHook<UpgradeData> UpgradesLoaded = new AddressablesLoadedHook<UpgradeData>();

        public AddressablesLoadedHook<WeatherData> WeatherDataLoaded = new AddressablesLoadedHook<WeatherData>();

        public AddressablesLoadedHook<WorldEventData> WorldEventsLoaded = new AddressablesLoadedHook<WorldEventData>();
    }
}
