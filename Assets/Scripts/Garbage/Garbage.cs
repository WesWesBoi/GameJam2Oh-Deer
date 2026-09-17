using System;
using UnityEngine;

public class Garbage : MonoBehaviour
{
    private Action<Garbage> OnDestroyCallback;
    
    public void Init(Action<Garbage> onDestroyCallback = null)
    {
        OnDestroyCallback = onDestroyCallback;
    }
    
    public void TryCollect(PlayerInteractHandler interacter)
    {
        if (!interacter.TryGetComponent(out GarbageCollector garbageCollector))
            return;

        if (garbageCollector.TryCollectGarbage(this))
        {
            OnCollect();
        }
    }

    public void OnCollect()
    {
        if (OnDestroyCallback != null)
            OnDestroyCallback.Invoke(this);
        
        Destroy(gameObject);
    }
}