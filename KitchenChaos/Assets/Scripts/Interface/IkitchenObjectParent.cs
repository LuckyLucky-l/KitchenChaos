using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IKitchenObjectParent
{
    public Transform GetKitchenObjectFollowTransform();
    //清空桌台
    public void ClearKitchenObject();
    //返回当前桌台上的物品
    public KitchenObject GetKitchenObject();
    //检查桌台是不是空的
    public bool HasKitchenObject();
    //更新桌台
    public void SetKitchenObject(KitchenObject kitchenObject);
}
