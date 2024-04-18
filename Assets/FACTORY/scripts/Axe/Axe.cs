using UnityEngine;
using System.Collections; // Import the System.Collections namespace for using coroutines

public class Axe : MonoBehaviour
{
    public float minImpactVelocity = 5f; // Minimum velocity required for the impact to register
    public int damage = 1; // Damage inflicted on the zombie per hit
    public AudioClip[] hitSounds; // Array of hit sounds for the axe
    public GameObject[] hitParticles; // Array of hit particle systems
    public string zombieTag = "Zombie"; // Tag of the gameobject the axe can hit

    private void Start()
    {
        // Deactivate all hit particle systems initially
        foreach (var particleSystem in hitParticles)
        {
            particleSystem.SetActive(false);
        }
    }

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
                Zombie zombie = collision.gameObject.GetComponent<Zombie>();
                if (zombie != null)
                {
                    zombie.TakeDamage(damage);
                }

                // Play hit sound
                PlayRandomHitSound();

                // Activate hit particle systems
                foreach (var particleSystem in hitParticles)
                {
                    particleSystem.SetActive(true);
                }

                // Start a coroutine to deactivate hit particle systems after a short delay
                StartCoroutine(DeactivateParticlesAfterDelay());
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

    private IEnumerator DeactivateParticlesAfterDelay()
    {
        // Wait for a short delay
        yield return new WaitForSeconds(0.5f);

        // Deactivate hit particle systems
        foreach (var particleSystem in hitParticles)
        {
            particleSystem.SetActive(false);
        }
    }
}
