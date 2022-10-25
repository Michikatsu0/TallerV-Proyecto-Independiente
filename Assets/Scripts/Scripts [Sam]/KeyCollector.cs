using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// controls the keys. Until we have the inventory system we will use this
/// starts with 0 keys, when touching one key it takes it
/// When having one key and touching a door, destroys the door
/// </summary>

public class KeyCollector : MonoBehaviour
{
     public static int Keys = 0;
    private void Start()
    {
        Keys = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("TheKey")) //player has the tag PlayerController
        {
            Keys = Keys + 1;    //pick up One key
        }

        if (other.CompareTag("LockedDoor") && Keys>0) //player has the tag PlayerController
        {
            Keys = Keys - 1;    //Use one key

        }
    }

}

