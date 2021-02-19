using UnityEngine;

namespace P4.MapGenerator
{
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;

        private Camera cam;
        
        private Vector2 cameraExtents;
        private Vector2 cameraSize;

        private void Start()
        { 
            cam = GetComponent<Camera>();
            CalculateCameraBounds();
        }

        private void Update()
        {
            Vector3 camPos = transform.position;
            Vector3 targetPos = target.position;
            
            targetPos.x = Mathf.Clamp(targetPos.x, cameraExtents.x, World.CurrentMap.Size.x - cameraExtents.x);
            targetPos.y = Mathf.Clamp(targetPos.y, cameraExtents.y, World.CurrentMap.Size.y - cameraExtents.y);
            
            transform.position = new Vector3(targetPos.x, targetPos.y, camPos.z);
        }

        private void CalculateCameraBounds()
        {
            float height = cam.orthographicSize * 2;
            float width = cam.aspect * height;
            
            cameraSize.x = width;
            cameraSize.y = height;
            
            cameraExtents = cameraSize * 0.5f;
        }
    }
}
