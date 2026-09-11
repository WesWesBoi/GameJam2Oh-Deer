using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class InteractableObject : MonoBehaviour
{
    public UnityEvent OnInteract = new();

    private bool isHighlighted;
    public UnityEvent<bool> OnHighlighted = new();
    
    public void Interact()
    {
        OnInteract.Invoke();
    }

    public void Highlight(bool isHighlighted)
    {
        if (this.isHighlighted == isHighlighted)
            return;
        
        this.isHighlighted = isHighlighted;
        OnHighlighted.Invoke(isHighlighted);
    }
}