using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

[RequireComponent(typeof(SphereCollider))]
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
        UpdateClosestObject();
    }

    /// <summary>
    /// Checks to see if any of the interact keys are pressed before firing the Interact event
    /// </summary>
    private void HandleInput()
    {
        bool isInteractKeyPressed = Keyboard.current.spaceKey.wasPressedThisFrame || Mouse.current.leftButton.wasPressedThisFrame;
        if (!isInteractKeyPressed)
            return;

        if (closestObject != null)
        {
            closestObject.Interact();
            OnInteract.Invoke(closestObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.TryGetComponent(out InteractableObject interactableObject))
            return;

        objectsInRange.Add(interactableObject);
        UpdateClosestObject();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<InteractableObject>(out InteractableObject interactableObject))
            return;
        
        objectsInRange.Remove(interactableObject);
        UpdateClosestObject();
    }

    private void UpdateClosestObject()
    {
        InteractableObject previousClosestObject = closestObject;

        closestObject = null;
        float closestSqrDistance = float.MaxValue;

        foreach (InteractableObject obj in objectsInRange)
        {
            if (obj == null)
                continue;

            float sqrDistance =
                (obj.transform.position - transform.position).sqrMagnitude;

            if (sqrDistance < closestSqrDistance)
            {
                closestSqrDistance = sqrDistance;
                closestObject = obj;
            }
        }

        if (previousClosestObject == closestObject)
            return;

        if (previousClosestObject != null)
            previousClosestObject.Highlight(false);

        if (closestObject != null)
            closestObject.Highlight(true);

        OnClosestChanged.Invoke(closestObject);
    }
}