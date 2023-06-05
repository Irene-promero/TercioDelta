using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ammo : Interactable
{
    public Gun _gun;

    // Start is called before the first frame update
    void Start()
    {
        
 _gun = GameObject.Find("M4_Carbine").GetComponent<Gun>();


    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //this function is where we will design our interaction using code
    protected override void Interact()
    {
        
        _gun.AddMaxAmmo(25); 
        Destroy(gameObject);
       
    }
}
