using System;
using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Diagnostics.CodeAnalysis;

// if you want to call envent let using system
public class Player : MonoBehaviour, IKitchenObjectParent

{   
    public static Player Instance { get; private set; }
   // public event EventHandler OnFootStep;
    public event EventHandler OnPickedUp;
    //<OnSeclectedCounterChangedEventArgs> used like genertic 
    public event EventHandler <OnSeclectedCounterChangedEventArgs> OnSelectedCounterChanged; // Defince Event to notify for Class SelectedCounterVisual 
    public class OnSeclectedCounterChangedEventArgs : EventArgs // class is inheriated by EventArgs
    {
        // clear slectedCounter from class above
        public BaseCounter selectedCounter; 
    }
    [SerializeField] private float moveSpeed = 7f;
    [SerializeField] private GameInput gameInput; 
    [SerializeField] private LayerMask countersLayerMask;
    [SerializeField] private Transform kitchenObjectHoldPoint;
    private Vector3 lastInteractDir;
    private bool isWalking;
    private KitchenObject kitchenObject;
    // decleare selectedCounter class ClearCounter
    private BaseCounter selectedCounter;


    private void Awake()
    {
        if(Instance != null)
        {
            Debug.LogError("There is more than one Player instance");
        }
        Instance = this;
        
    }
    private void Start()
    {
        // event OnteractionAction you have define in class GameInput
        gameInput.OnInteractAction += GameInput_OnInteractAction; //subcribe event
        gameInput.OnInteractAlternateAction += GameInput_OnInteractAlternateAction;
    }

    private void GameInput_OnInteractAlternateAction(object sender, EventArgs e)
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        if (selectedCounter != null)
        {
            selectedCounter.InteractAlternate(this);
        }
    }

    private void GameInput_OnInteractAction(object sender, System.EventArgs e) // My subcirber
    {
        if (!KitchenGameManager.Instance.IsGamePlaying()) return;
        // When event is triggered this interaction function is triggered
        if (selectedCounter != null)
        {
            selectedCounter.Interaction(this);
        }
        
    }

    private void Update()
    {
        HandleMovement();
        
        HandleInteraction();
    }
    public bool IsWalking()
    {
        return isWalking;
    }
    private void HandleInteraction()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float interactDistance = 2f;
        if(moveDir != Vector3.zero)
        {
            lastInteractDir = moveDir;
        }

        if(Physics.Raycast(transform.position, lastInteractDir, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            
            if (raycastHit.transform.TryGetComponent(out BaseCounter baseCounter))
            {
                
                if (baseCounter != selectedCounter)
                {
                    SetSelectedCounter(baseCounter); // trigger event                    
                }
            } 
            else
            {
                SetSelectedCounter(null); // trigger event
            }
        }
        else
        {
            SetSelectedCounter(null); //trigger event
        }
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();
        Vector3 moveDir = new Vector3(inputVector.x, 0f, inputVector.y);
        float playerHeight = 2.0f;
        float playerRadius = .5f;
        float maxDistance = 0.5f;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, maxDistance);
        if (!canMove)
        {
            // if in this case can not move but you still want move X
            Vector3 moveDirX = new Vector3(moveDir.x, 0f, 0f).normalized;
            canMove = moveDir.x != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirX, maxDistance);
            if (canMove)
            {
                moveDir = moveDirX;
            }
            else
            {
                // if in this case can not move but you still want move Z
                Vector3 moveDirZ = new Vector3(0f, 0f, moveDir.z).normalized;
                canMove = moveDir.z != 0 && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirZ, maxDistance);
                if (canMove)
                {
                    moveDir = moveDirZ;
                }
            }
        }
        if (canMove)
        {
            transform.position += moveDir * Time.deltaTime * moveSpeed;
        }

        isWalking = moveDir != Vector3.zero;
        
        transform.forward = Vector3.Slerp(transform.forward, moveDir, Time.deltaTime * 10);
    }
    private void SetSelectedCounter(BaseCounter selectedCounter)
    {
        this.selectedCounter = selectedCounter;
        OnSelectedCounterChanged?.Invoke(this, new OnSeclectedCounterChangedEventArgs // Raise the event trigger event 
        {
            selectedCounter = selectedCounter // if event trigger set selectedCounter(OnSelectedCounterChangeEventArgs) = selectedCounter(PlayerClass)
        });
    }

    public Transform GetChickKenObjectFollowTransform()
    {
        return kitchenObjectHoldPoint;
    }
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        this.kitchenObject = kitchenObject;
        if(kitchenObject != null)
        {
            OnPickedUp?.Invoke(this, EventArgs.Empty);
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
