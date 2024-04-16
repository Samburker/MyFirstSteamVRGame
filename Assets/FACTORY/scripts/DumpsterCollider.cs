using UnityEngine;

public class DumpsterCollider : MonoBehaviour
{
    private Health playerHealth; // Reference to the Health script attached to the player
    public AudioClip damageSound; // Sound to play when taking damage
    public AudioClip successSound; // Sound to play when correct item has been destroyed

    private void Start()
    {
        // Find the player GameObject and get the Health component attached to it
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has a specific tag
        if (other.CompareTag("FalseItem"))
        {
            AudioManager.Instance.PlaySoundEffect(successSound);
            // Destroy the specific item
            Destroy(other.gameObject);
        }
        else
        {
            // If the colliding object is not the specific item, reduce player's health and play sound
            if (playerHealth != null)
            {
                // Reduce player's health by 1
                playerHealth.TakeDamage(1);

                // Play damage sound through AudioManager if assigned
                if (damageSound != null)
                {
                    AudioManager.Instance.PlaySoundEffect(damageSound);
                }
            }
        }
    }
}
