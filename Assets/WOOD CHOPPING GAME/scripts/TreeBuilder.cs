using UnityEngine;

public class TreeBuilder : MonoBehaviour
{
    public GameObject trunkSectionPrefab; // Prefab of the trunk section
    public int numberOfSections = 5; // Number of sections in the tree
    public float sectionHeight = 1.0f; // Height of each trunk section
    public bool freezeBaseSection = true; // Option to freeze the base section

    void Start()
    {
        GameObject previousSection = null;
        Rigidbody baseSectionRigidbody = null; // Reference to the base section's Rigidbody component

        for (int i = 0; i < numberOfSections; i++)
        {
            // Calculate position of the current trunk section
            Vector3 sectionPosition = transform.position + Vector3.up * i * sectionHeight;

            // Instantiate trunk section prefab
            GameObject newSection = Instantiate(trunkSectionPrefab, sectionPosition, Quaternion.identity, transform);
            newSection.tag = "TrunkSection"; // Assign a tag to identify trunk sections

            // Connect to previous section
            if (previousSection != null)
            {
                // Add a fixed joint component to connect the current section with the previous one
                FixedJoint joint = newSection.AddComponent<FixedJoint>();

                // Connect the joint to the previous trunk section
                joint.connectedBody = previousSection.GetComponent<Rigidbody>();
            }
            else
            {
                // Store reference to the Rigidbody of the base section
                baseSectionRigidbody = newSection.GetComponent<Rigidbody>();
            }

            // Update the previous section reference for the next iteration
            previousSection = newSection;
        }

        // Freeze position and rotation of the base section
        if (freezeBaseSection && baseSectionRigidbody != null)
        {
            baseSectionRigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
    }
}
