using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class Zombie : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health of the zombie
    public Animator animator; // Reference to the animator component
    public List<Collider> ragdollColliders; // List of colliders for the ragdoll physics
    public List<Rigidbody> ragdollRigidbodies; // List of rigidbodies for the ragdoll physics
    public float destructionDelay = 5f; // Delay before destroying the zombie

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

                // Start coroutine to destroy the zombie after a delay
                StartCoroutine(DestroyAfterDelay(destructionDelay));
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
    }

    // Coroutine to destroy the zombie after a delay
    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }
}
