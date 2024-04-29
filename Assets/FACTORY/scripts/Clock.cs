using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using EasyTransition;
using Valve.VR;

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
    private bool dayEnded = false; // Flag to track whether the day has ended

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
        else if (!dayEnded) // Check if the day hasn't ended yet
        {
            // Set the flag to indicate that the day has ended
            dayEnded = true;

            // Transition to the next scene
            Scene currentScene = SceneManager.GetActiveScene();
            SceneTransitionManager.instance.TransitionToNextScene(currentScene.buildIndex + 1);
        }
    }


}
