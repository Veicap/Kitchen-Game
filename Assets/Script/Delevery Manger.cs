using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event EventHandler OnRecipeSpawned;
    public event EventHandler OnRecipeCompleted;
    public event EventHandler OnrecipeSuccess;
    public event EventHandler OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }
    [SerializeField] private RecipeListSO recipeListSO;
    private List<RecipeSO> waitingrecipeSOList;
    private float spawnRecipeTimer;
    private float spwanRecipeTimerMAX = 4f;
    private int watingRecipesMax = 4;
    private int recipeDeliverd = 0;
    
    private void Awake()
    {
       Instance = this;
       waitingrecipeSOList = new List<RecipeSO>();
    }
    private void Update()
    {
        spawnRecipeTimer -= Time.deltaTime;
        if(spawnRecipeTimer <= 0f)
        {
            spawnRecipeTimer = spwanRecipeTimerMAX;
            if(KitchenGameManager.Instance.IsGamePlaying() && waitingrecipeSOList.Count < watingRecipesMax)
            {
                RecipeSO waitingRecipeSO = recipeListSO.recipeSOList[UnityEngine.Random.Range(0, recipeListSO.recipeSOList.Count)];
                
                waitingrecipeSOList.Add(waitingRecipeSO);
                OnRecipeSpawned?.Invoke(this, EventArgs.Empty);
            }
            
        }
    }
    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        for(int i = 0; i < waitingrecipeSOList.Count; i++)
        {
            RecipeSO waitingRecipeSO = waitingrecipeSOList[i];
            if (waitingRecipeSO.kitchenObjectSOList.Count == plateKitchenObject.GetKitchenObjectSOList().Count)
            {
                // they have same number of ingredient
                bool plateContentMatchesRecipe = true;
                foreach(KitchenObjectSO recipeKitchenObjectSO in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach(KitchenObjectSO plateKitchenObjectSO in plateKitchenObject.GetKitchenObjectSOList())
                    {
                        ingredientFound = true;
                        break;
                    }
                    if(!ingredientFound)
                    {
                        plateContentMatchesRecipe = false;
                        // the recipe ingredient was not found on the flate
                    }
                }
                if (plateContentMatchesRecipe)
                {
                    recipeDeliverd++;
                    waitingrecipeSOList.RemoveAt(i);
                    OnRecipeCompleted?.Invoke(this, EventArgs.Empty);
                    OnrecipeSuccess?.Invoke(this, EventArgs.Empty);
                    return;
                }
            }
            
        }
        OnRecipeFailed?.Invoke(this, EventArgs.Empty);
        // No Matches found!
        // Player did not deliver correct recipe
        
    }
    public List<RecipeSO> GetWaitingRecipeSOList()
    {
        return waitingrecipeSOList;
    }
    public int GetRecipeDelivered()
    {
        return recipeDeliverd;
    }

}
