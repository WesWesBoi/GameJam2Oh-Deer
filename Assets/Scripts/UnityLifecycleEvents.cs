using UnityEngine;
using UnityEngine.Events;

public class UnityLifecycleEvents : MonoBehaviour
{
    public UnityEvent OnAwake = new();
    public UnityEvent OnStart = new();
    public UnityEvent OnOnDestroy = new();

    private void Awake()
    {
        OnAwake.Invoke();
    }

    private void Start()
    {
        OnStart.Invoke();
    }

    private void OnDestroy()
    {
        OnOnDestroy.Invoke();
    }
}