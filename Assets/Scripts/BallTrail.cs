using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallTrail : MonoBehaviour
{
    TrailRenderer trail;
    Rigidbody body;

    [SerializeField] private float maxVelocity = 60f;
    [SerializeField] private float minVelocity = 10f;
    [SerializeField] private float maxTime = 3;

    void Start()
    {
        trail = GetComponent<TrailRenderer>();
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        trail.AddPosition(transform.position);
        trail.time = maxTime + 5;

        float vel = new Vector2( body.velocity.x, body.velocity.y).magnitude;

        vel = Mathf.Clamp(vel, minVelocity, maxVelocity);
        trail.time = vel * maxTime / (maxVelocity - minVelocity);
    }
}
