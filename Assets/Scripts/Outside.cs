using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Outside : MonoBehaviour
{
    public event EventHandler BallonLosed;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        if (!other.gameObject.CompareTag("Ball")) return;

        if (GameManager.Instance.State != GameManager.GameState.PLAYING) return;

        RemoveBallon ballonRemover = other.gameObject.transform.parent.gameObject.GetComponent<RemoveBallon>();

        ballonRemover.StartRemoveBallon();

        if (BallonLosed != null)
        {
            BallonLosed.Invoke(this, new EventArgs());
        }
    }
}
