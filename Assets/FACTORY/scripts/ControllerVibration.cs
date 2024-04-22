using System.Collections;
using UnityEngine;
using Valve.VR;

public class ControllerVibration : MonoBehaviour
{
    public SteamVR_Action_Vibration hapticAction;

    // Call this method to vibrate the controllers briefly
    public void TriggerHapticPulse(float duration, float frequency, float amplitude)
    {
        StartCoroutine(LongVibration(duration, frequency, amplitude));
    }

    IEnumerator LongVibration(float length, float strength, float frequency)
    {
        for (float i = 0; i < length; i += Time.deltaTime)
        {
            hapticAction.Execute(0, strength, frequency, 1, SteamVR_Input_Sources.LeftHand);
            hapticAction.Execute(0, strength, frequency, 1, SteamVR_Input_Sources.RightHand);
            yield return null;
        }
    }
}
