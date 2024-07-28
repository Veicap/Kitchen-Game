using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This class is scripted by selected counter visual
public class SelectedCounterVisual : MonoBehaviour
{

    [SerializeField] private BaseCounter baseCounter;

    [SerializeField] private GameObject[] visualGameObjectArray;
    
    
    private void Start()
    {
        
        Player.Instance.OnSelectedCounterChanged += Player_OnSelectedCounterChanged; // subcribe event we have already decleare in Player class
    }

    private void Player_OnSelectedCounterChanged(object sender, Player.OnSeclectedCounterChangedEventArgs e)
    {
        if(e.selectedCounter == baseCounter)
        {
            Show();
        }
        else
        {
            Hide();
        }
    }
    private void Show()
    {
        foreach(GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(true);
        }
        
    }
    private void Hide()
    {
        foreach (GameObject visualGameObject in visualGameObjectArray)
        {
            visualGameObject.SetActive(false);
        }
    }
}
