using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlayerInteractHandler : MonoBehaviour
{
    public float interactMaxRange = 1.5f;
    public InputActionReference interactAction;

    public UnityEvent<InteractableObject> OnInteract = new();
    public UnityEvent<InteractableObject> OnFocusedChanged = new();
    
    public InteractableObject focusedObject;
    private Transform mainCameraTransform;

    private void Awake()
    {
        interactAction.action.Enable();
        mainCameraTransform = Camera.main.transform;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(Camera.main.transform.position, Camera.main.transform.position + interactMaxRange * Camera.main.transform.forward);
    }

    private void Update()
    {
        HandleInput();
        UpdateFocusedObject();
    }
    
    private void HandleInput()
    {
        if (!interactAction.action.WasPressedThisFrame())
            return;

        ExecuteInteractCommand();
    }

    private void UpdateFocusedObject()
    {
        InteractableObject newFocusedObject = null;

        if (Physics.Raycast(
                mainCameraTransform.position,
                mainCameraTransform.forward,
                out RaycastHit hit,
                interactMaxRange,
                LayerMask.GetMask("Interactable")))
        {
            hit.transform.TryGetComponent(out newFocusedObject);
        }

        if (newFocusedObject == focusedObject)
            return;

        if (focusedObject != null)
            focusedObject.Highlight(false);

        focusedObject = newFocusedObject;

        if (focusedObject != null)
            focusedObject.Highlight(true);

        OnFocusedChanged.Invoke(focusedObject);
    }

    public void ExecuteInteractCommand()
    {
        if (focusedObject == null)
            return;

        focusedObject.Interact(this);
        OnInteract.Invoke(focusedObject);
    }
}