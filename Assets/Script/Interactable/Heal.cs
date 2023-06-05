using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : Interactable
{
    public PlayerHealth playerHealth;

    // Start is called before the first frame update
    void Start()
    {
        
        playerHealth =GameObject.Find("Player").GetComponent<PlayerHealth>();

        


    }

    // Update is called once per frame
    void Update()
    {

    }
    //this function is where we will design our interaction using code
    protected override void Interact()
    {
        playerHealth.RestoreHealth(25);
        Destroy(gameObject);

    }
}