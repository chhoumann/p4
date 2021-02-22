using UnityEngine;

namespace P4.MapGenerator
{
    public abstract class Entity : MonoBehaviour
    {
        public Vector2 MoveDirection { get; protected set; }
    }
}