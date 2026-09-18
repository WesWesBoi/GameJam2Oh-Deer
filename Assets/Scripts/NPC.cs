using System;
using UnityEngine;
using UnityEngine.AI;

public class NPC : MonoBehaviour
{
    public Garbage currentTargetGarbage;
    public GarbageCollector playerCollector;
    public bool isWalkingToTrashCan = false;
    public bool isPickingUpTrash = false;
    public float pickupTrashDuration = 1f;
    private float pickupTimer = 0f;
    public GarbageEmptier trashCan;
    public Animator animator;
    public float moveSpeed = 2f;

    private void Awake()
    {
        playerCollector = FindObjectOfType<GarbageCollector>();
        trashCan = FindObjectOfType<GarbageEmptier>();
    }

    private void Update()
    {
        if (isPickingUpTrash)
        {
            pickupTimer += Time.deltaTime;
            if (pickupTimer >= pickupTrashDuration)
            {
                isPickingUpTrash = false;
                pickupTimer = 0f;
                isWalkingToTrashCan = true;
                animator.Play("Walk");
            }
            return;
        }
        
        if (isWalkingToTrashCan)
        {
            MoveTo(trashCan.transform.position);
            return;
        }
        
        if (currentTargetGarbage == null)
        {
            currentTargetGarbage = FindAnyObjectByType<Garbage>();
            return;
        }
        
        MoveTo(currentTargetGarbage.transform.position);
    }

    private void MoveTo(Vector3 destination)
    {
        Vector3 direction = destination - transform.position;
        direction.y = 0;
        direction.Normalize();
        transform.position += direction * moveSpeed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (currentTargetGarbage != null && other.gameObject == currentTargetGarbage.gameObject)
        {
            isPickingUpTrash = true;
            animator.Play("Pickup");
            playerCollector.AddTotalGarbage();
            other.gameObject.GetComponent<Garbage>().OnCollect();
        }

        if (isWalkingToTrashCan && other.gameObject.TryGetComponent(out GarbageEmptier emptier))
        {
            isWalkingToTrashCan = false;
        }
    }
}