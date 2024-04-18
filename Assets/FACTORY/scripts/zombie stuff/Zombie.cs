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
    public float stoppingDistance = 1f; // Distance at which to stop moving

    private int currentHealth; // Current health of the zombie
    private bool isTakingDamage = false; // Flag to indicate if the zombie is currently taking damage
    private UnityEngine.AI.NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component

    void Start()
    {
        currentHealth = maxHealth; // Set current health to max health
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>(); // Get reference to NavMeshAgent component
    }

    // Method to handle collisions
    void OnCollisionEnter(Collision collision)
    {
        // Check if collided with an object tagged as "Weapon" and the zombie is not currently taking damage
        if (collision.gameObject.CompareTag("Weapon") && !isTakingDamage)
        {
            // Set the flag to indicate that the zombie is currently taking damage
            isTakingDamage = true;

            // Reduce health
            TakeDamage(1); // Assume each collision deals 1 damage

            // Reset the flag after a short delay to allow taking damage again
            StartCoroutine(ResetTakingDamageFlag());
        }
    }

    // Method to reduce zombie's health
    public void TakeDamage(int damage)
    {
        currentHealth -= damage; // Reduce health by the amount of damage received
        Debug.Log("Zombie health lost: " + currentHealth);

        // Check if health has reached zero
        if (currentHealth <= 0)
        {
            // Enable ragdoll physics
            EnableRagdoll(true);

            // Stop the zombie's movement
            StopMovement();

            // Start coroutine to destroy the zombie after a delay
            StartCoroutine(DestroyAfterDelay(destructionDelay));
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

    // Method to stop the zombie's movement
    private void StopMovement()
    {
        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;
            navMeshAgent.velocity = Vector3.zero;
            navMeshAgent.angularSpeed = 0f;
            navMeshAgent.ResetPath();
        }
    }

    // Coroutine to destroy the zombie after a delay
    IEnumerator DestroyAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    // Coroutine to reset the flag indicating the zombie is taking damage
    IEnumerator ResetTakingDamageFlag()
    {
        yield return new WaitForSeconds(0.5f); // Adjust the delay as needed
        isTakingDamage = false; // Reset the flag
    }
}
