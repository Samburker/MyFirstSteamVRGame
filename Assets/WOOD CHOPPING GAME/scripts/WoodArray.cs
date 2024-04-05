using UnityEngine;

public class WoodStack : MonoBehaviour
{
    public GameObject prefab; // The prefab you want to instantiate
    public int stackHeight = 5; // Number of objects to stack
    public float verticalSpacing = 1.0f; // Spacing between each object vertically

    void Start()
    {
        // Get the position of the GameObject this script is attached to
        Vector3 startPosition = transform.position;

        // Loop through the desired stack height
        for (int i = 0; i < stackHeight; i++)
        {
            // Calculate position based on the stack height, vertical spacing, and starting position
            Vector3 position = startPosition + new Vector3(0, i * verticalSpacing, 0);

            // Instantiate prefab at calculated position
            Instantiate(prefab, position, Quaternion.identity, transform);
        }
    }
}
