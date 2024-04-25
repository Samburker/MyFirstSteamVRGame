using UnityEngine;

public class MissileStacking : MonoBehaviour
{
    [Tooltip("Stacking timer starts when explicitly started.")]
    public float stackingTimeLimit = 30f; // Time limit for stacking missiles
    public AudioClip successSound; // Sound to play when stacking is successful
    public int healthLostPerMissile = 1; // Health points lost per missile not on pallet
    public string palletTag = "Pallet"; // Tag of the pallet collider

    private float remainingTime; // Remaining time for stacking
    private bool isStacked; // Flag to track if missiles are stacked
    private bool hasFailed; // Flag to track if the fail sound has been played
    private int totalMissiles; // Total number of missiles spawned

    private Collider palletCollider; // Reference to the pallet collider

    private void Start()
    {
        // Find the pallet collider in the scene
        palletCollider = GameObject.FindGameObjectWithTag(palletTag)?.GetComponent<Collider>();
    }

    // Method to start the stacking timer with total number of missiles
    public void StartStackingTimer(int totalMissiles)
    {
        this.totalMissiles = totalMissiles; // Set the total number of missiles
        remainingTime = stackingTimeLimit; // Initialize the timer
        isStacked = false;
        hasFailed = false;
    }

    private void Update()
    {
        if (!isStacked && remainingTime > 0f)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0f && !hasFailed)
            {
                // Check if any missile is touching the pallet trigger collider
                if (palletCollider != null)
                {
                    Collider[] colliders = Physics.OverlapBox(palletCollider.bounds.center, palletCollider.bounds.extents, Quaternion.identity);

                    int missilesOnPallet = 0;

                    foreach (Collider collider in colliders)
                    {
                        if (collider.CompareTag("Missile"))
                        {
                            if (IsOnPallet(collider.gameObject))
                            {
                                missilesOnPallet++;
                            }
                        }
                    }

                    // Calculate health points lost and reduce player's health
                    int healthLost = (totalMissiles - missilesOnPallet) * healthLostPerMissile;
                    GameObject.FindGameObjectWithTag("Player").GetComponent<Health>().TakeDamage(healthLost); // Reduce player's health
                    DestroyMissileObjects(); // Destroy all missiles

                    // Play appropriate sound effects
                    if (missilesOnPallet == totalMissiles)
                    {
                        AudioManager.Instance.PlaySoundEffect(successSound);
                        isStacked = true;
                    }
                    else
                    {
                 
                        hasFailed = true;
                    }
                }
                else
                {
                    Debug.LogWarning("Pallet collider not found.");
                }
            }
        }
    }

    private bool IsOnPallet(GameObject missile)
    {
        Collider[] colliders = Physics.OverlapBox(missile.transform.position, missile.GetComponent<Collider>().bounds.extents, Quaternion.identity);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag(palletTag))
            {
                return true;
            }
        }
        return false;
    }

    private void DestroyMissileObjects()
    {
        GameObject[] missiles = GameObject.FindGameObjectsWithTag("Missile");

        foreach (GameObject missile in missiles)
        {
            Destroy(missile);
        }
    }
}
