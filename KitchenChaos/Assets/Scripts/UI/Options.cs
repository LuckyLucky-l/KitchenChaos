using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    public static Options Instance;
    [SerializeField]private Button SoudEffectsButton;
    [SerializeField]private TextMeshProUGUI SoudEffectsText;
    [SerializeField]private Button MusicButton;
    [SerializeField]private TextMeshProUGUI MusicText;
    [SerializeField]private Button CloseButton;
    [Header("换绑Buttton")]
    [SerializeField]private Button Move_Up_Button;
    [SerializeField]private Button Move_Down_Button;
    [SerializeField]private Button Move_Left_Button;
    [SerializeField]private Button Move_Right_Button;
    [SerializeField]private Button Interact_Button;
    [SerializeField]private Button Interact_Alt_Button;
    [SerializeField]private Button Pause_Button;
    [SerializeField]private Button Gamepad_Interact_Button;
    [SerializeField]private Button Gamepad_InteractAlternate_Button;
    [SerializeField]private Button Gamepad_Pause_Button;
    [Header("换绑Text")]
    [SerializeField]private TextMeshProUGUI Move_Up_Text;
    [SerializeField]private TextMeshProUGUI Move_Down_Text;
    [SerializeField]private TextMeshProUGUI Move_Left_Text;
    [SerializeField]private TextMeshProUGUI Move_Right_Text;
    [SerializeField]private TextMeshProUGUI Interact_Text;
    [SerializeField]private TextMeshProUGUI Interact_Alt_Text;
    [SerializeField]private TextMeshProUGUI Pause_Text;
    [SerializeField]private TextMeshProUGUI Gamepad_Interact_Text;
    [SerializeField]private TextMeshProUGUI Gamepad_InteractAlternate_Text;
    [SerializeField]private TextMeshProUGUI Gamepad_Pause_Text;
    [SerializeField]private Transform presstoRebindKeyTransform;
    private Action onCloseButtonAction;  
    void Awake()
    {
        Instance = this;

        SoudEffectsButton.onClick.AddListener(() =>{SoundManager.Instance.ChangeVolume();UpdareVisual();});
        MusicButton.onClick.AddListener(() =>{MusicManager.Instance.ChangeVolume();UpdareVisual();});
        CloseButton.onClick.AddListener(() =>{Hide();onCloseButtonAction();});
        Move_Up_Button.onClick.AddListener(() => {RebingBinding(gameInput.Bingding.Move_Up);});
        Move_Down_Button.onClick.AddListener(()=>{RebingBinding(gameInput.Bingding.Move_Down);});
        Move_Left_Button.onClick.AddListener(()=>{RebingBinding(gameInput.Bingding.Move_Left);});
        Move_Right_Button.onClick.AddListener(()=>{RebingBinding(gameInput.Bingding.Move_Right);});
        Interact_Button.onClick.AddListener(()=>{RebingBinding(gameInput.Bingding.Interact);});
        Interact_Alt_Button.onClick.AddListener(()=>{RebingBinding(gameInput.Bingding.InteractAlternate);});
        Pause_Button.onClick.AddListener(()=>{RebingBinding(gameInput.Bingding.Pause);});
        Gamepad_Interact_Button.onClick.AddListener(()=>{RebingBinding(gameInput.Bingding.Gamepad_Interact);});
        Gamepad_InteractAlternate_Button.onClick.AddListener(()=>{RebingBinding(gameInput.Bingding.Gamepad_InteractAlternate);});
        Gamepad_Pause_Button.onClick.AddListener(()=>{RebingBinding(gameInput.Bingding.Gamepad_Pause);});
    }


    void Start()
    {
        GameManager.Instance.OnGameUnPaused+=(sender,e)=>{Hide();};
         UpdareVisual();
         HidePressToRebindKey();
         Hide();
    }
    public void UpdareVisual(){
        SoudEffectsText.text="Sound Effects:"+MathF.Round(SoundManager.Instance.GetVolume()*10);
        MusicText.text="Music:"+MathF.Round(MusicManager.Instance.GetVolume()*10);
        Move_Up_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Move_Up);
        Move_Down_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Move_Down);
        Move_Left_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Move_Left);
        Move_Right_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Move_Right);
        Interact_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Interact);
        Interact_Alt_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.InteractAlternate);
        Pause_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Pause);
        Gamepad_Interact_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Gamepad_Interact);
        Gamepad_InteractAlternate_Text.text=gameInput.Instance.GetBindingText(gameInput.Bingding.Gamepad_InteractAlternate);
        Gamepad_Pause_Text.GetComponent<TextMeshProUGUI>().text=gameInput.Instance.GetBindingText(gameInput.Bingding.Gamepad_Pause);
    }
    public void Hide(){
        gameObject.SetActive(false);
    }
    public void Show(Action onCloseButtonAction){
        this.onCloseButtonAction=onCloseButtonAction;
        SoudEffectsButton.Select();
        gameObject.SetActive(true);
    }
    public void ShowPressToRebindKey(){
        presstoRebindKeyTransform.gameObject.SetActive(true);
    }
    public void HidePressToRebindKey(){
        presstoRebindKeyTransform.gameObject.SetActive(false);
    }
    public void RebingBinding(gameInput.Bingding binding){
        ShowPressToRebindKey();
        gameInput.Instance.RebindBingding(binding,()=>{
            HidePressToRebindKey();
            UpdareVisual();
        });
    }
}
