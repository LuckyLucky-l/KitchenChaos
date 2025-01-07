using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu]
public class CuttingCounterSO : ScriptableObject
{
    public KitchenObjectSO inPut;
    public KitchenObjectSO outPut;
    public float cuttingProgressMax;
}
