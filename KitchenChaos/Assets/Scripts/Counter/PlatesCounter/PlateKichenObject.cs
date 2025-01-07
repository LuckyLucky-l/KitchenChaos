using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateKichenObject : KitchenObject
{
    //----------当往盘子里添加食材时触发的事件，通知盘子上物品的激活
    public event EventHandler<OnIngredientAddedEventArgs> OnIngredientAdd;
    public class OnIngredientAddedEventArgs : EventArgs{
        public KitchenObjectSO kitchenObjectSO;
    }
    //--------------------------
    [SerializeField]private List<KitchenObjectSO> validKitcheObjectList;//有效的 Kitchene 对象列表
    private List<KitchenObjectSO> kitchenObjectSOList;//当前盘子上物品的列表
    void Awake()
    {
        kitchenObjectSOList=new List<KitchenObjectSO>();
    }
    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSO){
        if (!validKitcheObjectList.Contains(kitchenObjectSO))
        {
            return false;//如果不在有效的 KitchenObjectSO 列表中，则直接返回false,底下的代码不会执行
        }
        if (kitchenObjectSOList.Contains(kitchenObjectSO))
        {
            return false;
        }else
        {
            kitchenObjectSOList.Add(kitchenObjectSO);
            OnIngredientAdd?.Invoke(this, new OnIngredientAddedEventArgs() { kitchenObjectSO = kitchenObjectSO });
            return true;
        }
    }
    public List<KitchenObjectSO> GetKitchenObjectSOList(){
        return kitchenObjectSOList;
    }

    internal void Deliver()
    {
        throw new NotImplementedException();
    }
}
