using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveBurnFlasingUI : MonoBehaviour
{
    
    private const string IS_FLASING = "IsFlashing";
    [SerializeField] private StoveCounter stoveCounter;

    private Animator animator;
    private void Awake()
    {
        animator = GetComponent<Animator>();    
    }
    private void Start()
    {
        stoveCounter.OnProgressChanged += StoveCounter_OnProgressChanged;
        animator.SetBool(IS_FLASING, false);
    }

    private void StoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e)
    {
        float burnShowProgressAmount = .5f;
        bool show = (stoveCounter.GetState() == StoveCounter.State.Fried && e.progressNormalized >= burnShowProgressAmount);
        animator.SetBool(IS_FLASING, show);
    }
    
}
