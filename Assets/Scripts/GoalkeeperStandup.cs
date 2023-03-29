using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalkeeperStandup : MonoBehaviour
{
    [SerializeField] float fixAngle = 0f;

    Quaternion targetRotation;
    private void Start()
    {
        targetRotation = Quaternion.Euler(transform.rotation.eulerAngles.x, 0f, fixAngle);
    }

    void Update()
    {
        Quaternion newRotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1400 * Time.deltaTime).normalized;
        transform.rotation = newRotation;
    }
}
