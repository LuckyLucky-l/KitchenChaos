using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField]private Button MainMenuButton;
    [SerializeField]private Button ResumeButton;
    [SerializeField]private Button OptionButton;
    void Awake()
    {
        MainMenuButton.onClick.AddListener(()=>{Loader.Load(Loader.Scene.MainMenuScene);});
        ResumeButton.onClick.AddListener(()=>{GameManager.Instance.TogglePauseGame();});
        OptionButton.onClick.AddListener(()=>{Hide();Options.Instance.Show(Show);});
    }
    void Start()
    {

        GameManager.Instance.OnGamePaused+=GameManager_OnGamePaused;
        GameManager.Instance.OnGameUnPaused+=GameManager_OnGameUnPaused;
        Hide();
    }
    private void GameManager_OnGameUnPaused(object sender, EventArgs e)
    {
        Hide();
    }

    private void GameManager_OnGamePaused(object sender, EventArgs e)
    {
        Show();
    }
    public void Show()
    {
        gameObject.SetActive(true);
        MainMenuButton.Select();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
