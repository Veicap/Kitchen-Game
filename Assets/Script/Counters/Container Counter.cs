using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ContainerCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO;

    public event EventHandler OnPlayerGrabbedObject;

    public override void Interaction(Player player)
    {          
        if(!player.HasKitchenObject())
        {
            //Transform kitchenObjectTransform = Instantiate(kitchenObjectSO.prefab);
            KitchenObject.SpawnKitchenObject(kitchenObjectSO, player);
            //OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        
              
    }
    
}
