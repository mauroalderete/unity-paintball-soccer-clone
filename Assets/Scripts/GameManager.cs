using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();

                if (instance == null)
                {
                    GameObject singleton = new GameObject(typeof(GameManager).Name);
                    instance = singleton.AddComponent<GameManager>();
                }
            }

            return instance;
        }
    }

    public event EventHandler StateChanged;

    [SerializeField] private GameObject ballonCollection;
    [SerializeField] private LauncherBallon launcherBallon;
    [SerializeField] private SelfArc selfArc;
    [SerializeField] private Outside outsideLeft;
    [SerializeField] private Outside outsideRight;
    [SerializeField] private CounterArc counterArc;

    public enum GameState
    {
        LAUNCHING_BALLON,
        PLAYING,
    }

    public GameState State { get; private set; }

    void Start()
    {
        SetNewState(GameState.LAUNCHING_BALLON);

        selfArc.BallonLaunched += SelfArc_BallonLaunched;
        outsideLeft.BallonLosed += Outside_BallonLosed;
        outsideRight.BallonLosed += Outside_BallonLosed;
        counterArc.BallonEntered += CounterArc_BallonEntered;
    }

    private void CounterArc_BallonEntered(object sender, System.EventArgs e)
    {
    }

    private void Outside_BallonLosed(object sender, System.EventArgs e)
    {
    }

    private void SelfArc_BallonLaunched(object sender, System.EventArgs e)
    {
        SetNewState(GameState.PLAYING);
    }

    void Update()
    {
        if (ballonCollection.transform.childCount == 0)
        {
            SetNewState(GameState.LAUNCHING_BALLON);
        }
    }

    private void SetNewState(GameState newState)
    {
        switch (newState)
        {
            case GameState.LAUNCHING_BALLON:
                {
                    launcherBallon.Launch();
                }
                break;
            case GameState.PLAYING:
                {

                }
                break;
        }

        State = newState;

        if(StateChanged != null )
        {
            StateChanged.Invoke(this, new EventArgs());
        }
    }

    private void BallonLosed_BallonGone(object sender, System.EventArgs e)
    {
        SetNewState(GameState.LAUNCHING_BALLON);
    }
}
