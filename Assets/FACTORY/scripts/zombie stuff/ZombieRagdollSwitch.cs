using UnityEngine;
using UnityEngine.AI;

public class ZombieRagdollSwitch : MonoBehaviour
{
    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;
    public Animator animator;
    public Collider mainCollider;
    public Rigidbody mainRigidbody;
    public NavMeshAgent zombieNavMeshAgent;

    public int health = 3; // Health of the zombie

    private void Start()
    {
        // Get all rigidbodies and colliders in the ragdoll
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        // Get the animator, main collider, and main rigidbody
        animator = GetComponent<Animator>();
        mainCollider = GetComponent<Collider>();
        mainRigidbody = GetComponent<Rigidbody>();

        // Disable the ragdoll components at start
        RagdollOff();
    }

    public void TakeDamage()
    {
        
             // If hit by a weapon, decrease health
            health--;
        Debug.Log("Zombie health lost");

            if (health <= 0)
            {
            // If health reaches zero or below, turn on ragdoll
            ZombieDie();
            }
      
    }

    public void RagdollOn()
    {
        // Disable animator
        animator.enabled = false;

        // Disable main collider
        mainCollider.enabled = false;

        mainRigidbody.isKinematic = true;

        // Enable all ragdoll colliders and rigidbodies
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            if (rb != mainRigidbody)
            {
                rb.isKinematic = false;
            }
        }
        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            if (col != mainCollider)
            {
                col.enabled = true;
            }
        }
    }

    public void RagdollOff()
    {
        // Enable animator
        animator.enabled = true;

        // Enable main collider
        mainCollider.enabled = true;

        mainRigidbody.isKinematic = false;

        // Disable all ragdoll colliders and rigidbodies
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            if (rb != mainRigidbody)
            {
                rb.isKinematic = true;
            }
        }
        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            if (col != mainCollider)
            {
                col.enabled = false;
            }
        }
    }
    public void ZombieDie()
    {
        RagdollOn();
        zombieNavMeshAgent.enabled = false;
    }

}
