using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject visualParticles;

    [SerializeField] private StoveCounter stoveCounter;

    private void Start()
    {
        stoveCounter.OnChanged += StoveCounter_OnChanged;
    }

    

    private void StoveCounter_OnChanged(object sender, StoveCounter.OnChangedEventArgs e)
    {
        bool is_Cooking = e.state == StoveCounter.State.Fried || e.state == StoveCounter.State.Frying;
        stoveOnVisual.SetActive(is_Cooking);
        visualParticles.SetActive(is_Cooking);  
    }
}
