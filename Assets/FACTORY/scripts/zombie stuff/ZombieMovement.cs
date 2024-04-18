using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    [HideInInspector] // Hide the target field in the Inspector
    public Transform target; // Target for the zombie to move towards

    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>(); // Get the NavMeshAgent component

        // If the target is not assigned, try to find it at runtime
        if (target == null)
        {
            target = GameObject.FindWithTag("Generator").transform; // Find the generator GameObject with the "Generator" tag
            if (target == null)
            {
                Debug.LogError("Target not found! Make sure there is an object with the 'Generator' tag in the scene.");
                return;
            }
        }

        // Set the destination of the NavMeshAgent to the target's position
        navMeshAgent.SetDestination(target.position);
    }

    void Update()
    {
        // If the zombie has reached its destination, stop moving
        if (navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance)
        {
            // Optionally, trigger attack animation or behavior here
            // For example: GetComponent<Animator>().SetTrigger("Attack");
        }
    }
}
