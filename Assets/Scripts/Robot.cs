using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [SerializeField]
    private float moveSpeed;
    [SerializeField]
    private float rotationSpeed;
    [SerializeField]
    private GameObject target;

    private Rigidbody rb;
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target) {
            RotateToFaceTarget();

            MoveTowardsTarget();
        } else {
            GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
            if (targets.Length >= 1) {
                target = targets[0];
            }
        }
    }

    void RotateToFaceTarget() {
        Vector3 targetDir = transform.position - target.transform.position;
        Vector3 forward = transform.forward;
        Vector3 localTarget = transform.InverseTransformPoint(target.transform.position);

        float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * rotationSpeed * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    void MoveTowardsTarget() {
        anim.SetTrigger("startRunning");
        Vector3 movementVector = transform.forward;
        movementVector = movementVector.normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movementVector);
    }

    public void foundTarget() {
        anim.SetTrigger("foundTarget");
    }
}
