using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// 用来加载场景的类
/// </summary>
public static class Loader
{
    public enum Scene{
    MainMenuScene,
    GameScene,
    LoadingScene,
}
   public static Scene targetScene;//GameScene
   public static void Load(Scene targetScene)
   {
       Loader.targetScene=targetScene;
       SceneManager.LoadScene(Scene.LoadingScene.ToString());
   }
   public static void LoadCallBack(){
        SceneManager.LoadScene(targetScene.ToString());
   }
}
