using UnityEngine;

public class ConveyorBelt : MonoBehaviour
{
    public float moveSpeed = 1.0f; // Adjust this value in the Inspector to control the speed of the belt
    public string objectTag = "BeltObject"; // Tag of the objects to be moved by the conveyor belt

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag(objectTag))
        {
            Rigidbody rb = other.GetComponent<Rigidbody>();
            if (rb != null)
            {
                MoveObject(rb.gameObject);
            }
        }
    }

    private void MoveObject(GameObject obj)
    {
        // Move the object forward along the conveyor belt
        obj.transform.Translate(transform.forward * moveSpeed * Time.deltaTime, Space.World);
    }

    private void OnDrawGizmosSelected()
    {
        // Draw a visual representation of the conveyor belt area in the scene view
        Gizmos.color = Color.yellow;
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, new Vector3(transform.localScale.x, transform.localScale.y, 0.1f));
        Gizmos.DrawWireCube(Vector3.zero, Vector3.one);
    }
}
