using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event EventHandler OnPlateRemoved;
    [SerializeField]private KitchenObjectSO plateKitchenObjectSO;
    public event EventHandler OnPlateSpawned;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax =4f;
    private float PlateSpawnedAmount;
    private float PlateSpawnedAmountMax = 4f;
    void Update()
    {
        spawnPlateTimer+=Time.deltaTime;
        if (spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer=0f;
            //生成一个盘子
            if (GameManager.Instance.IsGamePlaying()&&PlateSpawnedAmount<PlateSpawnedAmountMax)
            {
                PlateSpawnedAmount++;
                OnPlateSpawned?.Invoke(this, new EventArgs());   
            }
        }
    }
    public override void Interact(IKitchenObjectParent player)
    {
       if (!player.HasKitchenObject())
       {
            if (PlateSpawnedAmount>0)
            {
                PlateSpawnedAmount--;
                KitchenObject.SpawKitchenObject(plateKitchenObjectSO,player);
                OnPlateRemoved?.Invoke(this, new EventArgs());
            }
       }
    }
}
