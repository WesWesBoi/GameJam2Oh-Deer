using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    //leaving notes for my fellow programers if they dont know waht does what look at the notes Ill leave
    public float moveSpeed = 5f;

    public Camera playerCamera;

    private void Update()
    {
        MovePlayer();
        CheckForInteract();
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

    void CheckForInteract()
    {
        if (Mouse.current.leftButton.wasPressedThisFrame ||
            Keyboard.current.spaceKey.wasPressedThisFrame)
            {
             Ray ray = playerCamera.ScreenPointToRay(
                new Vector3(Screen.width / 2, Screen.height / 2)
            );

            RaycastHit hit;

            if (Physics.Raycast(ray, out hit, 3f))
            {
                Interactable objectToInteract =
                    hit.collider.GetComponent<Interactable>();

                if (objectToInteract != null)
                {
                    objectToInteract.Interact();
                }
            }
        }

        void Interact()
        {
            Debug.Log("Interact button pressed!");
        }
    }
}