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
            Bounds bounds = other.bounds;

            Vector3 min = bounds.min;
            Vector3 max = bounds.max;

            for (float x = min.x; x <= max.x; x += 0.3f)
            {
                for (float y = min.y; y <= max.y; y += 0.3f)
                {
                    Vector3Int cellPos = itemTilemap.WorldToCell(new Vector3(x, y, 0));
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
                            GameManager.instance.BoostSpeed(4f, 2f);//�뷱�� ���� ù��° �ӵ�, �ι�° ��
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
    }
}