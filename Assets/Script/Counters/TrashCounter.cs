using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;
using Unity.VisualScripting;

public class TrashCounter : BaseCounter
{
    public static event EventHandler OnTrashDrop;
    public override void Interaction(Player player)
    {
        if(player.HasKitchenObject())
        {
            player.GetKitchenObject().DestroySelf();
            OnTrashDrop?.Invoke(this, new EventArgs());
        }
    }
    new public static void RestartStatic()
    {
        OnTrashDrop = null;
    }
}
