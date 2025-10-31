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
                string tileName = tile.name;

                if (tileName == "gem_blue")
                {
                    GameManager.instance.AddScore(1000);
                    Debug.Log(GameManager.instance.score);
                }
                else if (tileName == "conveyor")
                {
                    GameManager.instance.BoostSpeed(5f,5f);
                    Debug.Log("���ǵ� ����");
                }
                else if (tileName == "hud_heart")
                {
                    //other.GetComponent<PlayerMove>().Heal(20);
                    Debug.Log("ü��ȸ��");
                }

                itemTilemap.SetTile(cellPos, null);
                
            }

        }
    }
}
