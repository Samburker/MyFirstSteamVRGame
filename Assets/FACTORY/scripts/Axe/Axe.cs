using UnityEngine;

public class Axe : MonoBehaviour
{
    public float minImpactVelocity = 5f; // Minimum velocity required for the impact to register
    public int damage = 1; // Damage inflicted on the zombie per hit
    public AudioClip[] hitSounds; // Array of hit sounds for the axe
    public string zombieTag = "Zombie"; // Tag of the gameobject the axe can hit

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an object tagged as a zombie
        if (collision.gameObject.CompareTag(zombieTag))
        {
            // Calculate impact velocity
            float impactVelocity = collision.relativeVelocity.magnitude;

            // Check if impact velocity is sufficient to register a hit
            if (impactVelocity >= minImpactVelocity)
            {
                // Reduce zombie health
                ZombieRagdollSwitch zombie = collision.gameObject.GetComponent<ZombieRagdollSwitch>();
                if (zombie != null)
                {
                    zombie.TakeDamage();
                }

                // Play hit sound
                PlayRandomHitSound();
            }
        }
    }

    private void PlayRandomHitSound()
    {
        if (hitSounds.Length > 0)
        {
            // Choose a random hit sound from the array
            AudioClip randomSound = hitSounds[Random.Range(0, hitSounds.Length)];
            AudioSource.PlayClipAtPoint(randomSound, transform.position);
        }
    }
}
