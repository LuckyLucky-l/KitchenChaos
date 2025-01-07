using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//菜单
[CreateAssetMenu]
public class RecipeListSO : ScriptableObject
{
    public List<RecipeSO> recipeSOList;
    public string recipeName;
}
