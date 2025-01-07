using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseCounter : MonoBehaviour,IKitchenObjectParent
{
        public static event EventHandler OnAnyObjectPlacedHere;//物品放到桌子上发出的声音
        private KitchenObject kitchenObject;//当前桌台上的物品
        [SerializeField]private Transform counterTopPoint;
        public virtual void Interact(IKitchenObjectParent player){}
        public virtual void InteractAlternate(IKitchenObjectParent player){}
        public Transform GetKitchenObjectFollowTransform(){
            return counterTopPoint;
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
                OnAnyObjectPlacedHere?.Invoke(this,EventArgs.Empty);
            }
        }
        public static void ResetStaticDataManager(){
            OnAnyObjectPlacedHere=null;
        }
}
