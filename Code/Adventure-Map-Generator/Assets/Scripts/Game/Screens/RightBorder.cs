using UnityEngine;

namespace Dazel.Game.Screens
{
    public sealed class RightBorder : ScreenBorder
    {
        protected override Direction Direction => Direction.Right;
        
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