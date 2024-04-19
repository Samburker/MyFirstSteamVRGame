using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 3; // Maximum health points
    public int currentHealth; // Current health points

    // Initialize health points
    private void Start()
    {
        currentHealth = maxHealth;
    }
    private void Update()
    {
      
    }


    // Method to decrease health points
    public void TakeDamage(int damageAmount)
    {
        // Reduce health by the specified amount
        currentHealth -= damageAmount;

        // Check if health has reached zero
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    // Method to increase health points (optional)
    public void Heal(int healAmount)
    {
        // Increase health by the specified amount
        currentHealth += healAmount;

        // Ensure health doesn't exceed maximum
        currentHealth = Mathf.Min(currentHealth, maxHealth);
    }

    // Method called when health reaches zero
    private void Die()
    {
        // Perform any actions for player death, such as game over, respawn, etc.
        Debug.Log("Player has died!");
        // Example: GameManager.Instance.GameOver();
    }

}
