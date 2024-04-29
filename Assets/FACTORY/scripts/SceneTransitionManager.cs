using UnityEngine;
using UnityEngine.SceneManagement;
using Valve.VR;
using System.Collections;

public class SceneTransitionManager : MonoBehaviour
{
    public static SceneTransitionManager instance; // Singleton instance

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Keep this object between scene changes
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TransitionToNextScene(int buildIndex)
    {
        // Fade to black
        SteamVR_Fade.Start(Color.black, 2);

        // Load the next scene after a short delay
        StartCoroutine(LoadNextScene(buildIndex));
    }

    private IEnumerator LoadNextScene(int buildIndex)
    {
        // Wait for a short duration to let the fade effect take place
        yield return new WaitForSeconds(2.0f);

        // Load the next scene asynchronously
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(buildIndex);

        // Wait until the asynchronous scene fully loads
        while (!asyncLoad.isDone)
        {
            yield return null;
        }

        // Fade back in
        SteamVR_Fade.Start(Color.clear, 1);
    }

}
