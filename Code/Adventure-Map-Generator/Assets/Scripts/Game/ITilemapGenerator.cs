using System.Collections.Generic;
using Dazel.IntermediateModels;

namespace Dazel.Game
{
    public interface ITilemapGenerator
    {
        public void Generate(List<TileModel> tiles);
    }
}