using System.Collections.Generic;

namespace Dazel.IntermediateModels
{
    public sealed class ScreenModel
    {
        public Stack<IGenerator> TileStack { get; set; } = new Stack<IGenerator>();
        public List<EntityModel> Entities { get; set; } = new List<EntityModel>();
        public List<ScreenExitModel> ScreenExits { get; set; } = new List<ScreenExitModel>();
        public List<TileExitModel> TileExits { get; set; } = new List<TileExitModel>();

        public int Width { get; set; }
        public int Height { get; set; }
        
        public string Identifier { get; }

        public ScreenModel(string identifier)
        {
            Identifier = identifier;
        }
    }
}