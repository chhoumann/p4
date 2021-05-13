using UnityEngine;

namespace Dazel.Game
{
    public abstract class ScreenBorder : MonoBehaviour
    {
        [SerializeField] protected EdgeCollider2D edgeCollider;
        
        public abstract Direction Direction { get; }

        public abstract void SetupBorderSize(Vector2Int screenSize);
    }
}