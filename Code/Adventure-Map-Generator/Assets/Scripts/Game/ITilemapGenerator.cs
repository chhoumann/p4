using System.Collections.Generic;
using Dazel.IntermediateModels;
using UnityEngine;

namespace Dazel.Game
{
    public interface ITilemapGenerator
    {
        public void Generate(Vector2Int tilemapSize, List<TileModel> tiles);
    }
}