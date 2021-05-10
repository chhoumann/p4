using System;
using Dazel.Game.Entities;
using UnityEngine;

namespace Dazel.Game
{
    public sealed class World : MonoBehaviour
    {
        [SerializeField] private Transform mapContainer;

        public static event Action<Map> MapLoaded;
        
        public static Map Map { get; private set; }

        private Map[] maps;
        
        private void Awake()
        {
            maps = new Map[mapContainer.childCount];

            for (int i = 0; i < maps.Length; i++)
            {
                maps[i] = mapContainer.GetChild(i).GetComponent<Map>().Setup();
            }
            
            Map = maps[0];
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
            Map currentMap = Map;
            Map mapToLoad = currentMap.GetMap(exitDirection);

            if (!mapToLoad) return;
            
            currentMap.gameObject.SetActive(false);
            mapToLoad.gameObject.SetActive(true);

            SetPlayerPosition(player, currentMap, mapToLoad, exitDirection);

            MapLoaded?.Invoke(mapToLoad);
            Map = mapToLoad;
        }

        private void SetPlayerPosition(Player player, Map currentMap, Map mapToLoad, Direction exitDirection)
        {
            float offset = Physics2D.defaultContactOffset;
            Vector2 playerPos = (player.Position / currentMap.Size) * mapToLoad.Size;
            
            Vector2 minPos = new Vector2(player.Extents.x, player.Extents.y);
            Vector2 maxPos = new Vector2(mapToLoad.Size.x - player.Extents.x, mapToLoad.Size.y - player.Extents.y);

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
