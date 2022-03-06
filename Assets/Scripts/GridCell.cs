using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridCell : MonoBehaviour
{
    int posX;
    int posY;
    Vector3 offset = new Vector3( 0, 0.5f, 0);

    public GameObject objectInThisGridSpace = null;

    public bool isOccupied = false;

    public Color occupiedColor = Color.cyan;
    public Color unoccupiedColor = Color.gray;

    private void Start() {
        //gameObject.GetComponent<MeshRenderer>().material.color = unoccupiedColor;
    }

    public void SetPosition(int x, int y) {
        posX = x;
        posY = y;
    }

    public Vector2Int GetPosition() {
        return new Vector2Int(posX, posY);
    }

    public void instantiateObjectOnCell(GameObject prefab) {
        if (isOccupied) {
            Debug.LogError("Attempted to instantiateObjectOnCell, but cell " + GetPosition() + " is already occupied.");
        } else {
            objectInThisGridSpace = Instantiate(prefab, gameObject.transform.position + offset,
                Quaternion.identity);
            isOccupied = true;
            gameObject.GetComponent<MeshRenderer>().material.color = occupiedColor;
        }
    }

    public void moveObjectToCell(GameObject go) {
        objectInThisGridSpace = go;
        go.transform.position = gameObject.transform.position + offset;
        isOccupied = true;
        gameObject.GetComponent<MeshRenderer>().material.color = occupiedColor;
        Debug.Log("Set color to " + occupiedColor);
    }

    public void removeObjectFromCell() {
        objectInThisGridSpace = null;
        isOccupied = false;
        gameObject.GetComponent<MeshRenderer>().material.color = unoccupiedColor;
    }
}
