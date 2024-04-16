using UnityEngine;

public class FloorCollider : MonoBehaviour
{
    private Health playerHealth; // Reference to the Health script attached to the player
    public AudioClip damageSound; // Sound to play when taking damage

    private void Start()
    {
        // Find the player GameObject and get the Health component attached to it
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has a tag that triggers health reduction
        if (other.CompareTag("BeltObject") || other.CompareTag("FalseItem"))
        {
            // Reduce player's health by 1
            playerHealth.TakeDamage(1);
            Destroy(other.gameObject);

            // Play damage sound through AudioManager if assigned
            if (damageSound != null)
            {
                AudioManager.Instance.PlaySoundEffect(damageSound);
            }
        }
    }
}
