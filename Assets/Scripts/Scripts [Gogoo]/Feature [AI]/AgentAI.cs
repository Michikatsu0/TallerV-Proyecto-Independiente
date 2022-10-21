using System.Collections;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.AI;

public class AgentAI : MonoBehaviour
{
    public enum AIStates { Patroling, Chasing, Catching }
    [Header("AI Agent Patrolling And Chasing")]
    [SerializeField] private Transform _centrePoint;
    [SerializeField] private Transform _targetPos;
    [SerializeField] private AIStates _aIState;
    [SerializeField] private float _range; 
    private NavMeshAgent _agent;


    [Header("AI Agent Field Of View")]
    [SerializeReference] [Range(1, 360)] private float _fov = 45f;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _targetLayer;
    [SerializeField] private LayerMask _obstaclesLayer;
    [SerializeField] private GameObject _playerRef;
    [SerializeField] private GameObject _light;
    [SerializeField] private bool _onSeenPlayer = false;
    private Vector2 _pointFov = Vector2.zero;
    private Vector2 _currentDirectionFov = Vector2.zero;

    [Header("AI Agent Catch")]
    [SerializeField] private float _delayToCatch = 5f;
    [SerializeField] private float _delayToPatrol = 3f;
    private float _timeToCatch = 0;
    private float _timeToPatrol = 0;
    
    public GameObject Light { get => _light; set => _light = value; }

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }


    private void Start()
    {
        _playerRef = GameObject.FindGameObjectWithTag("Player");
        
    }
    public void FieldOfView()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, _radius, _targetLayer);

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 targetDirection = (target.position - transform.position).normalized;
            float targetDistance = Vector2.Distance(transform.position, _targetPos.position);

            if (Vector2.Angle(_currentDirectionFov, targetDirection) < _fov / 2 && targetDistance <= _radius)
            {
                

                if (!Physics2D.Raycast(transform.position, targetDirection, targetDistance, _obstaclesLayer))
                {
                    Debug.DrawRay(transform.position, targetDirection, Color.green);
                    _onSeenPlayer = true;
                    _aIState = AIStates.Chasing;
                }
                else
                    _onSeenPlayer = false;
            }
            else
                _onSeenPlayer = false;
        }
        else if (_onSeenPlayer)
            _onSeenPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AIStateMachine()
    {
        switch (_aIState)
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
        GoToRandomPoint();
        _currentDirectionFov = ((Vector3)_pointFov - _centrePoint.position).normalized;
        if (_currentDirectionFov.x >= 0)
            ChangeLightRotation(-Vector2.Angle(_centrePoint.transform.up, _currentDirectionFov));
        else
            ChangeLightRotation(Vector2.Angle(_centrePoint.transform.up, _currentDirectionFov));
    }
    private void GoToRandomPoint()
    {
        if (_agent.remainingDistance <= _agent.stoppingDistance)
        {
            Vector3 point;
            if (RandomPoint(_centrePoint.position, _range, out point))
            {
                Debug.DrawLine(point, Vector2.one, Color.blue, 1.0f);
                _agent.SetDestination(point);

            }
        }
    }
    private bool RandomPoint(Vector3 center, float range, out Vector3 result)
    {

        Vector2 randomPoint = center + Random.insideUnitSphere * range; //random point in a sphere 
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPoint, out hit, 3.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
        {
            //the 1.0f is the max distance from the random point to a point on the navmesh, might want to increase if range is big
            //or add a for loop like in the documentation
            result = hit.position;
            _pointFov = result;
            return true;
        }
        result = Vector2.zero;
        return false;
    }


    private void MoveToTarget()
    {
        if (_onSeenPlayer)
        {
            

            if (_timeToCatch >= _delayToCatch)
            {
                
                _aIState = AIStates.Catching;
            }
            else
            {
                _timeToPatrol = 0;
                _timeToCatch += Time.deltaTime;
                _agent.SetDestination(_targetPos.position);
                LightChasePlayer();
                FOVChasePlayer();
            }

        }
        else
        {

            if (_timeToPatrol >= _delayToPatrol)
            {
                _aIState = AIStates.Patroling;
                _timeToPatrol = 0;
            }
            else
            {
                _timeToCatch = 0;
                _timeToPatrol += Time.deltaTime;
                _agent.SetDestination(_targetPos.position);
                LightChasePlayer();
                FOVChasePlayer();
            }
        }
        

        
    }

    private void FOVChasePlayer()
    {
        _currentDirectionFov = (_targetPos.position - _centrePoint.position).normalized;
        if (_currentDirectionFov.x >= 0)
            ChangeLightRotation(-Vector2.Angle(_centrePoint.transform.up, _currentDirectionFov));
        else
            ChangeLightRotation(Vector2.Angle(_centrePoint.transform.up, _currentDirectionFov));
    }
    private void LightChasePlayer()
    {
        
        Vector2 targetDirection = (_targetPos.position - _centrePoint.transform.position).normalized;
        if (targetDirection.x >= 0)
            ChangeLightRotation(-Vector2.Angle(_centrePoint.transform.up, targetDirection));
        else
            ChangeLightRotation(Vector2.Angle(_centrePoint.transform.up, targetDirection));
    }


    void ChangeLightRotation(float newRotation)
    {
        _light.transform.rotation = Quaternion.AngleAxis(newRotation, Vector3.forward);
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        GameObject target = other.gameObject;
        if (target.CompareTag("Player"))
        {
            _aIState = AIStates.Catching;
        }
    }
}
