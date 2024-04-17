using UnityEngine;
using UnityEngine.SceneManagement;

public class DaySystemManager : MonoBehaviour
{
    public float dayDurationInSeconds = 60f; // Duration of each day in seconds
    private int currentDay = 1; // Current day
    private float currentTime = 0f; // Current time within the day

    private void Update()
    {
        currentTime += Time.deltaTime;

        if (currentTime >= dayDurationInSeconds)
        {
            EndDay();
        }
    }

    private void EndDay()
    {
        currentDay++;
        SceneManager.LoadScene("TransitionScene");
        currentTime = 0f; // Reset current time
    }

}
