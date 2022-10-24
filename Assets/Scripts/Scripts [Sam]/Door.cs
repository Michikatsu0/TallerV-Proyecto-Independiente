using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// The door, when having a key and colliding, is destroyed
/// </summary>

public class Door : MonoBehaviour
{
   
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerController") && KeyCollector.Keys > 0) //player has the tag PlayerController
        {
            Destroy(gameObject);     //If the player collides with the door while having a key, destroys the key
        }
    }
}
