using UnityEngine;

namespace Dazel.Game
{
    public sealed class LeftBorder : ScreenBorder
    {
        public override Direction Direction => Direction.Left;
        
        public override void SetupBorderSize(Vector2Int screenSize)
        {
            edgeCollider.points = new[]
            {
                new Vector2(0, 0),
                new Vector2(0, screenSize.y)
            };
        }
    }
}