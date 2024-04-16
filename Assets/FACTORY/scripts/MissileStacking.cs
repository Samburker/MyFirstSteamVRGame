using UnityEngine;

public class MissileStacking : MonoBehaviour
{
    public float stackingTimeLimit = 30f; // Time limit for stacking missiles
    public AudioClip successSound; // Sound to play when stacking is successful
    public AudioClip failSound; // Sound to play when stacking fails

    private float remainingTime; // Remaining time for stacking
    private bool isStacked; // Flag to track if missiles are stacked
    private bool hasFailed; // Flag to track if the fail sound has been played

    private void Start()
    {
        remainingTime = stackingTimeLimit;
        isStacked = false;
        hasFailed = false;
    }

    // Method to start the stacking timer
    public void StartStackingTimer()
    {
        remainingTime = stackingTimeLimit;
        isStacked = false;
        hasFailed = false;
    }

    private void Update()
    {
        if (!isStacked)
        {
            remainingTime -= Time.deltaTime;

            if (remainingTime <= 0f && !hasFailed)
            {
                // Check if any missile is touching the pallet trigger collider
                Collider[] colliders = Physics.OverlapBox(transform.position, transform.localScale / 2f, Quaternion.identity);
                bool missileStacked = false;
                foreach (Collider collider in colliders)
                {
                    if (collider.CompareTag("Missile"))
                    {
                        missileStacked = true;
                        break;
                    }
                }

                if (missileStacked)
                {
                    // Play success sound and set isStacked flag to true
                    AudioManager.Instance.PlaySoundEffect(successSound);
                    isStacked = true;
                }
                else
                {
                    // Play fail sound and set hasFailed flag to true
                    AudioManager.Instance.PlaySoundEffect(failSound);
                    hasFailed = true;
                }
            }
        }
    }
}
