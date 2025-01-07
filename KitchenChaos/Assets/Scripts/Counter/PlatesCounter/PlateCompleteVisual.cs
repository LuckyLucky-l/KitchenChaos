using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [SerializeField]private PlateKichenObject plateKichenObject;
    [Serializable]
    public struct KichenObjectSO_GameObject{
        public KitchenObjectSO kitchenObjectSO;
        public GameObject gameObject;
    }
    [SerializeField] private List<KichenObjectSO_GameObject> KichenObjectSOGameObjectList;

    void Start()
    {
        plateKichenObject.OnIngredientAdd += PlateKichenObject_OnAddIngredient;
        foreach (KichenObjectSO_GameObject kichenObjectSOGameObject in KichenObjectSOGameObjectList)
        {
                kichenObjectSOGameObject.gameObject.SetActive(false);
        }
    }

    private void PlateKichenObject_OnAddIngredient(object sender, PlateKichenObject.OnIngredientAddedEventArgs e)
    {
        foreach (KichenObjectSO_GameObject kichenObjectSOGameObject in KichenObjectSOGameObjectList)
        {
            if (e.kitchenObjectSO == kichenObjectSOGameObject.kitchenObjectSO)
            {
                kichenObjectSOGameObject.gameObject.SetActive(true);
            }
        }
    }
}
