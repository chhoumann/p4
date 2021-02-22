using UnityEngine;
using UnityEngine.Tilemaps;

namespace P4.MapGenerator
{
    public sealed class TilemapGeneratorTest : MonoBehaviour
    {
        [SerializeField] private TileBase[] grassTiles;
        
        private void Start()
        {
            Tilemap tilemap = GetComponent<Tilemap>();

            for (int y = 0; y < 24; y++)
            {
                for (int x = 0; x < 24; x++)
                {
                    tilemap.SetTile(new Vector3Int(x, y, 0), grassTiles[Random.Range(0, grassTiles.Length)]);
                }
            }
        }
    }
}
