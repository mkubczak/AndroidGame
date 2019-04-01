using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TileManager : MonoBehaviour
{

    public GameObject[] tilePrefabs; // tablica obiektow 

    private Transform playerTransform;
    private float spawnZ = 0.0f; // miejsce poczatkowe
    private float tileLength = 47.8f; // dlusgosc generowanego segmentu
    private int amnTilesOnScreen = 6; // ilosc segmentow na scenie 
    private float safeZone = 63; // 
    private int lastPrefabIndex = 0;

    private List<GameObject> activeTiles;

    private void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        activeTiles = new List<GameObject>();

        for (int i = 0; i < amnTilesOnScreen; i++)
        {
            if (i < 2)
                SpawnTile(0);
            else
                SpawnTile();

        }
    }


    private void Update()
    {
        if (playerTransform.position.z - safeZone > (spawnZ - amnTilesOnScreen * tileLength))
        {
            SpawnTile();
            DeleteTile();
        }
    }

    private void SpawnTile(int prefabIndex = -1)
    {
        GameObject go;
        if (prefabIndex == -1)
            go = Instantiate(tilePrefabs[RandomPrefabIndex()]) as GameObject;
        else
        go = Instantiate(tilePrefabs[prefabIndex]) as GameObject;
        go.transform.SetParent(transform);
        go.transform.position = Vector3.forward * spawnZ;
        spawnZ += tileLength;
        activeTiles.Add(go);
    }

    private void DeleteTile()
    {
        Destroy(activeTiles[0]);
        activeTiles.RemoveAt(0);
    }

    private int RandomPrefabIndex()
    {
        if (tilePrefabs.Length <= 1)
            return 0;

        int randomIndex = lastPrefabIndex;
        while (randomIndex == lastPrefabIndex)
        {
            randomIndex = Random.Range(0, tilePrefabs.Length);
        }

        lastPrefabIndex = randomIndex;
        return randomIndex;
    }
}
