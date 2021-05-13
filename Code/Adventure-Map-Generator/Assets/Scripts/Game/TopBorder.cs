using UnityEngine;

namespace Dazel.Game
{
    public sealed class TopBorder : ScreenBorder
    {
        public override Direction Direction => Direction.Up;
        
        public override void SetupBorderSize(Screen screen)
        {
            edgeCollider.points = new[]
            {
                new Vector2(0, screen.Size.y),
                new Vector2(screen.Size.x, screen.Size.y)
            };
        }
    }
}