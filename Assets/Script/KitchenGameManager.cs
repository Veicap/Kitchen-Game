using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KitchenGameManager : MonoBehaviour
{
    public static KitchenGameManager Instance { get; private set; }
    public event EventHandler OnStateChanged;
    public event EventHandler OnGamePause;
    public event EventHandler OnGameResume;
    private enum State
    {
        waitingToStart,
        countDownToStart,
        gamePlaying,
        gameOver,
    }
    private State state;
   
    private float countDownTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMAX = 40f;
    private bool isGamePause = false;
    private void Awake()
    {
        state = State.waitingToStart;
        Instance = this;
    }
    private void Start()
    {
        GameInput.Instance.OnPauseAction += GameInput_OnPauseAction;
        GameInput.Instance.OnInteractAction += GameInput_OnInteractAction;
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)
    {
        if(state == State.waitingToStart)
        {
            state = State.countDownToStart;
            OnStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    private void GameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }

    private void Update()
    {
        switch (state)
        {
            case State.waitingToStart:
                
                break;
            case State.countDownToStart:
                countDownTimer -= Time.deltaTime;
                if (countDownTimer < 0f)
                {
                    gamePlayingTimer = gamePlayingTimerMAX;
                    state = State.gamePlaying;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;
            case State.gamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                
                if (gamePlayingTimer < 0f)
                {
                    state = State.gameOver;
                    OnStateChanged?.Invoke(this, EventArgs.Empty);
                    
                }
                break;
            case State.gameOver:
                break;
        }
        Debug.Log(state);
    }
    public bool IsGamePlaying()
    {
        return state == State.gamePlaying;

    }
    public bool IsCountDownStartUpdate()
    {
        return state == State.countDownToStart;
    }
    public bool IsGameOver()
    {
        return state == State.gameOver;
    }
    public float GetCountDownTimer()
    {
        return countDownTimer;
    }
    public float GetGamePlayingTimerNomarlized()
    {
        return 1 - gamePlayingTimer/ gamePlayingTimerMAX;
    }
    public void TogglePauseGame()
    {
        isGamePause = !isGamePause;
        if(isGamePause)
        {
            Time.timeScale = 0f;
            OnGamePause?.Invoke(this, EventArgs.Empty );
        }
        else
        {
            Time.timeScale = 1f;
            OnGameResume?.Invoke(this, EventArgs.Empty);
        }
        
    }
}
