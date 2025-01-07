using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter,IKitchenObjectParent
{
    public event EventHandler  OnPlayerGradbbedObject;
    [SerializeField]private KitchenObjectSO kitchenObject_SO;
    public override void Interact(IKitchenObjectParent player){
        if (!player.HasKitchenObject())
        {
            KitchenObject.SpawKitchenObject(kitchenObject_SO,player);//生成厨房用品
             OnPlayerGradbbedObject?.Invoke(this,EventArgs.Empty);
        }
    }
}
