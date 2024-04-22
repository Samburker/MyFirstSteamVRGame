using UnityEngine;
using TMPro;
using System.Collections.Generic;

[System.Serializable]
public struct Subtitle
{
    public float time;
    public string text;
}

public class SubtitleController : MonoBehaviour
{
    public TMP_Text subtitleText;
    public AudioClip audioClip;
    public List<Subtitle> subtitles;

    private AudioSource audioSource;
    private bool isPlaying;
    private bool isStarted;

    public float startDelay = 2f; // Delay in seconds before audio starts

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = audioClip;
        isPlaying = false;
        isStarted = false;

        Invoke("StartAudioWithSubtitles", startDelay);
    }

    void Update()
    {
        if (isStarted && isPlaying)
        {
            float currentTime = audioSource.time;
            subtitleText.text = GetSubtitle(currentTime);

            // Stop updating the subtitle text when the audio ends
            if (!audioSource.isPlaying)
            {
                isPlaying = false;
                subtitleText.text = "";
            }
        }
    }

    void StartAudioWithSubtitles()
    {
        isStarted = true;
        isPlaying = true;
        audioSource.Play();
    }

    string GetSubtitle(float currentTime)
    {
        // Your logic to get the subtitle based on the current time of the audio
        // For example:
        foreach (Subtitle sub in subtitles)
        {
            if (sub.time >= currentTime)
            {
                return sub.text;
            }
        }
        return "";
    }
}
