using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// script for destoying the keys
/// </summary>

public class Key : MonoBehaviour
{
    public int LocalKey = KeyCollector.Keys;
    private void Update()
    {
         LocalKey = KeyCollector.Keys;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerController") && LocalKey == 0) //player has the tag PlayerController
        {
            Destroy(gameObject);     //If the player collides with it, and has no keys, is destructed
        }
    }
}
