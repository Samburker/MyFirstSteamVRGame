using UnityEngine;

public class CanvasAppearanceController : MonoBehaviour
{
    public GameObject canvasObject;
    public float appearanceDelay = 5f; // Delay in seconds before the canvas appears

    void Start()
    {
        canvasObject.SetActive(false);
        Invoke("ShowCanvas", appearanceDelay);
       
    }

    void ShowCanvas()
    {
        canvasObject.SetActive(true);
    }
}
