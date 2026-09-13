using UnityEngine;

public class GarbageEmptier : MonoBehaviour
{
    public void EmptyGarbageCollector(PlayerInteractHandler interacter)
    {
        if (!interacter.TryGetComponent(out GarbageCollector collector))
            return;

        EmptyGarbageCollector(collector);
    }
    
    public void EmptyGarbageCollector(GarbageCollector collector)
    {
        collector.EmptyGarbage();
    }
}