using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CounterArc : MonoBehaviour
{
    public event EventHandler BallonEntered;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        if (!other.gameObject.CompareTag("Ball")) return;

        if (GameManager.Instance.State != GameManager.GameState.PLAYING) return;

        RemoveBallon ballonRemover = other.gameObject.transform.parent.gameObject.GetComponent<RemoveBallon>();

        ballonRemover.StartRemoveBallon();

        if (BallonEntered != null)
        {
            BallonEntered.Invoke(this, new EventArgs());
        }
    }
}
