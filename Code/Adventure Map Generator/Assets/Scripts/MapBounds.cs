using System;
using P4.MapGenerator.Entities;
using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class MapBounds : MonoBehaviour
    {
        [SerializeField] private Player player;
        public static event Action<Player, Direction> PlayerExitedBounds;
        
        private void FixedUpdate()
        {
            if (player.Position.y + player.Size.y * 0.5f > World.Map.Bounds.max.y)
            {
                OnExitBounds(Direction.Up);
            }
            
            if (player.Position.y - player.Size.y * 0.5f < World.Map.Bounds.min.y)
            {
                OnExitBounds(Direction.Down);   
            }
            
            if (player.Position.x + player.Size.y * 0.5f > World.Map.Bounds.max.x)
            {
                OnExitBounds(Direction.Right);
            }
            
            if (player.Position.x - player.Size.y * 0.5f < World.Map.Bounds.min.x)
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