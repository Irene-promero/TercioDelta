using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Transform respawnPoint;
 

    public void RespawnHere()
    {
        player.transform.position = respawnPoint.transform.position;
        Physics.SyncTransforms();
    }
}
