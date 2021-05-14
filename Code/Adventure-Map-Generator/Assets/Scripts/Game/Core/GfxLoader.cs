using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Dazel.Game.Core
{
    public sealed class GfxLoader
    {
        private static readonly string[] AllowedImageFiles = {"png", "jpg"};
        
        private readonly string gfxPath;

        private readonly Dictionary<string, Texture2D> cachedFiles = new Dictionary<string, Texture2D>();

        public GfxLoader(string gfxPath)
        {
            this.gfxPath = gfxPath;
        }

        public Texture2D LoadTile(string gfxName)
        {
            if (gfxName.Contains("."))
            {
                return LoadTileFromFile(gfxName);
            }

            return LoadTileFromDirectory(gfxName);
        }
        
        private Texture2D LoadTileFromFile(string fileName)
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

        public Texture2D LoadTileFromDirectory(string directoryName)
        {
            string directoryPath = Path.Combine(gfxPath, directoryName);

            if (!Directory.Exists(directoryPath))
            {
                throw new FileNotFoundException($"Directory {directoryPath} does not exist!");
            }

            IEnumerable<string> files = GetFilesFrom(directoryPath);
            List<Texture2D> textures = new List<Texture2D>();
            
            foreach (string file in files)
            {
                textures.Add(LoadTileFromFile(file));
            }

            return textures[Random.Range(0, textures.Count)];
        }
        
        private static IEnumerable<string> GetFilesFrom(string searchFolder)
        {
            List<string> filesFound = new List<string>();
            
            foreach (string filter in AllowedImageFiles)
            {
                filesFound.AddRange(Directory.GetFiles(searchFolder, $"*.{filter}"));
            }
            
            return filesFound.ToArray();
        }
    }
}
