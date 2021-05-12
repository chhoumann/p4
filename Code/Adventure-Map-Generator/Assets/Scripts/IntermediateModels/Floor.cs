using System.Collections.Generic;

namespace Dazel.IntermediateModels
{
    public sealed class Floor : IGenerator
    {
        public IEnumerable<TileModel> Tiles { get; }
        
        public Floor(int width, int height, string tileName)
        {
            List<TileModel> floorTiles = new List<TileModel>();
            
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    TileModel tile = new TileModel(x, y, tileName);
                    floorTiles.Add(tile);
                }
            }

            Tiles = floorTiles;
        }
    }
}