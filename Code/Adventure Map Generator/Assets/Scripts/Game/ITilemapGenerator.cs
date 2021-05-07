using UnityEngine;

namespace P4.MapGenerator
{
    public interface ITilemapGenerator
    {
        public void Generate(Vector2Int tilemapSize);
    }
}