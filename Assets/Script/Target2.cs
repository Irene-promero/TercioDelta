using UnityEngine;

public class Target2 : MonoBehaviour
{
    public float radius = 5f;
    public float force = 700f;
    public GameObject destroyedVersion2;
    // Start is called before the first frame update
    public float health = 30f;



    public void TakeDamage2(float amout)
    {
        health -= amout;
        if (health <= 0f)
        { Die2(); }
    }

    void Die2()
    {
        Instantiate(destroyedVersion2, transform.position, transform.rotation);


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
