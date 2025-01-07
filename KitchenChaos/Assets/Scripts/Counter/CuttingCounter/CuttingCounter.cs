using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem.XR.Haptics;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static CuttingCounter Instance;
    void Awake()
    {
        Instance = this;
    }
    public static event EventHandler OnAnyCut;
    public event EventHandler OutCut;//切菜动画的事件
    public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    [SerializeField]private CuttingCounterSO[] kitchenObjectSOArrays;//完整的菜-->切完的菜
    private float cuttingProgress;//切菜的次数

    public override void Interact(IKitchenObjectParent player){
        //空桌台只是拿起放下的功能
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject()&&HasRecipeWithInput(player.GetKitchenObject().GetKitchenObject_SO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                cuttingProgress=0;
                CuttingCounterSO cuttingCounterSO=GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObject_SO());
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{progressNormalized=(float)cuttingProgress/cuttingCounterSO.cuttingProgressMax});
            }else
            {
                //桌子没有，玩家也没有
            }
        }else//桌子有物品
        {
            if (player.HasKitchenObject())
            {
                //桌子有，人也有
                if (player.GetKitchenObject().TryGetPlate(out PlateKichenObject plateKichenObject))
                {
                     plateKichenObject=player.GetKitchenObject() as PlateKichenObject;
                    if (plateKichenObject.TryAddIngredient(GetKitchenObject().GetKitchenObject_SO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }
                    OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{progressNormalized=0});
                }
            }else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
    public override void InteractAlternate(IKitchenObjectParent player)
    {
        if (HasKitchenObject()&&GetKitchenObject().GetKitchenObject_SO())//有物品，并且有SO
        {
            cuttingProgress++;//切菜次数+1
            OutCut?.Invoke(this,EventArgs.Empty);
            OnAnyCut?.Invoke(this,EventArgs.Empty);
            CuttingCounterSO cuttingCounterSO=GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObject_SO());//得到当前待切的菜
            if (cuttingCounterSO!=null)
            {
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs{progressNormalized=(float)cuttingProgress/cuttingCounterSO.cuttingProgressMax});
                if (cuttingProgress>=cuttingCounterSO.cuttingProgressMax)
                {
                    KitchenObjectSO outPutKitchenObjectSO= GetOutkitchenObjectSO(GetKitchenObject().GetKitchenObject_SO());
                    GetKitchenObject().DestroySelf();       
                    KitchenObject.SpawKitchenObject(outPutKitchenObjectSO,this);
                }
            }
        }
    }
    //返回当前桌子上菜的SO
    private KitchenObjectSO  GetOutkitchenObjectSO(KitchenObjectSO inkitchenObjectSO){
       CuttingCounterSO cuttingCounterSO=GetCuttingRecipeSOWithInput(inkitchenObjectSO);
       if (cuttingCounterSO!=null)
       {
            return cuttingCounterSO.outPut;  
       }else
       {
            return null;
       }

    }
    //能不能放在桌子上
    public bool HasRecipeWithInput(KitchenObjectSO inkitchenObjectSO){
       CuttingCounterSO cuttingCounterSO= GetCuttingRecipeSOWithInput(inkitchenObjectSO);
       return cuttingCounterSO!=null;
        }
        //如果当前输入的SO在切菜SO里面，把切菜SO Return出去
    public CuttingCounterSO GetCuttingRecipeSOWithInput(KitchenObjectSO inkitchenObjectSO){
        foreach (CuttingCounterSO cuttingRecipeSO in kitchenObjectSOArrays)
        {
            if (cuttingRecipeSO.inPut==inkitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
     new public static void ResetStaticDataManager(){
        OnAnyCut=null;
    }
} 
