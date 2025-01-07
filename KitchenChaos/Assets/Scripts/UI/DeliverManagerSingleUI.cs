using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeliverManagerSingleUI : MonoBehaviour
{
    [SerializeField]private Text RecipeNmaeText;
    [SerializeField]private Transform InconContainer;
    [SerializeField]private Transform IconTemplate;
    void Awake()
    {
        IconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO(RecipeSO recipeSO){
        RecipeNmaeText.text = recipeSO.recipeName;
        foreach (Transform child in InconContainer)
        {
            if (child==IconTemplate) continue;
            Destroy(child.gameObject);
        }
        foreach (KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform iconTransform=Instantiate(IconTemplate,InconContainer);
            iconTransform.GetComponent<Image>().sprite=kitchenObjectSO.sprite;
            iconTransform.gameObject.SetActive(true);
        }
    }
}
