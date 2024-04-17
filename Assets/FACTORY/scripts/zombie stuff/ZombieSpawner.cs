using UnityEngine;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Reference to the zombie prefab to spawn
    public Transform spawnPoint; // Spawn point for zombies
    public int maxZombies = 5; // Maximum number of zombies to spawn
    public bool spawnOnStart = false; // Whether to spawn zombies on start
    public GameObject groundReference; // Object whose y-position will be used as the ground level reference

    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnZombies();
        }
    }

    // Method to spawn zombies around the spawn point
    public void SpawnZombies()
    {
        if (groundReference == null)
        {
            Debug.LogError("Ground reference object is not assigned!");
            return;
        }

        // Get the ground level from the reference object's y-position
        float groundLevel = groundReference.transform.position.y;

        for (int i = 0; i < maxZombies; i++)
        {
            // Generate a random angle around the spawn point
            float angle = Random.Range(0f, 360f);
            // Calculate the spawn position based on the angle and distance from the spawn point
            Vector3 spawnDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            Vector3 spawnPosition = spawnPoint.position + spawnDirection;

            // Set the y-coordinate of the spawn position to match the ground level
            spawnPosition.y = groundLevel;

            // Spawn the zombie at the calculated position
            GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            // Optionally, you can set up any other properties of the spawned zombies here
        }
    }
}
