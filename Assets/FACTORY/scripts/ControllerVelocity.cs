// Controller Velocity script (ControllerVelocity.cs)
using UnityEngine;
using Valve.VR;

public class ControllerVelocity : MonoBehaviour
{
    private SteamVR_Behaviour_Pose controllerPose;

    private void Start()
    {
        // Get reference to the SteamVR controller pose script
        controllerPose = GetComponent<SteamVR_Behaviour_Pose>();
    }

    public float GetControllerVelocity()
    {
        // Return the velocity of the controller
        if (controllerPose != null)
        {
            return controllerPose.GetVelocity().magnitude;
        }
        else
        {
            return 0f;
        }
    }

    private void Update()
    {
        /*Äif (controllerPose != null)
        {
            // Print the magnitude of the controller's velocity
            Debug.Log("Controller Velocity Magnitude: " + controllerPose.GetVelocity().magnitude);
        }*/
    }

}
