using UnityEngine;

namespace Dazel.Game
{
    public sealed class TopBorder : ScreenBorder
    {
        public override Direction Direction => Direction.Up;
        
        public override void SetupBorderSize(Vector2Int screenSize)
        {
            edgeCollider.points = new[]
            {
                new Vector2(0, screenSize.y),
                new Vector2(screenSize.x, screenSize.y)
            };
        }
    }
}