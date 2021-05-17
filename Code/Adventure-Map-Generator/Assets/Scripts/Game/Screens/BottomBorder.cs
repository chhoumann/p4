﻿using UnityEngine;

namespace Dazel.Game.Screens
{
    public sealed class BottomBorder : ScreenBorder
    {
        protected override Direction Direction => Direction.Down;
        
        public override void SetupBorderSize(GameScreen screen)
        {
            edgeCollider.points = new[]
            {
                new Vector2(0, 0),
                new Vector2(screen.Size.x, 0)
            };
        }
    }
}