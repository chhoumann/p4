using DG.Tweening;
using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class TriggerExample : MonoBehaviour
    {
        [SerializeField] private Transform world;
        
        private bool used;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!used && other.CompareTag("Player"))
            {
                used = true;
                world.DOMoveX(-16, 1).SetEase(Ease.InOutSine);
            }
        }
    }
}
