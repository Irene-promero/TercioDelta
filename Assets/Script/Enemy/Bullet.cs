using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int damage = 10;
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTran = collision.transform;
        // if(hitTransform.CompareTar("Player"))

        if (collision.gameObject.CompareTag("Player"))
        {
            // Apply damage to the player or handle the collision accordingly
            PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth >();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(Random.Range(5, 10));
            }
        }

        //{ Debug.Log("Hit Player");

        Debug.Log("Bullet");
        //    hitTransform.GetComponent<PlayerHealth>().TakeDamage(10); }


        Destroy(gameObject);
    }
}
