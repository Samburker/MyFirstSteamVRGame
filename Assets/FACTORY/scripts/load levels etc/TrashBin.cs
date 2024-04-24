using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EasyTransition
{
    

    public class TrashBin : MonoBehaviour
    {
        public TransitionSettings transition;
        public string transitionSceneName = "TransitionScene"; // Name of the transition scene

        private void OnTriggerEnter(Collider other)
        {
            // Check if the entering game object has the tag "FalseItem"
            if (other.CompareTag("FalseItem"))
            {
                Scene currentScene = SceneManager.GetActiveScene();
                TransitionManager.Instance().Transition(currentScene.buildIndex+1, transition, 2f);
            }
        }
    }
}
