using System;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraInputController : InputAxisControllerBase<CameraInputController.Reader>
{
    void Update()
    { 
        if (!Application.isPlaying) 
            return;
        UpdateControllers();
    }

    [Serializable]
    public class Reader : IInputAxisReader
    {
        public float sensitivity = 1f;
        public InputActionReference lookInputAction;
        
        public float GetValue(UnityEngine.Object context, IInputAxisOwner.AxisDescriptor.Hints hint)
        {
            if(!lookInputAction.action.enabled)
                lookInputAction.action.Enable();
            
            Vector2 lookDirection = lookInputAction.action.ReadValue<Vector2>();
            
            if (hint == IInputAxisOwner.AxisDescriptor.Hints.X)
                return lookDirection.x * sensitivity;

            if (hint == IInputAxisOwner.AxisDescriptor.Hints.Y)
                return -lookDirection.y * sensitivity;

            return 0f;
        }
    }
}