
using UnityEngine;

public class Explosion : MonoBehaviour
{

    public GameObject destroyedVersion;
    public GameObject efectoDeExplosion;
    public float radius = 5f;
    public float force = 700f;
    
    
    // Start is called before the first frame update
    public float health = 30f;
    
    
    public void TakeDamage (float amout)
    {  health -= amout; 
        if (health <= 0f)
        {
            ExplotarCosas();
        }
    }

    void ExplotarCosas()
    {
    Instantiate(efectoDeExplosion, transform.position, transform.rotation);


        Collider[] collidersToDestroy = Physics.OverlapSphere(transform.position, radius);
        Destroy(gameObject);
        foreach (Collider nearbyObject in collidersToDestroy)
        {
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if (dest != null)
            {
                dest.Destroy();
            }
        }

        Collider[] collidersToMove = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToMove)
        {
            Rigidbody rb = nearbyObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                rb.AddExplosionForce(force, transform.position, radius);
            }
        }

        Destroy(gameObject);
    }
}
