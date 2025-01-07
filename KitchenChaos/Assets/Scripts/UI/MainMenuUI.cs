using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
    [SerializeField]private Button Playbutton;
    [SerializeField]private Button Quitbutton;
    void Start()
    {
        Playbutton.onClick.AddListener(()=>{
            Loader.Load(Loader.Scene.GameScene);
        });
        Quitbutton.onClick.AddListener(()=>{
            Application.Quit();
        });
        Time.timeScale = 1;
    }
}
