using EasyTransition;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EasyTransition
{
    

    public class TrashBin : MonoBehaviour
    {
        public TransitionSettings transition;

        private void OnTriggerEnter(Collider other)
        {
            // Check if the entering game object has the tag "FalseItem"
            if (other.CompareTag("FalseItem"))
            {
                Destroy(other.gameObject);
                Scene currentScene = SceneManager.GetActiveScene();
                TransitionManager.Instance().Transition(currentScene.buildIndex+1, transition, 2f);
            }
        }
    }
}
