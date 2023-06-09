using UnityEngine;

public abstract class BaseState
{
    public Enemy enemy;
    public StateMachine stateMachine;
    //instance of statemachine class
    public AudioManager audioManager;
    public abstract void Enter();
    public abstract void Perform();
    public abstract void Exit();

    protected virtual void ShootAudioEnemy()
    {
       
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        audioManager.Play("ShootAudioEnemy");
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene!");
        }


    }

    

}