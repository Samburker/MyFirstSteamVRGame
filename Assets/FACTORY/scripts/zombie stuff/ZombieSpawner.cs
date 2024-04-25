using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ZombieSpawner : MonoBehaviour
{
    public GameObject zombiePrefab; // Reference to the zombie prefab to spawn
    public Transform[] spawnPoints; // Array of spawn points for zombies
    public int maxZombies = 5; // Maximum number of zombies to spawn
    public bool spawnOnStart = false; // Whether to spawn zombies on start
    public GameObject groundReference; // Object whose y-position will be used as the ground level reference
    public float minDistance = 2f; // Minimum distance between spawned zombies
    public float spawnInterval = 5f; // Time interval between spawns
    private List<GameObject> spawnedZombies = new List<GameObject>(); // List to store spawned zombies
    private int currentSpawnPointIndex = 0; // Index to track current spawn point

    private void Start()
    {
        if (spawnOnStart)
        {
            StartCoroutine(SpawnZombiesRoutine());
        }
    }

    // Coroutine to continuously spawn zombies
    private IEnumerator SpawnZombiesRoutine()
    {
        while (true)
        {
            if (spawnedZombies.Count < maxZombies)
            {
                SpawnZombie();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    // Method to spawn a single zombie from the next spawn point
    private void SpawnZombie()
    {
        if (groundReference == null)
        {
            Debug.LogError("Ground reference object is not assigned!");
            return;
        }

        // Get the ground level from the reference object's y-position
        float groundLevel = groundReference.transform.position.y;

        // Choose the current spawn point
        Transform spawnPoint = spawnPoints[currentSpawnPointIndex];

        // Increment the index to move to the next spawn point (loop around if necessary)
        currentSpawnPointIndex = (currentSpawnPointIndex + 1) % spawnPoints.Length;

        // Generate a random angle around the spawn point
        float angle = Random.Range(0f, 360f);
        // Calculate the spawn position based on the angle and distance from the spawn point
        Vector3 spawnDirection = Quaternion.Euler(0f, angle, 0f) * Vector3.forward;
        Vector3 spawnPosition = spawnPoint.position + spawnDirection;

        // Set the y-coordinate of the spawn position to match the ground level
        spawnPosition.y = groundLevel;

        // Check if the new spawn position is too close to existing spawn positions
        bool tooClose = false;
        foreach (GameObject zombie in spawnedZombies)
        {
            if (Vector3.Distance(spawnPosition, zombie.transform.position) < minDistance)
            {
                tooClose = true;
                break;
            }
        }

        if (!tooClose)
        {
            // Spawn the zombie at the calculated position
            GameObject newZombie = Instantiate(zombiePrefab, spawnPosition, Quaternion.identity);
            // Optionally, you can set up any other properties of the spawned zombies here

            // Add the spawned zombie to the list
            spawnedZombies.Add(newZombie);
        }
    }
}
