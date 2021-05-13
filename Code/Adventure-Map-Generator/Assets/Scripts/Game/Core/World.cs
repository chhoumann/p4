using System;
using System.Collections.Generic;
using Dazel.Game.Entities;
using Dazel.Game.Screen;
using Dazel.IntermediateModels;
using UnityEngine;

namespace Dazel.Game.Core
{
    public sealed class World : MonoBehaviour
    {
        [SerializeField] private GameObject screenTemplate;
        [SerializeField] private Transform screenContainer;

        public static event Action<Screen.Screen> MapLoaded;
        
        public static Screen.Screen CurrentScreen { get; private set; }
        
        public static IEnumerable<ScreenModel> ScreenModels { get; set; }
        
        private void Awake()
        {
            if (ScreenModels == null) return;
            
            Dictionary<string, Screen.Screen> screens = CreateScreens();

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

        private Dictionary<string, Screen.Screen> CreateScreens()
        {
            Dictionary<string, Screen.Screen> screens = new Dictionary<string, Screen.Screen>();

            foreach (ScreenModel screenModel in ScreenModels)
            {
                GameObject newScreen = Instantiate(screenTemplate, screenContainer);
                newScreen.transform.name = screenModel.Identifier;

                Screen.Screen screen = newScreen.GetComponent<Screen.Screen>().Setup(screenModel);

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

        private static void ConnectScreens(IReadOnlyDictionary<string, Screen.Screen> screens)
        {
            foreach (ScreenModel screenModel in ScreenModels)
            {
                Screen.Screen screen = screens[screenModel.Identifier];

                foreach (ScreenExitModel screenExitModel in screenModel.ScreenExits)
                {
                    Screen.Screen connectedScreen = screens[screenExitModel.ConnectedScreenIdentifier];

                    screen.ConnectedScreens.Add(screenExitModel.ExitDirection, connectedScreen);
                    connectedScreen.ConnectedScreens.Add(screenExitModel.ExitDirection.GetOpposite(), screen);
                }
            }
        }

        private static void OnExitMapBounds(Player player, Direction exitDirection)
        {
            Screen.Screen currentScreen = CurrentScreen;
            Screen.Screen screenToLoad = currentScreen.GetMap(exitDirection);

            if (!screenToLoad) return;
            
            currentScreen.gameObject.SetActive(false);
            screenToLoad.gameObject.SetActive(true);

            SetPlayerPosition(player, currentScreen, screenToLoad, exitDirection);

            MapLoaded?.Invoke(screenToLoad);
            CurrentScreen = screenToLoad;
        }

        private static void SetPlayerPosition(Player player, Screen.Screen currentScreen, Screen.Screen screenToLoad, Direction exitDirection)
        {
            float offset = Physics2D.defaultContactOffset * 2;
            Vector2 playerPos = (player.Position / currentScreen.Size) * screenToLoad.Size;
	
            Vector2 minPos = new Vector2(player.Extents.x + offset, offset);
            Vector2 maxPos = new Vector2(screenToLoad.Size.x - player.Extents.x - offset, screenToLoad.Size.y - 2 * player.Extents.y - offset);

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
