using UnityEngine;

public class TreeBuilder : MonoBehaviour
{
    public GameObject woodDiscPrefab; // Prefab of the wood disc
    public GameObject jointConnectorPrefab; // Prefab for joint connector (for maintaining integrity of trunk)
    public GameObject cuttingTool; // Reference to the cutting tool (e.g., axe)
    public int numberOfDiscs = 5; // Number of discs in the tree
    public float discSpacing = 1.0f; // Spacing between each disc
    public bool freezeBaseDisc = true; // Option to freeze the base disc
    public float trunkJointBreakForce = 5000f; // Break force for the joints holding the trunk together
    public float trunkJointBreakTorque = 5000f; // Break torque for the joints holding the trunk together
    public float axeForceRequirement = 100f; // Minimum force required for the axe to break the trunk

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
            newDisc.tag = "WoodDisc"; // Assign a tag to identify wood discs

            // Connect to previous disc with a joint (e.g., FixedJoint)
            if (previousDisc != null)
            {
                // Add a fixed joint component to connect the current wood disc with the previous one
                FixedJoint joint = newDisc.AddComponent<FixedJoint>();

                // Connect the joint to the previous wood disc
                joint.connectedBody = previousDisc.GetComponent<Rigidbody>();

                // Set joint properties for trunk stability
                joint.breakForce = trunkJointBreakForce;
                joint.breakTorque = trunkJointBreakTorque;

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
        // Check for axe collision with wood discs
        if (cuttingTool != null)
        {
            // Ensure the cutting tool (axe) has a collider attached to it
            Collider axeCollider = cuttingTool.GetComponent<Collider>();
            if (axeCollider != null)
            {
                Collider[] hitColliders = Physics.OverlapSphere(cuttingTool.transform.position, 0.5f); // Adjust the radius as needed

                foreach (Collider collider in hitColliders)
                {
                    // Check if the cutting tool collides with a wood disc
                    if (collider.CompareTag("WoodDisc"))
                    {
                        // Calculate the force applied by the cutting tool
                        float axeForce = cuttingTool.GetComponent<Rigidbody>().velocity.magnitude;

                        // Check if the force applied by the cutting tool exceeds the requirement to break the trunk
                        if (axeForce >= axeForceRequirement)
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
    }
}
