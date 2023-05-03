using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Winch.Util
{
    public static class TextureUtil
    {
        private static Dictionary<string, Texture2D> TextureMap = new();

        public static Texture2D? GetTexture(string key) => TextureMap[key];

        public static Sprite GetSprite(string key)
        {
            Texture2D tex = GetTexture(key) ?? throw new InvalidOperationException($"Texture '{key}' not found");
            Rect spriteRect = new Rect(0, 0, tex.width, tex.height);
            return Sprite.Create(tex, spriteRect, Vector2.zero);
        }

        internal static void LoadTextureFromFile(string path)
        {
            byte[] textureData = File.ReadAllBytes(path);
            var texture = new Texture2D(2, 2);
            texture.LoadImage(textureData);
            
            string fileName = Path.GetFileNameWithoutExtension(path);
            TextureMap[fileName] = texture;
        }
    }
}
