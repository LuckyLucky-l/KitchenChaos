using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public enum GameState{
    waitingToStart,
    CountdownToStart,
    GamePlaying,
    GameOver
}
public class GameManager : MonoBehaviour
{
    public static GameManager Instance{get; private set;}
    public event EventHandler OnStateChanged;//状态发生改变的事件
    public GameState currentState=GameState.waitingToStart;
    private float waitingToStartTimer=1f;
    private float countdownToStartTimer=3f;
    private float gamePlayingTimer=0f;
    private bool isPaused=false;
    public event EventHandler OnGamePaused;//游戏暂停的事件
    public event EventHandler OnGameUnPaused;//游戏恢复的事件
    [SerializeField]private float gamePlayingTimerMax=10f;
    void Awake()
    {
        Instance=this;
    }
    void Start()
    {
        gameInput.Instance.OnPauseAction+=gameInput_OnPauseAction;
        gameInput.Instance.OnInteractAction+=gameInput_OnInteractAction;
    }

    private void gameInput_OnInteractAction(object sender, EventArgs e)
    {
        if (currentState==GameState.waitingToStart)
        {
            currentState=GameState.CountdownToStart;
            OnStateChanged?.Invoke(this,EventArgs.Empty);
        }
    }

    private void gameInput_OnPauseAction(object sender, EventArgs e)
    {
        TogglePauseGame();
    }
    void Update()
    {
        switch (currentState)
        {
            case GameState.waitingToStart:
                break;
            case GameState.CountdownToStart:
                countdownToStartTimer-=Time.deltaTime;
                if (countdownToStartTimer<=0)
                {
                     gamePlayingTimer=gamePlayingTimerMax;
                    currentState=GameState.GamePlaying;
                    OnStateChanged?.Invoke(this,EventArgs.Empty);
                }
                break;
            case GameState.GamePlaying:
                gamePlayingTimer-=Time.deltaTime;
                if (gamePlayingTimer<=0)
                {
                    currentState=GameState.GameOver;
                    OnStateChanged?.Invoke(this,EventArgs.Empty);
                    
                }
                break;
            case GameState.GameOver:
                break;     
        }             
    }
    public bool IsGamePlaying(){
        return currentState==GameState.GamePlaying;
    }
    public bool IsCountdownToStartActive(){
        return currentState==GameState.CountdownToStart;
    }
    public float GetCoutdownToStartTimer(){
        return countdownToStartTimer;
    }
    public bool IsGameOver(){
        return currentState==GameState.GameOver;
    }
    public float GetGamePlayingTimerNormalized(){
        return 1-(gamePlayingTimer/gamePlayingTimerMax);
    }
    public void TogglePauseGame(){
        if (!isPaused)//游戏暂停
        {
            Time.timeScale=0;
            isPaused=true;
            OnGamePaused?.Invoke(this,EventArgs.Empty);
        }else//游戏解除暂停
        {
            Time.timeScale=1;
            isPaused=false;
            OnGameUnPaused?.Invoke(this,EventArgs.Empty);
        }
    }
}
