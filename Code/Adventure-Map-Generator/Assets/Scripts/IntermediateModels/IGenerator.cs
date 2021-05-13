using System.Collections.Generic;

namespace Dazel.IntermediateModels
{
    public interface IGenerator
    {
        IEnumerable<TileModel> Tiles { get; }
    }
}