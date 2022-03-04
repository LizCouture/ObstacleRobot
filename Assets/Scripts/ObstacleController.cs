using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    GameObject[] obstaclePrefabs;
    [SerializeField]
    GameObject obstacleParent;

    public void spawnObstacle(Vector3 spawnLocation) {
        GameObject prefabToSpawn = randomPrefab();
        GameObject newObstacle = Instantiate(prefabToSpawn, spawnLocation, prefabToSpawn.transform.rotation);
        newObstacle.transform.SetParent(obstacleParent.transform);
    }

    GameObject randomPrefab() {
        return obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
    }
}
