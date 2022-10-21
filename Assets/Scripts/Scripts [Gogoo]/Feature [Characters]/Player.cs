using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Player Variables")]
    [SerializeField] private float _speed;
    private Rigidbody2D _rigidbody2D;
    private Vector3 _direction;


    void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _rigidbody2D.velocity = new Vector3(_direction.x * _speed, _direction.y * _speed);
    }

    public void DirectionMovement()
    {
        _direction = new Vector3(Input.GetAxis("Horizontal1"), Input.GetAxis("Vertical1"), 0);
    }


}
