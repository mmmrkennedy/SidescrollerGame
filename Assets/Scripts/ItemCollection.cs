using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemCollection : MonoBehaviour
{
    public PlayerMovement player;
    public PlayerInventory inventory; 
    public GameObject item;

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collected Item");
        inventory.coinCount++;
        Destroy(item);
    }
}
