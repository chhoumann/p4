using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class MapBounds : MonoBehaviour
    {
        [SerializeField] private Player player;
        
        private void Update()
        {
            if (player.Position.y > World.Map.Bounds.max.y)
            {
                OnExitBounds(Direction.Up);
            }
            
            if (player.Position.y < World.Map.Bounds.min.y)
            {
                OnExitBounds(Direction.Down);   
            }
            
            if (player.Position.x > World.Map.Bounds.max.x)
            {
                OnExitBounds(Direction.Right);
            }
            
            if (player.Position.x < World.Map.Bounds.min.x)
            {
                OnExitBounds(Direction.Left);
            }
        }

        private void OnExitBounds(Direction exitDirection)
        {
            Debug.Log($"Exited {exitDirection}");
        }
    }
}