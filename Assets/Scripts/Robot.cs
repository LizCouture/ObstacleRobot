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

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target) {
            RotateToFaceTarget();

            MoveTowardsTarget();
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
        Vector3 movementVector = transform.forward;
        movementVector = movementVector.normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movementVector);
    }
}
