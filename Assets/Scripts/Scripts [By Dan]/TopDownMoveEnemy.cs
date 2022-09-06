using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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
            //In case the distance between the player and the enemy is less than the enemy's view range, execute
            ChasePlayer();
        }
        else
        {
            //In case the distance between the player and the enemy is greater than the enemy's view range, execute
            StopChasePlayer();
        }        
    }
    //Execution of the patrol system
    private void ChasePlayer()
    {
        //Move to selected patrol point
        transform.position = Vector2.MoveTowards(transform.position, player.position, speedMove * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoints[randomNum].position) < minDistance)
        {
            //Random choice of new patrol point
            randomNum = Random.Range(0, patrolPoints.Length);
        }
    }
    //Running the tracking system
    private void StopChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[randomNum].position, speedMove * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, patrolPoints[randomNum].position) < minDistance)
        {
            randomNum = Random.Range(0, patrolPoints.Length);
        }
    }
}
