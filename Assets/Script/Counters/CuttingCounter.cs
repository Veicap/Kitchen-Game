using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CuttingCounter : BaseCounter, IHasProgress
{
    public static event EventHandler OnChop;
    [SerializeField] private CuttingRecipeSO[] cuttingRecipeArray;
    public event EventHandler <IHasProgress.OnProgressChangedEventArgs> OnProgressChanged;
    public event EventHandler OnCut;
    new public static void RestartStatic()
    {
        OnChop = null;
    }

    private int cuttingProgress;
    public override void Interaction(Player player)  
    {
        if (!HasKitchenObject())
        {
            // there is no kitchenObject here
            if (player.HasKitchenObject())
            {
                // player is carrying something
                // if have kitchenObject can be cut then player just can be put them on cuttingcounter
                if(HasKitchenObjectInput(player.GetKitchenObject().GetKitchenObjectSO())) {
                    
                    player.GetKitchenObject().SetKitchenObjectParent(this);
                    cuttingProgress = 0;
                    CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeS0WithInput(GetKitchenObject().GetKitchenObjectSO());
                    OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                    {
                        progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                    });
                } 
                
            }
            else
            {
                // player not carrying anyhthing
            }
        }
        else
        {
            if (player.HasKitchenObject())
            {
                //player is carrying something
                if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
                {

                    if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                    {
                        GetKitchenObject().DestroySelf();
                    }

                }
            }
            else
            {
                // player not carrying soemthing
                
                GetKitchenObject().SetKitchenObjectParent(player);
            }
            // there is a KitchenObject here
        }

    }
    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            if (HasKitchenObjectInput(GetKitchenObject().GetKitchenObjectSO()))
            {
                cuttingProgress++;
                OnCut?.Invoke(this, EventArgs.Empty);
                OnChop?.Invoke(this, EventArgs.Empty);
                CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeS0WithInput(GetKitchenObject().GetKitchenObjectSO());
                OnProgressChanged?.Invoke(this, new IHasProgress.OnProgressChangedEventArgs
                {
                    progressNormalized = (float)cuttingProgress / cuttingRecipeSO.cuttingProgressMax
                });
                if (cuttingProgress >= cuttingRecipeSO.cuttingProgressMax)
                {
                    KitchenObjectSO outputKichenObjectS0 = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(outputKichenObjectS0, this);
                }
                
            }
           
        }
    }
    private bool HasKitchenObjectInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeS0WithInput(inputKitchenObjectSO);
        return cuttingRecipeSO != null;
    }
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO inputKitchenObjectSO)
    {
        CuttingRecipeSO cuttingRecipeSO = GetCuttingRecipeS0WithInput(inputKitchenObjectSO);
        if(cuttingRecipeSO != null)
        {
            return cuttingRecipeSO.output;
        }
        else { return null; }
    }
    private CuttingRecipeSO GetCuttingRecipeS0WithInput(KitchenObjectSO inputKitchenObjectSO)
    {
        foreach (CuttingRecipeSO cuttingRecipeSO in cuttingRecipeArray)
        {
            if (cuttingRecipeSO.input == inputKitchenObjectSO)
            {
                return cuttingRecipeSO;
            }
        }
        return null;
    }
}

