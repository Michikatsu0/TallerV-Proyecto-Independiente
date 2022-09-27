using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownWaypointsSystem : MonoBehaviour
{
    [SerializeField] private float speed = 4f;
    [SerializeField] private float chaseTime = 10f;

    [SerializeField] private List<Vector3> waypointList;
    [SerializeField] private List<float> waitTimeList;
    
    private int waypointIndex;

    [SerializeField] private Vector3 aimDirection;

    [SerializeField] private TopDownPlayer player;
    [SerializeField] private Transform pfFieldOfView;
    [SerializeField] private float fov = 90f;
    [SerializeField] private float viewDistance = 50f;

    private TopDownFieldOfView fieldOfView;
    private float setTime;

    private enum State
    {
        Waiting,
        Moving,
        Attack,
    }

    private State state;
    private float waitTimer;
    
    private Vector3 lastMoveDir;

    private void Start()
    {
        state = State.Waiting;
        waitTimer = waitTimeList[0];
        lastMoveDir = aimDirection;

        fieldOfView = Instantiate(pfFieldOfView, null).GetComponent<TopDownFieldOfView>();
        fieldOfView.SetFoV(fov);
        fieldOfView.SetViewDistance(viewDistance);

        setTime = chaseTime;
    }

    private void Update()
    {
        switch (state)
        {
            default:
            case State.Waiting:
            case State.Moving:
                NormalMovementEnemy();
                FindPlayer();
                break;

            case State.Attack:
                AttackPlayer();
                break;

        }

        if (fieldOfView != null)
        {
            fieldOfView.SetOrigin(transform.position);
            fieldOfView.SetAimDirection(GetAimDir());
        }

        //Debug.DrawLine(transform.position, transform.position + GetAimDir() * 10f);
    }

    private void FindPlayer()
    {
        if (Vector3.Distance(GetPosition(), player.GetPosition()) < viewDistance)
        {
            // Player inside viewDistance

            Vector3 dirToPlayer = (player.GetPosition() - GetPosition()).normalized;

            if (Vector3.Angle(GetAimDir(), dirToPlayer) < fov / 2f)
            {
                // Player inside Field of View
                RaycastHit2D raycastHit2D = Physics2D.Raycast(GetPosition(), dirToPlayer, viewDistance);
                Debug.DrawRay(GetPosition(), dirToPlayer * viewDistance);
                if (raycastHit2D.collider != null)
                {
                    // Hit something
                    if (raycastHit2D.collider.gameObject.GetComponentInParent<TopDownPlayer>() != null) 
                    {
                        // Hit Player
                        StartAttackingPlayer();
                    }
                    else 
                    {
                        // Hit something else
                    }
                }
            }
        }
    }

    public void StartAttackingPlayer()
    {
        AttackPlayer();
    }

    private void AttackPlayer()
    {
        state = State.Attack;

        Vector3 targetPosition = player.GetPosition();

        Vector3 dirToTarget = (targetPosition - GetPosition()).normalized;

        lastMoveDir = dirToTarget;

        chaseTime -= Time.deltaTime;

        if (chaseTime <= 0f)
        {
            chaseTime = setTime;

            state = State.Moving;
        }
        else
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, speed * Time.deltaTime);

            if (player == null)
            {
                state = State.Moving;
            }
        }
    }

    private void NormalMovementEnemy()
    {
        switch (state)
        {
            case State.Waiting:

                waitTimer -= Time.deltaTime;

                if (waitTimer <= 0f)
                {
                    state = State.Moving;
                }
                break;

            case State.Moving:

                Vector3 waypoint = waypointList[waypointIndex];

                Vector3 waypointDir = (waypoint - transform.position).normalized;

                lastMoveDir = waypointDir;

                float distanceBefore = Vector3.Distance(transform.position, waypoint);

                transform.position = transform.position + waypointDir * speed * Time.deltaTime;

                float distanceAfter = Vector3.Distance(transform.position, waypoint);

                float arriveDistance = 0.1f;

                if (distanceAfter < arriveDistance || distanceBefore <= distanceAfter)
                {
                    // Go to next waypoint
                    waitTimer = waitTimeList[waypointIndex];
                    waypointIndex = (waypointIndex + 1) % waypointList.Count;
                    state = State.Waiting;
                }

                break;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Vector3 GetAimDir()
    {
        return lastMoveDir;
    }

}