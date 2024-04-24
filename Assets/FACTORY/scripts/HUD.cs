using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    public Text healthText; // Reference to the UI Text element for displaying health
    public Health playerHealth; // Reference to the Health script attached to the player

    void Start()
    {
        // Get the Health component from the player GameObject
        playerHealth = GameObject.FindWithTag("Player").GetComponent<Health>();

        // Update the health text initially
        UpdateHealthText();
    }

    // Update health text with current health value
    void UpdateHealthText()
    {

        healthText.text = PlayerStats.currentHealth.ToString();
    }

    void Update()
    {
        // Update health text if health has changed
        UpdateHealthText();
    }
}
