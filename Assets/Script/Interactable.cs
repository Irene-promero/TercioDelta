using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    
    // Start is called before the first frame update
    public string promptMessage;
    public void BaseInteract()
    {
        Interact();
        
    }
    protected virtual void Interact()
    {
        
    }
}
