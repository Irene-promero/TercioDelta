using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletPlayer : MonoBehaviour
{
    Vector3 lastPosition;
    public float speed = 200f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        lastPosition = transform.position;
        transform.position += transform.forward * speed * Time.deltaTime;
    }
}
