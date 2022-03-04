using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [SerializeField]
    GameObject robot;
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject == robot)
            Destroy(gameObject);
    }
}
