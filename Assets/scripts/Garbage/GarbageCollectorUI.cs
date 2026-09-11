using System;
using TMPro;
using UnityEngine;

public class GarbageCollectorUI : MonoBehaviour
{
    [SerializeField] private GarbageCollector garbageCollector;
    [SerializeField] private TMP_Text text;

    private void Update()
    {
        text.text = $"Bag: {garbageCollector.garbageCollected}";
    }
}