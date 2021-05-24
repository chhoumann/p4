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

        private int screenWidth;
        private int screenHeight;
        
        private void Start()
        { 
            cam = GetComponent<Camera>();
            CalculateCameraBounds(World.CurrentScreen);
        }

        private void OnEnable()
        {
            World.ScreenLoaded += CalculateCameraBounds;
        }

        private void OnDisable()
        {
            World.ScreenLoaded -= CalculateCameraBounds;
        }

        private void Update()
        {
            UpdateCameraPosition();

            if (Screen.width != screenWidth || Screen.height != screenHeight)
            {
                CalculateCameraBounds(World.CurrentScreen);
            }
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

            if (screen.Size.x < cameraSize.x)
            {
                minPosition.x = maxPosition.x = screen.Size.x * 0.5f;
            }

            if (screen.Size.y < cameraSize.y)
            {
                minPosition.x = maxPosition.y = screen.Size.y * 0.5f;
            }

            screenWidth = Screen.width;
            screenHeight = Screen.height;
        }
        
        private void UpdateCameraPosition()
        {
            Vector3 targetPos = target.position;
            Vector3 camPos = transform.position;

            targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
            targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
            
            transform.position = new Vector3(targetPos.x, targetPos.y, camPos.z);
        }
    }
}
