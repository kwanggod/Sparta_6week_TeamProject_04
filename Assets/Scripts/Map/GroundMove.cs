using UnityEngine;

public class GroundMove : MonoBehaviour
{
    public GameObject[] mapPrefabs;
    private int mapWidth = 23;
    private int mapCount = 1;
    private float nextMap = -14f;
    private Transform groundTran;
    private float speedUp;

    void Start()
    {
        groundTran = transform;
        SpawnRandomMap();
        GameManager.instance.StartGame();
    }

    void Update()
    {
        groundTran.position -= new Vector3(GameManager.instance.groundSpeed * Time.deltaTime, 0, 0);
        speedUp += Time.deltaTime;
        if (speedUp >= 20f)
        {
            GameManager.instance.SpeedUp(0.5f);
            speedUp = 0;
        }
        if(groundTran.position.x <= nextMap)
        {
            SpawnRandomMap();
            nextMap -= 14f;
        }

    }

    void SpawnMap(GameObject prefab)
    {
        float pos = mapCount * mapWidth;
        Vector3 spawnPos = new Vector3(pos, 0, 0);
        GameObject map = Instantiate(prefab);
        map.transform.SetParent(transform, true);
        map.transform.localPosition = spawnPos;
        mapCount++;
    }

    void SpawnRandomMap()
    {
        int rand = Random.Range(0, mapPrefabs.Length);
        GameObject selectedPrefab = mapPrefabs[rand];
        SpawnMap(selectedPrefab);
    }
}
