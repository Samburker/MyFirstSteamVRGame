using UnityEngine;

public class RespawnToy : MonoBehaviour
{
    public GameObject spawnPoint;

    void Start()
    {
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn point is not assigned!");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("FloorCollider"))
        {
            Respawn();
        }
    }

    void Respawn()
    {

        GameObject newObject = Instantiate(gameObject, spawnPoint.transform.position, spawnPoint.transform.rotation);
        Destroy(newObject.GetComponent<RespawnToy>());
    }
}
