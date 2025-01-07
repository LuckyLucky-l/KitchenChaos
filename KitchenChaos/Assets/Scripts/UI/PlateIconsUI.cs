using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 盘子上的物品图标UI
/// </summary>
public class PlateIconsUI : MonoBehaviour
{
    [SerializeField]private PlateKichenObject plateKichenObject;
    [SerializeField]private Transform iconTemplate;

    void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    void Start()
    {
        plateKichenObject.OnIngredientAdd+=plateKichenObject_OnIngredientAdd;
    }

    private void plateKichenObject_OnIngredientAdd(object sender, PlateKichenObject.OnIngredientAddedEventArgs e)
    {
           UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child==iconTemplate)continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO  in plateKichenObject.GetKitchenObjectSOList())
        {
           Transform iconTransform= Instantiate(iconTemplate,transform);
           iconTransform.gameObject.SetActive(true);
           iconTransform.GetComponent<PlateIconSingleUI>().SetKichenObjectSO(kitchenObjectSO);
        }
    }
}
