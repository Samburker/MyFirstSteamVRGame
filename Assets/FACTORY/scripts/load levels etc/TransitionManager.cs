using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; // Required for IEnumerator

public class TransitionManager : MonoBehaviour
{
    public TextMeshProUGUI dayText; // Reference to the TextMeshProUGUI element displaying the day number
    private int currentDay = 0; // Current day
    public float waitforSecondsBeforeNextScene = 2f;

    private void Start()
    {
        // Update the day text
        dayText.text = "Day " + currentDay + 1;

        // Start the transition coroutine
        StartCoroutine(TransitionToNextDay());
    }

    private IEnumerator TransitionToNextDay()
    {
        // Wait for a short delay before starting the transition
        yield return new WaitForSeconds(waitforSecondsBeforeNextScene);

        // Load the next day's scene
        SceneManager.LoadScene("Day" + (currentDay + 1));
    }
}
