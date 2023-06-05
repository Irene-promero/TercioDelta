 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletEnemy : MonoBehaviour
{
    public int damage = 10;

    void OnCollisionEnter(Collision collision)
    {
        // Check if the bullet collided with the player
        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player or handle the collision accordingly
            
            PlayerHealthAttribute playerHealth = collision.gameObject.GetComponent<PlayerHealthAttribute>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
                
            }
        }

        // Destroy the bullet on collision with any object
        
    }
}