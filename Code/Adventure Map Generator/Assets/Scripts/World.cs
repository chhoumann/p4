using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class World : MonoBehaviour
    {
        public static Map CurrentMap { get; } = new Map(24, 24);
    }
}
