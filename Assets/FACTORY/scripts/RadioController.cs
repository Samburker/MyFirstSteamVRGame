using System.Collections;
using UnityEngine;
using Valve.VR;

public class RadioController : MonoBehaviour
{
    public SteamVR_Action_Boolean toggleRadioAction;
    public SteamVR_Action_Boolean adjustVolumeAction;
    public AudioSource radioAudioSource;
    public AudioClip[] songs;
    private int currentSongIndex = 0;
    private bool isRadioOn = false;
    private float startAngle = 0f;
    private float previousAngle = 0f;
    private const float ANGLE_THRESHOLD = 5f;
    private bool isRotating = false;

    void OnEnable()
    {
        toggleRadioAction.AddOnStateDownListener(ToggleRadio, SteamVR_Input_Sources.Any);
        adjustVolumeAction.AddOnStateDownListener(AdjustVolume, SteamVR_Input_Sources.Any);
    }

    void OnDisable()
    {
        toggleRadioAction.RemoveOnStateDownListener(ToggleRadio, SteamVR_Input_Sources.Any);
        adjustVolumeAction.RemoveOnStateDownListener(AdjustVolume, SteamVR_Input_Sources.Any);
    }

    private void ToggleRadio(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        isRadioOn = !isRadioOn;

        if (isRadioOn)
        {
            radioAudioSource.clip = songs[currentSongIndex];
            radioAudioSource.Play();
        }
        else
        {
            radioAudioSource.Stop();
        }
    }

    private void AdjustVolume(SteamVR_Action_Boolean fromAction, SteamVR_Input_Sources fromSource)
    {
        if (isRadioOn)
        {
            radioAudioSource.volume = Mathf.Clamp01(radioAudioSource.volume + 0.1f);
        }
    }

    private void Update()
    {
        if (isRadioOn && !isRotating)
        {
            float currentAngle = transform.localEulerAngles.y;

            if (currentAngle - previousAngle < -180)
            {
                currentAngle += 360;
            }
            else if (currentAngle - previousAngle > 180)
            {
                currentAngle -= 360;
            }

            float deltaAngle = currentAngle - previousAngle;

            if (Mathf.Abs(deltaAngle) > ANGLE_THRESHOLD)
            {
                isRotating = true;

                if (deltaAngle < 0)
                {
                    currentSongIndex--;
                    if (currentSongIndex < 0)
                    {
                        currentSongIndex = songs.Length - 1;
                    }
                }
                else
                {
                    currentSongIndex++;
                    if (currentSongIndex >= songs.Length)
                    {
                        currentSongIndex = 0;
                    }
                }

                radioAudioSource.clip = songs[currentSongIndex];
                radioAudioSource.Play();

                StartCoroutine(ResetRotation());
            }

            previousAngle = currentAngle;
        }
    }

    private IEnumerator ResetRotation()
    {
        yield return new WaitForSeconds(0.5f);
        isRotating = false;
    }
}
