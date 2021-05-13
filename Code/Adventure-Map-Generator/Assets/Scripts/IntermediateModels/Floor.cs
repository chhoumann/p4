using System.Collections.Generic;

namespace Dazel.IntermediateModels
{
    public sealed class Floor : IGenerator
    {
        public IEnumerable<TileModel> Tiles { get; }
        
        public Floor(ScreenModel screenModel, string tileName)
        {
            List<TileModel> floorTiles = new List<TileModel>();
            
            for (int x = 0; x < screenModel.Width; x++)
            {
                for (int y = 0; y < screenModel.Height; y++)
                {
                    TileModel tile = new TileModel(x, y, tileName);
                    floorTiles.Add(tile);
                }
            }

            Tiles = floorTiles;
        }
    }
}