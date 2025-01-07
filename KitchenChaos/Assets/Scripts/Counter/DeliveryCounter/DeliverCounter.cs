using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliverCounter : BaseCounter
{
    public static DeliverCounter Instance;

    private void Awake()
    {
        Instance = this;
    }
    public override void Interact(IKitchenObjectParent player)
    {
        if (player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKichenObject plateKichenObject))
            {
                DeliverManager.Instance.DeliverRecipe(plateKichenObject);
                player.GetKitchenObject().DestroySelf();
            }
        }
    }
}