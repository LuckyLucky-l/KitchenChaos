using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class FryingRecipeSO : ScriptableObject
{
    public KitchenObjectSO inPut;
    public KitchenObjectSO outPut;
    public float fryingTimerMax;
}
