using UnityEngine;

namespace P4.MapGenerator
{
    [RequireComponent(typeof(Camera))]
    public sealed class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        
        private Camera cam;
        
        private Vector2 cameraExtents;
        private Vector2 cameraSize;

        private Vector2 minPosition;
        private Vector2 maxPosition;

        private void Start()
        { 
            cam = GetComponent<Camera>();
            CalculateCameraBounds();
        }

        private void Update()
        {
            UpdateCameraPosition();
        }

        private void CalculateCameraBounds()
        {
            float height = cam.orthographicSize * 2;
            float width = cam.aspect * height;
            
            cameraSize.x = width;
            cameraSize.y = height;
            
            cameraExtents = cameraSize * 0.5f;

            minPosition.x = cameraExtents.x;
            minPosition.y = cameraExtents.y;

            maxPosition.x = World.Map.Size.x - cameraExtents.x;
            maxPosition.y = World.Map.Size.y - cameraExtents.y;
        }
        
        private void UpdateCameraPosition()
        {
            Vector3 camPos = transform.position;
            Vector3 targetPos = target.position;
            
            targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
            
            transform.position = new Vector3(targetPos.x, targetPos.y, camPos.z);
        }
    }
}
