using System;
using Dazel.Game.Entities;
using UnityEngine;

namespace Dazel.Game
{
    public abstract class ScreenBorder : MonoBehaviour
    {
        [SerializeField] protected EdgeCollider2D edgeCollider;
        
        public static event Action<Player, Direction> PlayerExitedBounds;

        protected abstract Direction Direction { get; }
        
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.transform.CompareTag(Tags.Player) && other.transform.TryGetComponent(out Player player))
            {
                PlayerExitedBounds?.Invoke(player, Direction);
            }
        }

        public abstract void SetupBorderSize(Screen screen);
    }
}