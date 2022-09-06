using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TopDownMoveEnemy : MonoBehaviour
{
    [SerializeField] private float speedMove = 4f;
    [SerializeField] private Transform[] patrolPoints;
    [SerializeField] private float minDistance = 0.2f;

    private int randomNum;

    [SerializeField] public Transform player;

    [SerializeField] private float rangeVision;

    Rigidbody2D rb2d;


    private bool onTarget = false;

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
            ChasePlayer();
        }
        else
        {
            StopChasePlayer();
        }

        
    }

    private void ChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, player.position, speedMove * Time.deltaTime);

        if (Vector2.Distance(transform.position, patrolPoints[randomNum].position) < minDistance)
        {
            randomNum = Random.Range(0, patrolPoints.Length);
        }
    }
    private void StopChasePlayer()
    {
        transform.position = Vector2.MoveTowards(transform.position, patrolPoints[randomNum].position, speedMove * Time.deltaTime);
        
        if (Vector2.Distance(transform.position, patrolPoints[randomNum].position) < minDistance)
        {
            randomNum = Random.Range(0, patrolPoints.Length);
        }
    }
}
