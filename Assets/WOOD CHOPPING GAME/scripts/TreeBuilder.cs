using UnityEngine;

public class TreeBuilder : MonoBehaviour
{
    public GameObject woodDiscPrefab; // Prefab of the wood disc
    public GameObject jointConnectorPrefab; // Prefab for joint connector (for maintaining integrity of trunk)
    public GameObject cuttingTool; // Reference to the cutting tool (e.g., axe)
    public int numberOfDiscs = 5; // Number of discs in the tree
    public float discSpacing = 1.0f; // Spacing between each disc
    public bool freezeBaseDisc = true; // Option to freeze the base disc

    void Start()
    {
        GameObject previousDisc = null;
        Rigidbody baseDiscRigidbody = null; // Reference to the base disc's Rigidbody component

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
            else
            {
                // Store reference to the Rigidbody of the base disc
                baseDiscRigidbody = newDisc.GetComponent<Rigidbody>();
            }

            // Update the previous disc reference for the next iteration
            previousDisc = newDisc;
        }

        // Freeze position and rotation of the base disc
        if (freezeBaseDisc && baseDiscRigidbody != null)
        {
            baseDiscRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }

    void Update()
    {
        // Check for cutting tool collision
        if (cuttingTool != null)
        {
            Collider[] hitColliders = Physics.OverlapSphere(cuttingTool.transform.position, 0.5f); // Adjust the radius as needed

            foreach (Collider collider in hitColliders)
            {
                // Check if the cutting tool collides with a wood disc
                if (collider.CompareTag("WoodDisc"))
                {
                    // Break the joint connecting the disc with the trunk
                    FixedJoint[] joints = collider.GetComponents<FixedJoint>();
                    foreach (FixedJoint joint in joints)
                    {
                        Destroy(joint);
                    }
                }
            }
        }
    }
}
