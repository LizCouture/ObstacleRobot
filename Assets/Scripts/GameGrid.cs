using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameGrid : MonoBehaviour
{
    int height = 10;
    int width = 10;
    float gridSpaceSize = 1f;

    [SerializeField]
    GameObject gridCellPrefab;

    GameObject[,] gameGrid;

    public int GetHeight() {
        return height;
    }

    public int GetWidth() {
        return width;
    }

    // Start is called before the first frame update
    void Awake()
    {
        createGrid(); 
    }

    void createGrid() {
        gameGrid = new GameObject[height, width];
        if (gridCellPrefab) {
            for (int y = 0; y < height; y++) {
                for (int x = 0; x < width; x++) {
                    gameGrid[x, y] = Instantiate(gridCellPrefab,
                        new Vector3(x * gridSpaceSize, 0, y * gridSpaceSize),
                        Quaternion.identity);
                    gameGrid[x, y].GetComponent<GridCell>().SetPosition(x, y);
                    gameGrid[x, y].transform.parent = transform;
                    gameGrid[x, y].gameObject.name = "Grid Space( X: " + x.ToString() + " Y: " + y.ToString() + " )";
                }
            }
        }
    }

    public Vector2Int GetGridPosFromWorld(Vector3 worldPosition) {
        int x = Mathf.FloorToInt(worldPosition.x / gridSpaceSize);
        int y = Mathf.FloorToInt(worldPosition.y / gridSpaceSize);

        x = Mathf.Clamp(x, 0, width);
        y = Mathf.Clamp(x, 0, height);

        return new Vector2Int(x, y);
    }

    public Vector3 GetWorldPosFromGridPos(Vector2Int gridPos) {
        float x = gridPos.x * gridSpaceSize;
        float y = gridPos.y * gridSpaceSize;

        return new Vector3(x, y, 0);
    }

    public void InstantiateObjectOnGrid(GameObject go, Vector2Int gridPos) {
        gameGrid[gridPos.x, gridPos.y].GetComponent<GridCell>().instantiateObjectOnCell(go);
    }

    // Accepts an object THAT IS NOT YET ON THE GRID and moves it to a specific cell
    public void MoveObjectToCell(GameObject go, Vector2Int to) {
        gameGrid[to.x, to.y].GetComponent<GridCell>().moveObjectToCell(go);
    }

    // Moves the object on a given occupied cell to another cell
    public void MoveObjectFromCellToCell(Vector2Int from, Vector2Int to) {
        GridCell fromCell = gameGrid[from.x, from.y].GetComponent<GridCell>();

        if (!fromCell.isOccupied) {
            Debug.LogError("ERROR:  Attempted to move Object from empty cell " + from);
        } else {
            GameObject objectOnCell = fromCell.objectInThisGridSpace;
            gameGrid[to.x, to.y].GetComponent<GridCell>().moveObjectToCell(objectOnCell);
            fromCell.removeObjectFromCell();
        }
    }
}
