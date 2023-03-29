using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallBorder : MonoBehaviour
{
    [SerializeField] private GameObject ball;

    private void Update()
    {
        transform.position = ball.transform.position;
    }
}
