using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LauncherBallon : MonoBehaviour
{
    [SerializeField] private GameObject ballonCollection;
    [SerializeField] private GameObject ballonPrefab;
    [SerializeField] private Transform directionLeft;
    [SerializeField] private Transform directionRight;
    [SerializeField] private float launchForce = 15f;

    public void Launch()
    {
        GameObject ballon = Instantiate(ballonPrefab, transform.position, Quaternion.identity, ballonCollection.transform);

        GameObject ball = ballon.transform.GetChild(0).gameObject;
        Rigidbody ballBody = ball.GetComponentInChildren<Rigidbody>();


        Vector3 launchDirection;

        if ( Random.value > 0.5 )
        {
            launchDirection = directionLeft.position - transform.position;
        } else
        {
            launchDirection = directionRight.position - transform.position;
        }

        ballBody.AddForce(launchDirection.normalized * launchForce, ForceMode.Impulse);
    }
}
