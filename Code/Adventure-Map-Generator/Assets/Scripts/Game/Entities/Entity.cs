using UnityEngine;

namespace P4.MapGenerator.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        public Vector2 MoveDirection { get; protected set; }
    }
}