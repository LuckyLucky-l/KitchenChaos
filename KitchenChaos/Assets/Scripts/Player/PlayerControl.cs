using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerControl : MonoBehaviour,IKitchenObjectParent
{
    //---实例化对象
    public static PlayerControl Instance{get;private set;}
    public class OnselectedCounterChangedEventArgs:EventArgs{//存储柜台的嵌套类
    public BaseCounter selectedCounter;
    }
    //---事件---
    public  event EventHandler OnPickedSomething;//拾取物品发出的声音
    public event EventHandler<OnselectedCounterChangedEventArgs> OnselectedCounterChanged;//选定的柜台被更改事件
    //---可视变量---
     [SerializeField]private Transform SetKichenObjectParent;
     //---变量---
    private KitchenObject kitchenObject;
    private BaseCounter selectedCounter;//当前选定的柜台
    
   
    void Awake()
    {
        Instance=this;
        
    }
   [SerializeField] private float moveSpeed=7;
   [SerializeField] private float rotateSpeed=10;
   [SerializeField] private gameInput gameInput;
   [SerializeField] private LayerMask counterslayerMask;
    private Vector3 lastInteractDir=new(0,0,0);
   private bool iswaking;

   void Start()
   {
       gameInput.OnInteractAction+=GameInput_OnInteractAction;//监听E键交互
       gameInput.OnInteractAlternateAction+=GameInput_OnInteractAlternateAction;//监听F键交互
   }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (selectedCounter!=null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, EventArgs e)//按E交互
    {
        if (selectedCounter!=null)
        {
            selectedCounter.Interact(this);//如果不为空则进行交互
        }
    }

    void Update()
    {
         HandleMovement();//处理移动
         HandleInteractions();//处理交互
    }
    private void HandleInteractions(){//处理交互
        Vector2 inputVector=gameInput.GetMovementVecotorNomalized();
        Vector3 moveDir=new(inputVector.x,0f,inputVector.y);
        if (moveDir != Vector3.zero)
        {
            // 如果玩家有移动输入，记录最后一次有效的移动方向（或位置）
            lastInteractDir=moveDir;
         }
         float interactDistance=2f;
        // 使用射线检测环境中的交互对象
         if (Physics.Raycast(transform.position,lastInteractDir,out RaycastHit raycastHit,interactDistance,counterslayerMask))
        {
             // 开始检测当前是不是桌子
            if (raycastHit.transform.TryGetComponent(out BaseCounter selectedCounter)) // 假设检测到目标对象
             {
                SetselectedCounter(selectedCounter);
             }else
             {
                SetselectedCounter(null);
             }
        }else
        {
            SetselectedCounter(null);
        }
    }
    public void SetselectedCounter(BaseCounter selectedCounter){//选择方向
        this.selectedCounter=selectedCounter;
        OnselectedCounterChanged?.Invoke(this,new OnselectedCounterChangedEventArgs{selectedCounter=selectedCounter});
    }
    public void HandleMovement(){
        Vector2 inputVector=gameInput.GetMovementVecotorNomalized();
        Vector3 moveDir=new(inputVector.x,0f,inputVector.y);
        iswaking=moveDir!=Vector3.zero;
        float moveDistance=moveSpeed*Time.deltaTime;//随着移动,不断的改变最大距离
        float PlayerRadius=.7f;
        float PlayerHeight=2f;
        bool canMove=moveDir!=Vector3.zero && !Physics.CapsuleCast(transform.position,transform.position+Vector3.up*PlayerHeight,PlayerRadius,moveDir,moveDistance);
        if (!canMove)
        {
            Vector3 moveDirX=new Vector3(moveDir.x,0,0).normalized;
            canMove=(moveDir.x<-0.5||moveDir.x>0.5) &&!Physics.CapsuleCast(transform.position,transform.position+Vector3.up*PlayerHeight,PlayerRadius,moveDirX,moveDistance);
            if (canMove)
            {
                moveDir=moveDirX;
            }else
            {
                Vector3 moveDirZ=new Vector3(0,0,moveDir.z).normalized;
                canMove=(moveDir.z<-0.5||moveDir.z>0.5) &&!Physics.CapsuleCast(transform.position,transform.position+Vector3.up*PlayerHeight,PlayerRadius,moveDirZ,moveDistance);
                if (canMove)
                {
                 moveDir=moveDirZ;   
                }else{
                    //任何方向都不能移动了
                }
            }
        }        
        if (canMove)
        {
            transform.position+=moveDir*moveDistance;
        }
        transform.forward+=Vector3.Slerp(transform.forward,moveDir,rotateSpeed*Time.deltaTime);
    }
    public bool IsWalking(){
        return iswaking;
    }
    public Transform GetKitchenObjectFollowTransform(){
        return SetKichenObjectParent;
    }
    //清空桌台
    public void ClearKitchenObject(){
        kitchenObject=null;
    }
    //返回当前桌台上的物品
    public KitchenObject GetKitchenObject(){
        return kitchenObject;
    }
    //检查要转移的桌台是不是空的
    public bool HasKitchenObject(){
        return kitchenObject!=null;
    }
    //更新桌台
    public void SetKitchenObject(KitchenObject kitchenObject){
        this.kitchenObject=kitchenObject;
        if (kitchenObject!=null)
        {
            OnPickedSomething?.Invoke(this,EventArgs.Empty);//触发拾取物品事件,发出声音
        }
    }
}
