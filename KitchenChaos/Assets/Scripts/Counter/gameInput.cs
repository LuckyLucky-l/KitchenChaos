using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.InputSystem;

public class gameInput : MonoBehaviour
{
    public enum Bingding{
        Move_Up,
        Move_Down,
        Move_Left,
        Move_Right,
        Interact,
        InteractAlternate,
        Pause,
        Gamepad_Move,
        Gamepad_Interact,
        Gamepad_InteractAlternate,
        Gamepad_Pause
    }
    private const string PLAYER_PREFS_BINDINGS = "IOnputBindings";//输入绑定
    public static gameInput Instance{ get; private set; }
    public event EventHandler OnInteractAction;//发布事件，通知自己已经按下了E键，可以进行交互
    public event EventHandler OnInteractAlternateAction;//按键F事件
    public event EventHandler OnPauseAction;//按键ESP事件
    public event EventHandler OnRebindBingding;//重新绑定事件
    public PlayerInputActions playerInputActions;
    void Awake()
    {
        Instance = this;
        playerInputActions=new PlayerInputActions();//每次进入场景都会重新创建一个PlayerInputActions对象，可能会报错
        if (PlayerPrefs.HasKey(PLAYER_PREFS_BINDINGS))
        {
            playerInputActions.LoadBindingOverridesFromJson(PlayerPrefs.GetString(PLAYER_PREFS_BINDINGS));
        }
        playerInputActions.Enable();
        playerInputActions.Player.Interact.performed+=Interact_performed;
        playerInputActions.Player.InteractAlternate.performed+=InteractAlternate_performed;
        playerInputActions.Player.Pause.performed+=Pause_performed;
    }
    public void OnDestroy()
    {
        playerInputActions.Player.Interact.performed-=Interact_performed;
        playerInputActions.Player.InteractAlternate.performed-=InteractAlternate_performed;
        playerInputActions.Player.Pause.performed-=Pause_performed;
        playerInputActions.Disable();
    }

    private void Pause_performed(InputAction.CallbackContext context)
    {
        OnPauseAction?.Invoke(this,EventArgs.Empty);
    }

    private void InteractAlternate_performed(InputAction.CallbackContext context)
    {
        if (!GameManager.Instance.IsGamePlaying())//如果游戏不是在进行中，则不响应事件
        {
            return;
        }
        OnInteractAlternateAction?.Invoke(this,EventArgs.Empty);
    }

    private void Interact_performed(InputAction.CallbackContext context)
    {
         if (GameManager.Instance.currentState==GameState.GameOver)//如果游戏不是在进行中，则不响应事件
        {
            return;
        }
        OnInteractAction?.Invoke(this,EventArgs.Empty);
    }

    public Vector3 GetMovementVecotorNomalized(){
        Vector2 inputVector=playerInputActions.Player.Move.ReadValue<Vector2>();
        return inputVector;
    }
    /// <summary>
    /// 获得当前的绑定按键
    /// </summary>
    /// <param name="bingding"></param>
    /// <returns></returns>
    public string GetBindingText(Bingding bingding){
        switch (bingding)
        {
            case Bingding.Gamepad_Move:
                return playerInputActions.Player.Move.bindings[0].ToDisplayString();
            case Bingding.Move_Up:
                return playerInputActions.Player.Move.bindings[2].ToDisplayString();//显示字符串
            case Bingding.Move_Down:
                return playerInputActions.Player.Move.bindings[3].ToDisplayString();
            case Bingding.Move_Left:
                return playerInputActions.Player.Move.bindings[4].ToDisplayString();
            case Bingding.Move_Right:
                return playerInputActions.Player.Move.bindings[5].ToDisplayString();
            case Bingding.Interact:
                return playerInputActions.Player.Interact.bindings[0].ToDisplayString();
            case Bingding.InteractAlternate:
                return playerInputActions.Player .InteractAlternate.bindings[0].ToDisplayString();
            case Bingding.Pause:
                return playerInputActions.Player.Pause.bindings[0].ToDisplayString();
            case Bingding.Gamepad_Interact:
                return playerInputActions.Player.Interact.bindings[1].ToDisplayString();
            case Bingding.Gamepad_InteractAlternate:
                return playerInputActions.Player.InteractAlternate.bindings[1].ToDisplayString();
            case Bingding.Gamepad_Pause:
                return playerInputActions.Player.Pause.bindings[1].ToDisplayString();
        }
        return null;
    }
    /// <summary>
    /// 重新绑定
    /// </summary>
    /// <param name="bingding"></param>
    /// <param name="onActionRebound"></param>
    public void RebindBingding(Bingding bingding,Action onActionRebound){
        playerInputActions.Player.Disable();
        InputAction inputAction;
        int index;
        switch (bingding)
        {
            case Bingding.Gamepad_Move:
                inputAction=playerInputActions.Player.Move;
                index=0;
                break;
            case Bingding.Move_Up:
                inputAction=playerInputActions.Player.Move;
                index=2;
                break;
            case Bingding.Move_Down:
                inputAction=playerInputActions.Player.Move;
                index=3;
                break;                
            case Bingding.Move_Left:
                inputAction=playerInputActions.Player.Move;
                index=4;
                break;
            case Bingding.Move_Right:
                inputAction=playerInputActions.Player.Move;
                index=5;
                break;
            case Bingding.Interact:
                inputAction=playerInputActions.Player.Interact;
                index=0;
                break;
            case Bingding.InteractAlternate:
                inputAction=playerInputActions.Player.InteractAlternate;
                index=0;
                break;
            case Bingding.Pause:
                inputAction=playerInputActions.Player.Pause;
                index=0;
                break;
            case Bingding.Gamepad_Interact:
                inputAction=playerInputActions.Player.Interact;
                index=1;
                break;
            case Bingding.Gamepad_InteractAlternate:
                inputAction=playerInputActions.Player.InteractAlternate;
                index=1;
                break;
            case Bingding.Gamepad_Pause:
                inputAction=playerInputActions.Player.Pause;
                index=1;
                break;
            default:
                inputAction=null;
                index=0;
                break;
        }
        inputAction.PerformInteractiveRebinding(index).//执行交互式重新绑定
        OnComplete(callback=>{
            callback.Dispose();
            playerInputActions.Player.Enable();
            onActionRebound();
            PlayerPrefs.SetString(PLAYER_PREFS_BINDINGS,playerInputActions.SaveBindingOverridesAsJson());
            PlayerPrefs.Save();
            OnRebindBingding?.Invoke(this,EventArgs.Empty);
        }).Start();
    }
}
