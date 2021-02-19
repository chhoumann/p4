using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class Map : MonoBehaviour
    {
        [SerializeField] private Map mapAbove;
        [SerializeField] private Map mapBelow;
        [SerializeField] private Map mapLeft;
        [SerializeField] private Map mapRight;

        [SerializeField] [Range(16, 64)] private int sizeX;
        [SerializeField] [Range(12, 64)] private int sizeY;
        
        public Vector2Int Size => size;
        public Bounds Bounds { get; private set; }

        private Vector2Int size;
        
        public Map Setup()
        {
            size = new Vector2Int(sizeX, sizeY);
            Bounds = new Bounds(new Vector3(size.x * 0.5f, size.y * 0.5f), new Vector3(size.x, size.y));

            return this;
        }

        public Map GetMap(Direction direction)
        {
            return direction switch
            {
                Direction.Up => mapAbove,
                Direction.Down => mapBelow,
                Direction.Left => mapLeft,
                Direction.Right => mapRight,
                _ => null
            };
        }
    }
}