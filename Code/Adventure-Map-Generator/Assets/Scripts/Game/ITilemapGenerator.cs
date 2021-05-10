using UnityEngine;

namespace Dazel.Game
{
    public interface ITilemapGenerator
    {
        public void Generate(Vector2Int tilemapSize);
    }
}