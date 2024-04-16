using UnityEngine;

public class HangarDoor : MonoBehaviour
{
    public float targetHeight = 5f; // Y-coordinate of the opening position
    public float moveDuration = 2f; // Duration for the door movement
    public AudioClip openSound; // Sound to play when the door starts opening

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private bool isOpen = false;
    private bool hasPlayedOpenSound = false; // Flag to track if the open sound has been played

    private AudioManager audioManager; // Reference to AudioManager script

    void Start()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(startPosition.x, targetHeight, startPosition.z);
        audioManager = FindObjectOfType<AudioManager>(); // Find AudioManager in the scene
    }

    // Reference this from another script to open the door
    public void OpenDoor()
    {
        if (!isOpen)
        {
            StartCoroutine(MoveDoor(targetPosition));
            isOpen = true;
        }
    }

    // Method to check if the door is open
    public bool IsDoorOpen()
    {
        return isOpen;
    }

    private System.Collections.IEnumerator MoveDoor(Vector3 targetPosition)
    {
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;

            // Play open sound when the door starts opening
            if (!hasPlayedOpenSound && openSound != null && audioManager != null)
            {
                audioManager.PlaySoundEffect(openSound);
                hasPlayedOpenSound = true;
            }

            yield return null;
        }

        // Ensure the door reaches the exact target position
        transform.position = targetPosition;
    }
}
