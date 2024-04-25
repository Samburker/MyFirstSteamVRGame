using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using EasyTransition;

public class Clock : MonoBehaviour
{
    public Text timerText; // Reference to the Text element displaying the timer

    public string startTime = "06:00"; // Start time in 24-hour clock format (HH:mm)
    public string endTime = "18:00"; // End time in 24-hour clock format (HH:mm)
    public float dayLengthInSeconds = 180f; // Length of the day in seconds
    private DateTime startTimeDT; // Start time as DateTime
    private DateTime endTimeDT; // End time as DateTime
    private float timeElapsed = 0f; // Time elapsed since the start of the day
    public TransitionSettings transition;

    private void Start()
    {
        // Convert start and end times from string to DateTime
        startTimeDT = DateTime.ParseExact(startTime, "HH:mm", null);
        endTimeDT = DateTime.ParseExact(endTime, "HH:mm", null);
    }

    private void Update()
    {
        // Update the timer text
        if (timeElapsed < dayLengthInSeconds)
        {
            // Calculate time elapsed since the start of the day
            timeElapsed += Time.deltaTime;
            float ratio = timeElapsed / dayLengthInSeconds;
            TimeSpan timeToAdd = TimeSpan.FromSeconds(ratio * (endTimeDT - startTimeDT).TotalSeconds);
            DateTime currentTime = startTimeDT.Add(timeToAdd);
            timerText.text = currentTime.ToString(@"HH\:mm");
        }
        else
        {
            timerText.text = endTime;
            // Load next scene when timer ends
            int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
            TransitionManager.Instance().Transition(currentSceneIndex+1, transition, 2f);
          
        }
    }
}
