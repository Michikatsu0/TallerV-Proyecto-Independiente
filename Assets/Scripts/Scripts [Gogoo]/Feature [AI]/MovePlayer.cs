using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovePlayer : MonoBehaviour
{
    [SerializeField] private Transform _player;
    [SerializeField] private float _speed; 
    Vector3 _direction;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _direction = new Vector3(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1"),0);
        _player.Translate(_direction.normalized * Time.deltaTime * _speed)  ;
    }
}
