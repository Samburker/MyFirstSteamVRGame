using UnityEngine;

public class ConveyorSound : MonoBehaviour
{
    public AudioClip conveyorSound; // Sound clip for the conveyor belt
    public float volume = 0.5f; // Volume of the conveyor sound

    private AudioSource audioSource;

    void Start()
    {
        // Get or add AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Set up the audio source properties
        audioSource.clip = conveyorSound;
        audioSource.loop = true;
        audioSource.volume = volume;

        // Play the conveyor sound
        audioSource.Play();
    }

    void OnDestroy()
    {
        // Stop the conveyor sound when the object is destroyed
        if (audioSource != null)
        {
            audioSource.Stop();
        }
    }
}
