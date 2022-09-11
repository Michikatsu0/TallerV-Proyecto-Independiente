using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 
/// This script takes care of the A.I. of the enemy allowing him to patrol the map through patrol points located on the map and at the moment in which the player is a short distance from the enemy, he recognizes the distance and begins the chase.
/// 
/// </summary>
public class TopDownMoveEnemy : MonoBehaviour
{
    [SerializeField] private float speedMove = 4f;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float minDistance = 0.2f;
    [SerializeField] public Transform player;
    [SerializeField] private float rangeVision;

    private int randomNum;

    Rigidbody2D rb2d;

    private void Start()
    {
        rb2d = GetComponent<Rigidbody2D>();

        randomNum = Random.RandomRange(0, patrolPoints.Length);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }
    private void Update()
    {
        float distToPlayer = Vector2.Distance(transform.position, player.position);

        if (distToPlayer < rangeVision)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.position, speedMove * Time.deltaTime);
        }
        else
        {
            transform.position = Vector2.MoveTowards(transform.position, patrolPoints[randomNum].position, speedMove * Time.deltaTime);

            if (Vector2.Distance(transform.position, patrolPoints[randomNum].position) < minDistance)
            {
                randomNum = Random.Range(0, patrolPoints.Length);
            }
        }
    }
}
