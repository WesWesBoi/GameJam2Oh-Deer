using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractPrompt : MonoBehaviour
{
    public TMP_Text text;
    
    private InteractableObject currentInteractable;
    private RectTransform rectTransform;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (currentInteractable == null)
        {
            gameObject.SetActive(false);
            return;
        }
    }

    public void UpdateCurrentInteractable(InteractableObject interactable)
    {
        currentInteractable = interactable;
        gameObject.SetActive(currentInteractable != null);

        if (currentInteractable != null)
        {
            string displayText = $"{currentInteractable.displayName}";
            if (currentInteractable.TryGetComponent(out Stackable stackable))
            {
                displayText = $"{stackable.stackCount}x {currentInteractable.displayName}";
            }

            text.text = displayText;
            LayoutRebuilder.ForceRebuildLayoutImmediate(rectTransform);
        }
    }
}