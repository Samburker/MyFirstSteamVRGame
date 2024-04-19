using UnityEngine;

public class AmbienceSound : MonoBehaviour
{
    public AudioClip[] loopedSounds; // Array of background sounds to play in a loop
    public AudioClip[] randomSounds; // Array of additional sounds to play randomly
    public float loopDelayMin = 5f; // Minimum delay between switching looped sounds
    public float loopDelayMax = 10f; // Maximum delay between switching looped sounds
    public float randomDelayMin = 10f; // Minimum delay between playing random sounds
    public float randomDelayMax = 20f; // Maximum delay between playing random sounds

    private AudioSource audioSource;
    private AudioClip currentLoopedSound;
    private float loopTimer;
    private float randomTimer;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (loopedSounds.Length > 0)
        {
            // Start playing a random background sound from the loopedSounds array
            currentLoopedSound = loopedSounds[Random.Range(0, loopedSounds.Length)];
            audioSource.clip = currentLoopedSound;
            audioSource.loop = true;
            audioSource.Play();

            // Set the initial loop timer
            loopTimer = Random.Range(loopDelayMin, loopDelayMax);
        }

        // Set the initial random sound timer
        randomTimer = Random.Range(randomDelayMin, randomDelayMax);
    }

    void Update()
    {
        // Check if it's time to switch the looped sound
        loopTimer -= Time.deltaTime;
        if (loopTimer <= 0 && loopedSounds.Length > 1)
        {
            SwitchLoopedSound();
            loopTimer = Random.Range(loopDelayMin, loopDelayMax);
        }

        // Check if it's time to play a random sound
        randomTimer -= Time.deltaTime;
        if (randomTimer <= 0 && randomSounds.Length > 0)
        {
            PlayRandomSound();
            randomTimer = Random.Range(randomDelayMin, randomDelayMax);
        }
    }

    // Method to switch the looped sound
    void SwitchLoopedSound()
    {
        AudioClip newLoopedSound;
        do
        {
            newLoopedSound = loopedSounds[Random.Range(0, loopedSounds.Length)];
        } while (newLoopedSound == currentLoopedSound); // Ensure the new looped sound is different from the current one

        currentLoopedSound = newLoopedSound;
        audioSource.clip = currentLoopedSound;
        audioSource.Play();
    }

    // Method to play a random sound
    void PlayRandomSound()
    {
        AudioClip randomSound = randomSounds[Random.Range(0, randomSounds.Length)];
        audioSource.PlayOneShot(randomSound);
    }
}
