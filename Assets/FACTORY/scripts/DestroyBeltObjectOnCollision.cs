using UnityEngine;

public class DestroyBeltObjectOnCollision : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BeltObject"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.collider.CompareTag("FalseItem"))
        {
            Debug.Log("Lose health");
            Destroy(collision.gameObject);
        }

    }
}
