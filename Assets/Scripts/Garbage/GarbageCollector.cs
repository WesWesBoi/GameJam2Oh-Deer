using UnityEngine;
using UnityEngine.Events;

public class GarbageCollector : MonoBehaviour
{
    public int garbageCollected = 0;
    public int maxGarbage = 30;
    public int totalCollected = 0;

    public UnityEvent<int> OnGarbageCollected = new();
    public UnityEvent<int> OnTotalGarbageCollected = new();


    public AudioClip emptyGarbageClip;
    public AudioSource emptyGarbageClipSource;

    public bool TryCollectGarbage(Garbage garbage)
    {
        if (garbageCollected >= maxGarbage)
            return false;
        
        garbageCollected++;
        OnGarbageCollected.Invoke(garbageCollected);

        AddTotalGarbage();
        return true;
    }

    public void AddTotalGarbage()
    {
        totalCollected++;
        OnTotalGarbageCollected.Invoke(totalCollected);
    }

    public void EmptyGarbage()
    {
        if (garbageCollected <= 0)
            return;
        
        garbageCollected = 0;
        emptyGarbageClipSource.PlayOneShot(emptyGarbageClip);
    }
}