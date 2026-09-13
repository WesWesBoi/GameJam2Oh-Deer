using UnityEngine;

public class GarbageCollector : MonoBehaviour
{
    public int garbageCollected = 0;
    public int maxGarbage = 30;

    public bool TryCollectGarbage(Garbage garbage)
    {
        if (garbageCollected >= maxGarbage)
            return false;
        
        garbageCollected++;
        return true;
    }

    public void EmptyGarbage()
    {
        if (garbageCollected <= 0)
            return;
        
        garbageCollected = 0;
    }
}