using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI textMesh;
    private const string NUMBER_POPUP="NumberPopup";
    private Animator animator;
    private float previousCountdownNumber;
    void Awake()
    {
        animator = GetComponent<Animator>();
    }
   void Start()
   {
       GameManager.Instance.OnStateChanged+=GameManager_OnStateChanged;
       HideMe();      
   }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            ShowMe();
        }else
        {
            HideMe();
        }
    }
    void Update()
    {
        int countdownNumber = Mathf.CeilToInt(GameManager.Instance.GetCoutdownToStartTimer());
        textMesh.text=Mathf.Ceil(GameManager.Instance.GetCoutdownToStartTimer()).ToString();
        if (previousCountdownNumber!=countdownNumber)
        {
            previousCountdownNumber = countdownNumber;
            animator.SetTrigger(NUMBER_POPUP);
            SoundManager.Instance.PlayCountDownSound();
        }
    }
    public void ShowMe(){
        gameObject.SetActive(true);
    }
    public void HideMe(){
        gameObject.SetActive(false);
    }
}
