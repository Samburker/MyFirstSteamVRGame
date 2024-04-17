using UnityEngine;

public class MissileSpawner : MonoBehaviour
{
    public GameObject missilePrefab; // Reference to the missile prefab to spawn
    public int numMissilesToSpawn = 10; // Number of missiles to spawn
    public float spawnDelay = 1f; // Delay between each missile spawn
    public float startDelay = 2f; // Delay before the spawning starts
    public float hangarDoorDelay = 5f; // Delay before opening the hangar door

    private int missilesSpawned = 0;
    private bool isSpawning = false;

    private void Start()
    {
        Invoke("StartSpawning", startDelay);
    }

    private void StartSpawning()
    {
        isSpawning = true;
        InvokeRepeating("SpawnMissile", 0f, spawnDelay);

        // Call the HangarDoor script's OpenDoor method after a delay
        Invoke("OpenHangarDoor", hangarDoorDelay);
    }

    private void SpawnMissile()
    {
        if (isSpawning && missilesSpawned < numMissilesToSpawn)
        {
            // Random rotation
            Quaternion randomRotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);

            // Instantiate the missile with random rotation at the spawner's position
            Instantiate(missilePrefab, transform.position, randomRotation);
            missilesSpawned++;
        }
        else if (missilesSpawned >= numMissilesToSpawn)
        {
            // Cancel the InvokeRepeating once all missiles are spawned
            CancelInvoke("SpawnMissile");
        }
    }

    private void OpenHangarDoor()
    {
        // Find the HangarDoor script in the scene and call its OpenDoor method
        HangarDoor hangarDoor = FindObjectOfType<HangarDoor>();
        if (hangarDoor != null)
        {
            hangarDoor.OpenDoor();

        }
    }
}
