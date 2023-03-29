using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallLandForce : MonoBehaviour
{
    [SerializeField] float landForce = 1f;

    Rigidbody body;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        body.AddForce(Vector3.forward * landForce, ForceMode.Force);
    }
}
