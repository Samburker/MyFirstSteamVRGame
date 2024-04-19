using UnityEngine;

public class HangarDoor : MonoBehaviour
{
    public float targetHeight = 5f; // Y-coordinate of the opening position
    public float moveDuration = 2f; // Duration for the door movement
    public float closeDelay = 5f; // Delay before closing the door after it's fully opened
    public bool leaveDoorOpen = false; // Whether to leave the door open indefinitely
    public AudioClip openSound; // Sound to play when the door starts opening
    public AudioClip closeSound; // Sound to play when the door starts closing

    private Vector3 startPosition;
    private Vector3 targetPosition;
    public bool isOpen = false;
    private bool hasPlayedOpenSound = false; // Flag to track if the open sound has been played
    private AudioSource audiosource;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(startPosition.x, startPosition.y + targetHeight, startPosition.z); // Update target position to be relative to current position
        audiosource = GetComponent<AudioSource>();
    }

    // Reference this from another script to open the door
    public void OpenDoor()
    {
        if (!isOpen)
        {
            StartCoroutine(MoveDoor(targetPosition, moveDuration));
            isOpen = true;
            MissileStacking missileStacking = FindAnyObjectByType<MissileStacking>();
            MissileSpawner missileSpawner = FindAnyObjectByType<MissileSpawner>();
            missileStacking.StartStackingTimer(missileSpawner.numMissilesToSpawn);
        }
    }

    // Reference this from another script to close the door
    public void CloseDoor()
    {
        if (isOpen)
        {
            if (closeSound != null)
            {
                audiosource.PlayOneShot(closeSound);
            }

            StartCoroutine(MoveDoor(startPosition, moveDuration));
            isOpen = false;
        }
    }


    // Method to check if the door is open
    public bool IsDoorOpen()
    {
        return isOpen;
    }

    private System.Collections.IEnumerator MoveDoor(Vector3 targetPosition, float duration)
    {
        float elapsedTime = 0f;
        Vector3 initialPosition = transform.position;

        while (elapsedTime < duration)
        {
            float t = elapsedTime / duration;
            transform.position = Vector3.Lerp(initialPosition, targetPosition, t);
            elapsedTime += Time.deltaTime;

            // Play open sound when the door starts opening
            if (!hasPlayedOpenSound && openSound != null)
            {
                audiosource.PlayOneShot(openSound);
                hasPlayedOpenSound = true;
            }

            yield return null;
        }

        // Ensure the door reaches the exact target position
        transform.position = targetPosition;

        if (leaveDoorOpen) yield break; // If leaving door open indefinitely, exit coroutine

        // Delay before closing the door
        yield return new WaitForSeconds(closeDelay);

        // Start closing the door
        CloseDoor();
    }
}
