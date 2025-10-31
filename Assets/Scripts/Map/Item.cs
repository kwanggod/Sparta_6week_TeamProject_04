using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Item : MonoBehaviour
{
    private Tilemap itemTilemap;

    private void Start()
    {
        itemTilemap = GetComponent<Tilemap>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Vector3 playerPosition = other.transform.position;
            Vector3Int cellPos = itemTilemap.WorldToCell(playerPosition);

            TileBase tile = itemTilemap.GetTile(cellPos);
            if (tile != null)
            {
                itemTilemap.SetTile(cellPos, null);
                GameManager.instance.AddScore(1000);
                Debug.Log(GameManager.instance.score);
            }

        }
    }
}
