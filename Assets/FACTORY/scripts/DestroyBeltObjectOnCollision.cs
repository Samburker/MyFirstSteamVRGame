using UnityEngine;

public class DestroyBeltObjectOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BeltObject"))
        {
            Destroy(collision.gameObject);
        }
    }
}
