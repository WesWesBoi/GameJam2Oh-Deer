using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent<PlayerInteractHandler> OnInteract = new();

    private bool isHighlighted;
    public UnityEvent<bool> OnHighlighted = new();
    
    public void Interact(PlayerInteractHandler interacter)
    {
        OnInteract.Invoke(interacter);
    }

    public void Highlight(bool isHighlighted)
    {
        if (this.isHighlighted == isHighlighted)
            return;
        
        this.isHighlighted = isHighlighted;
        OnHighlighted.Invoke(isHighlighted);
    }
}