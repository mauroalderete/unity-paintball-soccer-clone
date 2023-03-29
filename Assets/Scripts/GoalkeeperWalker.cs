using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperWalker : MonoBehaviour
{
    [SerializeField] private float speed = 3.0f;
    [SerializeField] private float minAngle = 1.0f;
    [SerializeField] private float maxAngle = 1.0f;

    private float boardX;

    Quaternion targetRotationFrom;
    Quaternion targetRotationTo;
    Quaternion targetRotation;

    Quaternion startRotation;

    Rigidbody rb;

    bool start = false;

    enum Direction
    {
        CLOCKWISE,
        COUNTERCLOCKWISE,
    }

    Direction dir;

    void Start()
    {
        boardX = transform.rotation.eulerAngles.x;
        startRotation = transform.rotation;

        targetRotationFrom = Quaternion.Euler(boardX, 0f, minAngle);
        targetRotationTo = Quaternion.Euler(boardX, 0f, maxAngle);

        rb = GetComponent<Rigidbody>();

        Restart();

        GameManager.Instance.StateChanged += Instance_StateChanged;
    }

    private void Instance_StateChanged(object sender, System.EventArgs e)
    {
        if (GameManager.Instance.State == GameManager.GameState.LAUNCHING_BALLON)
        {
            Restart();
        }

        if (GameManager.Instance.State == GameManager.GameState.PLAYING)
        {
            StartWalk();
        }
    }

    private void Restart()
    {
        start = false;

        rb.rotation = startRotation;

        dir = Random.value > 0.5f ? Direction.CLOCKWISE : Direction.COUNTERCLOCKWISE;

        if (dir == Direction.CLOCKWISE)
        {
            targetRotation = targetRotationTo;
        }
        else
        {
            targetRotation = targetRotationFrom;
        }
    }

    private void StartWalk()
    {
        start = true;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(!start) { return; }

        if( transform.rotation == targetRotation )
        {
            dir = dir == Direction.CLOCKWISE? Direction.COUNTERCLOCKWISE:Direction.CLOCKWISE;

            if (dir == Direction.CLOCKWISE)
            {
                targetRotation = targetRotationTo;
            }
            else
            {
                targetRotation = targetRotationFrom;
            }
        }

        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, speed * Time.deltaTime).normalized;

        rb.MoveRotation(newRotation);
    }
}
