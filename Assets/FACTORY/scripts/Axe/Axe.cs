using UnityEngine;
using Valve.VR;

public class Axe : MonoBehaviour
{
    public float minImpactVelocity = 5f; // Minimum velocity required for the impact to register
    public int damage = 1; // Damage inflicted on the zombie per hit
    public AudioClip[] hitSounds; // Array of hit sounds for the axe
    public string zombieTag = "Zombie"; // Tag of the gameobject the axe can hit
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.playOnAwake = false;
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
                // enable particles when zombie is hit
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

    private void PlayRandomHitSound()
    {
        if (hitSounds.Length > 0)
        {
            // Choose a random hit sound from the array
            AudioClip randomSound = hitSounds[Random.Range(0, hitSounds.Length)];
            audioSource.PlayOneShot(randomSound);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Check the velocity of the controller
        SteamVR_Behaviour_Pose trackedObj = GetComponentInParent<SteamVR_Behaviour_Pose>();
        if (trackedObj != null)
        {
            Vector3 controllerVelocity = trackedObj.GetVelocity();

            // You can use controllerVelocity.magnitude to check the magnitude of the velocity
            // and compare it with your minimum impact velocity if needed.
        }
    }
}
