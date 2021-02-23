using System.Collections;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace P4.MapGenerator
{
    public sealed class TilemapGeneratorTest : MonoBehaviour, ITilemapGenerator
    {
        [SerializeField] private TileBase[] grassTiles;

        public void Generate(Vector2Int tilemapSize)
        {
            StartCoroutine(GenerateDelayed(tilemapSize));
            /*Tilemap tilemap = GetComponent<Tilemap>();
            Vector3Int position = new Vector3Int();
            
            for (int y = 0; y < tilemapSize.y; y++)
            {
                for (int x = 0; x < tilemapSize.x; x++)
                {
                    TileBase tile = grassTiles[Random.Range(0, grassTiles.Length)];
                    position.x = x;
                    position.y = y;
                    
                    tilemap.SetTile(position, tile);
                }
            }*/
        }

        private IEnumerator GenerateDelayed(Vector2Int tilemapSize)
        {
            Tilemap tilemap = GetComponent<Tilemap>();
            Vector3Int position = new Vector3Int();

            YieldInstruction wait = new WaitForSeconds(0.05f);
            
            for (int y = 0; y < tilemapSize.y; y++)
            {
                for (int x = 0; x < tilemapSize.x; x++)
                {
                    TileBase tile = grassTiles[Random.Range(0, grassTiles.Length)];
                    position.x = x;
                    position.y = y;
                    
                    tilemap.SetTile(position, tile);

                    yield return wait;
                }
            }
        } 
    }
}
