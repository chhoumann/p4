using System.Collections.Generic;
using Dazel.IntermediateModels;
using UnityEngine;

namespace Dazel.Game
{
    public sealed class FloorGenerator : MonoBehaviour, ITilemapGenerator
    {
        public void Generate(Vector2Int tilemapSize, List<TileModel> tiles)
        {
            Vector3Int position = new Vector3Int();
            
            foreach (TileModel tileModel in tiles)
            {
                position.x = tileModel.X;
                position.y = tileModel.Y;

                Texture2D texture = GameManager.Instance.GfxLoader.LoadTile(tileModel.GraphicName);
                
                GameObject tile = new GameObject();
                tile.transform.SetParent(transform);
                tile.transform.position = position;

                Rect rect = new Rect(0, 0, texture.width, texture.height);
                Vector2 pivot = new Vector2(0, 0);
                
                SpriteRenderer spriteRenderer = tile.AddComponent<SpriteRenderer>();
                spriteRenderer.sprite = Sprite.Create(texture, rect, pivot, GameManager.PixelsPerUnit);
                spriteRenderer.sortingLayerName = SortingLayers.Ground;
            }
        }
    }
}
