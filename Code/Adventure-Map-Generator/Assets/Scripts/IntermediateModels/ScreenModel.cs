using System.Collections.Generic;

namespace Dazel.IntermediateModels
{
    public sealed class ScreenModel
    {
        public Stack<List<TileModel>> TileStack { get; set; } = new Stack<List<TileModel>>();
        public List<EntityModel> Entities { get; set; } = new List<EntityModel>();
        public List<ExitModel> Exits { get; set; } = new List<ExitModel>();

        public int Width { get; set; }
        public int Height { get; set; }
        
        public string Name { get; set; }

        public ScreenModel SetFloor(string tileName)
        {
            List<TileModel> floorTiles = new List<TileModel>();
            
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    TileModel tile = new TileModel(x, y, tileName);
                    floorTiles.Add(tile);
                }
            }
            
            TileStack.Push(floorTiles);

            return this;
        }
    }
}