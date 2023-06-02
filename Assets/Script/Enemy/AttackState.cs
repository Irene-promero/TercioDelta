using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;


public class AttackState : BaseState
{
    private float moveTimer;
    private float losePlayerTimer;
    private float shotTimer;
  
    
    
    public override void Enter()
    {
    }

    public override void Exit()
    {
        
    }

    public override void Perform()
    {
        if(enemy.CanSeePlayer())//player can be seen.
        {
            losePlayerTimer = 0;
            moveTimer += Time.deltaTime;
            shotTimer += Time.deltaTime;
            enemy.transform.LookAt(enemy.Player.transform);
            //if shot timer > firerate
            if(shotTimer > enemy.fireRate)
            {
                Shoot();
                base.ShootAudio();
            }
            if(moveTimer > Random.Range(3, 7))
            {
                enemy.Agent.SetDestination(enemy.transform.position + (Random.insideUnitSphere * 5));
                moveTimer = 0;
            }
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if(losePlayerTimer > 8)
            {
                //CHange to the search state.
                stateMachine.ChangeState(new PatrolState()); 
            }
        }
    }

    public void Shoot()
    {
        

        Transform gunbarrel = enemy.gunBarrel;
        GameObject bullet = GameObject.Instantiate(Resources.Load("Prefabs/Bullet") as GameObject, gunbarrel.position, enemy.transform.rotation);
       
        Vector3 shootDirection = (enemy.Player.transform.position - gunbarrel.transform.position).normalized;
        bullet.GetComponent<Rigidbody>().velocity = shootDirection * 50;
        
        
        shotTimer= 0;
    }

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
