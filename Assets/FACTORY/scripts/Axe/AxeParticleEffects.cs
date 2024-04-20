using UnityEngine;

public class AxeParticleEffects : MonoBehaviour
{
    public float particleDuration = 0.5f; // Duration in seconds for which particle systems remain active

    private ParticleSystem[] particleSystems; // Array to store the particle systems

    void Start()
    {
        // Get all the ParticleSystem components attached to child GameObjects
        particleSystems = GetComponentsInChildren<ParticleSystem>(true);

        // Disable all particle systems at the start
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
    }

    public void EnableParticlesWhenHit()
    {
        
            // Enable all particle systems
            foreach (ParticleSystem ps in particleSystems)
            {
                ps.Play();
            }
            // Call the method to disable particle systems after a delay
            Invoke("DisableParticleSystems", particleDuration);
  
    }

    // Method to disable particle systems
    private void DisableParticleSystems()
    {
        foreach (ParticleSystem ps in particleSystems)
        {
            ps.Stop();
        }
    }
}
