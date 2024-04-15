using UnityEngine;

public class ConveyorBeltObject : MonoBehaviour
{
    public float moveSpeed = 1.0f; // Adjust this value to control the speed of movement

    private Rigidbody rb;
    private bool isOnBelt = false;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (isOnBelt)
        {
            // Move the object forward while it's on the conveyor belt
            rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Belt"))
        {
            // Start moving the object forward when it collides with the conveyor belt
            isOnBelt = true;
            rb.constraints = RigidbodyConstraints.FreezeRotation; // Freeze rotation to prevent tumbling
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Belt"))
        {
            // Stop moving the object when it exits the conveyor belt
            isOnBelt = false;
            rb.constraints = RigidbodyConstraints.None; // Allow rotation again
        }
    }
}
