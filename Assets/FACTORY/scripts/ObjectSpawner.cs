using UnityEngine;

[System.Serializable]
public class ObjectSpawnInfo
{
    public GameObject prefab;
    public float weight = 1f;
}

public class ObjectSpawner : MonoBehaviour
{
    public ObjectSpawnInfo[] objectsToSpawn; // Array of prefabs to spawn with weights
    public float minSpawnInterval = 1f; // Minimum time between spawns
    public float maxSpawnInterval = 3f; // Maximum time between spawns

    private void Start()
    {
        // Start spawning objects with random intervals
        Invoke("SpawnObject", Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    private void SpawnObject()
    {
        // Choose a random prefab based on weights
        GameObject objectToSpawn = ChooseRandomPrefab();

        // Instantiate the chosen prefab at the spawn point position and rotation
        Instantiate(objectToSpawn, transform.position, Quaternion.identity);

        // Schedule the next spawn
        Invoke("SpawnObject", Random.Range(minSpawnInterval, maxSpawnInterval));
    }

    private GameObject ChooseRandomPrefab()
    {
        // Calculate total weight
        float totalWeight = 0f;
        foreach (ObjectSpawnInfo spawnInfo in objectsToSpawn)
        {
            totalWeight += spawnInfo.weight;
        }

        // Generate a random value between 0 and totalWeight
        float randomValue = Random.Range(0f, totalWeight);

        // Choose the prefab based on weights
        GameObject chosenPrefab = null;
        foreach (ObjectSpawnInfo spawnInfo in objectsToSpawn)
        {
            randomValue -= spawnInfo.weight;
            if (randomValue <= 0)
            {
                chosenPrefab = spawnInfo.prefab;
                break;
            }
        }

        return chosenPrefab;
    }
}
