using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PusherHit : MonoBehaviour
{
    [SerializeField] private float repulseForce = 30f;
    [SerializeField] private Transform center;
    [SerializeField] private bool isCanceller = false;

    private void Start()
    {
        if (center == null)
        {
            center = transform;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other == null) {
            return;
        }

        if ( other.gameObject.CompareTag("Ball") )
        {
            Vector3 direction = other.gameObject.transform.position - center.position;

            // uso un vector de tres posiciones para jugar con el valor del elemento de la altura.
            // si el centro del pusher esta mas abajo que el de la esfera, entonces la fuerza hara que la pelota salte.
            // esto genera mas dinamismo al juego
            // mientras mas abajo este el pusher mas alto volara la pelota.

            if (isCanceller)
            {
                other.rigidbody.Sleep();
            }

            other.rigidbody.AddForce(direction.normalized * repulseForce, ForceMode.Impulse);
        }
    }
}
