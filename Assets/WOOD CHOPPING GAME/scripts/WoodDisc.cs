using UnityEngine;

public class WoodDisc : MonoBehaviour
{
    public float breakForceRequirement = 100f; // Minimum force required to break the wood disc
    public GameObject slicedDiscPrefab; // Prefab of the sliced wood disc

    // Called when a collider enters this object's collision zone
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collider belongs to the cutting tool (axe)
        if (collision.gameObject.CompareTag("Axe"))
        {
            // Calculate the force applied by the cutting tool
            float axeForce = collision.relativeVelocity.magnitude;

            // Check if the force applied by the cutting tool exceeds the requirement to break the wood disc
            if (axeForce >= breakForceRequirement)
            {
                // Break the wood disc
                BreakWoodDisc();
            }
        }
    }

    // Method to break the wood disc
    void BreakWoodDisc()
    {
        // Instantiate the sliced disc prefab at the same position and rotation as the wood disc
        Instantiate(slicedDiscPrefab, transform.position, transform.rotation);

        // Destroy this wood disc
        Destroy(gameObject);

        // Optionally, add effects or trigger events for breaking the disc
    }
}
