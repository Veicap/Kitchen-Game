using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ClearCounter : BaseCounter
{
    [SerializeField] private KitchenObjectSO kitchenObjectSO; // Object origin    
    
    
    public override void Interaction(Player player)
    {
        /*Instantiate has 4 declaration in this circumstance I used the decleare is 
         * public static Object Instantiate(Object origin, Transform parent)
         * Object origin is kitchenObjectSo for ex like tomato
         * counterTopPoint is transform parent is likely position of game world where you want to place the instantiated kitchen object
        */
        // In this line code set localPosition = Vector3.zero means it will positioned at the exact origin of its parent(counterTopPoint) or in the world space if it has no parent
        if(!HasKitchenObject())
        {
            // there is no kitchenObject here
            if(player.HasKitchenObject())
            {
                // player is carrying something
                player.GetKitchenObject().SetKitchenObjectParent(this);
                //put kitchenObject on this counter
            }
            else
            {
                // player not carrying anything
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                //player is carring something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {                    
                    if(plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }                   
                } 
                else
                {
                    //player carrying something else not plate
                    if(GetKitchenObject().TryGetPlate(out plateKitchenObject)) {
                        if(plateKitchenObject.TryAddIngredient(player.GetKitchenObject().GetKitchenObjectSO()))
                        {
                            player.GetKitchenObject().DestroySelf();
                        }
                    }
                }
            } 
            else {
                // player not carrying soemthing
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            // there is a KitchenObject here
        }
       
    }
    
    
}
 