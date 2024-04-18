using UnityEngine;

public class TestDie : MonoBehaviour
{
    public Zombie zombie; // Reference to the Zombie script

    void Update()
    {
        // Check if spacebar is pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Zombie zombie = FindAnyObjectByType<Zombie>();
            zombie.EnableRagdoll(true);
        }
    }
}
