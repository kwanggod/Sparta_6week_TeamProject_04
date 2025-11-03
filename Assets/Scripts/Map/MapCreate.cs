using UnityEngine;
using System.Collections.Generic;

public class MapManager : MonoBehaviour
{
    public GameObject[] mapPrefabs;
    private List<GameObject> mapPool = new List<GameObject>();

    private float spawnX = 60f;
    private GameObject currentMap;

    void Start()
    {
        InitializePool();
        ActivateRandomMap();
        GameManager.instance.StartGame();
    }

    void Update()
    {
        if (currentMap != null && currentMap.transform.position.x <= 0f)
        {
            ActivateRandomMap();
        }
    }

    void InitializePool()
    {
        for (int i = 0; i < mapPrefabs.Length; i++)
        {
            GameObject obj = Instantiate(mapPrefabs[i], transform);
            obj.SetActive(false);
            mapPool.Add(obj);
        }
    }

    void ActivateRandomMap()
    {
        GameObject map = GetInactiveMap();
        if (map != null)
        {
            map.transform.localPosition = new Vector3(spawnX, 0, 0);
            map.SetActive(true);
            currentMap = map;
        }
    }

    GameObject GetInactiveMap()
    {
        List<GameObject> inactiveMaps = new List<GameObject>();

        foreach (GameObject obj in mapPool)
        {
            if (!obj.activeInHierarchy)
                inactiveMaps.Add(obj);
        }

        if (inactiveMaps.Count > 0)
        {
            int randIndex = Random.Range(0, inactiveMaps.Count);
            return inactiveMaps[randIndex];
        }

        return null;
    }
}
