using UnityEngine;

public class DestroyBeltObjectOnCollision : MonoBehaviour
{
    private Health playerHealth; // Reference to the Health script attached to the player
    public AudioClip damageSound; // Sound to play when taking damage

    private void Start()
    {
        // Find the player GameObject and get the Health component attached to it
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("BeltObject"))
        {
            Destroy(collision.gameObject);
        }
        if (collision.collider.CompareTag("FalseItem"))
        {
            // Call the TakeDamage method from the player's Health component to reduce health by 1
            playerHealth.TakeDamage(1);

            // Play damage sound through AudioManager if assigned
            if (damageSound != null)
            {
                AudioManager.Instance.PlaySoundEffect(damageSound);
            }

            Destroy(collision.gameObject);
        }
    }
}
