using System;
using UnityEngine;

public class InteractPrompt : MonoBehaviour
{
    private InteractableObject currentInteractable;
    private RectTransform rectTransform;
    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
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
        
        Vector2 interactableScreenPosition = mainCamera.WorldToScreenPoint(currentInteractable.transform.position);
        rectTransform.position = interactableScreenPosition;
    }

    public void UpdateCurrentInteractable(InteractableObject interactable)
    {
        currentInteractable = interactable;
        gameObject.SetActive(currentInteractable != null);
    }
}