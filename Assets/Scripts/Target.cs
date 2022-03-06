using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    GameObject robot;

    private void Start() {
        robot = GameObject.FindGameObjectWithTag("Robot");
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject == robot)
            robot.GetComponent<Robot>().FoundTarget();
            Destroy(gameObject);
    }
}
