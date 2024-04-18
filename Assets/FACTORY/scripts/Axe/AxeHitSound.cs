using UnityEngine;

public class AxeHitSound : MonoBehaviour
{
    public AudioClip[] hitSounds; // Array of hit sounds for the axe
    public string zombieTag = "Zombie"; // Tag of the gameobject the axe can hit

    private AudioSource audioSource; // Reference to AudioSource component
    private bool isPlayingSound = false; // Flag to track if a hit sound is currently playing

    void Start()
    {
        // Add AudioSource component if not already attached
        if (!GetComponent<AudioSource>())
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        else
        {
            audioSource = GetComponent<AudioSource>();
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the collider belongs to an object with the zombie tag
        if (other.CompareTag(zombieTag))
        {
            // Play hit sound only if no sound is currently playing
            if (!isPlayingSound)
            {
                PlayRandomHitSound();
            }
            // If a sound is currently playing, stop it and play a new one immediately
            else
            {
                audioSource.Stop();
                PlayRandomHitSound();
            }
        }
    }

    // Method to play a random hit sound from the array
    private void PlayRandomHitSound()
    {
        if (hitSounds.Length > 0)
        {
            // Choose a random hit sound from the array
            AudioClip randomSound = hitSounds[Random.Range(0, hitSounds.Length)];
            audioSource.PlayOneShot(randomSound);
        }
    }
}
