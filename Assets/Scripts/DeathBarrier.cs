using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBarrier : MonoBehaviour
{
    public PlayerMovement player;
    public GameObject spawn;

    void OnCollisionEnter2D(Collision2D col)
    {
        player.transform.position = spawn.transform.position;
    }

}

