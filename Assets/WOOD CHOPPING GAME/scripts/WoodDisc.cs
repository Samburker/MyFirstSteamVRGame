using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WoodDisc : MonoBehaviour
{
    public GameObject slicedWoodPrefab; // Prefab for sliced wood pieces
    public float breakForceThreshold = 10f; // Minimum collision force required to slice the wood disc

    void OnCollisionEnter(Collision collision)
    {
        // Check if collision force is greater than threshold
        if (collision.relativeVelocity.magnitude >= breakForceThreshold)
        {
            // Spawn sliced wood prefab at the same position and rotation as the wood disc
            Instantiate(slicedWoodPrefab, transform.position, transform.rotation);

            // Apply gravity to the sliced wood prefab
            slicedWoodPrefab.GetComponent<Rigidbody>().useGravity = true;

            // Destroy the wood disc
            Destroy(gameObject);
        }
    }
}

