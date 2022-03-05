using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    GameGrid gameGrid;
    [SerializeField]
    LayerMask gridLayer;

    private void Start() {
        gameGrid = FindObjectOfType<GameGrid>();
    }

    private void Update() {
        GridCell cellMouseIsOver = mouseOverAGridSpace();
        if (cellMouseIsOver != null) {
            if (Input.GetMouseButtonDown(0)) {
                cellMouseIsOver.GetComponent<MeshRenderer>().material.color = Color.green;
            }
        }
    }

    private GridCell mouseOverAGridSpace() {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hitInfo, 100f, gridLayer)) {
            return hitInfo.transform.GetComponent<GridCell>();
        } else {
            return null;
        }
    }
}
