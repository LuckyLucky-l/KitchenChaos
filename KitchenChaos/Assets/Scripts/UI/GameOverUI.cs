using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverUI : MonoBehaviour
{
   [SerializeField]private TextMeshProUGUI LabeRecipesDeliveredText;
   void Start()
   {
       GameManager.Instance.OnStateChanged+=GameManager_OnStateChanged;
       HideMe();      
   }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsGameOver())
        {
            ShowMe();
            LabeRecipesDeliveredText.text=Mathf.Ceil(DeliverManager.Instance.GetSuccessFulRecipesAmount()).ToString();
        }else
        {
            HideMe();
        }
    }
    public void ShowMe(){
        gameObject.SetActive(true);
    }
    public void HideMe(){
        gameObject.SetActive(false);
    }
}
