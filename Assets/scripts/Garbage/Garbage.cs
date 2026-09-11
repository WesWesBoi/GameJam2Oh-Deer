using UnityEngine;

public class Garbage : MonoBehaviour
{
    public void TryCollect(PlayerInteractHandler interacter)
    {
        if (!interacter.TryGetComponent(out GarbageCollector garbageCollector))
            return;

        if (garbageCollector.TryCollectGarbage(this))
        {
            Destroy(gameObject);
        }
    }
}