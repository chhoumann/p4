using System;
using Dazel.Game.Entities;
using Dazel.IntermediateModels;
using UnityEngine;

namespace Dazel.Game
{
    public sealed class World : MonoBehaviour
    {
        [SerializeField] private Transform mapContainer;

        public static event Action<Screen> MapLoaded;
        
        public static Screen Screen { get; private set; }

        private Screen[] maps;
        
        private void Awake()
        {
            ScreenModel mockModel = new ScreenModel
            {
                Width = 47,
                Height = 30
            };
            
            mockModel.TileStack.Push(new Floor(mockModel.Width, mockModel.Height, "Grass.png"));
            
            maps = new Screen[mapContainer.childCount];

            for (int i = 0; i < maps.Length; i++)
            {
                maps[i] = mapContainer.GetChild(i).GetComponent<Screen>().Setup(mockModel);
            }
            
            Screen = maps[0];
        }

        private void OnEnable()
        {
            MapBounds.PlayerExitedBounds += OnExitMapBounds;
        }

        private void OnDisable()
        {
            MapBounds.PlayerExitedBounds -= OnExitMapBounds;
        }

        private void OnExitMapBounds(Player player, Direction exitDirection)
        {
            Screen currentScreen = Screen;
            Screen screenToLoad = currentScreen.GetMap(exitDirection);

            if (!screenToLoad) return;
            
            currentScreen.gameObject.SetActive(false);
            screenToLoad.gameObject.SetActive(true);

            SetPlayerPosition(player, currentScreen, screenToLoad, exitDirection);

            MapLoaded?.Invoke(screenToLoad);
            Screen = screenToLoad;
        }

        private void SetPlayerPosition(Player player, Screen currentScreen, Screen screenToLoad, Direction exitDirection)
        {
            float offset = Physics2D.defaultContactOffset;
            Vector2 playerPos = (player.Position / currentScreen.Size) * screenToLoad.Size;
            
            Vector2 minPos = new Vector2(player.Extents.x, player.Extents.y);
            Vector2 maxPos = new Vector2(screenToLoad.Size.x - player.Extents.x, screenToLoad.Size.y - player.Extents.y);

            playerPos.x = Mathf.Clamp(playerPos.x, minPos.x, maxPos.x);
            playerPos.y = Mathf.Clamp(playerPos.y, minPos.y, maxPos.y);
            
            switch (exitDirection)
            {
                case Direction.Up:
                    playerPos.y = minPos.y + offset;
                    break;
                case Direction.Down:
                    playerPos.y = maxPos.y - offset;
                    break;
                case Direction.Left:
                    playerPos.x = maxPos.x - offset;
                    break;
                case Direction.Right:
                    playerPos.x = minPos.x + offset;
                    break;
            }
            
            player.Position = playerPos;
        }
    }
}
