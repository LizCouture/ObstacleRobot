using System;
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

    public bool gridMode = true;

    private GameObject robot;
    private GameGrid gameGrid;

    Ray ray;
    RaycastHit hit;

    // Start is called before the first frame update
    void Awake()
    {
        Debug.Log("Finding robot:");
        robot = GameObject.FindObjectOfType<Robot>().gameObject;
        robot.GetComponent<Robot>().SetGridMode(gridMode);
        targetController.SetGridMode(gridMode);

        Debug.Log("Robot: " + robot);
        if (gridMode) {
            gameGrid = GameObject.FindObjectOfType<GameGrid>().GetComponent<GameGrid>();
        }
    }

    private void Start() {
        if (gridMode) {
            placeRobotOnGrid();
            placeTargetOnGrid();

            populateGridWithObstacles();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!gridMode) {
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

    void placeRobotOnGrid() {
        Debug.Log("gameGrid: " + gameGrid);
        Debug.Log("robot: " + robot);
        Vector2Int origin = new Vector2Int(0, 0);
        gameGrid.MoveObjectToCell(robot, origin);
    }

    void placeTargetOnGrid() {
        Vector2Int targetCell = new Vector2Int(gameGrid.GetHeight() - 1, gameGrid.GetWidth() - 1);
        Debug.Log("TargetCell: " + targetCell);
        gameGrid.MoveObjectToCell(targetController.setTargetLocation(gameGrid.GetWorldPosFromGridPos(targetCell)), targetCell);
    }

    void populateGridWithObstacles() {
        int numObstacles = 10;

        Vector2Int[] obstacleLocations = new Vector2Int[numObstacles];
        for (int i = 0; i < numObstacles; i++) {
            Vector2Int randomLocation = new Vector2Int(UnityEngine.Random.Range(0, gameGrid.GetWidth()), UnityEngine.Random.Range(0, gameGrid.GetHeight()));
            while (Array.IndexOf(obstacleLocations, randomLocation) > -1) {
                randomLocation = new Vector2Int(UnityEngine.Random.Range(0, gameGrid.GetWidth()), UnityEngine.Random.Range(0, gameGrid.GetHeight()));
            }
            obstacleLocations[i] = randomLocation;
            GameObject obstacle = obstacleController.spawnObstacle(gameGrid.GetWorldPosFromGridPos(randomLocation));
            gameGrid.MoveObjectToCell(obstacle, randomLocation);
        }
    }
}
