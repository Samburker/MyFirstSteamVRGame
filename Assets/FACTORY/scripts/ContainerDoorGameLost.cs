using EasyTransition;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace EasyTransition
{

    public class ContainerDoorGameLost : MonoBehaviour
    {
        public float delayBeforeClosing = 3f; // Delay before the door starts closing
        public float closingSpeed = 90f; // Speed at which the door closes (degrees per second)
        public Canvas gameEndCanvas; // Reference to the Canvas containing the game end text
        public AudioClip closingSound; // Sound to play when the door starts closing
        private AudioSource audioSource;
        private bool isOpen = false;
        public AudioClip disciplineVoiceLine;
        public TransitionSettings transition;

        // Start is called before the first frame update
        void Start()
        {
            gameEndCanvas.gameObject.SetActive(false);
            audioSource = GetComponent<AudioSource>();
            StartCoroutine(CloseDoorDelayed());
            audioSource.PlayOneShot(disciplineVoiceLine);
        }

        IEnumerator CloseDoorDelayed()
        {
            yield return new WaitForSeconds(delayBeforeClosing);

            if (closingSound != null)
                audioSource.PlayOneShot(closingSound);

            float initialYRotation = transform.rotation.eulerAngles.y;
            float targetYRotation = initialYRotation - 127f;

            while (Mathf.Abs(Mathf.DeltaAngle(transform.rotation.eulerAngles.y, targetYRotation)) > 0.01f)
            {
                float newYRotation = Mathf.MoveTowardsAngle(transform.rotation.eulerAngles.y, targetYRotation, closingSpeed * Time.deltaTime);
                transform.rotation = Quaternion.Euler(0f, newYRotation, 0f);
                yield return null;
            }

            transform.rotation = Quaternion.Euler(0f, targetYRotation, 0f);
            isOpen = false;

            // Activate the game end canvas
            gameEndCanvas.gameObject.SetActive(true);

            //yield return new WaitForSeconds(3f); // Change the delay time as needed
            TransitionManager.Instance().Transition(0,transition, 2f);

        }
    }
}
