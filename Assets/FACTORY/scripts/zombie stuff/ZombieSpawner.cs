using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Reference to the zombie prefab to spawn
    public Transform[] spawnPoints; // Array of spawn points for zombies
    public int maxZombies = 5; // Maximum number of zombies to spawn
    public bool spawnOnStart = false; // Whether to spawn zombies on start

    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnZombies();
        }
    }

    // Method to spawn zombies at specified spawn points
    public void SpawnZombies()
    {
        int zombiesToSpawn = Mathf.Min(maxZombies, spawnPoints.Length);

        for (int i = 0; i < zombiesToSpawn; i++)
        {
            GameObject zombie = Instantiate(zombiePrefab, spawnPoints[i].position, Quaternion.identity);
            // Optionally, you can set up any other properties of the spawned zombies here
        }
    }
}
