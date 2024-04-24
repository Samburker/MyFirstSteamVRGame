using UnityEngine;
using EasyTransition;
using UnityEngine.SceneManagement;

public static class PlayerStats
{
    public static int maxHealth = 10; // Maximum health points
    public static int currentHealth; // Current health points

    static PlayerStats()
    {
        // Initialize health points
        if (SceneManager.GetActiveScene().name == "Day1")
        {
            currentHealth = maxHealth;
        }
    }
}

public class Health : MonoBehaviour
{
    public GameObject damageParticles; // Reference to the damage particles GameObject
    public GameObject bloodParticles;
    public float damageParticleDuration = 1f; // Duration for which damage particles are active
    public AudioClip[] damageSounds; // Array of damage sound effects
    public AudioClip[] errorSounds;
    public AudioClip[] zombiedamageSounds; // Array of damage sound effects
    public AudioSource audioSource; // Reference to the player's AudioSource
    public TransitionSettings transition;

    // Initialize health points
    private void Start()
    {
        if (SceneManager.GetActiveScene().name == "Day1")
        {
            PlayerStats.currentHealth = PlayerStats.maxHealth;
        }
    }

    private void Update()
    {
    }

    public void TakeZombieHitDamage(int damageAmount)
    {
        PlayerStats.currentHealth -= damageAmount;

        if (PlayerStats.currentHealth <= 0)
        {
            if (damageSounds.Length > 0 && audioSource != null)
            {
                AudioClip randomSound = zombiedamageSounds[Random.Range(0, zombiedamageSounds.Length)];
                audioSource.PlayOneShot(randomSound);
            }
            if (bloodParticles != null)
            {
                ActivateBloodParticles();
            }
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
        PlayerStats.currentHealth -= damageAmount;

        // Check if health has reached zero
        if (PlayerStats.currentHealth <= 0)
        {
            if (damageSounds.Length > 0 && audioSource != null)
            {
                AudioClip randomSound = damageSounds[Random.Range(0, damageSounds.Length)];
                audioSource.PlayOneShot(randomSound);
                Invoke("PlayVoiceLine", 1f); //play Serenity voiceline after a second has passed from taking error damage 

            }
            if (damageParticles != null)
            {
                ActivateDamageParticles();
            }
            Die();
        }
        else
        {
            // Play random damage sound & Serenity voiceline effect
            if (damageSounds.Length > 0 && audioSource != null)
            {
                AudioClip randomSound = damageSounds[Random.Range(0, damageSounds.Length)];
                audioSource.PlayOneShot(randomSound);
                Invoke("PlayVoiceLine", 1f); //play Serenity voiceline after a second has passed from taking error damage 

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
    void PlayVoiceLine()
    {
        // Play a random voice line
        AudioClip randomVoiceLine = errorSounds[Random.Range(0, errorSounds.Length)];
        audioSource.PlayOneShot(randomVoiceLine);
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
        PlayerStats.currentHealth += healAmount;

        // Ensure health doesn't exceed maximum
        PlayerStats.currentHealth = Mathf.Min(PlayerStats.currentHealth, PlayerStats.maxHealth);
    }

    // Method called when health reaches zero
    private void Die()
    {
        TransitionManager.Instance().Transition(7, transition, 4f);
    }
}
