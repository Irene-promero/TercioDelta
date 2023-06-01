using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnCollisionEnter(Collision collision)
    {
        Transform hitTran = collision.transform;
       // if(hitTransform.CompareTar("Player"))
        //{ Debug.Log("Hit Player");
        //    hitTransform.GetComponent<PlayerHealth>().TakeDamage(10); }
        Destroy(gameObject);
    }
}
