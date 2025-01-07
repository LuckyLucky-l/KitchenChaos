using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField]private KitchenObjectSO kitchenObject_SO;
    private IKitchenObjectParent kitchenObjectParent;//持有点
    public KitchenObjectSO GetKitchenObject_SO(){//得到当前桌子上的菜
        return kitchenObject_SO;
    }
    public void SetKitchenObjectParent(IKitchenObjectParent kitchenObjectParent){//外面设置自己属于哪一个桌子
        if (this.kitchenObjectParent!=null)
        {
           this.kitchenObjectParent.ClearKitchenObject();
        }
        this.kitchenObjectParent=kitchenObjectParent;
        if (kitchenObjectParent.HasKitchenObject())
        {
            Debug.LogError("已有物品");
            return;
        }
        kitchenObjectParent.SetKitchenObject(this);
        
        transform.parent=kitchenObjectParent.GetKitchenObjectFollowTransform();
        transform.localPosition=Vector3.zero;
    }
    public IKitchenObjectParent GetClearCounter(){//告诉外面自己的桌子是哪一张
        return kitchenObjectParent;
    }
    public void DestroySelf(){
        kitchenObjectParent.ClearKitchenObject();
        Destroy(gameObject);
    }
    //生成厨房用品
    public static KitchenObject SpawKitchenObject(KitchenObjectSO kitchenObjectSO,IKitchenObjectParent kitchenObjectParent){
     Transform KitchenObjectTransform= Instantiate(kitchenObjectSO.prefb);
      KitchenObject kitchenObject= KitchenObjectTransform.GetComponent<KitchenObject>();
      kitchenObject.SetKitchenObjectParent(kitchenObjectParent);
      return kitchenObject;
    }
    public bool TryGetPlate(out PlateKichenObject plateKichenObject){//尝试判断是否是盘子
        if (this is PlateKichenObject)
        {
            plateKichenObject=this as PlateKichenObject;
            return true;
        }
        else
        {
            plateKichenObject=null;
            return false;
        }
    }
}
