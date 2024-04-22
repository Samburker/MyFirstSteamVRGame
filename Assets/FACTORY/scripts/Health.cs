using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health points
    public int currentHealth; // Current health points
    public GameObject damageParticles; // Reference to the damage particles GameObject
    public GameObject bloodParticles;
    public float damageParticleDuration = 1f; // Duration for which damage particles are active
    public AudioClip[] damageSounds; // Array of damage sound effects
    public AudioClip[] zombiedamageSounds; // Array of damage sound effects

    public AudioSource audioSource; // Reference to the player's AudioSource


    // Initialize health points
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
    }

    public void TakeZombieHitDamage(int damageAmount)
    {
        currentHealth -= damageAmount;

        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Play random damage sound effect
            if (damageSounds.Length > 0 && audioSource != null)
            {
                AudioClip randomSound = zombiedamageSounds[Random.Range(0, zombiedamageSounds.Length)];
                audioSource.PlayOneShot(randomSound);
            }

            // Activate zombiedamage particles
            if (bloodParticles != null)
            {
                ActivateBloodParticles();
            }
        }
    }


    // Method to decrease health points
    public void TakeDamage(int damageAmount)
    {
        // Reduce health by the specified amount
        currentHealth -= damageAmount;

        // Check if health has reached zero
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            // Play random damage sound effect
            if (damageSounds.Length > 0 && audioSource != null)
            {
                AudioClip randomSound = damageSounds[Random.Range(0, damageSounds.Length)];
                audioSource.PlayOneShot(randomSound);
            }

            // Activate damage particles
            if (damageParticles != null)
            {
                ActivateDamageParticles();
            }
        }
    }

    // Method to activate damage particles for a short duration
    private void ActivateDamageParticles()
    {
        damageParticles.SetActive(true);
        Invoke("DeactivateDamageParticles", damageParticleDuration);
    }

    private void ActivateBloodParticles()
    {
        bloodParticles.SetActive(true);
        Invoke("DeactivateBloodParticles", damageParticleDuration);
    }

    // Method to deactivate damage particles
    private void DeactivateDamageParticles()
    {
        damageParticles.SetActive(false);
    }

    // Method to deactivate damage particles cause by zombies

    private void DeactivateBloodParticles()
    {
        bloodParticles.SetActive(false);
    }


    // Method to increase health points (optional)
    public void Heal(int healAmount)
    {
        // Increase health by the specified amount
        currentHealth += healAmount;

        // Ensure health doesn't exceed maximum
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    // Method called when health reaches zero
    private void Die()
    {
        // Perform any actions for player death, such as game over, respawn, etc.
        Debug.Log("Player has died!");
        // Example: GameManager.Instance.GameOver();
    }
}
