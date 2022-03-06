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

    [SerializeField]
    private LineRenderer line;

    [SerializeField]
    int extraForce = 3;

    private Rigidbody rb;
    private Animator anim;

    private bool gridMode = false;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        line.SetPosition(0, transform.position);
        line.SetPosition(1, transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (!gridMode) {
            line.SetPosition(0, transform.position);
            if (target) {
                rotateToFaceTarget();

                moveTowardsTarget();
                line.SetPosition(1, target.transform.position);
            } else {
                line.SetPosition(1, transform.position);
                GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
                if (targets.Length >= 1) {
                    target = targets[0];
                    line.SetPosition(1, target.transform.position);
                }
            }
        }
    }

    void rotateToFaceTarget() {
        Vector3 targetDir = transform.position - target.transform.position;
        Vector3 forward = transform.forward;
        Vector3 localTarget = transform.InverseTransformPoint(target.transform.position);

        float angle = Mathf.Atan2(localTarget.x, localTarget.z) * Mathf.Rad2Deg;

        Vector3 eulerAngleVelocity = new Vector3(0, angle, 0);
        Quaternion deltaRotation = Quaternion.Euler(eulerAngleVelocity * rotationSpeed * Time.deltaTime);
        rb.MoveRotation(rb.rotation * deltaRotation);
    }

    void moveTowardsTarget() {
        anim.SetTrigger("startRunning");
        Vector3 movementVector = transform.forward;
        movementVector = movementVector.normalized * moveSpeed * Time.deltaTime;
        rb.MovePosition(transform.position + movementVector);
    }

    public void FoundTarget() {
        anim.SetTrigger("foundTarget");
    }

    public void SetGridMode(bool mode) {
        gridMode = mode;
    }

    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.tag == "Obstacle") {
            Vector3 dir = collision.contacts[0].point - transform.position;
            collision.gameObject.GetComponent<Rigidbody>().AddForce(dir * extraForce);
        }
    }
}
