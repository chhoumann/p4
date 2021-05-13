using System.Collections.Generic;

namespace Dazel.IntermediateModels
{
    public sealed class ScreenModel
    {
        public Stack<IGenerator> TileStack { get; set; } = new Stack<IGenerator>();
        public List<EntityModel> Entities { get; set; } = new List<EntityModel>();
        public List<ExitModel> Exits { get; set; } = new List<ExitModel>();

        public int Width { get; set; }
        public int Height { get; set; }
        
        public string Name { get; set; }
    }
}