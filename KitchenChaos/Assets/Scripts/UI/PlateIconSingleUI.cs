using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 单个盘子的UI设置
/// </summary>
public class PlateIconSingleUI : MonoBehaviour
{
    [SerializeField]private Image image;
    public void SetKichenObjectSO(KitchenObjectSO kitchenObjectSO){
        image.sprite = kitchenObjectSO.sprite;
    }
}
