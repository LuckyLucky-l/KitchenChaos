using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TutorialUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI MoveUpText;
    [SerializeField]private TextMeshProUGUI MoveDownText;
    [SerializeField]private TextMeshProUGUI MoveLeftText;    
    [SerializeField]private TextMeshProUGUI MoveRightText;
    [SerializeField]private TextMeshProUGUI InteractText;
    [SerializeField]private TextMeshProUGUI Interact_Alt_Text;
    [SerializeField]private TextMeshProUGUI PauseText;
    [SerializeField]private TextMeshProUGUI Gamepad_Move_Text;
    [SerializeField]private TextMeshProUGUI Gamepad_Interact_Text;
    [SerializeField]private TextMeshProUGUI Gamepad_Interact_Alt_Text;
    [SerializeField]private TextMeshProUGUI Gamepad_Pause_Text;
    void Update()
    {
       
    }
    void Start()
    {
        gameInput.Instance.OnRebindBingding += gameInput_OnRebindBingding;
        GameManager.Instance.OnStateChanged+=GameManager_OnStateChanged;
        UpdateVisual();
        Show();
    }

    private void GameManager_OnStateChanged(object sender, EventArgs e)
    {
        if (GameManager.Instance.IsCountdownToStartActive())
        {
            Hide();
        }
    }

    private void gameInput_OnRebindBingding(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    public void UpdateVisual()
    {
        MoveUpText.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Move_Up);
        MoveDownText.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Move_Down);
        MoveLeftText.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Move_Left);
        MoveRightText.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Move_Right);
        InteractText.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Interact);
        Interact_Alt_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.InteractAlternate);
        PauseText.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Pause);
        Gamepad_Move_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Gamepad_Move);
        Gamepad_Interact_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Gamepad_Interact);
        Gamepad_Interact_Alt_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Gamepad_InteractAlternate);
        Gamepad_Pause_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Gamepad_Pause);
    }
    public void Show(){
        gameObject.SetActive(true);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
}
