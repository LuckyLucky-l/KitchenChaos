using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu()]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO inPut;
    public KitchenObjectSO outPut;
    public float burningTimerMax;
}
