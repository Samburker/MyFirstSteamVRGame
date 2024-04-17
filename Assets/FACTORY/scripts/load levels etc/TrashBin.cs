using UnityEngine;
using UnityEngine.SceneManagement;

public class TrashBin : MonoBehaviour
{
    public string transitionSceneName = "TransitionScene"; // Name of the transition scene

    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering game object has the tag "FalseItem"
        if (other.CompareTag("FalseItem"))
        {
            // Load the transition scene
            SceneManager.LoadScene(transitionSceneName);
        }
    }
}
