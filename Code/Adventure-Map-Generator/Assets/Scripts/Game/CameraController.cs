using Dazel.Game.Core;
using Dazel.Game.Screens;
using UnityEngine;

namespace Dazel.Game
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

        private bool centerCameraX;
        private bool centerCameraY;

        private void Start()
        { 
            cam = GetComponent<Camera>();
            CalculateCameraBounds(World.CurrentScreen);
        }

        private void OnEnable()
        {
            World.MapLoaded += CalculateCameraBounds;
        }

        private void OnDisable()
        {
            World.MapLoaded -= CalculateCameraBounds;
        }

        private void Update()
        {
            UpdateCameraPosition();
        }

        private void CalculateCameraBounds(GameScreen screen)
        {
            float height = cam.orthographicSize * 2;
            float width = cam.aspect * height;
            
            cameraSize.x = width;
            cameraSize.y = height;
            
            cameraExtents = cameraSize * 0.5f;

            minPosition.x = cameraExtents.x;
            minPosition.y = cameraExtents.y;

            maxPosition.x = screen.Size.x - cameraExtents.x;
            maxPosition.y = screen.Size.y - cameraExtents.y;

            centerCameraX = centerCameraY = false;

            if (screen.Size.x < cameraSize.x)
            {
                minPosition.x = maxPosition.x = screen.Size.x * 0.5f;
                centerCameraX = true;
            }

            if (screen.Size.y < cameraSize.y)
            {
                minPosition.x = maxPosition.y = screen.Size.y * 0.5f;
                centerCameraY = true;
            }
        }
        
        private void UpdateCameraPosition()
        {
            Vector3 targetPos = target.position;
            Vector3 camPos = transform.position;

            targetPos.x = centerCameraX ? maxPosition.x : Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
            targetPos.y = centerCameraY ? maxPosition.y : Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
            
            transform.position = new Vector3(targetPos.x, targetPos.y, camPos.z);
        }
    }
}
