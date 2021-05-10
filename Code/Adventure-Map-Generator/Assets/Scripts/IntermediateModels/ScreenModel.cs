using System.Collections.Generic;

namespace IntermediateModels
{
    public sealed class ScreenModel
    {
        public Stack<List<TileModel>> TileStack { get; set; }
        public List<EntityModel> Entities { get; set; }
        public List<ExitModel> Exits { get; set; }

        public int Width { get; set; }
        public int Height { get; set; }
        
        public string Name { get; set; }
    }
}