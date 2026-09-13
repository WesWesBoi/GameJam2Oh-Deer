using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //leaving notes for my fellow programers if they dont know waht does what look at the notes Ill leave
    public float moveSpeed = 5f;

    private void Update()
    {
        MovePlayer();
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
        Vector3 movement = new Vector3(horizontal, 0f, vertical);

        // Prevent diagonal movement from being faster
        movement = movement.normalized;

        // Move the player
        transform.position += movement * moveSpeed * Time.deltaTime;
    }
}