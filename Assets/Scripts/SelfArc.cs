using System;
using UnityEngine;

public class SelfArc : MonoBehaviour
{
    public event EventHandler BallonLaunched;

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        if (!other.gameObject.CompareTag("Ball")) return;

        if (GameManager.Instance.State == GameManager.GameState.LAUNCHING_BALLON) return;

        Destroy(other.gameObject.transform.parent.gameObject, 1f);  // seria mejor una segunda zona fuera de la pantalla
    }

    private void OnTriggerExit(Collider other)
    {
        if (other == null) return;

        if (!other.gameObject.CompareTag("Ball")) return;

        if (GameManager.Instance.State != GameManager.GameState.LAUNCHING_BALLON) return;

        if (BallonLaunched != null)
        {
            BallonLaunched.Invoke(this, new EventArgs());
        }
    }
}
