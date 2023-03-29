using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OwnGoalkeeperHit : MonoBehaviour
{
    [SerializeField] private float repulseForce = 20f;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null)
        {
            return;
        }

        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody rb = other.gameObject.GetComponent<Rigidbody>();
            rb.Sleep();
            rb.AddForce(Vector3.up * repulseForce, ForceMode.Impulse);
        }
    }
}
