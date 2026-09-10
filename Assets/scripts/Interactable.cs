using UnityEngine;

public class Interactable : MonoBehaviour
{
    //leaving notes for my fellow programers if they dont know waht does what look at the notes Ill leave

    public float interactDistance = 3f;

    private Transform player;

    private void Start()
    {
        // Find the player when the game starts
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");

        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    public void Interact()
    {
        if (player == null)
        {
            return;
        }

        // Check how far the player is from the object
        float distance = Vector3.Distance(transform.position, player.position);

        if (distance <= interactDistance)
        {
            // Make the object disappear
            gameObject.SetActive(false);
        }
    }
}