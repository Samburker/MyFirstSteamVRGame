using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections; // Required for IEnumerator

public class TransitionManager : MonoBehaviour
{
    public TextMeshProUGUI dayText; // Reference to the TextMeshProUGUI element displaying the day number
    public TextMeshProUGUI additionalText; // Reference to the TextMeshProUGUI element displaying additional text
    private int currentDay = 0; // Current day
    public float waitforSecondsBeforeNextScene = 2f;

    private void Start()
    {
        // Update the day text
        dayText.text = "DAY " + (currentDay + 1);

        // Update additional text depending on the day
        UpdateAdditionalText(currentDay + 1);

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

    private void UpdateAdditionalText(int day)
    {
        // Update additional text depending on the day
        switch (day)
        {
            case 1:
                additionalText.text = "Welcome to the first day!\n\nTasks to be done:\nThrow wrong items into the dumbster.";
                break;
            case 2:
                additionalText.text = "Things are starting to get interesting...";
                break;
            case 3:
                additionalText.text = "Hope you're having fun!";
                break;
            // Add more cases for additional days as needed
            default:
                additionalText.text = "Enjoy your day!";
                break;
        }
    }
}
