using Dazel.IntermediateModels;
using UnityEngine;

namespace Dazel.Game
{
    public sealed class Screen : MonoBehaviour
    {
        [SerializeField] private Screen screenAbove;
        [SerializeField] private Screen screenBelow;
        [SerializeField] private Screen screenLeft;
        [SerializeField] private Screen screenRight;

        public Vector2Int Size => size;
        public Bounds Bounds { get; private set; }

        private Vector2Int size;
        
        public Screen Setup(ScreenModel screenModel)
        {
            size = new Vector2Int(screenModel.Width, screenModel.Height);
            Bounds = new Bounds(new Vector3(size.x * 0.5f, size.y * 0.5f), new Vector3(size.x, size.y));

            foreach (ITilemapGenerator tilemapGenerator in GetComponentsInChildren<ITilemapGenerator>())
            {
                tilemapGenerator.Generate(size, screenModel.TileStack.Pop());
            }
            
            return this;
        }

        public Screen GetMap(Direction direction)
        {
            return direction switch
            {
                Direction.Up => screenAbove,
                Direction.Down => screenBelow,
                Direction.Left => screenLeft,
                Direction.Right => screenRight,
                _ => null
            };
        }

        private void OnDrawGizmos()
        {
            Vector3 cubeCenter = new Vector3(size.x * 0.5f, size.y * 0.5f);
            Vector3 cubeSize = new Vector3(size.x, size.y);

            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(cubeCenter, cubeSize);
        }
    }
}