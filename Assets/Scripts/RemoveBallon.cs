using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoveBallon : MonoBehaviour
{
    public event EventHandler BallonRemoved;

    [SerializeField] private float decelerationRate = 20f;

    private bool start = false;
    Rigidbody ballrb;

    private void Start()
    {
         ballrb = GetComponentInChildren<Rigidbody>();
    }

    private void Update()
    {
        if (!start) { return; }

        if (ballrb.velocity.magnitude > 0f)
        {
            float currentSpeed = ballrb.velocity.magnitude;
            float newSpeed = Mathf.Lerp(currentSpeed, 0f, decelerationRate * Time.deltaTime);
            ballrb.velocity = ballrb.velocity.normalized * newSpeed;
        }
    }

    public void StartRemoveBallon()
    {
        List<RendererFadeOut> children = new List<RendererFadeOut>(GetComponentsInChildren<RendererFadeOut>());

        children.ForEach(c =>
        {
            c.StartFadeOut();
            c.Faded += Children_Faded;
        });

        ballrb.useGravity = false;
    }

    private void Children_Faded(object sender, System.EventArgs e)
    {
        if (BallonRemoved != null)
        {
            BallonRemoved.Invoke(this, new EventArgs());
        }
        Destroy(gameObject);
    }
}
