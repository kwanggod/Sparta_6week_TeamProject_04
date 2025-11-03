using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Item : MonoBehaviour
{
    private Tilemap itemTilemap;
    private Dictionary<Vector3Int, TileBase> originalTiles = new Dictionary<Vector3Int, TileBase>();
    private ItemSFX sfx;

    private void Awake()
    {
        itemTilemap = GetComponent<Tilemap>();
        sfx = GetComponent<ItemSFX>();
    }
    private void Start()
    {
        BoundsInt bounds = itemTilemap.cellBounds;
        foreach (Vector3Int pos in bounds.allPositionsWithin)
        {
            TileBase tile = itemTilemap.GetTile(pos);
            if (tile != null)
            {
                originalTiles[pos] = tile;
            }
        }
    }
    private void OnEnable()
    {
        // 타일맵 복원
        foreach (var pair in originalTiles)
        {
            itemTilemap.SetTile(pair.Key, pair.Value);
        }
    }


    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (GameManager.instance.IsPlaying)
            {
                Bounds bounds = other.bounds;

                Vector3 min = bounds.min;
                Vector3 max = bounds.max;

                for (float x = min.x; x <= max.x; x += 0.3f)
                {
                    for (float y = min.y; y <= max.y; y += 0.3f)
                    {
                        Vector3Int cellPos = itemTilemap.WorldToCell(new Vector3(x, y, 0));
                        TileBase tile = itemTilemap.GetTile(cellPos);
                        if (tile == null) continue;

                        string tileName = tile.name;

                        switch (tileName)
                        {
                            case "gem_blue":
                                GameManager.instance.AddScore(1000);
                                break;
                            case "gem_red":
                                GameManager.instance.AddScore(5000);
                                break;
                            case "gem_green":
                                GameManager.instance.AddScore(10000);
                                break;
                            case "gem_yellow":
                                GameManager.instance.AddScore(15000);
                                break;
                            case "conveyor":
                                GameManager.instance.BoostSpeed(4f, 2f);
                                break;
                            case "hud_heart":
                                other.GetComponent<PlayerMove>().Heal(10);
                                break;
                        }
                        sfx?.PlayItemSound();
                        itemTilemap.SetTile(cellPos, null);
                    }
                }
            }
        }
    }
}
