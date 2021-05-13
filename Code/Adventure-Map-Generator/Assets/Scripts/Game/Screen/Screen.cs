using System.Collections.Generic;
using Dazel.IntermediateModels;
using UnityEngine;

namespace Dazel.Game.Screen
{
    public sealed class Screen : MonoBehaviour
    {
        public Dictionary<Direction, Screen> ConnectedScreens { get; } = new Dictionary<Direction, Screen>();

        public Vector2Int Size => size;

        private Vector2Int size;
        
        public Screen Setup(ScreenModel screenModel)
        {
            size = new Vector2Int(screenModel.Width, screenModel.Height);

            foreach (ITilemapGenerator tilemapGenerator in GetComponentsInChildren<ITilemapGenerator>())
            {
                tilemapGenerator.Generate(screenModel.TileStack.Pop());
            }
            
            foreach (ScreenBorder border in GetComponentsInChildren<ScreenBorder>())
            {
                border.SetupBorderSize(this);
            }
            
            return this;
        }

        public Screen GetMap(Direction direction)
        {
            return ConnectedScreens.TryGetValue(direction, out Screen screen) ? screen : null;
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