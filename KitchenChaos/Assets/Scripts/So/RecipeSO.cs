using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//一道菜
[CreateAssetMenu]
public class RecipeSO : ScriptableObject
{
    public List<KitchenObjectSO> kitchenObjectSOList;
    public string recipeName;
}
