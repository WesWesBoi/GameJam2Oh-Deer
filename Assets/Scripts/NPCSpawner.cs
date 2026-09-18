using UnityEngine;

public class NPCSpawner : MonoBehaviour
{
    public NPC npcPrefab;
    public Transform spawnPoint;

    public void SpawnNPC()
    {
        Instantiate(npcPrefab, spawnPoint.position, Quaternion.identity, transform);
    }
}