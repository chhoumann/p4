using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class World : MonoBehaviour
    {
        public static Map Map { get; } = new Map(24, 24);
    }
}
