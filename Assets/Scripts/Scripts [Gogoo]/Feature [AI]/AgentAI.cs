using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour
{
    public enum AIStates { Patroling, Chasing, Catching }

    [SerializeField] private Transform _targetPos;
    [SerializeField] private AIStates _aIState;
    
    private NavMeshAgent _agent;

    public AIStates AIState { get => _aIState; set => _aIState = value; }

    public float _range; //radius of sphere
    public Transform _centrePoint;
    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }

    // Update is called once per frame
    void Update()
    {
        AIStateMachine();
    }

    private void AIStateMachine()
    {
        switch (AIState)
        {
            case AIStates.Patroling:
                Patroling();
                break;
            case AIStates.Chasing:
                MoveToTarget();
                break;
            case AIStates.Catching:

                break;
        }
    }

    private void Patroling()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance) //done with path
        {
            Vector3 point;
            if (RandomPoint(_centrePoint.position, _range, out point)) //pass in our centre point and radius of area
            {
                Debug.DrawRay(point, Vector2.one, Color.blue, 1.0f); //so you can see with gizmos
                _agent.SetDestination(point);
            }
        }
    }

    bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector2 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            return true;
        }

        result = Vector2.zero;
        return false;
    }

    private void FieldOfView()
    {
        
    }

    private void MoveToTarget()
    {
        _agent.SetDestination(_targetPos.position);
    }
    
    
   

    private void OnTriggerEnter(Collider other)
    {
        GameObject target = other.gameObject;
        if (other.CompareTag("Player"))
        {

        }
    }




}
