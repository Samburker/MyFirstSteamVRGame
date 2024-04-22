using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class RadioController : MonoBehaviour
{
    public CircularDrive powerButton;
    public CircularDrive volumeKnob;
    public AudioSource audioSource;
    public AudioClip[] songs;
    public float minAngle = 30f;
    public float maxAngle = 330f;
    private bool isOn = false;
    private int currentSongIndex = 0;

    private void Start()
    {
        // Turn off the radio initially
        audioSource.Stop();
    }

    private void Update()
    {
        float angle = powerButton.outAngle;

        // Check if the angle is within the limits
        if (angle > minAngle && angle < maxAngle)
        {
            if (!isOn)
            {
                TurnOnRadio();
            }
        }
        else
        {
            if (isOn)
            {
                TurnOffRadio();
            }
        }

        if (isOn)
        {
            AdjustVolume(volumeKnob.outAngle);
        }
    }

    private void AdjustVolume(float angle)
    {
        float volume = Mathf.InverseLerp(minAngle, maxAngle, angle);
        audioSource.volume = volume;
    }

    private void TurnOnRadio()
    {
        isOn = true;
        if (currentSongIndex >= songs.Length)
        {
            currentSongIndex = 0;
        }
        audioSource.clip = songs[currentSongIndex];
        audioSource.Play();
        currentSongIndex++;
    }

    private void TurnOffRadio()
    {
        isOn = false;
        audioSource.Stop();
    }
}
