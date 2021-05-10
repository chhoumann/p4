using System;
using Dazel.Game.Entities;
using UnityEngine;

namespace Dazel.Game
{
    public sealed class MapBounds : MonoBehaviour
    {
        [SerializeField] private Player player;
        public static event Action<Player, Direction> PlayerExitedBounds;
        
        private void FixedUpdate()
        {
            if (player.Position.y + player.Extents.y > World.Screen.Bounds.max.y)
            {
                OnExitBounds(Direction.Up);
            }
            
            if (player.Position.y - player.Extents.y < World.Screen.Bounds.min.y)
            {
                OnExitBounds(Direction.Down);   
            }
            
            if (player.Position.x + player.Extents.x > World.Screen.Bounds.max.x)
            {
                OnExitBounds(Direction.Right);
            }
            
            if (player.Position.x - player.Extents.x < World.Screen.Bounds.min.x)
            {
                OnExitBounds(Direction.Left);
            }
        }

        private void OnExitBounds(Direction exitDirection)
        {
            PlayerExitedBounds?.Invoke(player, exitDirection);
        }
    }
}