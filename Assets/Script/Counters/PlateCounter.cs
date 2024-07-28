using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlateCounter : BaseCounter 
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;
    private float spawnPlateTimer;
    private float spawnPlateTimerMax = 2f;
    private int spawnPlateAmount;
    private int spawnPlateAmountMax = 4;
    public event EventHandler OnPlateSpawned;
    public event EventHandler OnPlateRemove;
    private void Update()
    {
        spawnPlateTimer += Time.deltaTime;
        if(spawnPlateTimer > spawnPlateTimerMax)
        {
            spawnPlateTimer = 0;
            if(KitchenGameManager.Instance.IsGamePlaying() && spawnPlateAmount < spawnPlateAmountMax )
            {
                spawnPlateAmount++;
                OnPlateSpawned?.Invoke(this, EventArgs.Empty);
                
            }
        }        
    }
    public override void Interaction(Player player)
    {
        if (!player.HasKitchenObject())
        {
           //player empty hand
           if(spawnPlateAmount > 0)
            {
                spawnPlateAmount--;
                KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
                OnPlateRemove?.Invoke(this, EventArgs.Empty);
            }
        }
        else
        {
            // player not carrying soemthing
        }

    }

}
