using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform followTarget;
    private Vector3 offset;

    private void Awake()
    {
        offset = transform.position - followTarget.position;
    }

    private void Update()
    {
        transform.position = followTarget.position + offset;
    }
}