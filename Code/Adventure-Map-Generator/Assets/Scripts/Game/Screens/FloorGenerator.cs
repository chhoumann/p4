using Dazel.Game.Core;
using Dazel.IntermediateModels;
using UnityEngine;

namespace Dazel.Game.Screens
{
    public sealed class FloorGenerator : MonoBehaviour, ITilemapGenerator
    {
        [SerializeField] private GameObject tileTemplate;
        
        public void Generate(IGenerator generator)
        {
            const int ppu = GameManager.PixelsPerUnit;
            Vector3Int position = new Vector3Int();
            
            foreach (TileModel tileModel in generator.Tiles)
            {
                position.x = tileModel.X;
                position.y = tileModel.Y;

                Texture2D texture = GameManager.Instance.GfxLoader.LoadGraphic(tileModel.GraphicName);

                GameObject tile = Instantiate(tileTemplate, transform);
                tile.transform.localPosition = position;

                Rect rect = new Rect(0, 0, texture.width, texture.height);
                Vector2 pivot = new Vector2(0, 0);
                
                SpriteRenderer spriteRenderer = tile.GetComponent<SpriteRenderer>();
                spriteRenderer.sprite = Sprite.Create(texture, rect, pivot, ppu);
                spriteRenderer.sortingLayerName = SortingLayers.Ground;

                tile.transform.localScale = new Vector3(
                    ppu / spriteRenderer.sprite.rect.width, 
                    ppu / spriteRenderer.sprite.rect.height
                );
            }
        }
    }
}
