using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

public class GarbageSpawner : MonoBehaviour
{
    [SerializeField] private List<Garbage> garbagePrefabs;
    public List<Garbage> spawnedGarbages = new();
    
    [Header("Config")]
    [SerializeField] private Vector2 size = new Vector2(1, 1);
    [SerializeField] private Vector2Int gridDimensions = new Vector2Int(2, 2);
    [SerializeField] private int garbageCount = 1;
    [SerializeField] private float cellPadding = 0.1f;
    [SerializeField] private float spawnHeight = 5f;
    private Vector2 individualGridSize;

    private void OnValidate()
    {
        int maxGarbage = gridDimensions.x * gridDimensions.y; // one garbage per grid spot
        garbageCount = Mathf.Clamp(garbageCount, 1, maxGarbage);
    }

    private void Awake()
    {
        individualGridSize = new Vector2(size.x / gridDimensions.x, size.y / gridDimensions.y);
    }

    private void Start()
    {
        SpawnAllGarbages();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;

        float width = size.x;
        float depth = size.y;
        float height = spawnHeight * 2f;

        int columns = Mathf.Max(1, gridDimensions.x);
        int rows = Mathf.Max(1, gridDimensions.y);

        Vector3 bottomLeft = transform.position - new Vector3(
            width / 2f,
            0f,
            depth / 2f
        );

        float cellWidth = width / columns;
        float cellDepth = depth / rows;

        Vector3 paddedCellSize = new Vector3(
            Mathf.Max(0f, cellWidth - cellPadding * 2f),
            height,
            Mathf.Max(0f, cellDepth - cellPadding * 2f)
        );

        for (int column = 0; column < columns; column++)
        {
            for (int row = 0; row < rows; row++)
            {
                Vector3 cellCenter = bottomLeft + new Vector3(
                    (column + 0.5f) * cellWidth,
                    0f,
                    (row + 0.5f) * cellDepth
                );

                cellCenter.y = transform.position.y;

                Gizmos.DrawWireCube(cellCenter, paddedCellSize);
            }
        }
    }

    public void SpawnAllGarbages()
    {
        if (gridDimensions.x <= 0 || gridDimensions.y <= 0)
            return;

        HashSet<Vector2Int> availableCells = new();
        for (int r = 0; r < gridDimensions.x; r++)
        {
            for (int c = 0; c < gridDimensions.y; c++)
            {
                availableCells.Add(new Vector2Int(r, c));
            }
        }

        Vector3 bottomLeft = transform.position - new Vector3(
            size.x / 2f,
            0f,
            size.y / 2f
        );

        for (int i = 0; i < garbageCount; i++)
        {
            Vector2Int randomCell = availableCells.ElementAt(
                UnityEngine.Random.Range(0, availableCells.Count)
            );

            Vector3 cellBottomLeft = bottomLeft + new Vector3(
                randomCell.x * individualGridSize.x,
                0f,
                randomCell.y * individualGridSize.y
            );

            Vector3 spawnPosition = new Vector3(
                UnityEngine.Random.Range(
                    cellBottomLeft.x + cellPadding,
                    cellBottomLeft.x + individualGridSize.x - cellPadding
                ),
                transform.position.y + spawnHeight,
                UnityEngine.Random.Range(
                    cellBottomLeft.z + cellPadding,
                    cellBottomLeft.z + individualGridSize.y - cellPadding
                )
            );

            SpawnGarbage(spawnPosition);
            availableCells.Remove(randomCell);
        }
    }
    
    public Garbage SpawnGarbage(Vector3 position)
    {
        Garbage newGarbage = Instantiate(garbagePrefabs[Random.Range(0, garbagePrefabs.Count)], position, Random.rotation, transform);
        newGarbage.Init(OnGarbageDestroyed);
        spawnedGarbages.Add(newGarbage);
        
        return newGarbage;  
    }

    public void OnGarbageDestroyed(Garbage garbage)
    {
        spawnedGarbages.Remove(garbage);
    }
}