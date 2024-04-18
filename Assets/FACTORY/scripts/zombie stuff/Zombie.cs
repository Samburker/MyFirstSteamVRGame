using UnityEngine;
using System.Collections.Generic;

public class Zombie : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health of the zombie
    public Animator animator; // Reference to the animator component
    public Collider mainCollider; // Reference to the main collider used for collisions
    public List<Collider> ragdollColliders; // List of colliders for the ragdoll physics
    public List<Rigidbody> ragdollRigidbodies; // List of rigidbodies for the ragdoll physics

    private int currentHealth; // Current health of the zombie

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max health
    }

    // Method to handle collisions
    void OnCollisionEnter(Collision collision)
    {
        // Check if collided with an object tagged as "Weapon"
        if (collision.gameObject.CompareTag("Weapon"))
        {
            // Reduce health
            currentHealth--;
            Debug.Log("Zombie health lost");

            // Check if health has reached zero
            if (currentHealth <= 0)
            {
                // Enable ragdoll physics
                EnableRagdoll(true);
            }
        }
    }

    // Method to enable/disable ragdoll physics
    public void EnableRagdoll(bool enableRagdoll)
    {
        animator.enabled = !enableRagdoll; // Disable animator when ragdoll is enabled

        // Enable/disable colliders for ragdoll
        foreach (Collider collider in ragdollColliders)
        {
            collider.enabled = enableRagdoll;
        }

        // Enable/disable rigidbodies for ragdoll
        foreach (Rigidbody rb in ragdollRigidbodies)
        {
            rb.isKinematic = !enableRagdoll;
        }

        // Enable/disable main collider
        mainCollider.enabled = !enableRagdoll;
    }
}
