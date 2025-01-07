using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryResultUI : MonoBehaviour
{
    [SerializeField]private TextMeshProUGUI MessageText;
    [SerializeField]private Image IconImage;
    [SerializeField]private Image backgroundImage;
    [SerializeField]private Sprite successSprite;
    [SerializeField]private Sprite failedSprite;
    [SerializeField]private Color successColor;
    [SerializeField]private Color failedColor;

    private const string POPUP="Prop";
    private Animator animator;
    void Awake()
    {
        animator=GetComponent<Animator>();
    }
    void Start()
    {
        DeliverManager.Instance.OnRecipeSuccess+=DeliverManager_OnRecipeSuccess;
        DeliverManager.Instance.OnRecipeFail+=DeliverManager_OnRecipeFail;
        Hide();
    }
    private void DeliverManager_OnRecipeSuccess(object sender, EventArgs e)
    {
        Show();
        animator.SetTrigger(POPUP);
        MessageText.text="DELIVERY\nSUCCESS";
        IconImage.sprite=successSprite;
        backgroundImage.color=successColor;
    }
    private void DeliverManager_OnRecipeFail(object sender, EventArgs e)
    {
        print("Delivery failed");
        Show();
        animator.SetTrigger(POPUP);
        MessageText.text="DELIVERY\nFAILED";
        IconImage.sprite=failedSprite;
        backgroundImage.color=failedColor;
    }
    public void Show(){
        gameObject.SetActive(true);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
}
