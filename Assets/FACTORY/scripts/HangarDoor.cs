using UnityEngine;

public class HangarDoor : MonoBehaviour
{
    public float targetHeight = 5f; // Y-coordinate of the opening position
    public float moveDuration = 2f; // Duration for the door movement
    public float delayBeforeOpen = 5f; // Delay before the door opens automatically

    private Vector3 startPosition;
    private Vector3 targetPosition;

    private bool isOpen = false;

    void Start()
    {
        startPosition = transform.position;
        targetPosition = new Vector3(startPosition.x, targetHeight, startPosition.z);
        Invoke("OpenDoor", delayBeforeOpen);
    }

    public void OpenDoor()
    {
        if (!isOpen)
        {
            StartCoroutine(MoveDoor(targetPosition));
            isOpen = true;
        }
    }

    private System.Collections.IEnumerator MoveDoor(Vector3 targetPosition)
    {
        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        // Ensure the door reaches the exact target position
        transform.position = targetPosition;
    }
}
