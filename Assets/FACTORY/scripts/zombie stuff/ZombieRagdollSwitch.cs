using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class ZombieRagdollSwitch : MonoBehaviour
{
    private Rigidbody[] ragdollRigidbodies;
    private Collider[] ragdollColliders;
    public Animator animator;
    public Collider mainCollider;
    //public Rigidbody mainRigidbody; REMOVE THIS
    public NavMeshAgent zombieNavMeshAgent;

    public float sinkDuration = 2f; // Duration for sinking into the ground
    public float sinkSpeed = 1f; // Speed of sinking into the ground
    public float disableColliderDelay = 3f; // Delay before disabling colliders and rigidbodies
    public float destroyDelay;

    public int health = 3; // Health of the zombie

    private void Start()
    {
        // Get all rigidbodies and colliders in the ragdoll
        ragdollRigidbodies = GetComponentsInChildren<Rigidbody>();
        ragdollColliders = GetComponentsInChildren<Collider>();

        // Get the animator, main collider, and main rigidbody
        animator = GetComponent<Animator>();
        mainCollider = GetComponent<Collider>();
        //mainRigidbody = GetComponent<Rigidbody>();REMOVE THIS

        // Disable the ragdoll components at start
        RagdollOff();
    }

    public void TakeDamage()
    {

        // decrease health
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

        //mainRigidbody.isKinematic = true; REMOVE THIS

        // Enable all ragdoll colliders and rigidbodies
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
                rb.isKinematic = false;
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

        //mainRigidbody.isKinematic = false; REMOVE THIS

        // Disable all ragdoll colliders and rigidbodies
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
           
                rb.isKinematic = true;
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
        StartCoroutine(SinkIntoGroundAndDestroy());

    }


    IEnumerator SinkIntoGroundAndDestroy()
    {
        // Deactivate colliders and rigidbodies after a delay
        yield return new WaitForSeconds(disableColliderDelay);
        DeactivateCollidersAndRigidbodies();

        // Start sinking into the ground
        float initialY = transform.position.y; // Initial Y position
        float timer = 0f;

        // Sinking into the ground
        while (timer < sinkDuration)
        {
            float sinkAmount = sinkSpeed * Time.deltaTime;
            transform.position -= Vector3.up * sinkAmount;
            yield return null;
            timer += Time.deltaTime;
        }

        // Destroy the zombie after sinking into the ground
        yield return new WaitForSeconds(destroyDelay);
        Destroy(gameObject);
    }

    void DeactivateCollidersAndRigidbodies()
    {
        // Disable all colliders and rigidbodies
        foreach (Collider col in GetComponentsInChildren<Collider>())
        {
            col.enabled = false;
        }
        foreach (Rigidbody rb in GetComponentsInChildren<Rigidbody>())
        {
            rb.isKinematic = true;
        }
    }
}
