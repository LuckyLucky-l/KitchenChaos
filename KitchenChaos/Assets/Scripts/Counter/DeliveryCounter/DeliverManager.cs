using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
public class DeliverManager : MonoBehaviour
{
    [SerializeField]private RecipeListSO recipeListSO;//菜单
    public static DeliverManager Instance;

    public  event EventHandler OnRecipeSuccess;//上菜成功
    public  event EventHandler OnRecipeFail;//上菜失败
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    
    private List<RecipeSO> waitingRecipeList;

    private float spawnRecipeTimer;
    private float spawnRecipeTimerMax=4;
    private float waitingRecipesMax=4;
    private float successFulRecipesAmount=0;
    private bool palteCountentsMatchesRecipe;
    void Awake()
    {
        Instance = this;
        waitingRecipeList = new List<RecipeSO>();
    }
    //更新待做菜单
    void Update()
    {
        spawnRecipeTimer-=Time.deltaTime;
        if (spawnRecipeTimer<=0)
        {
            spawnRecipeTimer=spawnRecipeTimerMax;
            if (GameManager.Instance.IsGamePlaying()&&waitingRecipeList.Count<waitingRecipesMax)
            {
               RecipeSO waitingRecipeSO= recipeListSO.recipeSOList[UnityEngine.Random.Range(0,recipeListSO.recipeSOList.Count)];
              waitingRecipeList.Add(waitingRecipeSO);
               OnRecipeSpawned?.Invoke(this,EventArgs.Empty);

            }
        }
    }
    //上菜的逻辑，判断盘子里的东西和待做菜单里的东西是否匹配
    public void DeliverRecipe(PlateKichenObject plateKichenObject)
    {
        for (int i = 0; i < waitingRecipeList.Count; i++)
        {
            RecipeSO waitingRecipeSO=waitingRecipeList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count==plateKichenObject.GetKitchenObjectSOList().Count)
            {
                palteCountentsMatchesRecipe=true;
                foreach (KitchenObjectSO recipeKichenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound=false;
                    foreach (KitchenObjectSO  platekichenObjectSO in plateKichenObject.GetKitchenObjectSOList())
                    {
                        if (recipeKichenObjectSO==platekichenObjectSO)
                        {
                            ingredientFound=true;
                            break;
                        }
                    }
                    if (!ingredientFound)
                    {
                        palteCountentsMatchesRecipe=false;
                    }
                }
                if (palteCountentsMatchesRecipe)
                {
                    waitingRecipeList.RemoveAt(i);
                    successFulRecipesAmount++;
                    OnRecipeCompleted?.Invoke(this,EventArgs.Empty);
                    OnRecipeSuccess?.Invoke(this,EventArgs.Empty);
                    palteCountentsMatchesRecipe=false;
                    return;
                }
            }  
        }
        if (!palteCountentsMatchesRecipe)
        {
            OnRecipeFail?.Invoke(this,EventArgs.Empty);
        } 
    }
    public List<RecipeSO> GetWaitingRecipeList(){
        return waitingRecipeList;
    }
    public float GetSuccessFulRecipesAmount(){
        return successFulRecipesAmount;
    }
}
