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
        if (other.CompareTag("PlayerController")) //player has the tag PlayerController
        {
                
            gameObject.GetComponent<SpriteRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider2D>().enabled = false; //If the player collides with it, the key visually disappears and looses the hitbox

        }
    }
}
