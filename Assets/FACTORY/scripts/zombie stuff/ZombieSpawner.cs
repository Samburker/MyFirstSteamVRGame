using UnityEngine;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Reference to the zombie prefab to spawn
    public Transform[] spawnPoints; // Array of spawn points for zombies
    public int maxZombies = 5; // Maximum number of zombies to spawn
    public bool spawnOnStart = false; // Whether to spawn zombies on start
    public GameObject groundReference; // Object whose y-position will be used as the ground level reference
    public float minDistance = 2f; // Minimum distance between spawned zombies

    private void Start()
    {
        if (spawnOnStart)
        {
            SpawnZombies();
        }
    }

    // Method to spawn zombies around the spawn points
    public void SpawnZombies()
    {
        if (groundReference == null)
        {
            Debug.LogError("Ground reference object is not assigned!");
            return;
        }

        // Get the ground level from the reference object's y-position
        float groundLevel = groundReference.transform.position.y;

        List<Vector3> spawnPositions = new List<Vector3>();

        while (spawnPositions.Count < maxZombies)
        {
            // Choose a random spawn point from the array
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Generate a random angle around the spawn point
            float angle = Random.Range(0f, 360f);
            // Calculate the spawn position based on the angle and distance from the spawn point
            Vector3 spawnDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
            Vector3 spawnPosition = randomSpawnPoint.position + spawnDirection;

            // Set the y-coordinate of the spawn position to match the ground level
            spawnPosition.y = groundLevel;

            // Check if the new spawn position is too close to existing spawn positions
            bool tooClose = false;
            foreach (Vector3 existingPosition in spawnPositions)
            {
                if (Vector3.Distance(spawnPosition, existingPosition) < minDistance)
                {
                    tooClose = true;
                    break;
                }
            }

            if (tooClose)
            {
                // Skip this iteration if too close to existing zombies
                continue;
            }

            // Spawn the zombie at the calculated position
            GameObject zombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            // Optionally, you can set up any other properties of the spawned zombies here

            // Add the spawn position to the list
            spawnPositions.Add(spawnPosition);
        }
    }
}
