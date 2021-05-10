using System.IO;
using UnityEngine;

namespace Dazel.Game
{
    public sealed class GfxLoader
    {
        private readonly string gfxPath;

        public GfxLoader(string gfxPath)
        {
            this.gfxPath = gfxPath;
        }

        public Texture2D LoadTile(string fileName)
        {
            string filePath = Path.Combine(gfxPath, fileName);

            if (!File.Exists(filePath))
            {
                throw new FileNotFoundException($"{filePath} does not exist!");
            }
            
            byte[] fileData = File.ReadAllBytes(filePath);
            Texture2D tex = new Texture2D(16, 16);
            tex.filterMode = FilterMode.Point;
            tex.LoadImage(fileData);

            return tex;
        }
    }
}
