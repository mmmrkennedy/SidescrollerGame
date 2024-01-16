using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCamera : MonoBehaviour
{
    public PlayerMovement player;
    public Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void FixedUpdate () {
        transform.position = new Vector3 (player.transform.position.x + offset.x, player.transform.position.y + offset.y, -10f); // Camera follows the player with specified offset position
    }

}

