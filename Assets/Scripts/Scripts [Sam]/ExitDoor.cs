using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
/// <summary>
/// When colliding, changes to the end scene
/// player has the tag PlayerController
/// </summary>
public class ExitDoor : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerController")) //player has the tag PlayerController
        {
            SceneManager.LoadScene(3);     //when something with the tag PlayerController collides with something with this script, changes to the designated scene
        }
    } 

}
