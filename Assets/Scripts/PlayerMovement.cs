using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public InputActionReference moveInputAction;
    
    public float moveSpeed = 5f;
    
    private Vector3 moveDirection;

    private void Awake()
    {
        moveInputAction.action.Enable();
    }

    private void Update()
    {
        MoveAndRotatePlayer();
    }

    void MoveAndRotatePlayer()
    {
        Vector2 input = moveInputAction.action.ReadValue<Vector2>();
        moveDirection = new Vector3(input.x, 0f, input.y);
        // Prevent diagonal movement from being faster
        moveDirection = moveDirection.normalized;
    
        float targetAngle = Camera.main.transform.eulerAngles.y;
        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.up);
        transform.position += targetRotation * moveDirection * (moveSpeed * Time.deltaTime);
        transform.rotation = targetRotation;
    }
}