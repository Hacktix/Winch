using System;
using System.IO;
using Winch.Util;

namespace Winch.Core
{
    internal class AssetLoader
    {
        internal static void LoadAssets()
        {
            WinchCore.Log.Debug("Loading assets...");

            string[] modDirs = Directory.GetDirectories("Mods");
            foreach (string modDir in modDirs)
            {
                string assetFolderPath = Path.Combine(modDir, "Assets");
                if (!Directory.Exists(assetFolderPath))
                    continue;
                LoadAssetFolder(assetFolderPath);
            }
        }

        private static void LoadAssetFolder(string path)
        {
            string localizationFolderPath = Path.Combine(path, "Localization");
            string textureFolderPath = Path.Combine(path, "Textures");
            string itemFolderPath = Path.Combine(path, "Items");
            string poiFolderpath = Path.Combine(path, "POI");

            if(Directory.Exists(localizationFolderPath)) LoadLocalizationFiles(localizationFolderPath);
            if(Directory.Exists(textureFolderPath)) LoadTextureFiles(textureFolderPath);
            if(Directory.Exists(itemFolderPath)) LoadItemFiles(itemFolderPath);
            if(Directory.Exists(poiFolderpath)) LoadPoiFiles(poiFolderpath);
        }

        private static void LoadPoiFiles(string poiFolderPath)
        {
            WinchCore.Log.Debug("LoadingPOI files...");
            string conversationPoiPath = Path.Combine(poiFolderPath, "Conversation");
            string harvestPoiPath = Path.Combine(poiFolderPath, "Harvest");

            WinchCore.Log.Debug("LoadingPOI files... Conversation");
            if (Directory.Exists(poiFolderPath)) LoadConversationPoiFiles(conversationPoiPath);
            WinchCore.Log.Debug("LoadingPOI files.. Harvest");
            if (Directory.Exists(harvestPoiPath)) LoadHarvestPoiFiles(harvestPoiPath);
        }

        private static void LoadItemFiles(string itemFolderPath)
        {
            string spatialItemsPath = Path.Combine(itemFolderPath, "General");
            string fishItemsPath = Path.Combine(itemFolderPath, "Fish");

            if(Directory.Exists(spatialItemsPath)) LoadItemFilesOfType<SpatialItemData>(spatialItemsPath);
            if(Directory.Exists(fishItemsPath)) LoadItemFilesOfType<FishItemData>(fishItemsPath);
        }

        private static void LoadConversationPoiFiles(string conversationPoiFolderPath)
        {
            string autoMovePoiPath = Path.Combine(conversationPoiFolderPath, "AutoMove");
            string explosivePoiPath = Path.Combine(conversationPoiFolderPath, "Explosive");
            string inspectPoiPath = Path.Combine(conversationPoiFolderPath, "Inspect");
            
            if (Directory.Exists(autoMovePoiPath)) LoadPoiFilesOfType<AutoMovePOI>(autoMovePoiPath);
            if (Directory.Exists(explosivePoiPath)) LoadPoiFilesOfType<ExplosivePOI>(explosivePoiPath);
            if (Directory.Exists(inspectPoiPath)) LoadPoiFilesOfType<InspectPOI>(inspectPoiPath);
        }

        private static void LoadHarvestPoiFiles(string harvestPoiFolderPath)
        {
            string baitHarvestPoiPath = Path.Combine(harvestPoiFolderPath, "Bait");
            string placedHarvestPoiPath = Path.Combine(harvestPoiFolderPath, "Placed");
            WinchCore.Log.Debug("LoadingPOI Folder.. Harvest");
            if (Directory.Exists(harvestPoiFolderPath)) LoadCustomHarvestPoisFromMetadata(harvestPoiFolderPath);
            WinchCore.Log.Debug("LoadingPOI Folder.. BaitHarvest");
            if (Directory.Exists(baitHarvestPoiPath)) LoadPoiFilesOfType<BaitHarvestPOI>(baitHarvestPoiPath);
            WinchCore.Log.Debug("LoadingPOI Folder.. PlacedHarvest");
            if (Directory.Exists(placedHarvestPoiPath)) LoadPoiFilesOfType<PlacedHarvestPOI>(placedHarvestPoiPath);
        }

        private static void LoadCustomHarvestPoisFromMetadata(string poiFolderPath)
        {
            string[] poiFiles = Directory.GetFiles(poiFolderPath);
            foreach(string file in poiFiles)
            {
                try
                {
                    PoiUtil.AddCustomHarvestPoiFromMetadata(file);
                }
                catch(Exception ex)
                {
                    WinchCore.Log.Error($"Failed to load POI from {file}: {ex}");
                }
            }
        }

        private static void LoadPoiFilesOfType<T>(string poiFolderPath) where T : POI
        {
            string[] poiFiles = Directory.GetFiles(poiFolderPath);
            foreach(string file in poiFiles)
            {
                try
                {
                    //PoiUtil.AddPoiFromMeta<T>(file);
                }
                catch(Exception ex)
                {
                    WinchCore.Log.Error($"Failed to load POI from {file}: {ex}");
                }
            }
        }

        private static void LoadItemFilesOfType<T>(string itemFolderPath) where T : ItemData
        {
            string[] itemFiles = Directory.GetFiles(itemFolderPath);
            foreach(string file in itemFiles)
            {
                try
                {
                    ItemUtil.AddItemFromMeta<T>(file);
                }
                catch(Exception ex)
                {
                    WinchCore.Log.Error($"Failed to load item from {file}: {ex}");
                }
            }
        }

        private static void LoadLocalizationFiles(string localizationFolderPath)
        {
            string[] localizationFiles = Directory.GetFiles(localizationFolderPath);
            foreach (string file in localizationFiles) {
                try
                {
                    LocalizationUtil.LoadLocalizationFile(file);
                }
                catch(Exception ex)
                {
                    WinchCore.Log.Error($"Failed to load localization file {file}: {ex}");
                }
            }
        }

        private static void LoadTextureFiles(string textureFolderPath)
        {
            string[] textureFiles = Directory.GetFiles(textureFolderPath);
            foreach (string file in textureFiles)
            {
                try
                {
                    TextureUtil.LoadTextureFromFile(file);
                }
                catch(Exception ex)
                {
                    WinchCore.Log.Error($"Failed to load texture file {file}: {ex}");
                }
            }
        }
    }
}
