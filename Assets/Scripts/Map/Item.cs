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

                var sfx = GetComponent<ItemSFX>();

                if (tileName == "gem_blue")
                {
                    GameManager.instance.AddScore(1000);//�뷱�� ����
                    Debug.Log(GameManager.instance.score);
                    sfx?.PlayItemSound();
                }
                if (tileName == "gem_red")
                {
                    GameManager.instance.AddScore(5000);//�뷱�� ����
                    Debug.Log(GameManager.instance.score);
                    sfx?.PlayItemSound();
                }
                else if (tileName == "conveyor")
                {
                    GameManager.instance.BoostSpeed(5f, 5f);//�뷱�� ���� ù��° �ӵ�, �ι�° ��
                    Debug.Log("���ǵ� ����");
                    sfx?.PlayItemSound();
                }
                else if (tileName == "hud_heart")
                {
                    other.GetComponent<PlayerMove>().Heal(10);//�뷱�� ����
                    Debug.Log("ü��ȸ��");
                    sfx?.PlayItemSound();
                }

                itemTilemap.SetTile(cellPos, null);

            }

        }
    }
}
