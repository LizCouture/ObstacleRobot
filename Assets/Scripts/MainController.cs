using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is responsible for keeping track of current game world, game state, and user interaction.
public class MainController : MonoBehaviour
{
    [SerializeField]
    TargetController targetController;
    [SerializeField]
    ObstacleController obstacleController;

    public bool setTarget;
    public bool spawnObstacle;

    Ray ray;
    RaycastHit hit;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit)) {
            if (Input.GetMouseButtonDown(0)) {
                Vector3 hitLocation = new Vector3(hit.point.x, 1.5f, hit.point.z);

                if (setTarget) {
                    targetController.setTargetLocation(hitLocation);
                }

                if (spawnObstacle) {
                    obstacleController.spawnObstacle(hitLocation);
                }
            }
        }
    }
}
