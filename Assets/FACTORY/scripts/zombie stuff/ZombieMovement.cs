using UnityEngine;
using UnityEngine.AI;

public class ZombieMovement : MonoBehaviour
{
    private Transform player; // Reference to the player's transform
    private NavMeshAgent navMeshAgent; // Reference to the NavMeshAgent component

    void Start()
    {
        // Find the player GameObject using its tag
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Get the NavMeshAgent component attached to the zombie
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Set the initial destination to the player's position
        SetDestination(player.position);
    }

    void Update()
    {
        // Update the destination to the player's position if it has changed
        SetDestination(player.position);
    }

    // Method to set the destination for the NavMeshAgent
    private void SetDestination(Vector3 destination)
    {
        if (navMeshAgent != null && navMeshAgent.isActiveAndEnabled)
        {
            navMeshAgent.SetDestination(destination);
        }
    }
}
