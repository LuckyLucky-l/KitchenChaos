using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SeletctedCounterVisual : MonoBehaviour
{
    [SerializeField]private BaseCounter baseCounter;
    [SerializeField]private GameObject[] visualGameObjectArrays;
    void Start()
    {
        //监听交互视觉
        PlayerControl.Instance.OnselectedCounterChanged+=Player_OnSeletedCounterChanged;
    }

    private void Player_OnSeletedCounterChanged(object sender, PlayerControl.OnselectedCounterChangedEventArgs e)
    {
        if (e.selectedCounter==baseCounter)
        {
            Show();
        }else
        {
            Hide();
        }
    }
    private void Show()
    {
        foreach (GameObject item in visualGameObjectArrays)
        {
            item.SetActive(true);
        }
    }
    private void Hide()
    {
        foreach (GameObject item in visualGameObjectArrays)
        {
            item.SetActive(false);
        }
    }
}
