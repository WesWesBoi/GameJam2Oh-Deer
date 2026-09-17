using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject character;
    public int maxSpawns = 3;
    public int currentSpawns = 0;
    public float delay = 1f;
    public Transform maxX;
    public Transform maxZ;
    private float randomX;
    private float randomZ;
    public Transform spawnPosition;
    public Quaternion Q; 
    public NPCFollow nf;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (currentSpawns < maxSpawns)
        {
            if (delay > 0)
            {
                delay = delay - Time.deltaTime;
            }
            else 
            {
                selectRandomSpawnPoint();
                currentSpawns = currentSpawns + 1;
                Instantiate(character, spawnPosition.position, Q);
                delay = 2f;
            }
        }
    }

    private void selectRandomSpawnPoint()
    {
        float distanceX = transform.position.x - maxX.position.x;
        float distanceZ = transform.position.z - maxZ.position.z;

        randomX = Random.Range((transform.position.x + distanceX), maxX.position.x);
        randomZ = Random.Range((transform.position.z + distanceZ), maxZ.position.z);
        spawnPosition.position = new Vector3(randomX, spawnPosition.position.y, randomZ);
        Q = Quaternion.Euler(new Vector3(0, Random.Range(0, 361), 0));
    }
}
