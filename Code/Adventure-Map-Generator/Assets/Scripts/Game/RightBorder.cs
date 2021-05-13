using UnityEngine;

namespace Dazel.Game
{
    public sealed class RightBorder : ScreenBorder
    {
        public override Direction Direction => Direction.Right;
        
        public override void SetupBorderSize(Screen screen)
        {
            edgeCollider.points = new[]
            {
                new Vector2(screen.Size.x, 0),
                new Vector2(screen.Size.x, screen.Size.y)
            };
        }
    }
}