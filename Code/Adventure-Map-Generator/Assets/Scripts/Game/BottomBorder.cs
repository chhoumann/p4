using UnityEngine;

namespace Dazel.Game
{
    public sealed class BottomBorder : ScreenBorder
    {
        public override Direction Direction => Direction.Down;
        
        public override void SetupBorderSize(Vector2Int screenSize)
        {
            edgeCollider.points = new[]
            {
                new Vector2(0, 0),
                new Vector2(screenSize.x, 0)
            };
        }
    }
}