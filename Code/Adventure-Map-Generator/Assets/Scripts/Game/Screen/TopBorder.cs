using UnityEngine;

namespace Dazel.Game.Screen
{
    public sealed class TopBorder : ScreenBorder
    {
        protected override Direction Direction => Direction.Up;
        
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