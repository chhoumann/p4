using System.Collections.Generic;

namespace IntermediateModels
{
    public sealed class Screen
    {
        public Stack<List<Tile>> TileStack { get; set; }
        public List<Entity> Entities { get; set; }
        public List<Exit> Exits { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        
        public string Name { get; set; }
    }
}