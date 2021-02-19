using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class Map
    {
        public static readonly Vector2Int MinSize = new Vector2Int(16, 12);
        public static readonly Vector2Int MaxSize = new Vector2Int(64, 64);

        public Vector2Int Size => size;
        public Bounds Bounds => bounds;
        
        private readonly Vector2Int size;
        private readonly Bounds bounds;

        public Map(int sizeX, int sizeY)
        {
            size.x = Mathf.Clamp(sizeX, MinSize.x, MaxSize.x);
            size.y = Mathf.Clamp(sizeY, MinSize.y, MaxSize.y);

            bounds = new Bounds(new Vector3(size.x * 0.5f, size.y * 0.5f), new Vector3(size.x, size.y));
        }

        public Map() : this(MinSize.x, MinSize.y) { }
    }
}