
using UnityEngine;

public class PlayerHealthAttribute : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;


    private void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damageAmount)
    {

        PlayerHealth playerHealth = gameObject.GetComponent<PlayerHealth>();
        currentHealth -= damageAmount;
        playerHealth.TakeDamage(Random.Range(5, 7.5f));
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        // Handle player death (e.g., game over, reset level, etc.)
        Debug.Log("Player died!");
    }
}