using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleController : MonoBehaviour
{
    [SerializeField]
    GameObject[] obstaclePrefabs;
    [SerializeField]
    GameObject obstacleParent;

    public GameObject spawnObstacle(Vector3 spawnLocation) {
        GameObject prefabToSpawn = randomPrefab();
        GameObject newObstacle = Instantiate(prefabToSpawn, spawnLocation, prefabToSpawn.transform.rotation);
        newObstacle.transform.Rotate(0.0f, 0.0f, Random.Range(0.0f, 360.0f));
        newObstacle.transform.SetParent(obstacleParent.transform);
        return newObstacle;
    }

    GameObject randomPrefab() {
        return obstaclePrefabs[Random.Range(0, obstaclePrefabs.Length)];
    }
}
