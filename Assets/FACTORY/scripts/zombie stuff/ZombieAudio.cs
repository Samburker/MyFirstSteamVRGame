using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    public AudioClip[] growlSounds; // Array of growl sounds for the zombie
    public AudioClip healthLossSound; // Sound to play when the zombie loses health
    public AudioClip[] attackSounds;

    public float minGrowlInterval = 5f; // Minimum interval between growl sounds
    public float maxGrowlInterval = 10f; // Maximum interval between growl sounds

    private AudioSource audioSource; // Reference to the AudioSource component
    private float nextGrowlTime; // Time for the next growl sound

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component

        // Set the initial time for the next growl sound
        nextGrowlTime = Time.time + Random.Range(minGrowlInterval, maxGrowlInterval);

        
    }

    void Update()
    {
        // Check if it's time to play a growl sound
        if (Time.time >= nextGrowlTime)
        {
            // Play a random growl sound
            PlayGrowlSound();

            // Update the time for the next growl sound
            nextGrowlTime = Time.time + Random.Range(minGrowlInterval, maxGrowlInterval);
        }
    }

    // Method to play a random growl sound
    private void PlayGrowlSound()
    {
        if (growlSounds.Length > 0 && audioSource != null)
        {
            // Choose a random growl sound from the array
            AudioClip randomGrowl = growlSounds[Random.Range(0, growlSounds.Length)];

            // Play the growl sound through the AudioSource
            audioSource.clip = randomGrowl;
            audioSource.Play();
        }
    }

    // Method to play the health loss sound
    public void PlayHealthLossSound()
    {
        if (healthLossSound != null && audioSource != null)
        {
            // Play the health loss sound through the AudioSource
            audioSource.clip = healthLossSound;
            audioSource.Play();
        }
    }

    public void PlayZombieHitSound()
    {
        // Choose a random attack sound from the array
        AudioClip attackSound = attackSounds[Random.Range(0, attackSounds.Length)];

        // Play the attack sound through the AudioSource
        audioSource.clip = attackSound;
        audioSource.Play();
    }
}
