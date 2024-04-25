using UnityEngine;

public class PlayWinMusic : MonoBehaviour
{
    public AudioClip musicClip; // The audio clip to play
    public float delayTime = 5f; // Delay time in seconds before playing the music

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Assuming AudioSource component is attached to the same GameObject
        if (audioSource == null)
        {
            // If AudioSource component is not found, add it
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        // Invoke the method to play the music after the delay
        Invoke("PlayMusic", delayTime);
    }

    void PlayMusic()
    {
        // Check if musicClip is assigned
        if (musicClip != null)
        {
            // Play the music clip
            audioSource.clip = musicClip;
            audioSource.Play();
        }
        else
        {
            Debug.LogWarning("Music clip is not assigned!");
        }
    }
}
