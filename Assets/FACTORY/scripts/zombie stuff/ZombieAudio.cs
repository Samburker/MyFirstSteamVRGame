using UnityEngine;

public class ZombieAudio : MonoBehaviour
{
    public AudioClip[] growlSounds; // Array of growl sounds for the zombie
    public AudioClip[] walkSounds; // Array of walk sounds
    public AudioClip healthLossSound; // Sound to play when the zombie loses health
    public AudioClip[] attackSounds; // Array of attack sounds

    public float minGrowlInterval = 5f; // Minimum interval between growl sounds
    public float maxGrowlInterval = 10f; // Maximum interval between growl sounds
    public float footstepDelay = 0.5f; // Delay between footstep sounds

    private AudioSource audioSource; // Reference to the AudioSource component
    private Animator animator; // Reference to the Animator component
    private bool isWalking = false; // Flag to track walking state
    private bool isPlayingWalkSound = false; // Flag to track if walk sound is playing
    private float footstepTimer = 0f; // Timer to track footstep delay
    private float nextGrowlTime;

    void Start()
    {
        audioSource = GetComponent<AudioSource>(); // Get the AudioSource component
        animator = GetComponent<Animator>(); // Get the Animator component

        // Set the initial time for the next growl sound
        nextGrowlTime = Time.time + Random.Range(minGrowlInterval, maxGrowlInterval);

        // Start playing walk sound if the zombie is initially walking
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            isWalking = true;
            PlayWalkSound();
        }
    }

    void Update()
    {
        // Check if the zombie is currently walking
        if (animator != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Walking"))
        {
            // If not already playing the walk sound, start playing it
            if (!isPlayingWalkSound)
            {
                isWalking = true;
                PlayWalkSound();
            }
        }
        else
        {
            // If the zombie is not walking, stop playing the walk sound if it's currently playing
            if (isPlayingWalkSound)
            {
                isWalking = false;
                StopWalkSound();
            }
        }

        // Update footstep timer
        if (isWalking)
        {
            footstepTimer += Time.deltaTime;

            // If enough time has passed, play a footstep sound and reset the timer
            if (footstepTimer >= footstepDelay)
            {
                PlayFootstepSound();
                footstepTimer = 0f;
            }
        }

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

    // Method to play the walk sound
    private void PlayWalkSound()
    {
        if (walkSounds.Length > 0 && audioSource != null && !isPlayingWalkSound)
        {
            // Choose a random walk sound from the array
            AudioClip randomWalk = walkSounds[Random.Range(0, walkSounds.Length)];

            // Set the walk sound to loop
            audioSource.clip = randomWalk;
            audioSource.loop = true;

            // Play the walk sound through the AudioSource
            audioSource.Play();

            isPlayingWalkSound = true;
        }
    }

    // Method to stop the walk sound
    private void StopWalkSound()
    {
        audioSource.loop = false; // Stop loop
        audioSource.Stop(); // Stop playing the walk sound
        isPlayingWalkSound = false;
    }

    // Method to play a footstep sound
    private void PlayFootstepSound()
    {
        if (walkSounds.Length > 0 && audioSource != null)
        {
            // Choose a random footstep sound from the array
            AudioClip randomFootstep = walkSounds[Random.Range(0, walkSounds.Length)];

            // Play the footstep sound through the AudioSource
            audioSource.PlayOneShot(randomFootstep);
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

    // Method to play the zombie hit sound
    public void PlayZombieHitSound()
    {
        if (attackSounds.Length > 0 && audioSource != null)
        {
            // Choose a random attack sound from the array
            AudioClip randomAttackSound = attackSounds[Random.Range(0, attackSounds.Length)];

            // Play the attack sound through the AudioSource
            audioSource.clip = randomAttackSound;
            audioSource.Play();
        }
    }
}
