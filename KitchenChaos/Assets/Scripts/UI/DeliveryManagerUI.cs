using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// 待做菜单UI
/// </summary>
public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField]private Transform Container;
    [SerializeField]private Transform RecipeTemplate;
    void Awake()
    {
        RecipeTemplate.gameObject.SetActive(false);
    }
    void Start()
    {
        DeliverManager.Instance.OnRecipeSpawned += DeliverManager_OnRecipeSpawned;
        DeliverManager.Instance.OnRecipeCompleted+=DeliverManager_OnRecipeCompleted;
        UpdateViusal();
    }

    private void DeliverManager_OnRecipeCompleted(object sender, EventArgs e)
    {
        UpdateViusal();
    }

    private void DeliverManager_OnRecipeSpawned(object sender, EventArgs e)
    {
        UpdateViusal();   
    }

    //1.更新菜单UI
    private void UpdateViusal(){
    foreach (Transform child in Container)
    {
        if (child==RecipeTemplate)continue;
        Destroy(child.gameObject);
    }
    foreach (RecipeSO recipeSo in DeliverManager.Instance.GetWaitingRecipeList())
    {
        Transform recipeTransform=Instantiate(RecipeTemplate,Container);
        recipeTransform.GetComponent<DeliverManagerSingleUI>().SetRecipeSO(recipeSo);
        recipeTransform.gameObject.SetActive(true);
    }
   }
}
