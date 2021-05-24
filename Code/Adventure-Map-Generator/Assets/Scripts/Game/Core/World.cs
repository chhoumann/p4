using System;
using System.Collections.Generic;
using Dazel.Game.Entities;
using Dazel.Game.Screens;
using Dazel.IntermediateModels;
using UnityEngine;

namespace Dazel.Game.Core
{
    public sealed class World : MonoBehaviour
    {
        [SerializeField] private GameObject screenTemplate;
        [SerializeField] private GameObject entityTemplate;
        [SerializeField] private Transform screenContainer;

        public static event Action<GameScreen> ScreenLoaded;
        
        public static GameScreen CurrentScreen { get; private set; }
        
        public static IEnumerable<ScreenModel> ScreenModels { get; set; }
        
        private void Awake()
        {
            if (ScreenModels == null) return;
            
            Dictionary<string, GameScreen> screens = CreateScreens();

            ConnectScreens(screens);
            SpawnEntities(screens);
        }
        
        private void OnEnable()
        {
            ScreenBorder.PlayerExitedBounds += OnExitMapBounds;
        }

        private void OnDisable()
        {
            ScreenBorder.PlayerExitedBounds -= OnExitMapBounds;
        }

        private Dictionary<string, GameScreen> CreateScreens()
        {
            Dictionary<string, GameScreen> screens = new Dictionary<string, GameScreen>();

            foreach (ScreenModel screenModel in ScreenModels)
            {
                GameObject newScreen = Instantiate(screenTemplate, screenContainer);
                newScreen.transform.name = screenModel.Identifier;

                GameScreen screen = newScreen.GetComponent<GameScreen>().Setup(screenModel);

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

        private static void ConnectScreens(IReadOnlyDictionary<string, GameScreen> screens)
        {
            foreach (ScreenModel screenModel in ScreenModels)
            {
                GameScreen screen = screens[screenModel.Identifier];
                foreach (ScreenExitModel screenExitModel in screenModel.ScreenExits)
                {
                    GameScreen connectedScreen = screens[screenExitModel.ConnectedScreenIdentifier];

                    screen.ConnectedScreens.Add(screenExitModel.ExitDirection, connectedScreen);
                    connectedScreen.ConnectedScreens.Add(screenExitModel.ExitDirection.GetOpposite(), screen);
                }
            }
        }

        private void SpawnEntities(IReadOnlyDictionary<string, GameScreen> screens)
        {
            const int ppu = GameManager.PixelsPerUnit;
            
            foreach (ScreenModel screenModel in ScreenModels)
            {
                foreach (EntityModel entityModel in screenModel.Entities)
                {
                    Texture2D entityTexture = GameManager.Instance.GfxLoader.LoadGraphic(entityModel.Identifier + ".png");

                    GameObject entity = Instantiate(entityTemplate, screens[screenModel.Identifier].transform);
                    entity.name = entityModel.Identifier;
                    entity.transform.localPosition = new Vector3(entityModel.SpawnPosition.X + 0.5f, entityModel.SpawnPosition.Y);
                    
                    Rect rect = new Rect(0, 0, entityTexture.width, entityTexture.height);
                    Vector2 pivot = new Vector2(0.5f, 0);
                
                    SpriteRenderer spriteRenderer = entity.GetComponent<SpriteRenderer>();
                    spriteRenderer.spriteSortPoint = SpriteSortPoint.Pivot;
                    spriteRenderer.sprite = Sprite.Create(entityTexture, rect, pivot, ppu);

                    BoxCollider2D collision = entity.AddComponent<BoxCollider2D>();
                    collision.size = new Vector2(collision.size.x, collision.size.y * 0.5f);
                    collision.offset = new Vector2(0, collision.size.y * 0.5f);
                }
            }
        }

        private static void OnExitMapBounds(Player player, Direction exitDirection)
        {
            GameScreen currentScreen = CurrentScreen;
            GameScreen screenToLoad = currentScreen.GetMap(exitDirection);

            if (!screenToLoad) return;
            
            currentScreen.gameObject.SetActive(false);
            screenToLoad.gameObject.SetActive(true);

            SetPlayerPosition(player, currentScreen, screenToLoad, exitDirection);

            ScreenLoaded?.Invoke(screenToLoad);
            CurrentScreen = screenToLoad;
        }

        private static void SetPlayerPosition(Player player, GameScreen currentScreen, GameScreen screenToLoad, Direction exitDirection)
        {
            float offset = Physics2D.defaultContactOffset * 3;
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
