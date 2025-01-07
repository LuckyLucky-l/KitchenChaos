using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter,IKitchenObjectParent
{
    public override void Interact(IKitchenObjectParent player){
        //空桌台只是拿起放下的功能
        if (!HasKitchenObject())
        {
            if (player.HasKitchenObject())
            {
                player.GetKitchenObject().SetKitchenObjectParent(this);
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
                }else
                {
                    //桌子有，人也有，但是人没有盘子
                    //检查桌子上的物品是不是盘子
                    if (GetKitchenObject().TryGetPlate(out  plateKichenObject))
                    {
                        plateKichenObject=GetKitchenObject() as PlateKichenObject;
                        if (plateKichenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObject_SO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            }else
            {
                GetKitchenObject().SetKitchenObjectParent(player);
            }
        }
    }
}
