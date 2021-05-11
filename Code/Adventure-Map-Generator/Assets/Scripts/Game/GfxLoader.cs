using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Dazel.Game
{
    public sealed class GfxLoader
    {
        private readonly string gfxPath;

        private readonly Dictionary<string, Texture2D> cachedFiles = new Dictionary<string, Texture2D>();
        
        public GfxLoader(string gfxPath)
        {
            this.gfxPath = gfxPath;
        }

        public Texture2D LoadTile(string fileName)
        {
            string filePath = Path.Combine(gfxPath, fileName);

            if (cachedFiles.TryGetValue(filePath, out Texture2D cachedTexture))
            {
                return cachedTexture;
            }

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{filePath} does not exist!");
            }
            
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D texture = new Texture2D(16, 16) {filterMode = FilterMode.Point};
            texture.LoadImage(fileData);
            
            cachedFiles.Add(filePath, texture);

            return texture;
        }
    }
}
