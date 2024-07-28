using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private Transform iconContainer;
    [SerializeField] private Transform iconTemplate;

    private void Start()
    {
        iconTemplate.gameObject.SetActive(false);
    }
    public void SetRecipeSO (RecipeSO recipeSO)
    {
        recipeNameText.text = recipeSO.recipeName;  
        foreach(Transform child in iconContainer)
        {
            if (child == iconTemplate)
            {
                continue;
            }
            Destroy(child.gameObject);
            
        } 
        foreach(KitchenObjectSO kitchenObjectSO in recipeSO.kitchenObjectSOList)
        {
            Transform inconTransform = Instantiate(iconTemplate, iconContainer);
            iconContainer.gameObject.SetActive(true);
            inconTransform.GetComponent<Image>().sprite = kitchenObjectSO.sprite;

        }
    }
}
