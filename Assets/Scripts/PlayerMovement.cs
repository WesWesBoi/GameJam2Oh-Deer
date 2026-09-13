using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //leaving notes for my fellow programers if they dont know waht does what look at the notes Ill leave
    public float moveSpeed = 5f;
    public float rotationSpeed = 10f;

    private Vector3 moveDirection;

    private void Update()
    {
        MovePlayer();
        RotatePlayer();
    }

    void MovePlayer()
    {
        float horizontal = 0f;
        float vertical = 0f;

        // A and D
        if (Keyboard.current.aKey.isPressed)
        {
            horizontal = -1f;
        }

        if (Keyboard.current.dKey.isPressed)
        {
            horizontal = 1f;
        }

        // W and S
        if (Keyboard.current.wKey.isPressed)
        {
            vertical = 1f;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            vertical = -1f;
        }

        // Create the movement direction
        moveDirection = new Vector3(horizontal, 0f, vertical);

        // Prevent diagonal movement from being faster
        moveDirection = moveDirection.normalized;

        // Move the player
        transform.position += moveDirection * moveSpeed * Time.deltaTime;
    }

    private void RotatePlayer()
    {
        if (moveDirection == Vector3.zero)
            return;
        
        float targetAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.up);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
    }
}