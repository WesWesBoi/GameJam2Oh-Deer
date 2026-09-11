using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SphereCollider))]
public class InteractableObject : MonoBehaviour
{
    public UnityEvent OnInteract = new();
    
    public void Interact()
    {
        OnInteract.Invoke();
    }
}