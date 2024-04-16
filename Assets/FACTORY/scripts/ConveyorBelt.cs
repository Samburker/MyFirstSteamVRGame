using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float moveSpeed = 1.0f; // Adjust this value in the Inspector to control the speed of the belt

    private void OnTriggerStay(Collider other)
    {
        // Check if the collider belongs to an object with a Rigidbody component
        Rigidbody rb = other.GetComponent<Rigidbody>();
        if (rb != null)
        {
            MoveObject(rb.gameObject);
        }
    }

    private void MoveObject(GameObject obj)
    {
        // Move the object forward along the conveyor belt
        obj.transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }

  
}
