using UnityEngine;

public class AxeShooter : MonoBehaviour
{
    public GameObject axePrefab; // Prefab of the axe
    public float shootForce = 10f; // Force applied to the axe when shot

    // Update is called once per frame
    void Update()
    {
        // Check for input to shoot the axe
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("key pressed");
            // Instantiate the axe prefab at the position and rotation of the AxeShooter
            GameObject newAxe = Instantiate(axePrefab, transform.position, transform.rotation);

            // Get the Rigidbody component of the new axe
            Rigidbody axeRigidbody = newAxe.GetComponent<Rigidbody>();

            // Apply forward force to shoot the axe
            axeRigidbody.AddForce(transform.forward * shootForce, ForceMode.Impulse);
        }
    }
}
