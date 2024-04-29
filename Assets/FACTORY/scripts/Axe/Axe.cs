using UnityEngine;

public class Axe : MonoBehaviour
{
    public float minImpactVelocity = 5f; // Minimum velocity required for the impact to register
    public int damage = 1; // Damage inflicted on the zombie per hit
    public AudioClip[] hitSounds; // Array of hit sounds for the axe
    public string zombieTag = "Zombie"; // Tag of the gameobject the axe can hit
    private AudioSource audioSource;
    private ControllerVelocity controllerVelocityScript;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;

        // Find the ControllerVelocity script in the scene
        controllerVelocityScript = FindObjectOfType<ControllerVelocity>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collision is with an object tagged as a zombie
        if (collision.gameObject.CompareTag(zombieTag))
        {
            
            // Get the controller velocity from the ControllerVelocity script
            if (controllerVelocityScript != null)
            {
                float impactVelocity = controllerVelocityScript.GetControllerVelocity();
                Debug.Log(impactVelocity);
                // Check if impact velocity is sufficient to register a hit
                if (impactVelocity >= minImpactVelocity)
                {
                    // Enable particles when zombie is hit
                    AxeParticleEffects axeParticles = gameObject.GetComponent<AxeParticleEffects>();
                    axeParticles.EnableParticlesWhenHit();

                    // Reduce zombie health
                    ZombieRagdollSwitch zombie = collision.gameObject.GetComponent<ZombieRagdollSwitch>();
                    if (zombie != null)
                    {
                        zombie.TakeDamage();
                        ZombieAudio zombieAudio = collision.gameObject.GetComponent<ZombieAudio>();
                        PlayRandomHitSound();
                        zombieAudio.PlayHealthLossSound();
                    }
                }
            }
        }
    }

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
