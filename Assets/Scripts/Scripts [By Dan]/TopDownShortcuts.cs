using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 
/// This script makes that when the player collides with the "TriggerIn" of the portal, the player's position variable is transformed to that of a point "exit"
/// 
/// </summary>

public class TopDownShortcuts : MonoBehaviour
{
    [SerializeField] public GameObject player;
    [SerializeField] public GameObject exit;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            player.transform.position = exit.transform.position;
        }
    }
}
