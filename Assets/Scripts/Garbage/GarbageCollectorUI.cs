using System;
using TMPro;
using UnityEngine;

public class GarbageCollectorUI : MonoBehaviour
{
    [SerializeField] private GarbageCollector garbageCollector;
    [SerializeField] private GarbageSpawner garbageSpawner;
    [SerializeField] private TMP_Text currentGarbageCountText;
    [SerializeField] private TMP_Text remainingGarbageCountText;

    private void Update()
    {
        currentGarbageCountText.text = $"Bag: {garbageCollector.garbageCollected}";
        remainingGarbageCountText.text = $"Remaining: {garbageSpawner.spawnedGarbages.Count}";
    }
}