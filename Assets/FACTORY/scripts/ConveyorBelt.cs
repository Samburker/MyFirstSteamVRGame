using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float moveSpeed = 1.0f; // Adjust this value in the Inspector to control the speed of the belt
    public string objectTag = "BeltObject"; // Tag of the objects to be moved by the conveyor belt

    private void OnCollisionStay(Collision collision)
    {
        if (collision.collider.CompareTag(objectTag))
        {
            MoveObject(collision.collider.gameObject);
        }
    }

    private void MoveObject(GameObject obj)
    {
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Move the object forward along the conveyor belt
            rb.MovePosition(rb.position + transform.forward * moveSpeed * Time.fixedDeltaTime);
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a visual representation of the conveyor belt area in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.localScale);
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
