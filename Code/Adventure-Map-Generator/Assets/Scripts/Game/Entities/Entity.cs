using UnityEngine;

namespace Dazel.Game.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public Vector2 MoveDirection { get; protected set; }
    }
}