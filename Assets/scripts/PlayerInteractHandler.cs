using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteractHandler : MonoBehaviour
{
    private SphereCollider sphereCollider;
    
    public float interactDistance = 3f;

    public UnityEvent<InteractableObject> OnInteract = new();
    public UnityEvent<InteractableObject> OnClosestChanged = new();

    private HashSet<InteractableObject> objectsInRange = new();
    public InteractableObject closestObject; 

    private void OnValidate()
    {
        if (sphereCollider == null)
            sphereCollider = GetComponent<SphereCollider>();

        sphereCollider.isTrigger = true;
        sphereCollider.radius = interactDistance;
    }

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void Update()
    {
        HandleInput();
    }

    /// <summary>
    /// Checks to see if any of the interact keys are pressed before firing the Interact event
    /// </summary>
    private void HandleInput()
    {
        bool isInteractKeyPressed = Keyboard.current[Key.Space].wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame;
        if (!isInteractKeyPressed)
            return;

        if (closestObject != null)
        {
            closestObject.Interact();
            OnInteract.Invoke(closestObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.TryGetComponent<InteractableObject>(out InteractableObject interactableObject))
            return;

        if (objectsInRange.Contains(interactableObject))
            return;
        
        objectsInRange.Add(interactableObject);
        UpdateClosestObject();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<InteractableObject>(out InteractableObject interactableObject))
            return;

        if (!objectsInRange.Contains(interactableObject))
            return;
        
        objectsInRange.Remove(interactableObject);
        UpdateClosestObject();
    }

    private void UpdateClosestObject()
    {
        InteractableObject previousClosestObject = closestObject;
        
        if (objectsInRange == null || objectsInRange.Count == 0)
            closestObject = null;
        else
            closestObject = objectsInRange.OrderBy(obj => Vector3.Distance(transform.position, obj.transform.position)).First();
        
        if (previousClosestObject != closestObject)
            OnClosestChanged.Invoke(closestObject);
    }
}