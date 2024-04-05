using UnityEngine;

public class WoodDisc : MonoBehaviour
{
    public GameObject brokenWoodPrefab; // Prefab for broken wood pieces
    public float breakForceThreshold = 10f; // Minimum collision force required to break the wood disc

    void OnCollisionEnter(Collision collision)
    {
        // Check if collision force is greater than threshold
        if (collision.relativeVelocity.magnitude >= breakForceThreshold)
        {
            // Spawn broken wood prefab at the same position and rotation as the wood disc
            Instantiate(brokenWoodPrefab, transform.position, transform.rotation);

            // Activate gravity for the broken wood prefab
            brokenWoodPrefab.GetComponent<Rigidbody>().useGravity = true;

            // Destroy the wood disc
            Destroy(gameObject);
        }
    }
}
