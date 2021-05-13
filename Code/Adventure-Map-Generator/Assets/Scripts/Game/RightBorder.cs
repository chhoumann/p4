using UnityEngine;

namespace Dazel.Game
{
    public sealed class RightBorder : ScreenBorder
    {
        public override Direction Direction => Direction.Right;
        
        public override void SetupBorderSize(Vector2Int screenSize)
        {
            edgeCollider.points = new[]
            {
                new Vector2(screenSize.x, 0),
                new Vector2(screenSize.x, screenSize.y)
            };
        }
    }
}