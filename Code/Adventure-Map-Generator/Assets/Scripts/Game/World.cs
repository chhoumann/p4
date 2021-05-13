using System;
using System.Collections.Generic;
using Dazel.Game.Entities;
using Dazel.IntermediateModels;
using UnityEngine;

namespace Dazel.Game
{
    public sealed class World : MonoBehaviour
    {
        [SerializeField] private GameObject screenTemplate;
        [SerializeField] private Transform screenContainer;

        public static event Action<Screen> MapLoaded;
        
        public static Screen CurrentScreen { get; private set; }
        
        public static IEnumerable<ScreenModel> ScreenModels { get; set; }
        
        private void Awake()
        {
            if (ScreenModels == null) return;
            
            Dictionary<string, Screen> screens = CreateScreens();

            ConnectScreens(screens);
        }
        
        private void OnEnable()
        {
            ScreenBorder.PlayerExitedBounds += OnExitMapBounds;
        }

        private void OnDisable()
        {
            ScreenBorder.PlayerExitedBounds -= OnExitMapBounds;
        }

        private Dictionary<string, Screen> CreateScreens()
        {
            Dictionary<string, Screen> screens = new Dictionary<string, Screen>();

            foreach (ScreenModel screenModel in ScreenModels)
            {
                GameObject newScreen = Instantiate(screenTemplate, screenContainer);
                newScreen.transform.name = screenModel.Identifier;

                Screen screen = newScreen.GetComponent<Screen>().Setup(screenModel);

                screens.Add(screenModel.Identifier, screen);

                if (CurrentScreen == null)
                {
                    CurrentScreen = screen;
                }
                else
                {
                    newScreen.SetActive(false);
                }
            }

            return screens;
        }

        private static void ConnectScreens(IReadOnlyDictionary<string, Screen> screens)
        {
            foreach (ScreenModel screenModel in ScreenModels)
            {
                Screen screen = screens[screenModel.Identifier];

                foreach (ScreenExitModel screenExitModel in screenModel.ScreenExits)
                {
                    Screen connectedScreen = screens[screenExitModel.ConnectedScreenIdentifier];

                    screen.ConnectedScreens.Add(screenExitModel.ExitDirection, connectedScreen);
                    connectedScreen.ConnectedScreens.Add(screenExitModel.ExitDirection.GetOpposite(), screen);
                }
            }
        }

        private void OnExitMapBounds(Player player, Direction exitDirection)
        {
            Screen currentScreen = CurrentScreen;
            Screen screenToLoad = currentScreen.GetMap(exitDirection);

            if (!screenToLoad) return;
            
            currentScreen.gameObject.SetActive(false);
            screenToLoad.gameObject.SetActive(true);

            SetPlayerPosition(player, currentScreen, screenToLoad, exitDirection);

            MapLoaded?.Invoke(screenToLoad);
            CurrentScreen = screenToLoad;
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
