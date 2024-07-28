using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu] // Creat Asset Property when you want to add KitChenObjectS0
public class KitchenObjectSO : ScriptableObject
{
    public Transform prefab; // Transform component represents the positon, rotation and scale of a game object
    public Sprite sprite;
    public string objectName;
    
}
