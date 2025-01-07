using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress
{
    public enum State{
    Idel,
    Frying,
    Fried,
    Burned
}
    //定义一个进度条改变的事件
     public event EventHandler<IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;

    public event EventHandler<OnStateChangedEventArgs> OnStateChanged;//定义一个粒子动画状态改变的事件
    public class OnStateChangedEventArgs:EventArgs{
        public State state;
    }
    [SerializeField]private FryingRecipeSO[] fryingRecipeSOArray;
    [SerializeField]private BurningRecipeSO[] burningRecipesArray;
    private BurningRecipeSO burningRecipeSO;
    private FryingRecipeSO fryingRecipeSO;
    private float fryingTimer;
    private float burningTimer;
    [SerializeField]private State state=State.Idel;
    void Start()
    {
        state=State.Idel;
    }
    void Update()
    {
        switch (state)
        {
            case State.Idel:
            break;
            case State.Frying:
                if (HasKitchenObject())
                 {
                    fryingTimer+=Time.deltaTime;
                     OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                    {progressNormalized=(float)fryingTimer/fryingRecipeSO.fryingTimerMax});
                    if (fryingTimer>=fryingRecipeSO.fryingTimerMax)
                    {
                        fryingTimer=0;
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawKitchenObject(fryingRecipeSO.outPut,this);
                        burningTimer=0;
                        state=State.Fried;
                        burningRecipeSO=GetBurningRecipeSOWithInput(GetKitchenObject().GetKitchenObject_SO());
                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                        {progressNormalized=(float)fryingTimer/fryingRecipeSO.fryingTimerMax});
                    }
                 }
            OnStateChanged?.Invoke(this,new OnStateChangedEventArgs(){state=state});
            break;
            case State.Fried:
                burningTimer+=Time.deltaTime;
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                {progressNormalized=(float)burningTimer/burningRecipeSO.burningTimerMax});
                if (HasKitchenObject())
                 {
                    if (burningTimer>=burningRecipeSO.burningTimerMax)
                    {
                        burningTimer=0;
                        GetKitchenObject().DestroySelf();
                        KitchenObject.SpawKitchenObject(burningRecipeSO.outPut,this);
                        state=State.Burned;
                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                        {progressNormalized=0});
                    }
                 }
                OnStateChanged?.Invoke(this,new OnStateChangedEventArgs(){state=state});
            break;
            case State.Burned:
            OnStateChanged?.Invoke(this,new OnStateChangedEventArgs(){state=state});
            break;
        }
        
    }
    public override void Interact(IKitchenObjectParent player){
        //空桌台只是拿起放下的功能
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject()&&HasRecipeWithInput(player.GetKitchenObject().GetKitchenObject_SO()))
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
                state=State.Frying;
                fryingRecipeSO=GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObject_SO());
                OnStateChanged?.Invoke(this,new OnStateChangedEventArgs(){state=state});
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                {progressNormalized=(float)fryingTimer/fryingRecipeSO.fryingTimerMax});
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
                        state=State.Idel;
                        OnStateChanged?.Invoke(this,new OnStateChangedEventArgs(){state=state});
                        OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                    {progressNormalized=0});
                    }
                }
            }else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
                state=State.Idel;
                OnStateChanged?.Invoke(this,new OnStateChangedEventArgs(){state=state});
                OnProgressChanged?.Invoke(this,new IHasProgress.OnProgressChangedEventArgs
                {progressNormalized=0});
            }
        }
    }
    //返回当前桌子上菜的SO
    private KitchenObjectSO  GetOutkitchenObjectSO(KitchenObjectSO inkitchenObjectSO){
       FryingRecipeSO fryingRecipeSO=GetCuttingRecipeSOWithInput(inkitchenObjectSO);
       if (fryingRecipeSO!=null)
       {
            return fryingRecipeSO.outPut;  
       }else
       {
            return null;
       }

    }
    //能不能放在桌子上
    public bool HasRecipeWithInput(KitchenObjectSO inkitchenObjectSO){
       FryingRecipeSO fryingRecipeSO= GetCuttingRecipeSOWithInput(inkitchenObjectSO);
       return fryingRecipeSO!=null;
        }
        
        //如果当前输入的SO在切菜SO里面，把切菜SO Return出去
    public FryingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO inkitchenObjectSO){
        foreach (FryingRecipeSO fryingRecipeSO in fryingRecipeSOArray)
        {
            if (fryingRecipeSO.inPut==inkitchenObjectSO)
            {
                return fryingRecipeSO;
            }
        }
        return null;
    }
    //返回炸糊的SO
        public BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO inkitchenObjectSO){
            foreach (BurningRecipeSO burningRecipeSO in burningRecipesArray)
            {
                if (burningRecipeSO.inPut==inkitchenObjectSO)
                {
                    return burningRecipeSO;
                }
            }
        return null;
    }
    //返回当前状态是不是煎熟的状态
    public bool Isfried(){
        return state==State.Fried;
    }
}
