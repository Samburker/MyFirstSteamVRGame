using UnityEngine;

public class WoodDisc : MonoBehaviour
{
    public GameObject trunkSectionPrefab; // Prefab of the trunk section

    // Called when a collider enters this object's collision zone
    void OnCollisionEnter(Collision collision)
    {
        // Check if the collider belongs to the cutting tool (axe)
        if (collision.gameObject.CompareTag("Axe"))
        {
            // Break the tree into separate trunk sections
            BreakTree();
        }
    }

    // Method to break the tree into separate trunk sections
    void BreakTree()
    {
        // Instantiate trunk section prefab at the position of this disc
        Instantiate(trunkSectionPrefab, transform.position, transform.rotation);

        // Destroy this wood disc
        Destroy(gameObject);
    }
}
