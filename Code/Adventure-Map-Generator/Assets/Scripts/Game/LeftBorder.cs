using UnityEngine;

namespace Dazel.Game
{
    public sealed class LeftBorder : ScreenBorder
    {
        protected override Direction Direction => Direction.Left;

        public override void SetupBorderSize(Screen screen)
        {
            edgeCollider.points = new[]
            {
                new Vector2(0, 0),
                new Vector2(0, screen.Size.y)
            };
        }
    }
}