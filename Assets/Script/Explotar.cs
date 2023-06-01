using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explotar : MonoBehaviour
{
    bool haExplotado = false;

    public GameObject destroyedVersion;
    public GameObject efectoDeExplosion;
    public float radius = 5f;
    public float force = 700f;

 

    // Start is called before the first frame update
    void OnCollisionEnter(Collision coll)
    {

        if (coll.gameObject.tag == "Bullet" && !haExplotado)
        {
            Instantiate(destroyedVersion, transform.position, transform.rotation);
            ExplotarCosas ();
            Destroy(gameObject);
            haExplotado=true;
            
        }
    }
    void ExplotarCosas ()
    {
        Instantiate(efectoDeExplosion, transform.position, transform.rotation);
       
        
        Collider[] collidersToDesroy = Physics.OverlapSphere(transform.position, radius);

        foreach (Collider nearbyObject in collidersToDesroy)
        {
            Destructible dest = nearbyObject.GetComponent<Destructible>();
            if (dest != null )
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
