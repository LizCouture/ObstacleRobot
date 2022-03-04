using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetController : MonoBehaviour
{
    [SerializeField]
    GameObject targetPrefab;
    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Start()
    {
     if (!targetPrefab) {
            Debug.Log("No target prefab to instantiate.");
        }
        ensureOneTargetInScene();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setTargetLocation(Vector3 targetLocation) {
        spawnTarget(targetLocation);
        ensureOneTargetInScene();
    }

    void ensureOneTargetInScene() {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        if (targets.Length > 1) {
            for(int i = 1; i <= targets.Length; i++) {
                Destroy(targets[i]);
            }
        } else if (targets.Length == 0) {
            spawnTarget(randomLocation());
        }
    }

    void spawnTarget(Vector3 spawnLocation) {
        Instantiate(targetPrefab, spawnLocation, Quaternion.identity);
    }

    Vector3 randomLocation() {
        // This is a magic number but I'm lazy.  It's a point 2 units off the ground, at a random point within the size of the ground plane.
        return new Vector3(Random.Range(-38, 38), 2, Random.Range(-38, 38));
    }
}
