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

    [Header("AI Agent Variables")]
    [SerializeField] private Transform _centrePoint;
    [SerializeField] private Transform _targetPos;
    [SerializeField] private AIStates _aIState;
    [SerializeField] private float _range; 
    private NavMeshAgent _agent;

    [Header("AI Agent Field Of View")]
    [Range(1, 360)] public float _fov = 45f;
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
    

    public AIStates AIState { get => _aIState; set => _aIState = value; }
    public bool OnSeenPlayer { get => _onSeenPlayer; set => _onSeenPlayer = value; }

    void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
    }


    private void Start()
    {
        _playerRef = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(FOVCheckOut());
        
    }

    private IEnumerator FOVCheckOut()
    {
        WaitForSeconds waitFor = new WaitForSeconds(0.2f);
        while (true)
        {
            yield return waitFor;
            FieldOfView();
        }
    }
    private void FieldOfView()
    {
        Collider2D[] rangeCheck = Physics2D.OverlapCircleAll(transform.position, _radius, _targetLayer);

        if (rangeCheck.Length > 0)
        {
            Transform target = rangeCheck[0].transform;
            Vector2 targetDirection = (target.position - transform.position).normalized;

            if (Vector2.Angle(_currentDirectionFov, targetDirection) < _fov / 2)
            {
                float targetDistance = Vector2.Distance(transform.position, _targetPos.position);

                if (!Physics2D.Raycast(transform.position, targetDirection, targetDistance, _obstaclesLayer))
                {
                    _onSeenPlayer = true;
                    _aIState = AIStates.Chasing;
                }
                else
                    OnSeenPlayer = false;
            }
            else
                OnSeenPlayer = false;
        }
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
        if (NavMesh.SamplePosition(randomPoint, out hit, 1.0f, NavMesh.AllAreas)) //documentation: https://docs.unity3d.com/ScriptReference/AI.NavMesh.SamplePosition.html
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
        if (OnSeenPlayer)
        {
            _timeToPatrol = 0;
            _timeToCatch += Time.deltaTime;
            OnSeenPlayerEvent();

        }
        else
        {
            _timeToCatch = 0;
            _timeToPatrol += Time.deltaTime;
            if (_timeToPatrol >= _delayToPatrol)
            {
                AIState = AIStates.Patroling;
            }
        }
        

        
    }
    private void OnSeenPlayerEvent()
    {
        
        if (_timeToCatch >= _delayToCatch)
        {
            AIState = AIStates.Catching;
            _timeToCatch = 0;
        }
        else
        {
            _agent.SetDestination(_targetPos.position);

            Vector2 targetDirection = (_targetPos.position - _centrePoint.transform.position).normalized;
            if (targetDirection.x >= 0)
                ChangeLightRotation(-Vector2.Angle(_centrePoint.transform.up, targetDirection));
            else
                ChangeLightRotation(Vector2.Angle(_centrePoint.transform.up, targetDirection));
        }
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
            AIState = AIStates.Catching;
        }
    }
}
