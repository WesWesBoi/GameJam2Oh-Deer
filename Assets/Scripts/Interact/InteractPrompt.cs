using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractPrompt : MonoBehaviour
{
    public TMP_Text text;
    
    private InteractableObject currentInteractable;
    private RectTransform rectTransform;
    private Camera mainCamera;
    private Vector2 offset;

    private void Awake()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    private void LateUpdate()
    {
        UpdatePromptPosition();
    }
    
    private void UpdatePromptPosition()
    {
        if (currentInteractable == null)
        {
            gameObject.SetActive(false);
            return;
        }
        
        Vector2 screenPosition = mainCamera.WorldToScreenPoint(currentInteractable.transform.position);
        screenPosition += offset;

        rectTransform.position = screenPosition;
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
            offset = new Vector2(rectTransform.rect.width * 0.5f, rectTransform.rect.height * 0.5f);
        }
    }
}