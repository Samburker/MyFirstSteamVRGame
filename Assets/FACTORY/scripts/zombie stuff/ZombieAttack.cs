using UnityEngine;

public class ZombieAttack : MonoBehaviour
{
    public float attackRange = 2f; // Distance at which the zombie will start attacking
    public float attackCooldown = 2f; // Cooldown between attacks
    public float attackDamage = 1; // Amount of damage the zombie's attack inflicts
    public Animator animator; // Reference to the Animator component for controlling animations
    private Health playerHealth; // Reference to the player's Health script

    private bool canAttack = true; // Flag to control attack cooldown

    void Start()
    {
        // Find the player GameObject and get its Health component
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    void Update()
    {
        // Calculate the distance between the zombie and the player
        float distanceToPlayer = Vector3.Distance(transform.position, playerHealth.transform.position);

        // If the player is within attack range and attack is off cooldown, initiate attack
        if (distanceToPlayer <= attackRange && canAttack)
        {
            Attack();
        }
        // If the player moves out of attack range, switch to walk animation
        else if (distanceToPlayer > attackRange)
        {
            SwitchToWalkAnimation();
        }
    }

    // Method to handle the zombie's attack
    void Attack()
    {
        // Play attack animation
        if (animator != null)
        {
            animator.SetTrigger("Attack");
            ZombieAudio zombieAudio = GetComponent<ZombieAudio>();
            zombieAudio.PlayZombieHitSound();
        }

        // Inflict damage to the player's health
        if (playerHealth != null)
        {
            playerHealth.TakeDamage(Mathf.FloorToInt(attackDamage));
        }

        // Start attack cooldown
        canAttack = false;
        Invoke("ResetAttackCooldown", attackCooldown);
    }

    // Method to reset the attack cooldown
    void ResetAttackCooldown()
    {
        canAttack = true;
    }

    // Method to switch back to the walk animation
    void SwitchToWalkAnimation()
    {
        if (animator != null)
        {
            animator.ResetTrigger("Attack");
            animator.SetTrigger("Walk");
        }
    }
}
