using UnityEngine;

public class RespawnToy : MonoBehaviour
{
    public Transform spawnPoint;

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
            MoveToSpawnPoint();
        }
    }

    void MoveToSpawnPoint()
    {
        // Reset the position and rotation of the current object to match the spawn point
        transform.position = spawnPoint.position;
        transform.rotation = spawnPoint.rotation;

        // Reset the velocity to zero to remove momentum
        Rigidbody rb = GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
        }
    }

}
