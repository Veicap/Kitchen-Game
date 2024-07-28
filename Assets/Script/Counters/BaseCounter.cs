using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public static event EventHandler OnDropOut;
    public static void RestartStatic()
    {
        OnDropOut = null;
    }
    [SerializeField] private Transform counterTopPoint;
    private KitchenObject kitchenObject;

    public virtual void Interaction(Player player)
    {
        Debug.LogError("Base Counter Interact()");  
    }
    public virtual void InteractAlternate(Player player)
    {
        //Debug.LogError("Base Counter InteractAlternate()");
    }
    public Transform GetChickKenObjectFollowTransform()
    {
        return counterTopPoint;
    }   
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnDropOut?.Invoke(this, EventArgs.Empty);
        }
    }
    public KitchenObject GetKitchenObject()
    {
        return kitchenObject;
    }
    public void ClearKitchenObject()
    {
        kitchenObject = null;
    }
    public bool HasKitchenObject()
    {
        return kitchenObject != null;
    }
}
