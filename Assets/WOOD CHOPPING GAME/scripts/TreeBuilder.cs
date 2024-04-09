using UnityEngine;

public class TreeBuilder : MonoBehaviour
{
    public GameObject woodDiscPrefab; // Prefab of the wood disc
    public GameObject jointConnectorPrefab; // Prefab for joint connector (for maintaining integrity of trunk)
    public int numberOfDiscs = 5; // Number of discs in the tree
    public float discSpacing = 1.0f; // Spacing between each disc

    void Start()
    {
        GameObject previousDisc = null;

        for (int i = 0; i < numberOfDiscs; i++)
        {
            // Calculate position of the current wood disc
            Vector3 discPosition = transform.position + Vector3.up * i * discSpacing;

            // Instantiate wood disc prefab
            GameObject newDisc = Instantiate(woodDiscPrefab, discPosition, Quaternion.identity, transform);

            // Connect to previous disc with a joint (e.g., FixedJoint)
            if (previousDisc != null)
            {
                // Add a fixed joint component to connect the current wood disc with the previous one
                FixedJoint joint = newDisc.AddComponent<FixedJoint>();

                // Connect the joint to the previous wood disc
                joint.connectedBody = previousDisc.GetComponent<Rigidbody>();

                // Instantiate joint connector prefab to maintain integrity of trunk
                GameObject connector = Instantiate(jointConnectorPrefab, (newDisc.transform.position + previousDisc.transform.position) / 2, Quaternion.identity, transform);
            }

            // Update the previous disc reference for the next iteration
            previousDisc = newDisc;
        }
    }
}
