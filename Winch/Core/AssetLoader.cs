using System;
using System.Collections.Generic;
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

            if(Directory.Exists(localizationFolderPath)) LoadLocalizationFiles(localizationFolderPath);
            if(Directory.Exists(textureFolderPath)) LoadTextureFiles(textureFolderPath);
            if(Directory.Exists(itemFolderPath)) LoadItemFiles(itemFolderPath);
        }

        private static void LoadItemFiles(string itemFolderPath)
        {
            Dictionary<Type, string> _pathData = new Dictionary<Type, string>()
            {
                { typeof(NonSpatialItemData), Path.Combine(itemFolderPath, "NonSpatial")},
                { typeof(SpatialItemData), Path.Combine(itemFolderPath, "General")},
                { typeof(FishItemData), Path.Combine(itemFolderPath, "Fish")},
                { typeof(EngineItemData), Path.Combine(itemFolderPath, "Engines")},
                { typeof(LightItemData), Path.Combine(itemFolderPath, "Lights")},
                { typeof(RodItemData), Path.Combine(itemFolderPath, "Rods")},
                { typeof(RelicItemData), Path.Combine(itemFolderPath, "Relics")},
                { typeof(ResearchableItemData), Path.Combine(itemFolderPath, "Books")},
                { typeof(MessageItemData), Path.Combine(itemFolderPath, "Messages")},
                { typeof(DeployableItemData), Path.Combine(itemFolderPath, "Pots")},
                { typeof(DredgeItemData), Path.Combine(itemFolderPath, "Dredge")},
                { typeof(DamageItemData), Path.Combine(itemFolderPath, "Damage")},
            };

            foreach (KeyValuePair<Type, string> item in _pathData)
            {
                var baseMethod = typeof(AssetLoader).GetMethod(nameof(AssetLoader.LoadItemFilesOfType));
                var genericMethod = baseMethod.MakeGenericMethod(item.Key);
                if (Directory.Exists(item.Value))
                {
                    genericMethod.Invoke(null, new object[] { item.Value });
                }
            }
        }

        public static void LoadItemFilesOfType<T>(string itemFolderPath) where T : ItemData
        {
            string[] itemFiles = Directory.GetFiles(itemFolderPath);
            foreach (string file in itemFiles)
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
