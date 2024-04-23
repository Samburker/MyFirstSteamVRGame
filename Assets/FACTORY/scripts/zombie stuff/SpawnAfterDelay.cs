using UnityEngine;

public class SpawnAfterDelay : MonoBehaviour
{
    public GameObject zombieSpawner;
    public float spawnDelay = 5f;

    void Start()
    {
        zombieSpawner.SetActive(false);
        Invoke("SpawnZombies", spawnDelay);

    }

    void SpawnZombies()
    {
        zombieSpawner.SetActive(true);
    }
}
